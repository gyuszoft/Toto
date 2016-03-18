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
    [TotoTimerFilter]
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

        public ActionResult Index() {
            return View();
        }

        public ActionResult Generate() {
            // ViewBag.BaseTipps = BaseTippKeyValuePair.BASETIPP_ToKeyValuePairs();

            var session = System.Web.HttpContext.Current.Session;
            repo.SetAlaptipp( session["alaptipp"] as TotoAlapTipp );

            return View( repo.GetAlaptipp() );
        }

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
                        session["aktPage"] = 1;
                        session["maxPage"] = (int) (tippSor.TippSor.Count / TotoConst.PAGECOUNT+ 1);

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

        public ActionResult Filter() {
            var session = System.Web.HttpContext.Current.Session;
            var tippSor = (TotoTippSor)session["tippSor"];
            if(tippSor == null)
                return RedirectToAction( "Generate" );
            var aktPage = (int?)session["aktPage"];
            aktPage = (aktPage == null) ? 1 : aktPage;
            session["aktPage"]=aktPage;
            ViewBag.mettol = (aktPage - 1) * TotoConst.PAGECOUNT;
            ViewBag.meddig = Math.Min( (int)(aktPage * TotoConst.PAGECOUNT), tippSor.TippSor.Count );

            return View( tippSor );
        }

        public ActionResult Next() {
            var session = System.Web.HttpContext.Current.Session;
            var aktPage = (int?)session["aktPage"];
            var maxPage = (int)session["maxPage"];
            if(true) {

            }
            aktPage = (aktPage == null) ? 1 : aktPage+1;
            session["aktPage"] = aktPage;

            return RedirectToAction( "Filter" );
        }

        public ActionResult Prev() {
            var session = System.Web.HttpContext.Current.Session;
            var aktPage = (int?)session["aktPage"];
            aktPage = (aktPage == null) ? 1 : aktPage - 1;
            session["aktPage"] = aktPage;

            return RedirectToAction( "Filter" );
        }

        public ActionResult Test() {
            return View( new TotoBaseTipp() { Tipp = BASETIPP._1 } );
        }


    }
}