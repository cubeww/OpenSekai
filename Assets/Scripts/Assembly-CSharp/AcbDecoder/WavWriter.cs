using System;
using System.IO;
using System.Text;

namespace AcbDecoder
{
    public static class WavWriter
    {
        public static void WritePcm16(string path, AudioData audio)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (audio == null)
                throw new ArgumentNullException(nameof(audio));

            using (var stream = File.Create(path))
            using (var writer = new BinaryWriter(stream, Encoding.ASCII))
            {
                int dataBytes = checked(audio.Samples.Length * 2);
                writer.Write(Encoding.ASCII.GetBytes("RIFF"));
                writer.Write(36 + dataBytes);
                writer.Write(Encoding.ASCII.GetBytes("WAVE"));

                writer.Write(Encoding.ASCII.GetBytes("fmt "));
                writer.Write(16);
                writer.Write((short)1);
                writer.Write((short)audio.Channels);
                writer.Write(audio.SampleRate);
                writer.Write(audio.SampleRate * audio.Channels * 2);
                writer.Write((short)(audio.Channels * 2));
                writer.Write((short)16);

                writer.Write(Encoding.ASCII.GetBytes("data"));
                writer.Write(dataBytes);

                for (int i = 0; i < audio.Samples.Length; i++)
                {
                    float sample = audio.Samples[i];
                    if (sample > 1.0f)
                        sample = 1.0f;
                    else if (sample < -1.0f)
                        sample = -1.0f;

                    int pcm = sample < 0 ? (int)(sample * 32768.0f) : (int)(sample * 32767.0f);
                    writer.Write((short)pcm);
                }
            }
        }
    }
}
