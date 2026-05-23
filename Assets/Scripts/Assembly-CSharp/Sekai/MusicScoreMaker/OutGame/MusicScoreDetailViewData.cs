namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreDetailViewData
	{
		public readonly MusicScoreDetailInfoViewData DetailInfoViewData;

		public readonly MusicScorePreviewInfoViewData PreviewInfoViewData;

		public MusicScoreDetailViewData()
		{
			DetailInfoViewData = new MusicScoreDetailInfoViewData();
			PreviewInfoViewData = new MusicScorePreviewInfoViewData();
		}
	}
}
