// ***********************************************************************
// <copyright file="StorageService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;

    /// <summary>
    /// Blob Service
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Service.IStorageService" />
    public class StorageService : IStorageService
    {
        /// <summary>
        /// The syngenta sip unit of work
        /// </summary>
        private readonly ICryptoService cryptoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageService" /> class.
        /// </summary>
        /// <param name="cryptoService">The crypto service.</param>
        public StorageService(ICryptoService cryptoService)
        {
            this.cryptoService = cryptoService;
        }

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="name">The name.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="encryptionKey">The encryption key.</param>
        public void AddFile(string containerName, string name, Stream fileStream, string encryptionKey = null)
        {
            var container = this.GetContainer(containerName);
            var blob = container.GetBlockBlobReference(name);
            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                blob.UploadFromStream(fileStream);
            }
            else
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    var encryptedBytes = this.cryptoService.Encrypt(encryptionKey, memoryStream.ToArray());
                    blob.UploadFromByteArray(encryptedBytes, 0, encryptedBytes.Length);
                }
            }
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="name">The <see cref="string" /></param>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <returns>
        /// The <see cref="byte[]" />
        /// </returns>
        public byte[] ReadFile(string containerName, string name, string encryptionKey = null)
        {
            var container = this.GetContainer(containerName);
            var blob = container.GetBlockBlobReference(name);
            if (blob.Exists())
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    blob.DownloadToStream(stream);

                    var result = stream.ToArray();
                    if (string.IsNullOrWhiteSpace(encryptionKey))
                    {
                        return result;
                    }

                    return this.cryptoService.Decrypt(encryptionKey, result);
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Reads the file by File Prefix Name.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="prefixName">The <see cref="string" /></param>
        /// <param name="userId"> user's id</param>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>
        /// The <see cref="byte[]" />
        /// </returns>
        public IDictionary<string, byte[]> ReadFilesByPrefix(string containerName, string prefixName, string userId, string encryptionKey = null, int skip = 0, int take = 0)
        {
            IDictionary<string, byte[]> dict = new Dictionary<string, byte[]>();
            var container = this.GetContainer(containerName);
            var listBlobs = container.ListBlobs(string.Concat(prefixName, userId.ToString())).OfType<CloudBlockBlob>().OrderByDescending(x => x.Properties.LastModified).ToList();
            if (listBlobs.Count > 0)
            {
                foreach (var blobItem in listBlobs)
                {
                    string key = blobItem.Name.Substring(Constants.BlobSetting.UserStorageContainer.Length + 1, blobItem.Name.Length - Constants.BlobSetting.UserStorageContainer.Length - 1);
                    byte[] value = this.ReadFile(containerName, blobItem.Name, encryptionKey);
                    dict.Add(key, value);
                }
            }
            else
            {
                return null;
            }

            return dict;
        }

        /// <summary>
        /// Gets the list of documents.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns> List of Documents</returns>
        public IList<string> GetListOfDocuments(string containerName, string folderName)
        {
            if(containerName.Equals("0"))
            {
                containerName = DateTime.UtcNow.Year.ToString();
            }

            CloudBlobContainer blobContainer = this.GetContainer(containerName);
            IList<string> listOfDocuments = new List<string>();
            var listOfBlobs = blobContainer.ListBlobs(string.Concat(folderName, "/")).OfType<CloudBlockBlob>().ToList();

            foreach (var blob in listOfBlobs)
            {
                listOfDocuments.Add(blob.Name.Replace(string.Concat(folderName, "/"), string.Empty).Trim());
            }

            return listOfDocuments;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>
        /// Cloud Blob Container
        /// </returns>
        private CloudBlobContainer GetContainer(string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(Constants.BlobSetting.BlobStorageAppSettingName));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            return container;
        }
    }
}