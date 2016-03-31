using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCToto.Models.Toto.Interface {
    interface ITotoBaseClasses {
    }

    public interface ITotoBaseTippDisplay {
        string Display( Basetipp tipp );
    }

    public interface ITotoAlapTipp {
        TotoTipp[] AlapTipp { get; set; }
        void Set( int i, TotoTipp tipp );
        ITotoAlapTipp Clear();
    }

    public interface ITotoTippsor {
        void Clear( List<TotoEgyOszlop> list);
        void Clear( List<TotoEgyOszlop> list, int n );
        void Add( List<TotoEgyOszlop> list, TotoEgyOszlop oszlop );
    }
}
