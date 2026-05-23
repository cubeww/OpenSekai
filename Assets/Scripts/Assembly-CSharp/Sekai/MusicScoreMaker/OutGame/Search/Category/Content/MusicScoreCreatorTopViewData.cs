using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public sealed class MusicScoreCreatorTopViewData : ContentViewDataBase
	{
		public MusicScoreListCellData[] MusicScoreCellDataArray;

		public int SelectedMusicScoreIndex;

		public MusicScoreCreatorInfoData CreatorInfoData;

		public SortOrderBy MusicScoreSortOrder;

		public bool IsFilteredMusicScoreCell;

		public MusicScoreCreatorTopViewData()
		{
			SelectedMusicScoreIndex = -1;
			MusicScoreSortOrder = SortOrderBy.Desc;
		}
	}
}
