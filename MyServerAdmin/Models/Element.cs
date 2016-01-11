using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using MyServerAdmin.Data;
using System.Data;
using System.Diagnostics;
using System.Web.WebPages;

namespace MyServerAdmin.Models
{
    public class Element
    {
        public string name { get; set; }
        public string value { get; set; }
        public object type { get; set; }
        public bool isKey { get; set; }

        public enum Type { Digit, Alpha, Date, boolean};

        public Type SetType(string value) {
            Type _type;
            if (StringExtensions.IsInt(value) || StringExtensions.IsDecimal(value) || StringExtensions.IsFloat(value))
                _type = Type.Digit;
            else if (StringExtensions.IsDateTime(value))
                _type = Type.Date;
            else if (StringExtensions.IsBool(value))
                _type = Type.boolean;
            else
                _type = Type.Alpha;
            return _type;

        }
    }

}