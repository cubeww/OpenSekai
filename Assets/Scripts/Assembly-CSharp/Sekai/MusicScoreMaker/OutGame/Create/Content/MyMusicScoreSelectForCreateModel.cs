using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Common.PublishedMusicScoreList;

namespace Sekai.MusicScoreMaker.OutGame.Create.Content
{
	public sealed class MyMusicScoreSelectForCreateModel : ContentModelBase<MyMusicScoreSelectForCreateViewData>
	{
		private readonly PublishedMusicScoreListModel _publishedListModel;

		public MusicScoreFilterData MusicScoreFilterData
		{
			get
			{
				throw null;
			}
		}

		public MyMusicScoreSelectForCreateModel()
		{
			throw null;
		}

		public void Setup()
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

		public void ApplySortOrder(SortOrderBy sortOrder)
		{
			throw null;
		}

		public void ApplyContentType(Defines.ContentType contentType)
		{
			throw null;
		}

		public void ApplyFilter(MusicScoreFilterData filterData)
		{
			throw null;
		}
	}
}
