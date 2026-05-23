using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sekai.SubWindow;
using UnityEngine;

namespace Sekai
{
	public class SubWindowSlideAnimation : SubWindowAnimationBase
	{
		[Serializable]
		private enum SlideType
		{
			WindowAll = 0,
			ContentOnly = 1
		}

		[SerializeField]
		private SubWindowComponent subWindowComponent;

		[SerializeField]
		private Vector2 slideOffset;

		[SerializeField]
		private SlideType slideType;

		private ScreenSlideInOut slideInOut;

		private Vector2 SlideSize
		{
			get
			{
				Vector2 size = Vector2.zero;
				if (subWindowComponent != null)
				{
					size = slideType == SlideType.ContentOnly ? subWindowComponent.ContentSize : subWindowComponent.WindowSize;
				}
				return size + slideOffset;
			}
		}

		public override void InitializeAnimation()
		{
			base.InitializeAnimation();
			if (subWindowComponent == null)
			{
				return;
			}

			slideInOut = subWindowComponent.gameObject.AddComponent<ScreenSlideInOut>();
			slideInOut.Setup(subWindowComponent.gameObject);
		}

		public override UniTask OpenAnimation(CancellationToken ct = default(CancellationToken))
		{
			if (slideInOut == null || subWindowComponent == null)
			{
				OpenImmediately();
				return UniTask.CompletedTask;
			}

			Vector2 openStart = GetOpenStart();
			slideInOut.SetVector(new Vector3(openStart.x, openStart.y, 0f), Vector3.zero);
			return slideInOut.Play(ct);
		}

		public override UniTask CloseAnimation(CancellationToken ct = default(CancellationToken))
		{
			if (slideInOut == null || subWindowComponent == null)
			{
				CloseImmediately();
				return UniTask.CompletedTask;
			}

			Vector2 closeTarget = GetCloseTarget();
			slideInOut.SetVector(Vector3.zero, new Vector3(closeTarget.x, closeTarget.y, 0f));
			return slideInOut.Play(ct);
		}

		public override void CloseImmediately()
		{
			RectTransform rectTransform = subWindowComponent != null ? subWindowComponent.transform as RectTransform : null;
			if (rectTransform != null)
			{
				rectTransform.anchoredPosition = GetCloseTarget();
			}
		}

		public override void OpenImmediately()
		{
			RectTransform rectTransform = subWindowComponent != null ? subWindowComponent.transform as RectTransform : null;
			if (rectTransform != null)
			{
				rectTransform.anchoredPosition = Vector2.zero;
			}
		}

		private Vector2 GetOpenStart()
		{
			if (subWindowComponent == null)
			{
				return Vector2.zero;
			}

			switch (subWindowComponent.GetWindowType)
			{
				case WindowType.Left:
					return new Vector2(-SlideSize.x, 0f);
				case WindowType.Right:
					return new Vector2(SlideSize.x, 0f);
				case WindowType.Bottom:
					return new Vector2(0f, -SlideSize.y);
				default:
					return Vector2.zero;
			}
		}

		private Vector2 GetCloseTarget()
		{
			if (subWindowComponent == null)
			{
				return Vector2.zero;
			}

			switch (subWindowComponent.GetWindowType)
			{
				case WindowType.Left:
					return new Vector2(-SlideSize.x, 0f);
				case WindowType.Right:
					return new Vector2(SlideSize.x, 0f);
				case WindowType.Bottom:
					return new Vector2(0f, -SlideSize.y);
				default:
					return Vector2.zero;
			}
		}

		public override void Complete()
		{
			if (slideInOut != null)
			{
				slideInOut.Complete();
			}
		}
	}
}
