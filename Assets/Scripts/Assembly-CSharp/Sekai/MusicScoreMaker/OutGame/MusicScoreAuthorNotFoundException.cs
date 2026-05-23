using System;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreAuthorNotFoundException : Exception
	{
		public MusicScoreAuthorNotFoundException()
		{
		}

		public MusicScoreAuthorNotFoundException(string message)
			: base(message)
		{
		}

		public MusicScoreAuthorNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
