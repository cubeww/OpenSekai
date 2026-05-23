using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsMusicCollaborationRibbon : MonoBehaviour
	{
		[SerializeField]
		private CustomTextMesh collaborationText;

		[SerializeField]
		private float collaborationTextPadding;

		[SerializeField]
		private float widthMax;

		[SerializeField]
		private CustomImage coverImage;

		public bool Lock
		{
			get
			{
				return coverImage != null && coverImage.gameObject.activeSelf;
			}
			set
			{
				if (coverImage != null)
				{
					coverImage.gameObject.SetActive(value);
				}
			}
		}

		public void SetText(string text)
		{
			if (collaborationText == null)
			{
				return;
			}
			collaborationText.SetText(text);
			RectTransform rectTransform = transform as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			Vector2 size = rectTransform.sizeDelta;
			float preferredWidth = collaborationText.preferredWidth + collaborationTextPadding;
			size.x = Mathf.Min(preferredWidth, widthMax);
			rectTransform.sizeDelta = size;
		}

		public UIPartsMusicCollaborationRibbon()
		{
			collaborationTextPadding = 18f;
			widthMax = 856f;
		}
	}
}
