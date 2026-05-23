using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGachaCeilExchangeCost
	{
		[Key("gachaCeilExchangeId")]
		public int gachaCeilExchangeId;

		[Key("gachaCeilItemId")]
		public int gachaCeilItemId;

		[Key("quantity")]
		public int quantity;

		[Key("resourceType")]
		public string resourceType;

		[Key("resourceId")]
		public int resourceId;

		[Key("substituteCostId")]
		public int substituteCostId;

		public MasterGachaCeilExchangeCost()
		{
		}
	}
}
