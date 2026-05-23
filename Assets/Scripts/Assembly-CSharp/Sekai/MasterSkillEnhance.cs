using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterSkillEnhance : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("skillEnhanceType")]
		public string skillEnhanceType;

		[Key("activateEffectValueType")]
		public string activateEffectValueType;

		[Key("activateEffectValue")]
		public int activateEffectValue;

		[Key("skillEnhanceCondition")]
		public MasterSkillEnhanceCondition skillEnhanceCondition;

		[IgnoreMember]
		public ActivateEffectValueType ActivateEffectValueType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public SkillEnhanceType SkillEnhanceType
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

		void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
		{
			throw null;
		}

		void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
		{
			throw null;
		}

		public MasterSkillEnhance()
		{
		}
	}
}
