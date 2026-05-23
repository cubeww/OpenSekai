namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public sealed class MusicScoreSearchCondition
	{
		public readonly int MusicId;

		public readonly int MusicScoreTagId;

		public readonly MusicDifficulty Difficulty;

		public readonly SearchDetailDefines.SortCategoryType SortCategory;

		public MusicScoreSearchCondition(int musicId = -1, int musicScoreTagId = -1, MusicDifficulty difficulty = MusicDifficulty.none, SearchDetailDefines.SortCategoryType sortCategory = SearchDetailDefines.SortCategoryType.Popular)
		{
			throw null;
		}
	}
}
