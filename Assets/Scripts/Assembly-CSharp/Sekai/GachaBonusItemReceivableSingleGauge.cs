using System.Collections.Generic;
using DG.Tweening;
using Sekai.ApiData;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class GachaBonusItemReceivableSingleGauge : MonoBehaviour
	{
		[SerializeField]
		private CustomImage _bodyImage;

		[SerializeField]
		private CustomImage _maskImage;

		[SerializeField]
		private CustomTextMesh _valueText;

		[SerializeField]
		private float _gaugeWidth;

		[SerializeField]
		private RectTransform _borderRoot;

		[SerializeField]
		private RectTransform _bonusItemRoot;

		[SerializeField]
		private GachaBonusGaugeBorder _borderPrefab;

		[SerializeField]
		private GachaBonusItemReceivableGaugeItem _itemPrefab;

		[SerializeField]
		private CustomButton _infoButton;

		private int _gachaId;

		private float _pointMax;

		private UserGachaBonusPoint _current;

		private Tweener _animationTweener;

		private List<GachaBonusGaugeBorder> _borders;

		private List<GachaBonusItemReceivableGaugeItem> _bonusItems;

		private MasterGachaBonusItemReceivableRewardGroup[] _masters;

		public void Setup(int gachaId)
		{
			throw null;
		}

		public void SetupAnimation(int gachaId, int spinCount, float delay, bool isOnce)
		{
			throw null;
		}

		public void StopAnimation()
		{
			throw null;
		}

		private void SetupBorderAndBonusItem(MasterGachaBonusItemReceivableRewardGroup[] targetRewardGroups)
		{
			throw null;
		}

		private GachaBonusGaugeBorder CreateBorder(MasterGachaBonusItemReceivableRewardGroup rewardGroup, float itemPositionX)
		{
			throw null;
		}

		private GachaBonusItemReceivableGaugeItem CreateBonusItem(MasterGachaBonusItemReceivableRewardGroup rewardGroup, float itemPositionX)
		{
			throw null;
		}

		private void UpdateGauge(float value, bool updateText = true)
		{
			throw null;
		}

		private void Empty()
		{
			throw null;
		}

		private void OnClickGaugeInfo()
		{
			throw null;
		}

		public GachaBonusItemReceivableSingleGauge()
		{
		}
	}
}
