using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sekai.UI
{
	public abstract class UIPartsLeftTabListCellBase : MonoBehaviour
	{
		[Serializable]
		public abstract class ViewData
		{
			[SerializeField]
			public ViewData[] SubTabListViewData;

			public int Id
			{
				[CompilerGenerated]
				get
				{
					return _Id;
				}
			}

			public bool IsSelected
			{
				[CompilerGenerated]
				get
				{
					return _IsSelected;
				}
				[CompilerGenerated]
				set
				{
					_IsSelected = value;
				}
			}

			public bool IsLocked
			{
				[CompilerGenerated]
				get
				{
					return _IsLocked;
				}
				[CompilerGenerated]
				set
				{
					_IsLocked = value;
				}
			}

			public bool IsLast
			{
				[CompilerGenerated]
				get
				{
					return _IsLast;
				}
				[CompilerGenerated]
				set
				{
					_IsLast = value;
				}
			}

			public bool IsShowBadge
			{
				[CompilerGenerated]
				get
				{
					return _IsShowBadge;
				}
				[CompilerGenerated]
				set
				{
					_IsShowBadge = value;
				}
			}

			public bool Interactive
			{
				[CompilerGenerated]
				get
				{
					return _Interactive;
				}
				[CompilerGenerated]
				set
				{
					_Interactive = value;
				}
			}

			public Action<ViewData> OnSelectEvent
			{
				[CompilerGenerated]
				get
				{
					return _OnSelectEvent;
				}
				[CompilerGenerated]
				set
				{
					_OnSelectEvent = value;
				}
			}

			public Action<ViewData> OnLockedEvent
			{
				[CompilerGenerated]
				get
				{
					return _OnLockedEvent;
				}
				[CompilerGenerated]
				set
				{
					_OnLockedEvent = value;
				}
			}

			public bool IsDefault
			{
				get
				{
					return !IsSub && !HasSubData;
				}
			}

			public bool HasSubData
			{
				get
				{
					return SubTabListViewData != null && SubTabListViewData.Length > 0;
				}
			}

			public bool IsSub
			{
				[CompilerGenerated]
				get
				{
					return _IsSub;
				}
			}

			public bool IsDisplayingSubTab
			{
				[CompilerGenerated]
				get
				{
					return _IsDisplayingSubTab;
				}
				[CompilerGenerated]
				set
				{
					_IsDisplayingSubTab = value;
				}
			}

			public bool ShowsLockBalloon
			{
				[CompilerGenerated]
				get
				{
					return _ShowsLockBalloon;
				}
				[CompilerGenerated]
				set
				{
					_ShowsLockBalloon = value;
				}
			}

			public string LockBalloonText
			{
				[CompilerGenerated]
				get
				{
					return _LockBalloonText;
				}
				[CompilerGenerated]
				set
				{
					_LockBalloonText = value;
				}
			}

			[CompilerGenerated]
			private int _Id;

			[CompilerGenerated]
			private bool _IsSelected;

			[CompilerGenerated]
			private bool _IsLocked;

			[CompilerGenerated]
			private bool _IsLast;

			[CompilerGenerated]
			private bool _IsShowBadge;

			[CompilerGenerated]
			private bool _Interactive;

			[CompilerGenerated]
			private Action<ViewData> _OnSelectEvent;

			[CompilerGenerated]
			private Action<ViewData> _OnLockedEvent;

			[CompilerGenerated]
			private bool _IsSub;

			[CompilerGenerated]
			private bool _IsDisplayingSubTab;

			[CompilerGenerated]
			private bool _ShowsLockBalloon;

			[CompilerGenerated]
			private string _LockBalloonText;

			public void OnSelect()
			{
				OnSelectEvent?.Invoke(this);
			}

			public void OnLocked()
			{
				OnLockedEvent?.Invoke(this);
			}

			protected ViewData(ViewData[] subTabData = null, bool isSub = false)
			{
				Interactive = true;
				SubTabListViewData = subTabData;
				_IsSub = isSub;
			}

			protected ViewData(int id, ViewData[] subTabData = null, bool isSub = false)
			{
				Interactive = true;
				_Id = id;
				SubTabListViewData = subTabData;
				_IsSub = isSub;
			}

			public bool ContainsSubData(ViewData subData)
			{
				if (!HasSubData)
				{
					return false;
				}

				foreach (var data in SubTabListViewData)
				{
					if (data == subData)
					{
						return true;
					}
				}

				return false;
			}
		}

		[SerializeField]
		protected CustomButton button;

		[SerializeField]
		protected UIPartsLeftTabListCellView cellView;

		protected ViewData viewData;

		public void Setup(ViewData data)
		{
			viewData = data;
			SetView();
			SetCallback();
		}

		protected virtual void SetView()
		{
			if (viewData == null || cellView == null)
			{
				return;
			}

			cellView.SetLocked(viewData.IsLocked);
			SetSelected(viewData.IsSelected);
			SetLine();
			SetAttentionBadge();
			SetInteractive();
			SetSubTabObject();
		}

		protected void SetText(string name)
		{
			cellView?.SetText(name);
		}

		protected void SetActiveText(bool isActive)
		{
			cellView?.SetActiveText(isActive);
		}

		private void SetLine()
		{
			if (viewData != null)
			{
				cellView?.SetLine(viewData.IsSub);
			}
		}

		protected void SetAttentionBadge()
		{
			if (viewData != null)
			{
				cellView?.SetAttentionBadge(viewData.IsShowBadge);
			}
		}

		private void SetInteractive()
		{
			if (button != null && viewData != null)
			{
				button.interactable = viewData.Interactive;
			}
		}

		protected virtual void SetSubTabObject()
		{
			if (viewData != null)
			{
				cellView?.SetSubTabObject(viewData.HasSubData, viewData.IsSelected, viewData.IsDisplayingSubTab);
			}
		}

		private void SetCallback()
		{
			if (button == null)
			{
				return;
			}

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(OnClick);
		}

		protected virtual void SetSelected(bool isSelected)
		{
			cellView?.SetSelected(isSelected);
		}

		protected void SetLocked(bool isLocked)
		{
			cellView?.SetLocked(isLocked);
		}

		protected virtual void OnClick()
		{
			if (viewData == null)
			{
				return;
			}

			if (viewData.IsLocked)
			{
				viewData.OnLocked();
				cellView?.SetLockBalloon(viewData.ShowsLockBalloon, viewData.LockBalloonText);
				return;
			}

			viewData.OnSelect();
		}

		protected UIPartsLeftTabListCellBase()
		{
		}
	}
}
