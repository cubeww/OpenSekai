using System.Runtime.CompilerServices;

namespace Sekai
{
	public class ClientErrorInfomation
	{
		public const string ERROR_CODE_CHEAT = "cheat";

		public const string ERROR_CODE_LOGIN_BAN = "login_ban";

		public const string ERROR_CODE_CS_BAN = "cs_ban";

		public const string ERROR_CODE_CS_INSPECTING = "cs_inspecting";

		public const string ERROR_CODE_INVALID_TOKEN = "session_error";

		public const string ERROR_CODE_OUT_OF_PERIOD_APRIL_2022 = "out_of_period_2022_april_fool";

		public const string ERROR_CODE_USER_BLOCK_COUNT_LIMIT = "user_block_count_limit";

		public const string ERROR_CODE_STORY_INVALID_TERM = "invalid_term";

		public const string ERROR_CODE_COLLABO_MUSIC_OUT_OF_TERM = "limited_time_music_out_of_term";

		public const string LIVE_ALREADY_END = "live_already_end";

		public const string LIVE_RESULT_ALREADY_PROCESSED = "live_result_already_processed";

		public ClientErrorResponse Response
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ClientErrorInfomation()
		{
		}
	}
}
