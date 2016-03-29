using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using MVCToto.Models.Toto.Interface;

namespace MVCToto.Models.Toto.Logger {
    public class SqlLogger : ITotoLogger {
        public void Log( string msg ) {
            // Szimuláljuk az adatbázisba írást
            Thread.Sleep( 2000 );
            Debug.WriteLine( msg + " - " + DateTime.Now );
        }
    }
}