using System.Collections.Generic;
using MessagePack;
using Sekai.ApiData;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMaterialExchangeSummary
	{
		public enum Category
		{
			None = 0,
			master_crystal = 1,
			master_piece = 2,
			card_ticket = 3,
			vocal_card_ticket = 4,
			gacha_seal = 5,
			common_ticket = 6,
			home_exchange = 7,
			mysekai_material_game_character = 8
		}

		[Key("id")]
		public int Id;

		[Key("startAt")]
		public long StartAt;

		[Key("endAt")]
		public long EndAt;

		[Key("materialExchanges")]
		public List<MasterMaterialExchange> MaterialExchanges;

		[Key("name")]
		public string Name;

		[Key("exchangeCategory")]
		public string ExchangeCategory;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("seq")]
		public int seq;

		[Key("materialExchangeType")]
		public string materialExchangeType;

		[Key("notificationRemainHour")]
		public int notificationRemainHour;

		[Key("materialExchangeDisplayResourceGroupId")]
		public int materialExchangeDisplayResourceGroupId;

		[Key("materialExchangeDisplayResourceGroups")]
		public MaterialExchangeDisplayResourceGroup[] materialExchangeDisplayResourceGroups;

		[Key("materialExchangeFreebieGroupJson")]
		public MaterialExchangeFreebieGroupJson materialExchangeFreebieGroupJson;

		[Key("materialExchangeFreebies")]
		public MaterialExchangeFreebie[] materialExchangeFreebies;

		[IgnoreMember]
		public MaterialExchangeType ExchangeType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public Category ExchangeCategoryType
		{
			get
			{
				throw null;
			}
		}

		public MasterMaterialExchangeSummary()
		{
		}
	}
}
