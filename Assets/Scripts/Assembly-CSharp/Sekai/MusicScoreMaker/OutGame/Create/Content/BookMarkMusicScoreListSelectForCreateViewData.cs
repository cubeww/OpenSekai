using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Common.MusicScoreFilterTab;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.Create.Content
{
	public sealed class BookMarkMusicScoreListSelectForCreateViewData : ContentViewDataBase
	{
		public MusicScoreFilterTabData[] FilterTabs;

		public MusicScoreListCellData[] MusicScoreCellDataArray;

		public SortOrderBy MusicScoreSortOrder;

		public bool IsFilteredMusicScoreCell;

		public int SelectedMusicScoreIndex;

		public BookMarkMusicScoreListSelectForCreateViewData()
		{
			FilterTabs = System.Array.Empty<MusicScoreFilterTabData>();
			MusicScoreCellDataArray = System.Array.Empty<MusicScoreListCellData>();
			MusicScoreSortOrder = SortOrderBy.Desc;
			SelectedMusicScoreIndex = -1;
		}
	}
}
