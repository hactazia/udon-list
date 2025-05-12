using System;
using UnityEngine;
using VRC.SDK3.Data;

namespace Hactazia.Types
{
    public static class BufferExtensions
    {
        public static byte[] NewBuffer(ushort maxLength)
        {
            return ListExtensions.New<byte>(maxLength);
        }

        public static byte[] NewBuffer()
        {
            return ListExtensions.New<byte>();
        }

        private static byte[] ResizeBuffer(this byte[] buffer, ushort maxLength)
        {
            if (buffer == null || buffer.Length == 0)
                return NewBuffer(maxLength);
            if (buffer.Length >= maxLength) return buffer;
            var newBuffer = NewBuffer(maxLength);
            Array.Copy(buffer, newBuffer, buffer.Length);
            return newBuffer;
        }

        private static byte[] ResizeBuffer(this byte[] buffer, ushort sizeValue, ushort index)
        {
            return buffer.ResizeBuffer((ushort)(index + sizeValue));
        }

        public static ushort SizeOf<T>()
        {
            if (typeof(T) == typeof(byte)) return 1;
            if (typeof(T) == typeof(short)) return 2;
            if (typeof(T) == typeof(ushort)) return SizeOf<short>();
            if (typeof(T) == typeof(int)) return 4;
            if (typeof(T) == typeof(uint)) return SizeOf<int>();
            if (typeof(T) == typeof(long)) return 8;
            if (typeof(T) == typeof(ulong)) return SizeOf<long>();
            if (typeof(T) == typeof(float)) return 4;
            if (typeof(T) == typeof(double)) return 8;
            if (typeof(T) == typeof(Vector3)) return (ushort)(SizeOf<float>() * 3);
            if (typeof(T) == typeof(Quaternion)) return (ushort)(SizeOf<float>() * 4);
            if (typeof(T) == typeof(string)) return SizeOf<ushort>();
            if (typeof(T) == typeof(DateTimeOffset)) return SizeOf<long>();
            if (typeof(T) == typeof(byte[])) return 0;
            Debug.Log($"Error to get length of {typeof(T)}");
            return 0;
        }

        public static ushort SizeOf(string value)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            return (ushort)(SizeOf<string>() + bytes.Length);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, byte value)
        {
            buffer = buffer.ResizeBuffer(1, index);
            buffer[index] = value;
            return buffer;
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, short value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return buffer.WriteBuffer(index, bytes);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return buffer.WriteBuffer(index, bytes);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, int value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return buffer.WriteBuffer(index, bytes);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return buffer.WriteBuffer(index, bytes);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, long value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return buffer.WriteBuffer(index, bytes);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return buffer.WriteBuffer(index, bytes);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, float value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return buffer.WriteBuffer(index, bytes);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, double value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return buffer.WriteBuffer(index, bytes);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, Vector3 value)
        {
            buffer = buffer.WriteBuffer(index, value.x);
            buffer = buffer.WriteBuffer((ushort)(index + 4), value.y);
            return buffer.WriteBuffer((ushort)(index + 8), value.z);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, Quaternion value)
        {
            buffer = buffer.WriteBuffer(index, value.x);
            buffer = buffer.WriteBuffer((ushort)(index + 4), value.y);
            buffer = buffer.WriteBuffer((ushort)(index + 8), value.z);
            return buffer.WriteBuffer((ushort)(index + 12), value.w);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, string value)
        {
            if (value == null)
                return buffer.WriteBuffer(index, (ushort)0);
            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            buffer = buffer.WriteBuffer(index, (ushort)bytes.Length);
            return buffer.WriteBuffer((ushort)(index + 2), bytes);
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, DateTimeOffset value)
        {
            return buffer.WriteBuffer(index, value.ToUnixTimeMilliseconds());
        }

        public static byte[] WriteBuffer(this byte[] buffer, ushort index, byte[] value)
        {
            buffer = buffer.ResizeBuffer((ushort)(index + value.Length));
            for (var i = 0; i < value.Length; i++)
                buffer[index + i] = value[i];
            return buffer;
        }

        public static string ToBufferString(this byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                return string.Empty;
            var str = "Buffer[(" + buffer.Length + ")";
            foreach (var t in buffer)
                str += " " + t.ToString("X2");
            return str + "]";
        }

        public static byte ReadBuffer(this byte[] buffer, ushort index)
        {
            return buffer[index];
        }

        public static short ReadBufferShort(this byte[] buffer, ushort index)
        {
            // read 2 bytes in big endian
            return (short)(
                (buffer[index + 1] << 8)
                | buffer[index]
            );
        }

        public static ushort ReadBufferUShort(this byte[] buffer, ushort index)
        {
            // read 2 bytes in big endian
            return (ushort)(
                (buffer[index + 1] << 8)
                | buffer[index]
            );
        }

        public static int ReadBufferInt(this byte[] buffer, ushort index)
        {
            return (int)(
                (buffer[index + 3] << 24)
                | (buffer[index + 2] << 16)
                | (buffer[index + 1] << 8)
                | buffer[index]
            );
        }

        public static uint ReadBufferUInt(this byte[] buffer, ushort index)
        {
            return (uint)(
                (buffer[index + 3] << 24)
                | (buffer[index + 2] << 16)
                | (buffer[index + 1] << 8)
                | buffer[index]
            );
        }

