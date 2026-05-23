using Beebyte.Obfuscator;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Sekai.Core.Live
{
	public class SkillTextViewAnimation : MonoBehaviour
	{
		private static readonly float DURATION;

		[SerializeField]
		private Transform root;

		[SerializeField]
		private Transform levelRoot;

		[SerializeField]
		private Transform skillEffectRoot;

		[SerializeField]
		private TextMeshPro skillEffectText;

		[SerializeField]
		private Transform iconRoot;

		[SerializeField]
		private SpriteRenderer iconSprite;

		[SerializeField]
		private Transform referenceTargetRoot;

		[SerializeField]
		private SkillTextViewThumbnail referenceTargetThumbnail;

		[SerializeField]
		private SkillTextViewThumbnail referenceTargetThumbnailForEncore;

		[SerializeField]
		private SpriteRenderer referenceSprite;

		private Tween[] tweens;

		private Sequence animationSequence;

		private bool isReferenceSkill;

		private bool isEncore;

		private SpriteRenderer[] spriteRenderers;

		[SerializeField]
		private SpriteRenderer[] excludeSpriteRenderers;

		private SpriteRenderer[] levelSpriteRenderers;

		public void Initialize(bool isReferenceSkill = false)
		{
			this.isReferenceSkill = isReferenceSkill;
			spriteRenderers = root == null ? GetComponentsInChildren<SpriteRenderer>(true) : root.GetComponentsInChildren<SpriteRenderer>(true);
			levelSpriteRenderers = levelRoot == null ? new SpriteRenderer[0] : levelRoot.GetComponentsInChildren<SpriteRenderer>(true);
			if (referenceTargetRoot != null)
			{
				referenceTargetRoot.gameObject.SetActive(isReferenceSkill);
			}
			Clear();
		}

		public void Execute(bool isEncore = false)
		{
			this.isEncore = isEncore;
			CreateAnimation();
			animationSequence?.Restart();
		}

		[Skip]
		public void Clear()
		{
			animationSequence?.Kill(false);
			animationSequence = null;
			if (tweens != null)
			{
				for (int i = 0; i < tweens.Length; i++)
				{
					tweens[i]?.Kill(false);
				}
			}
			Fade(spriteRenderers, 0f);
			if (root != null)
			{
				root.localScale = Vector3.one;
			}
			if (iconRoot != null)
			{
				iconRoot.localScale = Vector3.one;
			}
			if (skillEffectRoot != null)
			{
				skillEffectRoot.localScale = Vector3.one;
			}
			if (referenceSprite != null)
			{
				Color color = referenceSprite.color;
				color.a = 0f;
				referenceSprite.color = color;
			}
			referenceTargetThumbnail?.FadeOutImmediate();
			referenceTargetThumbnailForEncore?.FadeOutImmediate();
		}

		private void Fade(SpriteRenderer[] spriteRenderers, float value)
		{
			if (spriteRenderers == null)
			{
				return;
			}

			for (int i = 0; i < spriteRenderers.Length; i++)
			{
				SpriteRenderer renderer = spriteRenderers[i];
				if (renderer == null || IsExcluded(renderer))
				{
					continue;
				}

				Color color = renderer.color;
				color.a = value;
				renderer.color = color;
			}
		}

		private void CreateAnimation()
		{
			animationSequence?.Kill(false);
			animationSequence = DOTween.Sequence();

			Fade(spriteRenderers, 0f);
			if (root != null)
			{
				root.localScale = Vector3.one * 0.9f;
				animationSequence.Join(root.DOScale(1f, DURATION * 0.35f).SetEase(Ease.OutBack));
			}

			animationSequence.Join(DOTween.To(() => 0f, value => Fade(spriteRenderers, value), 1f, DURATION * 0.25f));
			if (iconRoot != null)
			{
				iconRoot.localScale = Vector3.one * 0.7f;
				animationSequence.Join(iconRoot.DOScale(1f, DURATION * 0.35f).SetEase(Ease.OutBack));
			}
			if (skillEffectRoot != null)
			{
				skillEffectRoot.localScale = Vector3.one * 0.8f;
				animationSequence.Join(skillEffectRoot.DOScale(1f, DURATION * 0.35f).SetEase(Ease.OutBack));
			}

			SkillTextViewThumbnail thumbnail = isEncore ? referenceTargetThumbnailForEncore : referenceTargetThumbnail;
			if (isReferenceSkill && thumbnail != null)
			{
				animationSequence.Join(thumbnail.FadeIn(DURATION * 0.2f));
			}

			animationSequence.AppendInterval(DURATION * 0.65f);
			animationSequence.Append(DOTween.To(() => 1f, value => Fade(spriteRenderers, value), 0f, DURATION * 0.25f));
			animationSequence.SetAutoKill(false);
		}

		public SkillTextViewAnimation()
		{
		}

		static SkillTextViewAnimation()
		{
			DURATION = 1.5f;
		}

		private bool IsExcluded(SpriteRenderer renderer)
		{
			if (excludeSpriteRenderers == null)
			{
				return false;
			}

			for (int i = 0; i < excludeSpriteRenderers.Length; i++)
			{
				if (excludeSpriteRenderers[i] == renderer)
				{
					return true;
				}
			}
			return false;
		}
	}
}
