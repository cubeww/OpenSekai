using System;
using CP;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class DeckCostumeCell : MonoBehaviour
	{
		public class CellData
		{
			public MasterGameCharacter MasterGameCharacter;

			public MasterCard MasterCard;

			public Action<MasterGameCharacter, CostumePartType> OnClick;

			public MVOnlyDeckCostumeLocalData mvOnlyDeckCostumeLocalData;

			public void InvokeOnClick(CostumePartType costumePartType)
			{
				throw null;
			}

			public bool IsCostumeLock()
			{
				throw null;
			}

			public bool IsCostumeAndCharacterValid()
			{
				throw null;
			}

			public bool ExistsCharacter()
			{
				throw null;
			}

			public bool ExistsMvOnlyDeckCostumeLocalData()
			{
				throw null;
			}

			public CellData()
			{
				throw null;
			}
		}

		[Serializable]
		public class CostumeCell
		{
			public CostumePartType Type;

			public UIPartsItemThumbnail Thumbnail;

			public CustomButton Button;

			public ClickDetector ClickDetector;

			public GameObject LockObject;

			public CostumeCell()
			{
				throw null;
			}
		}

		[SerializeField]
		private CostumeCell[] costumeCells;

		private CellData cellData;

		private bool isCostumeLock;

		public void Setup(CellData cellData)
		{
			throw null;
		}

		private UserCharacterCostume3D GetUserCharacterLiveCostume()
		{
			throw null;
		}

		private void SetupThumbnail(UserCharacterCostume3D userCharacterLiveCostume)
		{
			throw null;
		}

		private void SetupThumbnail(UIPartsItemThumbnail uiPartsItemThumbnail, int costume3dId, UnitType unitType)
		{
			throw null;
		}

		private void SetupLockObject()
		{
			throw null;
		}

		private void SetupCostumeButton()
		{
			throw null;
		}

		public DeckCostumeCell()
		{
			throw null;
		}
	}
}
