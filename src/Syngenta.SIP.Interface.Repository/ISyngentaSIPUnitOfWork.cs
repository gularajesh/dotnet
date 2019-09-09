// ***********************************************************************
// <copyright file="ISyngentaSIPUnitOfWork.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Interface.Repository
{
    /// <summary>
    /// ISyngentaSIPUnitOfWork class
    /// </summary>
    public interface ISyngentaSIPUnitOfWork
    {
        /// <summary>
        /// Gets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        IUserRepository UserRepository { get; }

        /// <summary>
        /// Gets the plan repository.
        /// </summary>
        /// <value>
        /// The plan repository.
        /// </value>
        IPlanRepository PlanRepository { get; }

        /// <summary>
        /// Gets the data repository.
        /// </summary>
        /// <value>
        /// The data repository.
        /// </value>
        IDataRepository DataRepository { get; }

        /// <summary>
        /// Gets the crypto repository.
        /// </summary>
        /// <value>
        /// The crypto repository.
        /// </value>
        IApplicationSettingRepository ApplicationSettingRepository { get; }


        /// <summary>
        /// Gets the document repository.
        /// </summary>
        /// <value>
        /// The document repository.
        /// </value>
        IDocumentRepository DocumentRepository { get; }
    }
}
