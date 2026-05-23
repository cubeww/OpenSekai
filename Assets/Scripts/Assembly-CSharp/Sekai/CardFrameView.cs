using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class CardFrameView : MonoBehaviour
	{
		public enum SizeType
		{
			Undefined = -1,
			S = 0,
			M = 1,
			L = 2,
			MSlim = 3
		}

		[SerializeField]
		private SizeType frameSizeType;

		[SerializeField]
		private CustomImage frameImage;

		[SerializeField]
		private CustomImage frameMaskImage;

		public bool IsLargeSize
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

		public void Refresh(MasterCard card)
		{
			throw null;
		}

		public void RefreshFrame(CardRarityType rarity)
		{
			throw null;
		}

		private static string GetSizeSuffixName(SizeType size)
		{
			throw null;
		}

		private static string GetFrameSuffixName(CardRarityType rarityType)
		{
			throw null;
		}

		public CardFrameView()
		{
			throw null;
		}
	}
}
