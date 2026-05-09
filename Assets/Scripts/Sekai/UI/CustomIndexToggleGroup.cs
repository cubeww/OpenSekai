using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
    public class CustomIndexToggleGroup : ToggleGroup
    {
        [SerializeField] protected List<CustomToggle> indexToggles = new List<CustomToggle>();
        [SerializeField] private bool selectedOnAwake;
        [SerializeField] private CustomText allText;
        [SerializeField] private CustomText otherText;

        protected int selectedIndex = -1;
        protected CustomToggle selectToggle;
        protected bool initializing;

        public List<CustomToggle> IndexToggles => indexToggles;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetSelectedIndex(value, true); }
        }

        public CustomToggle SelectedToggle => selectToggle;

        public Action<int> OnSelectedIndexChanged { get; set; }

        protected override void Awake()
        {
            base.Awake();
            CollectToggles(selectedOnAwake);
        }

        public void SetSelectedIndexWithoutNotify(int value)
        {
            SetSelectedIndex(value, false);
        }

        [Obsolete]
        public void InitializeSelectIndex(int index, bool callSelectedEvent = true)
        {
            SetSelectedIndex(index, callSelectedEvent);
        }

        public void CollectToggles(bool shouldSelectOnAwake)
        {
            indexToggles = GetComponentsInChildren<CustomToggle>(true).ToList();
            for (int i = 0; i < indexToggles.Count; i++)
            {
                int index = i;
                CustomToggle toggle = indexToggles[i];
                toggle.group = this;
                toggle.onValueChanged.RemoveListener(OnChanged);
                toggle.onValueChanged.AddListener(OnChanged);
                if (shouldSelectOnAwake && toggle.isOn)
                {
                    selectedIndex = index;
                    selectToggle = toggle;
                }
            }
        }

        public void DisableToggle(int index)
        {
            if (IsValidIndex(index))
            {
                indexToggles[index].interactable = false;
            }
        }

        public void EnableToggle(int index)
        {
            if (IsValidIndex(index))
            {
                indexToggles[index].interactable = true;
            }
        }

        private void OnChanged(bool isOn)
        {
            if (!isOn || initializing)
            {
                return;
            }

            for (int i = 0; i < indexToggles.Count; i++)
            {
                if (indexToggles[i] != null && indexToggles[i].isOn)
                {
                    selectedIndex = i;
                    selectToggle = indexToggles[i];
                    OnSelectedIndexChanged?.Invoke(i);
                    return;
                }
            }
        }

        private void SetSelectedIndex(int value, bool notify)
        {
            if (!IsValidIndex(value))
            {
                return;
            }

            initializing = !notify;
            selectedIndex = value;
            selectToggle = indexToggles[value];
            indexToggles[value].isOn = true;
            initializing = false;

            if (notify)
            {
                OnSelectedIndexChanged?.Invoke(value);
            }
        }

        private bool IsValidIndex(int index)
        {
            return indexToggles != null && index >= 0 && index < indexToggles.Count && indexToggles[index] != null;
        }
    }
}
