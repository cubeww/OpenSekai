namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class MusicCellData
	{
		public readonly MasterMusicAllModel MusicData;

		public readonly bool IsReleased;

		public MusicCellData(MasterMusicAllModel musicData, bool isReleased)
		{
			MusicData = musicData;
			IsReleased = isReleased;
		}
	}
}
