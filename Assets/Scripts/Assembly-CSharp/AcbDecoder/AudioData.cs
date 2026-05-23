namespace AcbDecoder
{
    public sealed class AudioData
    {
        public AudioData(float[] samples, int sampleRate, int channels, int loopStartSample = 0, int loopEndSample = 0)
        {
            Samples = samples;
            SampleRate = sampleRate;
            Channels = channels;
            LoopStartSample = loopStartSample;
            LoopEndSample = loopEndSample;
        }

        public float[] Samples { get; }

        public int SampleRate { get; }

        public int Channels { get; }

        public int SampleCount => Channels == 0 ? 0 : Samples.Length / Channels;

        public bool HasLoop => LoopEndSample > LoopStartSample;

        public int LoopStartSample { get; }

        public int LoopEndSample { get; }
    }
}
