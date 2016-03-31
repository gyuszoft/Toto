using System.Web.Mvc;

namespace MVCToto.Models.Toto.Interface {
    public interface ITotoHome {
        #region alapTipp
        ITotoAlapTipp GetAlaptipp();
        void SetAlaptipp( ITotoAlapTipp alapTipp );
        TotoAlapTipp GetAlaptippFromCollection( FormCollection coll );
        bool IsValidAlaptipp();
        #endregion

        #region generate
        TotoTippSor GenerateAllFromAlaptipp();
        #endregion
    }
}
