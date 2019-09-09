// <copyright file="LogService.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Implementation.Service
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using log4net;
    using Syngenta.SIP.Interface.Repository;
    using Syngenta.SIP.Interface.Service;

    /// <summary>
    /// class LogService
    /// </summary>
    /// <seealso cref="Syngenta.SIP.Interface.Service.ILogService" />
    public class LogService : ILogService
    {
        /// <summary>
        /// The syngenta unit of work
        /// </summary>
        private readonly ISyngentaSIPUnitOfWork syngentaUnitOfWork;

        /// <summary>
        /// The log
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(ConfigurationManager.AppSettings["LoggerName"]);

        /// <summary>
        /// Initializes a new instance of the <see cref="LogService" /> class.
        /// </summary>
        /// <param name="syngentaUnitOfWork">The syngenta unit of work.</param>
        public LogService(ISyngentaSIPUnitOfWork syngentaUnitOfWork)
        {
            if (this.syngentaUnitOfWork == null)
            {
                this.syngentaUnitOfWork = syngentaUnitOfWork;
            }
        }

        /// <summary>
        /// Gets all Countries list.
        /// </summary>
        /// <param name="message">The Message</param>
        public void LogInfo(string message)
        {
            this.log.Info(message);
        }

        /// <summary>
        /// Downs the load data.
        /// </summary>
        /// <param name="message">The Message</param>
        /// <param name="exception">The Exception</param>
        /// <returns>
        /// returns error
        /// </returns>
        public string LogError(string message, Exception exception)
        {
            var apiName = "Unknown";
            var methodName = "Unknown";
            try
            {
                var stackTrace = new StackTrace(exception);

                var frames = stackTrace.GetFrames().Where(x => x.GetMethod().Module.Name.StartsWith("Syngenta")).Select(x => x.GetMethod()).ToList();
                if (frames != null && frames.Count > 0)
                {
                    apiName = frames[frames.Count - 1].DeclaringType.FullName + "." + frames[frames.Count - 1].Name;
                    methodName = frames[0].DeclaringType.FullName + "." + frames[0].Name;
                }
            }
            catch (Exception)
            {
            }

            return this.LogError(apiName, methodName, exception.Message, exception);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="api">The API.</param>
        /// <param name="currentMethod">The current method.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>returns log error</returns>
        public string LogError(string api, string currentMethod, string message, Exception exception)
        {
            var uniqueId = Guid.NewGuid().ToString();
            log4net.LogicalThreadContext.Properties["ReferenceId"] = uniqueId;
            log4net.LogicalThreadContext.Properties["API"] = api;
            log4net.LogicalThreadContext.Properties["Method"] = currentMethod;
            this.log.Error(message, exception);
            return uniqueId;
        }
    }
}
