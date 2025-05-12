using System;

namespace Hactazia.Types
{
    public static class FlagExtensions
    {
        public static bool HasFlag<T>(this T value, T flag) where T : Enum
        {
            var valueAsInt = System.Convert.ToUInt64(value);
            var flagAsInt = System.Convert.ToUInt64(flag);
            return (valueAsInt & flagAsInt) == flagAsInt;
        }

        public static bool HasFlag(this byte value, byte flag)
        {
            return (value & flag) == flag;
        }

        public static bool HasFlag(this ushort value, ushort flag)
        {
            return (value & flag) == flag;
        }

        public static bool HasFlag(this uint value, uint flag)
        {
            return (value & flag) == flag;
        }

        public static bool HasFlag(this ulong value, ulong flag)
        {
            return (value & flag) == flag;
        }
    }
}