using System.Web;
using Dapper;
using MyServerAdmin.Data;
using System.Data;
using System.Collections.Generic;

namespace MyServerAdmin.Models
{
    public class Table
    {
        public string name { get; set; }
        ICollection<Row> collection { get; set; }
    }
}