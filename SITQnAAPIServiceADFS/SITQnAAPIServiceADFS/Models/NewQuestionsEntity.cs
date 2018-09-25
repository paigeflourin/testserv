using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.CosmosDB.Table;

namespace SITQnAAPIServiceADFS.Models
{
    public class NewQuestionsEntity : TableEntity
    {
        public NewQuestionsEntity(string skey, string rowkey)
        {
            this.PartitionKey = skey;
            this.RowKey = rowkey;
        }

        public NewQuestionsEntity() { }

        public string PostedBy { get; set; }
        public DateTime PostedDate { get; set; }
        public string Contact { get; set; }
        public string Status { get; set; }
        public string Question { get; set; }

    }
}