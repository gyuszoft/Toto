using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVCToto.Models.Toto {
    // Abstract class, csak próba abszolút semmi értelme most :)
    public abstract class TotoBaseAbstract {
        private static int _InstanceCount = 0;
        public int InstanceCount { get { return _InstanceCount; } }

        public TotoBaseAbstract() {
            _InstanceCount++;
        }
        ~TotoBaseAbstract() {
            _InstanceCount--;
        }
    }

    public enum BASETIPP { EMPTY, _1, _2, _X }
    //Csak egy tipp, 1 2 x üres lehet
    public class TotoBaseTipp : TotoBaseAbstract {
        public int Tipp_ID { get; protected set; }
        public TotoBaseTipp():base() {}
        public BASETIPP Tipp {
            get { return (BASETIPP)Tipp_ID; }
            set { Tipp_ID = (int)value; }
        }
        public string Display() {
            switch((BASETIPP)Tipp_ID) {
                case BASETIPP.EMPTY:
                    return " ";
                case BASETIPP._1:
                    return "1";
                case BASETIPP._2:
                    return "2";
                case BASETIPP._X:
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
                if(Tipp3.Tipp != BASETIPP.EMPTY)
                    return 3;
                if(Tipp2.Tipp != BASETIPP.EMPTY)
                    return 2;
                if(Tipp1.Tipp != BASETIPP.EMPTY)
                    return 1;
                return 0;
            }
        }
    }
    // Alaptipp, ebből generáljuk le az oszlopokat
    public class TotoAlapTipp : TotoBaseAbstract {
        public TotoTipp[] AlapTipp { get; set; }
        public TotoAlapTipp():base() {
            AlapTipp = new TotoTipp[TotoConst.TOTO_SOR + 1];
            for(int i = 1; i < AlapTipp.Length; i++) {
                AlapTipp[i] = TotoFactory.NewTotoTipp();
                AlapTipp[i].Tipp1 = TotoFactory.NewBaseTipp( BASETIPP._1 );
            }
        }
        public void Set( [Range( 1, TotoConst.TOTO_SOR + 1 )] int i, TotoTipp tipp ) {
            if(i >= 1 && i <= TotoConst.TOTO_SOR) {
                AlapTipp[i] = tipp;
            }
        }
    }

    // Egy toto oszlop, max 14 lehet
    public class TotoEgyOszlop:TotoBaseAbstract {
        public TotoBaseTipp[] Oszlop { get; set; }
        public TotoEgyOszlop():base() {
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