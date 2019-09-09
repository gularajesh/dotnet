// ***********************************************************************
// <copyright file="ApplicationSettingService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Implementation.Service
{
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Interface.Service;
    using Syngenta.SIP.Models;

    /// <summary>
    /// class ApplicationSettingService
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Service.IApplicationSettingService" />
    public class ApplicationSettingService : IApplicationSettingService
    {
        /// <summary>
        /// The syngenta sip unit of work
        /// </summary>
        private readonly ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingService"/> class.
        /// </summary>
        /// <param name="syngentaSIPUnitOfWork">The syngenta sip unit of work.</param>
        public ApplicationSettingService(ISyngentaSIPUnitOfWork syngentaSIPUnitOfWork)
        {
            if (this.syngentaSIPUnitOfWork == null)
            {
                this.syngentaSIPUnitOfWork = syngentaSIPUnitOfWork;
            }
        }

        /// <summary>
        /// Saves the application setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// key if passed then it returns value successfully other wise empty
        /// </returns>
        public string SaveApplicationSetting(string key)
        {
            ApplicationSettingModel applicationSettingModel = new ApplicationSettingModel();
            if (!string.IsNullOrWhiteSpace(key))
            {
                applicationSettingModel.KeyName = Constants.ApplicationSettingCodes.CryptoKey;
                applicationSettingModel.Value = key;
            }

            return this.syngentaSIPUnitOfWork.ApplicationSettingRepository.SaveApplicationSetting(applicationSettingModel);
        }

        /// <summary>
        /// Gets the name of the application setting by key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns> Application key</returns>
        public ApplicationSettingModel GetApplicationSettingByKeyName(string keyName)
        {
            var applicationSettings = this.syngentaSIPUnitOfWork.ApplicationSettingRepository.GetApplicationSettingByKeyName(keyName);
            return applicationSettings;
        }
    }
}
