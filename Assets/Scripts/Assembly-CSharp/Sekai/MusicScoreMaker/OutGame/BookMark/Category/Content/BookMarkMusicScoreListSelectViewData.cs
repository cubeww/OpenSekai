using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Common.MusicScoreFilterTab;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.BookMark.Category.Content
{
	public sealed class BookMarkMusicScoreListSelectViewData : ContentViewDataBase
	{
		public MusicScoreFilterTabData[] FilterTabs;

		public MusicScoreListCellData[] MusicScoreCellDataArray;

		public SortOrderBy MusicScoreSortOrder;

		public bool IsFilteredMusicScoreCell;

		public int SelectedMusicScoreIndex;

		public BookMarkMusicScoreListSelectViewData()
		{
			FilterTabs = System.Array.Empty<MusicScoreFilterTabData>();
			MusicScoreCellDataArray = System.Array.Empty<MusicScoreListCellData>();
			MusicScoreSortOrder = SortOrderBy.Desc;
			SelectedMusicScoreIndex = -1;
		}
	}
}
