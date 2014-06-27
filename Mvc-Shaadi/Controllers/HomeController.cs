using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcShaadi.Models;

namespace MvcShaadi.Controllers
{
    public class HomeController : Controller
    {
        private MvcShaadiContext db = new MvcShaadiContext();
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

          

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Browse(int id)
        {
            var profiles = db.Profiles;
  
            return View(profiles);
        }
    }
}
