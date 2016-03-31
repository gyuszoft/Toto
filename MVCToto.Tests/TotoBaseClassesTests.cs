using MVCToto.Models.Toto;
using MVCToto.Models.Toto.Repo;
using NUnit.Framework;

namespace MVCToto.Tests {
    [TestFixture()]
    public class TotoBaseClassesTests {
        [Test]
        public void TotoBaseTippTest() {
            var t = TotoFactory.NewBaseTipp( Basetipp.X );
            //Assert.Greater( t.InstanceCount, 0, "Nem jó az abstract osztály!" );
            var x = t.Display();
            Assert.AreEqual( x, "X", "Nem jó a kiírás!" );
            t.Tipp = Basetipp._1;
            x = t.Display();
            Assert.AreEqual( x, "1", "Nem jó a kiírás!" );
            t.Tipp = Basetipp._2;
            x = t.Display();
            Assert.AreEqual( x, "2", "Nem jó a kiírás!" );
            t.Tipp = Basetipp.EMPTY;
            x = t.Display();
            Assert.AreEqual( x, " ", "Nem jó a kiírás!" );
        }

        [Test()]
        public void TotoTippTest() {
            var tipp = TotoFactory.NewTotoTipp();
            //Assert.AreEqual( tipp.InstanceCount, 4, "Instance száma nem jó!" );
            Assert.AreEqual( tipp.Esely, 0, "Az esély száma nem jó! 0" );
            tipp.Tipp1.Tipp = Basetipp.X;
            Assert.AreEqual( tipp.Esely, 1, "Az esély nem jó! 1" );
            tipp.Tipp2.Tipp = Basetipp._1;
            Assert.AreEqual( tipp.Esely, 2, "Az esély nem jó! 2" );
            tipp.Tipp3.Tipp = Basetipp._2;
            Assert.AreEqual( tipp.Esely, 3, "Az esély nem jó! 3" );
            tipp.Tipp3.Tipp = Basetipp.EMPTY;
            Assert.AreEqual( tipp.Esely, 2, "Az esély nem jó! 4" );
        }

        [Test()]
        public void TotoAlaptippTest() {
            var t = TotoFactory.NewTotoAlapTipp();
            Assert.AreEqual( t.AlapTipp.Length, 15, "Nem megfelelő alaptipp array" );

            var t1 = new TotoTipp { Tipp1 = { Tipp = Basetipp.X } };
            var t2 = new TotoTipp {
                Tipp1 = { Tipp = Basetipp._1 },
                Tipp2 = { Tipp = Basetipp._2 }
            };
            var t3 = new TotoTipp {
                Tipp1 = { Tipp = Basetipp.X },
                Tipp2 = { Tipp = Basetipp._1 },
                Tipp3 = { Tipp = Basetipp._2 }
            };
            t.Set( 3, t1 );
            Assert.AreEqual( t.AlapTipp[3].Esely, 1, "Nem megfelelő esély" );
            Assert.AreEqual( t.AlapTipp[3].Tipp1.Tipp, Basetipp.X, "Nem megfelelő tipp" );
            t.Set( 2, t2 );
            Assert.AreEqual( t.AlapTipp[2].Esely, 2, "Nem megfelelő esély" );
            Assert.AreEqual( t.AlapTipp[2].Tipp1.Tipp, Basetipp._1, "Nem megfelelő tipp" );
            Assert.AreEqual( t.AlapTipp[2].Tipp2.Tipp, Basetipp._2, "Nem megfelelő tipp" );
            t.Set( 5, t3 );
            Assert.AreEqual( t.AlapTipp[5].Esely, 3, "Nem megfelelő esély" );
            Assert.AreEqual( t.AlapTipp[5].Tipp1.Tipp, Basetipp.X, "Nem megfelelő tipp" );
            Assert.AreEqual( t.AlapTipp[5].Tipp2.Tipp, Basetipp._1, "Nem megfelelő tipp" );
            Assert.AreEqual( t.AlapTipp[5].Tipp3.Tipp, Basetipp._2, "Nem megfelelő tipp" );
        }

        [Test()]
        public void TotoEgyOszlopTest() {
            var t = TotoFactory.NewTotoEgyOszlop();
            Assert.AreEqual( t.Oszlop.Length, 15, "Nem megfelelő alaptipp array" );
            t.Set( 14, TotoFactory.NewBaseTipp( Basetipp._1 ) );
            t.Set( 12, TotoFactory.NewBaseTipp( Basetipp.X ) );
            var t2 = t.Clone();
            Assert.AreEqual( t.Oszlop[12].Tipp, t2.Oszlop[12].Tipp, "Klónozás nem működik" );
            Assert.AreEqual( t.Oszlop[14].Tipp, t2.Oszlop[14].Tipp, "Klónozás nem működik" );
            Assert.AreEqual( t.Oszlop[14].Tipp, Basetipp._1, "TotoEgyoszlop set nem működik" );
        }

        [Test()]
        public void TotoTippSorTest() {
            var t = TotoFactory.NewTotoTippSor();
            t.Add( TotoFactory.NewTotoEgyOszlop());
            t.Add( TotoFactory.NewTotoEgyOszlop());
            Assert.AreEqual(t.TippSor.Count,2,"Nem jó az oszlopszám");
            t.Clear();
            Assert.AreEqual( t.TippSor.Count, 0, "Nem jó az oszlopszám" );
            t.Add( TotoFactory.NewTotoEgyOszlop() );
            t.Add( TotoFactory.NewTotoEgyOszlop() );
            t.Clear(1);
            Assert.AreEqual( t.TippSor.Count, 1, "Nem jó az oszlopszám" );
        }
    }
}