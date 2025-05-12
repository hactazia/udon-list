using System;

namespace Hactazia.Types
{
    public static class Convert
    {
        public static byte[] FromBase128ToBase256(byte[] base128)
        {
            if (base128 == null || base128.Length == 0) return null;

            var base256 = new byte[base128.Length * 2];
            var index = 0;

            for (var i = 0; i < base128.Length; i++)
            {
                base256[index++] = (byte)(base128[i] & 0x7F);
                if (i < base128.Length - 1)
                    base256[index++] = (byte)((base128[i] >> 7) & 0x7F);
            }

            return base256;
        }
    }
}