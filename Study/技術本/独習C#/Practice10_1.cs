using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dokushu
{
    // デリゲート
    class Practice10_1
    {

        // デリゲート (Cでいう関数ポインタのようなもの)
        // 戻り値と型が同じなら代入できる。
        delegate void EventPart(string s);

        static void Main(string[] args)
        {
            // デリゲード
            Show1();
            
            // 匿名メソッド
            Show2(delegate(string name) { Console.WriteLine(name + "エフェクトを発生させる。"); });
            
            // ラムダ式
            Show2((String name) => Console.WriteLine("name + エフェクト発生"));
        }

        // デリゲード
        static void Show1()
        {
            EventPart eventParts;
            var eventManager = new EventManager();

            // コレクションのように足しこめる。
            eventParts = eventManager.Wait;
            eventParts += eventManager.Action1;
            eventParts += eventManager.Action2;

            // 追加した全てのメソッドを実行する。
            eventParts("");

            // 削除もできる。
            eventParts -= eventManager.Action1;

            eventParts("");
        }

        // 匿名メソッド
        static void Show2(Action<string> action)
        {
            action("");
        }

    }

    class EventManager
    {
        public void Action1(string name)
        {
            Console.WriteLine(name + ":文字を表示する。");
        }
        public void Action2(string name)
        {
            Console.WriteLine(name + "キャラクターを移動させる。");
        }
        public void Wait(string name)
        {
            Console.WriteLine(name + "待ち時間");
        }
    }
}
