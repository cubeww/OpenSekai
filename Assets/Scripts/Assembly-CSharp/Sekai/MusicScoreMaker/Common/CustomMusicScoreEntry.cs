using System;
using System.IO;
using System.Threading;
using CP;
using Cysharp.Threading.Tasks;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.Networking;

namespace Sekai.MusicScoreMaker.Common
{
	public sealed class CustomMusicScoreEntry
	{
		public CustomMusicScoreEntry(string rootDirectory, CustomMusicScoreManifest manifest)
		{
			RootDirectory = rootDirectory;
			Manifest = manifest ?? new CustomMusicScoreManifest();
			Manifest.Normalize();
			MusicId = CreateStableMusicId(Manifest.id);
			AudioCueName = "custom_" + Manifest.id;
		}

		public string RootDirectory { get; }

		public CustomMusicScoreManifest Manifest { get; }

		public int MusicId { get; }

		public string AudioCueName { get; }

		public long AudioLengthMs { get; private set; }

		public string ManifestPath => Path.Combine(RootDirectory, CustomMusicScoreStorage.ManifestFileName);

		public string ScorePath => ResolveEntryPath(Manifest.scoreFileName);

		public string AudioPath => ResolveEntryPath(Manifest.audioFileName);

		public string JacketPath => ResolveEntryPath(Manifest.jacketFileName);

		public string VideoPath => ResolveEntryPath(Manifest.videoFileName);

		public int MusicDurationSec
		{
			get
			{
				if (Manifest.secForMusicScoreMaker > 0)
				{
					return Manifest.secForMusicScoreMaker;
				}
				if (AudioLengthMs > 0L)
				{
					float seconds = AudioLengthMs / 1000f - Manifest.fillerSec;
					return Mathf.Max(1, Mathf.CeilToInt(seconds));
				}
				return 120;
			}
		}

		public MusicScoreMakerData LoadScore()
		{
			if (!File.Exists(ScorePath))
			{
				return null;
			}

			MusicScoreMakerData data = DeepCopyHelper.FromJson<MusicScoreMakerData>(File.ReadAllText(ScorePath));
			if (data == null)
			{
				return null;
			}
			data.MigrateToCurrentVersion();
			data.InitializeIdCount();
			data.MusicId = MusicId;
			return data;
		}

		public void SaveScore(MusicScoreMakerData data)
		{
			if (data == null)
			{
				return;
			}

			Directory.CreateDirectory(RootDirectory);
			data.MusicId = MusicId;
			File.WriteAllText(ScorePath, DeepCopyHelper.ToJsonString(data));
		}

		public async UniTask<bool> RegisterAudioAsync(CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			AudioClip audioClip = await LoadAudioClipAsync(token);
			if (audioClip == null)
			{
				return false;
			}

			try
			{
				AudioLengthMs = (long)(audioClip.length * 1000f);
				byte[] wavData = CreatePcm16WavData(audioClip);
				long sourceTicks = File.Exists(AudioPath) ? File.GetLastWriteTimeUtc(AudioPath).Ticks : DateTime.UtcNow.Ticks;
				return SoundManager.Instance.RegisterExternalAudioData(AudioCueName, wavData, audioClip.channels, audioClip.frequency, AudioLengthMs, sourceTicks);
			}
			catch (Exception exception)
			{
				LogUtility.LogWarning("Failed to register custom music audio. path:{0} error:{1}", AudioPath, exception.Message);
				return false;
			}
			finally
			{
				UnityEngine.Object.Destroy(audioClip);
			}
		}

		private async UniTask<AudioClip> LoadAudioClipAsync(CancellationToken token)
		{
			if (!File.Exists(AudioPath))
			{
				LogUtility.LogWarning("Custom music audio not found. path:{0}", AudioPath);
				return null;
			}

			AudioType audioType = ResolveAudioType(AudioPath);
			if (audioType == AudioType.UNKNOWN)
			{
				LogUtility.LogWarning("Unsupported custom music audio format. path:{0}", AudioPath);
				return null;
			}

			using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(ToFileUri(AudioPath), audioType))
			{
				if (request.downloadHandler is DownloadHandlerAudioClip downloadHandler)
				{
					downloadHandler.streamAudio = false;
					downloadHandler.compressed = false;
				}
				UnityWebRequestAsyncOperation operation = request.SendWebRequest();
				await UniTask.WaitUntil(() => operation.isDone, cancellationToken: token);
				token.ThrowIfCancellationRequested();

				if (request.result != UnityWebRequest.Result.Success)
				{
					LogUtility.LogWarning("Failed to load custom music audio. path:{0} error:{1}", AudioPath, request.error);
					return null;
				}

				AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
				if (clip != null)
				{
					clip.name = AudioCueName;
					if (!await LoadAudioClipPcmAsync(clip, token))
					{
						LogUtility.LogWarning("Failed to decode custom music audio PCM. path:{0} state:{1}", AudioPath, clip.loadState);
						return null;
					}
				}
				return clip;
			}
		}

