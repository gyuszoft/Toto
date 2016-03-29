﻿using System;
using System.Web.Mvc;
using MVCToto.Models.Toto.Interface;

namespace MVCToto.Models.Toto.Repo {
    public class TotoHome : ITotoHome {
        private ITotoAlaptipp _alaptipp;

        //Generáláshoz local
        private readonly TotoEgyOszlop _locEgyOszlop;
        private readonly TotoTippSor _locTippSor;

        public TotoHome( ITotoAlaptipp alaptipp ) {
            _alaptipp = alaptipp;
            //_alaptipp = TotoFactory.NewTotoAlapTipp();
            _locEgyOszlop = TotoFactory.NewTotoEgyOszlop();
            _locTippSor = TotoFactory.NewTotoTippSor();
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
                tipp.Tipp1.Tipp = (Basetipp)Convert.ToInt32( coll["AlapTipp[" + i + "].Tipp1.Tipp"] );
                tipp.Tipp2.Tipp = (Basetipp)Convert.ToInt32( coll["AlapTipp[" + i + "].Tipp2.Tipp"] );
                tipp.Tipp3.Tipp = (Basetipp)Convert.ToInt32( coll["AlapTipp[" + i + "].Tipp3.Tipp"] );
                alaptipp.Set( i, tipp );
            }
            SetAlaptipp( alaptipp );
            return alaptipp;
        }

        #region Rekurzív generálás

        public TotoTippSor GenerateAllFromAlaptipp() {
            _locTippSor.Clear();
            GenerateAllFromAlaptipp( 1 );
            return _locTippSor;
        }

        private void GenerateAllFromAlaptipp( int aktSor ) {
            if(aktSor == TotoConst.TOTO_SOR + 1) {
                // Itt van kész egy oszlop
                _locTippSor.Add( _locEgyOszlop.Clone() );
            } else {
                if(_alaptipp.AlapTipp[aktSor].Esely == 0) {
                    _locEgyOszlop.Set( aktSor, TotoFactory.NewBaseTipp( Basetipp._1 ) );
                    GenerateAllFromAlaptipp( aktSor + 1 );
                } else {

                    for(int i = 0; i < _alaptipp.AlapTipp[aktSor].Esely; i++) {
                        switch(i) {
                            case 0:
                                _locEgyOszlop.Set( aktSor, _alaptipp.AlapTipp[aktSor].Tipp1 );
                                GenerateAllFromAlaptipp( aktSor + 1 );
                                break;
                            case 1:
                                _locEgyOszlop.Set( aktSor, _alaptipp.AlapTipp[aktSor].Tipp2 );
                                GenerateAllFromAlaptipp( aktSor + 1 );
                                break;
                            case 2:
                                _locEgyOszlop.Set( aktSor, _alaptipp.AlapTipp[aktSor].Tipp3 );
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
                        if(tipp.Tipp1.Tipp == Basetipp.EMPTY) {
                            tipp.Tipp1.Tipp = tipp.Tipp2.Tipp;
                            tipp.Tipp2.Tipp = tipp.Tipp3.Tipp;
                            tipp.Tipp3.Tipp = Basetipp.EMPTY;
                            if(tipp.Tipp1.Tipp == Basetipp.EMPTY) {
                                tipp.Tipp1.Tipp = tipp.Tipp2.Tipp;
                                tipp.Tipp2.Tipp = Basetipp.EMPTY;
                            }
                            _alaptipp.Set( i, tipp );
                        } else {
                            if(tipp.Tipp2.Tipp == Basetipp.EMPTY) {
                                tipp.Tipp2.Tipp = tipp.Tipp3.Tipp;
                                tipp.Tipp3.Tipp = Basetipp.EMPTY;
                                _alaptipp.Set( i, tipp );
                            }
                        }
                        break;
                    case 2:
                        if(tipp.Tipp1.Tipp == Basetipp.EMPTY) {
                            tipp.Tipp1.Tipp = tipp.Tipp2.Tipp;
                            tipp.Tipp2.Tipp = Basetipp.EMPTY;
                            _alaptipp.Set( i, tipp );
                        }
                        break;
                    default:
                        break;
                }
                #endregion

                #region Ismétlődések ellenőrzése
                if((tipp.Tipp1.Tipp == tipp.Tipp2.Tipp || tipp.Tipp1.Tipp == tipp.Tipp3.Tipp) && tipp.Tipp1.Tipp != Basetipp.EMPTY) {
                    return false;
                }
                if(tipp.Tipp2.Tipp == tipp.Tipp3.Tipp && tipp.Tipp2.Tipp != Basetipp.EMPTY) {
                    return false;
                }
                #endregion
            } //next
            return true;
        }
    }
}