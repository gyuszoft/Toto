using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCToto.Models.Toto.General {
    static class MySession {
        public static T GetSession<T>( string sessionId ) {
            T val = default( T );
            var session = System.Web.HttpContext.Current.Session;

            if(session[sessionId] != null) {
                val = (T)session[sessionId];
            }

            return val;
        }
    }
}