using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCToto.Models.Toto.General {
    //public enum PAGE_ACTION { FIRST, NEXT, PREV, LAST };

    public class MyPagination {
        public int start { get; protected set; }
        public int end { get; protected set; }
        public int actPage { get; protected set; }
        public int count { get; protected set; }
        public int onePage { get; protected set; }

        private int minPage = 1;
        private int maxPage;

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
            this.actPage = Math.Min( Math.Max( actPage, minPage ), maxPage );
            CalculateFromPage();
        }

        private void CalculateFromPage() {
            start = (actPage - 1) * onePage + 1;
            end = Math.Min( actPage * onePage, count );
        }

        public void Next() {
            this.SetActPage( this.actPage + 1 );
        }
        public void Prev() {
            this.SetActPage( this.actPage - 1 );
        }
        public void First() {
            this.SetActPage( 1 );
        }
        public void Last() {
            this.SetActPage( this.maxPage );
        }
    }
}