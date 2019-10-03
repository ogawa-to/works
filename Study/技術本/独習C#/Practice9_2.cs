﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dokushu
{
    // 自前のジェネリッククラス
    class Practice9_2
    {
        static void Main(string[] args)
        {
            var s = new Sample<int, float>(15, 20.0f);
            s.Show();
            s = new Sample<int, float>();
            s.Show();
        }
        
    }

    // ジェネリックなクラスを定義する。
    class Sample<T1, T2>
    {
        private T1 _item1 { get; set; }
        private T2 _item2 { get; set; }

        public Sample()
        {
            this._item1 = default(T1);
            this._item2 = default(T2);
        }

        public Sample(T1 t1, T2 t2)
        {
            this._item1 = t1;
            this._item2 = t2;
        }
        public void Show ()
        {
            Console.WriteLine(_item1.ToString());
            Console.WriteLine(_item2.ToString());
        }

        public (T1, T2) getT()
        {
            return (_item1, _item2);
        }
    }

}
