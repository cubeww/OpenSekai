using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserDeck
	{
		[Key("userId")]
		public long userId;

		[Key("deckId")]
		public int deckId;

		[Key("name")]
		public string name;

		[Key("leader")]
		public int leader;

		[Key("subLeader")]
		public int subLeader;

		[Key("member1")]
		public int member1;

		[Key("member2")]
		public int member2;

		[Key("member3")]
		public int member3;

		[Key("member4")]
		public int member4;

		[Key("member5")]
		public int member5;

		public UserDeck()
		{
		}

		public UserDeck(long userId, int deckId, string deckName, int[] cardIds)
		{
			throw null;
		}
	}
}
