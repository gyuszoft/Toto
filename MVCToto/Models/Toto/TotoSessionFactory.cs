using MVCToto.Models.Toto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;

namespace MVCToto.Models.Toto.General {

    public static class TotoSessionFactory {
        public static ITotoPagination Pagination {
            get {
                var pagi = (TotoPagination)HttpContext.Current.Session["Pagination"];
                return (pagi == null) ? new TotoPagination(new MyPagination(TotoConst.DEFPAGECOUNT)) : pagi;
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
    }
}