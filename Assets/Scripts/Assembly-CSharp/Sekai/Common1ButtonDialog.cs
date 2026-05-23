using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class Common1ButtonDialog : DialogBase
	{
		[SerializeField]
		protected CustomButton positiveButton;

		[SerializeField]
		protected CustomText positiveButtonLabel;

		[SerializeField]
		protected CustomTextMesh positiveButtonLabelText;

		[SerializeField]
		protected CustomText messageBodyText;

		[SerializeField]
		protected CustomTextMesh messageBodyTextMesh;

		protected ICustomText messageBodyTextInterFace;
		protected Action onClickOK;

		public string TitleText
		{
			get => header != null ? header.TitleText : string.Empty;
			set
			{
				if (header != null)
				{
					header.TitleText = value;
				}
			}
		}

		public string TitleKey
		{
			get => header != null ? header.TitleKey : string.Empty;
			set
			{
				if (header != null)
				{
					header.TitleKey = value;
				}
			}
		}

		public void Setup(string messageBodyKey, string okButtonLabelKey, Action onClickOK)
		{
			Initialize(messageBodyKey, okButtonLabelKey, onClickOK);
		}

		public void Initialize(string messageBodyKey, string okButtonLabelKey, Action onClickOK, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true)
		{
			base.Initialize(dialogSize, allowCloseExternal);
			Initialize(messageBodyKey, okButtonLabelKey, onClickOK);
		}

		public void Initialize(string titleKey, string messageBodyKey, string okButtonLabelKey, Action onClickOK, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true)
		{
			if (!string.IsNullOrEmpty(titleKey))
			{
				TitleKey = titleKey;
			}

			Initialize(messageBodyKey, okButtonLabelKey, onClickOK, dialogSize, allowCloseExternal);
		}

		private void Initialize(string messageBodyKey, string okButtonLabelKey, Action onClickOK)
		{
			this.onClickOK = onClickOK;
			SetButtonLabel(okButtonLabelKey);
			BindButton();
			SetMessageBodyTextInterface();
			if (!string.IsNullOrEmpty(messageBodyKey))
			{
				SetWordingText(messageBodyKey);
			}
		}

		private void SetMessageBodyText()
		{
			messageBodyTextInterFace?.UpdateWordingText();
		}

		public void SetWordingText(string key, params object[] args)
		{
			SetMessageBodyTextInterface();
			if (messageBodyTextInterFace == null || string.IsNullOrEmpty(key))
			{
				return;
			}

			messageBodyTextInterFace.UseWordingKey = true;
			if (args != null && args.Length > 0)
			{
				messageBodyTextInterFace.SetWordingText(key, args);
			}
			else
			{
				messageBodyTextInterFace.SetWordingText(key);
			}
		}

		public void SetMessageBodyText(string messageBody)
		{
			SetMessageBodyTextInterface();
			if (messageBodyTextInterFace == null)
			{
				return;
			}

			messageBodyTextInterFace.UseWordingKey = false;
			messageBodyTextInterFace.SetText(messageBody);
		}

		public void ChangeWindowSizePreferredHeight(float width, float height)
		{
			float preferredHeight = height;
			SetMessageBodyTextInterface();
			if (messageBodyTextInterFace != null)
			{
				preferredHeight = Mathf.Max(height, messageBodyTextInterFace.PreferredHeight);
			}

			ChangeWindowSize(new Vector2(width, preferredHeight));
		}

		public override void Open()
		{
			SetMessageBodyText();
			base.Open();
		}

		protected virtual void OnClickOK()
		{
			onClickOK?.Invoke();
			Close();
		}

		protected override void OnCloseExternal()
		{
			OnClickOK();
		}

		protected override void OnHardwareBackKeyProcess()
		{
			OnClickOK();
		}

		private void BindButton()
		{
			if (positiveButton == null)
			{
				return;
			}

			positiveButton.onClick.RemoveAllListeners();
			positiveButton.onClick.AddListener(OnClickOK);
		}

		private void SetButtonLabel(string wordingKey)
		{
			if (string.IsNullOrEmpty(wordingKey))
			{
				return;
			}

			ICustomText customText = positiveButtonLabelText != null ? (ICustomText)positiveButtonLabelText : positiveButtonLabel;
			if (customText == null)
			{
				return;
			}

			customText.UseWordingKey = true;
			customText.SetWordingText(wordingKey);
		}

		private void SetMessageBodyTextInterface()
		{
			if (messageBodyTextInterFace != null)
			{
				return;
			}

			messageBodyTextInterFace = messageBodyTextMesh != null ? (ICustomText)messageBodyTextMesh : messageBodyText;
		}
	}
}
