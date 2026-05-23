using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common.Content;

namespace Sekai.MusicScoreMaker.OutGame.Create.Content
{
	public sealed class BookMarkMusicScoreListSelectForCreateModel : ContentModelBase<BookMarkMusicScoreListSelectForCreateViewData>
	{
		private BookmarkedMusicScoreData[] _musicScoreDataArray;

		private BookmarkedMusicScoreData[] _filteredMusicScoreDataArray;

		private string _selectedMusicScoreId;

		private readonly MusicScoreFilterEvaluator _filterEvaluator;

		private MusicScoreFilterData _musicScoreFilterData;

		private SortOrderBy _sortOrder;

		public MusicDifficulty SelectedMusicDifficulty
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public MusicScoreFilterData MusicScoreFilterData
		{
			get
			{
				throw null;
			}
		}

		public void Setup()
		{
			throw null;
		}

		public void ApplySelectedMusicScore(int index)
		{
			throw null;
		}

		public void ApplyFilter(int tabIndex)
		{
			throw null;
		}

		public void ApplyFilter(MusicScoreFilterData result)
		{
			throw null;
		}

		public void ApplySortOrder(SortOrderBy sortOrder)
		{
			throw null;
		}

		[CanBeNull]
		public MusicScoreData GetSelectedMusicScoreData()
		{
			throw null;
		}

		public void RemoveMusicScore(string id)
		{
			throw null;
		}

		private void CreateMusicScoreDataArray()
		{
			throw null;
		}

		private void ApplyMusicScoreCellDataArray()
		{
			throw null;
		}

		private void ApplySortOrderToCellDataArray()
		{
			throw null;
		}

		private void ApplySelectedMusicScore()
		{
			throw null;
		}

		private void CreateFilterTabs()
		{
			throw null;
		}

		private void ApplySelectedIndexToViewData()
		{
			throw null;
		}

		public BookMarkMusicScoreListSelectForCreateModel()
		{
			throw null;
		}
	}
}
