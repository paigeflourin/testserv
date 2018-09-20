using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.ActiveDirectory;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(SIT_QnA_APIService.Startup))]

namespace SIT_QnA_APIService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig.Register(config);
            app.UseWebApi(config);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
            new WindowsAzureActiveDirectoryBearerAuthenticationOptions
            {
                Tenant = ConfigurationManager.AppSettings["ida:Tenant"],
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = ConfigurationManager.AppSettings["ida:Audience"]
                },
            });
        }
    }
}
