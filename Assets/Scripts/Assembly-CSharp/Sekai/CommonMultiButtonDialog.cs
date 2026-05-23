using System;
using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class CommonMultiButtonDialog : DialogBase
	{
		[SerializeField]
		protected List<CommonMultiButtonDialogObjects> dialogButtonObjectList;

		[SerializeField]
		protected CustomTextMesh messageBodyTextMesh;

		protected ICustomText messageBodyTextInterFace;

		public Dictionary<string, Action> Actions { get; private set; } = new Dictionary<string, Action>();

		public CommonMultiButtonDialogObjects GetButton(string key)
		{
			if (dialogButtonObjectList == null || string.IsNullOrEmpty(key))
			{
				return null;
			}

			foreach (var buttonObject in dialogButtonObjectList)
			{
				if (buttonObject != null && buttonObject.Key == key)
				{
					return buttonObject;
				}
			}
			return null;
		}

		public virtual void Initialize(Dictionary<string, Action> actionDic, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true)
		{
			base.Initialize(dialogSize, allowCloseExternal);
			Actions = actionDic ?? new Dictionary<string, Action>();
			BindActions(Actions);
		}

		public void Initialize(string messageBodyKey, Dictionary<string, string> labelKeyDic, Dictionary<string, Action> actionDic, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true)
		{
			base.Initialize(dialogSize, allowCloseExternal);
			Actions = actionDic ?? new Dictionary<string, Action>();
			ApplyButtonLabels(labelKeyDic);
			BindActions(Actions);
			SetMessageBodyTextInterface();
			if (!string.IsNullOrEmpty(messageBodyKey))
			{
				SetWordingText(messageBodyKey);
			}
		}

		public void Setup(string messageBodyKey, Dictionary<string, string> labelKeyDic, Dictionary<string, Action> actionDic)
		{
			Initialize(messageBodyKey, labelKeyDic, actionDic);
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

		public override void Open()
		{
			base.Open();
		}

		protected override void OnCloseExternal()
		{
			base.OnCloseExternal();
			Close();
		}

		private void SetMessageBodyTextInterface()
		{
			if (messageBodyTextInterFace != null)
			{
				return;
			}

			messageBodyTextInterFace = messageBodyTextMesh;
		}

		private void ApplyButtonLabels(Dictionary<string, string> labelKeyDic)
		{
			if (dialogButtonObjectList == null || labelKeyDic == null)
			{
				return;
			}

			foreach (var buttonObject in dialogButtonObjectList)
			{
				if (buttonObject == null || buttonObject.DialogButtonLabelMesh == null)
				{
					continue;
				}
				if (labelKeyDic.TryGetValue(buttonObject.Key, out var labelKey) && !string.IsNullOrEmpty(labelKey))
				{
					buttonObject.DialogButtonLabelMesh.SetWordingText(labelKey);
				}
			}
		}

		private void BindActions(Dictionary<string, Action> actionDic)
		{
			if (dialogButtonObjectList == null || actionDic == null)
			{
				return;
			}

			foreach (var buttonObject in dialogButtonObjectList)
			{
				if (buttonObject == null || buttonObject.DialogButton == null)
				{
					continue;
				}
				if (actionDic.TryGetValue(buttonObject.Key, out var action))
				{
					BindButton(buttonObject.DialogButton, action);
				}
				else
				{
					Debug.LogErrorFormat("CommonMultiButtonDialog action key was not found: {0}", buttonObject.Key);
				}
			}
		}

		private void BindButton(CustomButton button, Action action)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => OnClickButton(action));
		}

		private void OnClickButton(Action action)
		{
			action?.Invoke();
			Close();
		}
	}
}
