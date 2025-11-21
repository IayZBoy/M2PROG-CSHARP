namespace console
{
    class other
    {   
        internal void print(string msg)
        {
            Console.WriteLine(msg);
        }

        internal void printhi()
        {
            print("hi");
        }
    }
    class Program
    {
        private void printhello() {
            other o = new other();
            o.print("hello");
        }

        static void Main(string[] args)
        {
            other o = new other();
            o.print("hello");
            o.printhi();
        }
    }
}