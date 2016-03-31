using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCToto.Models.Toto.General {
    public class MyPagination {
        #region Private
        private const byte DEFAULT_ONE_PAGE_COUNT = 20; //Egy oldalra ennyi sor alapértelmezetten
        private const byte DEFAULT_X_COUNT = 5; //a minx-maxx ennyi számot tartalmazhat 

        private int _actPage;   // Akt. oldal
        private int _count;     // Egy az össz darabszám  
        private int _onePage;   // Egy oldara ennyi

        private const int MIN_PAGE = 1;
        private int _maxPage; //Maximális oldalszám

        private int _minMaxCount = DEFAULT_X_COUNT;
        #endregion

        public int Start { get; private set; }
        public int End { get; private set; }
        public int MinX { get; private set; }
        public int MaxX { get; private set; }

        public int OnePage {
            get { return _onePage; }
            set { SetOnePage( value ); }
        }

        public bool FirstEnabled { get; protected set; }
        public bool PrevEnabled { get; protected set; }
        public bool NextEnabled { get; protected set; }
        public bool LastEnabled { get; protected set; }

        public MyPagination( int onePage = DEFAULT_ONE_PAGE_COUNT ) {
            OnePage = onePage;
        }

        public void SetCount( int count ) {
            _count = Math.Max( 0, count );
            _maxPage = (count - 1) / _onePage + 1;
        }

        public void SetOnePage( int onePage ) {
            if(onePage > 0) {
                _onePage = onePage;
                SetCount(_count); //MaxPage-t ki kell számolni újra
            }
        }

        public void SetActPage( int actPage ) {
            _actPage = Math.Min( Math.Max( actPage, MIN_PAGE ), _maxPage );
            CalculateFromPage();
        }

        private void CalculateFromPage() {
            Start = (_actPage - 1) * _onePage + 1;
            End = Math.Min( _actPage * _onePage, _count );

            FirstEnabled = _actPage != 1;
            PrevEnabled = _actPage != 1;
            NextEnabled = _actPage != _maxPage;
            LastEnabled = _actPage != _maxPage;

            #region Minx MaxX számolás
            var xL = (_minMaxCount - 1) / 2;
            var xR = (_minMaxCount - 1) / 2;
            xR = xL + xR + 1 == _minMaxCount ? xR : xR + 1;
            MinX = _actPage - xL;
            MaxX = _actPage + xR;
            if(MinX <= 0) {
                MinX = 1;
                MaxX = _minMaxCount;
            }
            if(MaxX >= _maxPage) {
                MaxX = _maxPage;
                MinX = _maxPage - _minMaxCount;
            }

            MinX = Math.Max( MinX, 1 );
            MaxX = Math.Min( MaxX, _maxPage );
            #endregion
        }

        public void First() {
            SetActPage( 1 );
        }
        public void Next() {
            SetActPage( _actPage + 1 );
        }
        public void Prev() {
            SetActPage( _actPage - 1 );
        }
        public void Last() {
            SetActPage( _maxPage );
        }
    }
}