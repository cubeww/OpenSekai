using System;
using Cysharp.Threading.Tasks;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public class SearchDetailView : ContentViewBase<SearchDetailViewData>, IDisposable
	{
		[SerializeField]
		private MusicFilterView _musicFilterView;

		[SerializeField]
		private MusicScoreTagFilterView _tagFilterView;

		[SerializeField]
		private CustomIndexToggleGroup _musicDifficultyToggleGroup;

		[SerializeField]
		private UIPartsMusicDifficultyLabel[] _musicDifficultyLabels;

		[SerializeField]
		private CustomIndexToggleGroup _sortCategoryToggleGroup;

		[SerializeField]
		private CustomButton _searchButton;

		[SerializeField]
		private CustomButton _musicScoreIdSearchButton;

		public void Setup(Action onFocusMusicFilter, Action<string> onValueChangedMusicFilter, Action<string> onSubmitEditMusicFilter, Action<int> onFilteredMusic, Action onClearMusicFilter, Action onFilterTag, Action onClearTag, Action<int> onSelectedMusicDifficulty, Action<int> onSelectedSortCategory, Action onDecidedSearch, Action onMusicScoreIdSearchButtonClicked)
		{
			throw null;
		}

		private void SetupMusicDifficultyLabels()
		{
			throw null;
		}

		public void ApplyFilteredMusics()
		{
			throw null;
		}

		public void ApplyFilteredMusic()
		{
			throw null;
		}

		public void ClearMusicFilter()
		{
			throw null;
		}

		public void RefreshTagFilter()
		{
			throw null;
		}

		public override UniTask RefreshAsync()
		{
			throw null;
		}

		public override void Dispose()
		{
			throw null;
		}

		public SearchDetailView()
		{
			throw null;
		}
	}
}
