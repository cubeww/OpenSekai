using System;

namespace AcbDecoder
{
    internal ref struct BitReader
    {
        private ReadOnlySpan<byte> _data;
        private int _bit;
        private int _sizeBits;

        public BitReader(ReadOnlySpan<byte> data)
        {
            _data = data;
            _bit = 0;
            _sizeBits = data.Length * 8;
        }

        public int BitPosition => _bit;

        public uint Read(int bits)
        {
            uint value = Peek(bits);
            _bit += bits;
            return value;
        }

        public void Skip(int bits)
        {
            _bit += bits;
        }

        public uint Peek(int bits)
        {
            if (bits <= 0)
                return 0;
            if (_bit + bits > _sizeBits)
                return 0;

            int bitPos = _bit;
            int bitRem = bitPos & 7;
            int bitsLeft = _sizeBits - bitPos;
            int bitOffset = bits + bitRem;
            int bytePos = bitPos >> 3;

            if (bitsLeft >= 32 && bitOffset >= 25)
            {
                uint v = ((uint)_data[bytePos] << 24) |
                         ((uint)_data[bytePos + 1] << 16) |
                         ((uint)_data[bytePos + 2] << 8) |
                         _data[bytePos + 3];
                uint mask = bitRem == 0 ? 0xFFFFFFFFu : 0xFFFFFFFFu >> bitRem;
                v &= mask;
                return v >> (32 - bitRem - bits);
            }

            if (bitsLeft >= 24 && bitOffset >= 17)
            {
                uint v = ((uint)_data[bytePos] << 16) |
                         ((uint)_data[bytePos + 1] << 8) |
                         _data[bytePos + 2];
                uint mask = bitRem == 0 ? 0xFFFFFFu : 0xFFFFFFu >> bitRem;
                v &= mask;
                return v >> (24 - bitRem - bits);
            }

            if (bitsLeft >= 16 && bitOffset >= 9)
            {
                uint v = ((uint)_data[bytePos] << 8) | _data[bytePos + 1];
                uint mask = bitRem == 0 ? 0xFFFFu : 0xFFFFu >> bitRem;
                v &= mask;
                return v >> (16 - bitRem - bits);
            }

            {
                uint v = _data[bytePos];
                uint mask = bitRem == 0 ? 0xFFu : 0xFFu >> bitRem;
                v &= mask;
                return v >> (8 - bitRem - bits);
            }
        }
    }
}
