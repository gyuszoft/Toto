using MVCToto.Models.Toto.General;
using MVCToto.Models.Toto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCToto.Models.Toto {
    public class TotoPagination : ITotoPagination {
        public MyPagination pagi { get; }

        public TotoPagination( MyPagination pagi) {
            this.pagi = pagi;
        }

        public void First() {
            pagi.First();
        }

        public void Last() {
            pagi.Last();
        }

        public void Next() {
            pagi.Next();
        }

        public void Prev() {
            pagi.Prev();
        }

        public void SetActPage( int actPage ) {
            pagi.SetActPage( actPage );
        }

        public void SetCount( int count ) {
            pagi.SetCount( count );
        }

        public void SetOnePage( int onePage ) {
            pagi.SetOnePage( onePage );
        }

        public int GetStart() {
            return pagi.GetStart();
        }

        public int GetEnd() {
            return pagi.GetEnd();
        }
    }
}