using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.CosmosDB.Table;

namespace SITQnAAPIServiceADFS.Models
{
    public class NewQuestionsEntity : TableEntity
    {
        public NewQuestionsEntity(string skey, string Question)
        {
            this.PartitionKey = skey;
            this.RowKey = Question;
        }

        public NewQuestionsEntity() { }

        public string PostedBy { get; set; }
        public DateTime PostedDate { get; set; }
        public string Contact { get; set; }
       // public string Question { get; set; }

    }
}