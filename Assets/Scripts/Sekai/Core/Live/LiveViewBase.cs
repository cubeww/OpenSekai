using UnityEngine;

namespace Sekai.Core.Live
{
    public abstract class LiveViewBase : MonoBehaviour
    {
        public virtual void Setup(BaseLiveController baseController)
        {
        }

        public virtual void OnLoad()
        {
        }

        public virtual void OnUnload()
        {
        }

        public virtual void MusicStart(float time)
        {
        }

        public virtual void RhythmGameStart()
        {
        }

        public virtual void Pause(float time)
        {
        }

        public virtual void Resume(float time)
        {
        }

        public virtual void Retry()
        {
        }

        public virtual void OnFailure(float time)
        {
        }

        public virtual void Finish()
        {
        }
    }
}
