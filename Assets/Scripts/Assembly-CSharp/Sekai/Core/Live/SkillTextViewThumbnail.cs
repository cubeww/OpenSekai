using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Sekai.Core.Live
{
	public class SkillTextViewThumbnail : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer thumbnail;

		[SerializeField]
		private SpriteRenderer frame;

		[SerializeField]
		private SpriteRenderer rarityFrame;

		[SerializeField]
		private Sprite[] raritySprites;

		public void Initialize(Sprite thumbnailSprite, CardRarityType rarityType, float size)
		{
			if (thumbnail != null)
			{
				thumbnail.sprite = thumbnailSprite;
				thumbnail.enabled = thumbnailSprite != null;
			}

			if (rarityFrame != null)
			{
				Sprite raritySprite = GetRaritySprite(rarityType);
				rarityFrame.sprite = raritySprite;
				rarityFrame.enabled = raritySprite != null;
			}

			transform.localScale = Vector3.one * size;
			SetActive(true);
			FadeInImmediate();
		}

		public void SetFrameColor(Color color)
		{
			if (frame != null)
			{
				frame.color = color;
			}
		}

		private Sprite GetRaritySprite(CardRarityType rarityType)
		{
			if (raritySprites == null || raritySprites.Length == 0)
			{
				return null;
			}

			int index = Mathf.Clamp((int)rarityType - 1, 0, raritySprites.Length - 1);
			return raritySprites[index];
		}

		public void SetActive(bool isActive)
		{
			gameObject.SetActive(isActive);
		}

		public Sequence FadeOut(float duration)
		{
			return FadeTo(0f, duration);
		}

		public Sequence FadeIn(float duration)
		{
			gameObject.SetActive(true);
			return FadeTo(1f, duration);
		}

		public void FadeInImmediate()
		{
			ChangeAllAlpha(1f);
		}

		public void FadeOutImmediate()
		{
			ChangeAllAlpha(0f);
		}

		private void ChangeAllAlpha(float a)
		{
			foreach (SpriteRenderer renderer in GetRenderers())
			{
				Color color = renderer.color;
				color.a = a;
				renderer.color = color;
			}
		}

		public SkillTextViewThumbnail()
		{
		}

		private Sequence FadeTo(float alpha, float duration)
		{
			Sequence sequence = DOTween.Sequence();
			foreach (SpriteRenderer renderer in GetRenderers())
			{
				renderer.DOKill();
				sequence.Join(renderer.DOFade(alpha, duration));
			}
			return sequence;
		}

		private SpriteRenderer[] GetRenderers()
		{
			List<SpriteRenderer> renderers = new List<SpriteRenderer>(3);
			if (thumbnail != null)
			{
				renderers.Add(thumbnail);
			}
			if (frame != null)
			{
				renderers.Add(frame);
			}
			if (rarityFrame != null)
			{
				renderers.Add(rarityFrame);
			}
			return renderers.ToArray();
		}
	}
}
