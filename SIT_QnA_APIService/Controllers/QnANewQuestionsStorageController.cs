using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using SIT_QnA_APIService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

namespace SIT_QnA_APIService.Controllers
{
    [Authorize]
    [RoutePrefix("api/newQuestions")]
    public class QnANewQuestionsStorageController : ApiController
    {

        private static string _connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

        [HttpGet]
        [Authorize]
        public IEnumerable<NewQuestionsEntity> Get()
        {

            List<NewQuestionsEntity> _records = new List<NewQuestionsEntity>();

            //Create a storage account object.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("NewQuestions");
            table.CreateIfNotExists();
            TableContinuationToken token = null;
     

            var query = table.CreateQuery<NewQuestionsEntity>().Where(x => x.PartitionKey == partition.PartitionKey);

            do
            {
                var page = query.Take(512);

                var segment = table.ExecuteQuerySegmented((TableQuery<NewQuestionsEntity>)page, token);
                token = segment.ContinuationToken;

                _records.AddRange(segment.Results);
            }
            while (token != null);


            return _records;
        }



        // GET api/<controller>
        //public ienumerable<string> get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //get api/<controller>/5
        //public string get(int id)
        //{
        //    return "value";
        //}

        //post api/<controller>
        //public void post([frombody]string value)
        //{
        //}

        //put api/<controller>/5
        //public void put(int id, [frombody]string value)
        //{
        //}

        //delete api/<controller>/5
        //public void delete(int id)
        //{
        //}
    }
}