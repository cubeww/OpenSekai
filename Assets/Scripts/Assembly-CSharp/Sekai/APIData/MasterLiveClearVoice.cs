using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterLiveClearVoice
	{
		[Key("id")]
		public int id;

		[Key("gameCharacterUnitId")]
		public int gameCharacterUnitId;

		[Key("isNextGrade")]
		public bool isNextGrade;

		[Key("liveClearVoiceType")]
		public string liveClearVoiceType;

		[Key("voiceFileName")]
		public string voiceFileName;

		public MasterLiveClearVoice()
		{
		}
	}
}
