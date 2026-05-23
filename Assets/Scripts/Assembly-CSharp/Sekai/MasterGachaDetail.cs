using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGachaDetail : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("gachaId")]
		public int gachaId;

		[Key("cardId")]
		public int cardId;

		[Key("weight")]
		public int weight;

		[Key("fixedBonusWeight")]
		public int fixedBonusWeight;

		[Key("isWish")]
		public bool isWish;

		[Key("gachaDetailWishType")]
		public string gachaDetailWishType;

		[IgnoreMember]
		public CardRarityType RarityType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public GachaDetailWishType WishType
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

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public MasterGachaDetail()
		{
		}
	}
}
