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

		private const string ShortIdChars = "0123456789abcdefghijklmnopqrstuvwxyz";

		public static string RootDirectory => Path.Combine(Application.persistentDataPath, DirectoryName);

		public static CustomMusicScorePackage[] LoadAllPackages()
		{
			if (!Directory.Exists(RootDirectory))
			{
				return Array.Empty<CustomMusicScorePackage>();
			}

			List<CustomMusicScorePackage> packages = new List<CustomMusicScorePackage>();
			foreach (string directory in Directory.GetDirectories(RootDirectory))
			{
				CustomMusicScorePackage package = LoadPackage(directory);
				if (package != null)
				{
					packages.Add(package);
				}
			}

			return packages
				.OrderByDescending(x => File.GetLastWriteTimeUtc(x.ManifestPath))
				.ThenBy(x => x.Manifest.scoreTitle, StringComparer.OrdinalIgnoreCase)
				.ToArray();
		}

		public static CustomMusicScorePackage LoadFirstPackage()
		{
			CustomMusicScorePackage[] packages = LoadAllPackages();
			return packages.Length > 0 ? packages[0] : null;
		}

		public static CustomMusicScorePackage LoadPackage(string rootDirectory)
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
				return new CustomMusicScorePackage(rootDirectory, manifest);
			}
			catch (Exception exception)
			{
				LogUtility.LogWarning("Failed to load custom music score manifest. path:{0} error:{1}", manifestPath, exception.Message);
				return null;
			}
		}

		public static void SaveScore(CustomMusicScorePackage package, MusicScoreMakerData data)
		{
			if (package == null || data == null)
			{
				return;
			}

			package.SaveScore(data);
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
