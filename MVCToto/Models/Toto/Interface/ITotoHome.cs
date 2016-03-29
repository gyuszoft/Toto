using System.Web.Mvc;

namespace MVCToto.Models.Toto.Interface {
    public interface ITotoHome {
        #region alaptipp
        ITotoAlaptipp GetAlaptipp();
        void SetAlaptipp( TotoAlapTipp alaptipp );
        TotoAlapTipp GetAlaptippFromCollection( FormCollection coll );
        bool IsValidAlaptipp();
        #endregion

        #region generate
        TotoTippSor GenerateAllFromAlaptipp();
        #endregion
    }
}
