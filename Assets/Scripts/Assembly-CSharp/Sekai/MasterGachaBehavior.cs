using System.Runtime.CompilerServices;
using MessagePack;
using Sekai.Constants;

namespace Sekai
{
	[MessagePackObject(false)]
	public sealed class MasterGachaBehavior : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("gachaId")]
		public int gachaId;

		[Key("groupId")]
		public int groupId;

		[Key("priority")]
		public int priority;

		[Key("gachaBehaviorType")]
		public string gachaBehaviorType;

		[Key("costResourceType")]
		public string costResourceType;

		[Key("costResourceId")]
		public int costResourceId;

		[Key("costResourceQuantity")]
		public int? costResourceQuantity;

		[Key("spinCount")]
		public int spinCount;

		[Key("executeLimit")]
		public int executeLimit;

		[Key("gachaExtraId")]
		public int gachaExtraId;

		[Key("resourceCategory")]
		public string resourceCategory;

		[Key("gachaSpinnableType")]
		public string gachaSpinnableType;

		[IgnoreMember]
		public GachaBehaviourType GachaBehaviourType
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
		public GachaSpinnableType GachaSpinnableType
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
		public ResourceCategory ResourceCategory
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
		public ResourceType CostResourceType
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

		public bool IsAvailable()
		{
			throw null;
		}

		public bool IsExecutable()
		{
			throw null;
		}

		public MasterGachaBehavior()
		{
		}
	}
}
