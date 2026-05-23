using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sekai.UI
{
	public abstract class UIPartsLeftTabListBase : MonoBehaviour, IUIPartsLeftTabList
	{
		[SerializeField]
		private FixedSizeReuseScroller scroller;

		[SerializeField]
		private CustomImage topScrollableIcon;

		[SerializeField]
		private CustomImage bottomScrollableIcon;

		protected UIPartsLeftTabListCellBase.ViewData[] cellDataList;

		private Action<UIPartsLeftTabListCellBase, int> OnSetupCell;

		[SerializeField]
		private CustomScrollRect scrollRect;

		private CancellationTokenSource _updateScrollIconCts;

		public Action<UIPartsLeftTabListCellBase.ViewData> OnSelectCell
		{
			[CompilerGenerated]
			get
			{
				return _OnSelectCell;
			}
			[CompilerGenerated]
			set
			{
				_OnSelectCell = value;
			}
		}

		public Action OnSelectedCell
		{
			[CompilerGenerated]
			get
			{
				return _OnSelectedCell;
			}
			[CompilerGenerated]
			set
			{
				_OnSelectedCell = value;
			}
		}

		[CompilerGenerated]
		private Action<UIPartsLeftTabListCellBase.ViewData> _OnSelectCell;

		[CompilerGenerated]
		private Action _OnSelectedCell;

		private void Awake()
		{
			if (scrollRect == null && scroller != null)
			{
				scrollRect = scroller.GetComponent<CustomScrollRect>();
			}
		}

		public virtual void Initialize(UIPartsLeftTabListCellBase.ViewData[] datas, UIPartsLeftTabListCellBase.ViewData selectedViewData)
		{
			SetCellData(datas, selectedViewData);
			SetScroller();
			SetScrollRect();
		}

		public virtual void Initialize(UIPartsLeftTabListCellBase.ViewData[] dataList, int selectedId)
		{
			UIPartsLeftTabListCellBase.ViewData selectedViewData = null;
			if (dataList != null)
			{
				foreach (var data in dataList)
				{
					if (data != null && data.Id == selectedId)
					{
						selectedViewData = data;
						break;
					}
				}
			}

			Initialize(dataList, selectedViewData);
		}

		public void SetSelected(UIPartsLeftTabListCellBase.ViewData viewData)
		{
			if (cellDataList == null)
			{
				return;
			}

			foreach (var data in cellDataList)
			{
				if (data == null)
				{
					continue;
				}

				data.IsSelected = false;
				if (data.HasSubData)
				{
					foreach (var subData in data.SubTabListViewData)
					{
						if (subData != null)
						{
							subData.IsSelected = false;
						}
					}
				}
			}

			var selectTabViewData = GetSelectTabViewData(viewData);
			if (selectTabViewData != null)
			{
				selectTabViewData.IsSelected = true;
			}
		}

		private void SetCellData(UIPartsLeftTabListCellBase.ViewData[] datas, UIPartsLeftTabListCellBase.ViewData selectedViewData)
		{
			cellDataList = datas ?? Array.Empty<UIPartsLeftTabListCellBase.ViewData>();
			SetSelected(selectedViewData);
			if (cellDataList.Length > 0 && cellDataList[cellDataList.Length - 1] != null)
			{
				cellDataList[cellDataList.Length - 1].IsLast = true;
			}
		}

		private void SetScroller()
		{
			if (scroller == null)
			{
				return;
			}

			if (scroller.onUpdateItem != null)
			{
				scroller.onUpdateItem.RemoveAllListeners();
				scroller.onUpdateItem.AddListener(OnCreateCell);
			}

			scroller.Refresh(cellDataList != null ? cellDataList.Length : 0);
		}

		public void SetScrollRect()
		{
			if (scrollRect == null)
			{
				return;
			}

			scrollRect.OnEndDragEvent = SetScrollableIcon;
			scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
			scrollRect.onValueChanged.AddListener(OnScrollValueChanged);

			_updateScrollIconCts?.Cancel();
			_updateScrollIconCts?.Dispose();
			_updateScrollIconCts = new CancellationTokenSource();
			UpdateScrollIconAfterLayout(_updateScrollIconCts.Token).Forget();
		}

		private void OnDestroy()
		{
			if (scrollRect != null)
			{
				scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
			}

			_updateScrollIconCts?.Cancel();
			_updateScrollIconCts?.Dispose();
			_updateScrollIconCts = null;
		}

		private async UniTaskVoid UpdateScrollIconAfterLayout(CancellationToken cancellationToken)
		{
			if (!gameObject.activeInHierarchy)
			{
				await UniTask.WaitUntil(() => gameObject.activeInHierarchy, cancellationToken: cancellationToken);
			}

			if (!cancellationToken.IsCancellationRequested)
			{
				UpdateScrollableState();
			}
		}

		private void OnScrollValueChanged(Vector2 _)
		{
			SetScrollableIcon();
		}

		private void UpdateScrollableState()
		{
			if (scrollRect == null)
			{
				topScrollableIcon?.SetActive(false);
				bottomScrollableIcon?.SetActive(false);
				return;
			}

			var scrollable = scrollRect.Scrollable;
			topScrollableIcon?.SetActive(scrollable);
			bottomScrollableIcon?.SetActive(scrollable);
			if (scrollable)
			{
				SetScrollableIcon();
			}
		}

		private void SetScrollableIcon()
		{
			if (scrollRect == null)
			{
				return;
			}

			var reachedEdge = scrollRect.GetReachedEdge();
			if (topScrollableIcon != null)
			{
				topScrollableIcon.Alpha = reachedEdge.Top ? 0.2f : 1f;
			}

			if (bottomScrollableIcon != null)
			{
				bottomScrollableIcon.Alpha = reachedEdge.Bottom ? 0.2f : 1f;
			}
		}

		private void OnCreateCell(int dataIndex, int itemIndex, GameObject instance)
		{
			if (instance == null || cellDataList == null || dataIndex < 0 || dataIndex >= cellDataList.Length)
			{
				return;
			}

			var cell = instance.GetComponent<UIPartsLeftTabListCellBase>();
			var data = cellDataList[dataIndex];
			if (cell == null || data == null)
			{
				return;
			}

			data.OnSelectEvent = OnSelect;
			cell.Setup(data);
			OnSetupCell?.Invoke(cell, dataIndex);
		}

		private void OnSelect(UIPartsLeftTabListCellBase.ViewData viewData)
		{
			if (viewData == null)
			{
				return;
			}

			UIPartsLeftTabListCellBase.ViewData beforeSelectedData = null;
			if (cellDataList != null)
			{
				foreach (var data in cellDataList)
				{
					if (data != null && data.IsSelected)
					{
						beforeSelectedData = data;
						break;
					}
				}
			}

			if (AllowsPublishSelectEvent(beforeSelectedData, viewData))
			{
				SetSelected(viewData);
				scroller?.Refresh(false);
				OnSelectCell?.Invoke(viewData);
			}
			else
			{
				OnSelectedCell?.Invoke();
			}
		}

		private UIPartsLeftTabListCellBase.ViewData GetSelectTabViewData(UIPartsLeftTabListCellBase.ViewData viewData)
		{
			if (viewData == null || cellDataList == null)
			{
				return null;
			}

			if (viewData.IsSub)
			{
				foreach (var data in cellDataList)
				{
					if (data != null && data.ContainsSubData(viewData))
					{
						return viewData;
					}
				}
			}

			foreach (var data in cellDataList)
			{
				if (data == viewData)
				{
					return data;
				}
			}

			return null;
		}

		public void Refresh(bool resetContentPosition = false)
		{
			scroller?.Refresh(resetContentPosition);
		}

		protected abstract bool AllowsPublishSelectEvent(UIPartsLeftTabListCellBase.ViewData beforeSelectedData, UIPartsLeftTabListCellBase.ViewData viewData);

		protected UIPartsLeftTabListBase()
		{
		}
	}
}
