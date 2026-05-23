using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGachaCeilExchange : IMessagePackSerializationCallbackReceiver
	{
		public const int GACHA_CEIL_TICKET_MATERIAL_ID = 47;

		public const int GACHA_CEIL_TICKET_LIMITED_MATERIAL_ID = 48;

		[Key("id")]
		public int id;

		[Key("gachaCeilExchangeSummaryId")]
		public int gachaCeilExchangeSummaryId;

		[Key("seq")]
		public int seq;

		[Key("resourceBoxId")]
		public int resourceBoxId;

		[Key("exchangeLimit")]
		public int exchangeLimit;

		[Key("gachaCeilExchangeLabelType")]
		public string gachaCeilExchangeLabelType;

		[Key("gachaCeilExchangeCost")]
		public MasterGachaCeilExchangeCost gachaCeilExchangeCost;

		[Key("substituteLimit")]
		public int substituteLimit;

		[Key("gachaCeilExchangeSubstituteCosts")]
		public MasterGachaCeilExchangeSubstituteCost[] substituteCosts;

		[IgnoreMember]
		public GachaCeilExchangeLabelType LabelType
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

		void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
		{
			throw null;
		}

		void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
		{
			throw null;
		}

		public MasterGachaCeilExchange()
		{
		}
	}
}
