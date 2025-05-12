#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Hactazia.Types.Tests
{
    public class BufferTests
    {
        /*[InitializeOnLoadMethod]
        public static void RunTests()
        {
            var buffer = BufferExtensions.NewBuffer(255);
            ushort index = 0;
            buffer = buffer.WriteBuffer(index, (byte)1);
            index += BufferExtensions.GetLengthOf<byte>();
            buffer = buffer.WriteBuffer(index, (ushort)2);
            index += BufferExtensions.GetLengthOf<ushort>();
            buffer = buffer.WriteBuffer(index, (byte)3);
            index += BufferExtensions.GetLengthOf<byte>();
            buffer = buffer.WriteBuffer(index, 1);
            index += BufferExtensions.GetLengthOf<byte>();
            buffer = buffer.WriteBuffer(index, 15.858752f);
            index += BufferExtensions.GetLengthOf<float>();
            buffer = buffer.WriteBuffer(index, Vector3.one);
            index += BufferExtensions.GetLengthOf<Vector3>();
            buffer = buffer.WriteBuffer(index, "abcdefgh");
            index += BufferExtensions.GetLengthOf("abcdefgh");
            Debug.Log(buffer.ToBufferString());
        }*/
    }
}
#endif