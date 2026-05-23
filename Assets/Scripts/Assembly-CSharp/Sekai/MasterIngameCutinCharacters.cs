using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterIngameCutinCharacters : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("priority")]
		public int priority;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("gameCharacterUnitId1")]
		public int gameCharacterUnitId1;

		[Key("gameCharacterUnitId2")]
		public int gameCharacterUnitId2;

		[Key("assetbundleName1")]
		public string assetbundleName1;

		[Key("assetbundleName2")]
		public string assetbundleName2;

		[Key("ingameCutinCharacterType")]
		public string ingameCutinCharacterType;

		[Key("releaseConditionId")]
		public int releaseConditionId;

		[Key("isLotteryTarget")]
		public bool isLotteryTarget;

		[Key("firstCharacterArchiveVoiceId")]
		public int firstCharacterArchiveVoiceId;

		[Key("secondCharacterArchiveVoiceId")]
		public int secondCharacterArchiveVoiceId;

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public MasterIngameCutinCharacters()
		{
		}
	}
}
