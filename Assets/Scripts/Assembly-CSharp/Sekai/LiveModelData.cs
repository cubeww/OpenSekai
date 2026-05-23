using System.Collections.Generic;

namespace Sekai
{
	public class LiveModelData
	{
		public bool OriginalMember { get; set; }

		public LiveCharacterModelData[] Members { get; set; }

		public LiveCharacterModelData[] CutInMembers { get; set; }

		public HashSet<string> AssetBundleNameLists { get; }

		public LiveModelData(LiveDeckData deckData)
			: this(false)
		{
		}

		public LiveModelData(int[] charaIds, UnitType[] unitTypes)
			: this(false)
		{
		}

		public LiveModelData(MultiLiveFormationData[] formations)
			: this(false)
		{
		}

		public LiveModelData(LiveCharacterModelData[] modelDatas)
			: this(false)
		{
			Members = modelDatas ?? System.Array.Empty<LiveCharacterModelData>();
			CutInMembers = Members;
		}

		public LiveModelData(bool isOriginalMember)
		{
			OriginalMember = isOriginalMember;
			Members = System.Array.Empty<LiveCharacterModelData>();
			CutInMembers = Members;
			AssetBundleNameLists = new HashSet<string>();
		}
	}
}
