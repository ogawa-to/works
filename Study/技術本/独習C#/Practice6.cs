using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dokushu
{
    class Practice6
    {
        /*
        // コレクションでは名前空間はSystem.Collections.Genericを使う。
        static void Main(string[] args)
        {

            //
            // リスト (アクセスが高速)
            // 
            // 初期化
            var list1 = new List<int>();                    // 通常
            var list2 = new List<int>() { 2, 3, 4 };        // 初期化
            var list3 = new List<int> { };                  // かっこの省略
            var list4 = new List<int>(new[] {10, 20, 30});  // 配列で初期化

            list1.Add(1);           // 単一要素の追加
            list1.AddRange(list2);  // リストの追加

            // for分
            foreach (int i in list1) { }
            var enm = list1.GetEnumerator();
            while (enm.MoveNext()) { }

            // 読み取り専用のコレクションもあるらしい。

            // ラムダによる降順ソート
            list1.Sort((a, b) => b - a);

            //
            // リンクリスト (前後への追加・削除が高速)
            // 
            LinkedList<int> linkedList1 = new LinkedList<int>();
            LinkedListNode<int> node;
            linkedList1.AddFirst(1);               // 先頭に追加。
            node = linkedList1.AddLast(3);         // 末尾に追加
            node = linkedList1.AddBefore(node, 2); // nodeの前に追加。
            node = linkedList1.AddAfter(node, 4);  // nodeの後に追加。

            foreach (int i in linkedList1)
            {
                Console.WriteLine(i);
            }

            //
            // スタック
            //
            var stack1 = new Stack<int>();
            stack1.Push(1);
            stack1.Pop();

            //
            // キュー
            //
            var queue1 = new Queue<int>();
            queue1.Enqueue(10);
            queue1.Dequeue();

            //
            // ディクショナリ (javaでいうHashMap)
            //
            // 初期化
            var dictionaly1 = new Dictionary<int, string>();
            var dictionaly2 = new Dictionary<int, string>()
            {
                [10] = "黒魔導士",
                [20] = "シロ魔導士"
            };
            dictionaly1.Add(1, "サンダラ"); // 追加
            dictionaly1.Remove(1);          // 削除

        }
        */
    }
}
