using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsFillCover : MonoBehaviour
	{
		[SerializeField]
		private ColorFader fader;

		[SerializeField]
		private CustomImage fillImage;

		[SerializeField]
		private CustomButton closeButton;

		public Color FillColor
		{
			get => fillImage != null ? fillImage.color : Color.clear;
			set
			{
				if (fillImage != null)
				{
					fillImage.color = value;
				}
				if (fader != null)
				{
					fader.Set(value);
				}
			}
		}

		public ColorFader Fader => fader;
		public CustomButton CloseButton => closeButton;

		public bool EnableClick
		{
			get => closeButton != null && closeButton.interactable && fillImage != null && fillImage.raycastTarget;
			set
			{
				if (closeButton != null)
				{
					closeButton.interactable = value;
				}
				if (fillImage != null)
				{
					fillImage.raycastTarget = value;
				}
			}
		}

		private void Awake()
		{
			if (fillImage == null)
			{
				fillImage = GetComponent<CustomImage>();
			}
			if (closeButton == null)
			{
				closeButton = GetComponent<CustomButton>();
			}
			if (fader == null)
			{
				fader = GetComponent<ColorFader>();
			}

			if (fader != null && fillImage != null)
			{
				fader.Set(fillImage.color);
			}
		}
	}
}
