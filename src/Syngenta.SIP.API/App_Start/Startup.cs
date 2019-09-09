// ***********************************************************************
// <copyright file="Startup.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

[assembly: Microsoft.Owin.OwinStartup(typeof(SyngentaSIP.API.App_Start.Startup))]

namespace SyngentaSIP.API.App_Start
{
    using System.Configuration;
    using System.IdentityModel.Tokens;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.ActiveDirectory;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OpenIdConnect;
    using Owin;

    /// <summary>
    /// The Startup class
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// The application identifier
        /// </summary>
        private static string applicationId = ConfigurationManager.AppSettings["azure:ApplicationId"];

        /// <summary>
        /// The ad instance
        /// </summary>
        private static string aadInstance = ConfigurationManager.AppSettings["azure:AADInstance"];

        /// <summary>
        /// The tenant identifier
        /// </summary>
        private static string tenantId = ConfigurationManager.AppSettings["azure:TenantId"];

        /// <summary>
        /// The post logout redirect URI
        /// </summary>
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["azure:PostLogoutRedirectUri"];

        /// <summary>
        /// The post logout redirect URI
        /// </summary>
        private static string redirectUri = ConfigurationManager.AppSettings["azure:RedirectUri"];

        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }

        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        private void ConfigureAuth(IAppBuilder app)
        {            
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = applicationId,
                    Authority = string.Format(aadInstance, tenantId),
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    RedirectUri = redirectUri,
                });

            var tokenValidationParameter = new TokenValidationParameters();
            tokenValidationParameter.ValidAudiences = new string[] { applicationId };
            tokenValidationParameter.ValidateIssuer = false;
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(new WindowsAzureActiveDirectoryBearerAuthenticationOptions
            {
                TokenValidationParameters = tokenValidationParameter,
                Tenant = tenantId,
            });

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}
