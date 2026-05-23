namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class BookmarkedMusicScoreData
	{
		public MusicScoreData MusicScoreData { get; }

		public long BookmarkedAt { get; }

		public BookmarkedMusicScoreData(MusicScoreData musicScoreData, long bookmarkedAt)
		{
			MusicScoreData = musicScoreData;
			BookmarkedAt = bookmarkedAt;
		}
	}
}
