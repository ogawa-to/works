﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dokushu
{
    class Practice7_2
    {
        // ref, out , tuple, 匿名型
        static void Main(string[] args)
        {
            // 参照渡しの場合はrefを書く。(C++の &に相当)
            int n = 10;
            Increment(ref n);

            // 戻り値のデータを書き換えることで配列の値も変わる。
            var data = new int[] { 10, 20, 30 };
            int data_0 = Decrement(data);
            ref int data_1 = ref Decrement(data);
            data_1 = 40;



            // out引数による戻り値の設定。
            int n1 = 100;
            int n2 = 100;
            Devide(out n1, out n2);



            // tupleによる戻り値の設定。
            (int x, int y, int z) r = ReturnTupple();
            (var x1, var y1, var z1) = ReturnTupple();
            var (x2, y2, z2) = ReturnTupple();

            // tupleによる代入
            var axis = (100, 200, 300);
            int sum = axis.Item1 + axis.Item2 + axis.Item3;



            // 匿名型(名前付きtupleのようなものでreadonly)
            var tokumei = new { param1 = 1, param2 = "2" };
            // tokumei.param1 = 10; // エラーになる。
        }

        // 値の参照渡し
        static void Increment(ref int n)
        {
            n++;
        }

        // 戻り値の参照渡し
        static ref int Decrement(int[] data)
        {
            data[0]--;
            return ref data[0];
        }

        // outによる複数の戻り値の設定。
        static void Devide(out int n1, out int n2)
        {
            n1 = 10;
            n2 = 20;
        }

        // tupleによる複数の戻り値の設定。
        static (int r1, int r2, int r3) ReturnTupple()
        {
            return (10, 20, 30);
        }
    }
}
