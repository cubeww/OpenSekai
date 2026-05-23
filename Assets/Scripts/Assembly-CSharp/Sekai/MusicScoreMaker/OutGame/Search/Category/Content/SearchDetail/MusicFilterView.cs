using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail
{
	public sealed class MusicFilterView : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private MusicElementFreeWordFilterView elementFreeWordFilterView;

		[SerializeField]
		private FilteredMusicElementListView _filteredMusicElementListView;

		[SerializeField]
		private Canvas _canvas;

		[SerializeField]
		private TouchController _closeListViewChecker;

		[SerializeField]
		private CustomButton _clearButton;

		[SerializeField]
		private GameObject _searchIconObject;

		private MusicFilterViewData _viewData;

		private Action<string> _onSubmitEdit;

		private Action _onFocusMusicFilter;

		public void Setup(MusicFilterViewData viewData, Action onFocusMusicFilter, Action<string> onValueChangedEdit, Action<string> onSubmitEdit, Action<int> onFilteredMusic, Action onClearFilter)
		{
			throw null;
		}

		private void ApplyFilterState()
		{
			throw null;
		}

		public void ApplyFilteredMusicElements()
		{
			throw null;
		}

		public void ApplyFilteredMusicElement()
		{
			throw null;
		}

		public void Clear()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		private void OnFocusFilter()
		{
			throw null;
		}

		private void OnSubmitEdit(string freeWord)
		{
			throw null;
		}

		private void OpenListView()
		{
			throw null;
		}

		private void CloseListView()
		{
			throw null;
		}

		public MusicFilterView()
		{
			throw null;
		}
	}
}
