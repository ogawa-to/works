﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dokushu
{
    // enum
    class Practice9_1
    {
        static void Main(string[] args)
        {
            // Enum -> 数値
            long i = (long)Season.Autumn;
        
            // 数値 -> Enum型
            Season s = (Season)Enum.Parse(typeof(Season), i.ToString());

            Console.WriteLine(i);
            Console.WriteLine(s);
        }
    }

    enum Season : long
    {
        Spring = 1,
        Summer = 2,
        Autumn = 3,
        Winter = 4
    }
}
