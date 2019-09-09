// ***********************************************************************
// <copyright file="Utilities.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Models
{
    using System;
    using System.Data;
    using System.Reflection;

    /// <summary>
    /// Utilities class
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// To the data table.
        /// </summary>
        /// <typeparam name="T">The items</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>returns data table</returns>
        public static DataTable ToDataTable<T>(T items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            ////Get all the properties
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                ////Defining type of data column gives proper data table 
                var type = prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType;
                ////Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    ////inserting property values to datatable rows
                    values[i] = props[i].GetValue(items, null);
                }

                dataTable.Rows.Add(values);            
            ////put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
