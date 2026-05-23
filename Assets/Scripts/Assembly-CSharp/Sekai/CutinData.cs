using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.Multiplay;

namespace Sekai
{
	public class CutinData
	{
		public enum CutinType
		{
			Combo = 0,
			Outstanding = 1
		}

		public int Id
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public string UserId
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public MasterInGameCutInCharactersModel Master
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int CharacterId1
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int CharacterId2
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public CutinCardData MainCard
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public CutinCardData SubCard
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public CutinType Type
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		private CutinData()
		{
		}

		public List<string> GetAssetBundleNames()
		{
			throw null;
		}

		public static CutinData CreateComboCutinData(IngameComboCutin comboCutin)
		{
			throw null;
		}

		public static CutinData CreateOutstandingCutinData(int index, MultiIngameCutin multiCutin, MultiLivePartyMember[] multiMembers)
		{
			throw null;
		}

		private static CutinCardData CreateCardData(MultiLivePartyMember member, int cardId, string soundAssetName)
		{
			throw null;
		}
	}
}
