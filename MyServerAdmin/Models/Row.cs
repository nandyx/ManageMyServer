using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using MyServerAdmin.Data;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace MyServerAdmin.Models
{
    public class Row
    {
        public List<Element> content { get; set; }
        private Queue<Element> column;
        private IEnumerable<Element> columnBack;

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
            String query = @"Select * from " +table+" LIMIT 5;";
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
                            element.type = column.Peek().type;
                            element.isKey = column.Dequeue().isKey;
                            row.content.Add(element);
                        }
                    }
                    column = new Queue<Element>(columnBack);
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
                columnBack = (List<Element>)registro;
            }
            catch (Exception e)
            {
            }
            finally
            {
                c.Close(cnn);
            }
        }

        public void New(string db, string table)
        {
            Connection c = new MysqlConecction();
            IDbConnection cnn = c.Change(db);
            int indice = 1;
            Element key = new Element();
            StringBuilder query = new StringBuilder(@"INSERT IGNORE INTO ");
            query.Append(table);
            query.Append(" SET ");
            foreach (Element item in content)
            {
                if (item.isKey == true) key = item;

                query.Append(item.name + "= ");
                if (indice < content.Count)
                {
                    if (item.type.Equals("String") || item.type.Equals("char") || item.type.Equals("varchar") || item.type.Equals("datetime"))
                        query.Append("'" + item.value + "'" + ", ");
                    else
                        query.Append(item.value + ", ");
                    indice++;
                }
                else
                {
                    if (item.type.Equals("String") || item.type.Equals("char") || item.type.Equals("varchar") || item.type.Equals("datetime"))
                        query.Append("'" + item.value + "';");
                    else
                        query.Append(item.value + ";");
                }
            }
            Debug.WriteLine(query.ToString());
            try
            {
                SqlMapper.Query(cnn, query.ToString(), null, commandType: CommandType.Text);
            }
            catch (Exception e)
            {
            }
            finally
            {
                c.Close(cnn);
            }
        }


        public void Update(string db, string table) {
            Connection c = new MysqlConecction();
            IDbConnection cnn = c.Change(db);
            int indice = 1;
            Element key = new Element();
            StringBuilder update = new StringBuilder(@"UPDATE ");
            update.Append(table);
            update.Append(" SET ");
            foreach (Element item in content)
            {
                if(item.isKey==true) key = item;

                update.Append(item.name+"= ");
                if (indice < content.Count)
                {
                    if(item.type.Equals("String")|| item.type.Equals("char") || item.type.Equals("varchar") || item.type.Equals("datetime"))
                        update.Append("'"+item.value+"'" + ", ");
                    else
                        update.Append(item.value+ ", ");
                    indice++;
                }
                else
                {
                    if (item.type.Equals("String") || item.type.Equals("char") || item.type.Equals("varchar") || item.type.Equals("datetime"))
                        update.Append("'" + item.value + "'");
                    else
                        update.Append(item.value);
                    update.Append(" WHERE "+key.name+"= '"+key.value+"';");
                }
            }
            Debug.WriteLine(update.ToString());
            try
            {
                SqlMapper.Query(cnn, update.ToString(), null, commandType: CommandType.Text);
            }
            catch (Exception e)
            {
            }
            finally
            {
                c.Close(cnn);
            }
        }

        public void Delete(string db,string tb, string key, string value) {
            //DELETE FROM descriptor WHERE id = _idDominio and tipo = 'DOM';
            Connection c = new MysqlConecction();
            IDbConnection cnn = c.Change(db);
            StringBuilder delete = new StringBuilder(@"DELETE FROM ");
            delete.Append(tb+" WHERE "+key+" = ");
            delete.Append(value + ";");
            try
            {
                SqlMapper.Query(cnn, delete.ToString(), null, commandType: CommandType.Text);
               
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