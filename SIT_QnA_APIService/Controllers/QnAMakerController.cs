using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Configuration;

namespace SIT_QnA_APIService.Controllers
{
    [Authorize]
    [RoutePrefix("api/qnaMaker")]
    public class QnAMakerController : ApiController
    {


     

        static string host = "https://westus.api.cognitive.microsoft.com";
        static string service = "/qnamaker/v4.0";
        static string method = "/knowledgebases/";
      

        static string key = ConfigurationManager.AppSettings["QnAMakerKey"];


        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return null;
        }

        [HttpGet]
        [Route("test")]
        public string Test()
        {
            return "Hello World";
        }

        [HttpPost]
        [Route("publish/{id}")]
        public async Task<string> PublishQnA(int id)
        {
            var uri = host + service + method + id;
            Console.WriteLine("Calling " + uri + ".");
            var response = await Post(uri);
            Console.WriteLine("Press any key to continue.");

            return response;
        }

        async static Task<string> Post(string uri)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return "{'result' : 'Success.'}";
                }
                else
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }



        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}