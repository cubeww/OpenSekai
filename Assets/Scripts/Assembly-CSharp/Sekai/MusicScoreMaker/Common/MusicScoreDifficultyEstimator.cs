using System;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.Common
{
	public static class MusicScoreDifficultyEstimator
	{
		private static readonly MusicDifficulty[] Difficulties;

		private const int FallbackRangeStartNumerator = 2;
		private const int FallbackRangeStartDenominator = 1;
		private const int FallbackRangeStartOffset = 0;
		private const int FallbackRangeEndNumerator = 8;
		private const int FallbackRangeEndDenominator = 1;
		private const int FallbackRangeEndOffset = 0;
		private const int FallbackComboCountMinimumValue = 1;
		private const decimal FallbackLevelDesignRatioEasy = 0.2m;
		private const decimal FallbackLevelDesignRatioNormal = 0.4m;
		private const decimal FallbackLevelDesignRatioHard = 0.6m;
		private const decimal FallbackLevelDesignRatioExpert = 0.8m;

		public static MusicDifficulty Estimate([CanBeNull] MusicScoreMakerData musicScoreData, int durationSec)
		{
			if (musicScoreData == null)
			{
				return MusicDifficulty.easy;
			}

			int[] ranges = GetLevelDesignMaxValuesFromDuration(durationSec);
			NoteAndComboCountInfo countInfo = NoteAndComboCountInfo.Calculate(musicScoreData);
			return ResolveDifficulty(countInfo.TotalComboCount, ranges);
		}

		public static int CalculateComboCountMinimum(int durationSec)
		{
			// TODO(original): read ClientConfig.MusicScoreMaker LevelDesignRangeStart* and ComboCountMinimumValue.
			double value = (double)FallbackRangeStartNumerator / FallbackRangeStartDenominator * durationSec - FallbackRangeStartOffset;
			return Math.Max((int)Math.Ceiling(value), FallbackComboCountMinimumValue);
		}

		public static int CalculateLevelDesignRangeEnd(int durationSec, int rangeStart)
		{
			// TODO(original): read ClientConfig.MusicScoreMaker LevelDesignRangeEnd*.
			double value = (double)FallbackRangeEndNumerator / FallbackRangeEndDenominator * durationSec - FallbackRangeEndOffset;
			int rangeEnd = (int)Math.Ceiling(value);
			return rangeEnd <= rangeStart ? rangeStart + 1 : rangeEnd;
		}

		public static int[] GetLevelDesignMaxValuesFromDuration(int durationSec)
		{
			int start = CalculateComboCountMinimum(durationSec);
			int end = CalculateLevelDesignRangeEnd(durationSec, start);
			return new[]
			{
				IndexRowByRatio(start, end, FallbackLevelDesignRatioEasy),
				IndexRowByRatio(start, end, FallbackLevelDesignRatioNormal),
				IndexRowByRatio(start, end, FallbackLevelDesignRatioHard),
				IndexRowByRatio(start, end, FallbackLevelDesignRatioExpert)
			};
		}

		private static int IndexRowByRatio(int start, int end, decimal ratio)
		{
			int count = end - start + 1;
			if (count < 1)
			{
				return start;
			}
			return start + (int)Math.Ceiling(count * ratio) - 1;
		}

		private static MusicDifficulty ResolveDifficulty(int totalCombo, int[] ranges)
		{
			if (totalCombo < 1)
			{
				return MusicDifficulty.easy;
			}
			if (ranges == null || ranges.Length == 0)
			{
				return MusicDifficulty.master;
			}

			for (int i = 0; i < Difficulties.Length && i < ranges.Length; i++)
			{
				if (ranges[i] >= totalCombo)
				{
					return Difficulties[i];
				}
			}
			return MusicDifficulty.master;
		}

		static MusicScoreDifficultyEstimator()
		{
			Difficulties = new[]
			{
				MusicDifficulty.easy,
				MusicDifficulty.normal,
				MusicDifficulty.hard,
				MusicDifficulty.expert
			};
		}
	}
}
