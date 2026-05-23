using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai
{
	public sealed class DialogSizeFitter : MonoBehaviour
	{
		public enum ViewType
		{
			Default = 0,
			HasTab = 1
		}

		private static readonly Dictionary<DialogSize, float> SIZE_DIC;

		private static readonly float bottomPadding;

		[SerializeField]
		private RectTransform windowRect;

		[SerializeField]
		private LayoutElement paddingLayoutElement;

		[SerializeField]
		private LayoutElement content;

		public void Setup(DialogSize dialogSize, ViewType viewType = ViewType.Default)
		{
			if (dialogSize != DialogSize.Manual && SIZE_DIC.TryGetValue(dialogSize, out float width))
			{
				if (windowRect != null)
				{
					Vector2 sizeDelta = windowRect.sizeDelta;
					sizeDelta.x = width;
					windowRect.sizeDelta = sizeDelta;
				}
				if (content != null)
				{
					content.preferredWidth = width;
				}
			}

			if (paddingLayoutElement != null)
			{
				paddingLayoutElement.preferredHeight = viewType == ViewType.HasTab ? 0f : bottomPadding;
			}
		}

		public DialogSizeFitter()
		{
		}

		static DialogSizeFitter()
		{
			// Restored enough for copied dialog prefabs; exact original size constants
			// should be checked against IDA if another dialog depends on pixel parity.
			SIZE_DIC = new Dictionary<DialogSize, float>
			{
				{ DialogSize.Small, 720f },
				{ DialogSize.Medium, 960f },
				{ DialogSize.Large, 1200f }
			};
			bottomPadding = 32f;
		}
	}
}
