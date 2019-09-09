// ***********************************************************************
// <copyright file="IDocumentService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Interface.Service
{    
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using Syngenta.SIP.Models;

    /// <summary>
    /// interface IDocumentService
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// Uploads the user document.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="stream">The stream.</param>
        void UploadUserDocument(string containerName, string fileName, Stream stream);

        /// <summary>
        /// Reads the user document.
        /// </summary>
        /// <param name="prefixName">PrefixName of the file.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>
        /// returns user document
        /// </returns>
        IDictionary<string, byte[]> ReadUserDocument(int year, string prefixName, int skip, int take);

        /// <summary>
        /// Uploads the country document.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="stream">The stream.</param>
        void UploadCountryDocument(string containerName, string fileName, Stream stream);

        /// <summary>
        /// Reads the country document.
        /// </summary>
        /// <param name="countryName">Name of the country.</param>
        /// <returns>
        /// returns country document
        /// </returns>
        byte[] ReadCountryDocument(int year, string countryName);

        /// <summary>
        /// Tracks the user document download.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="documentName">Name of the document.</param>
        /// <param name="documentType">Type of the document.</param>
        /// <param name="year">The year.</param>
        void TrackUserDocumentDownload(UserModel userData, string documentName, string documentType, int year);

        /// <summary>
        /// Generates the user track document file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="year">The year.</param>
        /// <param name="countryId">The country identifier.</param>
        void GenerateUserTrackDocumentFile(System.IO.MemoryStream path, int year, int countryId = 0);
    }
}