using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCToto.Models.Toto {
    static class TotoFactory {
        public static TotoBaseTipp NewBaseTipp( BASETIPP tipp = BASETIPP.EMPTY ) {
            return new TotoBaseTipp() { Tipp = tipp };
        }

        public static TotoTipp NewTotoTipp() {
            return new TotoTipp();
        }

        public static TotoAlapTipp NewTotoAlapTipp() {
            return new TotoAlapTipp();
        }

        public static TotoEgyOszlop NewTotoEgyOszlop() {
            return new TotoEgyOszlop();
        }

        public static TotoTippSor NewTotoTippSor() {
            return new TotoTippSor();
        }
    }
}