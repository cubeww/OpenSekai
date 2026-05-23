using System;
using System.Collections.Generic;

namespace AcbDecoder
{
    internal sealed class AwbParser
    {
        private readonly byte[] _data;
        private readonly int _offset;
        private readonly int _size;

        public AwbParser(byte[] data, int offset, int size)
        {
            _data = data;
            _offset = offset;
            _size = size;
        }

        public ushort SubKey { get; private set; }

        public IReadOnlyList<AcbEntry> ReadEntries(IReadOnlyDictionary<int, string> namesByWaveId)
        {
            ReadOnlySpan<byte> awb = new ReadOnlySpan<byte>(_data, _offset, _size);
            if (!BinaryUtil.HasId(awb, 0, "AFS2"))
                throw new AcbException("ACB does not contain an AFS2/AWB memory bank.");

            int offsetSize = awb[0x05];
            int waveIdAlignment = BinaryUtil.U16LE(awb, 0x06);
            int total = BinaryUtil.I32LE(awb, 0x08);
            int offsetAlignment = BinaryUtil.U16LE(awb, 0x0C);
            SubKey = BinaryUtil.U16LE(awb, 0x0E);

            if (total <= 0)
                return Array.Empty<AcbEntry>();
            if (waveIdAlignment < 2)
                throw new AcbException($"Unsupported AWB wave id alignment {waveIdAlignment}.");
            if (offsetSize != 2 && offsetSize != 4)
                throw new AcbException($"Unsupported AWB offset size {offsetSize}.");
            if (offsetAlignment <= 0)
                offsetAlignment = 1;

            int idsOffset = 0x10;
            int offsetsOffset = idsOffset + total * waveIdAlignment;
            int offsetsBytes = (total + 1) * offsetSize;
            if (offsetsOffset < 0 || offsetsOffset + offsetsBytes > _size)
                throw new AcbException("AWB offset table is truncated.");

            var entries = new List<AcbEntry>(total);
            for (int i = 0; i < total; i++)
            {
                int waveId = BinaryUtil.U16LE(awb, idsOffset + i * waveIdAlignment);
                int rawOffset = ReadOffset(awb, offsetsOffset + i * offsetSize, offsetSize);
                int rawNext = ReadOffset(awb, offsetsOffset + (i + 1) * offsetSize, offsetSize);
                int subOffset = Align(rawOffset, offsetAlignment, _size);
                int subNext = Align(rawNext, offsetAlignment, _size);
                int subSize = subNext - subOffset;
                if (subOffset < 0 || subSize < 0 || subOffset > _size || _size - subOffset < subSize)
                    throw new AcbException($"Invalid AWB subfile range for index {i + 1}.");

                AcbCodec codec = DetectCodec(awb, subOffset, subSize);
                string name;
                if (!namesByWaveId.TryGetValue(waveId, out name) || string.IsNullOrEmpty(name))
                    name = $"wave_{waveId:D4}";

                entries.Add(new AcbEntry(i + 1, waveId, _offset + subOffset, subSize, codec, name));
            }

            return entries;
        }

        private static int ReadOffset(ReadOnlySpan<byte> data, int offset, int offsetSize)
        {
            return offsetSize == 2 ? BinaryUtil.U16LE(data, offset) : checked((int)BinaryUtil.U32LE(data, offset));
        }

        private static int Align(int value, int alignment, int fileSize)
        {
            int remainder = value % alignment;
            if (remainder != 0 && value < fileSize)
                value += alignment - remainder;
            return value;
        }

        private static AcbCodec DetectCodec(ReadOnlySpan<byte> data, int offset, int size)
        {
            if (size >= 4 && (BinaryUtil.U32BE(data, offset) & 0x7F7F7F7F) == 0x48434100)
                return AcbCodec.Hca;
            if (size >= 2 && BinaryUtil.U16BE(data, offset) == 0x8000)
                return AcbCodec.Adx;
            if (size >= 4 && BinaryUtil.HasId(data, offset, "RIFF"))
                return AcbCodec.Riff;
            return AcbCodec.Unknown;
        }
    }
}
