using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCToto.Models.Toto.Interface {
    public interface ITotoPagination
    {
        int Start { get; }
        int End { get;}
        int MinX { get;}
        int MaxX { get;}

        bool FirstEnabled { get; }
        bool PrevEnabled { get; }
        bool NextEnabled { get; }
        bool LastEnabled { get; }

        int OnePage { get; set; }

        void SetActPage( int actPage );  //Aktuális oldal
        void SetCount(int count);

        void Next(); //Köv oldal
        void Prev(); //Előző oldal
        void First(); //Első oldal
        void Last(); //Utolsó oldal
    }
}
