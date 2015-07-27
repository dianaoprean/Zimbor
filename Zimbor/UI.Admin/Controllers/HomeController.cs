using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories;
using Domain;
using Microsoft.Practices.Unity;

namespace UI.Admin.Controllers
{
    public class HomeController : Controller
    {

        [Dependency]
        public IZimborRepository<Produ> ProdusRepository { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
