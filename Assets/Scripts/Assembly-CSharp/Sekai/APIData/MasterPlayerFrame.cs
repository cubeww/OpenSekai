using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterPlayerFrame
	{
		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("playerFrameGroupId")]
		public int playerFrameGroupId;

		[Key("description")]
		public string description;

		[Key("gameCharacterId")]
		public int gameCharacterId;

		public MasterPlayerFrame()
		{
		}
	}
}
