using MessagePack;
using Sekai.Constants;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGachaCeilExchangeSubstituteCost
	{
		[Key("id")]
		public int id;

		[Key("resourceType")]
		public string resourceType;

		[Key("resourceId")]
		public int resourceId;

		[Key("substituteQuantity")]
		public int substituteQuantity;

		[IgnoreMember]
		public ResourceType ResourceType
		{
			get
			{
				throw null;
			}
		}

		public MasterGachaCeilExchangeSubstituteCost()
		{
		}
	}
}
