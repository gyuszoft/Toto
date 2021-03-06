﻿using MVCToto.Models.Toto.General;
using MVCToto.Models.Toto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCToto.Models.Toto {
    public class TotoPagination : ITotoPagination {
        public MyPagination Pagi { get; }

        public int Start => Pagi.Start;
        public int End => Pagi.End;
        public int MinX => Pagi.MinX;
        public int MaxX => Pagi.MaxX;

        public bool FirstEnabled => Pagi.FirstEnabled;
        public bool PrevEnabled => Pagi.PrevEnabled;
        public bool NextEnabled => Pagi.NextEnabled;
        public bool LastEnabled => Pagi.LastEnabled;

        public int OnePage {
            get { return Pagi.OnePage; }
            set { Pagi.OnePage = value; }
        }

        public TotoPagination( MyPagination pagi) {
            Pagi = pagi;
        }

        public void First() {
            Pagi.First();
        }

        public void Last() {
            Pagi.Last();
        }

        public void SetCount(int count) {
            Pagi.SetCount(count);
        }

        public void Next() {
            Pagi.Next();
        }

        public void Prev() {
            Pagi.Prev();
        }

        public void SetActPage( int actPage ) {
            Pagi.SetActPage( actPage );
        }
    }
}