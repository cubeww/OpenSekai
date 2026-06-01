using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Newtonsoft.Json;
using Sekai.MusicScoreMaker.Common;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.SUS;
using UnityEngine;

namespace Sekai.CustomMusicScoreManager
{
	public static class CustomMusicScoreManagerService
	{
		private const string ExportDirectoryName = "Exports";

		private const int JacketTextureSize = 740;

		private static readonly HashSet<string> AudioExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			".ogg",
			".mp3",
			".wav"
		};

		private static readonly HashSet<string> JacketExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			".png",
			".jpg",
			".jpeg"
		};

		private static readonly HashSet<string> ScoreExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			".json",
			".txt",
			".sus"
		};

		private static readonly HashSet<string> VideoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			".mp4"
		};

		public static IReadOnlyList<CustomMusicScoreManagerItem> LoadItems()
		{
			Directory.CreateDirectory(CustomMusicScoreStorage.RootDirectory);
			List<CustomMusicScoreManagerItem> items = new List<CustomMusicScoreManagerItem>();
			foreach (string directory in Directory.GetDirectories(CustomMusicScoreStorage.RootDirectory))
			{
				CustomMusicScoreEntry entry = CustomMusicScoreStorage.LoadEntry(directory);
				if (entry == null)
				{
					continue;
				}

				items.Add(CreateItem(entry));
			}

			return items
				.OrderByDescending(x => x.LastWriteTime)
				.ThenBy(x => x.Entry.Manifest.scoreTitle, StringComparer.OrdinalIgnoreCase)
				.ToArray();
		}

		public static CustomMusicScoreEntry CreateNewEntry()
		{
			Directory.CreateDirectory(CustomMusicScoreStorage.RootDirectory);
			CustomMusicScoreManifest manifest = new CustomMusicScoreManifest
			{
				id = CustomMusicScoreStorage.GenerateShortId(),
				title = "未命名",
				scoreTitle = "未命名谱面",
				userName = Environment.UserName,
				composer = string.Empty,
				lyricist = string.Empty,
				arranger = string.Empty,
				singer = string.Empty,
				collaborationLabel = string.Empty,
				audioFileName = "audio.ogg",
				jacketFileName = "jacket.png",
				scoreFileName = "score.json",
				videoFileName = string.Empty,
				fillerSec = 0f,
				secForMusicScoreMaker = 120,
				previewStartTimeSec = 0f,
				musicDifficultyType = "master",
				playLevel = 1
			};
			manifest.Normalize();

			string directory = GetUniqueDirectory(Path.Combine(CustomMusicScoreStorage.RootDirectory, CustomMusicScoreStorage.CreateFolderName(manifest)));
			Directory.CreateDirectory(directory);
			WriteManifest(directory, manifest);
			WriteDefaultScore(directory, manifest);
			return CustomMusicScoreStorage.LoadEntry(directory);
		}

		public static CustomMusicScoreEntry DuplicateEntry(CustomMusicScoreEntry source)
		{
			if (source == null || !Directory.Exists(source.RootDirectory))
			{
				return null;
			}

			CustomMusicScoreManifest manifest = CloneManifest(source.Manifest);
			manifest.id = CustomMusicScoreStorage.GenerateShortId();
			manifest.scoreTitle = string.IsNullOrWhiteSpace(manifest.scoreTitle) ? "副本" : manifest.scoreTitle + " 副本";
			manifest.Normalize();

			string destination = GetUniqueDirectory(Path.Combine(CustomMusicScoreStorage.RootDirectory, CustomMusicScoreStorage.CreateFolderName(manifest)));
			CopyDirectory(source.RootDirectory, destination);
			WriteManifest(destination, manifest);
			return CustomMusicScoreStorage.LoadEntry(destination);
		}

		public static void DeleteEntry(CustomMusicScoreEntry entry)
		{
			if (entry == null || string.IsNullOrEmpty(entry.RootDirectory) || !Directory.Exists(entry.RootDirectory))
			{
				return;
			}

			string root = Path.GetFullPath(CustomMusicScoreStorage.RootDirectory);
			string target = Path.GetFullPath(entry.RootDirectory);
			if (!target.StartsWith(root, StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("拒绝删除 CustomMusicScores 外部的文件夹。");
			}

			Directory.Delete(target, true);
		}

		public static CustomMusicScoreEntry ImportFolder(string sourceDirectory)
		{
			if (string.IsNullOrEmpty(sourceDirectory) || !Directory.Exists(sourceDirectory))
			{
				return null;
			}

			CustomMusicScoreEntry source = CustomMusicScoreStorage.LoadEntry(sourceDirectory);
			if (source == null)
			{
				return null;
			}

			CustomMusicScoreManifest manifest = CloneManifest(source.Manifest);
			manifest.Normalize();
			string destination = GetUniqueDirectory(Path.Combine(CustomMusicScoreStorage.RootDirectory, CustomMusicScoreStorage.CreateFolderName(manifest)));
			CopyDirectory(source.RootDirectory, destination);
			WriteManifest(destination, manifest);
			return CustomMusicScoreStorage.LoadEntry(destination);
		}

		public static CustomMusicScoreEntry ImportZip(string zipPath)
		{
			if (string.IsNullOrEmpty(zipPath) || !File.Exists(zipPath))
			{
				return null;
			}

			string tempRoot = CreateTemporaryPath("OpenSekaiCustomScore_");
			Directory.CreateDirectory(tempRoot);
			try
			{
				ZipFile.ExtractToDirectory(zipPath, tempRoot);
				string entryRoot = FindEntryRoot(tempRoot);
				return ImportFolder(entryRoot);
			}
			finally
			{
				if (Directory.Exists(tempRoot))
				{
					Directory.Delete(tempRoot, true);
				}
			}
		}

		public static string ExportZip(CustomMusicScoreEntry entry, string destinationPath = null)
		{
			if (entry == null || !Directory.Exists(entry.RootDirectory))
			{
				return null;
			}

			if (string.IsNullOrEmpty(destinationPath))
			{
				string exportDirectory = Path.Combine(CustomMusicScoreStorage.RootDirectory, ExportDirectoryName);
				Directory.CreateDirectory(exportDirectory);
				string safeTitle = SanitizeFileName(entry.Manifest.scoreTitle);
				if (string.IsNullOrEmpty(safeTitle))
				{
					safeTitle = "未命名";
				}
				destinationPath = Path.Combine(exportDirectory, safeTitle + "_" + entry.Manifest.id + ".zip");
			}

			string directory = Path.GetDirectoryName(destinationPath);
			if (!string.IsNullOrEmpty(directory))
			{
				Directory.CreateDirectory(directory);
			}
			if (File.Exists(destinationPath))
			{
				File.Delete(destinationPath);
			}

			ZipFile.CreateFromDirectory(entry.RootDirectory, destinationPath, System.IO.Compression.CompressionLevel.Optimal, false);
			return destinationPath;
		}

		public static CustomMusicScoreEntry SaveManifest(CustomMusicScoreEntry entry, CustomMusicScoreManifest manifest)
		{
			if (entry == null || manifest == null)
			{
				return entry;
			}

			manifest.Normalize();
			string directory = ResolveManifestSaveDirectory(entry, manifest);
			WriteManifest(directory, manifest);
			return CustomMusicScoreStorage.LoadEntry(directory);
		}

		public static CustomMusicScoreEntry ReplaceAudioFile(CustomMusicScoreEntry entry, string sourcePath)
		{
			return ReplaceEntryFile(
				entry,
				sourcePath,
				"audio",
				AudioExtensions,
				manifest => manifest.audioFileName,
				(manifest, fileName) => manifest.audioFileName = fileName);
		}

		public static CustomMusicScoreEntry ReplaceJacketFile(CustomMusicScoreEntry entry, string sourcePath)
		{
			return ReplaceEntryFile(
				entry,
				sourcePath,
				"jacket",
				JacketExtensions,
				manifest => manifest.jacketFileName,
				(manifest, fileName) => manifest.jacketFileName = fileName,
				ResizeAndWriteJacketFile);
		}

		public static CustomMusicScoreEntry ReplaceScoreFile(CustomMusicScoreEntry entry, string sourcePath)
		{
			if (entry == null || string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
			{
				return entry;
			}

			string extension = Path.GetExtension(sourcePath);
			if (string.IsNullOrEmpty(extension) || !ScoreExtensions.Contains(extension))
			{
				throw new InvalidOperationException("不支持的谱面文件扩展名：" + extension);
			}

			Directory.CreateDirectory(entry.RootDirectory);
			CustomMusicScoreManifest manifest = CloneManifest(entry.Manifest);
			string destinationPath = Path.Combine(entry.RootDirectory, "score.json");
			string tempCopyPath = null;
			string readSourcePath = sourcePath;
			if (IsPathInsideDirectory(entry.RootDirectory, sourcePath))
			{
				tempCopyPath = CreateTemporaryPath("OpenSekaiCustomScoreFile_", extension);
				File.Copy(sourcePath, tempCopyPath, true);
				readSourcePath = tempCopyPath;
			}

			try
			{
				DeleteExistingScoreFiles(entry.RootDirectory, manifest.scoreFileName);
				if (extension.Equals(".json", StringComparison.OrdinalIgnoreCase))
				{
					File.Copy(readSourcePath, destinationPath, true);
				}
				else
				{
					string susText = File.ReadAllText(readSourcePath);
					Converter converter = new Converter();
					// Original editor import calls SUS.Converter.Convert with isNeedCombo=false.
					// LongHoldCombo notes are generated later when MusicScoreMakerData is converted for live play.
					MusicScoreMakerData data = new MusicScoreMakerData(converter.Convert(susText, false, false));
					data.MusicId = entry.MusicId;
					data.InitializeIdCount();
					File.WriteAllText(destinationPath, DeepCopyHelper.ToJsonString(data));
				}

				manifest.scoreFileName = "score.json";
				manifest.Normalize();
				WriteManifest(entry.RootDirectory, manifest);
				return CustomMusicScoreStorage.LoadEntry(entry.RootDirectory);
			}
			finally
			{
				if (!string.IsNullOrEmpty(tempCopyPath) && File.Exists(tempCopyPath))
				{
					File.Delete(tempCopyPath);
				}
			}
		}

		public static CustomMusicScoreEntry ReplaceVideoFile(CustomMusicScoreEntry entry, string sourcePath)
		{
			return ReplaceEntryFile(
				entry,
				sourcePath,
				"video",
				VideoExtensions,
				manifest => manifest.videoFileName,
				(manifest, fileName) => manifest.videoFileName = fileName);
		}

		private static CustomMusicScoreManagerItem CreateItem(CustomMusicScoreEntry entry)
		{
			string manifestPath = entry.ManifestPath;
			string scorePath = entry.ScorePath;
			string audioPath = entry.AudioPath;
			string jacketPath = entry.JacketPath;
			DateTime lastWriteTime = Directory.GetLastWriteTime(entry.RootDirectory);
			if (File.Exists(manifestPath))
			{
				lastWriteTime = Max(lastWriteTime, File.GetLastWriteTime(manifestPath));
			}
			if (File.Exists(scorePath))
			{
				lastWriteTime = Max(lastWriteTime, File.GetLastWriteTime(scorePath));
			}
			if (File.Exists(entry.VideoPath))
			{
				lastWriteTime = Max(lastWriteTime, File.GetLastWriteTime(entry.VideoPath));
			}

			return new CustomMusicScoreManagerItem(
				entry,
				lastWriteTime,
				File.Exists(manifestPath),
				File.Exists(scorePath),
				File.Exists(audioPath),
				File.Exists(jacketPath));
		}

		private static void WriteDefaultScore(string directory, CustomMusicScoreManifest manifest)
		{
			MusicScoreMakerData data = new MusicScoreMakerData();
			data.MusicScoreEventDataList.Add(new MusicScoreEventData { id = 1, eventType = MusicScoreEventType.BPM, ticks = 0L, changeValue = 120f });
			data.MusicScoreEventDataList.Add(new MusicScoreEventData { id = 2, eventType = MusicScoreEventType.TimeSignature, ticks = 0L, changeValue = "4/4" });
			data.MusicScoreEventDataList.Add(new MusicScoreEventData { id = 3, eventType = MusicScoreEventType.HighSpeed, ticks = 0L, changeValue = 1f });
			data.MusicScoreEventDataList.Add(new MusicScoreEventData { id = 4, eventType = MusicScoreEventType.SeVolume, ticks = 0L, changeValue = 1f });
			data.InitializeIdCount();
			string path = Path.Combine(directory, manifest.scoreFileName);
			File.WriteAllText(path, Sekai.MusicScoreMaker.Ingame.Utilities.DeepCopyHelper.ToJsonString(data));
		}

		private static void WriteManifest(string directory, CustomMusicScoreManifest manifest)
		{
			Directory.CreateDirectory(directory);
			string json = JsonConvert.SerializeObject(manifest, Formatting.Indented);
			File.WriteAllText(Path.Combine(directory, CustomMusicScoreStorage.ManifestFileName), json);
		}

		private static string ResolveManifestSaveDirectory(CustomMusicScoreEntry entry, CustomMusicScoreManifest manifest)
		{
			string currentDirectory = Path.GetFullPath(entry.RootDirectory);
			if (!Directory.Exists(currentDirectory))
			{
				return currentDirectory;
			}

			string rootDirectory = Path.GetFullPath(CustomMusicScoreStorage.RootDirectory);
			string normalizedRoot = rootDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
			if (!currentDirectory.StartsWith(normalizedRoot, StringComparison.OrdinalIgnoreCase))
			{
				return currentDirectory;
			}

			string desiredDirectory = Path.GetFullPath(Path.Combine(rootDirectory, CustomMusicScoreStorage.CreateFolderName(manifest)));
			if (PathsEqual(currentDirectory, desiredDirectory))
			{
				return currentDirectory;
			}

			string destination = Directory.Exists(desiredDirectory) ? GetUniqueDirectory(desiredDirectory) : desiredDirectory;
			Directory.Move(currentDirectory, destination);
			return destination;
		}

		private static CustomMusicScoreEntry ReplaceEntryFile(
			CustomMusicScoreEntry entry,
			string sourcePath,
			string fixedBaseName,
			HashSet<string> allowedExtensions,
			Func<CustomMusicScoreManifest, string> getCurrentFileName,
			Action<CustomMusicScoreManifest, string> setFileName,
			Action<string, string, string> writeFile = null)
		{
			if (entry == null || string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
			{
				return entry;
			}

			string extension = ResolveSupportedExtension(sourcePath, allowedExtensions);
			if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
			{
				throw new InvalidOperationException("不支持的文件扩展名：" + extension);
			}

			Directory.CreateDirectory(entry.RootDirectory);
			CustomMusicScoreManifest manifest = CloneManifest(entry.Manifest);
			string fileName = fixedBaseName + extension.ToLowerInvariant();
			string destinationPath = Path.Combine(entry.RootDirectory, fileName);
			string copySourcePath = sourcePath;
			string tempCopyPath = null;
			if (IsPathInsideDirectory(entry.RootDirectory, sourcePath))
			{
				tempCopyPath = CreateTemporaryPath("OpenSekaiCustomScoreFile_", extension);
				File.Copy(sourcePath, tempCopyPath, true);
				copySourcePath = tempCopyPath;
			}

			try
			{
				DeleteExistingEntrySlotFiles(entry.RootDirectory, fixedBaseName, allowedExtensions, getCurrentFileName?.Invoke(manifest));
				if (writeFile != null)
				{
					writeFile(copySourcePath, destinationPath, extension);
				}
				else
				{
					File.Copy(copySourcePath, destinationPath, true);
				}
				setFileName(manifest, Path.GetFileName(destinationPath));
				manifest.Normalize();
				WriteManifest(entry.RootDirectory, manifest);
				return CustomMusicScoreStorage.LoadEntry(entry.RootDirectory);
			}
			finally
			{
				if (!string.IsNullOrEmpty(tempCopyPath) && File.Exists(tempCopyPath))
				{
					File.Delete(tempCopyPath);
				}
			}
		}

		private static void ResizeAndWriteJacketFile(string sourcePath, string destinationPath, string extension)
		{
			byte[] sourceBytes = File.ReadAllBytes(sourcePath);
			Texture2D sourceTexture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			if (!sourceTexture.LoadImage(sourceBytes))
			{
				UnityEngine.Object.Destroy(sourceTexture);
				throw new InvalidOperationException("不支持的封面图片数据。");
			}

			RenderTexture previous = RenderTexture.active;
			RenderTexture renderTexture = RenderTexture.GetTemporary(JacketTextureSize, JacketTextureSize, 0, RenderTextureFormat.ARGB32);
			Texture2D resizedTexture = new Texture2D(JacketTextureSize, JacketTextureSize, TextureFormat.RGBA32, false);
			try
			{
				Graphics.Blit(sourceTexture, renderTexture);
				RenderTexture.active = renderTexture;
				resizedTexture.ReadPixels(new Rect(0f, 0f, JacketTextureSize, JacketTextureSize), 0, 0);
				resizedTexture.Apply(false, false);

				byte[] resizedBytes = extension.Equals(".png", StringComparison.OrdinalIgnoreCase)
					? resizedTexture.EncodeToPNG()
					: resizedTexture.EncodeToJPG(95);
				File.WriteAllBytes(destinationPath, resizedBytes);
			}
			finally
			{
				RenderTexture.active = previous;
				RenderTexture.ReleaseTemporary(renderTexture);
				UnityEngine.Object.Destroy(sourceTexture);
				UnityEngine.Object.Destroy(resizedTexture);
			}
		}

		private static string ResolveSupportedExtension(string path, HashSet<string> allowedExtensions)
		{
			string extension = Path.GetExtension(path);
			if (!string.IsNullOrEmpty(extension) && allowedExtensions.Contains(extension))
			{
				return extension;
			}

			string detectedExtension = DetectExtensionFromHeader(path);
			return !string.IsNullOrEmpty(detectedExtension) ? detectedExtension : extension;
		}

		private static string DetectExtensionFromHeader(string path)
		{
			if (string.IsNullOrEmpty(path) || !File.Exists(path))
			{
				return string.Empty;
			}

			byte[] header = new byte[12];
			int readCount;
			using (FileStream stream = File.OpenRead(path))
			{
				readCount = stream.Read(header, 0, header.Length);
			}

			if (readCount >= 8
				&& header[0] == 0x89
				&& header[1] == 0x50
				&& header[2] == 0x4E
				&& header[3] == 0x47
				&& header[4] == 0x0D
				&& header[5] == 0x0A
				&& header[6] == 0x1A
				&& header[7] == 0x0A)
			{
				return ".png";
			}

			if (readCount >= 2 && header[0] == 0xFF && header[1] == 0xD8)
			{
				return ".jpg";
			}

			if (readCount >= 4 && header[0] == 0x4F && header[1] == 0x67 && header[2] == 0x67 && header[3] == 0x53)
			{
				return ".ogg";
			}

			if (readCount >= 12
				&& header[0] == 0x52
				&& header[1] == 0x49
				&& header[2] == 0x46
				&& header[3] == 0x46
				&& header[8] == 0x57
				&& header[9] == 0x41
				&& header[10] == 0x56
				&& header[11] == 0x45)
			{
				return ".wav";
			}

			if (readCount >= 3 && header[0] == 0x49 && header[1] == 0x44 && header[2] == 0x33)
			{
				return ".mp3";
			}

			if (readCount >= 2 && header[0] == 0xFF && (header[1] & 0xE0) == 0xE0)
			{
				return ".mp3";
			}

			return string.Empty;
		}

		private static void DeleteExistingEntrySlotFiles(
			string rootDirectory,
			string fixedBaseName,
			HashSet<string> allowedExtensions,
			string currentFileName)
		{
			HashSet<string> deletePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (!string.IsNullOrWhiteSpace(currentFileName))
			{
				string fileName = Path.GetFileName(currentFileName);
				if (!string.IsNullOrEmpty(fileName))
				{
					string extension = Path.GetExtension(fileName);
					if (allowedExtensions.Contains(extension))
					{
						deletePaths.Add(Path.Combine(rootDirectory, fileName));
					}
				}
			}

			foreach (string extension in allowedExtensions)
			{
				deletePaths.Add(Path.Combine(rootDirectory, fixedBaseName + extension.ToLowerInvariant()));
			}

			foreach (string path in Directory.GetFiles(rootDirectory))
			{
				if (allowedExtensions.Contains(Path.GetExtension(path)))
				{
					deletePaths.Add(path);
				}
			}

			foreach (string path in deletePaths)
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
		}

		private static void DeleteExistingScoreFiles(string rootDirectory, string currentFileName)
		{
			HashSet<string> deletePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
			{
				Path.Combine(rootDirectory, "score.json"),
				Path.Combine(rootDirectory, "score.txt"),
				Path.Combine(rootDirectory, "score.sus")
			};

			if (!string.IsNullOrWhiteSpace(currentFileName))
			{
				string fileName = Path.GetFileName(currentFileName);
				if (!string.IsNullOrEmpty(fileName) && !string.Equals(fileName, CustomMusicScoreStorage.ManifestFileName, StringComparison.OrdinalIgnoreCase))
				{
					string extension = Path.GetExtension(fileName);
					if (ScoreExtensions.Contains(extension))
					{
						deletePaths.Add(Path.Combine(rootDirectory, fileName));
					}
				}
			}

			foreach (string path in Directory.GetFiles(rootDirectory, "score.*"))
			{
				if (ScoreExtensions.Contains(Path.GetExtension(path)))
				{
					deletePaths.Add(path);
				}
			}

			foreach (string path in deletePaths)
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
		}

		private static CustomMusicScoreManifest CloneManifest(CustomMusicScoreManifest source)
		{
			if (source == null)
			{
				return new CustomMusicScoreManifest();
			}

			return JsonConvert.DeserializeObject<CustomMusicScoreManifest>(
				JsonConvert.SerializeObject(source)) ?? new CustomMusicScoreManifest();
		}

		private static string FindEntryRoot(string root)
		{
			if (File.Exists(Path.Combine(root, CustomMusicScoreStorage.ManifestFileName)))
			{
				return root;
			}

			foreach (string directory in Directory.GetDirectories(root))
			{
				string result = FindEntryRoot(directory);
				if (!string.IsNullOrEmpty(result))
				{
					return result;
				}
			}
			return null;
		}

		private static void CopyDirectory(string source, string destination)
		{
			Directory.CreateDirectory(destination);
			foreach (string directory in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
			{
				Directory.CreateDirectory(Path.Combine(destination, GetRelativePath(source, directory)));
			}
			foreach (string file in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
			{
				File.Copy(file, Path.Combine(destination, GetRelativePath(source, file)), true);
			}
		}

		private static string GetRelativePath(string root, string path)
		{
			string normalizedRoot = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
			string normalizedPath = Path.GetFullPath(path);
			if (normalizedPath.Length <= normalizedRoot.Length)
			{
				return string.Empty;
			}

			return normalizedPath.Substring(normalizedRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
		}

		private static string GetUniqueDirectory(string directory)
		{
			if (!Directory.Exists(directory))
			{
				return directory;
			}

			for (int i = 2; i < 1000; i++)
			{
				string candidate = directory + "_" + i;
				if (!Directory.Exists(candidate))
				{
					return candidate;
				}
			}

			return directory + "_" + Guid.NewGuid().ToString("N");
		}

		private static bool PathsEqual(string left, string right)
		{
			string normalizedLeft = Path.GetFullPath(left).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
			string normalizedRight = Path.GetFullPath(right).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
			return string.Equals(normalizedLeft, normalizedRight, StringComparison.OrdinalIgnoreCase);
		}

		private static bool IsPathInsideDirectory(string directory, string path)
		{
			string normalizedDirectory = Path.GetFullPath(directory).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
			string normalizedPath = Path.GetFullPath(path);
			return normalizedPath.StartsWith(normalizedDirectory, StringComparison.OrdinalIgnoreCase);
		}

		private static string CreateTemporaryPath(string prefix, string extension = null)
		{
			string tempDirectory = Path.Combine(Application.temporaryCachePath, "CustomMusicScoreManager");
			Directory.CreateDirectory(tempDirectory);
			return Path.Combine(tempDirectory, prefix + Guid.NewGuid().ToString("N") + (extension ?? string.Empty));
		}

		private static string SanitizeFileName(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}

			HashSet<char> invalidChars = new HashSet<char>(Path.GetInvalidFileNameChars());
			char[] chars = value.Trim().Select(c => invalidChars.Contains(c) || char.IsWhiteSpace(c) ? '_' : c).ToArray();
			return new string(chars).Trim('_');
		}

		private static DateTime Max(DateTime left, DateTime right)
		{
			return left >= right ? left : right;
		}
	}
}
