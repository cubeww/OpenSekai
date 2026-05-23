using System;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class AutoSaveMusicScoreLoadResult
	{
		public string FileName { get; }

		public DateTime LastWriteTime { get; }

		public MusicScoreMakerData Data { get; }

		[CanBeNull]
		public string BaseMusicScoreId { get; }

		public int BaseMusicDifficultyId { get; }

		public AutoSaveMusicScoreLoadResult(string fileName, DateTime lastWriteTime, [NotNull] MusicScoreMakerData data, [CanBeNull] string baseMusicScoreId, int baseMusicDifficultyId = -1)
		{
			FileName = fileName;
			LastWriteTime = lastWriteTime;
			Data = data;
			BaseMusicScoreId = baseMusicScoreId;
			BaseMusicDifficultyId = baseMusicDifficultyId;
		}
	}
}
