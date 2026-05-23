using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreDetailInfoViewData
	{
		private const int ReviewCountDisplayMax = 9999999;
		private const string MasterDataManagerTypeName = "Sekai.MasterDataManager";

		public Defines.ContentType ContentType;

		private bool _isOfficial;

		private MusicScoreData _musicScoreData;

		public string ScoreId { get; set; }

		public long AuthorId { get; private set; }

		public string ScoreTitle { get; private set; }

		public MusicDifficulty ScoreDifficulty { get; private set; }

		public MusicClearStatus ClearStatus { get; private set; }

		public int ScoreLevel { get; private set; }

		public string ScoreCreatorName { get; private set; }

		public int PlayCount { get; private set; }

		public int ReviewCount
		{
			get
			{
				return _musicScoreData != null ? _musicScoreData.ReviewCount : 0;
			}
		}

		public int ReviewCountForDisplay
		{
			get
			{
				return Math.Min(ReviewCount, ReviewCountDisplayMax);
			}
		}

		public float FullComboRate { get; private set; }

		public string[] TagNames { get; private set; }

		public string Comment { get; private set; }

		public bool ShowBaseMusicScoreMarker { get; private set; }

		public bool ShowDerivativeAllowedMarker { get; private set; }

		public bool ShowToCreatorButton { get; private set; }

		public MusicScoreDetailInfoViewData()
		{
			ApplyEmpty();
		}

		public void ApplyEmpty()
		{
			_musicScoreData = null;
			Apply(string.Empty, 0L, string.Empty, default, default, 0, string.Empty, 0, 0f, Array.Empty<string>(), string.Empty, false, false, false);
		}

		public void Apply(string musicScoreId, long authorId, string musicScoreTitle, MusicDifficulty musicScoreDifficulty, MusicClearStatus clearStatus, int musicScoreLevel, string musicScoreCreator, int playCount, float fullComboRate, string[] tagNames, string musicScoreComment, bool showBaseMusicScoreMarker, bool showDerivativeAllowedMarker, bool isOfficial)
		{
			ScoreId = musicScoreId ?? string.Empty;
			AuthorId = authorId;
			ScoreTitle = musicScoreTitle ?? string.Empty;
			ScoreDifficulty = musicScoreDifficulty;
			ClearStatus = clearStatus;
			ScoreLevel = musicScoreLevel;
			ScoreCreatorName = musicScoreCreator ?? string.Empty;
			PlayCount = playCount;
			FullComboRate = fullComboRate;
			TagNames = tagNames ?? Array.Empty<string>();
			Comment = musicScoreComment ?? string.Empty;
			_isOfficial = isOfficial;
			ShowBaseMusicScoreMarker = showBaseMusicScoreMarker;
			ShowDerivativeAllowedMarker = showDerivativeAllowedMarker;
			ShowToCreatorButton = IsShowToCreatorButton();
		}

		public void Apply([CanBeNull] MusicScoreData musicScoreData)
		{
			if (musicScoreData == null)
			{
				ApplyEmpty();
				return;
			}

			_musicScoreData = musicScoreData;
			Apply(
				musicScoreData.Id,
				musicScoreData.AuthorId,
				musicScoreData.Title,
				musicScoreData.Difficulty,
				musicScoreData.ClearStatus,
				musicScoreData.PlayLevel,
				musicScoreData.GetDisplayAuthorName(),
				musicScoreData.PlayCount,
				musicScoreData.FullComboRate,
				GetTagNames(musicScoreData.TagIdArray, musicScoreData.IsOfficial),
				musicScoreData.Description,
				musicScoreData.HasBaseMusicScore,
				musicScoreData.IsDerivativeAllowed,
				musicScoreData.IsOfficial);
		}

		public void ApplyContentType(Defines.ContentType contentType)
		{
			ContentType = contentType;
			ShowToCreatorButton = IsShowToCreatorButton();
		}

		private string[] GetTagNames(int[] tagIdArray, bool isOfficial)
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

		private bool IsShowToCreatorButton()
		{
			if (_isOfficial)
			{
				return false;
			}

			long? currentUserId = ResolveCurrentUserId();
			if (currentUserId.HasValue && currentUserId.Value == AuthorId)
			{
				return false;
			}

			return ContentType != Defines.ContentType.PublishedMusicScoreListSelect
				&& ContentType != Defines.ContentType.MyMusicScoreSelectForCreate
				&& ContentType != Defines.ContentType.MusicScoreCreatorTop
				&& ContentType != Defines.ContentType.BookMarkMusicScoreListSelectForCreate;
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

		private static long? ResolveCurrentUserId()
		{
			object userDataManager = GetSingletonInstance("Sekai.UserDataManager");
			object userInformation = GetMemberValue(userDataManager, "UserInformation") ?? GetMemberValue(userDataManager, "userInformation");
			object userId = GetMemberValue(userInformation, "UserId") ?? GetMemberValue(userInformation, "userId");
			if (userId == null)
			{
				object accountManager = GetSingletonInstance("Sekai.UserAccountManager");
				object data = GetMemberValue(accountManager, "Data") ?? GetMemberValue(accountManager, "data");
				userId = GetMemberValue(data, "UserId") ?? GetMemberValue(data, "userId");
			}

			if (userId == null)
			{
				// TODO(original): current local UserAccountData does not expose the original UserId.
				return null;
			}

			try
			{
				return Convert.ToInt64(userId, CultureInfo.InvariantCulture);
			}
			catch
			{
				return null;
			}
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

		private static object InvokeMember(object target, string methodName, params object[] args)
		{
			if (target == null)
			{
				return null;
			}

			try
			{
				Type type = target.GetType();
				const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
				foreach (MethodInfo method in type.GetMethods(flags))
				{
					if (method.Name == methodName && method.GetParameters().Length == args.Length)
					{
						return method.Invoke(method.IsStatic ? null : target, args);
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
