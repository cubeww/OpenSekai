using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterSkill
	{
		[Key("id")]
		public int id;

		[Key("skillFilterId")]
		public int skillFilterId;

		[Key("shortDescription")]
		public string shortDescription;

		[Key("description")]
		public string description;

		[Key("descriptionSpriteName")]
		public string descriptionSpriteName;

		[Key("skillEffects")]
		public MasterSkillEffect[] skillEffects;

		[IgnoreMember]
		public SkillSpriteType SkillSpriteType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public SkillFilterType SkillFilterType
		{
			get
			{
				throw null;
			}
		}

		public MasterSkill()
		{
		}
	}
}
