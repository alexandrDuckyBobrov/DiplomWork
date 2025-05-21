using DiplomProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomProject.Controllers
{
    public class HomeController : Controller
    {
        private DiplomEntities db = new DiplomEntities();
        public ActionResult Index()
        {
            var requests = db.catalogue;
            return View(requests.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}