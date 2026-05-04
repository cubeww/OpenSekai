using UnityEngine;

namespace Sekai
{
    public class JudgmentView : MonoBehaviour
    {
        [SerializeField] private Sprite miss;
        [SerializeField] private Sprite bad;
        [SerializeField] private Sprite good;
        [SerializeField] private Sprite great;
        [SerializeField] private Sprite perfect;
        [SerializeField] private Sprite justPerfect;
        [SerializeField] private Sprite auto;
        [SerializeField] private SpriteRenderer spriteRenderer;
    }
}
