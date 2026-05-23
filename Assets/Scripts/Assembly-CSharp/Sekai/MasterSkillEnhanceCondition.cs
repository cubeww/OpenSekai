using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterSkillEnhanceCondition
	{
		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("unit")]
		public string unit;

		[IgnoreMember]
		public UnitType UnitType
		{
			get
			{
				throw null;
			}
		}

		public MasterSkillEnhanceCondition()
		{
		}
	}
}
