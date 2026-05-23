using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public sealed class MusicScoreInfoModel : IDisposable
	{
		private const float PreviewDurationSeconds = 10f;
		private const string DecideWordingKey = "WORD_DECIDE";
		private const string PlayWordingKey = "WORD_MSM_PLAY";

		private Defines.ContentType _currentContentType;

		private readonly MusicScoreInfoViewData _viewData;

		[CanBeNull]
		private MusicScoreData _musicScoreData;

		[CanBeNull]
		private MusicScoreData _baseMusicScoreData;

		[CanBeNull]
		private MusicScoreData _childMusicScoreData;

		[CanBeNull]
		private readonly LiveMusicInfo _liveMusicInfo;

		private bool _isLiveMusicDownloaded;
		private string _liveMusicBundleName;
		private string _liveMusicCueName;
		private float _liveMusicFillerSec;

		[CanBeNull]
		public MusicScoreData AppliedMusicScoreData
		{
			get
			{
				return _baseMusicScoreData ?? _musicScoreData;
			}
		}

		public MusicScoreInfoViewData ViewData
		{
			get
			{
				return _viewData;
			}
		}

		public bool IsViewingBaseMusicScore
		{
			get
			{
				return _baseMusicScoreData != null;
			}
		}

		public bool IsLiveMusicDownloaded
		{
			get
			{
				return _isLiveMusicDownloaded;
			}
		}

		public string LiveMusicBundleName
		{
			get
			{
				return _liveMusicBundleName;
			}
		}

		public string LiveMusicCueName
		{
			get
			{
				return _liveMusicCueName;
			}
		}

		public float LiveMusicFillerSec
		{
			get
			{
				return _liveMusicFillerSec;
			}
		}

		public float StartLiveMusicSec
		{
			get
			{
				return _liveMusicFillerSec + PreviewStartTimeSec;
			}
		}

		public float CurrentLiveMusicSec
		{
			get
			{
				return _liveMusicFillerSec + CurrentMusicTime;
			}
		}

		public float PreviewStartTimeSec
		{
			get
			{
				return AppliedMusicScoreData != null ? AppliedMusicScoreData.PreviewStartTimeSec : 0f;
			}
		}

		public float CurrentMusicTime
		{
			get
			{
				return _viewData.PreviewInfoViewData.CurrentMusicTimeSec;
			}
		}

		public bool IsPlayingPreview
		{
			get
			{
				return ShouldPlayingPreview && IsReadyPreviewPlaying;
			}
		}

		private bool ShouldPlayingPreview
		{
			get
			{
				return _viewData.PreviewInfoViewData.IsPlaying;
			}
		}

		internal bool IsReadyPreviewPlaying { get; private set; }

		private bool IsValidBookmark { get; set; }

		public void ApplyMusicScoreData([CanBeNull] MusicScoreData musicScoreData)
		{
			_musicScoreData = musicScoreData;
			_baseMusicScoreData = null;
			_childMusicScoreData = null;
			IsReadyPreviewPlaying = false;
			ApplyLiveMusicInfo();
			ApplyToViewData();
		}

		public void ClearBaseMusicScoreData()
		{
			_baseMusicScoreData = null;
			_childMusicScoreData = null;
			IsReadyPreviewPlaying = false;
			ApplyLiveMusicInfo();
			ApplyToViewData();
		}

		public void SetIsValidBookmark(bool isValidBookmark)
		{
			IsValidBookmark = isValidBookmark;
			_viewData.IsBookMarkButtonVisible = ShouldShowBookmarkButtonVisible();
			_viewData.ShowsBookMarkButton = ShouldShowBookmarkButton();
		}

		private void ApplyToViewData()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				_viewData.PreviewInfoViewData.ApplyEmpty();
				_viewData.DetailInfoViewData.ApplyEmpty();
				_viewData.IsShowBaseMusicScoreButton = false;
				_viewData.IsDecideButtonEnabled = false;
				_viewData.IsLockButtonEnabled = false;
				_viewData.ShowsDeleteButton = false;
				_viewData.IsLiveMusicDownloadButtonEnable = false;
				_viewData.IsBookMarkButtonVisible = ShouldShowBookmarkButtonVisible();
				_viewData.ShowsBookMarkButton = false;
				_viewData.ShowsMusicScoreReviewButton = false;
				_viewData.IsReviewed = false;
				_viewData.DecideButtonWordingKey = GetDecideButtonWordingKey();
				_viewData.IsViewingBaseMusicScore = false;
				_viewData.ChildMusicScoreTitle = string.Empty;
				_viewData.ChildMusicScoreAuthorName = string.Empty;
				return;
			}

			_viewData.ApplyPreviewInfo(applied);
			_viewData.DetailInfoViewData.Apply(applied);
			_viewData.DetailInfoViewData.ApplyContentType(_currentContentType);
			_viewData.IsShowBaseMusicScoreButton = ShouldShowBaseMusicScoreButton();
			_viewData.IsDecideButtonEnabled = ShouldEnableDecideButton();
			_viewData.IsLockButtonEnabled = ShouldEnableLockButton();
			_viewData.IsBookMarkButtonVisible = ShouldShowBookmarkButtonVisible();
			_viewData.ShowsBookMarkButton = ShouldShowBookmarkButton();
			_viewData.ShowsDeleteButton = ShouldShowDeleteButton();
			_viewData.ShowsMusicScoreReviewButton = ShouldShowMusicScoreReviewButton();
			_viewData.IsReviewed = applied.IsReviewed;
			_viewData.DecideButtonWordingKey = GetDecideButtonWordingKey();
			_viewData.IsViewingBaseMusicScore = _baseMusicScoreData != null;
			_viewData.ChildMusicScoreTitle = _childMusicScoreData?.Title ?? string.Empty;
			_viewData.ChildMusicScoreAuthorName = _childMusicScoreData?.GetDisplayAuthorName() ?? string.Empty;
		}

		private bool ShouldShowBaseMusicScoreButton()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			return applied != null
				&& applied.HasBaseMusicScore
				&& _currentContentType != Defines.ContentType.MyMusicScoreSelectForCreate
				&& _currentContentType != Defines.ContentType.BookMarkMusicScoreListSelectForCreate;
		}

		private bool ShouldShowBookmarkButton()
		{
			return AppliedMusicScoreData != null && ShouldShowBookmarkButtonVisible();
		}

		private bool ShouldShowBookmarkButtonVisible()
		{
			if (!IsValidBookmark)
			{
				return false;
			}

			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				return true;
			}

			long? userId = ResolveCurrentUserId();
			if (!userId.HasValue)
			{
				// TODO(original): restore UserDataManager.UserInformation.UserId lookup.
				return true;
			}

			return applied.AuthorId != userId.Value;
		}

		private bool ShouldShowMusicScoreReviewButton()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				return false;
			}

			if (_currentContentType == Defines.ContentType.MyMusicScoreSelectForCreate
				|| _currentContentType == Defines.ContentType.BookMarkMusicScoreListSelectForCreate)
			{
				return false;
			}

			long? userId = ResolveCurrentUserId();
			if (userId.HasValue && applied.AuthorId == userId.Value)
			{
				return false;
			}

			return applied.IsReviewAllowed;
		}

		public bool NeedsReleaseConditionCheck()
		{
			int offset = (int)_currentContentType - (int)Defines.ContentType.MusicScoreListSelect;
			return offset >= 0 && offset <= 18 && ((0x6605D >> offset) & 1) != 0;
		}

		public int? GetReleaseConditionId()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null || HasSpecifiedMusic(applied.MusicId))
			{
				return null;
			}

			MasterMusicAll musicAll = LoadMasterMusicAll(applied.MusicId);
			return musicAll?.music != null ? musicAll.music.releaseConditionId : (int?)null;
		}

		private bool ShouldEnableDecideButton()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				return false;
			}

			if (IsPlayContent(_currentContentType))
			{
				return HasSpecifiedMusic(applied.MusicId);
			}

			if (_currentContentType == Defines.ContentType.BookMarkMusicScoreListSelectForCreate)
			{
				return applied.IsDerivativeAllowed && HasSpecifiedMusic(applied.MusicId);
			}

			if (_currentContentType == Defines.ContentType.MyMusicScoreSelectForCreate)
			{
				return applied.IsDerivativeAllowed || applied.IsMyMusicScore;
			}

			return false;
		}

		private bool ShouldEnableLockButton()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				return false;
			}

			if (_currentContentType == Defines.ContentType.BookMarkMusicScoreListSelectForCreate)
			{
				return applied.IsDerivativeAllowed && !HasSpecifiedMusic(applied.MusicId);
			}

			return IsPlayContent(_currentContentType) && !HasSpecifiedMusic(applied.MusicId);
		}

		private string GetDecideButtonWordingKey()
		{
			return IsPlayContent(_currentContentType) ? PlayWordingKey : DecideWordingKey;
		}

		private bool ShouldShowDeleteButton()
		{
			return _baseMusicScoreData == null
				&& _musicScoreData != null
				&& _currentContentType == Defines.ContentType.PublishedMusicScoreListSelect;
		}

		public bool AddPreviewCurrentMusicTime(float time)
		{
			return SyncPreviewTimeWithMusic(CurrentMusicTime + time);
		}

		public bool SyncPreviewTimeWithMusic(float scoreTime)
		{
			if (IsPreviewTimeExceeded(scoreTime))
			{
				HandlePreviewLoop(scoreTime);
				return true;
			}

			_viewData.PreviewInfoViewData.SetCurrentMusicTime(scoreTime);
			return false;
		}

		private bool IsPreviewTimeExceeded(float time)
		{
			return PreviewStartTimeSec + PreviewDurationSeconds <= time;
		}

		private void HandlePreviewLoop(float currentTime)
		{
			_viewData.PreviewInfoViewData.SetCurrentMusicTime(CalculateLoopedTime(currentTime));
		}

		private float CalculateLoopedTime(float currentTime)
		{
			float previewStart = PreviewStartTimeSec;
			float delta = currentTime - (previewStart + PreviewDurationSeconds);
			return previewStart + delta % PreviewDurationSeconds;
		}

		public void SetPreviewPlaying(bool isPlaying)
		{
			_viewData.PreviewInfoViewData.SetPlaying(isPlaying);
		}

		public void ApplyContentType(Defines.ContentType contentType)
		{
			if (_currentContentType == contentType)
			{
				return;
			}

			_currentContentType = contentType;
			_viewData.DetailInfoViewData.ApplyContentType(contentType);
		}

		public void ApplyForList()
		{
			_viewData.ApplyViewType(MusicScoreInfo.ViewType.List);
		}

		public void ApplyForDetail()
		{
			_viewData.ApplyViewType(MusicScoreInfo.ViewType.Detail);
		}

		public async UniTask FetchAndApplyBaseMusicScoreDataAsync(CancellationToken ct = default)
		{
			MusicScoreData applied = AppliedMusicScoreData;
			string baseMusicScoreId = applied?.BaseMusicScoreId;
			if (string.IsNullOrEmpty(baseMusicScoreId))
			{
				return;
			}

			UserCustomMusicScorePublishedResponse response = await FetchPublishedResponseByIdAsync(baseMusicScoreId, ct);
			if (response == null || ct.IsCancellationRequested)
			{
				return;
			}

			MusicScoreData baseData = MusicScoreDataFactory.ConvertToMusicScoreData(response, MusicScoreDataFactory.CreateBookmarkedIdSet());
			SetPreviewPlaying(false);
			ApplyBaseMusicScoreData(baseData);
		}

		public async UniTask ExecutePostCustomMusicScoreBookmark()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				return;
			}

			await ExecuteOptionalServiceAsync("Sekai.Service.CustomMusicScoreBookmarkService", applied.Id);
			applied.SetIsBookmarked(true);
			ApplyToViewData();
		}

		public async UniTask ExecutePostCustomMusicScoreReview()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				return;
			}

			await ExecuteOptionalServiceAsync("Sekai.Service.CustomMusicScoreReviewService", applied.Id);
			applied.SetIsReviewed(true);
			ApplyToViewData();
		}

		public async UniTask ExecuteDeleteCustomMusicScoreBookmark()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				return;
			}

			await ExecuteOptionalServiceAsync("Sekai.Service.DeleteCustomMusicScoreBookmarkService", applied.Id);
			applied.SetIsBookmarked(false);
			ApplyToViewData();
		}

		public void EnableReadyPreviewPlaying()
		{
			IsReadyPreviewPlaying = true;
		}

		public void DisableReadyPreviewPlaying()
		{
			IsReadyPreviewPlaying = false;
		}

		public void ResetScorePlayback()
		{
			_viewData.PreviewInfoViewData.SetCurrentMusicTime(PreviewStartTimeSec);
		}

		public async UniTask CreateMusicScoreDataAsync(CancellationToken cancellationToken = default)
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (string.IsNullOrEmpty(applied?.MusicScorePath))
			{
				_viewData.PreviewInfoViewData.SetMusicScoreData(null);
				return;
			}

			MusicScoreMakerData data = await MusicScoreDataFactory.CreateMusicScoreMakerDataForPreviewAsync(applied.MusicScorePath, applied.IsOfficial, cancellationToken);
			_viewData.PreviewInfoViewData.SetMusicScoreData(data);
		}

		private void ApplyBaseMusicScoreData(MusicScoreData musicScoreData)
		{
			_childMusicScoreData = AppliedMusicScoreData;
			_baseMusicScoreData = musicScoreData;
			ApplyLiveMusicInfo();
			ApplyToViewData();
			ResetScorePlayback();
		}

		public void ApplyLiveMusicInfo()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				ClearLiveMusicInfo();
				_viewData.IsLiveMusicDownloadButtonEnable = false;
				return;
			}

			MasterMusicAll masterMusicAll = LoadMasterMusicAll(applied.MusicId);
			_liveMusicFillerSec = masterMusicAll?.music != null ? masterMusicAll.music.fillerSec : 0f;
			_liveMusicBundleName = string.Empty;
			_liveMusicCueName = string.Empty;
			_isLiveMusicDownloaded = false;
			_viewData.IsLiveMusicDownloadButtonEnable = !_isLiveMusicDownloaded;
			// TODO(original): restore LiveMusicInfo.ApplyFromMusicScoreData once MasterMusicAllModel, MusicUtility, AssetBundleNames, and AssetBundleManager are available.
		}

		public void RefreshData()
		{
			ApplyLiveMusicInfo();
			ApplyToViewData();
		}

		public void Dispose()
		{
			ClearLiveMusicInfo();
		}

		public MusicScoreInfoModel()
		{
			_viewData = new MusicScoreInfoViewData();
			_liveMusicInfo = null;
			IsValidBookmark = true;
			ClearLiveMusicInfo();
			// TODO(original): instantiate LiveMusicInfo after its implementation is restored.
		}

		private static bool IsPlayContent(Defines.ContentType contentType)
		{
			return contentType == Defines.ContentType.MusicScoreListSelect
				|| contentType == Defines.ContentType.RankingMusicScoreListSelect
				|| contentType == Defines.ContentType.NewArrivalMusicScoreListSelect
				|| contentType == Defines.ContentType.SearchResultMusicScoreListSelect
				|| contentType == Defines.ContentType.PublishedMusicScoreListSelect
				|| contentType == Defines.ContentType.MusicScoreCreatorTop
				|| contentType == Defines.ContentType.MusicScoreInfo
				|| contentType == Defines.ContentType.BookMarkMusicScoreListSelect;
		}

		private void ClearLiveMusicInfo()
		{
			_isLiveMusicDownloaded = false;
			_liveMusicBundleName = string.Empty;
			_liveMusicCueName = string.Empty;
			_liveMusicFillerSec = 0f;
		}

		private static bool HasSpecifiedMusic(int musicId)
		{
			object result = InvokeStatic("Sekai.MusicUtility", "HasSpecifiedMusic", musicId);
			if (result is bool hasSpecifiedMusic)
			{
				return hasSpecifiedMusic;
			}

			// TODO(original): restore MusicUtility.HasSpecifiedMusic dependency.
			return true;
		}

		private static MasterMusicAll LoadMasterMusicAll(int musicId)
		{
			object manager = GetSingletonInstance("Sekai.MasterDataManager");
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

		private static async UniTask<UserCustomMusicScorePublishedResponse> FetchPublishedResponseByIdAsync(string id, CancellationToken ct)
		{
			Type serviceType = FindType("Sekai.Service.CustomMusicScorePublishedSearchByIdService");
			if (serviceType == null)
			{
				// TODO(original): restore CustomMusicScorePublishedSearchByIdService.
				return null;
			}

			object service;
			try
			{
				service = Activator.CreateInstance(serviceType, id);
			}
			catch
			{
				return null;
			}

			object result = InvokeMember(service, "ExecuteAsync", false);
			if (result is UniTask<UserCustomMusicScorePublishedResponse> task)
			{
				return await task.AttachExternalCancellation(ct);
			}

			return null;
		}

		private static async UniTask ExecuteOptionalServiceAsync(string serviceTypeName, string musicScoreId)
		{
			Type serviceType = FindType(serviceTypeName);
			if (serviceType == null)
			{
				// TODO(original): restore custom music score bookmark/review service classes.
				return;
			}

			object service;
			try
			{
				service = Activator.CreateInstance(serviceType);
			}
			catch
			{
				return;
			}

			object result = InvokeMember(service, "ExecuteAsync", musicScoreId);
			if (result is UniTask task)
			{
				await task;
				return;
			}

			if (result is UniTask<bool> boolTask)
			{
				await boolTask;
			}
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
