using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsLoadingCircle : MonoBehaviour
	{
		public enum ColorType
		{
			Green = 0,
			Gray = 1,
			White = 2,
			LightGray = 3
		}

		[SerializeField]
		private TweenScale[] circleTweens;

		[SerializeField]
		private CustomImage[] circleImages;

		[SerializeField]
		private ColorType colorType;

		[SerializeField]
		private CanvasGroup canvasGroup;

		public TweenBase.BehaviourType TweenBehaviour
		{
			set
			{
				if (circleTweens == null)
				{
					return;
				}

				foreach (TweenScale circleTween in circleTweens)
				{
					if (circleTween != null)
					{
						circleTween.Behaviour = value;
					}
				}
			}
		}

		public bool Enable
		{
			set
			{
				if (canvasGroup != null)
				{
					canvasGroup.alpha = value ? 1f : 0f;
				}
			}
		}

		private void Start()
		{
		}

		private void OnValidate()
		{
			UpdateView();
		}

		private void UpdateView()
		{
			if (circleImages == null)
			{
				return;
			}

			Color color = colorType switch
			{
				ColorType.Gray => new Color(0.45f, 0.45f, 0.45f, 1f),
				ColorType.White => Color.white,
				ColorType.LightGray => new Color(0.8f, 0.8f, 0.8f, 1f),
				_ => new Color(0f, 0.73f, 0.83f, 1f)
			};

			foreach (CustomImage image in circleImages)
			{
				if (image != null)
				{
					image.color = color;
				}
			}
		}

		public void SetReferenceTween(UIPartsLoadingCircle masterTween)
		{
			if (masterTween == null || circleTweens == null || masterTween.circleTweens == null)
			{
				return;
			}

			for (int i = 0; i < circleTweens.Length && i < masterTween.circleTweens.Length; i++)
			{
				if (circleTweens[i] != null)
				{
					circleTweens[i].ReferenceTween = masterTween.circleTweens[i];
					circleTweens[i].Behaviour = TweenBase.BehaviourType.Reference;
				}
			}
		}

		public void Show()
		{
			Enable = true;
		}

		public void Hide()
		{
			Enable = false;
		}

		public UIPartsLoadingCircle()
		{
		}
	}
}
