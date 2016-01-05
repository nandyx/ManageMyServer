using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using MyServerAdmin.Data;
using System.Data;
using System.Diagnostics;


namespace MyServerAdmin.Models
{
    public class Row
    {
        public List<Element> content { get; set; }
        private Queue<Element> column;

        /// <summary>
        /// Get all rows like collection in Table
        /// </summary>
        /// <param name="db"></param>
        /// <param name="table"></param>
        /// <returns>ICollection<Row></returns>
        public ICollection<Row> GetAll(string db, string table)
        {
            Element element;
            Row row;
            ICollection<Row> collection= new  List<Row>();
            IDictionary<object, string> d = null;
            this.GetColumn(db,table);
            Connection c = new MysqlConecction();
            IDbConnection cnn = c.Change(db);
            String query = @"Select * from " +table+";";
            try
            {
                var registro = SqlMapper.Query<object>(cnn, query, null, commandType: CommandType.Text);
                IDictionary<string, object> diccionary;
                foreach (var item in registro)
                {
                    diccionary = (IDictionary<string, object>)item;
                    row = new Row();
                    row.content = new List<Element>();
                    // Two collection in one foreach 
                    //collection 1. diccionary.Keys (nw.item1)
                    //collection 2. diccionary.values (nw.item2)
                    foreach (var nw in diccionary.Keys.Zip(diccionary.Values, Tuple.Create))
                    {
                        element = new Element();
                        if (nw.Item1.Equals(column.Peek().name))
                        {
                            element.name = nw.Item1;
                            element.value = nw.Item2.ToString();
                            element.type = column.Dequeue().type;
                            row.content.Add(element);
                        }
                    }
                    collection.Add(row);
                }

            }
            catch (Exception e)
            {
            }
            finally
            {
                c.Close(cnn);
            }
            return collection;
        }
        /// <summary>
        /// Get name_column and data_type
        /// </summary>
        /// <param name="db"></param>
        /// <param name="table"></param>
        private void GetColumn(string db, string table) {

            
            Connection c = new MysqlConecction();
            IDbConnection cnn = c.Open();
            try
            {
                var registro = SqlMapper.Query<Element>(cnn, "Server_GetColumn", new { db = db, tb = table}, commandType: CommandType.StoredProcedure);
                column = new Queue<Element>(registro);                
            }
            catch (Exception e)
            {
            }
            finally
            {
                c.Close(cnn);
            }
        }
    }
}