using System;
using MessagePack;
using Sekai.Constants;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterMaterialExchangeCost : IEquatable<MasterMaterialExchangeCost>
	{
		[Key("materialExchangeId")]
		public int materialExchangeId;

		[Key("costGroupId")]
		public int costGroupId;

		[Key("seq")]
		public int seq;

		[Key("resourceType")]
		public string resourceType;

		[Key("resourceId")]
		public int resourceId;

		[Key("quantity")]
		public int quantity;

		[IgnoreMember]
		public ResourceType ResourceType
		{
			get
			{
				throw null;
			}
		}

		public bool Equals(MasterMaterialExchangeCost other)
		{
			throw null;
		}

		public MasterMaterialExchangeCost()
		{
		}
	}
}
