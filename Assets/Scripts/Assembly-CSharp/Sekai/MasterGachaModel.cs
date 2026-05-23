using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.ApiData;

namespace Sekai
{
	public class MasterGachaModel
	{
		public enum MemberSelectGachaType
		{
			None = 0,
			ColorfulSelectGacha = 1,
			SelectGacha = 2,
			RateChoiceGacha = 3,
			CharacterSelectGacha = 4
		}

		public MasterGacha MasterGacha
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int Id
		{
			get
			{
				throw null;
			}
		}

		public string GachaType
		{
			get
			{
				throw null;
			}
		}

		public string Name
		{
			get
			{
				throw null;
			}
		}

		public int Seq
		{
			get
			{
				throw null;
			}
		}

		public string AssetbundleName
		{
			get
			{
				throw null;
			}
		}

		public string MovieAssetbundleName
		{
			get
			{
				throw null;
			}
		}

		public long StartAt
		{
			get
			{
				throw null;
			}
		}

		public long EndAt
		{
			get
			{
				throw null;
			}
		}

		public int SpinLimit
		{
			get
			{
				throw null;
			}
		}

		public int GachaCeilItemId
		{
			get
			{
				throw null;
			}
		}

		public MasterGachaBehavior[] GachaBehaviors
		{
			get
			{
				throw null;
			}
		}

		public MasterGachaPickup[] GachaPickups
		{
			get
			{
				throw null;
			}
		}

		public MasterGachaInformation GachaInformation
		{
			get
			{
				throw null;
			}
		}

		public List<MasterGachaDetail> GachaDetails
		{
			get
			{
				throw null;
			}
		}

		public List<MasterGachaCardRarityRate> GachaCardRarityRates
		{
			get
			{
				throw null;
			}
		}

		public int WishSelectCount
		{
			get
			{
				throw null;
			}
		}

		public int WishFixedSelectCount
		{
			get
			{
				throw null;
			}
		}

		public int WishLimitedSelectCount
		{
			get
			{
				throw null;
			}
		}

		public int GachaBonusId
		{
			get
			{
				throw null;
			}
		}

		public int GachaBonusItemReceivableRewardGroupId
		{
			get
			{
				throw null;
			}
		}

		public int GachaFreebieGroupId
		{
			get
			{
				throw null;
			}
		}

		public int DrawableGachaHour
		{
			get
			{
				throw null;
			}
		}

		public bool IsShowPeriod
		{
			get
			{
				throw null;
			}
		}

		public MasterRateChoiceGachaWishModel[] MasterRateChoiceGachaWishesModels
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public GachaTypeEnum Type
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public bool ExistsGachaBonus
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public bool ExistsItemReceivableGachaBonus
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public bool ExistAnyGachaBonus
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public bool ExistPrizeRewards
		{
			get
			{
				throw null;
			}
		}

		public bool IsTicketOnlyGacha
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public MasterGachaBehavior[] GachaBehaviorsByGroupFilters
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public MasterGachaBonusItemReceivableRewardGroup[] GachaBonusItemReceivableRewardGroups
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public DateTime GetEndAt
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public MemberSelectGachaType SelectGachaType
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public MasterGachaModel(MasterGacha masterGacha)
		{
			throw null;
		}

		public float GetGachaBonusLastPoint()
		{
			throw null;
		}

		public bool HasSelectedCardBonus()
		{
			throw null;
		}

		public bool HasRandomItemBonus()
		{
			throw null;
		}

		public bool SelectedCharacter()
		{
			throw null;
		}

		private bool HasGachaBonusItemRewardByType(GachaBonusRewardType type)
		{
			throw null;
		}

		private MemberSelectGachaType GetMemberSelectGachaType(MasterGacha gacha)
		{
			throw null;
		}

		private MasterGachaModel()
		{
		}

		public static MasterGachaModel Dummy(MasterGacha masterGacha)
		{
			throw null;
		}
	}
}
