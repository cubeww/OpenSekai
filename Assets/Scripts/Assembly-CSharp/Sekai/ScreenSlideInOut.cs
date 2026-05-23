using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Sekai
{
	public class ScreenSlideInOut : ScreenInOutAnimBase
	{
		private const float Duration = 0.2f;

		private Tweener tweener;

		private Vector3 from;

		private Vector3 to;

		private RectTransform rt;

		public override void Setup(GameObject obj)
		{
			base.Setup(obj);
			if (attachObject == null)
			{
				rt = null;
				return;
			}

			CanvasGroup canvasGroup = attachObject.GetComponent<CanvasGroup>();
			if (canvasGroup != null)
			{
				Destroy(canvasGroup);
			}

			rt = attachObject.GetComponent<RectTransform>();
			Destory();
		}

		public override void SetVector(Vector3 start, Vector3 target)
		{
			from = start;
			to = target;
			if (rt != null)
			{
				rt.anchoredPosition = new Vector2(from.x, from.y);
			}
		}

		public override async UniTask Play(CancellationToken ct = default(CancellationToken))
		{
			if (rt == null)
			{
				return;
			}

			Destory();
			rt.anchoredPosition = new Vector2(from.x, from.y);
			bool finished = false;
			tweener = rt.DOAnchorPos(new Vector2(to.x, to.y), Duration).SetEase(Ease.OutQuart);
			tweener.OnComplete(() => finished = true);
			tweener.OnKill(() => finished = true);

			try
			{
				await UniTask.WaitUntil(() => finished, cancellationToken: ct);
			}
			catch (OperationCanceledException)
			{
				Destory();
			}
		}

		public override void Play(Action callback)
		{
			this.callback = callback;
			if (rt == null)
			{
				OnFinished();
				return;
			}

			Destory();
			rt.anchoredPosition = new Vector2(from.x, from.y);
			tweener = rt.DOAnchorPos(new Vector2(to.x, to.y), Duration)
				.SetEase(Ease.OutQuart)
				.OnComplete(OnFinished);
		}

		public void Complete()
		{
			tweener?.Complete();
		}

		public override void Destory()
		{
			if (tweener != null && tweener.IsActive())
			{
				tweener.Kill(false);
			}
			tweener = null;
		}
	}
}
