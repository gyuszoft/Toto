using MVCToto.Filter;
using MVCToto.Models.Interface;
using MVCToto.Models.Repo;
using MVCToto.Models.Toto;
using MVCToto.Models.Toto.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCToto.Controllers.Toto {
    public class TotoController : Controller {
        #region Init
        private ITotoHome repo;
        private ITotoLogger logger;
        public TotoController( ITotoLogger logger ) {
            this.logger = logger;
            this.repo = new TotoHome();
        }

        public TotoController( ITotoLogger logger, ITotoHome repo ) {
            this.logger = logger;
            this.repo = repo;
        }
        #endregion

        [TotoTimerFilter]
        public ActionResult Index() {
            logger.Log( "Index metódus kezdete." );
            logger.Log( "Index metódus vége." );
            return View();
        }

        [TotoTimerFilter]
        public ActionResult Generate() {
           // ViewBag.BaseTipps = BaseTippKeyValuePair.BASETIPP_ToKeyValuePairs();

            var session = System.Web.HttpContext.Current.Session;
            repo.SetAlaptipp( session["alaptipp"] as TotoAlapTipp );

            return View( repo.GetAlaptipp() );
        }

        [TotoTimerFilter]
        [HttpPost]
        public ActionResult Generate( FormCollection coll ) {
            //ViewBag.BaseTipps = BaseTippKeyValuePair.BASETIPP_ToKeyValuePairs();
            try {
                // TODO: Add update logic here
                if(ModelState.IsValid) {
                    var alaptipp = repo.GetAlaptippFromCollection( coll );
                    if(repo.IsValidAlaptipp()) {
                        alaptipp = repo.GetAlaptipp();
                        var tippSor = repo.GenerateAllFromAlaptipp();

                        var session = System.Web.HttpContext.Current.Session;
                        session["tippSor"] = tippSor;
                        session["alaptipp"] = alaptipp;

                        return RedirectToAction( "Filter" );

                    } else {
                        ViewData["Error"] = "Nem jó az alaptipp!";
                        return View( repo.GetAlaptipp() );
                    }
                } else {
                    return View( TotoFactory.NewTotoAlapTipp() );
                }
            } catch {
                //TODO: Kiírni a tényleges hibát is!!
                ViewData["Error"] = "Fatális hiba (try)!";
                return View( TotoFactory.NewTotoAlapTipp() );
            }
        }

        [TotoTimerFilter]
        public ActionResult Filter() {
            var session = System.Web.HttpContext.Current.Session;
            var tippSor = session["tippSor"];

//            ViewBag.BaseTipps = BaseTippKeyValuePair.BASETIPP_ToKeyValuePairs();

            return View( tippSor );
        }

        [TotoTimerFilter]
        public ActionResult Test() {
            return View(new TotoBaseTipp() { Tipp=BASETIPP._1 } );
        }


    }
}