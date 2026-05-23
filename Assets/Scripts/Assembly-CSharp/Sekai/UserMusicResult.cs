using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserMusicResult
	{
		public const string MUSIC_PLAY_TYPE_SOLO = "solo";

		public const string MUSIC_PLAY_TYPE_MULTI = "multi";

		public const string MUSIC_PLAY_RESULT_FULL_PERFECT = "full_perfect";

		public const string MUSIC_PLAY_RESULT_FULL_COMBO = "full_combo";

		public const string MUSIC_PLAY_RESULT_CLEAR = "clear";

		public const string MUSIC_PLAY_RESULT_NOT_CLEAR = "not_clear";

		[Key("musicId")]
		public int musicId;

		[Key("musicDifficultyType")]
		public string musicDifficultyType;

		[Key("playType")]
		public string playType;

		[Key("playResult")]
		public string playResult;

		[Key("highScore")]
		public int highScore;

		[Key("fullComboFlg")]
		public bool fullComboFlg;

		[Key("fullPerfectFlg")]
		public bool fullPerfectFlg;

		[Key("mvpCount")]
		public int mvpCount;

		[Key("superStarCount")]
		public int superStarCount;

		[IgnoreMember]
		public MusicDifficulty MusicDifficulty
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public int ClearStatus
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public MusicPlayType MusicPlayType
		{
			get
			{
				throw null;
			}
		}

		public UserMusicResult()
		{
			throw null;
		}
	}
}
