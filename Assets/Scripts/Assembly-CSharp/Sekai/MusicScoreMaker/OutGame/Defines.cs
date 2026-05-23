namespace Sekai.MusicScoreMaker.OutGame
{
	public static class Defines
	{
		public enum CategoryType
		{
			Home = 0,
			Ranking = 1,
			NewArrival = 2,
			Music = 3,
			Search = 4,
			Published = 5,
			SaveDraft = 6,
			AutoSave = 7,
			MusicScoreCreate = 8,
			BookMark = 9,
			PublishedForCreate = 10,
			BookMarkForCreate = 11
		}

		public enum ContentType
		{
			None = 0,
			MusicScoreListSelect = 4,
			SearchDetail = 5,
			RankingMusicScoreListSelect = 6,
			NewArrivalMusicScoreListSelect = 7,
			SearchResultMusicScoreListSelect = 8,
			PublishedMusicScoreListSelect = 10,
			SaveDraftMusicScoreListSelect = 11,
			AutoSaveMusicScoreListSelect = 12,
			MusicScoreCreateTypeSelect = 13,
			MyMusicScoreSelectForCreate = 16,
			MusicScoreCreatorTop = 17,
			MusicScoreInfo = 18,
			SaveDraftMusicScoreLoadSelect = 19,
			AutoSaveMusicScoreLoadSelect = 20,
			BookMarkMusicScoreListSelect = 21,
			BookMarkMusicScoreListSelectForCreate = 22,
			SaveDraftMusicScoreCreateSelect = 23
		}

		public enum ApplyContentType
		{
			Forward = 0,
			FromBack = 1,
			Refresh = 2
		}

		public enum ClearStatusFilterType
		{
			All = 0,
			NotClear = 1,
			NotFullCombo = 2,
			NotAllPerfect = 3
		}

		public enum FullComboRateFilterType
		{
			All = 0,
			Range = 1
		}

		public enum ReviewCountFilterType
		{
			All = 0,
			OverThreshold = 1
		}

		public enum SaveDraftListType
		{
			None = 0,
			Save = 1,
			Load = 2,
			Create = 3
		}

		public static readonly float MinimumLoadingDisplaySeconds;

		public const float PreviewDurationSeconds = 10f;

		public static readonly string BackgroundBundleName;

		public static readonly string BackgroundFileName;

		public static readonly int PublishedMusicScoreMaxCount;

		public static readonly int SaveDraftMusicScoreMaxCount;

		public static readonly int AutoSaveMusicScoreMaxCount;

		public static readonly int SaveDraftMusicScoreTitleMaxLength;

		public static readonly int SaveDraftMusicScoreMemoMaxLength;

		public static readonly int MusicScoreTitleMaxLength;

		public static readonly int MusicScoreDescriptionMaxLength;

		public static readonly int PublishMinSelectableTagCount;

		public static readonly int PublishMaxSelectableTagCount;

		public static readonly int CustomMusicScoreBookmarkLimit;

		public const int ReviewCountDisplayMax = 9999999;

		public static readonly int UnselectedSlotNumber;

		public static readonly string InvalidMusicScoreId;

		public const int InvalidMusicScoreIndex = -1;

		static Defines()
		{
			MinimumLoadingDisplaySeconds = 0.5f;
			// TODO(original): restore AssetBundleNames.GetResultBackGroundBundleName/FileName(1)
			// once AssetBundleNames is copied back.
			BackgroundBundleName = string.Empty;
			BackgroundFileName = string.Empty;

			// TODO(original): restore MasterDataManager.GetMasterConfigToInt and
			// ClientConfig.MusicScoreMaker.AutoSaveMusicScoreLimit values.
			PublishedMusicScoreMaxCount = 100;
			SaveDraftMusicScoreMaxCount = 10;
			AutoSaveMusicScoreMaxCount = 20;
			SaveDraftMusicScoreTitleMaxLength = 30;
			SaveDraftMusicScoreMemoMaxLength = 100;
			MusicScoreTitleMaxLength = 30;
			MusicScoreDescriptionMaxLength = 140;
			PublishMinSelectableTagCount = 1;
			PublishMaxSelectableTagCount = 3;
			CustomMusicScoreBookmarkLimit = 100;
			UnselectedSlotNumber = 0;
			InvalidMusicScoreId = string.Empty;
		}
	}
}
