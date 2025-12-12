using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Value
    {
        internal bool active;
        internal object? item;
        internal object? name;

        internal object? getValue()
        {
            if (active)
                return item;
            else
                return "Item \"" + item + "\" is not active";
        }

        internal object? getName()
        {
            return name;
        }
    }

    class ListManager
    {
        private List<object> list = new List<object>();

        internal void Add(object item)
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

        internal void PrintAll(Action<object> printAction)
        {
            foreach (var item in list)
                printAction(item.ToString() ?? "nil");
        }

        internal void PrintCount(Action<object> printAction)
        {
            printAction(list.Count.ToString());
        }

        internal int GetIndex(object item)
        {
            return list.IndexOf(item);
        }

        internal List<object> GetList()
        {
            return list;
        }

        internal void SetList(List<object> list)
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

            List<Value> values = new List<Value>
            {
                new Value { active = true, item = "a" },
                new Value { active = false, item = "b" },
                new Value { active = true, item = "c" }
            };

            List<Value> inactives = values.Where(x => !x.active).ToList();

            foreach (var value in inactives)
                Console.WriteLine(value.getName() + ": " + value.getValue());
        }
    }
}
