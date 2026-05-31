using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using CP;
using Newtonsoft.Json;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Common
{
	public static class CustomMusicScoreStorage
	{
		public const string DirectoryName = "CustomMusicScores";
		public const string ManifestFileName = "manifest.json";
		public const string PlayHistoryFileName = "PlayHistory.jsonl";

		private const string ShortIdChars = "0123456789abcdefghijklmnopqrstuvwxyz";

		public static string RootDirectory => Path.Combine(Application.persistentDataPath, DirectoryName);

		public static string PlayHistoryPath => Path.Combine(RootDirectory, PlayHistoryFileName);

		public static CustomMusicScoreEntry[] LoadAllEntries()
		{
			if (!Directory.Exists(RootDirectory))
			{
				return Array.Empty<CustomMusicScoreEntry>();
			}

			List<CustomMusicScoreEntry> entries = new List<CustomMusicScoreEntry>();
			foreach (string directory in Directory.GetDirectories(RootDirectory))
			{
				CustomMusicScoreEntry entry = LoadEntry(directory);
				if (entry != null)
				{
					entries.Add(entry);
				}
			}

			return entries
				.OrderByDescending(x => File.GetLastWriteTimeUtc(x.ManifestPath))
				.ThenBy(x => x.Manifest.scoreTitle, StringComparer.OrdinalIgnoreCase)
				.ToArray();
		}

		public static CustomMusicScoreEntry LoadFirstEntry()
		{
			CustomMusicScoreEntry[] entries = LoadAllEntries();
			return entries.Length > 0 ? entries[0] : null;
		}

		public static CustomMusicScoreEntry LoadEntry(string rootDirectory)
		{
			if (string.IsNullOrEmpty(rootDirectory))
			{
				return null;
			}

			string manifestPath = Path.Combine(rootDirectory, ManifestFileName);
			if (!File.Exists(manifestPath))
			{
				return null;
			}

			try
			{
				CustomMusicScoreManifest manifest = JsonConvert.DeserializeObject<CustomMusicScoreManifest>(File.ReadAllText(manifestPath));
				if (manifest == null)
				{
					return null;
				}

				manifest.Normalize();
				return new CustomMusicScoreEntry(rootDirectory, manifest);
			}
			catch (Exception exception)
			{
				LogUtility.LogWarning("Failed to load custom music score manifest. path:{0} error:{1}", manifestPath, exception.Message);
				return null;
			}
		}

		public static void SaveScore(CustomMusicScoreEntry entry, MusicScoreMakerData data)
		{
			if (entry == null || data == null)
			{
				return;
			}

			entry.SaveScore(data);
		}

		public static string GenerateShortId()
		{
			byte[] bytes = new byte[12];
			using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(bytes);
			}

			char[] chars = new char[12];
			for (int i = 0; i < chars.Length; i++)
			{
				chars[i] = ShortIdChars[bytes[i] % ShortIdChars.Length];
			}
			return new string(chars);
		}

		public static string CreateFolderName(CustomMusicScoreManifest manifest)
		{
			if (manifest == null)
			{
				return GenerateShortId();
			}

			manifest.Normalize();
			string title = SanitizeFileName(manifest.title);
			if (string.IsNullOrEmpty(title))
			{
				title = "Untitled";
			}
			return title + "_" + manifest.id;
		}

		private static string SanitizeFileName(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}

			HashSet<char> invalidChars = new HashSet<char>(Path.GetInvalidFileNameChars());
			char[] chars = value.Trim().Select(c => invalidChars.Contains(c) || char.IsWhiteSpace(c) ? '_' : c).ToArray();
			string result = new string(chars);
			while (result.Contains("__"))
			{
				result = result.Replace("__", "_");
			}
			return result.Trim('_');
		}
	}
}
