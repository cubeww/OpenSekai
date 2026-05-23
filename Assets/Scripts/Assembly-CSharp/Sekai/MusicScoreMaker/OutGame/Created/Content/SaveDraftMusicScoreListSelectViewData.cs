using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.Created.Content
{
	public sealed class SaveDraftMusicScoreListSelectViewData : ContentViewDataBase
	{
		public SaveDraftMusicScoreSlotCellData[] SlotCellDataArray;

		public bool IsSaveButtonEnabled;

		public bool IsSaveButtonActive;

		public bool IsDeleteButtonEnabled;

		public bool IsLoadButtonEnabled;

		public bool IsLoadButtonActive;

		public int SavedCount
		{
			get
			{
				throw null;
			}
		}

		public SaveDraftMusicScoreListSelectViewData()
		{
			throw null;
		}
	}
}