        public static long ReadBufferLong(this byte[] buffer, ushort index)
        {
            return (long)(
                ((ulong)buffer[index + 7] << 56)
                | ((ulong)buffer[index + 6] << 48)
                | ((ulong)buffer[index + 5] << 40)
                | ((ulong)buffer[index + 4] << 32)
                | ((ulong)buffer[index + 3] << 24)
                | ((ulong)buffer[index + 2] << 16)
                | ((ulong)buffer[index + 1] << 8)
                | buffer[index]
            );
        }

        public static ulong ReadBufferULong(this byte[] buffer, ushort index)
        {
            return (ulong)(
                ((ulong)buffer[index + 7] << 56)
                | ((ulong)buffer[index + 6] << 48)
                | ((ulong)buffer[index + 5] << 40)
                | ((ulong)buffer[index + 4] << 32)
                | ((ulong)buffer[index + 3] << 24)
                | ((ulong)buffer[index + 2] << 16)
                | ((ulong)buffer[index + 1] << 8)
                | buffer[index]
            );
        }

        public static float ReadBufferFloat(this byte[] buffer, ushort index)
        {
            var bytes = new byte[4];
            for (var i = 0; i < 4; i++)
                bytes[i] = buffer[index + i];
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return BitConverter.ToSingle(bytes, 0);
        }

        public static double ReadBufferDouble(this byte[] buffer, ushort index)
        {
            var bytes = new byte[8];
            for (var i = 0; i < 8; i++)
                bytes[i] = buffer[index + i];
            if (!BitConverter.IsLittleEndian) bytes.Reverse();
            return BitConverter.ToDouble(bytes, 0);
        }

        public static Vector3 ReadBufferVector3(this byte[] buffer, ushort index)
        {
            var x = buffer.ReadBufferFloat(index);
            var y = buffer.ReadBufferFloat((ushort)(index + 4));
            var z = buffer.ReadBufferFloat((ushort)(index + 8));
            return new Vector3(x, y, z);
        }

        public static Quaternion ReadBufferQuaternion(this byte[] buffer, ushort index)
        {
            var x = buffer.ReadBufferFloat(index);
            var y = buffer.ReadBufferFloat((ushort)(index + 4));
            var z = buffer.ReadBufferFloat((ushort)(index + 8));
            var w = buffer.ReadBufferFloat((ushort)(index + 12));
            return new Quaternion(x, y, z, w);
        }

        public static string ReadBufferString(this byte[] buffer, ushort index)
        {
            var length = buffer.ReadBufferUShort(index);
            if (length == 0) return string.Empty;
            var bytes = new byte[length];
            for (var i = 0; i < length; i++)
                bytes[i] = buffer[index + 2 + i];
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static DateTimeOffset ReadBufferDateTime(this byte[] buffer, ushort index)
        {
            var unixTime = buffer.ReadBufferLong(index);
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
        }

        public static byte[] ReadBufferBytes(this byte[] buffer, ushort index, ushort length)
        {
            var bytes = new byte[length];
            for (var i = 0; i < length; i++)
                bytes[i] = buffer[index + i];
            return bytes;
        }

        public static byte[] ReadBuffer(this byte[] buffer, ushort index, ushort length)
        {
            var bytes = new byte[length];
            for (var i = 0; i < length; i++)
                bytes[i] = buffer[index + i];
            return bytes;
        }

        public static T ReadBuffer<T>(this byte[] buffer, ushort index)
        {
            if (typeof(T) == typeof(byte)) return (T)(object)buffer.ReadBuffer(index);
            if (typeof(T) == typeof(short)) return (T)(object)buffer.ReadBufferShort(index);
            if (typeof(T) == typeof(ushort)) return (T)(object)buffer.ReadBufferUShort(index);
            if (typeof(T) == typeof(int)) return (T)(object)buffer.ReadBufferInt(index);
            if (typeof(T) == typeof(uint)) return (T)(object)buffer.ReadBufferUInt(index);
            if (typeof(T) == typeof(long)) return (T)(object)buffer.ReadBufferLong(index);
            if (typeof(T) == typeof(ulong)) return (T)(object)buffer.ReadBufferULong(index);
            if (typeof(T) == typeof(float)) return (T)(object)buffer.ReadBufferFloat(index);
            if (typeof(T) == typeof(double)) return (T)(object)buffer.ReadBufferDouble(index);
            if (typeof(T) == typeof(Vector3)) return (T)(object)buffer.ReadBufferVector3(index);
            if (typeof(T) == typeof(Quaternion)) return (T)(object)buffer.ReadBufferQuaternion(index);
            if (typeof(T) == typeof(string)) return (T)(object)buffer.ReadBufferString(index);
            if (typeof(T) == typeof(DateTimeOffset)) return (T)(object)buffer.ReadBufferDateTime(index);
            Debug.Log($"Error to read buffer {typeof(T)}");
            return default;
        }

        public static int Sizeof(object p0)
        {
            if (p0 is byte) return SizeOf<byte>();
            if (p0 is short) return SizeOf<short>();
            if (p0 is ushort) return SizeOf<ushort>();
            if (p0 is int) return SizeOf<int>();
            if (p0 is uint) return SizeOf<uint>();
            if (p0 is long) return SizeOf<long>();
            if (p0 is ulong) return SizeOf<ulong>();
            if (p0 is float) return SizeOf<float>();
            if (p0 is double) return SizeOf<double>();
            if (p0 is Vector3) return SizeOf<Vector3>();
            if (p0 is Quaternion) return SizeOf<Quaternion>();
            if (p0 is string) return SizeOf<string>();
            Debug.Log($"Error to get size of {p0.GetType()}");
            return 0;
        }
    }
}