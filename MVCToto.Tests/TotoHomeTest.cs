using NUnit.Framework;
using MVCToto.Models.Toto.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCToto.Models.Toto;
using MVCToto.Models.Toto.Interface;

namespace MVCToto.Tests {
    [TestFixture()]
    public class TotoHomeTest {
        private ITotoHome _repo;

        public TotoHomeTest() {
            _repo = new TotoHome( new TotoAlapTipp() );
        }

        private void SetAlaptipp() {
            var a = _repo.GetAlaptipp().Clear();
            a.AlapTipp[1].Tipp1.Tipp = Basetipp.X;      //x 1
            a.AlapTipp[1].Tipp2.Tipp = Basetipp._1;     //1 2 x
            a.AlapTipp[2].Tipp2.Tipp = Basetipp._2;     //1
            a.AlapTipp[2].Tipp3.Tipp = Basetipp.X;      //1

            _repo.SetAlaptipp( a );
        }

        [Test()]
        public void IsValidAlaptippTest() {
            var a = _repo.GetAlaptipp().Clear();
            Assert.AreEqual( a.AlapTipp[1].Tipp2.Tipp, Basetipp.EMPTY, "Clear hiba" );

            a.AlapTipp[3].Tipp1.Tipp = Basetipp.X;
            a.AlapTipp[3].Tipp2.Tipp = Basetipp.EMPTY;
            a.AlapTipp[3].Tipp3.Tipp = Basetipp.X;
            Assert.IsFalse( _repo.IsValidAlaptipp(), "Alaptipp ellenőrzés hiba 1" );

            a.AlapTipp[3].Tipp1.Tipp = Basetipp.EMPTY;
            a.AlapTipp[3].Tipp2.Tipp = Basetipp.X;
            a.AlapTipp[3].Tipp3.Tipp = Basetipp.X;
            Assert.IsFalse( _repo.IsValidAlaptipp(), "Alaptipp ellenőrzés hiba 2" );

            a.AlapTipp[3].Tipp1.Tipp = Basetipp.X;
            a.AlapTipp[3].Tipp2.Tipp = Basetipp.X;
            a.AlapTipp[3].Tipp3.Tipp = Basetipp.EMPTY;
            Assert.IsFalse( _repo.IsValidAlaptipp(), "Alaptipp ellenőrzés hiba 2/2" );

            a.AlapTipp[3].Tipp1.Tipp = Basetipp.EMPTY;
            a.AlapTipp[3].Tipp2.Tipp = Basetipp.EMPTY;
            a.AlapTipp[3].Tipp3.Tipp = Basetipp.EMPTY;
            Assert.IsFalse( _repo.IsValidAlaptipp(), "Alaptipp ellenőrzés hiba 2/3" );

            a.AlapTipp[3].Tipp1.Tipp = Basetipp._1;
            a.AlapTipp[3].Tipp2.Tipp = Basetipp.EMPTY;
            a.AlapTipp[3].Tipp3.Tipp = Basetipp.X;
            Assert.IsTrue( _repo.IsValidAlaptipp(), "Alaptipp ellenőrzés hiba 3" );

            a.AlapTipp[3].Tipp1.Tipp = Basetipp.EMPTY;
            a.AlapTipp[3].Tipp2.Tipp = Basetipp.EMPTY;
            a.AlapTipp[3].Tipp3.Tipp = Basetipp.X;
            Assert.IsTrue( _repo.IsValidAlaptipp(), "Alaptipp ellenőrzés hiba 4" );

            a.AlapTipp[3].Tipp1.Tipp = Basetipp.EMPTY;
            a.AlapTipp[3].Tipp2.Tipp = Basetipp.X;
            a.AlapTipp[3].Tipp3.Tipp = Basetipp.EMPTY;
            Assert.IsTrue( _repo.IsValidAlaptipp(), "Alaptipp ellenőrzés hiba 5" );
        }

        [Test()]
        public void TotoHomeRepoTest() {
            SetAlaptipp();   
            Assert.AreEqual( _repo.GetAlaptipp().AlapTipp[1].Tipp2.Tipp, Basetipp._1, "Nem megfelelő alaptipp visszaolvasás" );
            var totoTippSor = _repo.GenerateAllFromAlaptipp();
            Assert.AreEqual( totoTippSor.TippSor.Count, 6, "Nem jó a generálás" );
            Assert.AreEqual( totoTippSor.TippSor[1].Oszlop[2].Tipp, Basetipp._2, "Nem jó a generálás 2" );
        }

    }
}