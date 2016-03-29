using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using MVCToto.Models.Shared.Controller;
using MVCToto.Models.Toto.Interface;
using MVCToto.Models.Toto.Logger;

namespace MVCToto.Toto {
    public class TotoControllerFactory : IControllerFactory {
        public IController CreateController( RequestContext requestContext, string controllerName ) {
            if(controllerName == "favico.ico")
                return null;

            IController controller = null;
            Type controllerType = null;

            MyControllerXmlParser parser = new MyControllerXmlParser();
            List<ControllerTypeInfo> types = parser.Parse();

            ControllerTypeInfo ti = types.FirstOrDefault( t => t.Name == controllerName );
            if(ti == null)
                throw new KeyNotFoundException();

            controllerType = Type.GetType( ti.Path );//Controller
            ITotoLogger logger = new DebuggerLogger();//Logger

            controller = (IController)Activator.CreateInstance( controllerType, logger );

            return controller;
        }

        public SessionStateBehavior GetControllerSessionBehavior( RequestContext requestContext, string controllerName ) {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController( IController controller ) {
            if(controller is IDisposable) {
                (controller as IDisposable).Dispose();
            }
        }
    }
}