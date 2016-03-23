using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCToto.Models.Toto.General {
    public class MyPagination {
        private int _start { get; set; }
        private int _end { get; set; }
        private int _actPage { get; set; }
        public int count { get; protected set; }
        public int onePage { get; protected set; }

        // TODO: Ezt még meg kell csinálni
        public bool FirstEnabled { get; protected set; }
        public bool PrevEnabled { get; protected set; }
        public bool NextEnabled { get; protected set; }
        public bool LastEnabled { get; protected set; }

        private int minPage = 1;
        private int maxPage;

        public int GetStart() {
            return _start;
        }
        public int GetEnd() {
            return _end;
        }
        //public int GetActPage() {
        //    return _actPage;
        //}

        //count:Ennyi darab van
        //onePage: Egy oldara ennyi
        //actPage: Melyik oldalszámra számolja ki a kezdetét és végét 1-től - akármeddig
        //return: 1 -től kezdi a számozást!!!
        public MyPagination( int count = 0, int onePage = 20, int aktPage = 1 ) {
            this.onePage = onePage;
            this.SetCount( count );
            this.SetActPage( aktPage );
        }


        public void SetCount( int count ) {
            this.count = Math.Max( 0, count );
            this.maxPage = (count - 1) / onePage + 1;
        }
        public void SetOnePage( int onePage ) {
            this.onePage = onePage;
        }
        public void SetActPage( int actPage ) {
            this._actPage = Math.Min( Math.Max( actPage, minPage ), maxPage );
            CalculateFromPage();
        }


        private void CalculateFromPage() {
            _start = (_actPage - 1) * onePage + 1;
            _end = Math.Min( _actPage * onePage, count );

            FirstEnabled = _actPage != 1;
            PrevEnabled = _actPage != 1;
            NextEnabled = _actPage != maxPage;
            LastEnabled = _actPage != maxPage;
        }

        public void Next() {
            this.SetActPage( this._actPage + 1 );
        }
        public void Prev() {
            this.SetActPage( this._actPage - 1 );
        }
        public void First() {
            this.SetActPage( 1 );
        }
        public void Last() {
            this.SetActPage( this.maxPage );
        }
    }
}