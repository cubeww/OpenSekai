using System;

namespace Sekai.MusicScoreMaker.OutGame.Publish
{
	public class CustomMusicScoreDerivationWorkNotUpdatedException : Exception
	{
		private const string WORDING_KEY = "MSG_MSM_CUSTOM_MUSIC_SCORE_DERIVATION_WORK_NOT_UPDATED";

		public CustomMusicScoreDerivationWorkNotUpdatedException()
			: base(WORDING_KEY)
		{
		}

		public string GetLocalizedMessage()
		{
			// TODO(original): call WordingManager.Get(WORDING_KEY) once WordingManager is restored.
			return WORDING_KEY;
		}
	}
}
