using System;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	[Serializable]
	public class SliderElementInfo : IUIElementInfo
	{
		public UIElementType ElementType
		{
			get { return UIElementType.Slider; }
		}

		public string DisplayName { get; private set; }

		public SliderValueType ValueType { get; private set; }

		public float MinValue { get; private set; }

		public float MaxValue { get; private set; }

		public string Format { get; private set; }

		public Func<float> GetFloatValue { get; private set; }

		public Action<float> SetFloatValue { get; private set; }

		public SliderElementInfo(string displayName, float minValue, float maxValue, SliderValueType valueType, string format, Func<float> getFloatValue, Action<float> setFloatValue)
		{
			DisplayName = displayName;
			MinValue = minValue;
			MaxValue = maxValue;
			ValueType = valueType;
			Format = format;
			GetFloatValue = getFloatValue;
			SetFloatValue = setFloatValue;
		}
	}
}
