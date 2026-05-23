using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterCharacterArchiveVoiceTag
	{
		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("characterArchiveVoiceTagType")]
		public string characterArchiveVoiceTagType;

		[Key("name")]
		public string name;

		public MasterCharacterArchiveVoiceTag()
		{
		}
	}
}
