namespace console
{
    class Other
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
            Other o = new Other();
            o.print("hello");
        }

        static void Main(string[] args)
        {
            Other o = new Other();
            o.print("hello");
            o.printhi();
            Program p = new Program();
            p.printhello();
        }
    }
}