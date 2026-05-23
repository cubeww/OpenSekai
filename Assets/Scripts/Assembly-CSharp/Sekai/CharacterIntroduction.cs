using Beebyte.Obfuscator;
using DG.Tweening;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class CharacterIntroduction : MonoBehaviour
	{
		private static float AUTO_CHANGE_DELAY;

		private const float VALUES_MAX = 10f;

		[SerializeField]
		private Sprite[] sprites;

		[SerializeField]
		private CustomImage bgImage;

		[SerializeField]
		private CustomButton leftArrow;

		[SerializeField]
		private CustomButton rightArrow;

		[SerializeField]
		private UIPartsGauge progressGauge;

		[SerializeField]
		private Texture2D maskTexNext;

		[SerializeField]
		private Texture2D maskTexPrev;

		[SerializeField]
		private CustomTextMesh progressText;

		private int currentIndex;

		private bool isAnimationRunning;

		private Tween autoChangeTweenHandle;

		private float _swapValue;

		public Material BgMaterial
		{
			get
			{
				return bgImage != null ? bgImage.material : null;
			}
		}

		public void Setup()
		{
			currentIndex = Mathf.Clamp(currentIndex, 0, Mathf.Max(0, (sprites?.Length ?? 1) - 1));
			ApplyCurrentSprite();
			UpdateProgressGauge(currentIndex + 1, sprites != null && sprites.Length > 0 ? sprites.Length : 1);
			StartAutoChange();
		}

		public void UpdateProgressGauge(float currentVal, float maxVal)
		{
			if (progressText != null)
			{
				progressText.SetText($"{Mathf.RoundToInt(currentVal)}/{Mathf.RoundToInt(maxVal)}");
			}
		}

		public void Unload()
		{
			StopAutoChange();
			if (BgMaterial != null)
			{
				BgMaterial.SetTexture("_MaskTex", null);
			}
		}

		private void SetSwapValue(float val)
		{
			_swapValue = val;
			if (BgMaterial != null)
			{
				BgMaterial.SetFloat("_SwapValue", _swapValue);
			}
		}

		[Skip]
		public void Next()
		{
			if (sprites == null || sprites.Length == 0 || isAnimationRunning)
			{
				return;
			}

			currentIndex = (currentIndex + 1) % sprites.Length;
			PlayChange(maskTexNext);
		}

		[Skip]
		public void Prev()
		{
			if (sprites == null || sprites.Length == 0 || isAnimationRunning)
			{
				return;
			}

			currentIndex = (currentIndex - 1 + sprites.Length) % sprites.Length;
			PlayChange(maskTexPrev);
		}

		[Skip]
		public void OnFlick(PointerGestureData gestureData)
		{
			if (gestureData == null)
			{
				return;
			}

			if (gestureData.IsFlickLeft)
			{
				Next();
			}
			else if (gestureData.IsFlickRight)
			{
				Prev();
			}
		}

		private void StartAutoChange()
		{
			StopAutoChange();
			if (sprites == null || sprites.Length <= 1)
			{
				return;
			}

			autoChangeTweenHandle = DOVirtual.DelayedCall(AUTO_CHANGE_DELAY, Next).SetLoops(-1, LoopType.Restart);
		}

		public void StopAutoChange()
		{
			if (autoChangeTweenHandle != null)
			{
				autoChangeTweenHandle.Kill(false);
				autoChangeTweenHandle = null;
			}
		}

		private void ResetAutoChange()
		{
			StartAutoChange();
		}

		private void PlayChange(Texture2D maskTexture)
		{
			ResetAutoChange();
			isAnimationRunning = true;
			if (BgMaterial != null)
			{
				BgMaterial.SetTexture("_MaskTex", maskTexture);
			}

			SetSwapValue(0f);
			DOTween.To(() => _swapValue, SetSwapValue, VALUES_MAX, 0.25f)
				.OnComplete(() =>
				{
					ApplyCurrentSprite();
					SetSwapValue(0f);
					isAnimationRunning = false;
				});
			UpdateProgressGauge(currentIndex + 1, sprites.Length);
		}

		private void ApplyCurrentSprite()
		{
			if (bgImage != null && sprites != null && sprites.Length > 0)
			{
				bgImage.sprite = sprites[currentIndex];
			}
		}

		public CharacterIntroduction()
		{
		}

		static CharacterIntroduction()
		{
			AUTO_CHANGE_DELAY = 5f;
		}
	}
}
