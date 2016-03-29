using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCToto.Controllers.Toto;
using MVCToto.Models.Toto.Logger;
using MVCToto.Models.Toto;
using System.Web.Mvc;
using MVCToto.Models.Toto.General;
using MVCToto.Models.Toto.Repo;

namespace MVCToto.Tests {
    [TestClass]
    public class TotoControllerTest {
        #region Init
        private readonly TotoController _controller;
        public TotoControllerTest() {
            _controller = new TotoController(
                    new DebuggerLogger(),
                    new TotoHome( TotoFactory.NewTotoAlapTipp() ),
                        TotoSessionFactory.Pagination );
        }
        #endregion

        [TestMethod]
        public void TestIndexAction() {
            ActionResult result = _controller.Index();
            var model = ((ViewResult)result).Model;
            Assert.IsNull( model, "Model not should be presented." );
        }

        [TestMethod]
        public void TestGenerateAction() {
            ActionResult result = _controller.Generate();
            var model = ((ViewResult)result).Model;
            Assert.IsNotNull( model, "Model should be presented." );
        }

    }
}
