using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCToto.Models.Toto.Interface;

namespace MVCToto.Models.Toto.Repo {
    public class TotoBaseClassesRepo {
    }
    public class TotoBaseTippDisplay : ITotoBaseTippDisplay {
        public string Display( Basetipp tipp ) {
            switch(tipp) {
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

    public class TotoTippsorRepo : ITotoTippsor {
        public void Clear( List<TotoEgyOszlop> list ) {
            list.Clear();
        }
        public void Clear( List<TotoEgyOszlop> list, int n ) {
            list.RemoveAt(n);
        }

        public void Add( List<TotoEgyOszlop> list, TotoEgyOszlop oszlop ) {
            list.Add( oszlop );
        }
    }
}