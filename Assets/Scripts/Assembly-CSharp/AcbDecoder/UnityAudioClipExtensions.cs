#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace AcbDecoder
{
    public static class UnityAudioClipExtensions
    {
        public static AudioClip ToAudioClip(this AudioData audio, string name = "ACB Audio", bool stream = false)
        {
            AudioClip clip = AudioClip.Create(name, audio.SampleCount, audio.Channels, audio.SampleRate, stream);
            clip.SetData(audio.Samples, 0);
            return clip;
        }
    }
}
#endif
