using System;
using System.Text;

namespace csharp.deps
{
    public static class Crypt
    {
        private const uint MASK32 = 0xFFFFFFFF;
        private const byte MASK8 = 0xFF;

        public static string BufferToString(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                return string.Empty;

            return string.Join(" ", buffer);
        }

        public static byte[] StringToBuffer(string str)
        {
            if (string.IsNullOrEmpty(str))
                return new byte[0];

            return Encoding.UTF8.GetBytes(str);
        }

        public static string BufHex(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder(buffer.Length * 3);
            foreach (byte b in buffer)
            {
                sb.AppendFormat("0x{0:X2} ", b);
            }
            return sb.ToString().TrimEnd();
        }

        public static string ToHex(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            byte[] bytes = Encoding.UTF8.GetBytes(str);
            StringBuilder sb = new StringBuilder(bytes.Length * 3);
            foreach (byte b in bytes)
            {
                sb.AppendFormat("0x{0:X2} ", b);
            }
            return sb.ToString().TrimEnd();
        }

        private static uint FNV1a32(string s)
        {
            uint h = 2166136261;
            foreach (byte b in Encoding.UTF8.GetBytes(s))
            {
                h ^= b;
                h = (h * 16777619) & MASK32;
            }
            return h;
        }

        private static uint XorShift32(uint state)
        {
            state ^= (state << 13);
            state ^= (state >> 17);
            state ^= (state << 5);
            return state & MASK32;
        }

        private static byte Rol(byte value, int count)
        {
            return (byte)((value << count) | (value >> (8 - count)));
        }

        private static byte Ror(byte value, int count)
        {
            return (byte)((value >> count) | (value << (8 - count)));
        }

        public static byte[] Encrypt(string text, string key)
        {
            uint keyHash = FNV1a32(key);
            uint state = keyHash;
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] outBuf = new byte[data.Length + 4];

            outBuf[0] = (byte)((keyHash >> 24) & 0xFF);
            outBuf[1] = (byte)((keyHash >> 16) & 0xFF);
            outBuf[2] = (byte)((keyHash >> 8) & 0xFF);
            outBuf[3] = (byte)(keyHash & 0xFF);

            for (int i = 0; i < data.Length; i++)
            {
                state = XorShift32(state);
                byte k = (byte)(state & 0xFF);

                byte b = data[i];
                byte rotated = Rol(b, (k % 7) + 1);
                byte added = (byte)((rotated + k) & MASK8);
                byte inverted = (byte)(~added & MASK8);

                outBuf[i + 4] = inverted;
            }

            return outBuf;
        }

        public static string Decrypt(byte[] buf, string key)
        {
            if (buf == null || buf.Length < 4)
                return "Wrong key was supplied.";

            uint storedHash = ((uint)buf[0] << 24) | ((uint)buf[1] << 16) | ((uint)buf[2] << 8) | buf[3];
            uint keyHash = FNV1a32(key);

            if (storedHash != keyHash)
                return "Wrong key was supplied.";

            uint state = keyHash;
            byte[] outBuf = new byte[buf.Length - 4];

            for (int i = 4; i < buf.Length; i++)
            {
                state = XorShift32(state);
                byte k = (byte)(state & 0xFF);

                byte b = buf[i];
                byte inverted = (byte)(~b & MASK8);
                byte sub = (byte)((inverted + (256 - k)) & MASK8);
                byte orig = Ror(sub, (k % 7) + 1);

                outBuf[i - 4] = orig;
            }

            return Encoding.UTF8.GetString(outBuf);
        }
    }
}
