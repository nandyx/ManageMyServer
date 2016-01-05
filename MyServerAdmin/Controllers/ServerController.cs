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
            Row element = new Row();
            element.GetAll("world","todo"); 
            return View();
        }
    }
}