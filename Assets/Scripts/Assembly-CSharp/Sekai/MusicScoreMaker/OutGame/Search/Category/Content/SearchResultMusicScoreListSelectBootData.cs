using Sekai.MusicScoreMaker.OutGame.Common.Content;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public class SearchResultMusicScoreListSelectBootData : IContentBootData
	{
		public readonly MusicScoreSearchCondition Condition;

		public SearchResultMusicScoreListSelectBootData(int musicId = -1, int musicScoreTagId = -1, MusicDifficulty difficulty = MusicDifficulty.none, SearchDetailDefines.SortCategoryType sortCategory = SearchDetailDefines.SortCategoryType.Popular)
		{
			throw null;
		}
	}
}
