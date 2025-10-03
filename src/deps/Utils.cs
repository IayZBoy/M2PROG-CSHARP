using System;
using System.Text;

namespace csharp.deps
{
    public static class Utils
    {
        private static readonly char[] CHARS;

        static Utils()
        {
            CHARS = new char[62];
            // Uppercase A-Z
            for (int i = 0; i < 26; i++) CHARS[i] = (char)('A' + i);
            // Lowercase a-z
            for (int i = 26; i < 52; i++) CHARS[i] = (char)('a' + i - 26);
            // Digits 0-9
            for (int i = 52; i < 62; i++) CHARS[i] = (char)('0' + i - 52);
        }

        public static string RandomString(int n)
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(n);
            for (int i = 0; i < n; i++)
            {
                sb.Append(CHARS[rnd.Next(0, CHARS.Length)]);
            }
            return sb.ToString();
        }
    }
}
