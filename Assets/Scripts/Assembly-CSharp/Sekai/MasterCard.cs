using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterCard : IMessagePackSerializationCallbackReceiver
	{
		public const int InvalidCardId = -1;

		public const string CARD_ATTR_1 = "cute";

		public const string CARD_ATTR_2 = "cool";

		public const string CARD_ATTR_3 = "pure";

		public const string CARD_ATTR_4 = "happy";

		public const string CARD_ATTR_5 = "mysterious";

		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("characterId")]
		public int characterId;

		[Key("cardRarityType")]
		public string cardRarityType;

		[Key("maxPower")]
		public int maxPower;

		[Key("specialTrainingPower1BonusFixed")]
		public int specialTrainingPower1BonusFixed;

		[Key("specialTrainingPower2BonusFixed")]
		public int specialTrainingPower2BonusFixed;

		[Key("specialTrainingPower3BonusFixed")]
		public int specialTrainingPower3BonusFixed;

		[Key("attr")]
		public string attr;

		[Key("supportUnit")]
		public string supportUnit;

		[Key("skillId")]
		public int skillId;

		[Key("cardSkillName")]
		public string cardSkillName;

		[Key("specialTrainingSkillId")]
		public int? specialTrainingSkillId;

		[Key("specialTrainingSkillName")]
		public string specialTrainingSkillName;

		[Key("prefix")]
		public string prefix;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("gachaPhrase")]
		public string gachaPhrase;

		[Key("flavorText")]
		public string flavorText;

		[Key("releaseAt")]
		public long releaseAt;

		[Key("cardParameters")]
		public MasterCardParameter[] cardParameters;

		[Key("specialTrainingCosts")]
		public MasterSpecialTrainingCost[] specialTrainingCosts;

		[Key("masterLessonAchieveResources")]
		public MasterMasterLessonAchieveResource[] masterLessonAchieveResources;

		[Key("masterCardExchangeResources")]
		public MasterCardExchangeResource[] masterCardExchangeResources;

		[Key("archivePublishedAt")]
		public long archivePublishedAt;

		[Key("archiveDisplayType")]
		public string archiveDisplayType;

		[Key("cardSupplyId")]
		public int cardSupplyId;

		[Key("specialTrainingRewardResourceBoxId")]
		public int specialTrainingRewardResourceBoxId;

		[Key("initialSpecialTrainingStatus")]
		public string initialSpecialTrainingStatus;

		[IgnoreMember]
		private Dictionary<CardParameterType, MasterCardParameter[]> masterCardParametersMap;

		[IgnoreMember]
		public ArchiveDisplayType ArchiveDisplayType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsOnlyAfterTraining
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public UnitType SupportUnitType
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

		[IgnoreMember]
		public bool HasSpecialTrainingSkill
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public CardRarityType RarityType
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

		[IgnoreMember]
		public CardRarityFilterType RarityFilterType
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

		[IgnoreMember]
		public CardAttributeType AttributeType
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

		[IgnoreMember]
		public IReadOnlyDictionary<CardParameterType, MasterCardParameter[]> MasterCardParametersMap
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public DateTime ReleaseAtTime
		{
			get
			{
				throw null;
			}
		}

		void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
		{
			throw null;
		}

		void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
		{
			throw null;
		}

		public MasterCard()
		{
		}
	}
}
