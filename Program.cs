using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class ListManager
    {
        private List<string> list = new List<string>();

        internal void Add(string item)
        {
            list.Add(item);
        }

        internal void Remove(int index)
        {
            if (index >= 0 && index < list.Count)
                list.RemoveAt(index);
        }

        internal void Clear()
        {
            list.Clear();
        }

        internal void PrintAll(Action<string> printAction)
        {
            foreach (var item in list)
                printAction(item);
        }

        internal void PrintCount(Action<string> printAction)
        {
            printAction(list.Count.ToString());
        }

        internal int GetIndex(string item)
        {
            return list.IndexOf(item);
        }

        internal List<string> GetList()
        {
            return list;
        }

        internal void SetList(List<string> list)
        {
            this.list = list;
        }
    }

    class Other
    {
        private bool active;

        internal void Print(string msg)
        {
            Console.WriteLine(msg);
        }

        internal void PrintHi()
        {
            Print("hi");
        }

        internal void SetActive(bool active)
        {
            this.active = active;
        }

        internal bool GetActive()
        {
            return active;
        }
    }

    class Program
    {
        private void PrintHello()
        {
            Other o = new Other();
            o.Print("hello");
        }

        private void PrintActive()
        {
            Other o = new Other();
            o.Print(o.GetActive().ToString());
        }

        static void Main(string[] args)
        {
            Other o = new Other();
            o.Print("hello");
            o.PrintHi();

            Program p = new Program();
            p.PrintHello();

            o.SetActive(true);
            p.PrintActive();

            o.SetActive(false);
            p.PrintActive();

            ListManager list = new ListManager();

            list.Add("item 1");
            list.Add("item 2");
            list.Add("item 3");
            list.PrintAll(o.Print);
            list.PrintCount(o.Print);
            list.Remove(1);
            list.PrintCount(o.Print);
            list.Clear();
            list.PrintCount(o.Print);
        }
    }
}
