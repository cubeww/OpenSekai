using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.OutGame.Common;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScorePreviewInfoViewData
	{
		private const string PreviewNotFoundWordingKey = "WORD_MUSIC_SCORE_PREVIEW_NOT_FOUND";
		private const string MasterDataManagerTypeName = "Sekai.MasterDataManager";

		private MusicScoreInfo.ViewType _viewType;

		public string ScoreId { get; private set; }

		public string ScoreComment { get; private set; }

		public (string assetBundleName, string fileName) MusicJacketResourceInfo { get; private set; }

		public string MusicTitle { get; private set; }

		public string MusicArtistName { get; private set; }

		public string HeaderMusicTitle
		{
			get
			{
				return string.IsNullOrEmpty(MusicArtistName) ? MusicTitle : string.Concat(MusicTitle, " - ", MusicArtistName);
			}
		}

		public MusicDifficulty ScoreDifficulty { get; private set; }

		public MusicScoreMakerData MusicScoreData { get; private set; }

		public float PreviewStartTimeSec { get; private set; }

		public string[] TagNames { get; private set; }

		public bool IsPlaying { get; private set; }

		public float CurrentMusicTimeSec { get; private set; }

		public bool ShowsMusicJacket
		{
			get
			{
				return _viewType == MusicScoreInfo.ViewType.Detail;
			}
		}

		public bool ShowsMusicTitle
		{
			get
			{
				return _viewType == MusicScoreInfo.ViewType.Detail;
			}
		}

		public bool ShowsMusicArtistName
		{
			get
			{
				return _viewType == MusicScoreInfo.ViewType.Detail;
			}
		}

		public bool ShowsComment
		{
			get
			{
				return _viewType == MusicScoreInfo.ViewType.List;
			}
		}

		public bool ShowDetailButton
		{
			get
			{
				return _viewType == MusicScoreInfo.ViewType.List && !IsEmpty;
			}
		}

		public bool ShowTag
		{
			get
			{
				return _viewType == MusicScoreInfo.ViewType.List;
			}
		}

		public bool IsEmpty { get; private set; }

		public MusicScorePreviewInfoViewData()
		{
			ApplyEmpty();
		}

		public void ApplyEmpty()
		{
			IsEmpty = true;
			ScoreId = string.Empty;
			ScoreComment = string.Empty;
			MusicJacketResourceInfo = (string.Empty, string.Empty);
			MusicTitle = GetWording(PreviewNotFoundWordingKey);
			MusicArtistName = string.Empty;
			ScoreDifficulty = default;
			MusicScoreData = null;
			PreviewStartTimeSec = 0f;
			TagNames = Array.Empty<string>();
			IsPlaying = false;
			CurrentMusicTimeSec = 0f;
		}

		public void Apply([CanBeNull] MusicScoreData musicScoreData)
		{
			if (musicScoreData == null)
			{
				ApplyEmpty();
				return;
			}

			MasterMusicAll masterMusicAll = LoadMasterMusicAll(musicScoreData.MusicId);
			IsEmpty = false;
			ScoreId = musicScoreData.Id ?? string.Empty;
			ScoreComment = musicScoreData.Description ?? string.Empty;
			MusicJacketResourceInfo = GetJacketResourceInfo(masterMusicAll);
			MusicTitle = masterMusicAll?.music?.title ?? string.Empty;
			MusicArtistName = masterMusicAll?.musicArtist?.name ?? string.Empty;
			ScoreDifficulty = musicScoreData.Difficulty;
			MusicScoreData = null;
			PreviewStartTimeSec = musicScoreData.PreviewStartTimeSec;
			TagNames = GetTagNames(musicScoreData.TagIdArray, musicScoreData.IsOfficial);
			IsPlaying = true;
			CurrentMusicTimeSec = musicScoreData.PreviewStartTimeSec;
		}

		public void ApplyViewType(MusicScoreInfo.ViewType viewType)
		{
			_viewType = viewType;
		}

		public void AddCurrentMusicTime(float time)
		{
			CurrentMusicTimeSec += time;
		}

		public void SetPlaying(bool isPlaying)
		{
			IsPlaying = isPlaying;
		}

		public void SetCurrentMusicTime(float currentTime)
		{
			CurrentMusicTimeSec = currentTime;
		}

		public void SetMusicScoreData(MusicScoreMakerData musicScoreData)
		{
			MusicScoreData = musicScoreData;
		}

		private static string[] GetTagNames(int[] tagIdArray, bool isOfficial)
		{
			if (tagIdArray == null || tagIdArray.Length == 0)
			{
				return Array.Empty<string>();
			}

			List<(int index, int sortOrder, string name)> tags = new List<(int index, int sortOrder, string name)>(tagIdArray.Length);
			for (int i = 0; i < tagIdArray.Length; i++)
			{
				if (TryGetTagData(tagIdArray[i], out string name, out int sortOrder) && !string.IsNullOrEmpty(name))
				{
					tags.Add((i, sortOrder, name));
				}
			}

			if (!isOfficial)
			{
				tags.Sort((a, b) =>
				{
					int sort = a.sortOrder.CompareTo(b.sortOrder);
					return sort != 0 ? sort : a.index.CompareTo(b.index);
				});
			}

			return tags.Select(tag => tag.name).ToArray();
		}

		private static MasterMusicAll LoadMasterMusicAll(int musicId)
		{
			object manager = GetSingletonInstance(MasterDataManagerTypeName);
			object music = InvokeMember(manager, "GetMasterMusicAll", musicId);
			if (music is MasterMusicAll masterMusicAll)
			{
				return masterMusicAll;
			}

			object map = InvokeMember(manager, "GetMasterMusicAllMap");
			if (map is IDictionary dictionary)
			{
				foreach (object value in dictionary.Values)
				{
					if (value is MasterMusicAll candidate && candidate.music != null && candidate.music.id == musicId)
					{
						return candidate;
					}
				}
			}

			// TODO(original): restore MasterDataManager.GetMasterMusicAll dependency.
			return null;
		}

		private static (string assetBundleName, string fileName) GetJacketResourceInfo(MasterMusicAll masterMusicAll)
		{
			object model = CreateMasterMusicAllModel(masterMusicAll);
			object result = InvokeStatic("Sekai.MusicUtility", "GetJacketResourceInfo", model ?? masterMusicAll);
			if (result is ValueTuple<string, string> tuple)
			{
				return tuple;
			}

			// TODO(original): restore MusicUtility.GetJacketResourceInfo/MasterMusicAllModel.
			return (masterMusicAll?.music?.assetbundleName ?? string.Empty, string.Empty);
		}

		private static object CreateMasterMusicAllModel(MasterMusicAll masterMusicAll)
		{
			if (masterMusicAll == null)
			{
				return null;
			}

			Type modelType = FindType("Sekai.MasterMusicAllModel");
			if (modelType == null)
			{
				return null;
			}

			try
			{
				return Activator.CreateInstance(modelType, masterMusicAll);
			}
			catch
			{
				return null;
			}
		}

		private static bool TryGetTagData(int id, out string name, out int sortOrder)
		{
			name = null;
			sortOrder = 0;

			object tagMap = InvokeMember(GetSingletonInstance(MasterDataManagerTypeName), "GetMasterCustomMusicScoreTags");
			object tag = GetDictionaryValue(tagMap, id);
			if (tag == null)
			{
				// TODO(original): restore MasterCustomMusicScoreTag master data access.
				return false;
			}

			name = GetString(tag, "name") ?? GetString(tag, "Name");
			sortOrder = GetInt(tag, "sortOrder", GetInt(tag, "SortOrder", GetInt(tag, "seq", GetInt(tag, "Seq", id))));
			return true;
		}

		private static string GetWording(string key)
		{
			object wording = InvokeStatic("Sekai.WordingManager", "Get", key);
			return wording as string ?? key;
		}

		private static object GetDictionaryValue(object dictionary, int key)
		{
			if (dictionary is IDictionary nonGeneric)
			{
				return nonGeneric.Contains(key) ? nonGeneric[key] : null;
			}

			if (dictionary is IEnumerable enumerable)
			{
				foreach (object item in enumerable)
				{
					object candidate = GetMemberValue(item, "Value") ?? item;
					if (GetInt(candidate, "id", GetInt(candidate, "Id", int.MinValue)) == key)
					{
						return candidate;
					}
				}
			}

			return null;
		}

		private static object GetSingletonInstance(string typeName)
		{
			Type type = FindType(typeName);
			if (type == null)
			{
				return null;
			}

			const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy;
			return type.GetProperty("Instance", flags)?.GetValue(null)
				?? type.GetField("Instance", flags)?.GetValue(null)
				?? type.GetField("instance", flags)?.GetValue(null);
		}

		private static object InvokeStatic(string typeName, string methodName, params object[] args)
		{
			Type type = FindType(typeName);
			if (type == null)
			{
				return null;
			}

			return InvokeMember(type, methodName, args);
		}

		private static object InvokeMember(object targetOrType, string methodName, params object[] args)
		{
			if (targetOrType == null)
			{
				return null;
			}

			try
			{
				Type type = targetOrType as Type ?? targetOrType.GetType();
				const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
				foreach (MethodInfo method in type.GetMethods(flags))
				{
					if (method.Name == methodName && method.GetParameters().Length == args.Length)
					{
						return method.Invoke(method.IsStatic ? null : targetOrType, args);
					}
				}
			}
			catch
			{
			}

			return null;
		}

		private static string GetString(object target, string memberName)
		{
			return GetMemberValue(target, memberName) as string;
		}

		private static int GetInt(object target, string memberName, int fallback = 0)
		{
			object value = GetMemberValue(target, memberName);
			if (value == null)
			{
				return fallback;
			}

			try
			{
				return Convert.ToInt32(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return fallback;
			}
		}

		private static object GetMemberValue(object target, string name)
		{
			if (target == null)
			{
				return null;
			}

			try
			{
				Type type = target.GetType();
				const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
				return type.GetProperty(name, flags)?.GetValue(target)
					?? type.GetField(name, flags)?.GetValue(target);
			}
			catch
			{
				return null;
			}
		}

		private static Type FindType(string fullName)
		{
			Type type = Type.GetType(fullName);
			if (type != null)
			{
				return type;
			}

			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				type = assembly.GetType(fullName);
				if (type != null)
				{
					return type;
				}
			}

			return null;
		}
	}
}
