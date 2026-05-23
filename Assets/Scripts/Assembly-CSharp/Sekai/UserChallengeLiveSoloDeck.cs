using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserChallengeLiveSoloDeck
	{
		[Key("characterId")]
		public int characterId;

		[Key("leader")]
		public int? leader;

		[Key("support1")]
		public int? support1;

		[Key("support2")]
		public int? support2;

		[Key("support3")]
		public int? support3;

		[Key("support4")]
		public int? support4;

		public UserChallengeLiveSoloDeck()
		{
		}

		public UserChallengeLiveSoloDeck(int characterId)
		{
			throw null;
		}
	}
}
