using System;
using System.IO;
using System.Linq;
using System.Threading;
using Beebyte.Obfuscator;
using CP;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.MusicScoreMaker.OutGame;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Common
{
	[Skip]
	public class MusicScoreMakerRepository
	{
		public const string AutoSaveFilePrefix = "autosave_";

		private static readonly string AutoSavePattern;

		private static readonly string SavePathBase;

		private const string TimestampFormat = "yyyyMMdd_HHmmss_fff";

		public MusicScoreMakerData MusicScoreMakerData { get; set; }

		[CanBeNull]
		public string BaseMusicScoreId { get; set; }

		public int BaseMusicDifficultyId { get; set; }

		public MusicScoreMakerRepository()
		{
			MusicScoreMakerData = null;
			BaseMusicScoreId = null;
			BaseMusicDifficultyId = -1;
		}

		public static MusicScoreMakerRepository LoadFromStorage(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				LogUtility.LogError("MusicScoreMakerRepository.LoadFromStorage: fileName is null or empty.");
				return new MusicScoreMakerRepository();
			}

			string path = BuildFilePath(fileName);
			if (!File.Exists(path))
			{
				return new MusicScoreMakerRepository();
			}

			MusicScoreMakerRepository repository = DeepCopyHelper.FromJson<MusicScoreMakerRepository>(File.ReadAllText(path));
			if (repository == null)
			{
				return new MusicScoreMakerRepository();
			}

			repository.MusicScoreMakerData?.MigrateToCurrentVersion();
			repository.MusicScoreMakerData?.InitializeIdCount();
			return repository;
		}

		[Skip]
		public static void SaveToStorage(MusicScoreMakerRepository data, string fileName)
		{
			if (data == null)
			{
				LogUtility.LogError("MusicScoreMakerRepository.SaveToStorage: data is null.");
				return;
			}
			if (string.IsNullOrEmpty(fileName))
			{
				LogUtility.LogError("MusicScoreMakerRepository.SaveToStorage: fileName is null or empty.");
				return;
			}

			Directory.CreateDirectory(SavePathBase);
			File.WriteAllText(BuildFilePath(fileName), DeepCopyHelper.ToJsonString(data));

			if (Path.GetFileName(fileName).StartsWith(AutoSaveFilePrefix, StringComparison.Ordinal))
			{
				DeleteOldestAutoSaveFiles(GetAutoSaveKeepCount());
			}
		}

		public static void Delete(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				LogUtility.LogError("MusicScoreMakerRepository.Delete: fileName is null or empty.");
				return;
			}

			string path = BuildFilePath(fileName);
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		public static async UniTask<int> DeleteAllAsync(CancellationToken cancellationToken = default)
		{
			int deleteCount = 0;
			foreach ((string fileName, _) in GetFileInfos(AutoSavePattern))
			{
				cancellationToken.ThrowIfCancellationRequested();
				Delete(fileName);
				deleteCount++;
				await UniTask.Yield(cancellationToken);
			}
			return deleteCount;
		}

		public static string GenerateAutoSaveFileName()
		{
			return AutoSaveFilePrefix + DateTime.Now.ToString(TimestampFormat);
		}

		public static (string, DateTime)[] GetFileInfos(string pattern = "*")
		{
			if (string.IsNullOrEmpty(pattern))
			{
				LogUtility.LogError("MusicScoreMakerRepository.GetFileInfos: pattern is null or empty.");
				return Array.Empty<(string, DateTime)>();
			}
			if (!Directory.Exists(SavePathBase))
			{
				return Array.Empty<(string, DateTime)>();
			}

			string searchPattern = pattern.EndsWith(".json", StringComparison.OrdinalIgnoreCase) ? pattern : pattern + ".json";
			return Directory.GetFiles(SavePathBase, searchPattern)
				.Select(path => (Path.GetFileName(path), File.GetLastWriteTime(path)))
				.OrderByDescending(info => info.Item2)
				.ToArray();
		}

		private static string BuildFilePath(string fileName)
		{
			string safeFileName = Path.GetFileName(fileName);
			if (string.IsNullOrEmpty(safeFileName))
			{
				safeFileName = "music_score.json";
			}
			if (!safeFileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
			{
				safeFileName += ".json";
			}
			return Path.Combine(SavePathBase, safeFileName);
		}

		private static void DeleteOldestAutoSaveFiles(int keepCount)
		{
			if (keepCount < 0)
			{
				keepCount = 0;
			}

			foreach ((string fileName, _) in GetFileInfos(AutoSavePattern).Skip(keepCount))
			{
				Delete(fileName);
			}
		}

		private static int GetAutoSaveKeepCount()
		{
			return Defines.AutoSaveMusicScoreMaxCount;
		}

		static MusicScoreMakerRepository()
		{
			AutoSavePattern = AutoSaveFilePrefix + "*";
			SavePathBase = Path.Combine(Application.persistentDataPath, "music_score_maker");
		}
	}
}
