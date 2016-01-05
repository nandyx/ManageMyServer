using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServerAdmin.Data
{
     public abstract class Connection
    {
        public abstract IDbConnection Open();
        public IDbConnection Change(string database)
        {
            IDbConnection con;
            con = this.Open();
            con.ChangeDatabase(database);
            return con;
        }
        public bool Close(IDbConnection c)
        {
            
            try
            {
                c.Dispose();
                c = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
