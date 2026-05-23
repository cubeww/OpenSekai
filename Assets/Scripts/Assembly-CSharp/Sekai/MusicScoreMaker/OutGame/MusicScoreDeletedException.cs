using System;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreDeletedException : Exception
	{
		public MusicScoreDeletedException()
		{
		}

		public MusicScoreDeletedException(string message)
			: base(message)
		{
		}

		public MusicScoreDeletedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