		private static async UniTask<bool> LoadAudioClipPcmAsync(AudioClip clip, CancellationToken token)
		{
			if (clip == null)
			{
				return false;
			}

			if (clip.loadState == AudioDataLoadState.Unloaded && !clip.LoadAudioData())
			{
				return false;
			}

			while (clip.loadState == AudioDataLoadState.Loading)
			{
				await UniTask.Yield(cancellationToken: token);
			}
			return clip.loadState == AudioDataLoadState.Loaded;
		}

		private string ResolveEntryPath(string fileName)
		{
			string safeFileName = Path.GetFileName(string.IsNullOrEmpty(fileName) ? string.Empty : fileName);
			return string.IsNullOrEmpty(safeFileName) ? RootDirectory : Path.Combine(RootDirectory, safeFileName);
		}

		private static string ToFileUri(string path)
		{
			return new Uri(Path.GetFullPath(path)).AbsoluteUri;
		}

		private static AudioType ResolveAudioType(string path)
		{
			string extension = Path.GetExtension(path)?.ToLowerInvariant();
			switch (extension)
			{
				case ".ogg":
					return AudioType.OGGVORBIS;
				case ".mp3":
					return AudioType.MPEG;
				case ".wav":
					return AudioType.WAV;
				default:
					return AudioType.UNKNOWN;
			}
		}

		private static byte[] CreatePcm16WavData(AudioClip clip)
		{
			int channels = clip.channels;
			int frequency = clip.frequency;
			int sampleFrames = clip.samples;
			if (channels <= 0 || frequency <= 0 || sampleFrames <= 0)
			{
				throw new InvalidOperationException(
					string.Format("AudioClip has invalid PCM info. channels:{0} frequency:{1} samples:{2}", channels, frequency, sampleFrames));
			}

			long sampleValueCount = (long)sampleFrames * channels;
			long dataSize = sampleValueCount * 2L;
			if (dataSize > int.MaxValue)
			{
				throw new InvalidOperationException("Custom music audio is too large to convert to WAV.");
			}

			using (MemoryStream stream = new MemoryStream((int)(44L + dataSize)))
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				WriteAscii(writer, "RIFF");
				writer.Write((int)(36L + dataSize));
				WriteAscii(writer, "WAVE");
				WriteAscii(writer, "fmt ");
				writer.Write(16);
				writer.Write((short)1);
				writer.Write((short)channels);
				writer.Write(frequency);
				writer.Write(frequency * channels * 2);
				writer.Write((short)(channels * 2));
				writer.Write((short)16);
				WriteAscii(writer, "data");
				writer.Write((int)dataSize);

				const int ChunkFrames = 65536;
				int offset = 0;
				while (offset < sampleFrames)
				{
					int frames = Math.Min(ChunkFrames, sampleFrames - offset);
					float[] samples = new float[frames * channels];
					byte[] bytes = new byte[samples.Length * 2];
					if (!clip.GetData(samples, offset))
					{
						throw new InvalidOperationException(
							string.Format("AudioClip PCM extraction failed. offset:{0} frames:{1}", offset, frames));
					}
					for (int i = 0; i < samples.Length; i++)
					{
						float value = Mathf.Clamp(samples[i], -1f, 1f);
						short pcm = value < 0f ? (short)(value * 32768f) : (short)(value * 32767f);
						int byteIndex = i * 2;
						bytes[byteIndex] = (byte)(pcm & 0xFF);
						bytes[byteIndex + 1] = (byte)((pcm >> 8) & 0xFF);
					}
					writer.Write(bytes);
					offset += frames;
				}
				return stream.ToArray();
			}
		}

		private static void WriteAscii(BinaryWriter writer, string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				writer.Write((byte)value[i]);
			}
		}

		private static int CreateStableMusicId(string id)
		{
			unchecked
			{
				uint hash = 2166136261U;
				string source = string.IsNullOrEmpty(id) ? "custom" : id.ToLowerInvariant();
				for (int i = 0; i < source.Length; i++)
				{
					hash ^= source[i];
					hash *= 16777619U;
				}

				int value = (int)(hash & 0x7FFFFFFF);
				return value == 0 ? -1 : -value;
			}
		}
	}
}
