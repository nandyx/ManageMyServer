﻿using System;
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
        public ICollection<Table> tables { get; set; }

        public ICollection<DataBase> List() {

            ICollection<DataBase> coleccion= null;
            Connection c = new MysqlConecction();
            IDbConnection cnn = c.Open();
            try
            {
                var registro = SqlMapper.Query<DataBase>(cnn, "Server_GetDatabases", null, commandType: CommandType.StoredProcedure);
                coleccion = (ICollection<DataBase>)registro;
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