using System;
using System.Collections.Generic;
using CP;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
	public class CustomIndexToggleGroup : ToggleGroup
	{
		[SerializeField]
		protected List<CustomToggle> indexToggles;

		[SerializeField]
		private bool selectedOnAwake;

		protected int selectedIndex;

		protected CustomToggle selectToggle;

		protected bool initializing;

		public List<CustomToggle> IndexToggles
		{
			get
			{
				return indexToggles;
			}
		}

		public int SelectedIndex
		{
			get
			{
				if (indexToggles == null)
				{
					return selectedIndex;
				}

				for (int i = 0; i < indexToggles.Count; i++)
				{
					if (indexToggles[i] != null && indexToggles[i].isOn)
					{
						return i;
					}
				}

				return 0;
			}
			set
			{
				if (!IsValidIndex(value))
				{
					LogUtility.LogError("CustomIndexToggleGroup selected index is out of range: {0}", value);
					return;
				}

				for (int i = 0; i < indexToggles.Count; i++)
				{
					if (indexToggles[i] != null)
					{
						indexToggles[i].isOn = i == value;
					}
				}
			}
		}

		public CustomToggle SelectedToggle
		{
			get
			{
				return selectToggle;
			}
		}

		public Action<int> OnSelectedIndexChanged { get; set; }

		public void SetSelectedIndexWithoutNotify(int value)
		{
			if (!IsValidIndex(value))
			{
				LogUtility.LogError("CustomIndexToggleGroup selected index is out of range: {0}", value);
				return;
			}

			selectedIndex = value;
			for (int i = 0; i < indexToggles.Count; i++)
			{
				if (indexToggles[i] != null)
				{
					indexToggles[i].SetIsOnWithoutNotify(i == selectedIndex);
				}
			}

			selectToggle = indexToggles[selectedIndex];
		}

		protected override void Awake()
		{
			base.Awake();
			CollectToggles(selectedOnAwake);
		}

		[Obsolete("廃止予定 SelectedIndexを使用してください")]
		public void InitializeSelectIndex(int index, bool callSelectedEvent = true)
		{
			initializing = !callSelectedEvent;
			SelectedIndex = index;
			initializing = false;
		}

		public void CollectToggles(bool selectedOnAwake)
		{
			indexToggles ??= new List<CustomToggle>();
			for (int i = 0; i < indexToggles.Count; i++)
			{
				CustomToggle toggle = indexToggles[i];
				if (toggle == null)
				{
					continue;
				}

				toggle.isOn = selectedOnAwake && i == SelectedIndex;
				toggle.group = this;
				toggle.onValueChanged.RemoveListener(OnChanged);
				toggle.onValueChanged.AddListener(OnChanged);
			}
		}

		private void OnChanged(bool isOn)
		{
			if (!isOn || indexToggles == null)
			{
				return;
			}

			selectedIndex = 0;
			for (int i = 0; i < indexToggles.Count; i++)
			{
				if (indexToggles[i] != null && indexToggles[i].isOn)
				{
					selectedIndex = i;
					break;
				}
			}

			selectToggle = IsValidIndex(selectedIndex) ? indexToggles[selectedIndex] : null;
			if (!initializing)
			{
				OnSelectedIndexChanged?.Invoke(selectedIndex);
			}
		}

		public void DisableToggle(int index)
		{
			if (IsValidIndex(index) && indexToggles[index] != null)
			{
				indexToggles[index].gameObject.SetActive(false);
			}
		}

		public void EnableToggle(int index)
		{
			if (IsValidIndex(index) && indexToggles[index] != null)
			{
				indexToggles[index].gameObject.SetActive(true);
			}
		}

		private bool IsValidIndex(int index)
		{
			return indexToggles != null && index >= 0 && index < indexToggles.Count;
		}

		public CustomIndexToggleGroup()
		{
			indexToggles = new List<CustomToggle>();
		}
	}
}
