using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterEventItem
	{
		[Key("id")]
		public int id;

		[Key("eventId")]
		public int eventId;

		[Key("gameCharacterId")]
		public int? gameCharacterId;

		[Key("name")]
		public string name;

		[Key("fravorText")]
		public string fravorText;

		[Key("assetbundleName")]
		public string assetbundleName;

		public MasterEventItem()
		{
		}
	}
}
