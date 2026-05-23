using System;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreBannedException : Exception
	{
		public MusicScoreBannedException()
		{
		}

		public MusicScoreBannedException(string message)
			: base(message)
		{
		}

		public MusicScoreBannedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
