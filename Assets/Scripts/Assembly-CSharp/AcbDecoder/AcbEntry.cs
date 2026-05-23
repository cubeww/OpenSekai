namespace AcbDecoder
{
    public enum AcbCodec
    {
        Unknown = 0,
        Hca,
        Adx,
        Riff,
    }

    public sealed class AcbEntry
    {
        internal AcbEntry(int index, int waveId, int offset, int size, AcbCodec codec, string name)
        {
            Index = index;
            WaveId = waveId;
            Offset = offset;
            Size = size;
            Codec = codec;
            Name = name;
        }

        /// <summary>
        /// 1-based AWB subsong index.
        /// </summary>
        public int Index { get; }

        public int WaveId { get; }

        public int Offset { get; }

        public int Size { get; }

        public AcbCodec Codec { get; }

        public string Name { get; }

        public override string ToString()
        {
            return $"{Index}: wave={WaveId}, codec={Codec}, size={Size}, name={Name}";
        }
    }
}
