using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sekai
{
	[RequireComponent(typeof(ScrollRect))]
	public class FixedSizeReuseScroller : MonoBehaviour
	{
		[Serializable]
		public class UpdateItemEvent : UnityEvent<int, int, GameObject>
		{
			public UpdateItemEvent()
			{
			}
		}

		[Serializable]
		public class DisableItemEvent : UnityEvent<int, GameObject>
		{
			public DisableItemEvent()
			{
			}
		}

		public enum DirectionType
		{
			Vertical = 0,
			Horizontal = 1
		}

		[Serializable]
		public class LayoutInfo
		{
			[Min(1f)]
			public int splitCount;

			public Vector2 itemSpacing;

			public LayoutInfo()
			{
			}
		}

		[Serializable]
		public struct UpdateItemInfo
		{
			public int dataIndex;

			public int itemIndex;

			public GameObject instance;
		}

		[Serializable]
		public struct DisableItemInfo
		{
			public int itemIndex;

			public GameObject instance;
		}

		[SerializeField]
		private ScrollRect scrollRect;

		[SerializeField]
		private RectTransform prefab;

		[SerializeField]
		private DirectionType baseDirectionType;

		[SerializeField]
		[FormerlySerializedAs("updateItemTimingRowCount")]
		private int updateItemTimingOffsetCount;

		[SerializeField]
		private LayoutInfo layoutInfo;

		[SerializeField]
		private RectOffset padding;

		private int poolCount;

		private int allDataCount;

		private RectTransform[] poolItems;

		private int currentDataIndex;

		private float decrementPerFrame;

		public UnityEvent onScrollItem;

		public UpdateItemEvent onUpdateItem;

		public DisableItemEvent onDisableItem;

		private float prevNormalizedContentPosition;

		public int PoolCount
		{
			get
			{
				return poolCount;
			}
		}

		public Vector2 NormalzeScrollPosition
		{
			get
			{
				return scrollRect != null ? scrollRect.normalizedPosition : Vector2.zero;
			}
			set
			{
				if (scrollRect != null)
				{
					scrollRect.normalizedPosition = value;
				}
			}
		}

		public Vector2 Velocity
		{
			get
			{
				return scrollRect != null ? scrollRect.velocity : Vector2.zero;
			}
			set
			{
				if (scrollRect != null)
				{
					scrollRect.velocity = value;
				}
			}
		}

		public bool Vertical
		{
			get
			{
				return scrollRect != null && scrollRect.vertical;
			}
			set
			{
				if (scrollRect != null)
				{
					scrollRect.vertical = value;
				}
			}
		}

		public bool Horizontal
		{
			get
			{
				return scrollRect != null && scrollRect.horizontal;
			}
			set
			{
				if (scrollRect != null)
				{
					scrollRect.horizontal = value;
				}
			}
		}

		public bool TryGetPoolItemAt(int index, out GameObject obj)
		{
			obj = null;
			if (poolItems == null || index < 0 || index >= poolItems.Length || poolItems[index] == null)
			{
				return false;
			}

			obj = poolItems[index].gameObject;
			return obj != null;
		}

		public void Refresh(bool resetContentPosition = true)
		{
			Refresh(allDataCount, resetContentPosition);
		}

		public IReadOnlyList<RectTransform> GetPoolItemAll()
		{
			return poolItems;
		}

		[Skip]
		public void Refresh(int allDataCount, bool resetContentPosition = true)
		{
			EnsureReferences();
			if (layoutInfo.splitCount <= 0)
			{
				layoutInfo.splitCount = 1;
			}

			this.allDataCount = Mathf.Max(0, allDataCount);
			if (resetContentPosition && scrollRect != null && scrollRect.content != null)
			{
				scrollRect.content.anchoredPosition = Vector2.zero;
				scrollRect.velocity = Vector2.zero;
				currentDataIndex = 0;
				decrementPerFrame = 0f;
			}

			if (poolItems == null || poolItems.Length == 0)
			{
				SetupPoolItems();
			}

			if (scrollRect != null)
			{
				prevNormalizedContentPosition = baseDirectionType == DirectionType.Horizontal
					? scrollRect.normalizedPosition.x
					: 1f - scrollRect.normalizedPosition.y;
			}

			SetupScrollRange(this.allDataCount);
			ResetupAllItem();
		}

		public void UpdateAllItem()
		{
			if (poolItems == null)
			{
				return;
			}

			for (var i = 0; i < poolItems.Length && currentDataIndex + i < allDataCount; i++)
			{
				var item = poolItems[i];
				if (item == null)
				{
					continue;
				}

				BuildUpdateItemData(i, currentDataIndex + i, item.gameObject, out var data);
				UpdateItemObserver(ref data);
			}
		}

		public void UpdateItemOf(int dataIndex)
		{
			if (poolItems == null || dataIndex < currentDataIndex || dataIndex >= currentDataIndex + poolItems.Length || dataIndex >= allDataCount)
			{
				return;
			}

			var itemIndex = dataIndex - currentDataIndex;
			if (itemIndex < 0 || itemIndex >= poolItems.Length || poolItems[itemIndex] == null)
			{
				return;
			}

			BuildUpdateItemData(itemIndex, dataIndex, poolItems[itemIndex].gameObject, out var data);
			UpdateItemObserver(ref data);
		}

		public Vector2 GetScrollPosition()
		{
			return scrollRect != null && scrollRect.content != null ? (Vector2)scrollRect.content.localPosition : Vector2.zero;
		}

		public void SetScrollPosition(Vector2 position)
		{
			if (scrollRect != null && scrollRect.content != null)
			{
				scrollRect.content.localPosition = new Vector3(position.x, position.y, 0f);
			}
		}

		public void SetScrollRectMovementType(ScrollRect.MovementType movementType)
		{
			if (scrollRect != null)
			{
				scrollRect.movementType = movementType;
			}
		}

		public void SetupPoolItems()
		{
			EnsureReferences();
			if (scrollRect == null || scrollRect.content == null || prefab == null)
			{
				poolItems = Array.Empty<RectTransform>();
				poolCount = 0;
				return;
			}

			poolCount = Mathf.Max(1, CalcShouldCreatePoolObjectCount());
			poolItems = new RectTransform[poolCount];
			var prefabObject = prefab.gameObject;
			var wasActive = prefabObject.activeSelf;
			if (!wasActive)
			{
				prefabObject.SetActive(true);
			}

			for (var i = 0; i < poolItems.Length; i++)
			{
				var instance = Instantiate(prefab, scrollRect.content, false);
				instance.name = i.ToString();
				poolItems[i] = instance;
				UpdateItemPositionOf(i, i);
				SetupItemIndexOf(i);
			}

			prefabObject.SetActive(false);
		}

		private void SetupScrollRange(int allItemCount)
		{
			if (scrollRect == null || scrollRect.content == null || prefab == null || layoutInfo == null)
			{
				return;
			}

			var splitCount = Mathf.Max(1, layoutInfo.splitCount);
			var rowCount = Mathf.Max(1, Mathf.CeilToInt((float)Mathf.Max(0, allItemCount) / splitCount));
			var prefabRect = prefab.rect;
			var size = scrollRect.content.sizeDelta;
			if (baseDirectionType == DirectionType.Vertical)
			{
				size.y = padding.top + padding.bottom + prefabRect.height * rowCount + layoutInfo.itemSpacing.y * Mathf.Max(0, rowCount - 1);
			}
			else
			{
				size.x = padding.left + padding.right + prefabRect.width * rowCount + layoutInfo.itemSpacing.x * Mathf.Max(0, rowCount - 1);
			}

			scrollRect.content.sizeDelta = size;
		}

		private void ResetupAllItem()
		{
			if (poolItems == null)
			{
				return;
			}

			for (var i = 0; i < poolItems.Length; i++)
			{
				SetupItemIndexOf(i);
			}
		}

		private void SetupItemIndexOf(int itemIndex)
		{
			if (poolItems == null || itemIndex < 0 || itemIndex >= poolItems.Length)
			{
				return;
			}

			var item = poolItems[itemIndex];
			if (item == null)
			{
				return;
			}

			var dataIndex = currentDataIndex + itemIndex;
			if (dataIndex < allDataCount)
			{
				if (!item.gameObject.activeSelf)
				{
					item.gameObject.SetActive(true);
				}

				UpdateItemPositionOf(itemIndex, dataIndex);
				BuildUpdateItemData(itemIndex, dataIndex, item.gameObject, out var data);
				UpdateItemObserver(ref data);
			}
			else
			{
				BuildDisableItemData(itemIndex, item.gameObject, out var data);
				if (item.gameObject.activeSelf)
				{
					item.gameObject.SetActive(false);
				}

				DisableItemObserver(ref data);
			}
		}

		private void OnUpdatePoolItemImpl(int nextIndexinAllItems)
		{
			currentDataIndex = Mathf.Clamp(nextIndexinAllItems, 0, Mathf.Max(0, allDataCount - 1));
			ResetupAllItem();
			ScrollItemObserver();
		}

		private Vector2 CalcItemPosition(int row, int column)
		{
			if (prefab == null || layoutInfo == null)
			{
				return Vector2.zero;
			}

			var basePosition = prefab.anchoredPosition;
			var rect = prefab.rect;
			return new Vector2(
				basePosition.x + padding.left + (rect.width + layoutInfo.itemSpacing.x) * row,
				basePosition.y - padding.top - (rect.height + layoutInfo.itemSpacing.y) * column);
		}

		private void UpdateItemPositionOf(int itemIndex, int dataIndex)
		{
			if (poolItems == null || itemIndex < 0 || itemIndex >= poolItems.Length || poolItems[itemIndex] == null)
			{
				return;
			}

			var rowAndColumn = CalcRowAndColumn(dataIndex);
			poolItems[itemIndex].anchoredPosition = CalcItemPosition(rowAndColumn.Item1, rowAndColumn.Item2);
		}

		private (int, int) CalcRowAndColumn(int dataIndex)
		{
			var splitCount = Mathf.Max(1, layoutInfo != null ? layoutInfo.splitCount : 1);
			return baseDirectionType == DirectionType.Horizontal
				? (dataIndex / splitCount, dataIndex % splitCount)
				: (dataIndex % splitCount, dataIndex / splitCount);
		}

		private void UpdateInternal(float directionStartPosition, float directionItemSize, float directionSpacing, float scrollContentPosition)
		{
			if (poolItems == null || poolItems.Length == 0 || layoutInfo == null)
			{
				return;
			}

			var itemStride = Mathf.Max(1f, directionItemSize + directionSpacing);
			var row = Mathf.Max(0, Mathf.FloorToInt(Mathf.Max(0f, scrollContentPosition - directionStartPosition) / itemStride));
			var nextDataIndex = row * Mathf.Max(1, layoutInfo.splitCount);
			nextDataIndex = Mathf.Clamp(nextDataIndex, 0, Mathf.Max(0, allDataCount - 1));
			if (nextDataIndex != currentDataIndex)
			{
				currentDataIndex = nextDataIndex;
				ResetupAllItem();
				ScrollItemObserver();
			}

			if (scrollRect != null)
			{
				prevNormalizedContentPosition = baseDirectionType == DirectionType.Horizontal
					? scrollRect.normalizedPosition.x
					: 1f - scrollRect.normalizedPosition.y;
			}
		}

		private void BuildUpdateItemData(int itemIndex, int dataIndex, GameObject obj, out UpdateItemInfo data)
		{
			data = new UpdateItemInfo
			{
				dataIndex = dataIndex,
				itemIndex = itemIndex,
				instance = obj
			};
		}

		private void BuildDisableItemData(int itemIndex, GameObject obj, out DisableItemInfo data)
		{
			data = new DisableItemInfo
			{
				itemIndex = itemIndex,
				instance = obj
			};
		}

		private void UpdateItemObserver(ref UpdateItemInfo info)
		{
			onUpdateItem?.Invoke(info.dataIndex, info.itemIndex, info.instance);
		}

		private void DisableItemObserver(ref DisableItemInfo info)
		{
			onDisableItem?.Invoke(info.itemIndex, info.instance);
		}

		private void ScrollItemObserver()
		{
			onScrollItem?.Invoke();
		}

		private int CalcShouldCreatePoolObjectCount()
		{
			EnsureReferences();
			if (scrollRect == null || scrollRect.viewport == null || prefab == null || layoutInfo == null)
			{
				return poolCount;
			}

			scrollRect.horizontal = baseDirectionType == DirectionType.Horizontal;
			scrollRect.vertical = baseDirectionType == DirectionType.Vertical;

			var viewportSize = baseDirectionType == DirectionType.Horizontal ? scrollRect.viewport.rect.width : scrollRect.viewport.rect.height;
			var itemSize = baseDirectionType == DirectionType.Horizontal ? prefab.rect.width : prefab.rect.height;
			if (itemSize <= 0f)
			{
				return poolCount;
			}

			var visibleLineCount = Mathf.CeilToInt(viewportSize / itemSize) + Mathf.Max(0, updateItemTimingOffsetCount);
			var splitCount = Mathf.Max(1, layoutInfo.splitCount);
			return Mathf.Max(splitCount, visibleLineCount * splitCount);
		}

		private void Awake()
		{
			EnsureReferences();
			SetupPoolItems();
		}

		private void OnDestroy()
		{
			onUpdateItem?.RemoveAllListeners();
			onScrollItem?.RemoveAllListeners();
			onDisableItem?.RemoveAllListeners();
		}

		private void LateUpdate()
		{
			if (scrollRect == null || scrollRect.content == null || prefab == null || layoutInfo == null)
			{
				return;
			}

			var prefabRect = prefab.rect;
			if (scrollRect.vertical)
			{
				UpdateInternal(prefab.anchoredPosition.y, prefabRect.height, layoutInfo.itemSpacing.y, -scrollRect.content.anchoredPosition.y);
			}

			if (scrollRect.horizontal)
			{
				UpdateInternal(prefab.anchoredPosition.x, prefabRect.width, layoutInfo.itemSpacing.x, scrollRect.content.anchoredPosition.x);
			}
		}

		private void OnEnable()
		{
			EnsureReferences();
			if (scrollRect != null)
			{
				scrollRect.horizontal = baseDirectionType == DirectionType.Horizontal;
				scrollRect.vertical = baseDirectionType == DirectionType.Vertical;
			}
		}

		public FixedSizeReuseScroller()
		{
			updateItemTimingOffsetCount = 1;
			poolCount = 7;
			onScrollItem = new UnityEvent();
			onUpdateItem = new UpdateItemEvent();
			onDisableItem = new DisableItemEvent();
			layoutInfo = new LayoutInfo { splitCount = 1 };
			padding = new RectOffset();
		}

		private void EnsureReferences()
		{
			if (scrollRect == null)
			{
				scrollRect = GetComponent<ScrollRect>();
			}

			if (layoutInfo == null)
			{
				layoutInfo = new LayoutInfo { splitCount = 1 };
			}

			if (layoutInfo.splitCount <= 0)
			{
				layoutInfo.splitCount = 1;
			}

			if (padding == null)
			{
				padding = new RectOffset();
			}

			if (onScrollItem == null)
			{
				onScrollItem = new UnityEvent();
			}

			if (onUpdateItem == null)
			{
				onUpdateItem = new UpdateItemEvent();
			}

			if (onDisableItem == null)
			{
				onDisableItem = new DisableItemEvent();
			}
		}
	}
}
