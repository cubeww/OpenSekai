using System;
using System.Buffers.Binary;
using System.Text;

namespace AcbDecoder
{
    internal static class BinaryUtil
    {
        public static uint U32BE(ReadOnlySpan<byte> data, int offset)
        {
            return BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset, 4));
        }

        public static uint U32LE(ReadOnlySpan<byte> data, int offset)
        {
            return BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(offset, 4));
        }

        public static int I32BE(ReadOnlySpan<byte> data, int offset)
        {
            return BinaryPrimitives.ReadInt32BigEndian(data.Slice(offset, 4));
        }

        public static int I32LE(ReadOnlySpan<byte> data, int offset)
        {
            return BinaryPrimitives.ReadInt32LittleEndian(data.Slice(offset, 4));
        }

        public static ushort U16BE(ReadOnlySpan<byte> data, int offset)
        {
            return BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
        }

        public static ushort U16LE(ReadOnlySpan<byte> data, int offset)
        {
            return BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(offset, 2));
        }

        public static ulong U64BE(ReadOnlySpan<byte> data, int offset)
        {
            return BinaryPrimitives.ReadUInt64BigEndian(data.Slice(offset, 8));
        }

        public static string NullTerminatedAscii(ReadOnlySpan<byte> data)
        {
            int length = data.IndexOf((byte)0);
            if (length < 0)
                length = data.Length;
            return Encoding.ASCII.GetString(data.Slice(0, length));
        }

        public static bool HasId(ReadOnlySpan<byte> data, int offset, string id)
        {
            if ((uint)offset > (uint)data.Length || data.Length - offset < id.Length)
                return false;

            for (int i = 0; i < id.Length; i++)
            {
                if (data[offset + i] != (byte)id[i])
                    return false;
            }

            return true;
        }

        public static float FloatFromBits(uint bits)
        {
            byte[] bytes = BitConverter.GetBytes(bits);
            return BitConverter.ToSingle(bytes, 0);
        }
    }
}
