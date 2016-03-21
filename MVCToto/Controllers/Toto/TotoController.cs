using MVCToto.Filter;
using MVCToto.Models.Interface;
using MVCToto.Models.Repo;
using MVCToto.Models.Toto;
using MVCToto.Models.Toto.General;
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
        const string ERROR1 ="Nem jó az alaptipp!";
        const string ERROR2 ="Fatális hiba (try)!";

        private ITotoHome repo;
        private ITotoLogger logger;
        private MyPagination pagi;

        public TotoController( ITotoLogger logger ):this(logger, new TotoHome()) {}

        public TotoController(ITotoLogger logger, ITotoHome repo ) {
            this.logger = logger;
            this.repo = repo;

            pagi = MySession.Pagination;
        }
        #endregion

        public ActionResult Index() {
            return View();
        }

        public ActionResult Generate() {
            // ViewBag.BaseTipps = BaseTippKeyValuePair.BASETIPP_ToKeyValuePairs();

            var session = System.Web.HttpContext.Current.Session;
            repo.SetAlaptipp( MySession.Alaptipp );

            return View( repo.GetAlaptipp() );
        }

        [HttpPost]
        public ActionResult Generate( FormCollection coll ) {
            try {
                if(ModelState.IsValid) {
                    var alaptipp = repo.GetAlaptippFromCollection( coll );
                    if(repo.IsValidAlaptipp()) {
                        alaptipp = repo.GetAlaptipp();
                        var tippSor = repo.GenerateAllFromAlaptipp();

                        var session = System.Web.HttpContext.Current.Session;
                        MySession.Tippsor = tippSor;
                        MySession.Alaptipp = alaptipp;
                        pagi.SetOnePage( TotoConst.PAGECOUNT );
                        pagi.SetCount( tippSor.TippSor.Count );
                        pagi.SetActPage( 1 );
                        MySession.Pagination = pagi;

                        return RedirectToAction( "Filter" );

                    } else {
                        MySession.Error = ERROR1;
                        return View( repo.GetAlaptipp() );
                    }
                } else {
                    return View( TotoFactory.NewTotoAlapTipp() );
                }
            } catch {
                //TODO: Kiírni a tényleges hibát is!!
                MySession.Error = ERROR2;
                return View( TotoFactory.NewTotoAlapTipp() );
            }
        }

        public ActionResult Filter() {
            var session = System.Web.HttpContext.Current.Session;
            var tippSor = MySession.Tippsor;
            if(tippSor == null)
                return RedirectToAction( "Generate" );

            ViewBag.Pagination = pagi;
            return View( tippSor );
        }

        public ActionResult Next() {
            pagi.Next();
            return RedirectToAction( "Filter" );
        }

        public ActionResult Prev() {
            pagi.Prev();
            return RedirectToAction( "Filter" );
        }
        public ActionResult First() {
            pagi.First();
            return RedirectToAction( "Filter" );
        }
        public ActionResult Last() {
            pagi.Last();
            return RedirectToAction( "Filter" );
        }



        public ActionResult Test() {
            return View( new TotoBaseTipp() { Tipp = BASETIPP._1 } );
        }
    }
}