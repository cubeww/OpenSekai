using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserCharacter
	{
		[Key("userId")]
		public long userId;

		[Key("characterId")]
		public int characterId;

		[Key("characterRank")]
		public int characterRank;

		[Key("exp")]
		public int exp;

		[Key("totalExp")]
		public int totalExp;

		public UserCharacter()
		{
		}
	}
}
