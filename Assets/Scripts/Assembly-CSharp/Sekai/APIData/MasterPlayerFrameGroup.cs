using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterPlayerFrameGroup
	{
		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("name")]
		public string name;

		[Key("assetbundleName")]
		public string assetbundleName;

		public MasterPlayerFrameGroup()
		{
		}
	}
}
