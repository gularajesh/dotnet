// ***********************************************************************
// <copyright file="ExcelService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Syngenta.SIP.Implementation.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;

    /// <summary>
    /// Defines the <see cref="ExcelService" />
    /// </summary>
    public class ExcelService
    {
        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <param name="cellReference">The cell reference.</param>
        /// <returns>returns column name</returns>
        public static string GetColumnName(string cellReference)
        {
            //// Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }

        /// <summary>
        /// Gets the name of the column index from.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>returns column index name</returns>
        public static int? GetColumnIndexFromName(string columnName)
        {
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }

            return number;
        }

        /// <summary>
        /// Creates the excel file.
        /// </summary>
        /// <param name="ms">The memory stream.</param>
        /// <param name="action">The <see cref="Action{WorkbookPart}" /></param>
        public void CreateExcelFile(System.IO.MemoryStream ms, Action<WorkbookPart> action)
        {
            using (SpreadsheetDocument spreedDoc = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workBookPart = spreedDoc.WorkbookPart;
                if (workBookPart == null)
                {
                    workBookPart = spreedDoc.AddWorkbookPart();
                    workBookPart.Workbook = new Workbook();
                }

                if (workBookPart.Workbook.Sheets == null)
                {
                    workBookPart.Workbook.AppendChild<Sheets>(new Sheets());
                }

                action(workBookPart);
                workBookPart.Workbook.Save();
            }
        }

        /// <summary>
        /// To the data table.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="headerRowNumber">The header row number.</param>
        /// <param name="rowNumberToStart">The row number to start.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <param name="columnsToRead">The columns to read.</param>
        /// <returns>
        /// returns data table
        /// </returns>
        public DataTable ToDataTable(Stream stream, int headerRowNumber = 0, int rowNumberToStart = 0, string sheetName = "", params string[] columnsToRead)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, false))
            {
                DataTable result = this.ExcelToDataTable(doc, headerRowNumber, rowNumberToStart, sheetName);
               
                    if (columnsToRead != null && columnsToRead.Length > 0)
                    {
                        var columnsToRemove = new List<DataColumn>();
                        foreach (DataColumn column in result.Columns)
                        {
                            if (!columnsToRead.Contains(column.ColumnName))
                            {
                                columnsToRemove.Add(column);
                            }
                        }

                        columnsToRemove.ForEach(x => result.Columns.Remove(x));
                    return result.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
                }
                else
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// Excels to data table.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="headerRowNumber">The header row number.</param>
        /// <param name="rowNumberToStart">The row number to start.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <returns>
        /// returns data table
        /// </returns>
        public DataTable ExcelToDataTable(SpreadsheetDocument doc, int headerRowNumber = 0, int rowNumberToStart = 0, string sheetName = "")
        {
            DataTable dt = new DataTable();
            Sheet sheet = doc.WorkbookPart.Workbook.Sheets.ChildElements[doc.WorkbookPart.Workbook.Sheets.Count() - 1] as Sheet;
            if (!string.IsNullOrEmpty(sheetName))
            {
                sheet = doc.WorkbookPart.Workbook.Sheets.ChildElements.Where(a => a.GetAttribute("name", string.Empty).Value == sheetName).FirstOrDefault() as Sheet;
                if (sheet == null)
                {
                    return dt;
                }
            }

            ////Get the Worksheet instance.
            Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

            ////Fetch all the rows present in the Worksheet.
            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
            List<string> columnRef = new List<string>();
            ////Loop through the Worksheet rows.
            foreach (Row row in rows)
            {
                ////Use the first row to add columns to DataTable.
                if (row.RowIndex.Value == headerRowNumber + 1)
                {
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        string colName = this.GetValue(doc, cell);
                        if (!string.IsNullOrEmpty(colName))
                        {
                            dt.Columns.Add(colName);
                            columnRef.Add(cell.CellReference.ToString().Substring(0, cell.CellReference.ToString().Length - 1));
                        }
                    }
                }
                else if (row.RowIndex.Value >= (rowNumberToStart + 1))
                {
                    DataRow tempRow = dt.NewRow();
                    int columnIndex = 0;
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        //// Gets the column index of the cell with data
                        if (columnIndex < dt.Columns.Count)
                        {
                            try
                            {
                                int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell.CellReference));
                                cellColumnIndex--; ////zero based index
                                if (columnIndex < cellColumnIndex)
                                {
                                    do
                                    {
                                        tempRow[columnIndex] = string.Empty; ////Insert blank data here;
                                        columnIndex++;
                                    }
                                    while (columnIndex < cellColumnIndex);
                                }

                                tempRow[columnIndex] = this.GetValue(doc, cell);
                            }
                            catch
                            {
                            }
                            finally
                            {
                                columnIndex++;
                            }
                        }
                    }

                    dt.Rows.Add(tempRow);
                }
            }

            return dt;
        }        

        /// <summary>
        /// Adds the data table.
        /// </summary>
        /// <param name="workBookPart">The <see cref="WorkbookPart" /></param>
        /// <param name="sheetName">The <see cref="string" /></param>
        /// <param name="dataTable">The <see cref="DataTable" /></param>
        public void AddDataTable(WorkbookPart workBookPart, string sheetName, DataTable dataTable)
        {
            WorksheetPart worksheetPart = workBookPart.AddNewPart<WorksheetPart>();

            var sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);

            var sheet = new Sheet()
            {
                Id = workBookPart.GetIdOfPart(worksheetPart),
                SheetId = (uint)workBookPart.WorksheetParts.Count(),
                Name = sheetName
            };

            workBookPart.Workbook.Sheets.AppendChild(sheet);

            uint rowindex = 1;
            Row excelHeaderRow = new Row();
            excelHeaderRow.RowIndex = rowindex;
            sheetData.AppendChild(excelHeaderRow);
            foreach (DataColumn column in dataTable.Columns)
            {
                excelHeaderRow.AppendChild(this.GetCellWithText(column.ColumnName));
            }

            rowindex++;
            foreach (DataRow row in dataTable.Rows)
            {
                Row excelRow = new Row();
                excelRow.RowIndex = rowindex;
                foreach (DataColumn column in dataTable.Columns)
                {
                    excelRow.AppendChild(this.GetCellWithText(Convert.ToString(row[column])));
                }

                sheetData.AppendChild(excelRow);
                rowindex++;
            }
        }

        /// <summary>
        /// Gets the cell with text.
        /// </summary>
        /// <param name="text">The <see cref="string" /></param>
        /// <returns>
        /// The <see cref="Cell" />
        /// </returns>
        private Cell GetCellWithText(string text)
        {
            Cell c1 = new Cell();
            c1.DataType = CellValues.InlineString;

            InlineString inlineString = new InlineString();
            Text t = new Text();
            t.Text = text;
            inlineString.AppendChild(t);

            c1.AppendChild(inlineString);
            return c1;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="cell">The cell.</param>
        /// <returns>returns value</returns>
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            try
            {
                if (cell == null || cell.CellValue == null)
                {
                    return string.Empty;
                }

                string value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }

                return value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
