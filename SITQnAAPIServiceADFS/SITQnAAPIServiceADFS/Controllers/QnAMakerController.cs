using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SITQnAAPIServiceADFS.Controllers
{
    [RoutePrefix("api/qnamaker")]
    public class QnaMakerController : ApiController
    {
        static string host = "https://westus.api.cognitive.microsoft.com";
        static string service = "/qnamaker/v4.0";
        static string method = "/knowledgebases/";
        static string key = ConfigurationManager.AppSettings["QnAMakerKey"];
        //kb id: 3fd5349a-7f39-4599-bbb2-6f3e041703b4


        [HttpGet]
        [Route("qna/{kbid}")]
        public async Task<string> GetAllKB([FromUri] string kbid, [FromBody] RemoteStreamInfo env)
        {
            var method_with_id = String.Format(method, kbid, env);
            var uri = host + service + method_with_id;
            Console.WriteLine("Calling " + uri + ".");

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(uri);
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        [HttpPatch]
        [Route("qna/{kbid}")]
        public async Task<Response> Updatekb([FromUri]string kbid, HttpRequestMessage new_kb)
        {
            
            var requestString = new_kb.Content.ReadAsStringAsync().Result;
            //return "hello patch" +kbid+" "+requestString ;

            var response = await PostUpdateKB(kbid, requestString);
            var operation = response.headers.GetValues("Location").First();
            var done = false;
            while (true != done)
            {
                response = await GetStatus(operation);

                var fields = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.response);

                // Gets and checks the state of the operation.
                String state = fields["operationState"];
                if (state.CompareTo("Running") == 0 || state.CompareTo("NotStarted") == 0)
                {
                    var wait = response.headers.GetValues("Retry-After").First();
                    Console.WriteLine("Waiting " + wait + " seconds...");
                    Thread.Sleep(Int32.Parse(wait) * 1000);
                }
                else
                {
                    // QnA Maker has completed updating the knowledge base.
                    done = true;
                    // res = "Updated";
                }
            }
            return response;
        }

   
        async static Task<Response> PostUpdateKB(string kb, string new_kb)
        {
            string uri = host + service + method + kb;
            Console.WriteLine("Calling " + uri + ".");
            return await Patch(uri, new_kb);
        }


        async static Task<Response> GetStatus(string operation)
        {
            string uri = host + service + operation;
            Console.WriteLine("Calling " + uri + ".");
            return await Get(uri);
        }

       
        async static Task<Response> Patch(string uri, string body)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("PATCH");
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                return new Response(response.Headers, responseBody);
            }
        }

        async static Task<Response> Get(string uri)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(uri);
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                return new Response(response.Headers, responseBody);
            }
        }


        //Publish QnA
        [HttpPost]
        [Route("qna/{kbid}")]
        public async Task<string> PublishQnA([FromUri]string kbid)
        {
            var uri = host + service + method + kbid;
            Console.WriteLine("Calling " + uri + ".");
            var response = await Post(uri);


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
    }
 }
