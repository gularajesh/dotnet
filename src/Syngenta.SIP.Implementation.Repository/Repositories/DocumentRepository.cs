// ***********************************************************************
// <copyright file="DocumentRepository.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Repository
{
    using System.Data;
    using System.Data.SqlClient;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Models;

    /// <summary>
    /// DocumentRepository Class
    /// </summary>
    public class DocumentRepository : IDocumentRepository
    {
        /// <summary>
        /// The syngenta context
        /// </summary>
        private readonly ISyngentaSIPContext syngentaSIPContext;

        /// <summary>
        /// The syngenta sip security context
        /// </summary>
        private readonly ISyngentaSIPSecurityContext syngentaSIPSecurityContext;

        /// <summary>
        /// The syngenta sip unit of work
        /// </summary>
        private readonly ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentRepository"/> class.
        /// </summary>
        /// <param name="syngentaSIPContext">The syngenta sip context.</param>
        /// <param name="syngentaSIPSecurityContext">The syngenta sip security context.</param>
        /// <param name="syngentaSIPUnitOfWork">The syngenta sip unit of work.</param>
        public DocumentRepository(ISyngentaSIPContext syngentaSIPContext, ISyngentaSIPSecurityContext syngentaSIPSecurityContext, ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork)
        {
            this.syngentaSIPContext = syngentaSIPContext;
            this.syngentaSIPSecurityContext = syngentaSIPSecurityContext;
            this.syngentaSIPUnitOfWork = syngentaSIPUnitOfWork;
        }

        /// <summary>
        /// Saves the track document download.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="documentName">Name of the document.</param>
        /// <param name="documentType">document type</param>
        /// <param name="year"> year of document</param>
        public void SaveTrackDocumentDownload(UserModel userData, string documentName, string documentType, int year)
        {
             this.syngentaSIPContext.ExecuteProcedure("Proc_DocumentAccessRecordInsert", new SqlParameter("@UserId", userData.Id), new SqlParameter("@DocumentName", documentName), new SqlParameter("@DocumentType", documentType), new SqlParameter("@Year", year));
        }

        /// <summary>
        /// Gets the user downloaded documents.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetUserDownloadedDocuments(int countryId, int year)
        {
            return this.syngentaSIPContext.ExecuteProcedure("Proc_GetUserDownloadedDocuments", new SqlParameter("@CountryId", countryId), new SqlParameter("@year", year));
        }
    }
}