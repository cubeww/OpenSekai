using System;
using System.Collections.Generic;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGacha
	{
		[Key("id")]
		public int id;

		[Key("gachaType")]
		public string gachaType;

		[Key("name")]
		public string name;

		[Key("seq")]
		public int seq;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("movieAssetbundleName")]
		public string movieAssetbundleName;

		[Key("startAt")]
		public long startAt;

		[Key("endAt")]
		public long endAt;

		[Key("spinLimit")]
		public int spinLimit;

		[Key("gachaCeilItemId")]
		public int gachaCeilItemId;

		[Key("gachaBehaviors")]
		public MasterGachaBehavior[] gachaBehaviors;

		[Key("gachaPickups")]
		public MasterGachaPickup[] gachaPickups;

		[Key("gachaInformation")]
		public MasterGachaInformation gachaInformation;

		[Key("gachaDetails")]
		public List<MasterGachaDetail> gachaDetails;

		[Key("gachaCardRarityRates")]
		public List<MasterGachaCardRarityRate> gachaCardRarityRates;

		[Key("wishSelectCount")]
		public int wishSelectCount;

		[Key("wishFixedSelectCount")]
		public int wishFixedSelectCount;

		[Key("wishLimitedSelectCount")]
		public int wishLimitedSelectCount;

		[Key("gachaBonusId")]
		public int gachaBonusId;

		[Key("gachaBonusItemReceivableRewardGroupId")]
		public int gachaBonusItemReceivableRewardGroupId;

		[Key("gachaFreebieGroupId")]
		public int gachaFreebieGroupId;

		[Key("drawableGachaHour")]
		public int drawableGachaHour;

		[Key("isShowPeriod")]
		public bool isShowPeriod;

		[Key("isSelectCharacter")]
		public bool isSelectCharacter;

		[Key("gachaCharacterBonusGroupId")]
		public int? gachaCharacterBonusGroupId;

		[Key("rateChoiceGachaWishGroupId")]
		public int rateChoiceGachaWishGroupId;

		[IgnoreMember]
		public GachaTypeEnum Type
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool ExistsGachaBonus
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool ExistsItemReceivableGachaBonus
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool ExistAnyGachaBonus
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsSelectCharacterGacha
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool ExistsCharacterBonus
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsTicketOnlyGacha
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsColorfulSelectGacha
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsSelectGacha
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsRateSelectGacha
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public MasterGachaBehavior[] GachaBehaviorsByGroupFilters
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public DateTime GetEndAt
		{
			get
			{
				throw null;
			}
		}

		public MasterGacha()
		{
		}
	}
}
