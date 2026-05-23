using System.Runtime.CompilerServices;

namespace Sekai
{
	public class MasterCostume3DDataModel
	{
		public enum CostumeType
		{
			NORMAL = 0,
			DEFAULT = 1,
			DISTRIBUTION = 2
		}

		public enum Rarity
		{
			normal = 0,
			rare = 1
		}

		private const int NONE_ACCESSORY_GROUP_MAX = 100;

		private const int DEFAULT_GROUPID_CONVERT_NUM = 100;

		private const int DEFAULT_GROUP_ID_MAX = 1000;

		private MasterCostume3D _masterCostume3D;

		public int Id
		{
			get
			{
				throw null;
			}
		}

		public int Seq
		{
			get
			{
				throw null;
			}
		}

		public int Costume3dGroupId
		{
			get
			{
				throw null;
			}
		}

		public CostumeType Costume3dType
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public string Name
		{
			get
			{
				throw null;
			}
		}

		public CostumePartType CostumePartType
		{
			get
			{
				throw null;
			}
		}

		public int ColorId
		{
			get
			{
				throw null;
			}
		}

		public string ColorName
		{
			get
			{
				throw null;
			}
		}

		public int CharacterId
		{
			get
			{
				throw null;
			}
		}

		public Rarity Costume3dRarity
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public string HowToObtain
		{
			get
			{
				throw null;
			}
		}

		public string AssetBundleName
		{
			get
			{
				throw null;
			}
		}

		public string Designer
		{
			get
			{
				throw null;
			}
		}

		public long PublishedAt
		{
			get
			{
				throw null;
			}
		}

		public long ArchivePublishedAt
		{
			get
			{
				throw null;
			}
		}

		public ArchiveDisplayType ArchiveDisplayType
		{
			get
			{
				throw null;
			}
		}

		public bool IsUnset
		{
			get
			{
				throw null;
			}
		}

		public bool ExistsDesigner
		{
			get
			{
				throw null;
			}
		}

		public int GroupIdDigit
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

		public int GroupId
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

		private bool HaveAccessoryDefaultGroup
		{
			get
			{
				throw null;
			}
		}

		public MasterCostume3DDataModel()
		{
		}

		public MasterCostume3DDataModel(int id)
		{
			throw null;
		}

		public MasterCostume3DDataModel(MasterCostume3D masterCostume3D)
		{
			throw null;
		}

		private int ConvertGroupId()
		{
			throw null;
		}

		private int ConvertGroupIdDigit()
		{
			throw null;
		}

		private Rarity ConvertRarityType()
		{
			throw null;
		}

		private CostumeType ConvertCostume3dType()
		{
			throw null;
		}
	}
}
