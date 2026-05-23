using System.Collections.Generic;
using JetBrains.Annotations;
using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterMaterialExchange
	{
		public const string REFRESH_CYCLE_NONE = "none";

		public const string REFRESH_CYCLE_WEEKLY = "weekly";

		public const string REFRESH_CYCLE_MONTHLY = "monthly";

		[Key("id")]
		public int id;

		[Key("materialExchangeSummaryId")]
		public int materialExchangeSummaryId;

		[Key("seq")]
		public int seq;

		[Key("displayName")]
		public string displayName;

		[Key("isDisplayQuantity")]
		public bool isDisplayQuantity;

		[Key("thumbnailAssetbundleName")]
		public string thumbnailAssetbundleName;

		[Key("resourceBoxId")]
		public int resourceBoxId;

		[Key("refreshCycle")]
		public string refreshCycle;

		[Key("exchangeLimit")]
		public int exchangeLimit;

		[Key("startAt")]
		public long startAt;

		[Key("endAt")]
		public long endAt;

		[Key("costs")]
		public List<MasterMaterialExchangeCost> costs;

		[Key("materialExchangeRelationParents")]
		public List<MasterMaterialExchangeRelationParent> materialExchangeRelationParents;

		[CanBeNull]
		public MasterMaterialExchangeCost GetMaterialExchangeCost(int materialExchangeCostGroupId)
		{
			throw null;
		}

		public MasterMaterialExchange()
		{
		}
	}
}
