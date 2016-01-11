using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyServerAdmin.Models;
using System.Diagnostics;
using System.Linq;

namespace MyServerAdmin.Controllers
{
    public class ServerController : Controller
    {
        // GET: Server
        public ActionResult Index()
        {
            DataBase db = new DataBase();
            ICollection<DataBase> dbs = db.List();
            Session["databases"] = dbs;
            return View();
        }
        public ActionResult Show(string db, string tb)
        {
            Table table = new Table();
            Row row = new Row();
            table.name = tb;
            table.collection = row.GetAll(db,tb);
            ViewData["collection"] = table;
            ViewBag.database = db;
            return View();
        }
        public ActionResult Delete(string db, string tb, string key, string value)
        {
            Row row = new Row();
            row.Delete(db, tb, key,value);
            return RedirectToAction("Show", new { db= db, tb = tb});

        }
        public ActionResult New()
        {
            Row column = (Row)Session["Row"];
            Row element = new Row();
            element.content = new List<Element>();
            foreach (Element  item in column.content)
            {
                item.value = Request[item.name];
                element.content.Add(item);
            }
            Session.Remove("Row");
            string db = Session["db"].ToString();
            string tb = Session["tb"].ToString();
            Session.Remove("db"); Session.Remove("tb");
            element.New(db, tb);
            return RedirectToAction("Show", new { db=db, tb=tb});
        }
        public ActionResult Update()
        {
            int indice = Convert.ToInt32( Request["indice"]);
            Table upd = (Table)Session["upd"];
            Session["data"] = upd.collection.ElementAt(indice);
            Session["db"].ToString();
            Session["tb"].ToString();

            return View();
        }
        [HttpPost]
        public ActionResult Save()
        {
            Row column = (Row)Session["data"];
            Row element = new Row();
            element.content = new List<Element>();
            foreach (Element item in column.content)
            {
                item.value = Request[item.name];
                element.content.Add(item);
            }
            Session.Remove("Row");
            string db = Session["db"].ToString();
            string tb = Session["tb"].ToString();
            Session.Remove("db"); Session.Remove("tb");
            element.Update(db, tb);
            return RedirectToAction("Show", new { db = db, tb = tb });
        }
    }
 }