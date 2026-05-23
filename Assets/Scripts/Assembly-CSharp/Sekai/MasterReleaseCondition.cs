using System;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterReleaseCondition : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("sentence")]
		public string sentence;

		[Key("releaseConditionType")]
		public string releaseConditionType;

		[Key("releaseConditionTypeId")]
		public int releaseConditionTypeId;

		[Key("releaseConditionTypeId2")]
		public int? releaseConditionTypeId2;

		[Key("releaseConditionTypeLevel")]
		public int releaseConditionTypeLevel;

		[Key("releaseConditionTypeQuantity")]
		public int releaseConditionTypeQuantity;

		[Key("releaseConditionTypeDate")]
		public long releaseConditionTypeDate;

		[IgnoreMember]
		public ReleaseConditionType ReleaseConditionType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public DateTime ReleaseConditionTypeDate
		{
			get
			{
				throw null;
			}
		}

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public MasterReleaseCondition()
		{
			throw null;
		}
	}
}
