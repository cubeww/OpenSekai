using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterCharacterArchiveVoice
	{
		[Key("id")]
		public int id;

		[Key("groupId")]
		public int groupId;

		[Key("gameCharacterId")]
		public int gameCharacterId;

		[Key("unit")]
		public string unit;

		[Key("characterArchiveVoiceType")]
		public string characterArchiveVoiceType;

		[Key("displayPhrase")]
		public string displayPhrase;

		[Key("displayPhrase2")]
		public string displayPhrase2;

		[Key("characterArchiveVoiceTagId")]
		public int characterArchiveVoiceTagId;

		[Key("externalId")]
		public int externalId;

		[Key("assetName")]
		public string assetName;

		[Key("isNextGrade")]
		public bool isNextGrade;

		[Key("displayStartAt")]
		public long displayStartAt;

		public MasterCharacterArchiveVoice()
		{
		}
	}
}
