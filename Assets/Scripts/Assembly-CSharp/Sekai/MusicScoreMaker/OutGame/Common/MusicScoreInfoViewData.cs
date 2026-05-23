using JetBrains.Annotations;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public sealed class MusicScoreInfoViewData
	{
		public MusicScorePreviewInfoViewData PreviewInfoViewData { get; }

		public MusicScoreDetailInfoViewData DetailInfoViewData { get; }

		public MusicScoreInfo.ViewType CurrentViewType { get; set; }

		public bool IsDetailInfoMode
		{
			get
			{
				return CurrentViewType == MusicScoreInfo.ViewType.Detail;
			}
		}

		public int MusicId { get; private set; }

		public bool IsShowBaseMusicScoreButton { get; set; }

		public bool IsBookmarked { get; set; }

		public bool IsBookMarkButtonVisible { get; set; }

		public bool ShowsBookMarkButton { get; set; }

		public bool IsDecideButtonEnabled { get; set; }

		public string DecideButtonWordingKey { get; set; }

		public bool IsLockButtonEnabled { get; set; }

		public bool ShowsDeleteButton { get; set; }

		public bool IsLiveMusicDownloadButtonEnable { get; set; }

		public bool ShowsMusicScoreReviewButton { get; set; }

		public bool IsReviewed { get; set; }

		public bool IsViewingBaseMusicScore { get; set; }

		public string ChildMusicScoreTitle { get; set; }

		public string ChildMusicScoreAuthorName { get; set; }

		public void ApplyPreviewInfo([CanBeNull] MusicScoreData musicScoreData)
		{
			MusicId = musicScoreData != null ? musicScoreData.MusicId : 0;
			PreviewInfoViewData.Apply(musicScoreData);
			PreviewInfoViewData.ApplyViewType(CurrentViewType);
			IsBookmarked = musicScoreData != null && musicScoreData.IsBookmarked;
		}

		public void ApplyViewType(MusicScoreInfo.ViewType viewType)
		{
			CurrentViewType = viewType;
			PreviewInfoViewData.ApplyViewType(viewType);
		}

		public MusicScoreInfoViewData()
		{
			PreviewInfoViewData = new MusicScorePreviewInfoViewData();
			DetailInfoViewData = new MusicScoreDetailInfoViewData();
			CurrentViewType = MusicScoreInfo.ViewType.List;
			DecideButtonWordingKey = "WORD_DECIDE";
			ChildMusicScoreTitle = string.Empty;
			ChildMusicScoreAuthorName = string.Empty;
			PreviewInfoViewData.ApplyViewType(CurrentViewType);
		}
	}
}
