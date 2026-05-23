using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.MusicScoreMaker.OutGame.Created.Content;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public sealed class SaveDraftScreenViewData
	{
		public readonly CategoryTabData[] TabDataArray;

		public readonly SaveDraftMusicScoreListSelectViewData ListSelectViewData;

		public SaveDraftScreenViewData(CategoryTabData[] tabDataArray, SaveDraftMusicScoreListSelectViewData listSelectViewData)
		{
			TabDataArray = tabDataArray;
			ListSelectViewData = listSelectViewData;
		}
	}
}
