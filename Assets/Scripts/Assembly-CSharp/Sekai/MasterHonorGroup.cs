using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterHonorGroup
	{
		public const string TYPE_CHARACTER = "character";

		public const string TYPE_ACHIEVEMENT = "achievement";

		public const string TYPE_EVENT = "event";

		public const string TYPE_BONDS = "bonds";

		public const string TYPE_RANK_LIVE = "rank_match";

		public const string TYPE_BIRTHDAY = "birthday";

		[Key("id")]
		public int id;

		[Key("honorType")]
		public string honorType;

		[Key("name")]
		public string name;

		[Key("backgroundAssetbundleName")]
		public string backgroundAssetbundleName;

		[Key("frameName")]
		public string frameName;

		[Key("pronunciation")]
		public string pronunciation;

		public MasterHonorGroup()
		{
			throw null;
		}
	}
}
