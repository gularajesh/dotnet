// <copyright file="IStorageService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Interface.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// IStorageService Service
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="name">The name.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="encryptionKey">The encryption key.</param>
        void AddFile(string containerName, string name, Stream fileStream, string encryptionKey = null);

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="name">The name.</param>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <returns>
        /// returns Bytes
        /// </returns>
        byte[] ReadFile(string containerName, string name, string encryptionKey = null);

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="prefixName">Name of the prefix.</param>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>
        /// returns Bytes
        /// </returns>
        IDictionary<string, byte[]> ReadFilesByPrefix(string containerName, string prefixName,string userId , string encryptionKey = null, int skip = 0, int take = 0);
        
        /// <summary>
        /// Gets the list of documents.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns></returns>
        IList<string> GetListOfDocuments(string containerName, string folderName);
    }
}
