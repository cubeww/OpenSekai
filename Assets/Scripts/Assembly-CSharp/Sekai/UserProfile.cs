using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserProfile
	{
		[Key("word")]
		public string word;

		[Key("twitterId")]
		public string twitterId;

		[Key("profileImageType")]
		public string profileImageType;

		[Key("profileImageId")]
		public int profileImageId;

		[IgnoreMember]
		public ProfileImageType ProfileImageType
		{
			get
			{
				throw null;
			}
		}

		public UserProfile()
		{
			throw null;
		}
	}
}
