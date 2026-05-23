using Sekai.Live;

namespace Sekai
{
	public class LiveBootDataBase : BootDataBase
	{
		public int playedPreliminaryTournamentId;

		public int StageId;

		public LiveMusicData MusicData { get; set; }

		public LiveScoreMode LiveScoreMode { get; set; }

		public LiveTournamentMode LiveTournamentMode { get; set; }

		public LiveDeckData DeckData { get; set; }

		public LiveSettingData LiveSettingData { get; set; }

		public LiveModelData LiveModelData { get; set; }

		public LiveEventData LiveEventData { get; set; }

		public string UserLiveId { get; set; }

		public MusicCategory MusicCategory { get; set; }

		public MVQualityType MVQualityType { get; set; }

		public LivePlayMode LivePlayMode { get; set; }

		public LiveBundleBuildData BundleBuildData { get; private set; }

		public bool IsEventPlayed { get; protected set; }

		public int PlayEventId { get; protected set; }

		public bool IsAuto { get; set; }

		public bool IsCustomMusicScore { get; set; }

		public LiveType LiveType { get; }

		public bool canSkipDisplayMusicInfo { get; set; }

		public LiveBootDataBase(int musicId, string difficulty, int vocalId, int deckId, LiveType liveType, LiveMusicData.CollaborationModeState collaboModeState, bool is6MemberMv = false)
		{
			LiveType = liveType;
			MusicData = new LiveMusicData(musicId, difficulty, vocalId, liveType.ToString(), collaboModeState);
			DeckData = new LiveDeckData(deckId, is6MemberMv);
			LiveSettingData = LiveSettingData.LoadFromStorage();
			LiveModelData = new LiveModelData(DeckData);
			LiveEventData = new LiveEventData();
			MusicCategory = MusicCategory.original;
			MVQualityType = LiveSettingData?.QualityType ?? MVQualityType.Default;
			LiveScoreMode = LiveScoreMode.Normal;
			LiveTournamentMode = LiveTournamentMode.None;
		}

		public LiveBootDataBase(int musicId, string difficulty, int vocalId, LiveType liveType, LiveMusicData.CollaborationModeState collaboModeState)
			: this(musicId, difficulty, vocalId, 0, liveType, collaboModeState)
		{
		}

		public void SetLiveMode(LiveSettingData.LiveModeType liveMode)
		{
			LiveSettingData ??= new LiveSettingData();
			LiveSettingData.LiveMode = liveMode;
		}
	}
}
