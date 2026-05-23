namespace Sekai.MusicScoreMaker.OutGame.Publish
{
	public sealed class MusicScorePublishCompletedViewData
	{
		public readonly string MusicScoreTitle;

		public readonly string MusicTitle;

		public readonly MusicDifficulty Difficulty;

		public readonly int PlayLevel;

		public readonly string MusicJacketAssetBundleName;

		public readonly string MusicJacketFileName;

		public string MusicScoreId { get; set; }

		public MusicScorePublishCompletedViewData(string musicScoreId, string musicScoreTitle, string musicTitle, MusicDifficulty difficulty, int playLevel, string musicJacketAssetBundleName, string musicJacketFileName)
		{
			MusicScoreId = musicScoreId;
			MusicScoreTitle = musicScoreTitle;
			MusicTitle = musicTitle;
			Difficulty = difficulty;
			PlayLevel = playLevel;
			MusicJacketAssetBundleName = musicJacketAssetBundleName;
			MusicJacketFileName = musicJacketFileName;
		}
	}
}
