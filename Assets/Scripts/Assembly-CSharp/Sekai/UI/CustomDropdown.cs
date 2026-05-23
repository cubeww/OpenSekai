using System;
using System.Collections.Generic;
using Sekai.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.UI
{
	[AddComponentMenu("Custom UI/Custom Dropdown")]
	public class CustomDropdown : TMP_Dropdown
	{
		[SerializeField]
		private GameObject baseObject;

		[SerializeField]
		private GameObject arrowObject;

		[SerializeField]
		private bool showsAllItems;

		[SerializeField]
		private bool enableItemClickEvent;

		[SerializeField]
		private bool centerScrollOnOpen;

		private ScrollRect scrollRect;

		private DropdownItem templateItem;

		[SerializeField]
		protected SeType se;

		private Action onSelect;

		private Action onPointerClick;

		private Dictionary<int, Action> onItemClickCallbacks;

		public void Setup(List<string> keys, int index, Action<int> onSelect)
		{
			ClearOptions();
			List<OptionData> optionData = new List<OptionData>();
			if (keys != null)
			{
				for (int i = 0; i < keys.Count; i++)
				{
					optionData.Add(new OptionData(keys[i]));
				}
			}
			AddOptions(optionData);
			SetValueWithoutNotify(Mathf.Clamp(index, 0, Mathf.Max(0, options.Count - 1)));
			onValueChanged.RemoveAllListeners();
			if (onSelect != null)
			{
				onValueChanged.AddListener(value => onSelect(value));
			}
		}

		public void RegisterOnValueChanged(Action<int> callback)
		{
			if (callback != null)
			{
				onValueChanged.AddListener(value => callback(value));
			}
		}

		public void Refresh(List<string> keys, int index)
		{
			ClearOptions();
			List<OptionData> optionData = new List<OptionData>();
			if (keys != null)
			{
				for (int i = 0; i < keys.Count; i++)
				{
					optionData.Add(new OptionData(keys[i]));
				}
			}
			AddOptions(optionData);
			SetValueWithoutNotify(Mathf.Clamp(index, 0, Mathf.Max(0, options.Count - 1)));
		}

		public override void OnPointerClick(PointerEventData eventData)
		{
			if (IsExpanded)
			{
				return;
			}

			base.OnPointerClick(eventData);
			CustomSelectableDefine.PlaySE(se, string.Empty);
			SetLayout();
			SetArrowObject(true);
			AdjustHeight();
			if (centerScrollOnOpen && scrollRect != null && scrollRect.content != null)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
				TargetScrollPosition();
			}
			UpdateAllItemCallbacks();
			onPointerClick?.Invoke();
		}

		private void UpdateAllItemCallbacks()
		{
			if (!enableItemClickEvent || scrollRect == null || onItemClickCallbacks == null || onItemClickCallbacks.Count == 0)
			{
				return;
			}
			DropdownItem[] items = scrollRect.GetComponentsInChildren<DropdownItem>();
			foreach (KeyValuePair<int, Action> pair in onItemClickCallbacks)
			{
				int index = pair.Key;
				if (index < 0 || index >= items.Length || items[index] == null)
				{
					continue;
				}
				var handler = items[index].GetComponent<DropdownItemClickHandler>();
				if (handler == null)
				{
					handler = items[index].gameObject.AddComponent<DropdownItemClickHandler>();
				}
				handler.SetCallback(pair.Value);
			}
		}

		public override void OnSelect(BaseEventData eventData)
		{
			onSelect?.Invoke();
			base.OnSelect(eventData);
		}

		public void SetOnSelectCallback(Action onSelect)
		{
			this.onSelect = onSelect;
		}

		public void SetOnPointerClick(Action onPointerClick)
		{
			this.onPointerClick = onPointerClick;
		}

		public void SetOnItemClick(int index, Action callback)
		{
			onItemClickCallbacks ??= new Dictionary<int, Action>();
			onItemClickCallbacks[index] = callback;
		}

		public void ClearItemClickCallbacks()
		{
			onItemClickCallbacks?.Clear();
		}

		public void SetDropDownLayout(Vector2 anchorMin, Vector2 anchorMax, int anchoredPositionY, Vector2 pivot)
		{
			if (template == null)
			{
				return;
			}
			RectTransform rectTransform = template;
			rectTransform.anchorMin = anchorMin;
			rectTransform.anchorMax = anchorMax;
			rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, anchoredPositionY);
			rectTransform.pivot = pivot;
		}

		public bool Contains(int index)
		{
			return index >= 0 && index < options.Count;
		}

		private void SetLayout()
		{
			if (scrollRect == null)
			{
				return;
			}

			var listRect = scrollRect.GetComponent<RectTransform>();
			var selfRect = transform as RectTransform;
			if (listRect == null || selfRect == null)
			{
				return;
			}

			float dropdownHeight;
			if (showsAllItems && scrollRect.content != null)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
				dropdownHeight = scrollRect.content.rect.height;
			}
			else
			{
				dropdownHeight = listRect.sizeDelta.y;
			}

			float halfBaseHeight = selfRect.sizeDelta.y * 0.5f;
			listRect.sizeDelta = new Vector2(listRect.sizeDelta.x, dropdownHeight + halfBaseHeight);
			listRect.anchoredPosition = new Vector2(listRect.anchoredPosition.x, -halfBaseHeight);
			if (scrollRect.viewport != null)
			{
				scrollRect.viewport.offsetMax = new Vector2(0f, -halfBaseHeight);
			}

			if (baseObject == null || baseObject.GetComponent<Canvas>() != null)
			{
				return;
			}

			var sourceCanvas = scrollRect.GetComponent<Canvas>();
			if (sourceCanvas == null)
			{
				return;
			}

			var canvas = baseObject.AddComponent<Canvas>();
			canvas.renderMode = sourceCanvas.renderMode;
			canvas.worldCamera = sourceCanvas.worldCamera;
			canvas.planeDistance = sourceCanvas.planeDistance;
			canvas.overrideSorting = sourceCanvas.overrideSorting;
			canvas.sortingLayerID = sourceCanvas.sortingLayerID;
			canvas.sortingOrder = sourceCanvas.sortingOrder + 1;
		}

		protected override GameObject CreateDropdownList(GameObject template)
		{
			GameObject dropdownList = base.CreateDropdownList(template);
			scrollRect = dropdownList != null ? dropdownList.GetComponent<CustomScrollRect>() : null;
			scrollRect ??= dropdownList != null ? dropdownList.GetComponentInChildren<CustomScrollRect>(true) : null;
			scrollRect ??= dropdownList != null ? dropdownList.GetComponentInChildren<ScrollRect>(true) : null;
			DropdownItem[] items = dropdownList != null ? dropdownList.GetComponentsInChildren<DropdownItem>() : null;
			templateItem = items != null && items.Length > 0 ? items[0] : null;
			if (enableItemClickEvent)
			{
				SetupItemClickEvents(items);
			}
			return dropdownList;
		}

		private void SetupItemClickEvents(DropdownItem[] items)
		{
			if (items == null)
			{
				return;
			}
			int count = Mathf.Min(items.Length, options.Count);
			for (int i = 0; i < count; i++)
			{
				if (items[i] == null || items[i].GetComponent<DropdownItemClickHandler>() != null)
				{
					continue;
				}
				items[i].gameObject.AddComponent<DropdownItemClickHandler>();
			}
		}

		protected override void DestroyDropdownList(GameObject dropdownList)
		{
			SetArrowObject(false);
			baseObject.RemoveComponentIfExists<Canvas>();
			base.DestroyDropdownList(dropdownList);
		}

		protected override GameObject CreateBlocker(Canvas rootCanvas)
		{
			GameObject blocker = base.CreateBlocker(rootCanvas);
			if (!centerScrollOnOpen)
			{
				TargetScrollPosition();
			}
			return blocker;
		}

		private void TargetScrollPosition()
		{
			if (scrollRect == null || scrollRect.viewport == null || scrollRect.content == null || templateItem == null || options.Count <= 0)
			{
				return;
			}

			float viewportHeight = scrollRect.viewport.rect.height;
			float contentHeight = scrollRect.content.rect.height;
			float itemHeight = templateItem.rectTransform != null ? templateItem.rectTransform.rect.height : 0f;
			float targetY = contentHeight * ((float)value / options.Count) + itemHeight * 0.5f - viewportHeight * 0.5f;
			float maxY = Mathf.Max(0f, contentHeight - viewportHeight);
			Vector2 position = scrollRect.content.anchoredPosition;
			position.y = Mathf.Clamp(targetY, 0f, maxY);
			scrollRect.content.anchoredPosition = position;
		}

		private void SetArrowObject(bool isSelected)
		{
			if (arrowObject != null)
			{
				arrowObject.SetActive(isSelected);
			}
		}

		private void AdjustHeight()
		{
			if (scrollRect == null)
			{
				return;
			}
			var rectTransform = scrollRect.GetComponent<RectTransform>();
			if (rectTransform == null)
			{
				return;
			}

			Vector2 screenPoint;
			if (ScreenManager.Instance != null)
			{
				screenPoint = ScreenManager.Instance.WorldToScreenPoint(transform.position, Vector2.zero);
			}
			else
			{
				screenPoint = RectTransformUtility.WorldToScreenPoint(null, transform.position);
			}

			float overflowBelow = rectTransform.rect.height - screenPoint.y;
			if (overflowBelow < 0f)
			{
				return;
			}

			float availableHeight = rectTransform.rect.height - overflowBelow - Screen.safeArea.yMin - 10f;
			float height = Mathf.Min(rectTransform.rect.height, Mathf.Max(0f, availableHeight));
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
		}

		public CustomDropdown()
		{
			onItemClickCallbacks = new Dictionary<int, Action>();
		}
	}
}
