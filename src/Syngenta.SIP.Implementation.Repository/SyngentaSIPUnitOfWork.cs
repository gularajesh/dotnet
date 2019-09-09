// ***********************************************************************
// <copyright file="SyngentaSIPUnitOfWork.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Repository
{
    using System;
    using Syngenta.SIP.Interface.Repository;

    /// <summary>
    /// SyngentaSIPUnitOfWork class
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Repository.ISyngentaSIPUnitOfWork" />
    public class SyngentaSIPUnitOfWork : ISyngentaSIPUnitOfWork
    {
        /// <summary>
        /// The _syngenta context
        /// </summary>
        private readonly ISyngentaSIPContext syngentaSIPContext;

        /// <summary>
        /// The syngenta sip security
        /// </summary>
        private readonly ISyngentaSIPSecurityContext syngentaSIPSecurity;

        /// <summary>
        /// The _questionnaire map repository
        /// </summary>
        private readonly Func<IUserRepository> userRepository;

        /// <summary>
        /// The plan repository
        /// </summary>
        private readonly Func<IPlanRepository> planRepository;
        
        /// <summary>
        /// The data repository
        /// </summary>
        private readonly Func<IDataRepository> dataRepository;

        /// <summary>
        /// The crypto repository
        /// </summary>
        private readonly Func<IApplicationSettingRepository> applicationSettingRepository;

        /// <summary>
        /// The document repository
        /// </summary>
        private readonly Func<IDocumentRepository> documentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyngentaSIPUnitOfWork"/> class.
        /// </summary>
        /// <param name="syngentaSIPContext">The syngenta sip context.</param>
        /// <param name="syngentaSIPSecurity">The syngenta sip security.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="planRepository">The plan repository.</param>
        /// <param name="dataRepository">The data repository.</param>
        /// <param name="applicationSettingRepository">The application setting repository.</param>
        public SyngentaSIPUnitOfWork(
            ISyngentaSIPContext syngentaSIPContext,
            ISyngentaSIPSecurityContext syngentaSIPSecurity,
            Func<IUserRepository> userRepository, 
            Func<IPlanRepository> planRepository,
            Func<IDataRepository> dataRepository,
            Func<IApplicationSettingRepository> applicationSettingRepository,
            Func<IDocumentRepository> documentRepository)
        {
            this.syngentaSIPContext = syngentaSIPContext;
            this.syngentaSIPSecurity = syngentaSIPSecurity;
            this.userRepository = userRepository;
            this.planRepository = planRepository;
            this.dataRepository = dataRepository;
            this.applicationSettingRepository = applicationSettingRepository;
            this.documentRepository = documentRepository;
        }

        /// <summary>
        /// Gets the users repository.
        /// </summary>
        /// <value>
        /// The users repository.
        /// </value>
        public IUserRepository UserRepository
        {
            get { return this.userRepository(); }
        }

        /// <summary>
        /// Gets the plan repository.
        /// </summary>
        /// <value>
        /// The plan repository.
        /// </value>
        public IPlanRepository PlanRepository
        {
            get { return this.planRepository(); }
        }

        /// <summary>
        /// Gets the data repository.
        /// </summary>
        /// <value>
        /// The data repository.
        /// </value>
        public IDataRepository DataRepository
        {
            get { return this.dataRepository(); }
        }

        /// <summary>
        /// Gets the crypto repository.
        /// </summary>
        /// <value>
        /// The crypto repository.
        /// </value>
        public IApplicationSettingRepository ApplicationSettingRepository
        {
            get { return this.applicationSettingRepository(); }
        }

        /// <summary>
        /// Gets the document repository.
        /// </summary>
        /// <value>
        /// The document repository.
        /// </value>
        public IDocumentRepository DocumentRepository
        {
            get { return this.documentRepository(); }
        }
    }
}
