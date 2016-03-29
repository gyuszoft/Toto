using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml.Linq;

namespace MVCToto.Models.Shared.Controller {
    public class MyControllerXmlParser {
        readonly string _filepath;
        public MyControllerXmlParser( string path = @"~/App_Data/Toto/controllers.xml" ) {
            _filepath = HostingEnvironment.MapPath( path );
        }
        public List<ControllerTypeInfo> Parse() {
            XDocument doc = XDocument.Load( _filepath );
            var query = from c in doc.Descendants( "controller" )
                        select new ControllerTypeInfo() {
                            Name = (string)c.Element( "name" ),
                            Path = c.Element( "path" ).Value
                        };
            return query.ToList();

        }
    }
}