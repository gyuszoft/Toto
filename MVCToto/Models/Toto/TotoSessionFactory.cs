using System.Web;
using MVCToto.Models.Toto.General;
using MVCToto.Models.Toto.Interface;

namespace MVCToto.Models.Toto {

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