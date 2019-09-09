// ***********************************************************************
// <copyright file="DocumentService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Implementation.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using Ionic.Zip;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;
    using System.Linq;

    /// <summary>
    /// class DocumentService
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Service.IDocumentService" />
    public class DocumentService : IDocumentService
    {
        /// <summary>
        /// The storage service
        /// </summary>
        private IStorageService storageService = null;

        /// <summary>
        /// The syngenta sip unit of work
        /// </summary>
        private readonly ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork;

        /// <summary>
        /// The application setting service
        /// </summary>
        private IApplicationSettingService applicationSettingService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentService"/> class.
        /// </summary>
        /// <param name="storageService">The storage service.</param>
        /// <param name="applicationSettingService">The application setting service.</param>
        public DocumentService(IStorageService storageService, IApplicationSettingService applicationSettingService, ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork)
        {
            this.storageService = storageService;
            this.applicationSettingService = applicationSettingService;
            this.syngentaSIPUnitOfWork = syngentaSIPUnitOfWork;
        }

        /// <summary>
        /// Uploads the user document.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="stream">The stream.</param>
        public void UploadUserDocument(string ContainerName,string fileName, Stream stream)
        {
            string encryptionKey = this.applicationSettingService.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey).Value;
            this.UploadDocument(ContainerName, fileName, stream, encryptionKey);
        }

        /// <summary>
        /// Reads the user document.
        /// </summary>
        /// <param name="prefixName">PrefixName of the file.</param>
        /// <param name="skip">The skip</param>
        /// <param name="take">The take</param>
        /// <returns>
        /// returns user document
        /// </returns>
        public IDictionary<string, byte[]> ReadUserDocument(int year, string prefixName, int skip, int take)
        {
            string fileName = prefixName;
            string encryptionKey = this.applicationSettingService.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey).Value;
            return this.storageService.ReadFilesByPrefix(year.ToString(), string.Concat(Constants.BlobSetting.UserStorageContainer,"/"),prefixName ,encryptionKey, skip, take);
        }       

        /// <summary>
        /// Uploads the country document.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="stream">The stream.</param>
        public void UploadCountryDocument(string containerName,string fileName, Stream stream)
        {
            string encryptionKey = this.applicationSettingService.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey).Value;
            this.UploadDocument(containerName, fileName, stream, encryptionKey);
        }

        /// <summary>
        /// Reads the country document.
        /// </summary>
        /// <param name="countryName">Name of the country.</param>
        /// <returns>
        /// returns country document
        /// </returns>
        public byte[] ReadCountryDocument(int year, string countryName)
        {
            string fileName = countryName + ".pdf";
            string encryptionKey = this.applicationSettingService.GetApplicationSettingByKeyName(Constants.ApplicationSettingCodes.CryptoKey).Value;
            return this.ReadDocument(year, Constants.BlobSetting.CountryStorageContainer, fileName, encryptionKey);
        }
        /// <summary>
        /// Uploads the document.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="encryptionKey">The encryption key.</param>
        private void UploadDocument(string containerName, string fileName, Stream stream, string encryptionKey)
        {
            if(containerName.Equals("0"))
            {
                containerName = DateTime.UtcNow.Year.ToString(); 
            }
            
            var allowedExtensions = new[] { ".pdf" };
            if (string.Equals(Path.GetExtension(fileName), ".pdf", StringComparison.InvariantCultureIgnoreCase))
            {
                this.storageService.AddFile(containerName, fileName, stream, encryptionKey);
            }
            else if (string.Equals(Path.GetExtension(fileName), ".zip", StringComparison.InvariantCultureIgnoreCase))
            {
                using (ZipFile zipFile = ZipFile.Read(stream))
                {
                    foreach (ZipEntry entry in zipFile.Entries)
                    {
                        if (entry.IsDirectory)
                        {
                            continue;
                        }

                        using (var entryStream = entry.OpenReader())
                        {
                            var checkextension = Path.GetExtension(entry.FileName).ToLower();
                            if (checkextension == allowedExtensions[0])
                            {
                                FileInfo info = new FileInfo(entry.FileName);
                                this.storageService.AddFile(containerName, info.Name, entryStream, encryptionKey);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reads the document.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <returns>
        /// returns document from container
        /// </returns>
        private byte[] ReadDocument(int year, string containerName, string fileName, string encryptionKey)
        {
            return this.storageService.ReadFile(year.ToString(), string.Concat(containerName,"/",fileName), encryptionKey);
        }

        /// <summary>
        /// Tracks the user document download.
        /// </summary>
        public void TrackUserDocumentDownload(UserModel userData, string documentName, string documentType, int year)
        {
            this.syngentaSIPUnitOfWork.DocumentRepository.SaveTrackDocumentDownload(userData, documentName, documentType, year);
        }

        /// <summary>
        /// Gets the user downloaded documents.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataTable GetUserDownloadedDocuments(int countryId)
        {
            return this.syngentaSIPUnitOfWork.DocumentRepository.GetUserDownloadedDocuments(countryId,Constants.CurrentYear);
        }

        /// <summary>
        /// Generates the user track document file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void GenerateUserTrackDocumentFile(MemoryStream path, int year, int countryId = 0)
        {
            DataTable documentsReport = this.syngentaSIPUnitOfWork.DocumentRepository.GetUserDownloadedDocuments(countryId,year);

            IList<string> userDocumentsList = this.storageService.GetListOfDocuments(year.ToString(),Constants.BlobSetting.UserStorageContainer);
            IList<string> countryDocumentsList = this.storageService.GetListOfDocuments(year.ToString(), Constants.BlobSetting.CountryStorageContainer);

            string employeeId = string.Empty;
            string isUserDocumentUploaded = string.Empty;
            string isCountryDocumentUploaded = string.Empty;
            foreach (DataRow row in documentsReport.Rows)
            {
                employeeId = Convert.ToString(row["EmployeeID"]);
                isUserDocumentUploaded= Convert.ToString(row["Is User Document Uploaded"]);
                isCountryDocumentUploaded = Convert.ToString(row["Is Country Document Uploaded"]);
                if (string.IsNullOrEmpty(employeeId).Equals(false))
                {
                    if (isUserDocumentUploaded.ToUpper().Equals("NO") &&  userDocumentsList.Any(x => x.StartsWith(string.Concat(employeeId, "_"))) )
                    {
                        row["Is User Document Uploaded"] = "Yes";

                    }
                    if (isCountryDocumentUploaded.ToUpper().Equals("NO") && countryDocumentsList.Any(x => x.ToUpper().StartsWith(row["Country"].ToString().ToUpper())))
                    {
                        row["Is Country Document Uploaded"] = "Yes";

                    }
                }               
            }
            
            var excelService = new ExcelService();
            excelService.CreateExcelFile(
                path,
                (DocumentFormat.OpenXml.Packaging.WorkbookPart workBookPart) =>
                {                    
                    excelService.AddDataTable(workBookPart, "DocumentsReport", documentsReport);
                });
        }
    }
}