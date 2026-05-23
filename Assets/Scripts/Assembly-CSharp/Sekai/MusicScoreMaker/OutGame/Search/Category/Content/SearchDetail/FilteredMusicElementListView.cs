using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail
{
	public sealed class FilteredMusicElementListView : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private ListView _listView;

		[SerializeField]
		private float _offsetHeightForAdjust;

		private FilteredMusicElementData[] _filteredMusicElementDataArray;

		private bool _isNoResultsMode;

		private Action<int> _onApply;

		private const float PEEK_OFFSET_HEIGHT = 44f;

		private const int MAX_VISIBLE_COUNT_WITHOUT_SCROLL = 3;

		public void Setup(Action<int> onApply)
		{
			throw null;
		}

		public void Refresh(FilteredMusicElementData[] filteredMusicElementDataArray, string word)
		{
			throw null;
		}

		public void Show()
		{
			throw null;
		}

		public void Hide()
		{
			throw null;
		}

		private void AdjustHeight()
		{
			throw null;
		}

		private float CalculateTotalHeight()
		{
			throw null;
		}

		private void ApplyHeightIfChanged(RectTransform rectTransform, float totalHeight)
		{
			throw null;
		}

		private void OnCreateCell(ListViewItem cell, int index)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public FilteredMusicElementListView()
		{
			throw null;
		}
	}
}
