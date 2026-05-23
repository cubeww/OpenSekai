using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sekai.UI
{
	public abstract class GenericTabItem<TData> : ListViewItem, IDisposable where TData : GenericTabData
	{
		[SerializeField]
		protected CustomButton _button;

		protected TData _data;

		private Action<int> _onSelected;

		private Func<int, bool> _isSelectedProvider;

		private bool _preventInteractionWhenSelected;

		protected bool IsSelected
		{
			get
			{
				return _data != null && _isSelectedProvider != null && _isSelectedProvider(_data.Index);
			}
		}

		public void Setup(TData data, Action<int> onSelected, Func<int, bool> isSelectedProvider, bool preventInteractionWhenSelected = false)
		{
			_data = data;
			_onSelected = onSelected;
			_isSelectedProvider = isSelectedProvider;
			_preventInteractionWhenSelected = preventInteractionWhenSelected;

			if (_button != null)
			{
				_button.onClick.RemoveListener(OnClicked);
				_button.onClick.AddListener(OnClicked);
				_button.interactable = data == null || data.IsEnabled;
			}

			OnSetupInternal();
			Refresh();
		}

		protected virtual void OnSetupInternal()
		{
		}

		public void Refresh()
		{
			RefreshInternal();
			ApplySelected();
		}

		public virtual void ApplySelected()
		{
			if (_button != null && _data != null)
			{
				_button.interactable = _data.IsEnabled && (!_preventInteractionWhenSelected || !IsSelected);
			}
		}

		public virtual UniTask OnSelectedAsync(CancellationToken ct)
		{
			ApplySelected();
			return UniTask.CompletedTask;
		}

		public virtual UniTask OnDeselectedAsync(CancellationToken ct)
		{
			ApplySelected();
			return UniTask.CompletedTask;
		}

		protected abstract void RefreshInternal();

		private void OnClicked()
		{
			if (_data == null || !_data.IsEnabled)
			{
				return;
			}

			if (_preventInteractionWhenSelected && IsSelected)
			{
				return;
			}

			_onSelected?.Invoke(_data.Index);
		}

		public void Dispose()
		{
			if (_button != null)
			{
				_button.onClick.RemoveListener(OnClicked);
			}

			DisposeInternal();
		}

		protected virtual void DisposeInternal()
		{
		}

		protected GenericTabItem()
		{
		}
	}
}
