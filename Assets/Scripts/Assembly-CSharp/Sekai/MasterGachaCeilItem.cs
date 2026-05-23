using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGachaCeilItem
	{
		[Key("id")]
		public int id;

		[Key("name")]
		public string name;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("convertStartAt")]
		public long convertStartAt;

		public bool IsEnableHeaderExchangeButton()
		{
			throw null;
		}

		public MasterGachaCeilItem()
		{
		}
	}
}
