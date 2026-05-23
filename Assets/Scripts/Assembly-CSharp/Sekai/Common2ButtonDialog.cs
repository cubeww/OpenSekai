using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class Common2ButtonDialog : DialogBase
	{
		[SerializeField]
		protected CustomButton positiveButton;

		[SerializeField]
		protected CustomTextMesh positiveButtonLabelMesh;

		[SerializeField]
		protected CustomText positiveButtonLabel;

		[SerializeField]
		protected CustomButton negativeButton;

		[SerializeField]
		protected CustomTextMesh negativeButtonLabelMesh;

		[SerializeField]
		protected CustomText negativeButtonLabel;

		[SerializeField]
		protected CustomTextMesh messageBodyTextMesh;

		[SerializeField]
		protected CustomText messageBodyText;

		protected ICustomText messageBodyTextInterFace;

		protected Action onClickOK;
		protected Action onClickCancel;

		public void Setup(string messageBodyKey, string okButtonLabelKey, string cancelButtonLabelKey, Action onClickOK, Action onClickCancel)
		{
			Initialize(messageBodyKey, okButtonLabelKey, cancelButtonLabelKey, onClickOK, onClickCancel);
		}

		public virtual void Initialize(string messageBodyKey, string okButtonLabelKey, string cancelButtonLabelKey, Action onClickOK, Action onClickCancel, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true)
		{
			base.Initialize(dialogSize, allowCloseExternal);
			this.onClickOK = onClickOK;
			this.onClickCancel = onClickCancel;
			SetButtonLabel(positiveButtonLabelMesh, positiveButtonLabel, okButtonLabelKey);
			SetButtonLabel(negativeButtonLabelMesh, negativeButtonLabel, cancelButtonLabelKey);
			BindButton(positiveButton, OnClickOK);
			BindButton(negativeButton, OnClickCancel);
			SetMessageBodyTextInterface();
			if (!string.IsNullOrEmpty(messageBodyKey))
			{
				SetWordingText(messageBodyKey);
			}
		}

		public virtual void Initialize(string titleKey, string messageBodyKey, string okButtonLabelKey, string cancelButtonLabelKey, Action onClickOK, Action onClickCancel, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true)
		{
			Initialize(messageBodyKey, okButtonLabelKey, cancelButtonLabelKey, onClickOK, onClickCancel, dialogSize, allowCloseExternal);
			if (!string.IsNullOrEmpty(titleKey) && header != null)
			{
				header.TitleKey = titleKey;
			}
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

		protected virtual void OnClickOK()
		{
			onClickOK?.Invoke();
			Close();
		}

		protected virtual void OnClickCancel()
		{
			onClickCancel?.Invoke();
			Close();
		}

		protected override void OnCloseExternal()
		{
			OnClickCancel();
		}

		protected override void OnHardwareBackKeyProcess()
		{
			OnClickCancel();
		}

		private static void BindButton(CustomButton button, Action action)
		{
			if (button == null)
			{
				return;
			}

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => action?.Invoke());
		}

		private static void SetButtonLabel(CustomTextMesh labelMesh, CustomText label, string wordingKey)
		{
			if (string.IsNullOrEmpty(wordingKey))
			{
				return;
			}

			ICustomText customText = labelMesh != null ? (ICustomText)labelMesh : label;
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
