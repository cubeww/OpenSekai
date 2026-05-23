using System;
using UnityEngine;

namespace Sekai.UI
{
	public class UIPartsTabGroup : MonoBehaviour
	{
		[SerializeField]
		private UIPartsSelectorCell[] selectorCells;

		[SerializeField]
		private UIPartsSelectorCell cellPrefab;

		[SerializeField]
		private RectTransform cellRoot;

		[SerializeField]
		private GameObject coverObj;

		private UIPartsSelectorCell.ViewData[] tabDataList;

		public Action<int> OnSelectCell { get; set; }

		public int CurrentSelectedIndex { get; private set; }

		public UIPartsSelectorCell CurrentCell
		{
			get
			{
				return selectorCells != null && CurrentSelectedIndex >= 0 && CurrentSelectedIndex < selectorCells.Length ? selectorCells[CurrentSelectedIndex] : null;
			}
		}

		public UIPartsSelectorCell[] SelectorCells
		{
			get
			{
				return selectorCells;
			}
		}

		public void ShowCover(bool isShow)
		{
			if (coverObj != null)
			{
				coverObj.SetActive(isShow);
			}
		}

		public void Setup(int defaultIndex)
		{
			if (selectorCells == null)
			{
				selectorCells = Array.Empty<UIPartsSelectorCell>();
			}

			tabDataList = new UIPartsSelectorCell.ViewData[selectorCells.Length];
			for (int i = 0; i < tabDataList.Length; i++)
			{
				tabDataList[i] = new UIPartsSelectorCell.ViewData(i)
				{
					Number = i + 1,
					IsSelected = i == defaultIndex,
					IsEnabled = true
				};
			}

			SetupCell(defaultIndex);
		}

		public void Setup(int defaultIndex, UIPartsSelectorCell.ViewData[] tabDataList)
		{
			this.tabDataList = tabDataList ?? Array.Empty<UIPartsSelectorCell.ViewData>();
			SetupCell(defaultIndex);
		}

		public void Setup(UIPartsSelectorCell.ViewData[] tabDataList, UIPartsSelectorCell[] selectorCells)
		{
			this.tabDataList = tabDataList ?? Array.Empty<UIPartsSelectorCell.ViewData>();
			this.selectorCells = selectorCells ?? Array.Empty<UIPartsSelectorCell>();
			SetupCell(0);
		}

		public void SetupReuseOrCreate(int defaultIndex, UIPartsSelectorCell.ViewData[] tabDataArray)
		{
			tabDataList = tabDataArray ?? Array.Empty<UIPartsSelectorCell.ViewData>();
			if (selectorCells == null || selectorCells.Length < tabDataList.Length)
			{
				Array.Resize(ref selectorCells, tabDataList.Length);
			}

			for (int i = 0; i < tabDataList.Length; i++)
			{
				if (selectorCells[i] == null && cellPrefab != null)
				{
					selectorCells[i] = Instantiate(cellPrefab, cellRoot != null ? cellRoot : transform);
				}

				if (selectorCells[i] != null)
				{
					selectorCells[i].gameObject.SetActive(true);
				}
			}

			for (int i = tabDataList.Length; selectorCells != null && i < selectorCells.Length; i++)
			{
				if (selectorCells[i] != null)
				{
					selectorCells[i].gameObject.SetActive(false);
				}
			}

			SetupCell(defaultIndex);
		}

		private void SetCellViewData()
		{
			if (selectorCells == null || tabDataList == null)
			{
				return;
			}

			for (int i = 0; i < selectorCells.Length && i < tabDataList.Length; i++)
			{
				if (selectorCells[i] != null)
				{
					selectorCells[i].Setup(tabDataList[i]);
				}
			}
		}

		private void SetCellCallback()
		{
			if (selectorCells == null)
			{
				return;
			}

			for (int i = 0; i < selectorCells.Length; i++)
			{
				if (selectorCells[i] != null)
				{
					selectorCells[i].SetCallback(OnClickCell);
				}
			}
		}

		public void SetClickSe(string seName)
		{
			// TODO(original): restore CustomButton.ChangeOtherSE after CustomButton is fully restored.
		}

		public void SetSelectedIndex(int selectIndex)
		{
			SetupCell(selectIndex);
			OnSelectCell?.Invoke(CurrentSelectedIndex);
		}

		public void SetSelectedIndexWithoutCallback(int selectIndex)
		{
			SetupCell(selectIndex);
		}

		private void SetupCell(int selectIndex)
		{
			if (selectorCells == null)
			{
				selectorCells = Array.Empty<UIPartsSelectorCell>();
			}

			if (tabDataList == null || tabDataList.Length == 0)
			{
				tabDataList = new UIPartsSelectorCell.ViewData[selectorCells.Length];
				for (int i = 0; i < tabDataList.Length; i++)
				{
					tabDataList[i] = new UIPartsSelectorCell.ViewData(i)
					{
						Number = i + 1,
						IsEnabled = true
					};
				}
			}

			CurrentSelectedIndex = Mathf.Clamp(selectIndex, 0, Mathf.Max(0, tabDataList.Length - 1));
			for (int i = 0; i < tabDataList.Length; i++)
			{
				if (tabDataList[i] != null)
				{
					tabDataList[i].IsSelected = i == CurrentSelectedIndex;
				}
			}

			SetCellViewData();
			SetCellCallback();
		}

		private void OnClickCell(int selectIndex)
		{
			SetSelectedIndex(selectIndex);
		}

		public void Refresh()
		{
			SetCellViewData();
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public UIPartsTabGroup()
		{
			CurrentSelectedIndex = 0;
		}
	}
}
