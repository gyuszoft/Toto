using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MVCToto.Models.Toto.Interface;

namespace MVCToto.Models.Toto.Logger {
    public class DebuggerLogger : ITotoLogger {
        public void Log( string msg ) {
            Debug.WriteLine( msg + " - " + DateTime.Now );
        }
    }
}