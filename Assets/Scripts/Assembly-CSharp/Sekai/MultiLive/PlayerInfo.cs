using MessagePack;

namespace Sekai.MultiLive
{
	[MessagePackObject(false)]
	public struct PlayerInfo
	{
		[Key("UserId")]
		public string UserId;

		[Key("UserName")]
		public string UserName;

		[Key("Index")]
		public int Index;

		[Key("Info")]
		public RoomUserBasicInfo Info;

		[Key("Difficulty")]
		public string Difficulty;

		[Key("CustomScoreId")]
		public string CustomScoreId;

		public bool PlayerEquals(PlayerInfo playerInfo)
		{
			throw null;
		}
	}
}
