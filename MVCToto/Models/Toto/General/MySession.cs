using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCToto.Models.Toto.General {

    public static class MySession {
        public static MyPagination Pagination {
            get {
                var pagi = (MyPagination)HttpContext.Current.Session["Pagination"];
                return (pagi == null) ? new MyPagination() : pagi;
            }
            set { HttpContext.Current.Session["Pagination"] = value; }
        }

        public static TotoAlapTipp Alaptipp {
            get { return (TotoAlapTipp)(HttpContext.Current.Session["Alaptipp"]); }
            set { HttpContext.Current.Session["Alaptipp"] = value; }
        }

        public static TotoTippSor Tippsor {
            get { return (TotoTippSor)(HttpContext.Current.Session["Tippsor"]); }
            set { HttpContext.Current.Session["Tippsor"] = value; }
        }

        public static string Error {
            get { return (string)(HttpContext.Current.Session["Error"]); }
            set { HttpContext.Current.Session["Error"] = value; }
        }
    }
}