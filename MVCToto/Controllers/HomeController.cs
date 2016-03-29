using MVCToto.Models.Toto.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCToto.Models.Toto.Interface;

namespace MVCToto.Controllers {
    public class HomeController : Controller {
        ITotoLogger _logger;
        public HomeController( ITotoLogger logger ) {
            this._logger = logger;
        }

        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}