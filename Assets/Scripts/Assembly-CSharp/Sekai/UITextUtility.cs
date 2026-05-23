using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai
{
	public class UITextUtility
	{
		public static void UpdateFittingShowAreaText(string str, Text text, RectTransform showArea)
		{
			if (text == null)
			{
				return;
			}

			text.text = str ?? string.Empty;
			if (showArea == null)
			{
				return;
			}

			var generator = text.cachedTextGeneratorForLayout;
			var settings = text.GetGenerationSettings(showArea.rect.size);
			var preferredWidth = generator.GetPreferredWidth(text.text, settings) / text.pixelsPerUnit;
			if (preferredWidth > showArea.rect.width && preferredWidth > 0f)
			{
				var scale = Mathf.Clamp01(showArea.rect.width / preferredWidth);
				text.rectTransform.localScale = new Vector3(scale, text.rectTransform.localScale.y, text.rectTransform.localScale.z);
			}
		}

		public static void UpdateFittingShowAreaText(string str, CustomTextMesh text, RectTransform showArea)
		{
			if (text == null)
			{
				return;
			}

			text.SetText(str);
			if (showArea == null)
			{
				return;
			}

			var preferredWidth = text.preferredWidth;
			if (preferredWidth > showArea.rect.width && preferredWidth > 0f)
			{
				var scale = Mathf.Clamp01(showArea.rect.width / preferredWidth);
				text.rectTransform.localScale = new Vector3(scale, text.rectTransform.localScale.y, text.rectTransform.localScale.z);
			}
		}

		public static string ChangeNonBreakingSpace(string str)
		{
			return string.IsNullOrEmpty(str) ? str : str.Replace(" ", "\u00A0");
		}

		public UITextUtility()
		{
		}
	}
}
