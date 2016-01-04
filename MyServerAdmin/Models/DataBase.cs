using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using MyServerAdmin.Data;
using System.Data;

namespace MyServerAdmin.Models
{
    public class DataBase
    {
        public string name { get; set; }

        public IEnumerable<DataBase> List() {

            IEnumerable<DataBase> coleccion= null;
            Connection c = new MysqlConecction();
            IDbConnection cnn = c.Open();
            try
            {
                var registro = Dapper.SqlMapper.Query<DataBase>(cnn, "Server_GetDatabases", null, commandType: CommandType.StoredProcedure);
                coleccion = (IEnumerable<DataBase>)registro;
            }
            catch (Exception e)
            {
            }
            finally
            {
                c.Close(cnn);
            }
            return coleccion;

        }

        public IEnumerable<String> Test()
        {

            IEnumerable<String> coleccion = null;
            Connection c = new MysqlConecction();
            IDbConnection cnn = c.ChangeDatabase("world");
            //String query = @"Select Name from city;";
            try
            {
                //var registro = Dapper.SqlMapper.Query<String>(cnn, query, null, commandType: CommandType.Text);
                var registro = Dapper.SqlMapper.Query<String>(cnn, "TEST_GetNameCity", null, commandType: CommandType.StoredProcedure);
                coleccion = (IEnumerable<String>)registro;
            }
            catch (Exception e)
            {
            }
            finally
            {
                c.Close(cnn);
            }
            return coleccion;

        }
    }
}