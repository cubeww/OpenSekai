using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Utilities
{
	public class ClipboardCacheManager
	{
		private const string SaveFolderName = "inappcache";

		private const string SaveFileName = "ClipboardCacheListData.json";

		private List<ClipboardCacheData> _caches;

		private List<ClipboardCacheData> _sortedCachesCache;

		private static ClipboardCacheManager _instance;

		public static ClipboardCacheManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ClipboardCacheManager();
					_instance.Load();
				}
				return _instance;
			}
		}

		private ClipboardCacheManager()
		{
			_caches = new List<ClipboardCacheData>();
			_sortedCachesCache = new List<ClipboardCacheData>();
		}

		public void AddCache(ClipboardCacheData cache)
		{
			if (cache == null)
			{
				return;
			}
			int maxCount = MusicScoreMakerSettingsManager.MaxClipboardCacheCount;
			while (_caches.Count >= maxCount && _caches.Count >= 1)
			{
				ClipboardCacheData oldestCache = _caches[0];
				for (int i = 1; i < _caches.Count; i++)
				{
					if (string.Compare(_caches[i].CreatedAt, oldestCache.CreatedAt) < 0)
					{
						oldestCache = _caches[i];
					}
				}
				_caches.Remove(oldestCache);
			}
			_caches.Add(cache);
			Save();
		}

		public void RemoveCache(string id)
		{
			for (int i = 0; i < _caches.Count; i++)
			{
				if (_caches[i].Id == id)
				{
					_caches.RemoveAt(i);
					Save();
					return;
				}
			}
		}

		public ClipboardCacheData GetCache(string id)
		{
			for (int i = 0; i < _caches.Count; i++)
			{
				if (_caches[i].Id == id)
				{
					return _caches[i];
				}
			}
			return null;
		}

		public IReadOnlyList<ClipboardCacheData> GetAllCaches()
		{
			_sortedCachesCache.Clear();
			_sortedCachesCache.AddRange(_caches);
			_sortedCachesCache.Sort((a, b) => string.Compare(b.CreatedAt, a.CreatedAt));
			return _sortedCachesCache.AsReadOnly();
		}

		public void ClearAll()
		{
			_caches.Clear();
			Save();
		}

		private void Save()
		{
			string folderPath = Path.Combine(Application.persistentDataPath, SaveFolderName);
			Directory.CreateDirectory(folderPath);
			File.WriteAllText(Path.Combine(folderPath, SaveFileName), JsonConvert.SerializeObject(new ClipboardCacheListData
			{
				Caches = _caches
			}));
		}

		private void Load()
		{
			string filePath = Path.Combine(Application.persistentDataPath, SaveFolderName, SaveFileName);
			if (!File.Exists(filePath))
			{
				_caches = new List<ClipboardCacheData>();
				return;
			}
			ClipboardCacheListData data = JsonConvert.DeserializeObject<ClipboardCacheListData>(File.ReadAllText(filePath));
			_caches = data?.Caches ?? new List<ClipboardCacheData>();
		}
	}
}
