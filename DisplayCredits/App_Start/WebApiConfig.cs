using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DisplayCredits
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Serialization hatası için eklendi
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling =
            Newtonsoft.Json.PreserveReferencesHandling.Objects;

            // Web API yapılandırması ve hizmetleri

            // Web API yolları
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
