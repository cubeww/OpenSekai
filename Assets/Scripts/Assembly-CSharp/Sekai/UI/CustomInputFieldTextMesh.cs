using System;
using Beebyte.Obfuscator;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.UI
{
	[AddComponentMenu("Custom UI/Custom InputField TextMesh")]
	public class CustomInputFieldTextMesh : TMP_InputField
	{
		[SerializeField]
		protected SeType se;

		[SerializeField]
		private InputManager.IntervalUseType interval;

		[SerializeField]
		protected bool checkEmoji;

		[SerializeField]
		protected bool allowEmpty;

		[Header("NGワードが含まれていたら弾くか")]
		[SerializeField]
		private bool _rejectIfContainsNgWord;

		private InputManager.ControlState controlState;

		public Action OnEndEditErrorDialogOnClickOK;

		public Action<string> OnEndEditSuccess;

		private Action OnSelectCallback;

		private string _validatedText;

		protected override void Awake()
		{
			base.Awake();
			onEndEdit.RemoveListener(OnEndEdit);
			onEndEdit.AddListener(OnEndEdit);
			onValueChanged.RemoveListener(OnValueChangedInternal);
			onValueChanged.AddListener(OnValueChangedInternal);
			_validatedText = text;
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			OnSelectCallback?.Invoke();
		}

		public void SetOnSelect(Action OnSelect)
		{
			OnSelectCallback = OnSelect;
		}

		protected char ValidateInput(string text, int charIndex, char addedChar)
		{
			return addedChar;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
		}

		[Skip]
		public void OnEndEdit(string str)
		{
			if (!allowEmpty && string.IsNullOrEmpty(str))
			{
				OnEndEditErrorDialogOnClickOK?.Invoke();
				return;
			}
			ApplyValidatedTextIfValid(str);
			OnEndEditSuccess?.Invoke(str);
		}

		[Skip]
		public override void OnPointerClick(PointerEventData eventData)
		{
			base.OnPointerClick(eventData);
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			base.OnPointerExit(eventData);
		}

		private void OnFinishedControlSelectable()
		{
		}

		protected override void OnDestroy()
		{
			onEndEdit.RemoveListener(OnEndEdit);
			onValueChanged.RemoveListener(OnValueChangedInternal);
			base.OnDestroy();
		}

		public void SetAllowEmpty(bool isAllow)
		{
			allowEmpty = isAllow;
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}

		private void OnValueChangedInternal(string currentText)
		{
			_validatedText = currentText;
		}

		private void ApplyValidatedTextIfValid(string target)
		{
			_validatedText = target;
		}

		public CustomInputFieldTextMesh()
		{
		}
	}
}
