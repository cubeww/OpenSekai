using UnityEngine;

namespace Sekai.Live
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer life;
        [SerializeField] private SpriteRenderer score;

        public void Setup()
        {
            if (life != null) life.enabled = false;
            if (score != null) score.enabled = false;
        }

        public void Load() { }
        public void Unload() { }
        public void Clear() { }
    }
}
