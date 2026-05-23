namespace AcbDecoder
{
    public sealed class DecodeOptions
    {
        public static readonly DecodeOptions Default = new DecodeOptions();

        /// <summary>
        /// CRI HCA 56-bit key. Leave at zero for unencrypted HCA.
        /// </summary>
        public ulong HcaKey { get; set; }

        /// <summary>
        /// AWB subkey, usually read from the AWB header. Override only when needed.
        /// </summary>
        public ushort? HcaSubKey { get; set; }
    }
}
