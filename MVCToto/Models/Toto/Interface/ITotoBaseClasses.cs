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

    public interface ITotoAlaptipp {
        TotoTipp[] AlapTipp { get; set; }
        void Set( int i, TotoTipp tipp );
    }

    public interface ITotoTippsor {
        void Clear( List<TotoEgyOszlop> list);
        void Add( List<TotoEgyOszlop> list, TotoEgyOszlop oszlop );
    }
}
