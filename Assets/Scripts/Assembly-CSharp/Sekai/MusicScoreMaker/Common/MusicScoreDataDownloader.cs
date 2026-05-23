using System;
using System.IO;
using System.Threading;
using CP;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.Networking;

namespace Sekai.MusicScoreMaker.Common
{
	public sealed class MusicScoreDataDownloader
	{
		public enum DownloadType
		{
			Default = 0,
			Preview = 1
		}

		private readonly DownloadType _downloadType;

		private readonly string _path;

		private readonly bool _isOfficial;

		public MusicScoreDataDownloader(DownloadType downloadType, bool isOfficial, string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException("path is null or empty.", nameof(path));
			}

			_downloadType = downloadType;
			_isOfficial = isOfficial;
			_path = path;
		}

		[ItemCanBeNull]
		public async UniTask<MusicScoreMakerData> ExecuteAsync(CancellationToken token)
		{
			string compressedBase64 = await DownloadGZipCompressedBase64Async(token);
			if (string.IsNullOrEmpty(compressedBase64))
			{
				return null;
			}

			string json = LooksLikeJson(compressedBase64) ? compressedBase64 : GZipJsonHelper.DecompressStringToJson(compressedBase64);
			MusicScoreMakerData data = DeepCopyHelper.FromJson<MusicScoreMakerData>(json);
			if (data != null)
			{
				data.MigrateToCurrentVersion();
				data.InitializeIdCount();
			}
			return data;
		}

		private async UniTask<string> DownloadGZipCompressedBase64Async(CancellationToken token)
		{
			token.ThrowIfCancellationRequested();

			if (_isOfficial)
			{
				// TODO(original): restore AssetManager.DownloadAssetBundleAsync +
				// AssetBundleLoader.LoadResource<TextAsset> using the official score bundle.
				TextAsset resource = Resources.Load<TextAsset>(_path);
				return resource != null ? resource.text : string.Empty;
			}

			if (File.Exists(_path))
			{
				return File.ReadAllText(_path);
			}

			string url = BuildAccessUrl();
			if (string.IsNullOrEmpty(url) || !Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
			{
				return string.Empty;
			}

			using UnityWebRequest request = UnityWebRequest.Get(uri);
			UnityWebRequestAsyncOperation operation = request.SendWebRequest();
			await UniTask.WaitUntil(() => operation.isDone, cancellationToken: token);

			if (request.result != UnityWebRequest.Result.Success)
			{
				LogUtility.LogError("MusicScoreDataDownloader failed: {0} {1}", request.responseCode, request.error);
				return string.Empty;
			}
			return request.downloadHandler?.text ?? string.Empty;
		}

		private string BuildAccessUrl()
		{
			switch (_downloadType)
			{
			case DownloadType.Default:
			case DownloadType.Preview:
				if (Uri.TryCreate(_path, UriKind.Absolute, out _))
				{
					return _path;
				}
				// TODO(original): use MasterDataManager config key
				// `custom_music_score_data_base_url` / preview equivalent and
				// EnvironmentConfig.SekaiGameAPIDomain, then combine with _path.
				return _path;
			default:
				throw new ArgumentOutOfRangeException(nameof(_downloadType), _downloadType, null);
			}
		}

		private static bool LooksLikeJson(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}
			char first = value.TrimStart()[0];
			return first == '{' || first == '[';
		}
	}
}
