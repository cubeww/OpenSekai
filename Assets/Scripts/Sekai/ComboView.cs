using UnityEngine;

namespace Sekai
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField] private Transform numberRoot;
        [SerializeField] private Transform effectNumberRoot;
        [SerializeField] private Transform allPerfectEffectRoot;
        [SerializeField] private Sprite[] numberSprites;
        [SerializeField] private Sprite[] allPerfectNumberSprites;
        [SerializeField] private Sprite[] allPerfectEffectSprites;
        [SerializeField] private SpriteRenderer comboSpriteRenderer;
        [SerializeField] private SpriteRenderer apEffectComboRenderer;
        [SerializeField] private Sprite comboTextSprite;
        [SerializeField] private Sprite allPerfectComboTextSprite;

        public void Setup(bool useAllPerfectEffect)
        {
            Clear();
        }

        public void Clear()
        {
            transform.localScale = Vector3.zero;
        }
    }
}
