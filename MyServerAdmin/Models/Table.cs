using System.Web;
using Dapper;
using MyServerAdmin.Data;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System;

namespace MyServerAdmin.Models
{
    public class Table
    {
        public string name { get; set; }
        public ICollection<Row> collection { get; set; }

        public ICollection<Table> GetAll(string db)
        {
            ICollection<Table> tbs=null;
            Connection c = new MysqlConecction();
            IDbConnection cnn = c.Change(db);
            StringBuilder update = new StringBuilder(@"select TABLE_NAME AS name from information_schema.TABLES where TABLE_SCHEMA = '");
            update.Append(db + "';");
            try
            {
               var registro= SqlMapper.Query<Table>(cnn, update.ToString(), null, commandType: CommandType.Text);
                tbs = (List<Table>)registro;
            }
            catch (Exception e)
            {
            }
            finally
            {
                c.Close(cnn);
            }
            return tbs;
        }
    }
}