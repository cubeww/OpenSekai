using UnityEngine;

namespace Sekai.Core.Live
{
    public sealed class AudioSourceSyncedUnityTimer
    {
        private const int MaximumDiffMs = 62;
        private const int StartTimeForUnityBasedUpdateMs = 100;
        private const float MillisecondsPerSecond = 1000f;

        private readonly AudioSource audioSource;
        private int playbackTimeMs = -1;
        private int referenceAudioTimeMs = -1;
        private float referenceUnityTime = -1f;
        private bool isWaitingForAudioTimeUpdate;

        public int PlaybackTime
        {
            get { return playbackTimeMs; }
        }

        public AudioSourceSyncedUnityTimer(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        public void Reset(int playbackTimeMs, float unityTime)
        {
            this.playbackTimeMs = playbackTimeMs;
            referenceAudioTimeMs = playbackTimeMs >= StartTimeForUnityBasedUpdateMs ? playbackTimeMs : -1;
            referenceUnityTime = playbackTimeMs >= StartTimeForUnityBasedUpdateMs ? unityTime : -1f;
            isWaitingForAudioTimeUpdate = false;
        }

        public void Execute(float unityTime)
        {
            int audioTimeMs = GetAudioTimeMs();
            if (audioTimeMs <= StartTimeForUnityBasedUpdateMs - 1)
            {
                playbackTimeMs = audioTimeMs;
                return;
            }

            if (referenceAudioTimeMs < 0)
            {
                referenceAudioTimeMs = audioTimeMs;
                referenceUnityTime = unityTime;
            }

            if (isWaitingForAudioTimeUpdate)
            {
                int previousPlaybackTimeMs = playbackTimeMs;
                referenceAudioTimeMs = audioTimeMs;
                referenceUnityTime = unityTime;
                if (audioTimeMs > previousPlaybackTimeMs)
                {
                    playbackTimeMs = audioTimeMs;
                    isWaitingForAudioTimeUpdate = false;
                }
                return;
            }

            int unityBasedTimeMs = referenceAudioTimeMs + (int)((unityTime - referenceUnityTime) * MillisecondsPerSecond);
            if (unityBasedTimeMs - audioTimeMs > MaximumDiffMs)
            {
                playbackTimeMs = audioTimeMs + MaximumDiffMs;
                isWaitingForAudioTimeUpdate = true;
            }
            else if (audioTimeMs - unityBasedTimeMs > MaximumDiffMs)
            {
                playbackTimeMs = audioTimeMs;
                referenceAudioTimeMs = audioTimeMs;
                referenceUnityTime = unityTime;
            }
            else
            {
                playbackTimeMs = unityBasedTimeMs;
            }
        }

        private int GetAudioTimeMs()
        {
            if (audioSource == null || audioSource.clip == null)
            {
                return playbackTimeMs;
            }

            int sampleTimeMs = audioSource.timeSamples > 0
                ? Mathf.FloorToInt((float)audioSource.timeSamples / audioSource.clip.frequency * MillisecondsPerSecond)
                : Mathf.FloorToInt(audioSource.time * MillisecondsPerSecond);
            return Mathf.Max(sampleTimeMs, 0);
        }
    }
}
