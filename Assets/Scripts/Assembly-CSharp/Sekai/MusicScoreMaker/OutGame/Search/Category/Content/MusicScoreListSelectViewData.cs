using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public sealed class MusicScoreListSelectViewData : ContentViewDataBase
	{
		public MusicScoreListCellData[] MusicScoreCellDataArray;

		public int SelectedMusicScoreIndex;

		public bool IsFilteredMusicScoreCell;

		public MusicScoreListSelectViewData()
		{
			throw null;
		}
	}
}
