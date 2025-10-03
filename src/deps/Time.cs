using System;

namespace csharp.deps
{
    public static class Time
    {
        private static readonly double Nano = 1e9;
        private static readonly double Micro = 1e6;
        private static readonly double Milli = 1e3;
        private static readonly double Minutes = 1 / 60;
        private static readonly double Hours = 1 / 3600;
        private static readonly double Days = 1 / 86400;

        public static double GetTime(string format = "seconds")
        {
            double c = DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
            switch (format)
            {
                case "nano": return c * Nano;
                case "micro": return c * Micro;
                case "milli": return c * Milli;
                case "seconds": return c;
                case "minutes": return c * Minutes;
                case "hours": return c * Hours;
                case "days": return c * Days;
                default: return c;
            }
        }

        public static string FormatTime(double s)
        {
            if (s < 1e-6) return $"{s * 1e9:F3} ns";
            if (s < 1e-3) return $"{s * 1e6:F3} μs";
            if (s < 1) return $"{s * 1e3:F3} ms";
            if (s < 60) return $"{s:F3} s";
            if (s < 3600) return $"{s / 60:F3} min";
            return $"{s / 3600:F3} h";
        }
    }
}
