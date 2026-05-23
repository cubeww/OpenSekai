using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Beebyte.Obfuscator;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.UI
{
	public class ListView : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler
	{
		private delegate void SetListViewItem(ListViewItem listViewItem, int dataIndex);

		[SerializeField]
		protected CustomScrollRect scrollRect;

		[SerializeField]
		private float horizontalSpacing;

		[SerializeField]
		protected float verticalSpacing;

		[SerializeField]
		protected RectOffset padding;

		[SerializeField]
		private int rowCount;

		[SerializeField]
		protected int columnCount;

		[SerializeField]
		private bool snap;

		[SerializeField]
		protected float snapSpeed;

		[SerializeField]
		private bool offSnapTypeCalc;

		private Vector2 beginDragPos;

		private IEnumerator snapCoroutine;

		private float snapTargetPosOffset;

		protected float currectSnapPosOffset;

		[SerializeField]
		private ListViewItem listViewItemPrefab;

		protected List<ListViewItem> listViewItems;

		protected int firstIndexNow;

		protected float oneViewItemSizeX;

		protected float oneViewItemSizeY;

		protected int dataNum;

		private SetListViewItem setListViewItem;

		private MonoBehaviour coroutineExecutor;

		private bool _isFastMode;

		public float HorizontalSpacing
		{
			get
			{
				return horizontalSpacing;
			}
			set
			{
				horizontalSpacing = value;
			}
		}

		public float VerticalSpacing
		{
			get
			{
				return verticalSpacing;
			}
			set
			{
				verticalSpacing = value;
			}
		}

		public ListViewItem ListViewItemPrefab
		{
			get
			{
				return listViewItemPrefab;
			}
			set
			{
				listViewItemPrefab = value;
			}
		}

		protected int needViewItemNum { get; set; }

		public Action<ListViewItem> OnInstantiate { get; set; }

		public Action<ListViewItem, int> OnCreateCell { get; set; }

		public Action OnExecuteSnapStart { get; set; }

		public Action OnExecuteSnapEnd { get; set; }

		public MonoBehaviour CoroutineExecutor
		{
			get
			{
				return coroutineExecutor != null ? coroutineExecutor : this;
			}
			set
			{
				coroutineExecutor = value;
			}
		}

		public CustomScrollRect ScrollRect
		{
			get
			{
				return scrollRect;
			}
			set
			{
				scrollRect = value;
				Initialize();
			}
		}

		public int DataNum
		{
			get
			{
				return dataNum;
			}
		}

		public RectOffset Padding
		{
			get
			{
				padding ??= new RectOffset();
				return padding;
			}
			set
			{
				padding = value;
			}
		}

		public int RowCount
		{
			get
			{
				return Mathf.Max(1, rowCount);
			}
			set
			{
				rowCount = Mathf.Max(1, value);
			}
		}

		public int ColumnCount
		{
			get
			{
				return Mathf.Max(1, columnCount);
			}
			set
			{
				columnCount = Mathf.Max(1, value);
			}
		}

		public float SnapTargetPosOffset
		{
			get
			{
				return snapTargetPosOffset;
			}
			set
			{
				snapTargetPosOffset = value;
			}
		}

		public void SetPaddingTargetFittingWidth(RectTransform targetRect)
		{
			if (targetRect == null || scrollRect == null || scrollRect.viewport == null)
			{
				return;
			}

			int side = Mathf.Max(0, Mathf.RoundToInt((scrollRect.viewport.rect.width - targetRect.rect.width) * 0.5f));
			Padding.left = side;
			Padding.right = side;
		}

		public void SetPaddingTargetFittingHeight(RectTransform targetRect)
		{
			if (targetRect == null || scrollRect == null || scrollRect.viewport == null)
			{
				return;
			}

			int side = Mathf.Max(0, Mathf.RoundToInt((scrollRect.viewport.rect.height - targetRect.rect.height) * 0.5f));
			Padding.top = side;
			Padding.bottom = side;
		}

		public void SetContentFirstIndex(int firstIndex, bool snapSkip = false)
		{
			SetContentPosition(firstIndex);
			ResetScrollRectVelocity();
			SetLoopListView();
			StartSnap(snapSkip);
		}

		private void SetContentPosition(int firstIndex)
		{
			SetContentPosition(GetContentPosition(firstIndex), true);
		}

		public virtual Vector2 GetContentPosition(int firstIndex, float offsetPos = 0f)
		{
			if (scrollRect == null || scrollRect.content == null)
			{
				return Vector2.zero;
			}

			firstIndex = Mathf.Clamp(firstIndex, 0, Mathf.Max(0, dataNum - 1));
			if (IsHorizontal())
			{
				float x = offsetPos - oneViewItemSizeX * (firstIndex / RowCount) - Padding.left;
				if (firstIndex == 0)
				{
					x = Padding.left + offsetPos;
				}
				x = Mathf.Clamp(x, GetContentPosMinHorizontal(), 0f);
				return new Vector2(x, scrollRect.content.anchoredPosition.y);
			}

			if (IsVertical())
			{
				float y = offsetPos + oneViewItemSizeY * (firstIndex / ColumnCount) + Padding.top;
				if (firstIndex == 0)
				{
					y = offsetPos - Padding.top;
				}
				y = Mathf.Clamp(y, 0f, GetContentPosMaxVertical());
				return new Vector2(scrollRect.content.anchoredPosition.x, y);
			}

			return Vector2.zero;
		}

		public virtual int GetContentFirstIndex()
		{
			return GetFirstIndex();
		}

		public virtual int GetContentCenterIndex(float offsetPos = 0f)
		{
			if (IsHorizontal() && scrollRect != null && scrollRect.viewport != null)
			{
				float centered = -GetContentPosition().x + scrollRect.viewport.rect.width * 0.5f + offsetPos;
				return Mathf.Clamp(Mathf.FloorToInt(centered / Mathf.Max(1f, oneViewItemSizeX)) * RowCount, 0, Mathf.Max(0, dataNum - 1));
			}

			if (IsVertical() && scrollRect != null && scrollRect.viewport != null)
			{
				float centered = GetContentPosition().y + scrollRect.viewport.rect.height * 0.5f + offsetPos;
				return Mathf.Clamp(Mathf.FloorToInt(centered / Mathf.Max(1f, oneViewItemSizeY)) * ColumnCount, 0, Mathf.Max(0, dataNum - 1));
			}

			return GetContentFirstIndex();
		}

		private void SetListViewData()
		{
			setListViewItem = IsHorizontal() ? SetListViewItemHorizontal : SetListViewItemVertical;
		}

		public void SetContentPosition(Vector2 pos, bool snapSkip = false)
		{
			if (scrollRect != null && scrollRect.content != null)
			{
				scrollRect.content.anchoredPosition = pos;
			}
		}

		public Vector2 GetContentPosition()
		{
			return scrollRect != null && scrollRect.content != null ? scrollRect.content.anchoredPosition : Vector2.zero;
		}

		public Vector2 GetContentSizeDelta()
		{
			return scrollRect != null && scrollRect.content != null ? scrollRect.content.sizeDelta : Vector2.zero;
		}

		public void SetClampPosition(bool snapSkip = false)
		{
			if (scrollRect == null || scrollRect.content == null)
			{
				return;
			}

			Vector2 pos = scrollRect.content.anchoredPosition;
			if (IsHorizontal())
			{
				pos.x = Mathf.Clamp(pos.x, GetContentPosMinHorizontal(), 0f);
			}
			else if (IsVertical())
			{
				pos.y = Mathf.Clamp(pos.y, 0f, GetContentPosMaxVertical());
			}

			SetContentPosition(pos, snapSkip);
		}

		private void SetContentSize()
		{
			if (scrollRect == null || scrollRect.content == null)
			{
				return;
			}

			if (IsHorizontal())
			{
				float count = Mathf.Ceil((float)dataNum / RowCount);
				float width = oneViewItemSizeX * count + Padding.left + Padding.right;
				if (count > 0f)
				{
					width -= horizontalSpacing;
				}
				scrollRect.content.sizeDelta = new Vector2(width, scrollRect.content.sizeDelta.y);
			}
			else if (IsVertical())
			{
				float count = Mathf.Ceil((float)dataNum / ColumnCount);
				float height = oneViewItemSizeY * count + Padding.top + Padding.bottom;
				if (count > 0f)
				{
					height -= verticalSpacing;
				}
				scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, height);
			}

			UpdateEnabledScroll();
		}

		protected void UpdateEnabledScroll()
		{
			if (scrollRect == null)
			{
				return;
			}

			bool enabled = EnableScroll();
			scrollRect.enabled = enabled;
			scrollRect.horizontal = IsHorizontal();
			scrollRect.vertical = IsVertical();
		}

		public bool EnableScroll()
		{
			if (scrollRect == null || scrollRect.viewport == null || scrollRect.content == null)
			{
				return false;
			}

			if (IsHorizontal())
			{
				return scrollRect.content.rect.width > scrollRect.viewport.rect.width;
			}

			if (IsVertical())
			{
				return scrollRect.content.rect.height > scrollRect.viewport.rect.height;
			}

			return false;
		}

		protected void ResetScrollRectVelocity()
		{
			if (scrollRect != null)
			{
				scrollRect.velocity = Vector2.zero;
			}
		}

		public void CreateViewItem(int dataNum, bool isSnap = true, bool isFastMode = false, bool isSetFirstIndex = true)
		{
			Initialize();
			_isFastMode = isFastMode;
			this.dataNum = Mathf.Max(0, dataNum);
			if (listViewItemPrefab != null)
			{
				oneViewItemSizeX = listViewItemPrefab.SizeX + horizontalSpacing;
				oneViewItemSizeY = listViewItemPrefab.SizeY + verticalSpacing;
			}

			SetContentSize();
			SetListViewData();
			CheckData();
			needViewItemNum = this.dataNum;
			EnsureItemCount(needViewItemNum);
			RefreshListViewItemAll(isSetFirstIndex);
			if (isSnap)
			{
				StartSnap(false);
			}
		}

		public UniTask CreateViewItemAsync(int dataCount, int frameCount, CancellationToken ctx, bool isSnap = false)
		{
			CreateViewItem(dataCount, isSnap);
			return UniTask.CompletedTask;
		}

		public void AddViewItem(int addDataNum)
		{
			CreateViewItem(dataNum + addDataNum, false, _isFastMode, false);
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		protected virtual void OnInstantiateCallback(ListViewItem listViewItem)
		{
			OnInstantiate?.Invoke(listViewItem);
		}

		protected virtual void OnCreateCellCallback(ListViewItem listViewItem, int dataIndex)
		{
			OnCreateCell?.Invoke(listViewItem, dataIndex);
		}

		protected virtual void Initialize()
		{
			listViewItems ??= new List<ListViewItem>();
			padding ??= new RectOffset();
			if (scrollRect == null)
			{
				scrollRect = GetComponentInChildren<CustomScrollRect>(true);
			}

			if (scrollRect != null)
			{
				scrollRect.onValueChanged.RemoveListener(OnValueChangedListView);
				scrollRect.onValueChanged.AddListener(OnValueChangedListView);
			}
		}

		protected void RefreshListViewItemAll(bool isSetFirstIndex = true)
		{
			if (isSetFirstIndex)
			{
				firstIndexNow = 0;
			}

			for (int i = 0; i < listViewItems.Count; i++)
			{
				ListViewItem item = listViewItems[i];
				if (item == null)
				{
					continue;
				}

				bool active = i < dataNum;
				item.gameObject.SetActive(active);
				if (active)
				{
					setListViewItem?.Invoke(item, i);
				}
			}
		}

		protected virtual void CheckData()
		{
			RowCount = rowCount;
			ColumnCount = columnCount;
		}

		protected virtual void SetListViewItemHorizontal(ListViewItem listViewItem, int dataIndex)
		{
			if (listViewItem == null)
			{
				return;
			}

			RectTransform rt = listViewItem.transform as RectTransform;
			if (rt != null)
			{
				rt.anchoredPosition = new Vector2(oneViewItemSizeX * (dataIndex / RowCount) + Padding.left, -oneViewItemSizeY * (dataIndex % RowCount) - Padding.top);
				listViewItem.SettingAnchoredPosition = rt.anchoredPosition;
			}

			listViewItem.SetData(dataIndex);
			OnCreateCellCallback(listViewItem, dataIndex);
		}

		protected virtual void SetListViewItemVertical(ListViewItem listViewItem, int dataIndex)
		{
			if (listViewItem == null)
			{
				return;
			}

			RectTransform rt = listViewItem.transform as RectTransform;
			if (rt != null)
			{
				rt.anchoredPosition = new Vector2(oneViewItemSizeX * (dataIndex % ColumnCount) + Padding.left, -oneViewItemSizeY * (dataIndex / ColumnCount) - Padding.top);
				listViewItem.SettingAnchoredPosition = rt.anchoredPosition;
			}

			listViewItem.SetData(dataIndex);
			OnCreateCellCallback(listViewItem, dataIndex);
		}

		[Skip]
		public virtual void OnValueChangedListView(Vector2 d)
		{
			SetLoopListView();
		}

		private void SetLoopListView()
		{
			firstIndexNow = GetFirstIndex();
		}

		protected virtual int GetFirstIndex()
		{
			if (dataNum <= 0)
			{
				return 0;
			}

			Vector2 pos = GetContentPosition();
			if (IsHorizontal())
			{
				return Mathf.Clamp(Mathf.FloorToInt(Mathf.Max(0f, -pos.x - Padding.left) / Mathf.Max(1f, oneViewItemSizeX)) * RowCount, 0, dataNum - 1);
			}

			if (IsVertical())
			{
				return Mathf.Clamp(Mathf.FloorToInt(Mathf.Max(0f, pos.y - Padding.top) / Mathf.Max(1f, oneViewItemSizeY)) * ColumnCount, 0, dataNum - 1);
			}

			return 0;
		}

		protected bool IsHorizontal()
		{
			return scrollRect != null && scrollRect.horizontal && !scrollRect.vertical;
		}

		protected bool IsVertical()
		{
			return scrollRect == null || scrollRect.vertical;
		}

		protected virtual int GetListViewIndex(int dataIndex)
		{
			return dataIndex;
		}

		private float GetOneViewItemSizeX()
		{
			return oneViewItemSizeX;
		}

		private float GetOneViewItemSizeY()
		{
			return oneViewItemSizeY;
		}

		protected virtual int GetNeedViewItemNumHorizontal()
		{
			return dataNum;
		}

		protected virtual int GetNeedViewItemNumVertical()
		{
			return dataNum;
		}

		protected int GetMaxViewItemNumHorizontal()
		{
			return dataNum;
		}

		protected int GetMaxViewItemNumVertical()
		{
			return dataNum;
		}

		private float GetContentPosMinHorizontal()
		{
			if (scrollRect == null || scrollRect.content == null || scrollRect.viewport == null)
			{
				return 0f;
			}

			return Mathf.Min(0f, scrollRect.viewport.rect.width - scrollRect.content.rect.width);
		}

		private float GetContentPosMaxVertical()
		{
			if (scrollRect == null || scrollRect.content == null || scrollRect.viewport == null)
			{
				return 0f;
			}

			return Mathf.Max(0f, scrollRect.content.rect.height - scrollRect.viewport.rect.height);
		}

		public void StartSnap(bool skip = false)
		{
			if (!snap || skip)
			{
				return;
			}

			StopSnap();
			ExecuteSnapStartAction();
			snapCoroutine = SnapListView(Vector2.zero, skip);
			CoroutineExecutor.StartCoroutine(snapCoroutine);
		}

		public void StopSnap()
		{
			if (snapCoroutine != null && CoroutineExecutor != null)
			{
				CoroutineExecutor.StopCoroutine(snapCoroutine);
			}
			snapCoroutine = null;
		}

		[Skip]
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			beginDragPos = scrollRect != null && scrollRect.content != null ? scrollRect.content.anchoredPosition : Vector2.zero;
			StopSnap();
		}

		[Skip]
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			Vector2 current = scrollRect != null && scrollRect.content != null ? scrollRect.content.anchoredPosition : Vector2.zero;
			StartCoroutine(SnapListView(current - beginDragPos));
		}

		private IEnumerator SnapListView(Vector2 dragDelta, bool skip = false)
		{
			SetClampPosition(skip);
			ExecuteSnapEndAction();
			yield break;
		}

		protected virtual float GetSnapTargetPosX(float checkPosX)
		{
			return checkPosX;
		}

		protected virtual float GetSnapTargetPosY(float checkPosY)
		{
			return checkPosY;
		}

		protected virtual void ExecuteSnapStartAction()
		{
			OnExecuteSnapStart?.Invoke();
		}

		protected virtual void ExecuteSnapEndAction()
		{
			OnExecuteSnapEnd?.Invoke();
		}

		public void OnExcuteAllCell(Action<ListViewItem, int> onExcuteAllCell, bool isSetFirstIndex = true)
		{
			if (listViewItems == null)
			{
				return;
			}

			for (int i = 0; i < listViewItems.Count && i < dataNum; i++)
			{
				if (listViewItems[i] != null)
				{
					onExcuteAllCell?.Invoke(listViewItems[i], i);
				}
			}
		}

		public void OnExcutePickCell(int index, Action<ListViewItem, int> onExcutePickCell)
		{
			if (listViewItems == null || index < 0 || index >= listViewItems.Count || index >= dataNum)
			{
				return;
			}

			onExcutePickCell?.Invoke(listViewItems[index], index);
		}

		[Skip]
		public void Clear()
		{
			if (listViewItems != null)
			{
				foreach (ListViewItem item in listViewItems)
				{
					if (item != null)
					{
						Destroy(item.gameObject);
					}
				}

				listViewItems.Clear();
			}

			dataNum = 0;
			needViewItemNum = 0;
		}

		public ListViewItem GetFirstItem()
		{
			return listViewItems != null && listViewItems.Count > 0 ? listViewItems[0] : null;
		}

		private void EnsureItemCount(int count)
		{
			if (listViewItems == null || listViewItemPrefab == null || scrollRect == null || scrollRect.content == null)
			{
				return;
			}

			for (int i = listViewItems.Count; i < count; i++)
			{
				ListViewItem item = Instantiate(listViewItemPrefab, scrollRect.content);
				item.gameObject.SetActive(true);
				listViewItems.Add(item);
				OnInstantiateCallback(item);
			}

			for (int i = count; i < listViewItems.Count; i++)
			{
				if (listViewItems[i] != null)
				{
					listViewItems[i].gameObject.SetActive(false);
				}
			}
		}

		public ListView()
		{
			padding = new RectOffset();
			rowCount = 1;
			columnCount = 1;
			snapSpeed = 1f;
			listViewItems = new List<ListViewItem>();
		}
	}
}
