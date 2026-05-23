using JetBrains.Annotations;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScorePreviewPlayData
	{
		public readonly float StartTimeSec;

		public readonly MusicScoreMakerData MusicScoreData;

		public MusicScorePreviewPlayData(float startTimeSec, [NotNull] MusicScoreMakerData musicScoreData)
		{
			StartTimeSec = startTimeSec;
			MusicScoreData = musicScoreData;
		}
	}
}
