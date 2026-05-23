using MessagePack;

namespace Sekai.MultiLive
{
	[MessagePackObject(false)]
	public struct MemberCharacterRank
	{
		[Key("characterId")]
		public int characterId;

		[Key("characterRank")]
		public int characterRank;
	}
}
