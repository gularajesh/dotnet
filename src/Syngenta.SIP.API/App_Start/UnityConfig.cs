// ***********************************************************************
// <copyright file="UnityConfig.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace SyngentaSIP.API
{
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    /// <summary>
    /// Unity Config
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Gets the unity container.
        /// </summary>
        /// <value>
        /// The unity container.
        /// </value>
        /// <author>
        /// santosh
        /// </author>
        /// <datetime>5/26/2016 12:23 PM</datetime>
        public static IUnityContainer UnityContainer { get; private set; }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        /// <author>
        /// santosh
        /// </author>
        /// <datetime>5/26/2016 12:23 PM</datetime>
        public static void Configure()
        {
            UnityContainer = CreateConfiguredUnityContainer();
            UnityServiceLocator locator = new UnityServiceLocator(UnityContainer);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        /// <summary>
        /// Creates the configured unity container.
        /// </summary>
        /// <returns>
        /// The Container
        /// </returns>
        /// <author>
        /// santosh
        /// </author>
        /// <datetime>5/26/2016 12:23 PM</datetime>
        private static IUnityContainer CreateConfiguredUnityContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration();
            return container;
        }
    }
}