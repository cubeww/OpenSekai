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

namespace Sekai.CustomMusicScoreManager
{
	public static class CustomMusicScoreManagerService
	{
		private const string ExportDirectoryName = "Exports";

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
			".txt"
		};

		public static IReadOnlyList<CustomMusicScoreManagerItem> LoadItems()
		{
			Directory.CreateDirectory(CustomMusicScoreStorage.RootDirectory);
			List<CustomMusicScoreManagerItem> items = new List<CustomMusicScoreManagerItem>();
			foreach (string directory in Directory.GetDirectories(CustomMusicScoreStorage.RootDirectory))
			{
				CustomMusicScorePackage package = CustomMusicScoreStorage.LoadPackage(directory);
				if (package == null)
				{
					continue;
				}

				items.Add(CreateItem(package));
			}

			return items
				.OrderByDescending(x => x.LastWriteTime)
				.ThenBy(x => x.Package.Manifest.scoreTitle, StringComparer.OrdinalIgnoreCase)
				.ToArray();
		}

		public static CustomMusicScorePackage CreateNewPackage()
		{
			Directory.CreateDirectory(CustomMusicScoreStorage.RootDirectory);
			CustomMusicScoreManifest manifest = new CustomMusicScoreManifest
			{
				id = CustomMusicScoreStorage.GenerateShortId(),
				title = "Untitled",
				scoreTitle = "Untitled Score",
				userName = Environment.UserName,
				composer = string.Empty,
				lyricist = string.Empty,
				arranger = string.Empty,
				singer = string.Empty,
				audioFileName = "audio.ogg",
				jacketFileName = "jacket.png",
				scoreFileName = "score.json",
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
			return CustomMusicScoreStorage.LoadPackage(directory);
		}

		public static CustomMusicScorePackage DuplicatePackage(CustomMusicScorePackage source)
		{
			if (source == null || !Directory.Exists(source.RootDirectory))
			{
				return null;
			}

			CustomMusicScoreManifest manifest = CloneManifest(source.Manifest);
			manifest.id = CustomMusicScoreStorage.GenerateShortId();
			manifest.scoreTitle = string.IsNullOrWhiteSpace(manifest.scoreTitle) ? "Copy" : manifest.scoreTitle + " Copy";
			manifest.Normalize();

			string destination = GetUniqueDirectory(Path.Combine(CustomMusicScoreStorage.RootDirectory, CustomMusicScoreStorage.CreateFolderName(manifest)));
			CopyDirectory(source.RootDirectory, destination);
			WriteManifest(destination, manifest);
			return CustomMusicScoreStorage.LoadPackage(destination);
		}

		public static void DeletePackage(CustomMusicScorePackage package)
		{
			if (package == null || string.IsNullOrEmpty(package.RootDirectory) || !Directory.Exists(package.RootDirectory))
			{
				return;
			}

			string root = Path.GetFullPath(CustomMusicScoreStorage.RootDirectory);
			string target = Path.GetFullPath(package.RootDirectory);
			if (!target.StartsWith(root, StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("Refusing to delete a folder outside CustomMusicScores.");
			}

			Directory.Delete(target, true);
		}

		public static CustomMusicScorePackage ImportFolder(string sourceDirectory)
		{
			if (string.IsNullOrEmpty(sourceDirectory) || !Directory.Exists(sourceDirectory))
			{
				return null;
			}

			CustomMusicScorePackage source = CustomMusicScoreStorage.LoadPackage(sourceDirectory);
			if (source == null)
			{
				return null;
			}

			CustomMusicScoreManifest manifest = CloneManifest(source.Manifest);
			manifest.Normalize();
			string destination = GetUniqueDirectory(Path.Combine(CustomMusicScoreStorage.RootDirectory, CustomMusicScoreStorage.CreateFolderName(manifest)));
			CopyDirectory(source.RootDirectory, destination);
			WriteManifest(destination, manifest);
			return CustomMusicScoreStorage.LoadPackage(destination);
		}

		public static CustomMusicScorePackage ImportZip(string zipPath)
		{
			if (string.IsNullOrEmpty(zipPath) || !File.Exists(zipPath))
			{
				return null;
			}

			string tempRoot = Path.Combine(Path.GetTempPath(), "OpenSekaiCustomScore_" + Guid.NewGuid().ToString("N"));
			Directory.CreateDirectory(tempRoot);
			try
			{
				ZipFile.ExtractToDirectory(zipPath, tempRoot);
				string packageRoot = FindPackageRoot(tempRoot);
				return ImportFolder(packageRoot);
			}
			finally
			{
				if (Directory.Exists(tempRoot))
				{
					Directory.Delete(tempRoot, true);
				}
			}
		}

		public static string ExportZip(CustomMusicScorePackage package, string destinationPath = null)
		{
			if (package == null || !Directory.Exists(package.RootDirectory))
			{
				return null;
			}

			if (string.IsNullOrEmpty(destinationPath))
			{
				string exportDirectory = Path.Combine(CustomMusicScoreStorage.RootDirectory, ExportDirectoryName);
				Directory.CreateDirectory(exportDirectory);
				string safeTitle = SanitizeFileName(package.Manifest.scoreTitle);
				if (string.IsNullOrEmpty(safeTitle))
				{
					safeTitle = "Untitled";
				}
				destinationPath = Path.Combine(exportDirectory, safeTitle + "_" + package.Manifest.id + ".zip");
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

			ZipFile.CreateFromDirectory(package.RootDirectory, destinationPath, CompressionLevel.Optimal, false);
			return destinationPath;
		}

		public static CustomMusicScorePackage SaveManifest(CustomMusicScorePackage package, CustomMusicScoreManifest manifest)
		{
			if (package == null || manifest == null)
			{
				return package;
			}

			manifest.Normalize();
			string directory = ResolveManifestSaveDirectory(package, manifest);
			WriteManifest(directory, manifest);
			return CustomMusicScoreStorage.LoadPackage(directory);
		}

		public static CustomMusicScorePackage ReplaceAudioFile(CustomMusicScorePackage package, string sourcePath)
		{
			return ReplacePackageFile(
				package,
				sourcePath,
				"audio",
				AudioExtensions,
				manifest => manifest.audioFileName,
				(manifest, fileName) => manifest.audioFileName = fileName);
		}

		public static CustomMusicScorePackage ReplaceJacketFile(CustomMusicScorePackage package, string sourcePath)
		{
			return ReplacePackageFile(
				package,
				sourcePath,
				"jacket",
				JacketExtensions,
				manifest => manifest.jacketFileName,
				(manifest, fileName) => manifest.jacketFileName = fileName);
		}

		public static CustomMusicScorePackage ReplaceScoreFile(CustomMusicScorePackage package, string sourcePath)
		{
			if (package == null || string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
			{
				return package;
			}

			string extension = Path.GetExtension(sourcePath);
			if (string.IsNullOrEmpty(extension) || !ScoreExtensions.Contains(extension))
			{
				throw new InvalidOperationException("Unsupported score file extension: " + extension);
			}

			Directory.CreateDirectory(package.RootDirectory);
			CustomMusicScoreManifest manifest = CloneManifest(package.Manifest);
			string destinationPath = Path.Combine(package.RootDirectory, "score.json");
			string tempCopyPath = null;
			string readSourcePath = sourcePath;
			if (IsPathInsideDirectory(package.RootDirectory, sourcePath))
			{
				tempCopyPath = Path.Combine(Path.GetTempPath(), "OpenSekaiCustomScoreFile_" + Guid.NewGuid().ToString("N") + extension);
				File.Copy(sourcePath, tempCopyPath, true);
				readSourcePath = tempCopyPath;
			}

			try
			{
				DeleteExistingScoreFiles(package.RootDirectory, manifest.scoreFileName);
				if (extension.Equals(".json", StringComparison.OrdinalIgnoreCase))
				{
					File.Copy(readSourcePath, destinationPath, true);
				}
				else
				{
					string susText = File.ReadAllText(readSourcePath);
					Converter converter = new Converter();
					MusicScoreMakerData data = new MusicScoreMakerData(converter.Convert(susText, true, false));
					data.MusicId = package.MusicId;
					data.InitializeIdCount();
					File.WriteAllText(destinationPath, DeepCopyHelper.ToJsonString(data));
				}

				manifest.scoreFileName = "score.json";
				manifest.Normalize();
				WriteManifest(package.RootDirectory, manifest);
				return CustomMusicScoreStorage.LoadPackage(package.RootDirectory);
			}
			finally
			{
				if (!string.IsNullOrEmpty(tempCopyPath) && File.Exists(tempCopyPath))
				{
					File.Delete(tempCopyPath);
				}
			}
		}

		private static CustomMusicScoreManagerItem CreateItem(CustomMusicScorePackage package)
		{
			string manifestPath = package.ManifestPath;
			string scorePath = package.ScorePath;
			string audioPath = package.AudioPath;
			string jacketPath = package.JacketPath;
			DateTime lastWriteTime = Directory.GetLastWriteTime(package.RootDirectory);
			if (File.Exists(manifestPath))
			{
				lastWriteTime = Max(lastWriteTime, File.GetLastWriteTime(manifestPath));
			}
			if (File.Exists(scorePath))
			{
				lastWriteTime = Max(lastWriteTime, File.GetLastWriteTime(scorePath));
			}

			return new CustomMusicScoreManagerItem(
				package,
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

		private static string ResolveManifestSaveDirectory(CustomMusicScorePackage package, CustomMusicScoreManifest manifest)
		{
			string currentDirectory = Path.GetFullPath(package.RootDirectory);
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

		private static CustomMusicScorePackage ReplacePackageFile(
			CustomMusicScorePackage package,
			string sourcePath,
			string fixedBaseName,
			HashSet<string> allowedExtensions,
			Func<CustomMusicScoreManifest, string> getCurrentFileName,
			Action<CustomMusicScoreManifest, string> setFileName)
		{
			if (package == null || string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
			{
				return package;
			}

			string extension = ResolveSupportedExtension(sourcePath, allowedExtensions);
			if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
			{
				throw new InvalidOperationException("Unsupported file extension: " + extension);
			}

			Directory.CreateDirectory(package.RootDirectory);
			CustomMusicScoreManifest manifest = CloneManifest(package.Manifest);
			string fileName = fixedBaseName + extension.ToLowerInvariant();
			string destinationPath = Path.Combine(package.RootDirectory, fileName);
			string copySourcePath = sourcePath;
			string tempCopyPath = null;
			if (IsPathInsideDirectory(package.RootDirectory, sourcePath))
			{
				tempCopyPath = Path.Combine(Path.GetTempPath(), "OpenSekaiCustomScoreFile_" + Guid.NewGuid().ToString("N") + extension);
				File.Copy(sourcePath, tempCopyPath, true);
				copySourcePath = tempCopyPath;
			}

			try
			{
				DeleteExistingPackageSlotFiles(package.RootDirectory, fixedBaseName, allowedExtensions, getCurrentFileName?.Invoke(manifest));
				File.Copy(copySourcePath, destinationPath, true);
				setFileName(manifest, Path.GetFileName(destinationPath));
				manifest.Normalize();
				WriteManifest(package.RootDirectory, manifest);
				return CustomMusicScoreStorage.LoadPackage(package.RootDirectory);
			}
			finally
			{
				if (!string.IsNullOrEmpty(tempCopyPath) && File.Exists(tempCopyPath))
				{
					File.Delete(tempCopyPath);
				}
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

		private static void DeleteExistingPackageSlotFiles(
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
				Path.Combine(rootDirectory, "score.txt")
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

		private static string FindPackageRoot(string root)
		{
			if (File.Exists(Path.Combine(root, CustomMusicScoreStorage.ManifestFileName)))
			{
				return root;
			}

			foreach (string directory in Directory.GetDirectories(root))
			{
				string result = FindPackageRoot(directory);
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
