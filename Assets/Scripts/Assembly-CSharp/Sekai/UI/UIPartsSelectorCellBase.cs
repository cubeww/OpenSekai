using System;
using UnityEngine;

namespace Sekai.UI
{
	public abstract class UIPartsSelectorCellBase<TViewData> : MonoBehaviour where TViewData : UIPartsSelectorCellBase<TViewData>.ViewDataBase
	{
		public abstract class ViewDataBase
		{
			public int Number;

			public string Title;

			public bool IsSelected;

			public bool ShowsLine;

			public bool ShowsBadge;

			public bool ShowsSelectBanner;

			public string SelectBannerName;

			public bool ShowsBannerCover;

			public bool IsEnabled;

			public int Index { get; }

			public ViewDataBase(int index)
			{
				Index = index;
				IsEnabled = true;
			}
		}

		[SerializeField]
		protected CustomTextMesh numberText;

		[SerializeField]
		protected CustomButton button;

		[SerializeField]
		protected GraphicButtonTapEffect _tapEffect;

		[SerializeField]
		protected CustomImage _icon;

		[SerializeField]
		protected GameObject selectedObj;

		[SerializeField]
		protected GameObject lineObj;

		[SerializeField]
		protected GameObject badgeObj;

		[SerializeField]
		protected Canvas bannerCanvas;

		[SerializeField]
		protected GameObject selectBannerObj;

		[SerializeField]
		protected CustomTextMesh selectBannerText;

		[SerializeField]
		protected GameObject selectBannerCover;

		protected int viewNumber;

		protected TViewData _viewData;

		protected Action<bool> onSetSelected;

		public CustomTextMesh NumberText
		{
			get
			{
				return numberText;
			}
		}

		public bool IsSelected
		{
			get
			{
				return _viewData != null && _viewData.IsSelected;
			}
		}

		public void SetCallback(Action<int> onClick)
		{
			if (button == null)
			{
				return;
			}

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() =>
			{
				if (_viewData != null)
				{
					onClick?.Invoke(_viewData.Index);
				}
			});
		}

		public virtual void Setup(TViewData viewData)
		{
			_viewData = viewData;
			if (_viewData == null)
			{
				SetActive(false);
				return;
			}

			SetActive(true);
			SetView(_viewData.Number);
			SetTitle();
			SetSelected(_viewData.IsSelected);
			ShowsLine(_viewData.ShowsLine);
			ShowsBadge(_viewData.ShowsBadge);
			ShowsSelectBanner(_viewData.ShowsSelectBanner, _viewData.ShowsBannerCover, _viewData.SelectBannerName);
			SetEnabled(_viewData.IsEnabled);
		}

		public void SetView(int viewNumber)
		{
			this.viewNumber = viewNumber;
			if (numberText != null)
			{
				numberText.SetText(viewNumber.ToString("00"));
			}
		}

		public virtual void SetSelected(bool isSelected)
		{
			if (_viewData != null)
			{
				_viewData.IsSelected = isSelected;
			}

			if (selectedObj != null)
			{
				selectedObj.SetActive(isSelected);
			}

			if (numberText != null)
			{
				numberText.color = GetTextColor(isSelected);
			}

			SetIconSelected(isSelected);
			onSetSelected?.Invoke(isSelected);
		}

		private void SetIconSelected(bool isSelected)
		{
			if (_icon == null)
			{
				return;
			}

			_icon.color = GetIconColor(isSelected);
			if (_tapEffect != null)
			{
				_tapEffect.SetDefaultColor(_icon.color);
				_tapEffect.SetEffectColor(GetIconColor(!isSelected));
			}
		}

		public void ShowsLine(bool shows)
		{
			if (lineObj != null)
			{
				lineObj.SetActive(shows);
			}
		}

		public TViewData GetViewData()
		{
			return _viewData;
		}

		protected abstract Color GetTextColor(bool isSelected);

		protected virtual Color GetIconColor(bool isSelected)
		{
			return GetTextColor(isSelected);
		}

		protected virtual void SetTitle()
		{
			SetText(_viewData != null ? _viewData.Title : string.Empty);
		}

		public void SetText(string text)
		{
			if (numberText != null)
			{
				numberText.SetText(text);
			}
		}

		private void ShowsBadge(bool shows)
		{
			if (badgeObj != null)
			{
				badgeObj.SetActive(shows);
			}
		}

		public void SetEnabled(bool isEnabled)
		{
			if (button != null)
			{
				button.interactable = isEnabled;
			}
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}

		private void ShowsSelectBanner(bool bannerShows, bool coverShows, string bannerName = "")
		{
			if (selectBannerObj != null)
			{
				selectBannerObj.SetActive(bannerShows);
			}

			if (selectBannerCover != null)
			{
				selectBannerCover.SetActive(coverShows);
			}

			if (selectBannerText != null)
			{
				selectBannerText.SetText(bannerName ?? string.Empty);
			}

			if (bannerCanvas != null)
			{
				bannerCanvas.enabled = bannerShows || coverShows;
			}
		}

		protected UIPartsSelectorCellBase()
		{
		}
	}
}
