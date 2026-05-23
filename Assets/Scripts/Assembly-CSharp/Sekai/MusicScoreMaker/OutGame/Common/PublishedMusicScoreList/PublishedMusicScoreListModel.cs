using JetBrains.Annotations;

namespace Sekai.MusicScoreMaker.OutGame.Common.PublishedMusicScoreList
{
	public sealed class PublishedMusicScoreListModel
	{
		private MusicScoreData[] _musicScoreDataArray;

		private string _selectedMusicScoreId;

		private readonly MusicScoreFilterEvaluator _filterEvaluator;

		private MusicScoreFilterData _musicScoreFilterData;

		private readonly PublishedMusicScoreListViewData _viewData;

		public MusicScoreFilterData MusicScoreFilterData
		{
			get
			{
				throw null;
			}
		}

		public PublishedMusicScoreListModel(PublishedMusicScoreListViewData viewData)
		{
			throw null;
		}

		public void Setup()
		{
			throw null;
		}

		public void RebuildMusicScoreListData()
		{
			throw null;
		}

		public void ApplyFilter(MusicScoreFilterData filterData)
		{
			throw null;
		}

		public void ApplySelectedMusicScore(int index)
		{
			throw null;
		}

		[CanBeNull]
		public MusicScoreData GetSelectedMusicScoreData()
		{
			throw null;
		}

		public void ApplyContentType(Defines.ContentType contentType)
		{
			throw null;
		}

		public void ApplySortOrder(SortOrderBy sortOrder)
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

		private void ResetSelectedMusicScore()
		{
			throw null;
		}

		private void ApplyPreviewInfoViewData()
		{
			throw null;
		}
	}
}
