using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Sekai.ApiData;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class GachaBonusItemReceivableInnerGauge : MonoBehaviour
	{
		private readonly Vector2 _enable_gauge_position;

		private readonly Vector2 _disable_gauge_position;

		[SerializeField]
		private RectTransform _rectTransform;

		[SerializeField]
		private CustomImage _activeFrame;

		[SerializeField]
		private CustomImage _disableCover;

		[SerializeField]
		private CustomImage _bodyImage;

		[SerializeField]
		private CustomImage _maskImage;

		[SerializeField]
		private CustomTextMesh _valueText;

		[SerializeField]
		private RectTransform _borderParent;

		[SerializeField]
		private GachaBonusGaugeBorder _borderPrefab;

		private List<GachaBonusGaugeBorder> _borders;

		public float ValueMin
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

		public float ValueMax
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

		private float _valueRange
		{
			get
			{
				throw null;
			}
		}

		public void Setup(float value, float valueMin, float valueMax, MasterGachaBonusItemReceivableRewardGroup[] targetRewardGroups, bool enable)
		{
			throw null;
		}

		public void SetEnable(bool enable)
		{
			throw null;
		}

		public void UpdateGauge(float value, bool updateText = true)
		{
			throw null;
		}

		public void Empty()
		{
			throw null;
		}

		public void SetAnchorPosition(bool enable)
		{
			throw null;
		}

		public Tween GetGaugePositionTween(bool enable, float duration)
		{
			throw null;
		}

		private void SetupBorders(MasterGachaBonusItemReceivableRewardGroup[] targetRewardGroups)
		{
			throw null;
		}

		private GachaBonusGaugeBorder CreateBorder(MasterGachaBonusItemReceivableRewardGroup rewardGroup, float itemPositionX)
		{
			throw null;
		}

		public GachaBonusItemReceivableInnerGauge()
		{
		}
	}
}
