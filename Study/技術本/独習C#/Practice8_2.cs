﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dokushu
{
    // インデクサ
    class Practice8_2
    {
        static void Main(string[] args)
        {
            // プロパティを介してprivateな変数にアクセスできる。
            Sample s = new Sample();
            s.Param1 = 10;
            int n = s.Param1;

            FreeArray array = new FreeArray(10);
            array[3] = 10;
        }
    }

    class FreeArray
    {

        // プロパティの配列版
        // 配列にアクセスした際の挙動を決める。
        private int[] _list;
        public int this[int index]
        {
            set
            {
                this._list[this.GetIndex(index)] = value;
            }
            get
            {
                return this._list[this.GetIndex(index)];
            }
        }

        public FreeArray(int size)
        {
            this._list = new int[size];
        }

        // 引数で渡したインデックスの次のインデックスを返却する。
        private int GetIndex(int index)
        {
            return ++index;
        }
    }


}
