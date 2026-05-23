using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterSkillEffect
	{
		[Key("id")]
		public int id;

		[Key("skillEffectType")]
		public string skillEffectType;

		[Key("shortDescription")]
		public string shortDescription;

		[Key("description")]
		public string description;

		[Key("activateNotesJudgmentType")]
		public string activateNotesJudgmentType;

		[Key("activateLife")]
		public int activateLife;

		[Key("conditionType")]
		public string conditionType;

		[Key("skillEffectDetails")]
		public MasterSkillEffectDetail[] skillEffectDetails;

		[Key("skillEnhance")]
		public MasterSkillEnhance skillEnhance;

		[Key("activateCharacterRank")]
		public int activateCharacterRank;

		[Key("activateUnitCount")]
		public int activateUnitCount;

		[IgnoreMember]
		public SkillEffectType SkillEffectType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public ActivateNotesJudgmentType ActivateNotesJudgmentType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public ConditionType ConditionType
		{
			get
			{
				throw null;
			}
		}

		public MasterSkillEffect()
		{
		}
	}
}
