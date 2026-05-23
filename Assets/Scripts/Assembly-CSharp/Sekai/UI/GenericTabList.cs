using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Sekai.UI
{
	public abstract class GenericTabList<TTabItem, TTabData> : MonoBehaviour, IDisposable where TTabItem : GenericTabItem<TTabData> where TTabData : GenericTabData
	{
		[SerializeField]
		private ListView _listView;

		[SerializeField]
		private bool _preventSameIndexReselection;

		private TTabData[] _dataArray;

		private Action<int> _onSelected;

		public int CurrentSelectedIndex { get; private set; }

		public void Setup([NotNull] TTabData[] dataArray, Action<int> onSelected, int defaultSelectedIndex = 0)
		{
			_dataArray = dataArray ?? Array.Empty<TTabData>();
			_onSelected = onSelected;
			CurrentSelectedIndex = Mathf.Clamp(defaultSelectedIndex, 0, Mathf.Max(0, _dataArray.Length - 1));

			if (_listView != null)
			{
				_listView.OnCreateCell = OnCreateCell;
				_listView.CreateViewItem(_dataArray.Length, false);
			}

			Refresh();
		}

		public void Refresh()
		{
			if (_listView != null)
			{
				_listView.OnExcuteAllCell((cell, index) => OnCreateCell(cell, index), false);
			}
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		private void OnCreateCell(ListViewItem cell, int index)
		{
			if (cell is TTabItem tabItem && _dataArray != null && index >= 0 && index < _dataArray.Length)
			{
				tabItem.Setup(_dataArray[index], OnSelectedTab, IsSelectedProvider, _preventSameIndexReselection);
			}
		}

		private void OnSelectedTab(int index)
		{
			if (_preventSameIndexReselection && index == CurrentSelectedIndex)
			{
				return;
			}

			TTabItem previousTab = FindTab(CurrentSelectedIndex);
			CurrentSelectedIndex = index;
			TTabItem newTab = FindTab(CurrentSelectedIndex);
			OnSelectedTabAsync(previousTab, newTab);
			_onSelected?.Invoke(index);
		}

		private async UniTaskVoid OnSelectedTabAsync(TTabItem previousTab, TTabItem newTab)
		{
			CancellationToken cancellationToken = this.GetCancellationTokenOnDestroy();
			if (previousTab != null)
			{
				await previousTab.OnDeselectedAsync(cancellationToken);
			}

			if (newTab != null)
			{
				await newTab.OnSelectedAsync(cancellationToken);
			}

			Refresh();
		}

		private TTabItem FindTab(int index)
		{
			if (_listView == null)
			{
				return null;
			}

			ListViewItem first = null;
			_listView.OnExcutePickCell(index, (cell, _) => first = cell);
			return first as TTabItem;
		}

		public void Dispose()
		{
			if (_listView != null)
			{
				_listView.OnCreateCell = null;
			}
		}

		private bool IsSelectedProvider(int index)
		{
			return index == CurrentSelectedIndex;
		}

		protected GenericTabList()
		{
		}
	}
}
