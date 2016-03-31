using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using MVCToto.Models.Toto.Interface;

namespace MVCToto.Models.Toto {
    // Abstract class, csak próba abszolút semmi értelme most :)
    public abstract class TotoBaseAbstract {
/*        private static int _instanceCount = 0;
        public int InstanceCount { get { return _instanceCount; } }

        protected TotoBaseAbstract() {
            _instanceCount++;
        }
        ~TotoBaseAbstract() {
            _instanceCount--;
        }*/
    }

    public enum Basetipp { EMPTY, _1, _2, X }
    //Csak egy tipp, 1 2 x üres lehet

    #region TotoBaseTipp

   public class TotoBaseTipp : TotoBaseAbstract {
        private int TippId { get; set; }
        public Basetipp Tipp {
            get { return (Basetipp)TippId; }
            set { TippId = (int)value; }
        }

        private readonly ITotoBaseTippDisplay _disp;

        public TotoBaseTipp( ITotoBaseTippDisplay disp ) : base() {
            _disp = disp;
        }

        public string Display() {
            return _disp.Display( Tipp );
        }
    }
    #endregion


    // Egy meccsre a tipp
    public class TotoTipp : TotoBaseAbstract {
        //Ennyi 1,2 vagy 3 esély a tipp
        public TotoBaseTipp Tipp1 { get; set; }
        public TotoBaseTipp Tipp2 { get; set; }
        public TotoBaseTipp Tipp3 { get; set; }
        public TotoTipp() : base() {
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
    // Alaptipp, ebből generáljuk le az oszlopokat
    public class TotoAlapTipp : TotoBaseAbstract, ITotoAlapTipp {
        public TotoTipp[] AlapTipp { get; set; }
        public TotoAlapTipp() : base() {
            AlapTipp = new TotoTipp[TotoConst.TOTO_SOR + 1];
            for(int i = 1; i < AlapTipp.Length; i++) {
                AlapTipp[i] = TotoFactory.NewTotoTipp();
                AlapTipp[i].Tipp1 = TotoFactory.NewBaseTipp( Basetipp._1 );
            }
            Clear();
        }
        public void Set( [Range( 1, TotoConst.TOTO_SOR + 1 )] int i, TotoTipp tipp ) {
            if(i >= 1 && i <= TotoConst.TOTO_SOR) {
                AlapTipp[i] = tipp;
            }
        }

        public ITotoAlapTipp Clear() {
            for(int i = 1; i < AlapTipp.Length; i++) {
                AlapTipp[i].Tipp1.Tipp = Basetipp._1;
                AlapTipp[i].Tipp2.Tipp = Basetipp.EMPTY;
                AlapTipp[i].Tipp3.Tipp = Basetipp.EMPTY;
            }
            return this;
        }
    }
    #endregion

    // Egy toto oszlop, max 14 lehet
    public class TotoEgyOszlop : TotoBaseAbstract {
        public TotoBaseTipp[] Oszlop { get; set; }
        public TotoEgyOszlop() {
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

   
    public class TotoTippSor : TotoBaseAbstract {
        private readonly ITotoTippsor _repo;
        public List<TotoEgyOszlop> TippSor { get; set; }
        public TotoTippSor( ITotoTippsor repo ) : base() {
            _repo = repo;
            TippSor = new List<TotoEgyOszlop>();
        }
        public void Clear() {
            _repo.Clear( TippSor );
        }
        public void Clear(int n) {
            _repo.Clear( TippSor,n );
        }
        public void Add( TotoEgyOszlop oszlop ) {
            _repo.Add( TippSor, oszlop );
        }
    }
}