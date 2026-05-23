using System;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreNotFoundException : Exception
	{
		public MusicScoreNotFoundException()
		{
		}

		public MusicScoreNotFoundException(string message)
			: base(message)
		{
		}

		public MusicScoreNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
