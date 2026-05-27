using JetBrains.Annotations;
using Sekai.Live;

namespace Sekai
{
	public class LiveMusicData
	{
		public enum CollaborationModeState
		{
			Off = 0,
			On = 1
		}

		private MusicScore musicScore;
		private int cachedCustomTotalNoteCount;

		public MasterMusic Music { get; set; }

		public bool IsCollaboration => CollaboModeState == CollaborationModeState.On;

		public MasterMusicCollaboration Collaboration { get; set; }

		public MasterMusicDifficulty Difficulty { get; set; }

		public MusicDifficulty DifficultyEnum { get; private set; }

		public string DifficultyString { get; private set; }

		public MasterMusicVocal Vocal { get; set; }

		public MasterPlayLevelScore Score { get; set; }

		public bool IsTestPlay { get; set; }

		public MusicScore MusicScore
		{
			get => musicScore;
			set
			{
				musicScore = value;
				cachedCustomTotalNoteCount = LiveUtility.CalculateTotalComboCount(value);
			}
		}

		public long StartMusicTimeMs { get; set; }

		public bool PlayStartEffectEnabled { get; set; }

		public MasterCollaborationModeModel CollaborationMode { get; private set; }

		public CollaborationModeState CollaboModeState { get; private set; }

		public bool IsUseCustomScore { get; set; }

		public int CustomPlayLevel { get; set; }

		public int PlayLevel => CustomPlayLevel > 0 ? CustomPlayLevel : Difficulty?.playLevel ?? 0;

		public int TotalNoteCount => cachedCustomTotalNoteCount > 0 ? cachedCustomTotalNoteCount : Difficulty?.totalNoteCount ?? 0;

		public LiveMusicData(int musicId, string difficulty, int vocalId, string liveType, CollaborationModeState collaboModeState)
		{
			DifficultyString = string.IsNullOrEmpty(difficulty) ? "master" : difficulty.ToLowerInvariant();
			DifficultyEnum = ParseDifficulty(DifficultyString);
			CollaboModeState = collaboModeState;
			Music = new MasterMusic
			{
				id = musicId,
				title = string.Empty,
				assetbundleName = musicId > 0 ? $"music_{musicId}" : string.Empty,
				fillerSec = 0f
			};
			Difficulty = new MasterMusicDifficulty
			{
				musicId = musicId,
				musicDifficulty = DifficultyString
			};
			Vocal = new MasterMusicVocal
			{
				id = vocalId,
				musicId = musicId,
				assetbundleName = musicId > 0 ? $"music_{musicId}_{vocalId}" : string.Empty
			};
			Score = new MasterPlayLevelScore { liveType = liveType, playLevel = 0 };
			PlayStartEffectEnabled = true;
		}

		[CanBeNull]
		private MasterCollaborationModeModel GetMasterCollaborationMode(MasterMusicAll musicAll)
		{
			return null;
		}

		private static MusicDifficulty ParseDifficulty(string value)
		{
			return System.Enum.TryParse(value, true, out MusicDifficulty result) ? result : MusicDifficulty.master;
		}
	}
}
