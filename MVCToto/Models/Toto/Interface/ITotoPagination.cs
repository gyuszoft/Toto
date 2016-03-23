﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCToto.Models.Toto.Interface {
    public interface ITotoPagination {
        void SetCount( int count );      //Ennyi az összes
        void SetOnePage( int onePage );  //Ennyi megy egy oldalra 
        void SetActPage( int actPage );  //Aktuális oldal

        int GetStart(); //Ettől
        int GetEnd()  ; //Eddig kell kiírni

        void Next(); //Köv oldal
        void Prev(); //Előző oldal
        void First(); //Első oldal
        void Last(); //Utolsó oldal
    }
}