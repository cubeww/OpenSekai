using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterSkillEffectDetail
	{
		[Key("id")]
		public int id;

		[Key("skillEffectId")]
		public int skillEffectId;

		[Key("level")]
		public int level;

		[Key("activateEffectDuration")]
		public float activateEffectDuration;

		[Key("activateEffectValueType")]
		public string activateEffectValueType;

		[Key("activateEffectValue")]
		public int activateEffectValue;

		[Key("activateEffectValue2")]
		public int activateEffectValue2;

		[IgnoreMember]
		public ActivateEffectValueType ActivateEffectValueType
		{
			get
			{
				throw null;
			}
		}

		public MasterSkillEffectDetail()
		{
		}
	}
}
