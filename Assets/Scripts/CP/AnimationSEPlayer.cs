using Sekai.Live;
using UnityEngine;

namespace CP
{
    public class AnimationSEPlayer : MonoBehaviour
    {
        private LiveSoundPlayer liveSoundPlayer;

        public void Setup(LiveSoundPlayer soundPlayer)
        {
            liveSoundPlayer = soundPlayer;
        }

        public void PLaySE(string soundName)
        {
            if (string.IsNullOrEmpty(soundName))
            {
                return;
            }

            if (liveSoundPlayer == null)
            {
                liveSoundPlayer = GetComponentInParent<LiveSoundPlayer>();
            }

            liveSoundPlayer?.PlayIngameSEOneShot(soundName);
        }
    }
}
