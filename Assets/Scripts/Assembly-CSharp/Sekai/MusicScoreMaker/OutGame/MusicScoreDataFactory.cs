using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using CP;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.Live;
using Sekai.MusicScoreMaker.Common;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.OutGame
{
	public static class MusicScoreDataFactory
	{
		public static MusicScoreFilterData CreateDefaultFilterData()
		{
			(int min, int max) range = GetDefaultPlayLevelRange();
			return CreateFilterData(
				Defines.ClearStatusFilterType.All,
				range.min,
				range.max,
				Defines.FullComboRateFilterType.All,
				0,
				100,
				Defines.ReviewCountFilterType.All,
				0,
				onlyDerivativeAllowed: false);
		}

		[CanBeNull]
		public static MusicScoreMakerData CreateMusicScoreMakerData(int musicId, string musicDifficulty)
		{
			try
			{
				object musicScore = CreateLiveMusicScore(musicId, musicDifficulty);
				if (musicScore is MusicScore score)
				{
					return new MusicScoreMakerData(score);
				}
			}
			catch (Exception exception)
			{
				LogUtility.LogError("CreateMusicScoreMakerData failed: {0}", exception.Message);
			}

			// TODO(original): restore Sekai.Live.MusicScoreFactory.Create when that factory is recovered.
			return null;
		}

		[ItemCanBeNull]
		public static async UniTask<MusicScoreMakerData> CreateMusicScoreMakerDataAsync(string path, bool isOfficialScore, CancellationToken ct)
		{
			try
			{
				MusicScoreDataDownloader downloader = new MusicScoreDataDownloader(MusicScoreDataDownloader.DownloadType.Default, isOfficialScore, path);
				return await downloader.ExecuteAsync(ct);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception exception)
			{
				LogUtility.LogError("CreateMusicScoreMakerDataAsync failed: {0}", exception.Message);
				// TODO(original): MusicScoreDataDownloader is still partially stubbed in this workspace.
				return null;
			}
		}

		[ItemCanBeNull]
		public static async UniTask<MusicScoreMakerData> CreateMusicScoreMakerDataForPreviewAsync(string path, bool isOfficialScore, CancellationToken ct)
		{
			try
			{
				MusicScoreDataDownloader downloader = new MusicScoreDataDownloader(MusicScoreDataDownloader.DownloadType.Preview, isOfficialScore, path);
				return await downloader.ExecuteAsync(ct);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception exception)
			{
				LogUtility.LogError("CreateMusicScoreMakerDataForPreviewAsync failed: {0}", exception.Message);
				// TODO(original): MusicScoreDataDownloader is still partially stubbed in this workspace.
				return null;
			}
		}

		public static IEnumerable<MusicScoreData> CreateMusicScoreDataListByPublished()
		{
			IEnumerable<UserCustomMusicScorePublishedResponse> responses = GetUserDataManagerEnumerable<UserCustomMusicScorePublishedResponse>("userCustomMusicScorePublisheds");
			return CreateMusicScoreDataList(responses);
		}

		public static HashSet<string> CreateBookmarkedIdSet()
		{
			IEnumerable<UserCustomMusicScorePublishedResponse> responses = GetUserDataManagerMethodEnumerable<UserCustomMusicScorePublishedResponse>("GetUserCustomMusicScoreBookmarksAsPublished");
			return new HashSet<string>(
				responses
					.Select(x => x?.userCustomMusicScoreId)
					.Where(x => !string.IsNullOrEmpty(x)));
		}

		public static IEnumerable<BookmarkedMusicScoreData> CreateBookmarkedMusicScoreDataList(IEnumerable<UserCustomMusicScorePublishedBookmarkResponse> bookmarkResponses)
		{
			if (bookmarkResponses == null)
			{
				return Enumerable.Empty<BookmarkedMusicScoreData>();
			}

			List<UserCustomMusicScorePublishedBookmarkResponse> bookmarkList = bookmarkResponses.ToList();
			HashSet<string> bookmarkedIdSet = new HashSet<string>(
				bookmarkList
					.Select(x => x?.userCustomMusicScorePublished?.userCustomMusicScoreId)
					.Where(x => !string.IsNullOrEmpty(x)));

			List<BookmarkedMusicScoreData> result = new List<BookmarkedMusicScoreData>(bookmarkList.Count);
			foreach (UserCustomMusicScorePublishedBookmarkResponse bookmark in bookmarkList)
			{
				if (bookmark?.userCustomMusicScorePublished == null)
				{
					continue;
				}

				MusicScoreData musicScoreData = ConvertToMusicScoreData(bookmark.userCustomMusicScorePublished, bookmarkedIdSet);
				if (musicScoreData == null)
				{
					continue;
				}

				try
				{
					result.Add(new BookmarkedMusicScoreData(musicScoreData, bookmark.bookmarkedAt));
				}
				catch (Exception exception)
				{
					LogUtility.LogError("CreateBookmarkedMusicScoreDataList failed: {0}", exception.Message);
					// TODO(original): BookmarkedMusicScoreData is outside this task's write scope and still has stubbed members.
				}
			}

			return result;
		}

		public static IEnumerable<MusicScoreData> CreateMusicScoreDataList(IEnumerable<UserCustomMusicScorePublishedResponse> responses)
		{
			if (responses == null)
			{
				return Enumerable.Empty<MusicScoreData>();
			}

			HashSet<string> bookmarkedIdSet = CreateBookmarkedIdSet();
			List<MusicScoreData> result = new List<MusicScoreData>();
			foreach (UserCustomMusicScorePublishedResponse response in responses)
			{
				MusicScoreData data = ConvertToMusicScoreData(response, bookmarkedIdSet);
				if (data != null)
				{
					result.Add(data);
				}
			}

			return result;
		}

		public static MusicScoreData ConvertToMusicScoreData(UserCustomMusicScorePublishedResponse response, HashSet<string> bookmarkedIdSet)
		{
			if (response == null)
			{
				return null;
			}

			if (!response.isOfficialScore)
			{
				return CreateMusicScoreDataFromResponse(response, bookmarkedIdSet);
			}

			object officialCreator = GetMasterCustomMusicScoreOfficialCreatorByScoreId(response.userCustomMusicScoreId);
			if (officialCreator == null)
			{
				LogUtility.LogError("MasterCustomMusicScoreOfficialCreator is not found. scoreId={0}", response.userCustomMusicScoreId);
				return CreateMusicScoreDataFromResponse(response, bookmarkedIdSet);
			}

			UserCustomMusicScoreInfo info = response.userCustomMusicScoreInfoJson;
			if (info == null)
			{
				return null;
			}

			object profile = GetMasterCustomMusicScoreOfficialCreatorProfile(GetInt(officialCreator, "customMusicScoreOfficialCreatorProfileId"));
			string authorName = GetString(profile, "name") ?? response.userName;
			MusicDifficulty difficulty = GetMusicDifficulty(officialCreator, "Difficulty", GetString(officialCreator, "musicDifficultyType"));
			string id = response.userCustomMusicScoreId;

			return new MusicScoreData(
				id,
				response.userId,
				authorName,
				GetInt(officialCreator, "musicId", response.musicId),
				GetInt(officialCreator, "playLevel", response.playLevel),
				difficulty,
				GetString(officialCreator, "title") ?? info.title,
				info.userCustomMusicScorePath,
				GetFloat(officialCreator, "previewStartTimeSec", response.previewStartTimeSec),
				GetString(officialCreator, "description") ?? response.description,
				GetOfficialTagIds(officialCreator),
				response.reviewCount,
				response.playCount,
				response.fullComboRate,
				ContainsId(bookmarkedIdSet, id),
				response.isReviewed,
				response.isReviewAllowed,
				GetClearStatus(response.playResult),
				info.baseMusicScoreId,
				GetBool(officialCreator, "isDerivativeAllowed", response.isDerivativeAllowed),
				GetLong(officialCreator, "publishedStartAt", response.publishedAt),
				isOfficial: true);
		}

		private static MusicScoreData CreateMusicScoreDataFromResponse(UserCustomMusicScorePublishedResponse response, HashSet<string> bookmarkedIdSet)
		{
			if (response?.userCustomMusicScoreInfoJson == null)
			{
				return null;
			}

			UserCustomMusicScoreInfo info = response.userCustomMusicScoreInfoJson;
			MusicDifficulty difficulty = MusicDifficulty.none;
			Enum.TryParse(response.musicDifficultyType, out difficulty);

			return new MusicScoreData(
				response.userCustomMusicScoreId,
				response.userId,
				GetResolvedUserName(response.userId, response.userName),
				response.musicId,
				response.playLevel,
				difficulty,
				info.title,
				info.userCustomMusicScorePath,
				response.previewStartTimeSec,
				response.description,
				response.customMusicScoreTags?.ToArray() ?? Array.Empty<int>(),
				response.reviewCount,
				response.playCount,
				response.fullComboRate,
				ContainsId(bookmarkedIdSet, response.userCustomMusicScoreId),
				response.isReviewed,
				response.isReviewAllowed,
				GetClearStatus(response.playResult),
				info.baseMusicScoreId,
				response.isDerivativeAllowed,
				response.publishedAt,
				response.isOfficialScore);
		}

		private static MusicScoreFilterData CreateFilterData(Defines.ClearStatusFilterType clearStatus, int minPlayLevel, int maxPlayLevel, Defines.FullComboRateFilterType fullComboRateFilterType, int minFullComboRate, int maxFullComboRate, Defines.ReviewCountFilterType reviewCountFilter, int reviewCountThreshold, bool onlyDerivativeAllowed)
		{
			try
			{
				return new MusicScoreFilterData(clearStatus, minPlayLevel, maxPlayLevel, fullComboRateFilterType, minFullComboRate, maxFullComboRate, reviewCountFilter, reviewCountThreshold, onlyDerivativeAllowed);
			}
			catch
			{
				// TODO(original): MusicScoreFilterData constructor is still stubbed in this workspace.
				MusicScoreFilterData data = (MusicScoreFilterData)FormatterServices.GetUninitializedObject(typeof(MusicScoreFilterData));
				SetField(data, nameof(MusicScoreFilterData.ClearStatus), clearStatus);
				SetField(data, nameof(MusicScoreFilterData.MinPlayLevel), minPlayLevel);
				SetField(data, nameof(MusicScoreFilterData.MaxPlayLevel), maxPlayLevel);
				SetField(data, nameof(MusicScoreFilterData.FullComboRateFilterType), fullComboRateFilterType);
				SetField(data, nameof(MusicScoreFilterData.MinFullComboRate), minFullComboRate);
				SetField(data, nameof(MusicScoreFilterData.MaxFullComboRate), maxFullComboRate);
				SetField(data, nameof(MusicScoreFilterData.ReviewCountFilter), reviewCountFilter);
				SetField(data, nameof(MusicScoreFilterData.ReviewCountThreshold), reviewCountThreshold);
				SetField(data, nameof(MusicScoreFilterData.OnlyDerivativeAllowed), onlyDerivativeAllowed);
				return data;
			}
		}

		private static (int min, int max) GetDefaultPlayLevelRange()
		{
			try
			{
				return new MusicScorePlayLevelRangeProvider().GetPlayLevelRange(MusicDifficulty.none);
			}
			catch
			{
				// TODO(original): restore MasterCustomMusicScoreDifficultyPlayLevel master data access.
				return (0, 0);
			}
		}

		private static object CreateLiveMusicScore(int musicId, string musicDifficulty)
		{
			Type factoryType = FindType("Sekai.Live.MusicScoreFactory");
			if (factoryType == null)
			{
				return null;
			}

			foreach (MethodInfo method in factoryType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
			{
				if (method.Name != "Create")
				{
					continue;
				}

				ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length == 4)
				{
					return method.Invoke(null, new object[] { musicId, musicDifficulty, 0, 0 });
				}
			}

			return null;
		}

		private static object GetMasterCustomMusicScoreOfficialCreatorByScoreId(string scoreId)
		{
			object masterDataManager = GetSingletonInstance("Sekai.MasterDataManager");
			return InvokeMember(masterDataManager, "GetMasterCustomMusicScoreOfficialCreatorByScoreId", scoreId);
		}

		private static object GetMasterCustomMusicScoreOfficialCreatorProfile(int profileId)
		{
			object masterDataManager = GetSingletonInstance("Sekai.MasterDataManager");
			return InvokeMember(masterDataManager, "GetMasterCustomMusicScoreOfficialCreatorProfile", profileId);
		}

		private static IEnumerable<T> GetUserDataManagerEnumerable<T>(string memberName)
		{
			object userDataManager = GetSingletonInstance("Sekai.UserDataManager");
			object value = GetMemberValue(userDataManager, memberName);
			return Enumerate<T>(value);
		}

		private static IEnumerable<T> GetUserDataManagerMethodEnumerable<T>(string methodName)
		{
			object userDataManager = GetSingletonInstance("Sekai.UserDataManager");
			object value = InvokeMember(userDataManager, methodName);
			return Enumerate<T>(value);
		}

		private static IEnumerable<T> Enumerate<T>(object value)
		{
			if (value is IEnumerable<T> typed)
			{
				return typed;
			}

			if (value is IEnumerable enumerable)
			{
				return enumerable.OfType<T>();
			}

			return Enumerable.Empty<T>();
		}

		private static string GetResolvedUserName(long userId, string userName)
		{
			// TODO(original): route through UserCustomMusicScorePublishedResponse.ResolvedUserName/UserDataUtility after APIData is restored.
			return userName;
		}

		private static MusicClearStatus GetClearStatus(string playResult)
		{
			switch (playResult)
			{
				case "full_perfect":
					return MusicClearStatus.AllPerfect;
				case "full_combo":
					return MusicClearStatus.FullCombo;
				case "clear":
					return MusicClearStatus.Clear;
				case "not_clear":
					return MusicClearStatus.NotClear;
				default:
					return MusicClearStatus.Undefined;
			}
		}

		private static bool ContainsId(HashSet<string> idSet, string id)
		{
			return idSet != null && !string.IsNullOrEmpty(id) && idSet.Contains(id);
		}

		private static int[] GetOfficialTagIds(object officialCreator)
		{
			int[] tagIds = GetIntArray(GetMemberValue(officialCreator, "TagIds"));
			if (tagIds != null)
			{
				return tagIds;
			}

			return new[]
				{
					GetInt(officialCreator, "tagId1"),
					GetInt(officialCreator, "tagId2"),
					GetInt(officialCreator, "tagId3")
				}
				.Where(id => id > 0)
				.ToArray();
		}

		private static MusicDifficulty GetMusicDifficulty(object target, string memberName, string fallbackDifficultyText)
		{
			object value = GetMemberValue(target, memberName);
			if (value is MusicDifficulty difficulty)
			{
				return difficulty;
			}

			if (value != null)
			{
				try
				{
					return (MusicDifficulty)Convert.ToInt32(value, CultureInfo.InvariantCulture);
				}
				catch
				{
				}
			}

			return Enum.TryParse(fallbackDifficultyText, out difficulty) ? difficulty : MusicDifficulty.none;
		}

		private static int[] GetIntArray(object value)
		{
			if (value is int[] array)
			{
				return array;
			}

			if (value is IEnumerable enumerable && !(value is string))
			{
				List<int> result = new List<int>();
				foreach (object item in enumerable)
				{
					if (item != null)
					{
						result.Add(Convert.ToInt32(item, CultureInfo.InvariantCulture));
					}
				}

				return result.ToArray();
			}

			return null;
		}

		private static string GetString(object target, string memberName)
		{
			object value = GetMemberValue(target, memberName);
			return value as string;
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

		private static long GetLong(object target, string memberName, long fallback = 0L)
		{
			object value = GetMemberValue(target, memberName);
			if (value == null)
			{
				return fallback;
			}

			try
			{
				return Convert.ToInt64(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return fallback;
			}
		}

		private static float GetFloat(object target, string memberName, float fallback = 0f)
		{
			object value = GetMemberValue(target, memberName);
			if (value == null)
			{
				return fallback;
			}

			try
			{
				return Convert.ToSingle(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return fallback;
			}
		}

		private static bool GetBool(object target, string memberName, bool fallback = false)
		{
			object value = GetMemberValue(target, memberName);
			if (value == null)
			{
				return fallback;
			}

			try
			{
				return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
			}
			catch
			{
				return fallback;
			}
		}

		private static object GetSingletonInstance(string typeName)
		{
			Type type = FindType(typeName);
			if (type == null)
			{
				return null;
			}

			const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy;
			return type.GetProperty("Instance", flags)?.GetValue(null) ?? type.GetField("instance", flags)?.GetValue(null);
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
				foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
				{
					if (method.Name != methodName || method.GetParameters().Length != args.Length)
					{
						continue;
					}

					return method.Invoke(target, args);
				}
			}
			catch
			{
			}

			return null;
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
				PropertyInfo property = type.GetProperty(name, flags);
				if (property != null)
				{
					return property.GetValue(target);
				}

				return type.GetField(name, flags)?.GetValue(target);
			}
			catch
			{
				return null;
			}
		}

		private static void SetField(object target, string name, object value)
		{
			target.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(target, value);
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
