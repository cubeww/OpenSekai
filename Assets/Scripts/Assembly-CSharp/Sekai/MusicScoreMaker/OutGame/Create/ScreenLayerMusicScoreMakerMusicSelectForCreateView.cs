using System;
using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.MusicScoreMaker.Outgame;
using Sekai.MusicShop;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Create
{
	public class ScreenLayerMusicScoreMakerMusicSelectForCreateView : MonoBehaviour
	{
		[SerializeField]
		private MusicSelectUnitTabList _unitTabList;

		[SerializeField]
		private FreeWordFilterContent _freeWordFilterContent;

		[SerializeField]
		private ListView _listView;

		[SerializeField]
		private CustomTextMesh _notFoundText;

		[SerializeField]
		private UIPartsFilterButton _filterButton;

		[SerializeField]
		private MusicSelectSortDropdown _sortDropdown;

		[SerializeField]
		private UIPartsSortOrder _sortOrder;

		[SerializeField]
		private ScreenBackground _screenBackground;

		private Action<MusicCellData> _onClickMusicCell;

		private MusicCellData[] _musicCellDataArray;

		public void Setup(ScreenLayerMusicScoreMakerMusicSelectForCreateModel model, Action<CategoryTabType> onSelectMusicCategory, Action<string> onFreeWordFiltered, Action<MusicCellData> onClickMusicCell, Action onClickFilterButton, Action<SortType> onSortTypeChanged, Action<SortOrderBy> onSortOrderChanged)
		{
			throw null;
		}

		public void RefreshList(ScreenLayerMusicScoreMakerMusicSelectForCreateModel model, bool resetScrollPosition = true)
		{
			throw null;
		}

		public void UpdateFilterButtonState(bool isFiltered)
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

		public ScreenLayerMusicScoreMakerMusicSelectForCreateView()
		{
			throw null;
		}
	}
}
