using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace MyServerAdmin.Data
{
    public class MysqlConecction : Connection
    {

        public override IDbConnection Open()
        {
            IDbConnection con;
            con = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ConnectionString);
            con.Open();
            return con;
        }
    }
}