using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyServerAdmin.Models;

namespace MyServerAdmin.Controllers
{
    public class ServerController : Controller
    {
        // GET: Server
        public ActionResult Index()
        {
            IEnumerable<String> db;
            DataBase obj = new DataBase();
            db = obj.Test();
            return View();
        }
    }
}