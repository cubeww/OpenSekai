using System;
using System.IO;
using Newtonsoft.Json;
using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Common
{
	public static class CustomMusicScorePlayHistoryStorage
	{
		public static void AppendLiveResult(FreeLiveBootData bootData, LiveScore score, long musicTimeMs, long musicLengthMs)
		{
			if (bootData == null || bootData.IsAuto || bootData.IsCustomMusicScore != true || bootData.MusicData?.IsTestPlay == true)
			{
				return;
			}

			if (string.IsNullOrEmpty(bootData.CustomMusicScoreId))
			{
				return;
			}

			try
			{
				Directory.CreateDirectory(CustomMusicScoreStorage.RootDirectory);
				CustomMusicScoreEntry entry = CustomMusicScoreStorage.LoadEntry(bootData.CustomMusicScorePath);
				CustomMusicScoreManifest manifest = entry?.Manifest;
				CustomMusicScorePlayHistoryRecord record = CreateRecord(bootData, manifest, score, musicTimeMs, musicLengthMs);
				File.AppendAllText(CustomMusicScoreStorage.PlayHistoryPath, JsonConvert.SerializeObject(record) + Environment.NewLine);
			}
			catch (Exception exception)
			{
				Debug.LogWarningFormat("Failed to append custom music score play history. path:{0} error:{1}", CustomMusicScoreStorage.PlayHistoryPath, exception.Message);
			}
		}

		public static bool TryLoadBestResult(string customMusicScoreId, out CustomMusicScorePlayHistoryRecord bestRecord)
		{
			bestRecord = null;
			if (string.IsNullOrEmpty(customMusicScoreId) || !File.Exists(CustomMusicScoreStorage.PlayHistoryPath))
			{
				return false;
			}

			try
			{
				foreach (string line in File.ReadLines(CustomMusicScoreStorage.PlayHistoryPath))
				{
					if (string.IsNullOrWhiteSpace(line))
					{
						continue;
					}

					CustomMusicScorePlayHistoryRecord record = JsonConvert.DeserializeObject<CustomMusicScorePlayHistoryRecord>(line);
					if (record == null || !string.Equals(record.customMusicScoreId, customMusicScoreId, StringComparison.Ordinal))
					{
						continue;
					}

					if (bestRecord == null || IsBetterResult(record, bestRecord))
					{
						bestRecord = record;
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogWarningFormat("Failed to load custom music score play history. path:{0} error:{1}", CustomMusicScoreStorage.PlayHistoryPath, exception.Message);
				bestRecord = null;
			}

			return bestRecord != null;
		}

		private static bool IsBetterResult(CustomMusicScorePlayHistoryRecord current, CustomMusicScorePlayHistoryRecord best)
		{
			int comparison = current.totalScore.CompareTo(best.totalScore);
			if (comparison != 0)
			{
				return comparison > 0;
			}

			comparison = current.maxCombo.CompareTo(best.maxCombo);
			if (comparison != 0)
			{
				return comparison > 0;
			}

			comparison = current.perfectCount.CompareTo(best.perfectCount);
			if (comparison != 0)
			{
				return comparison > 0;
			}

			comparison = current.missCount.CompareTo(best.missCount);
			if (comparison != 0)
			{
				return comparison < 0;
			}

			comparison = current.badCount.CompareTo(best.badCount);
			if (comparison != 0)
			{
				return comparison < 0;
			}

			comparison = current.goodCount.CompareTo(best.goodCount);
			if (comparison != 0)
			{
				return comparison < 0;
			}

			return string.CompareOrdinal(current.playedAtUtc, best.playedAtUtc) > 0;
		}

		private static CustomMusicScorePlayHistoryRecord CreateRecord(FreeLiveBootData bootData, CustomMusicScoreManifest manifest, LiveScore score, long musicTimeMs, long musicLengthMs)
		{
			bool isAllPerfect = score.totalComboCount > 0
				&& score.totalComboCount == score.perfectCount
				&& score.greatCount == 0
				&& score.goodCount == 0
				&& score.badCount == 0
				&& score.missCount == 0;
			bool isFullCombo = score.totalComboCount > 0 && score.maxCombo >= score.totalComboCount;
			string clearType = isAllPerfect ? "all_perfect" : isFullCombo ? "full_combo" : score.life > 0 ? "clear" : "life_zero";

			return new CustomMusicScorePlayHistoryRecord
			{
				schemaVersion = 1,
				recordId = Guid.NewGuid().ToString("N"),
				playedAtUtc = DateTime.UtcNow.ToString("O"),
				appVersion = Application.version,
				livePlayMode = bootData.LivePlayMode.ToString(),
				customMusicScoreId = bootData.CustomMusicScoreId,
				customMusicScorePath = bootData.CustomMusicScorePath ?? string.Empty,
				title = bootData.MusicData?.Music?.title ?? manifest?.title ?? string.Empty,
				scoreTitle = bootData.CustomMusicScoreTitle ?? manifest?.scoreTitle ?? string.Empty,
				authorName = bootData.CustomMusicScoreAuthorName ?? manifest?.userName ?? string.Empty,
				difficulty = bootData.MusicData?.DifficultyString ?? manifest?.musicDifficultyType ?? string.Empty,
				playLevel = bootData.MusicData?.PlayLevel ?? manifest?.playLevel ?? 0,
				scoreFileName = manifest?.scoreFileName ?? string.Empty,
				audioFileName = manifest?.audioFileName ?? string.Empty,
				jacketFileName = manifest?.jacketFileName ?? string.Empty,
				musicTimeMs = musicTimeMs,
				musicLengthMs = musicLengthMs,
				totalScore = score.totalScore,
				technicalScore = score.technicalScore,
				rank = score.rank.ToString(),
				life = score.life,
				clearType = clearType,
				isFullCombo = isFullCombo,
				isAllPerfect = isAllPerfect,
				maxCombo = score.maxCombo,
				totalComboCount = score.totalComboCount,
				justPerfectCount = score.justPerfectCount,
				perfectCount = score.perfectCount,
				greatCount = score.greatCount,
				goodCount = score.goodCount,
				badCount = score.badCount,
				missCount = score.missCount,
				fastCount = score.fastCount,
				lateCount = score.lateCount,
				flickMissCount = score.flickCount,
				autoCount = score.autoCount,
				noteSpeed = bootData.LiveSettingData?.NoteSpeed ?? 0f,
				timingAdjust = bootData.LiveSettingData?.TimingAdjustData ?? 0f,
				noteSkinIndex = bootData.LiveSettingData?.NoteSkinIndex ?? 0
			};
		}
	}

	public sealed class CustomMusicScorePlayHistoryRecord
	{
		public int schemaVersion;
		public string recordId;
		public string playedAtUtc;
		public string appVersion;
		public string livePlayMode;
		public string customMusicScoreId;
		public string customMusicScorePath;
		public string title;
		public string scoreTitle;
		public string authorName;
		public string difficulty;
		public int playLevel;
		public string scoreFileName;
		public string audioFileName;
		public string jacketFileName;
		public long musicTimeMs;
		public long musicLengthMs;
		public int totalScore;
		public int technicalScore;
		public string rank;
		public int life;
		public string clearType;
		public bool isFullCombo;
		public bool isAllPerfect;
		public int maxCombo;
		public int totalComboCount;
		public int justPerfectCount;
		public int perfectCount;
		public int greatCount;
		public int goodCount;
		public int badCount;
		public int missCount;
		public int fastCount;
		public int lateCount;
		public int flickMissCount;
		public int autoCount;
		public float noteSpeed;
		public float timingAdjust;
		public int noteSkinIndex;
	}
}
