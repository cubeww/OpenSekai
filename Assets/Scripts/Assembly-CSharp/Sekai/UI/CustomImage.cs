using UnityEngine;

namespace Sekai.UI
{
	[AddComponentMenu("Custom UI/Custom Image")]
	public class CustomImage : AtlasImage
	{
		public float Alpha
		{
			get
			{
				return color.a;
			}
			set
			{
				Color current = color;
				current.a = value;
				color = current;
			}
		}

		public void SnapSize()
		{
			SetNativeSize();
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}

		public void Show()
		{
			SetActive(true);
		}

		public void Hide()
		{
			SetActive(false);
		}

		public CustomImage()
		{
		}
	}
}
