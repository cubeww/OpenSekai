using System;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreAlreadyReviewedException : Exception
	{
		public MusicScoreAlreadyReviewedException()
		{
		}

		public MusicScoreAlreadyReviewedException(string message)
			: base(message)
		{
		}
	}
}
