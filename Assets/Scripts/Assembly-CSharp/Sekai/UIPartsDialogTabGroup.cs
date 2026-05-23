using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	[RequireComponent(typeof(CustomIndexToggleGroup))]
	public sealed class UIPartsDialogTabGroup : MonoBehaviour
	{
		[SerializeField]
		private CustomIndexToggleGroup toggleGroup;

		[SerializeField]
		private UIPartsDialogTab tabPrefab;

		private readonly List<UIPartsDialogTab> tabList;

		public CustomIndexToggleGroup ToggleGroup
		{
			get
			{
				return toggleGroup;
			}
		}

		public void Setup()
		{
			if (toggleGroup == null)
			{
				toggleGroup = GetComponent<CustomIndexToggleGroup>();
			}
			SetupTab();
		}

		private void OnTabSelectedChanged()
		{
			for (int i = 0; i < tabList.Count; i++)
			{
				UIPartsDialogTab tab = tabList[i];
				if (tab != null)
				{
					tab.IsOn = toggleGroup != null && toggleGroup.SelectedIndex == i;
				}
			}
		}

		private void SetupTab()
		{
			tabList.Clear();
			GetComponentsInChildren(true, tabList);
			for (int i = 0; i < tabList.Count; i++)
			{
				tabList[i]?.Setup(OnTabSelectedChanged);
			}
			if (toggleGroup != null)
			{
				toggleGroup.CollectToggles(false);
				toggleGroup.OnSelectedIndexChanged -= _ => OnTabSelectedChanged();
				toggleGroup.OnSelectedIndexChanged += _ => OnTabSelectedChanged();
			}
			OnTabSelectedChanged();
		}

		public void Setup(int tabCount)
		{
			if (toggleGroup == null)
			{
				toggleGroup = GetComponent<CustomIndexToggleGroup>();
			}
			tabCount = Mathf.Max(0, tabCount);
			if (tabPrefab != null)
			{
				for (int i = transform.childCount; i < tabCount; i++)
				{
					Instantiate(tabPrefab, transform, false);
				}
			}
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(i < tabCount);
			}
			SetupTab();
		}

		public void ShowBadge(int index, bool isShow)
		{
			if (index >= 0 && index < tabList.Count && tabList[index] != null)
			{
				tabList[index].IsShowBadge = isShow;
			}
		}

		public UIPartsDialogTabGroup()
		{
			tabList = new List<UIPartsDialogTab>();
		}
	}
}
