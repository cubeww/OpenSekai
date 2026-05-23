using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class GachaBonusGaugeBorder : MonoBehaviour
	{
		private static readonly int[] BonusPointShowThresholds;

		[SerializeField]
		private RectTransform _rectTransform;

		[SerializeField]
		private CustomImage _bonusPointImage;

		[SerializeField]
		private GameObject _borderLine;

		public RectTransform RectTransform
		{
			get
			{
				return _rectTransform;
			}
		}

		public void Setup(int gachaBonusPoint, bool isShowBorderLine)
		{
			bool showsBonusPoint = System.Array.IndexOf(BonusPointShowThresholds, gachaBonusPoint) >= 0;
			_bonusPointImage.SetActive(showsBonusPoint);
			if (showsBonusPoint)
			{
				_bonusPointImage.SpriteName = string.Format("gauge_text_{0}_gachaBonus", gachaBonusPoint);
				_bonusPointImage.SetNativeSize();
			}

			_borderLine.SetActive(isShowBorderLine);
		}

		public GachaBonusGaugeBorder()
		{
		}

		static GachaBonusGaugeBorder()
		{
			BonusPointShowThresholds = new[] { 50, 100, 150, 200 };
		}
	}
}
