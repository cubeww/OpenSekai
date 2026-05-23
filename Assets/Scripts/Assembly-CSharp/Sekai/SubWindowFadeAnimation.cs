using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Sekai
{
	public class SubWindowFadeAnimation : SubWindowAnimationBase
	{
		private readonly float OPEN_ANIMATION_DURATION;
		private readonly float CLOSE_ANIMATION_DURATION;

		[SerializeField]
		private CanvasGroup canvasGroup;

		public override async UniTask OpenAnimation(CancellationToken ct = default)
		{
			if (canvasGroup == null)
			{
				OpenImmediately();
				return;
			}

			canvasGroup.DOKill();
			canvasGroup.blocksRaycasts = true;
			canvasGroup.DOFade(1f, OPEN_ANIMATION_DURATION);
			await UniTask.Delay((int)(OPEN_ANIMATION_DURATION * 1000f), cancellationToken: ct);
		}

		public override void InitializeAnimation()
		{
			canvasGroup = canvasGroup != null ? canvasGroup : GetComponent<CanvasGroup>();
			if (canvasGroup == null)
			{
				return;
			}

			canvasGroup.DOKill();
			canvasGroup.alpha = 0f;
			canvasGroup.blocksRaycasts = false;
		}

		public override UniTask CloseAnimation(CancellationToken ct = default)
		{
			return CloseAnimation(CLOSE_ANIMATION_DURATION, ct);
		}

		public override async UniTask CloseAnimation(float duration, CancellationToken ct = default)
		{
			if (canvasGroup == null)
			{
				CloseImmediately();
				return;
			}

			canvasGroup.DOKill();
			canvasGroup.blocksRaycasts = false;
			canvasGroup.DOFade(0f, duration);
			await UniTask.Delay((int)(duration * 1000f), cancellationToken: ct);
		}

		public override void OpenImmediately()
		{
			if (canvasGroup == null)
			{
				canvasGroup = GetComponent<CanvasGroup>();
			}
			if (canvasGroup == null)
			{
				return;
			}

			canvasGroup.DOKill();
			canvasGroup.alpha = 1f;
			canvasGroup.blocksRaycasts = true;
		}

		public override void CloseImmediately()
		{
			if (canvasGroup == null)
			{
				canvasGroup = GetComponent<CanvasGroup>();
			}
			if (canvasGroup == null)
			{
				return;
			}

			canvasGroup.DOKill();
			canvasGroup.alpha = 0f;
			canvasGroup.blocksRaycasts = false;
		}

		public SubWindowFadeAnimation()
		{
			OPEN_ANIMATION_DURATION = 0.2f;
			CLOSE_ANIMATION_DURATION = 0.2f;
		}
	}
}
