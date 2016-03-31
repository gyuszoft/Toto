using MVCToto.Models.Toto;
using MVCToto.Models.Toto.General;
using NUnit.Framework;

namespace MVCToto.Tests {
    [TestFixture()]
    public class TotoPaginationTests {

        [Test()]
        public void PaginationTest() {
            var _egyoldal = 13;
            var _aktPage = 2;
            var _count = 13123;
            var pagi = new TotoPagination( new MyPagination( 20 ) );
            Assert.AreEqual( pagi.OnePage, 20, "Nem jó az ONEPAGE beállítása. 1" );

            pagi.SetCount( _count );
            pagi.OnePage = _egyoldal + 10;
            Assert.AreEqual( pagi.OnePage, _egyoldal + 10, "Nem jó az ONEPAGE beállítása. 2" );

            pagi.OnePage = _egyoldal;

            pagi.SetActPage( _aktPage );
            Assert.AreEqual( pagi.Start, (_aktPage - 1) * _egyoldal + 1, "Nem megfelelő kezdő" );
            Assert.AreEqual( pagi.End, _aktPage * _egyoldal, "Nem megfelelő vég" );

            pagi.Next();
            Assert.AreEqual( pagi.Start, (_aktPage) * _egyoldal + 1, "Nem megfelelő kezdő (Next)" );
            Assert.AreEqual( pagi.End, (_aktPage + 1) * _egyoldal, "Nem megfelelő vég (Next)" );
            Assert.AreEqual( pagi.MinX, 1, "Nem megfelelő minX (Next)" );
            Assert.AreEqual( pagi.MaxX, 5, "Nem megfelelő maxX (Next)" );
            Assert.IsTrue( pagi.FirstEnabled, "Firstenable hiba (Next)" );
            Assert.IsTrue( pagi.PrevEnabled, "Prevenabled hiba (Next)" );
            Assert.IsTrue( pagi.NextEnabled, "Nextenabled hiba (Next)" );
            Assert.IsTrue( pagi.LastEnabled, "Lastenabled hiba (Next)" );

            pagi.Prev();
            Assert.AreEqual( pagi.Start, (_aktPage - 1) * _egyoldal + 1, "Nem megfelelő kezdő (prev)" );
            Assert.AreEqual( pagi.End, _aktPage * _egyoldal, "Nem megfelelő vég (prev)" );
            Assert.AreEqual( pagi.MinX, 1, "Nem megfelelő minX (Prev)" );
            Assert.AreEqual( pagi.MaxX, 5, "Nem megfelelő maxX (Prev)" );
            Assert.IsTrue( pagi.FirstEnabled, "Firstenable hiba (Prev)" );
            Assert.IsTrue( pagi.PrevEnabled, "Prevenabled hiba (Prev)" );
            Assert.IsTrue( pagi.NextEnabled, "Nextenabled hiba (Prev)" );
            Assert.IsTrue( pagi.LastEnabled, "Lastenabled hiba (Prev)" );

            pagi.First();
            Assert.AreEqual( pagi.Start, 1, "Nem megfelelő kezdő (First)" );
            Assert.AreEqual( pagi.End, _egyoldal, "Nem megfelelő vég  (First)" );
            Assert.IsFalse( pagi.FirstEnabled, "Firstenable hiba (First)" );
            Assert.IsFalse( pagi.PrevEnabled, "Prevenabled hiba (First)" );
            Assert.IsTrue( pagi.NextEnabled, "Nextenabled hiba (First)" );
            Assert.IsTrue( pagi.LastEnabled, "Lastenabled hiba (First)" );
            Assert.AreEqual( pagi.MinX, 1, "Nem megfelelő minX (First)" );
            Assert.AreEqual( pagi.MaxX, 5, "Nem megfelelő maxX (First)" );

            pagi.Last();
            Assert.AreEqual( pagi.Start, ((_count - 1) / _egyoldal + 1 - 1) * _egyoldal + 1, "Nem megfelelő kezdő (Last)" );
            Assert.AreEqual( pagi.End, _count, "Nem megfelelő vég  (Last)" );
            Assert.IsTrue( pagi.FirstEnabled, "Firstenable hiba (Last)" );
            Assert.IsTrue( pagi.PrevEnabled, "Prevenabled hiba (Last)" );
            Assert.IsFalse( pagi.NextEnabled, "Nextenabled hiba (Last)" );
            Assert.IsFalse( pagi.LastEnabled, "Lastenabled hiba (Last)" );
            Assert.AreEqual( pagi.MinX, 1005, "Nem megfelelő minX (Last)" );
            Assert.AreEqual( pagi.MaxX, 1010, "Nem megfelelő maxX (Last)" );
        }
    }
}