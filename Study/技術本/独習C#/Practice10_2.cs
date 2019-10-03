﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dokushu
{
    // LINQ
    // 簡単に言うとforeachの強化版
    // データ集合に対しての検索や集約を簡潔に記述できる。
    // javaのstreamと同様。
    class Practice10_2
    {
        static void Main(string[] args)
        {
            var intList = new List<int> { 5, 1, 3, 4, 2, 1, -1, -3};
            IEnumerable<int> iIte;
            
            // Where (3以下の値を取り出す)
            iIte = intList.Where(s => s <= 3);
            Show(iIte);

            // Select (データを加工する)
            iIte = intList.Select(s => s * s);
            Show(iIte);

            // Distinct (重複削除)
            iIte = intList.Distinct();
            Show(iIte);

            // Order By (並び替え 昇順降順)
            iIte = intList.OrderBy(s => Math.Abs(s));
            iIte = intList.OrderByDescending(s => Math.Abs(s));
            Show(iIte);

            // 全てを複合できる。
            // 実行順序は上からになる。
            iIte = intList
                .Distinct()
                .Where(s => s <= 3)
                .OrderByDescending(s => Math.Abs(s))
                .Select(s => s * s);
            Show(iIte);



            // Sum 合計値
            int sum = intList.Sum();
            // Average 平均値
            double ave = intList.Average();
            // Count 要素数
            int count = intList.Count();
            // Max 最大値
            int max = intList.Max();
            // Min 最小値
            int min = intList.Min();

            Console.Write("合計値: " + sum + " 平均値: " + ave + " 要素数 : " + count + " 最大値 : " + max + " 最小値 :" + min);
        }

        static void Show(IEnumerable<int> itereator) 
        {
            foreach(int i in itereator)
            {
                Console.Write(i);
                Console.Write(" , ");
            }
            Console.WriteLine();
        }
    }
}
