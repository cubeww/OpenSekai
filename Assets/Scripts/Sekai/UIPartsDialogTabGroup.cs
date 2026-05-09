using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
    public sealed class UIPartsDialogTabGroup : MonoBehaviour
    {
        [SerializeField] private CustomIndexToggleGroup toggleGroup;
        [SerializeField] private UIPartsDialogTab tabPrefab;

        private readonly List<UIPartsDialogTab> tabList = new List<UIPartsDialogTab>();

        public CustomIndexToggleGroup ToggleGroup => toggleGroup;

        public void Setup()
        {
            CollectTabs();
            toggleGroup?.CollectToggles(true);
        }

        public void Setup(int tabCount)
        {
            CollectTabs();
            for (int i = 0; i < tabList.Count; i++)
            {
                if (tabList[i] != null)
                {
                    tabList[i].gameObject.SetActive(i < tabCount);
                }
            }

            toggleGroup?.CollectToggles(true);
        }

        public void ShowBadge(int index, bool isShow)
        {
            CollectTabs();
            if (index >= 0 && index < tabList.Count && tabList[index] != null)
            {
                tabList[index].IsShowBadge = isShow;
            }
        }

        private void CollectTabs()
        {
            tabList.Clear();
            GetComponentsInChildren(true, tabList);
        }
    }
}
