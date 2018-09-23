
using SITQnAAPIServiceADFS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using Microsoft.Azure.CosmosDB.Table; // Namespace for Table storage types
using System.Threading.Tasks;
using Microsoft.Azure.Storage;

namespace SITQnAAPIServiceADFS.Controllers
{
    public class NewQuestionsController : ApiController
    {
        private static string _connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

        // [HttpGet]
        // public IEnumerable<string> GetTest()
        //  {
        //       return new string[] { "value1", "value2" };
        // }


        [HttpGet]
       public List<NewQuestionsEntity> GetRecords()
        {
            List<NewQuestionsEntity> _records = new List<NewQuestionsEntity>();

            //Create a storage account object.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("NewQuestions");
            table.CreateIfNotExists();

            TableQuery<NewQuestionsEntity> query = new TableQuery<NewQuestionsEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "SIT New Questions"));

            foreach (NewQuestionsEntity entity in table.ExecuteQuery(query))
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey,
                    entity.PostedDate, entity.PostedBy);
                _records.Add(entity);
            }

            return _records;
        }

        [HttpDelete]
        public string DeleteRecord(NewQuestionsEntity entity)
        {
            string res; 
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("NewQuestions");
            TableOperation retrieveOperation = TableOperation.Retrieve<NewQuestionsEntity>(entity.PartitionKey, entity.RowKey);
            TableResult retrievedResult = table.Execute(retrieveOperation);

            NewQuestionsEntity deleteEntity = (NewQuestionsEntity)retrievedResult.Result;

            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                table.Execute(deleteOperation);
                Console.WriteLine("Entity deleted.");
                res =  "Entity Deleted";
            }
            else
            {
                Console.WriteLine("Could not retrieve the entity.");
                res = "error in deletingentity";
            }
            return res;
        } 

      

    }
}