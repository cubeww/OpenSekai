using UnityEngine;

namespace Sekai.Live
{
    public class LiveResultView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background;

        public void Execute(LiveResultAnimationType liveResultAnimationType)
        {
            if (background != null)
            {
                background.gameObject.SetActive(true);
            }
        }

        public void Unload() { }
    }
}
