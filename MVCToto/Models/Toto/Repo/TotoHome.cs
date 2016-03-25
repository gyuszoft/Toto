using MVCToto.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCToto.Models.Toto;
using System.Web.Mvc;

namespace MVCToto.Models.Repo {
    public class TotoHome : ITotoHome {
        private ITotoAlaptipp _alaptipp;

        //Generáláshoz local
        private TotoEgyOszlop locEgyOszlop;
        private TotoTippSor locTippSor;

        public TotoHome( ITotoAlaptipp alaptipp) {
            _alaptipp = alaptipp;
            //_alaptipp = TotoFactory.NewTotoAlapTipp();
            locEgyOszlop = TotoFactory.NewTotoEgyOszlop();
            locTippSor = TotoFactory.NewTotoTippSor();
        }
        public ITotoAlaptipp GetAlaptipp() {
            return _alaptipp;
        }
        public void SetAlaptipp( TotoAlapTipp alaptipp ) {
            if(alaptipp != null)
                _alaptipp = alaptipp;
            else
                _alaptipp = TotoFactory.NewTotoAlapTipp();
        }
        public TotoAlapTipp GetAlaptippFromCollection( FormCollection coll ) {
            var alaptipp = TotoFactory.NewTotoAlapTipp();
            for(int i = 1; i < TotoConst.TOTO_SOR + 1; i++) {
                var tipp = TotoFactory.NewTotoTipp();
                tipp.Tipp1.Tipp = (BASETIPP)Convert.ToInt32( coll["AlapTipp[" + i + "].Tipp1.Tipp"] );
                tipp.Tipp2.Tipp = (BASETIPP)Convert.ToInt32( coll["AlapTipp[" + i + "].Tipp2.Tipp"] );
                tipp.Tipp3.Tipp = (BASETIPP)Convert.ToInt32( coll["AlapTipp[" + i + "].Tipp3.Tipp"] );
                alaptipp.Set( i, tipp );
            }
            SetAlaptipp( alaptipp );
            return alaptipp;
        }

        #region Rekurzív generálás

        public TotoTippSor GenerateAllFromAlaptipp() {
            locTippSor.Clear();
            GenerateAllFromAlaptipp( 1 );
            return locTippSor;
        }

        private void GenerateAllFromAlaptipp( int aktSor ) {
            if(aktSor == TotoConst.TOTO_SOR + 1) {
                // Itt van kész egy oszlop
                locTippSor.Add( locEgyOszlop.Clone() );
            } else {
                if(_alaptipp.AlapTipp[aktSor].Esely == 0) {
                    locEgyOszlop.Set( aktSor, TotoFactory.NewBaseTipp(BASETIPP._1) );
                    GenerateAllFromAlaptipp( aktSor + 1 );
                } else {

                    for(int i = 0; i < _alaptipp.AlapTipp[aktSor].Esely; i++) {
                        switch(i) {
                            case 0:
                                locEgyOszlop.Set(aktSor, _alaptipp.AlapTipp[aktSor].Tipp1 );
                                GenerateAllFromAlaptipp( aktSor + 1 );
                                break;
                            case 1:
                                locEgyOszlop.Set( aktSor, _alaptipp.AlapTipp[aktSor].Tipp2);
                                GenerateAllFromAlaptipp( aktSor + 1 );
                                break;
                            case 2:
                                locEgyOszlop.Set( aktSor, _alaptipp.AlapTipp[aktSor].Tipp3);
                                GenerateAllFromAlaptipp( aktSor + 1 );
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        #endregion

        public bool IsValidAlaptipp() {
            for(int i = 1; i < _alaptipp.AlapTipp.Length; i++) {
                var tipp = _alaptipp.AlapTipp[i];
                #region Üres tippek eltüntetése
                switch(tipp.Esely) {
                    case 3:
                        if(tipp.Tipp1.Tipp == BASETIPP.EMPTY) {
                            tipp.Tipp1.Tipp = tipp.Tipp2.Tipp;
                            tipp.Tipp2.Tipp = tipp.Tipp3.Tipp;
                            tipp.Tipp3.Tipp = BASETIPP.EMPTY;
                            if(tipp.Tipp1.Tipp == BASETIPP.EMPTY) {
                                tipp.Tipp1.Tipp = tipp.Tipp2.Tipp;
                                tipp.Tipp2.Tipp = BASETIPP.EMPTY;
                            }
                            _alaptipp.Set( i, tipp );
                        } else {
                            if(tipp.Tipp2.Tipp == BASETIPP.EMPTY) {
                                tipp.Tipp2.Tipp = tipp.Tipp3.Tipp;
                                tipp.Tipp3.Tipp = BASETIPP.EMPTY;
                                _alaptipp.Set( i, tipp );
                            }
                        }
                        break;
                    case 2:
                        if(tipp.Tipp1.Tipp == BASETIPP.EMPTY) {
                            tipp.Tipp1.Tipp = tipp.Tipp2.Tipp;
                            tipp.Tipp2.Tipp = BASETIPP.EMPTY;
                            _alaptipp.Set( i, tipp );
                        }
                        break;
                    default:
                        break;
                }
                #endregion

                #region Ismétlődések ellenőrzése
                if((tipp.Tipp1.Tipp == tipp.Tipp2.Tipp || tipp.Tipp1.Tipp == tipp.Tipp3.Tipp) && tipp.Tipp1.Tipp != BASETIPP.EMPTY) {
                    return false;
                }
                if(tipp.Tipp2.Tipp == tipp.Tipp3.Tipp && tipp.Tipp2.Tipp != BASETIPP.EMPTY) {
                    return false;
                }
                #endregion
            } //next
            return true;
        }
    }
}