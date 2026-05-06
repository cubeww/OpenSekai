using Sekai.Core.Live;
using UnityEngine;

namespace Sekai
{
    public class ComboView : MonoBehaviour
    {
        private const float ApEffectSpeed = 4.1887903f;

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

        private SpriteRenderer[] numberSpriteRenderers;
        private SpriteRenderer[] effectNumberSpriteRenderers;
        private SpriteRenderer[] allPerfectEffectRenderers;
        private float time;
        private int currentCombo;
        private bool isAllPerfect;
        private bool useAllPerfectEffect;

        public SpriteRenderer[] NumberSpriteRenderers
        {
            get
            {
                if (numberSpriteRenderers == null)
                {
                    numberSpriteRenderers = numberRoot != null
                        ? numberRoot.GetComponentsInChildren<SpriteRenderer>(true)
                        : new SpriteRenderer[0];
                }
                return numberSpriteRenderers;
            }
        }

        public SpriteRenderer[] EffectNumberSpriteRenderers
        {
            get
            {
                if (effectNumberSpriteRenderers == null)
                {
                    effectNumberSpriteRenderers = effectNumberRoot != null
                        ? effectNumberRoot.GetComponentsInChildren<SpriteRenderer>(true)
                        : new SpriteRenderer[0];
                }
                return effectNumberSpriteRenderers;
            }
        }

        public void Setup(bool useAllPerfectEffect)
        {
            this.useAllPerfectEffect = useAllPerfectEffect;
            numberSpriteRenderers = numberRoot != null
                ? numberRoot.GetComponentsInChildren<SpriteRenderer>(true)
                : new SpriteRenderer[0];
            effectNumberSpriteRenderers = effectNumberRoot != null
                ? effectNumberRoot.GetComponentsInChildren<SpriteRenderer>(true)
                : new SpriteRenderer[0];
            allPerfectEffectRenderers = allPerfectEffectRoot != null
                ? allPerfectEffectRoot.GetComponentsInChildren<SpriteRenderer>(true)
                : new SpriteRenderer[0];
            Clear();
        }

        public void Clear()
        {
            transform.localScale = Vector3.zero;
            if (numberRoot != null)
            {
                numberRoot.localScale = Vector3.zero;
            }
            if (effectNumberRoot != null)
            {
                effectNumberRoot.localScale = Vector3.zero;
            }
            if (allPerfectEffectRoot != null)
            {
                allPerfectEffectRoot.localScale = Vector3.zero;
            }
            currentCombo = 0;
            isAllPerfect = false;
        }

        public void Excute(LiveScore score)
        {
            Excute(score.combo, score.IsAllPerfect);
        }

        public void Excute(int combo, bool isApScore)
        {
            int value = combo;
            if (value == currentCombo)
            {
                return;
            }

            currentCombo = value;
            time = 0f;
            transform.localScale = value <= 0 ? Vector3.zero : Vector3.one;

            Sprite[] sprites = numberSprites;
            Sprite comboSprite = comboTextSprite;
            isAllPerfect = useAllPerfectEffect && isApScore;
            if (isAllPerfect)
            {
                sprites = allPerfectNumberSprites != null && allPerfectNumberSprites.Length > 0
                    ? allPerfectNumberSprites
                    : numberSprites;
                comboSprite = allPerfectComboTextSprite != null ? allPerfectComboTextSprite : comboTextSprite;
            }

            if (allPerfectEffectRoot != null)
            {
                allPerfectEffectRoot.gameObject.SetActive(isAllPerfect);
            }
            if (apEffectComboRenderer != null)
            {
                apEffectComboRenderer.gameObject.SetActive(isAllPerfect);
            }
            if (comboSpriteRenderer != null)
            {
                comboSpriteRenderer.sprite = comboSprite;
            }

            int digitCount = value < 1 ? 1 : Mathf.FloorToInt(Mathf.Log10(value)) + 1;
            int workValue = Mathf.Max(0, value);
            for (int i = 0; i < NumberSpriteRenderers.Length; i++)
            {
                Vector3 scale = digitCount <= i ? Vector3.zero : Vector3.one;
                float x = (digitCount - 1) * 0.475f + i * -0.95f;
                Vector3 position = Vector3.right * x;

                int digit = workValue - workValue / 10 * 10;
                workValue /= 10;
                Sprite numberSprite = GetSprite(sprites, digit);

                NumberSpriteRenderers[i].transform.localScale = scale;
                NumberSpriteRenderers[i].transform.localPosition = position;
                NumberSpriteRenderers[i].sprite = numberSprite;

                if (i < EffectNumberSpriteRenderers.Length)
                {
                    EffectNumberSpriteRenderers[i].transform.localScale = scale;
                    EffectNumberSpriteRenderers[i].transform.localPosition = position;
                    EffectNumberSpriteRenderers[i].sprite = numberSprite;
                }

                if (allPerfectEffectRenderers != null && i < allPerfectEffectRenderers.Length)
                {
                    allPerfectEffectRenderers[i].transform.localScale = scale;
                    allPerfectEffectRenderers[i].transform.localPosition = position;
                    allPerfectEffectRenderers[i].sprite = GetSprite(allPerfectEffectSprites, digit);
                }
            }
        }

        private void Update()
        {
            if (transform.localScale == Vector3.zero)
            {
                return;
            }

            float numberRate = Mathf.Clamp01(time / 0.1f);
            float numberScale = numberRate * 0.4f + 0.6f;
            if (numberRoot != null)
            {
                numberRoot.localScale = Vector3.one * numberScale;
            }

            float effectRate = Mathf.Clamp01((time - 0.1f) / 0.1f);
            float effectScale = time >= 0.1f ? effectRate * 0.4f + 1f : 0f;
            if (effectNumberRoot != null)
            {
                effectNumberRoot.localScale = Vector3.one * effectScale;
            }

            Color effectColor = new Color(1f, 1f, 1f, time >= 0.1f ? (1f - effectRate) * 0.6f : 0f);
            for (int i = 0; i < EffectNumberSpriteRenderers.Length; i++)
            {
                if (EffectNumberSpriteRenderers[i] != null)
                {
                    EffectNumberSpriteRenderers[i].color = effectColor;
                }
            }

            if (isAllPerfect)
            {
                if (allPerfectEffectRoot != null)
                {
                    allPerfectEffectRoot.localScale = Vector3.one * numberScale;
                }
                Color apColor = new Color(1f, 1f, 1f, (Mathf.Sin(Time.time * ApEffectSpeed) + 1f) * 0.5f);
                if (allPerfectEffectRenderers != null)
                {
                    for (int i = 0; i < allPerfectEffectRenderers.Length; i++)
                    {
                        if (allPerfectEffectRenderers[i] != null)
                        {
                            allPerfectEffectRenderers[i].color = apColor;
                        }
                    }
                }
                if (apEffectComboRenderer != null)
                {
                    apEffectComboRenderer.color = apColor;
                }
            }

            time += Time.deltaTime;
        }

        private static Sprite GetSprite(Sprite[] sprites, int index)
        {
            if (sprites == null || sprites.Length == 0)
            {
                return null;
            }

            return sprites[Mathf.Clamp(index, 0, sprites.Length - 1)];
        }
    }
}
