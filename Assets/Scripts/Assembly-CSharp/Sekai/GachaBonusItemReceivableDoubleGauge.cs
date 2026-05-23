using System;
using System.Collections.Generic;
using DG.Tweening;
using Sekai.ApiData;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class GachaBonusItemReceivableDoubleGauge : MonoBehaviour
	{
		[Serializable]
		public class BonusItemParentView
		{
			[SerializeField]
			private RectTransform _rectTransform;

			[SerializeField]
			private CanvasGroup _canvasGroup;

			public RectTransform RectTransform
			{
				get
				{
					throw null;
				}
			}

			public CanvasGroup CanvasGroup
			{
				get
				{
					throw null;
				}
			}

			public BonusItemParentView()
			{
			}
		}

		private const int INNER_GAUGE_COUNT = 2;

		private const float UPDATE_GAUGE_DURATION = 1f;

		private const float SWITCH_ANIMATION_DURATION = 0.3f;

		[SerializeField]
		private GachaBonusItemReceivableInnerGauge[] _gauges;

		[SerializeField]
		private CanvasGroup[] _itemParentCanvasGroups;

		[SerializeField]
		private GachaBonusItemReceivableGaugeItem _itemPrefab;

		[SerializeField]
		private CustomButton _infoButton;

		[SerializeField]
		private CustomButton _switchButton;

		private int _gachaId;

		private float _pointMax;

		private UserGachaBonusPoint _current;

		private Sequence _animationSequence;

		private float _gaugeWidth;

		private List<GachaBonusItemReceivableGaugeItem> _bonusItems;

		private MasterGachaBonusItemReceivableRewardGroup[] _masters;

		private int _currentGaugeIndex;

		public void Setup(int gachaId, bool isSimplified = false)
		{
			throw null;
		}

		public void SetupAnimation(int gachaId, int spinCount, float delay, bool isOnce)
		{
			throw null;
		}

		public void StopAnimation(bool complete = false)
		{
			throw null;
		}

		private void SetupInnerGauge(bool isSimplified)
		{
			throw null;
		}

		private void SetupBonusItems(int index, MasterGachaBonusItemReceivableRewardGroup[] targetRewardGroups, bool enable, bool isSimplified)
		{
			throw null;
		}

		private GachaBonusItemReceivableGaugeItem CreateBonusItem(MasterGachaBonusItemReceivableRewardGroup rewardGroup, Transform instantiateRoot, float itemPositionX, bool isSimplified)
		{
			throw null;
		}

		private void UpdateGaugeView(float value)
		{
			throw null;
		}

		private void UpdateGaugeValue(float value)
		{
			throw null;
		}

		private void SwitchGauge(int prevIndex, int nextIndex, bool immediate = false, Action onComplete = null)
		{
			throw null;
		}

		private Tween GetUpdateGaugeValueTween(float startValue, float endValue, float duration)
		{
			throw null;
		}

		private Tween GetSwitchFadeBonusItemTween(int prevIndex, int nextIndex, float duration)
		{
			throw null;
		}

		private Tween GetSwitchEnableTween(int prevIndex, int nextIndex, float duration)
		{
			throw null;
		}

		private int GetTargetGaugeIndex(float value)
		{
			throw null;
		}

		private void OnClickGaugeInfo()
		{
			throw null;
		}

		private void OnClickSwitch()
		{
			throw null;
		}

		public GachaBonusItemReceivableDoubleGauge()
		{
		}
	}
}
