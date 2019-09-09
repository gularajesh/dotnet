// ***********************************************************************
// <copyright file="ApplicationSettingRepository.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Implementation.Repository
{
    using System.Linq;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Models;

    /// <summary>
    /// class CryptoRepository
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Repository.IApplicationSettingRepository" />
    public class ApplicationSettingRepository : IApplicationSettingRepository
    {
        /// <summary>
        /// The syngenta context
        /// </summary>
        private readonly ISyngentaSIPSecurityContext syngentaSIPSecurityContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingRepository"/> class.
        /// </summary>
        /// <param name="syngentaSIPSecurity">The syngenta sip security.</param>
        public ApplicationSettingRepository(ISyngentaSIPSecurityContext syngentaSIPSecurity)
        {
            this.syngentaSIPSecurityContext = syngentaSIPSecurity;
        }

        /// <summary>
        /// Gets the name of the key value by.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns>
        /// <see cref="ApplicationSettingModel"/>
        /// </returns>
        public ApplicationSettingModel GetApplicationSettingByKeyName(string keyName)
        {
            var applicationSettings = this.syngentaSIPSecurityContext.ApplicationSettings.Where(x => x.KeyName == keyName).FirstOrDefault();
            return applicationSettings;
        }

        /// <summary>
        /// Saves the application setting.
        /// </summary>
        /// <param name="applicationSettingModel">The application setting model.</param>
        /// <returns>
        /// returns string
        /// </returns>
        public string SaveApplicationSetting(ApplicationSettingModel applicationSettingModel)
        {
            var applicationSetting = this.syngentaSIPSecurityContext.ApplicationSettings.Where(x => x.KeyName == applicationSettingModel.KeyName).OrderByDescending(x => x.Id).FirstOrDefault();
            if (applicationSetting == null)
            {
                this.syngentaSIPSecurityContext.ApplicationSettings.Add(applicationSettingModel);
                this.syngentaSIPSecurityContext.Save();
                return "Genarated Key Successfully";
            }

            return "There is Already a Key";
        }
    }
}
