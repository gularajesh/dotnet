// <copyright file="ILogService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Interface.Service
{
    using System;

    /// <summary>
    /// interface ILogService
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>returns error</returns>
        string LogError(string message, Exception exception);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="api">The API.</param>
        /// <param name="currentMethod">The current method.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>returns error</returns>
        string LogError(string api, string currentMethod, string message, Exception exception);
    }
}
