namespace CSHARP;

using csharp.deps;

class Program
{
    static string tostring(object a)
    {
        if (a == null)
            return "nil";

        return a.ToString() ?? "nil";
    }

    static void print(string s)
    {
        Console.WriteLine(s);
    }

    static void Main(string[] args)
    {
        string to_enc = Utils.RandomString(128);
        string key = Utils.RandomString(32);
        double now = Time.GetTime();
        Byte[] enc = Crypt.Encrypt(to_enc, key);
        now = Time.GetTime() - now;
        print($"Encryption took {Time.FormatTime(now)}\n");
        //print($"{to_enc} & {key}:\n{Crypt.BufHex(enc)}");
        Console.ReadKey();
    }
}