// ***********************************************************************
// <copyright file="IDocumentRepository.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Interface.Repository
{
    using System.Data;
    using Syngenta.SIP.Models;    

    /// <summary>
    /// IDocumentRepository class
    /// </summary>
    public interface IDocumentRepository
    {
        /// <summary>
        /// Saves the track document download.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="documentName">Name of the document.</param>
        void SaveTrackDocumentDownload(UserModel userData, string documentName, string documentType,int year);

        /// <summary>
        /// Gets the user downloaded documents.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        DataTable GetUserDownloadedDocuments(int countryId, int year);             
    }
}
