using TMPro;
using UnityEngine;

namespace Sekai.UI
{
	[AddComponentMenu("Custom UI/Custom Text Mesh")]
	public class CustomTextMesh : TextMeshProUGUI, ICustomText
	{
		[SerializeField]
		private bool useWordingKey;

		[SerializeField]
		protected string wordingKey;

		[SerializeField]
		protected uint maxValueUpToAutoSize;

		[SerializeField]
		protected bool enableWidthScaleFit;

		private float originalScaleX;

		private bool isOriginalScaleXInitialized;

		private float lastRectWidth;

		private const float RECT_WIDTH_CHANGE_THRESHOLD = 0.01f;

		private string lastText;

		private object[] formatArgs;

		public string Text
		{
			get
			{
				return text;
			}
		}

		public RectTransform RectTransform
		{
			get
			{
				return rectTransform;
			}
		}

		public float PreferredHeight
		{
			get
			{
				return preferredHeight;
			}
		}

		public float FontSize
		{
			get
			{
				return fontSize;
			}
			set
			{
				fontSize = value;
			}
		}

		public string WordingKey
		{
			get
			{
				return wordingKey;
			}
			set
			{
				SetWordingKey(value);
			}
		}

		public bool UseWordingKey
		{
			get
			{
				return useWordingKey;
			}
			set
			{
				useWordingKey = value;
			}
		}

		protected override void Start()
		{
			base.Start();
			InitializeOriginalScaleX();
			if (useWordingKey)
			{
				UpdateWordingText();
			}
		}

		private void InitializeOriginalScaleX()
		{
			if (!isOriginalScaleXInitialized)
			{
				originalScaleX = transform.localScale.x;
				isOriginalScaleXInitialized = true;
			}
		}

		public void UpdateWordingText()
		{
			if (!useWordingKey || string.IsNullOrEmpty(wordingKey))
			{
				return;
			}

			SetText(formatArgs != null ? WordingManager.GetFormat(wordingKey, formatArgs) : WordingManager.Get(wordingKey), true);
		}

		public void SetWordingKey(string key)
		{
			wordingKey = key;
			useWordingKey = true;
			UpdateWordingText();
		}

		public void SetWordingText(string key)
		{
			SetWordingKey(key);
		}

		public void SetWordingKey(string key, params object[] args)
		{
			wordingKey = key;
			formatArgs = args;
			useWordingKey = true;
			UpdateWordingText();
		}

		public void SetWordingText(string key, params object[] args)
		{
			SetWordingKey(key, args);
		}

		public void SetDefaultFontDB()
		{
		}

		public void SetDefaultFontEB()
		{
		}

		public new void SetText(string value, bool breakSpace = false)
		{
			text = value ?? string.Empty;
			UpdateLastText();
			UpdateWidthScaleFit();
		}

		public void SetNumericReplaceIfExceedsThreshold(int value, int threshold, string replacement, bool breakSpace = false)
		{
			SetText(value > threshold ? replacement : value.ToString(), breakSpace);
		}

		public void SetActive(bool value)
		{
			gameObject.SetActive(value);
		}

		public void Show()
		{
			SetActive(true);
		}

		public void Hide()
		{
			SetActive(false);
		}

		public void SetMaterialOutlineColor(Color newColor)
		{
			if (fontMaterial != null && fontMaterial.HasProperty(ShaderUtilities.ID_OutlineColor))
			{
				fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, newColor);
			}
		}

		public void UpdateMaxValueUpToAutoSize()
		{
			if (maxValueUpToAutoSize > 0 && int.TryParse(text, out int value))
			{
				enableAutoSizing = value <= maxValueUpToAutoSize;
			}
		}

		private void EnableParentContentSizeFitter(bool enable)
		{
			UnityEngine.UI.ContentSizeFitter fitter = transform.parent != null ? transform.parent.GetComponent<UnityEngine.UI.ContentSizeFitter>() : null;
			if (fitter != null)
			{
				fitter.enabled = enable;
			}
		}

		public void ForceRebuildLayout()
		{
			UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		}

		private float GetTextWidth()
		{
			return preferredWidth;
		}

		private void ResetScaleX(Vector3 currentScale)
		{
			currentScale.x = originalScaleX;
			transform.localScale = currentScale;
		}

		private float CalculateScaleX(float textWidth, float rectWidth)
		{
			if (rectWidth <= 0f || textWidth <= rectWidth)
			{
				return originalScaleX;
			}
			return originalScaleX * rectWidth / textWidth;
		}

		private bool IsWidthScaleFitEnabled()
		{
			return enableWidthScaleFit && rectTransform != null;
		}

		private bool IsValidRectWidth(float rectWidth)
		{
			return rectWidth > 0.01f;
		}

		private void UpdateWidthScaleFit()
		{
			if (!IsWidthScaleFitEnabled())
			{
				return;
			}
			InitializeOriginalScaleX();
			float rectWidth = rectTransform.rect.width;
			if (!IsValidRectWidth(rectWidth))
			{
				return;
			}
			Vector3 scale = transform.localScale;
			float textWidth = GetTextWidth();
			scale.x = CalculateScaleX(textWidth, rectWidth);
			transform.localScale = scale;
			UpdateLastRectWidth(rectWidth);
		}

		private bool IsTextChanged()
		{
			return lastText != text;
		}

		private bool IsRectSizeChanged(float currentRectWidth)
		{
			return Mathf.Abs(lastRectWidth - currentRectWidth) > RECT_WIDTH_CHANGE_THRESHOLD;
		}

		private bool ShouldUpdateAutoSize(bool textChanged)
		{
			return textChanged && maxValueUpToAutoSize > 0;
		}

		private bool ShouldUpdateWidthScaleFit(bool textChanged, bool rectSizeChanged)
		{
			return IsWidthScaleFitEnabled() && (textChanged || rectSizeChanged);
		}

		private void UpdateLastText()
		{
			lastText = text;
		}

		private void UpdateLastRectWidth(float currentRectWidth)
		{
			lastRectWidth = currentRectWidth;
		}

		private void LateUpdate()
		{
			float rectWidth = rectTransform != null ? rectTransform.rect.width : 0f;
			bool textChanged = IsTextChanged();
			bool rectChanged = IsRectSizeChanged(rectWidth);
			if (ShouldUpdateAutoSize(textChanged))
			{
				UpdateMaxValueUpToAutoSize();
			}
			if (ShouldUpdateWidthScaleFit(textChanged, rectChanged))
			{
				UpdateWidthScaleFit();
			}
			if (textChanged)
			{
				UpdateLastText();
			}
		}

		public CustomTextMesh()
		{
			enableWidthScaleFit = true;
			originalScaleX = 1f;
		}
	}
}
