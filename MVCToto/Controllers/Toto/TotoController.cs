using MVCToto.Filter;
using MVCToto.Models.Toto;
using MVCToto.Models.Toto.General;
using MVCToto.Models.Toto.Interface;
using MVCToto.Models.Toto.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using MVCToto.Models.Toto.Repo;

namespace MVCToto.Controllers.Toto {
    [TotoTimerFilter]
    public class TotoController : Controller {

        #region Init
        const string ERROR1 ="Nem jó az alaptipp!";
        const string ERROR2 ="Fatális hiba (try)!";

        private ITotoHome _repo;
        private ITotoLogger _logger;
        private ITotoPagination _pagi;

        public TotoController( ITotoLogger logger )
            : this( logger,
                  new TotoHome( TotoFactory.NewTotoAlapTipp() ),
                  TotoSessionFactory.Pagination ) { }

        public TotoController( ITotoLogger logger, ITotoHome repo, ITotoPagination pagi ) {
            this._logger = logger;
            this._repo = repo;
            this._pagi = pagi;
        }
        #endregion

        public ActionResult Index() {
            return View();
        }

        public ActionResult Generate() {
            // ViewBag.BaseTipps = BaseTippKeyValuePair.BASETIPP_ToKeyValuePairs();

            var session = System.Web.HttpContext.Current.Session;
            _repo.SetAlaptipp( TotoSessionFactory.Alaptipp );

            return View( _repo.GetAlaptipp() );
        }

        [HttpPost]
        public ActionResult Generate( FormCollection coll ) {
            try {
                if(ModelState.IsValid) {
                    var alaptipp = _repo.GetAlaptippFromCollection( coll );
                    if(_repo.IsValidAlaptipp()) {
                        //alaptipp = repo.GetAlaptipp();
                        var tippSor = _repo.GenerateAllFromAlaptipp();

                        var session = System.Web.HttpContext.Current.Session;
                        TotoSessionFactory.Tippsor = tippSor;
                        TotoSessionFactory.Alaptipp = alaptipp;
                        _pagi.OnePage = TotoConst.DEFPAGECOUNT;
                        _pagi.SetCount( tippSor.TippSor.Count );
                        _pagi.SetActPage( 1 );
                        TotoSessionFactory.Pagination = _pagi;

                        return RedirectToAction( "Filter" );

                    } else {
                        ViewBag.Error = ERROR1;
                        return View( _repo.GetAlaptipp() );
                    }
                } else {
                    return View( TotoFactory.NewTotoAlapTipp() );
                }
            } catch {
                //TODO: Kiírni a tényleges hibát is!!
                ViewBag.Error = ERROR2;
                return View( TotoFactory.NewTotoAlapTipp() );
            }
        }

        public ActionResult Filter() {
            var session = System.Web.HttpContext.Current.Session;
            var tippSor = TotoSessionFactory.Tippsor;
            if(tippSor == null)
                return RedirectToAction( "Generate" );

            ViewBag.Pagination = _pagi;
            return View( tippSor );
        }

        public ActionResult Next() {
            _pagi.Next();
            return RedirectToAction( "Filter" );
        }

        public ActionResult Prev() {
            _pagi.Prev();
            return RedirectToAction( "Filter" );
        }
        public ActionResult First() {
            _pagi.First();
            return RedirectToAction( "Filter" );
        }
        public ActionResult Last() {
            _pagi.Last();
            return RedirectToAction( "Filter" );
        }
        public ActionResult GotoPage( int id ) {
            _pagi.SetActPage( id );
            return RedirectToAction( "Filter" );
        }
    }
}