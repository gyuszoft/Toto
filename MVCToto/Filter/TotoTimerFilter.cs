using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCToto.Filter {
    public class TotoTimerFilter : ActionFilterAttribute {
        private Stopwatch _asw,_rsw;
        public TotoTimerFilter() {
            _asw = new Stopwatch();
            _rsw = new Stopwatch();
        }
        public override void OnActionExecuting( ActionExecutingContext filterContext ) {
            // Ez a kettő = asw.Restart()
            _asw.Reset();
            _asw.Start();
        }
        public override void OnResultExecuting( ResultExecutingContext filterContext ) {
            _rsw.Restart();
        }
        public override void OnActionExecuted( ActionExecutedContext filterContext ) {
            _asw.Stop();
            base.OnActionExecuted( filterContext );
        }

        public override void OnResultExecuted( ResultExecutedContext filterContext ) {
            _rsw.Stop();

            string action = filterContext.RouteData.Values["action"] as string;
            var view = filterContext.Result as ViewResult;
            string viewName = view != null ? view.ViewName:"???";
            filterContext.HttpContext.Response.Write(
                string.Format( "<p>Action:{0}: {1} || View:{2}: {3}</p>",
                    action,
                    _asw.Elapsed,
                    viewName,
                    _rsw.Elapsed ) );
            base.OnResultExecuted( filterContext );
        }
    }
}