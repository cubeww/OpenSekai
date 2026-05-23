using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
	[AddComponentMenu("Custom UI/Custom Text")]
	public class CustomText : Text, ICustomText
	{
		[SerializeField]
		private bool useWordingKey;

		[SerializeField]
		protected string wordingKey;

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
				fontSize = (int)value;
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
				wordingKey = value;
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
			UpdateWordingText();
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
			formatArgs = null;
		}

		public void SetWordingText(string key)
		{
			SetWordingKey(key);
			UpdateWordingText();
		}

		public void SetWordingKey(string key, params object[] args)
		{
			wordingKey = key;
			formatArgs = args;
		}

		public void SetWordingText(string key, params object[] args)
		{
			SetWordingKey(key, args);
			UpdateWordingText();
		}

		public void SetDefaultFontDB()
		{
			var loadedFont = Resources.Load<Font>("Font/FOT-RodinNTLGPro-DB");
			if (loadedFont != null)
			{
				font = loadedFont;
			}
		}

		public void SetDefaultFontEB()
		{
			var loadedFont = Resources.Load<Font>("Font/FOT-RodinNTLGPro-EB");
			if (loadedFont != null)
			{
				font = loadedFont;
			}
		}

		public void SetText(string value, bool breakSpace = false)
		{
			text = breakSpace ? value ?? string.Empty : UITextUtility.ChangeNonBreakingSpace(value ?? string.Empty);
		}

		public CustomText()
		{
		}
	}
}
