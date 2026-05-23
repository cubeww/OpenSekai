using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterCardParameter : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("cardId")]
		public int cardId;

		[Key("cardLevel")]
		public int cardLevel;

		[Key("cardParameterType")]
		public string cardParameterType;

		[Key("power")]
		public int power;

		[IgnoreMember]
		public CardParameterType CardParameterType
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

		public MasterCardParameter()
		{
		}
	}
}
