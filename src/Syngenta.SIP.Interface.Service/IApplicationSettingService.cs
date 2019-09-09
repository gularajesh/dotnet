// ***********************************************************************
// <copyright file="IApplicationSettingService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Interface.Service
{
    using Syngenta.SIP.Models;

    /// <summary>
    /// interface IApplicationSettingService
    /// </summary>
    public interface IApplicationSettingService
    {
        /// <summary>
        /// Saves the application setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// <see cref="string"/>
        /// </returns>
        string SaveApplicationSetting(string key);

        /// <summary>
        /// Gets the name of the application setting by key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns>
        /// <see cref="ApplicationSettingModel"/>
        /// </returns>
        ApplicationSettingModel GetApplicationSettingByKeyName(string keyName);
    }
}
