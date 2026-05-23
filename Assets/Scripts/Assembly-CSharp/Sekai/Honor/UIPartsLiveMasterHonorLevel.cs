using Sekai.RankLive;
using Sekai.UI;
using UnityEngine;

namespace Sekai.Honor
{
	public sealed class UIPartsLiveMasterHonorLevel : MonoBehaviour
	{
		private const int MAX_STAR_COUNT = 10;

		[SerializeField]
		private CustomRawImage _scrollImage;

		[SerializeField]
		private Sekai.RankLive.NumberView _clearCountNumberView;

		[SerializeField]
		private CustomImage[] _enableStarImages;

		[SerializeField]
		private CustomImage[] _disableStarImages;

		public void SetActive(bool value)
		{
			throw null;
		}

		public void Initialize()
		{
			throw null;
		}

		public void Setup(uint honorLevel, int clearCount, HonorSlot slotType, bool emitLusterEffect = true)
		{
			throw null;
		}

		public void SetScrollTexture(Texture texture)
		{
			throw null;
		}

		public void SetColor(Color color)
		{
			throw null;
		}

		private void SetClearCount(uint honorLevel, int clearCount, HonorSlot slotType)
		{
			throw null;
		}

		private void UpdateStarImages(uint honorLevel, HonorSlot slotType)
		{
			throw null;
		}

		public UIPartsLiveMasterHonorLevel()
		{
			throw null;
		}
	}
}
