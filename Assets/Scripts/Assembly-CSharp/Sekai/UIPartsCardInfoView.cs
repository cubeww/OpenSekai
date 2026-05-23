using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsCardInfoView : MonoBehaviour
	{
		[SerializeField]
		private CardFrameView cardFrameView;

		[SerializeField]
		private RectTransform rarityParent;

		[SerializeField]
		private RectTransform costumeParent;

		[SerializeField]
		private DeckCostumeCell deckCostumeCell;

		[SerializeField]
		private CustomImage[] rarityImages;

		[SerializeField]
		private UIPartsCardAttributeView attributeView;

		[SerializeField]
		private UIPartsMasterLevel masterLvView;

		[SerializeField]
		private RectTransform paramTextParent;

		[SerializeField]
		protected CustomTextMesh paramText;

		public RectTransform RarityParent
		{
			get
			{
				throw null;
			}
		}

		public UIPartsMasterLevel MasterLvView
		{
			get
			{
				throw null;
			}
		}

		public void SetActiveFrame(bool active)
		{
			throw null;
		}

		public void SetActiveMasterLv(bool active)
		{
			throw null;
		}

		public void SetActiveAttribute(bool active)
		{
			throw null;
		}

		public void SetActiveRarity(bool active)
		{
			throw null;
		}

		public void SetActiveCostume(bool active)
		{
			throw null;
		}

		public void SetActiveParamText(bool active)
		{
			throw null;
		}

		public void Refresh(int cardId, bool isSpecialTraining, UIPartsMasterLevel.Size masterLvViewSize, int masterLvValue, int cardLv)
		{
			throw null;
		}

		public void Refresh(MasterCard masterCardInfo, bool isSpecialTraining)
		{
			throw null;
		}

		public void Refresh(MasterCard masterCardInfo, bool isSpecialTraining, UIPartsMasterLevel.Size masterLvViewSize, int masterLvValue)
		{
			throw null;
		}

		public void Refresh(MasterCard masterCardInfo, bool isSpecialTraining, UIPartsMasterLevel.Size masterLvViewSize, int masterLvValue, string paramText)
		{
			throw null;
		}

		public void RefreshRarity(CardRarityType rarity, bool isSpecialTrainig)
		{
			throw null;
		}

		public void RefreshMasterLv(UIPartsMasterLevel.Size size, int lv)
		{
			throw null;
		}

		public void RefreshParamText(string value)
		{
			throw null;
		}

		public void SetThumbnails(DeckCostumeCell.CellData cellData)
		{
			throw null;
		}

		private static string GetRaritySuffixName(CardRarityType rarityType, bool isSpecialTraining)
		{
			throw null;
		}

		public UIPartsCardInfoView()
		{
			throw null;
		}
	}
}
