using System;
using System.Globalization;
using System.Reflection;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreData
	{
		public string Id { get; }

		public long AuthorId { get; }

		public string AuthorName { get; }

		public int MusicId { get; }

		public int PlayLevel { get; }

		public MusicDifficulty Difficulty { get; }

		public string Title { get; }

		public string MusicScorePath { get; }

		public float PreviewStartTimeSec { get; }

		public string Description { get; }

		public int[] TagIdArray { get; }

		public int ReviewCount { get; private set; }

		public int PlayCount { get; }

		public float FullComboRate { get; }

		public bool IsBookmarked { get; private set; }

		public bool IsReviewed { get; private set; }

		public bool IsReviewAllowed { get; }

		public MusicClearStatus ClearStatus { get; }

		public bool HasBaseMusicScore
		{
			get
			{
				return !string.IsNullOrEmpty(BaseMusicScoreId);
			}
		}

		public string BaseMusicScoreId { get; }

		public bool IsDerivativeAllowed { get; }

		public bool IsMyMusicScore { get; set; }

		public long PublishedAt { get; }

		public bool IsOfficial { get; }

		public MusicScoreData(string id, long authorId, string authorName, int musicId, int playLevel, MusicDifficulty difficulty, string title, string musicScorePath, float previewStartTimeSec, string description, int[] tagIdArray, int reviewCount, int playCount, float fullComboRate, bool isBookmarked, bool isReviewed, bool isReviewAllowed, MusicClearStatus clearStatus, string baseMusicScoreId, bool isDerivativeAllowed, long publishedAt, bool isOfficial)
		{
			Id = id;
			AuthorId = authorId;
			AuthorName = authorName;
			MusicId = musicId;
			PlayLevel = playLevel;
			Difficulty = difficulty;
			Title = title;
			MusicScorePath = musicScorePath;
			PreviewStartTimeSec = previewStartTimeSec;
			Description = description;
			TagIdArray = tagIdArray ?? Array.Empty<int>();
			BaseMusicScoreId = baseMusicScoreId;
			IsBookmarked = isBookmarked;
			ReviewCount = reviewCount;
			PlayCount = playCount;
			FullComboRate = fullComboRate;
			IsReviewed = isReviewed;
			IsReviewAllowed = isReviewAllowed;
			ClearStatus = clearStatus;
			IsDerivativeAllowed = isDerivativeAllowed;
			IsOfficial = isOfficial;
			PublishedAt = publishedAt;
			IsMyMusicScore = ResolveCurrentUserId() == AuthorId;
		}

		public void SetIsBookmarked(bool isBookmarked)
		{
			IsBookmarked = isBookmarked;
		}

		public void SetIsReviewed(bool isReviewed)
		{
			IsReviewed = isReviewed;
		}

		public string GetDisplayAuthorName()
		{
			// TODO(original): route through Sekai.Utility.UserDataUtility.GetDisplayUserName once that utility is restored.
			if (!string.IsNullOrEmpty(AuthorName))
			{
				return AuthorName;
			}

			return AuthorId.ToString(CultureInfo.InvariantCulture);
		}

		private static long? ResolveCurrentUserId()
		{
			try
			{
				object instance = typeof(UserAccountManager).GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
				object data = GetMemberValue(instance, "Data") ?? GetMemberValue(instance, "data");
				object userId = GetMemberValue(data, "UserId") ?? GetMemberValue(data, "userId");
				if (userId == null)
				{
					// TODO(original): current local UserAccountData only exposes Credential; original has UserId.
					return null;
				}

				return Convert.ToInt64(userId, CultureInfo.InvariantCulture);
			}
			catch
			{
				return null;
			}
		}

		private static object GetMemberValue(object target, string name)
		{
			if (target == null)
			{
				return null;
			}

			Type type = target.GetType();
			const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			return type.GetProperty(name, flags)?.GetValue(target) ?? type.GetField(name, flags)?.GetValue(target);
		}
	}
}
