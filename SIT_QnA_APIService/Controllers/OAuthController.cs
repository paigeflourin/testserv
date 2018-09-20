using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SIT_QnA_APIService.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/oauth")]
    public class OAuthController : ApiController
    {
        [HttpGet]
        [Route("getwebtitle")]
        public string GetWebTitle(string sharePointUrl)
        {
            string accessToken = GetSharePointAccessToken(sharePointUrl, this.Request.Headers.Authorization.Parameter);

            using (var context = TokenHelper.GetClientContextWithAccessToken(sharePointUrl, accessToken))
            {
                Web web = context.Web;
                context.Load(web);
                context.ExecuteQuery();
                return web.Title;
            }
        }

        internal static string GetSharePointAccessToken(string url, string accessToken)
        {
            string clientID = ConfigurationManager.AppSettings["ida:ClientID"];
            string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];

            var appCred = new ClientCredential(clientID, clientSecret);
            var authContext = new AuthenticationContext("https://login.windows.net/common");

            // AuthenticationResult authResult = authContext.AcquireToken(new Uri(url).GetLeftPart(UriPartial.Authority), appCred, new UserAssertion(accessToken));

            return authContext.AcquireTokenAsync(new Uri(url).GetLeftPart(UriPartial.Authority), appCred, new UserAssertion(accessToken)).Result.AccessToken;
           // return authResult.AccessToken;
        }
    }
}