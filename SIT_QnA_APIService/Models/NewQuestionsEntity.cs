using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace SIT_QnA_APIService.DTO
{
    public class NewQuestionsEntity : TableEntity
    {
        public NewQuestionsEntity(string skey, string srow)
        {
            this.PartitionKey = skey;
            this.RowKey = srow;
        }

        public NewQuestionsEntity() { }

        public DateTime timestamp { get; set; }
        public DateTime postedDate { get; set; }
        public string contact { get; set; }
        public string question { get; set; }

    }
}