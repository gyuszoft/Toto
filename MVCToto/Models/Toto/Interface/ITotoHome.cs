using MVCToto.Models.Toto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCToto.Models.Interface {
    public interface ITotoHome {
        #region alaptipp
        TotoAlapTipp GetAlaptipp();
        void SetAlaptipp( TotoAlapTipp alaptipp );
        TotoAlapTipp GetAlaptippFromCollection( FormCollection coll );
        bool IsValidAlaptipp();
        #endregion

        #region generate
        TotoTippSor GenerateAllFromAlaptipp();
        #endregion
    }
}
