using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dokushu
{
    class Practice7_1
    {
        readonly int x;  // staticの付与が自由          
        const int y = 10;// 暗黙的にstaticが付与される。

        static void _Main(string[] args)
        {
        }

        void callLacalMethod()
        {
            int x = Sum(1, 2);

            // ローカル関数
            int Sum(int a, int b) { return a + b; }
        }

    }
}
