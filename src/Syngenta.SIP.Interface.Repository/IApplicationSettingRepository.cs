// ***********************************************************************
// <copyright file="IApplicationSettingRepository.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Interface.Repository
{
    using Syngenta.SIP.Models;

    /// <summary>
    /// interface ICryptoRepository
    /// </summary>
    public interface IApplicationSettingRepository
    {
        /// <summary>
        /// Gets the name of the key value by.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns>returns model</returns>
        ApplicationSettingModel GetApplicationSettingByKeyName(string keyName);

        /// <summary>
        /// Saves the application setting.
        /// </summary>
        /// <param name="applicationSettingModel">The application setting model.</param>
        /// <returns> returns string </returns>
        string SaveApplicationSetting(ApplicationSettingModel applicationSettingModel);
    }
}
