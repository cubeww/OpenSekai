using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.UI
{
	[AddComponentMenu("Custom UI/Custom Toggle")]
	public class CustomToggle : Toggle
	{
		public enum DisableActionType
		{
			None = 0,
			Grayout = 1
		}

		private readonly Lazy<Color> disableColor;

		[SerializeField]
		protected SeType se;

		[SerializeField]
		private InputManager.IntervalUseType interval;

		[SerializeField]
		private DisableActionType disableActionType;

		[SerializeField]
		private Graphic coverImage;

		[SerializeField]
		private List<Graphic> optionalCoverImages;

		[SerializeField]
		private ButtonViewInteractionBase interaction;

		[SerializeField]
		private CustomTextMesh _captionText;

		public Action onPointerClickAction;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (!interactable && disableActionType == DisableActionType.Grayout)
			{
				ShowCover(disableColor?.Value ?? Color.gray);
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
		}

		[Skip]
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (group != null && isOn)
			{
				return;
			}

			if (CustomSelectableDefine.CheckPointerClickAction(this, interval, se))
			{
				base.OnPointerClick(eventData);
				onPointerClickAction?.Invoke();
			}
		}

		private void ShowCover(Color color)
		{
			if (coverImage != null)
			{
				coverImage.color = color;
				coverImage.gameObject.SetActive(true);
			}
			if (optionalCoverImages == null)
			{
				return;
			}
			for (int i = 0; i < optionalCoverImages.Count; i++)
			{
				if (optionalCoverImages[i] != null)
				{
					optionalCoverImages[i].color = color;
					optionalCoverImages[i].gameObject.SetActive(true);
				}
			}
		}

		public void HideCover()
		{
			if (coverImage != null)
			{
				coverImage.gameObject.SetActive(false);
			}
			if (optionalCoverImages == null)
			{
				return;
			}
			for (int i = 0; i < optionalCoverImages.Count; i++)
			{
				if (optionalCoverImages[i] != null)
				{
					optionalCoverImages[i].gameObject.SetActive(false);
				}
			}
		}

		public void SetOptionalCoverImage(int index)
		{
			if (optionalCoverImages == null)
			{
				return;
			}
			for (int i = 0; i < optionalCoverImages.Count; i++)
			{
				if (optionalCoverImages[i] != null)
				{
					optionalCoverImages[i].gameObject.SetActive(i == index);
				}
			}
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
		}

		public void SetCaptionText(string caption)
		{
			if (_captionText != null)
			{
				_captionText.text = caption;
			}
		}

		public CustomToggle()
		{
			disableColor = new Lazy<Color>(() => Color.gray);
		}
	}
}
