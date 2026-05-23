using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Common.MusicScoreFilterTab;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public sealed class RankingMusicScoreListSelectViewData : ContentViewDataBase
	{
		public MusicScoreFilterTabData[] FilterTabs;

		public UIPartsSelectorCell.ViewData[] RankingFilterTabViewDataArray;

		public RankingMusicScoreListCellData[] MusicScoreCellDataArray;

		public int SelectedMusicScoreIndex;

		public bool IsFilteredMusicScoreCell;

		public int RankingFilterIndex
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public RankingMusicScoreListSelectViewData()
		{
			throw null;
		}
	}
}
