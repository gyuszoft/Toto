using MVCToto.Models.Toto.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCToto.Controllers {
    public class HomeController : Controller {
        ITotoLogger logger;
        public HomeController( ITotoLogger logger ) {
            this.logger = logger;
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