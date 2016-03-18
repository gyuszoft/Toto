using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCToto.Controllers.Toto;
using MVCToto.Models.Repo;
using MVCToto.Models.Toto.Logger;
using System.Web.Mvc;

namespace MVCToto.Tests {
    [TestClass]
    public class TotoControllerTest {
        #region Init
        private TotoController controller;
        public TotoControllerTest() {
            controller = new TotoController( new DebuggerLogger(), new TotoHome() );
        }
        #endregion


        [TestMethod]
        public void TestIndexAction() {
            ActionResult result = controller.Index();
            var model = ((ViewResult)result).Model;
            Assert.IsNull( model, "Model not should be presented." );
        }

        [TestMethod]
        public void TestGenerateAction() {
            ActionResult result = controller.Generate();
            var model = ((ViewResult)result).Model;
            Assert.IsNotNull( model, "Model should be presented." );
        }

    }
}
