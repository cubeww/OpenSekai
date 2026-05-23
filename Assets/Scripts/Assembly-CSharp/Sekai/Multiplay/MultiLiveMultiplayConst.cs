namespace Sekai.Multiplay
{
	public class MultiLiveMultiplayConst
	{
		public enum PartyMemberConnectStatus
		{
			Disconnected = 0,
			PreDisconnected = 1,
			Connected = 2
		}

		public enum RoomJoinRole
		{
			Create = 0,
			Join = 1
		}

		public const string LOBBY_NAME_RANDOM = "SEKAI_MULTI_LIVE_RANDOM";

		public const string LOBBY_NAME_TOTALPOWER = "SEKAI_MULTI_LIVE_TOTALPOWER";

		public const string LOBBY_NAME_PREFIX_PUBLIC = "PUBLIC_";

		public const string LOBBY_NAME_PREFIX_PRIVATE = "PRIVATE_";

		public const string LOBBY_NAME_PRIVATE_ROOM = "PRIVATE_SEKAI_MULTI_LIVE";

		public const int MAX_PLAYER = 5;

		public const int PLAYER_STATUS_LIVE_READY = 1;

		public const float ROOM_COUNTDOWN_TIMER_SEC = 30f;

		public const float KEEP_ALIVE_BACKGROUND_SEC = 10f;

		public const float PLAYER_TTL_SEC = 10f;

		public const float TOTALPOWER_MATCH_SEARCH_TIMEOUT = 4f;

		public const int COUNT_DOWN_CAUTION_START_SEC = 5;

		public const int MUSIC_SELECT_COUNT_DOWN_SEC = 20;

		public const int FINAL_CONFIRAMATION_COUNT_DOWN_SEC = 10;

		public MultiLiveMultiplayConst()
		{
		}
	}
}
