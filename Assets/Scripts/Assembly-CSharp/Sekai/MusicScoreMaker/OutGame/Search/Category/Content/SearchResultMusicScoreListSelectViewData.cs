using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public sealed class SearchResultMusicScoreListSelectViewData : ContentViewDataBase
	{
		public MusicScoreListCellData[] MusicScoreCellDataArray;

		public int SelectedMusicScoreIndex;

		public UIPartsSelectorCell.ViewData[] RankingFilterTabViewDataArray;

		public bool IsFilteredMusicScoreCell;

		public MusicScoreSearchCondition SearchCondition
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

		public bool IsRankingFilterVisible
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

		public SearchResultMusicScoreListSelectViewData()
		{
			throw null;
		}
	}
}
