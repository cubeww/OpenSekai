using Sekai.Core.Live;
using UnityEngine;

namespace Sekai
{
	public class ComboView : MonoBehaviour
	{
		private const float AP_EFFECT_SPEED = 4.1887903f;

		[SerializeField]
		private Transform numberRoot;

		[SerializeField]
		private Transform effectNumberRoot;

		[SerializeField]
		private Transform allPerfectEffectRoot;

		[SerializeField]
		private Sprite[] numberSprites;

		[SerializeField]
		private Sprite[] allPerfectNumberSprites;

		[SerializeField]
		private Sprite[] allPerfectEffectSprites;

		[SerializeField]
		private SpriteRenderer comboSpriteRenderer;

		[SerializeField]
		private SpriteRenderer apEffectComboRenderer;

		[SerializeField]
		private Sprite comboTextSprite;

		[SerializeField]
		private Sprite allPerfectComboTextSprite;

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
				if (numberSpriteRenderers == null && numberRoot != null)
				{
					numberSpriteRenderers = numberRoot.GetComponentsInChildren<SpriteRenderer>(true);
				}
				return numberSpriteRenderers;
			}
		}

		public SpriteRenderer[] EffectNumberSpriteRenderers
		{
			get
			{
				if (effectNumberSpriteRenderers == null && effectNumberRoot != null)
				{
					effectNumberSpriteRenderers = effectNumberRoot.GetComponentsInChildren<SpriteRenderer>(true);
				}
				return effectNumberSpriteRenderers;
			}
		}

		public void Setup(bool useAllPerfectEffect)
		{
			this.useAllPerfectEffect = useAllPerfectEffect;
			numberSpriteRenderers = numberRoot != null ? numberRoot.GetComponentsInChildren<SpriteRenderer>(true) : new SpriteRenderer[0];
			effectNumberSpriteRenderers = effectNumberRoot != null ? effectNumberRoot.GetComponentsInChildren<SpriteRenderer>(true) : new SpriteRenderer[0];
			allPerfectEffectRenderers = allPerfectEffectRoot != null ? allPerfectEffectRoot.GetComponentsInChildren<SpriteRenderer>(true) : new SpriteRenderer[0];
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
			time = 0f;
		}

		public void Excute(LiveScore score)
		{
			Excute(score.combo, score.IsAllPerfect);
		}

		public void Excute(int combo, bool isApScore)
		{
			if (currentCombo == combo)
			{
				return;
			}
			time = 0f;
			currentCombo = combo;
			transform.localScale = combo > 0 ? Vector3.one : Vector3.zero;

			Sprite[] digitSprites = numberSprites;
			Sprite textSprite = comboTextSprite;
			isAllPerfect = isApScore && useAllPerfectEffect;
			if (isAllPerfect)
			{
				digitSprites = allPerfectNumberSprites;
				textSprite = allPerfectComboTextSprite;
			}
			if (comboSpriteRenderer != null)
			{
				comboSpriteRenderer.sprite = textSprite;
			}
			if (allPerfectEffectRoot != null)
			{
				allPerfectEffectRoot.gameObject.SetActive(isAllPerfect);
			}
			if (apEffectComboRenderer != null)
			{
				apEffectComboRenderer.gameObject.SetActive(isAllPerfect);
			}

			int digitCount = combo > 0 ? Mathf.FloorToInt(Mathf.Log10(combo)) + 1 : 1;
			int value = combo;
			SpriteRenderer[] mainRenderers = NumberSpriteRenderers ?? new SpriteRenderer[0];
			SpriteRenderer[] effectRenderers = EffectNumberSpriteRenderers ?? new SpriteRenderer[0];
			for (int i = 0; i < mainRenderers.Length; i++)
			{
				bool visible = i < digitCount && combo > 0;
				float x = (digitCount - 1) * 0.475f + i * -0.95f;
				ApplyComboDigit(mainRenderers[i], visible, x, digitSprites, value % 10);
				if (i < effectRenderers.Length)
				{
					ApplyComboDigit(effectRenderers[i], visible, x, digitSprites, value % 10);
				}
				if (allPerfectEffectRenderers != null && i < allPerfectEffectRenderers.Length)
				{
					ApplyComboDigit(allPerfectEffectRenderers[i], visible, x, allPerfectEffectSprites, value % 10);
				}
				value /= 10;
			}
		}

		private void Update()
		{
			if (numberRoot != null)
			{
				float appear = Mathf.Clamp01(time / 0.1f);
				numberRoot.localScale = Vector3.one * (0.6f + appear * 0.4f);
			}

			float burst = Mathf.Clamp01((time - 0.1f) / 0.1f);
			float effectScale = time >= 0.1f ? 1f + burst * 0.4f : 0f;
			if (effectNumberRoot != null)
			{
				effectNumberRoot.localScale = Vector3.one * effectScale;
			}
			SpriteRenderer[] effectRenderers = EffectNumberSpriteRenderers;
			if (effectRenderers != null)
			{
				float alpha = time >= 0.1f ? (1f - burst) * 0.5f : 0f;
				for (int i = 0; i < effectRenderers.Length; i++)
				{
					SetRendererAlpha(effectRenderers[i], alpha);
				}
			}

			if (isAllPerfect)
			{
				if (allPerfectEffectRoot != null && numberRoot != null)
				{
					allPerfectEffectRoot.localScale = numberRoot.localScale;
				}
				float alpha = (Mathf.Sin(Time.time * AP_EFFECT_SPEED) + 1f) * 0.5f;
				if (allPerfectEffectRenderers != null)
				{
					for (int i = 0; i < allPerfectEffectRenderers.Length; i++)
					{
						SetRendererAlpha(allPerfectEffectRenderers[i], alpha);
					}
				}
				SetRendererAlpha(apEffectComboRenderer, alpha);
			}

			time += Time.deltaTime;
		}

		public ComboView()
		{
		}

		private static void ApplyComboDigit(SpriteRenderer renderer, bool visible, float localX, Sprite[] sprites, int digit)
		{
			if (renderer == null)
			{
				return;
			}
			renderer.transform.localScale = visible ? Vector3.one : Vector3.zero;
			renderer.transform.localPosition = Vector3.right * localX;
			if (visible && sprites != null && digit >= 0 && digit < sprites.Length)
			{
				renderer.sprite = sprites[digit];
			}
		}

		private static void SetRendererAlpha(SpriteRenderer renderer, float alpha)
		{
			if (renderer == null)
			{
				return;
			}
			Color color = renderer.color;
			color.a = alpha;
			renderer.color = color;
		}
	}
}
