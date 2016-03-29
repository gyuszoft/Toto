﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVCToto.Models.Toto {
    // Abstract class, csak próba abszolút semmi értelme most :)
    public abstract class TotoBaseAbstract {
        private static int _instanceCount = 0;
        public int InstanceCount { get { return _instanceCount; } }

        public TotoBaseAbstract() {
            _instanceCount++;
        }
        ~TotoBaseAbstract() {
            _instanceCount--;
        }
    }

    public enum Basetipp { EMPTY, _1, _2, X }
    //Csak egy tipp, 1 2 x üres lehet
    public class TotoBaseTipp : TotoBaseAbstract {
        public int TippId { get; protected set; }
        public TotoBaseTipp():base() {}
        public Basetipp Tipp {
            get { return (Basetipp)TippId; }
            set { TippId = (int)value; }
        }
        public string Display() {
            switch((Basetipp)TippId) {
                case Basetipp.EMPTY:
                    return " ";
                case Basetipp._1:
                    return "1";
                case Basetipp._2:
                    return "2";
                case Basetipp.X:
                    return "X";
                default:
                    return "?";
            }
        }
    }

    // Egy meccsre a tipp
    public class TotoTipp : TotoBaseAbstract {
        //Ennyi 1,2 vagy 3 esély a tipp
        public TotoBaseTipp Tipp1 { get; set; }
        public TotoBaseTipp Tipp2 { get; set; }
        public TotoBaseTipp Tipp3 { get; set; }
        public TotoTipp():base() {
            Tipp1 = TotoFactory.NewBaseTipp();
            Tipp2 = TotoFactory.NewBaseTipp();
            Tipp3 = TotoFactory.NewBaseTipp();
        }

        [Range( 0, 3 )]
        public byte Esely {
            get {
                if(Tipp3.Tipp != Basetipp.EMPTY)
                    return 3;
                if(Tipp2.Tipp != Basetipp.EMPTY)
                    return 2;
                if(Tipp1.Tipp != Basetipp.EMPTY)
                    return 1;
                return 0;
            }
        }
    }

    #region TotoAlaptipp
    public interface ITotoAlaptipp {
        TotoTipp[] AlapTipp { get; set; }
        void Set( int i, TotoTipp tipp );
    }

    // Alaptipp, ebből generáljuk le az oszlopokat
    public class TotoAlapTipp : TotoBaseAbstract,ITotoAlaptipp {
        public TotoTipp[] AlapTipp { get; set; }
        public TotoAlapTipp():base() {
            AlapTipp = new TotoTipp[TotoConst.TOTO_SOR + 1];
            for(int i = 1; i < AlapTipp.Length; i++) {
                AlapTipp[i] = TotoFactory.NewTotoTipp();
                AlapTipp[i].Tipp1 = TotoFactory.NewBaseTipp( Basetipp._1 );
            }
        }
        public void Set( [Range( 1, TotoConst.TOTO_SOR + 1 )] int i, TotoTipp tipp ) {
            if(i >= 1 && i <= TotoConst.TOTO_SOR) {
                AlapTipp[i] = tipp;
            }
        }
    }
    #endregion

    // Egy toto oszlop, max 14 lehet
    public class TotoEgyOszlop:TotoBaseAbstract {
        public TotoBaseTipp[] Oszlop { get; set; }
        public TotoEgyOszlop()
        {
            Oszlop = new TotoBaseTipp[TotoConst.TOTO_SOR + 1];
            for(int i = 1; i < Oszlop.Length; i++) {
                Oszlop[i] = TotoFactory.NewBaseTipp();
            }
        }

        public void Set( [Range( 1, TotoConst.TOTO_SOR + 1 )] int i, TotoBaseTipp tipp ) {
            if(i >= 1 && i <= TotoConst.TOTO_SOR) {
                Oszlop[i] = tipp;
            }
        }

        public TotoEgyOszlop Clone() {
            var clone = TotoFactory.NewTotoEgyOszlop();
            Array.Copy( Oszlop, clone.Oszlop, Oszlop.Length );
            return clone;
        }
    }
    // Tippsor
    public class TotoTippSor:TotoBaseAbstract {
        public List<TotoEgyOszlop> TippSor { get; set; }
        public TotoTippSor():base() {
            TippSor = new List<TotoEgyOszlop>();
        }
        public void Clear() {
            TippSor.Clear();
        }
        public void Add( TotoEgyOszlop oszlop ) {
            TippSor.Add( oszlop );
        }

    }
}