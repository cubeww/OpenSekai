using System.Collections.Generic;
using Sekai.ApiData;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public sealed class SaveDraftResult
	{
		public SaveDraftResultType ResultType { get; private set; }

		public IEnumerable<UserCustomMusicScoreDraft> UserMusicScoreDrafts { get; private set; }

		public SaveDraftResult(SaveDraftResultType resultType, IEnumerable<UserCustomMusicScoreDraft> userMusicScoreDrafts)
		{
			UserMusicScoreDrafts = userMusicScoreDrafts;
			ResultType = resultType;
		}
	}
}
