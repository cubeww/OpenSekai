using System.Collections.Generic;
using Sekai.ApiData;

namespace Sekai
{
	public sealed class UserLiveCharacterArchiveVoiceModel
	{
		private readonly UserLiveCharacterArchiveVoice _data;

		private readonly IReadOnlyDictionary<int, List<int>> _inGameCutInVoiceGroupIds;

		public UserLiveCharacterArchiveVoiceModel()
		{
		}

		public bool IsAlreadyReadPairCutIn(MasterInGameCutInCharactersModel inGameCutIn)
		{
			throw null;
		}

		public bool IsAlreadyRead(MasterCharacterArchiveVoiceModel characterArchiveVoice)
		{
			throw null;
		}

		public bool IsAlreadyRead(int archiveVoiceGroupId)
		{
			throw null;
		}
	}
}
