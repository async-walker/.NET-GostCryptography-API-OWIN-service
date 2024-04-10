using GostCryptographyAPI.Formatters;
using Owin;
using Serilog;
using System.Configuration;
using System.Web.Http;

namespace GostCryptographyAPI
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Add(new BinaryMediaTypeFormatter());

            appBuilder.UseWebApi(config);

            string logFilePath = ConfigurationManager.AppSettings["LogFilePath"];

            var log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(logFilePath)
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger = log;
        }
    }
}
