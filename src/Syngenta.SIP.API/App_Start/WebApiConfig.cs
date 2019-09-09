// ***********************************************************************
// <copyright file="WebApiConfig.cs" company="Syngenta">
//     Copyright ©  2018
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace SyngentaSIP.API
{
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Microsoft.Owin.Security.OAuth;
    using Newtonsoft.Json;
    using Syngenta.SIP.API;

    /// <summary>
    /// class Web API Config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
           config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new GlobalExceptionHandler());
            //// Web API routes
            config.MapHttpAttributeRoutes();
            //// Enable Cors
            ////config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.Routes.MapHttpRoute(
                name: "Api_1",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "{controller}", action = "{action}", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            //// config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new XmlMediaTypeFormatter());
            ////config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            //// remove null values from JSON
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }
    }
}
