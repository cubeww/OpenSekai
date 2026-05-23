using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.Common.PublishedMusicScoreList
{
	public sealed class PublishedMusicScoreListViewData : ContentViewDataBase
	{
		public Defines.ContentType ContentType;

		public MusicScoreListCellData[] MusicScoreCellDataArray;

		public SortOrderBy MusicScoreSortOrder;

		public MusicScoreData SelectedMusicScoreData;

		public bool IsFilteredMusicScoreCell
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

		public bool EnableDerivativeDisallowedIndicator
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public void SetEnableDerivativeDisallowedIndicator(bool enable)
		{
			throw null;
		}

		public int GetSelectedMusicScoreIndex()
		{
			throw null;
		}

		public PublishedMusicScoreListViewData()
		{
			throw null;
		}
	}
}
