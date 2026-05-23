using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.Core;

namespace Sekai
{
	public class LiveCharacterModelData
	{
		private static readonly string FACE_COMMON_BUNDLE;

		private static readonly string SHADER_BUNDLE;

		public MasterCharacter3D Character3D
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public MasterGameCharacter Character
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public MasterCard Card
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public MasterCostume3DDataModel HairCostume
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public MasterCostume3DModel HairCostumeModel
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public MasterCostume3DDataModel BodyCostume
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public MasterCostume3DModel BodyCostumeModel
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public MasterCostume3DDataModel AccessoryCostume
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public MasterCostume3DModel AccessoryCostumeModel
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public bool IsSameHeight
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public LiveCharacterModelData(int characterId, int cardId = 0)
		{
			throw null;
		}

		public LiveCharacterModelData(int characterId, UnitType unitType)
		{
			throw null;
		}

		private void SetCostumeParts(UserCharacterCostume3D userCharacterLiveCostume)
		{
			throw null;
		}

		public LiveCharacterModelData(int characterId, UnitType unitType, int bodyCostume3DId, int hairCostume3DId, int accessoryCostume3DId, bool isSameHeight = false, int cardId = 0)
		{
			throw null;
		}

		public LiveCharacterModelData(int characterId, int bodyCostume3DId, UnitType bodyModelUnit, int hairCostume3DId, UnitType hairModelUnit, int accessoryCostume3DId, UnitType accessoryModelUnit, bool isSameHeight = false)
		{
			throw null;
		}

		public LiveCharacterModelData(MasterCharacter3D masterCharacter3D, bool isSameHeight = false)
		{
			throw null;
		}

		public LiveCharacterModelData(CharacterInfo characterInfo)
		{
			throw null;
		}

		public LiveCharacterModelData(MasterGameCharacter character, string hairAssetbundleName, string bodyAssetbundleName, string accessoryAssetbundleName, string colorAssetbundleName = null)
		{
			throw null;
		}

		public LiveCharacterModelData DeepCopy()
		{
			throw null;
		}

		public List<string> GetAssetBundleNames()
		{
			throw null;
		}

		static LiveCharacterModelData()
		{
			}
	}
}
