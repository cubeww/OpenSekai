using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Common.MusicScoreFilterTab;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public sealed class NewArrivalMusicScoreListSelectViewData : ContentViewDataBase
	{
		public MusicScoreFilterTabData[] FilterTabs;

		public MusicScoreListCellData[] MusicScoreCellDataArray;

		public int SelectedMusicScoreIndex;

		public bool IsFilteredMusicScoreCell;

		public NewArrivalMusicScoreListSelectViewData()
		{
			throw null;
		}
	}
}
