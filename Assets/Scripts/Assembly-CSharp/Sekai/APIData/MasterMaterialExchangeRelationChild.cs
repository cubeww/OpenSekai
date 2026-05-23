using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterMaterialExchangeRelationChild
	{
		[Key("materialExchangeId")]
		public int materialExchangeId;

		[Key("materialExchangeCostGroupId")]
		public int materialExchangeCostGroupId;

		public MasterMaterialExchangeRelationChild()
		{
		}
	}
}
