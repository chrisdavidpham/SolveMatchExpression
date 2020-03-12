using System;

namespace LongExtensions
{
    public static class LongExtensions
    {
        // Largest power of 10 less that Int64.MaxValue (10^18)
        public static readonly long MAX_MAGNITUDE = 1000000000000000000L;

        public static long Abs(this long x)
        {
            // Fast absolute value
            return (x + (x >> 63)) ^ (x >> 63);
        }

        public static long Append(this long? x, char c)
        {
            return checked((x * 10 ?? 0) + (long)Char.GetNumericValue(c));
        }
    }
}
