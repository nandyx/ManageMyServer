using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyServerAdmin.Data
{
    public class FakeConnection :Connection
    {
        public override System.Data.IDbConnection Open()
        {
            throw new NotImplementedException();
        }
    }
}