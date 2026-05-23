using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.UI
{
	[AddComponentMenu("Custom UI/Custom Slider")]
	public class CustomSlider : Slider
	{
		[SerializeField]
		private bool m_isUseExtension;

		[SerializeField]
		private bool m_isUseSliderSE;

		[SerializeField]
		private bool m_isUsePositionOneValueMax;

		private float currentValue;

		private Action<float> _onDragCallback;

		public bool IsSEPlaying
		{
			[CompilerGenerated]
			get
			{
				return _isSEPlaying;
			}
			[CompilerGenerated]
			set
			{
				_isSEPlaying = value;
			}
		}

		private bool _isSEPlaying;

		protected override void Awake()
		{
			base.Awake();
			currentValue = value;
			if (m_isUseExtension)
			{
				SetOnValueChangeEvent();
			}
		}

		public void SetOnDrag(Action<float> onDrag)
		{
			_onDragCallback = onDrag;
		}

		public void SetSliderPositionOneValueMax()
		{
			if (m_isUsePositionOneValueMax && maxValue > minValue && value >= maxValue)
			{
				SetValueWithoutNotify(maxValue);
			}
		}

		public void SetOnValueChangeEvent()
		{
			onValueChanged.RemoveListener(OnValueChange);
			onValueChanged.AddListener(OnValueChange);
		}

		public void RemoveAllAndAddListener(Action<float> action)
		{
			onValueChanged.RemoveAllListeners();
			if (action != null)
			{
				onValueChanged.AddListener(action.Invoke);
			}
		}

		public void RemoveAllListeners()
		{
			onValueChanged.RemoveAllListeners();
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			currentValue = value;
			base.OnPointerDown(eventData);
			_onDragCallback?.Invoke(value);
		}

		public override void OnDrag(PointerEventData eventData)
		{
			base.OnDrag(eventData);
			_onDragCallback?.Invoke(value);
		}

		private void OnValueChange(float value)
		{
			currentValue = value;
			if (m_isUseSliderSE)
			{
				IsSEPlaying = true;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			currentValue = value;
			if (m_isUsePositionOneValueMax)
			{
				SetSliderPositionOneValueMax();
			}
		}

		public CustomSlider()
		{
		}
	}
}
