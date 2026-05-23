using System;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public static class UIElementInfoFactory
	{
		public static SliderElementInfo CreateSlider(string displayName, float minValue, float maxValue, SliderValueType valueType, string format, Func<float> getFloatValue, Action<float> setFloatValue)
		{
			return new SliderElementInfo(displayName, minValue, maxValue, valueType, format, getFloatValue, setFloatValue);
		}

		public static CheckBoxElementInfo CreateCheckBox(string displayName, Func<bool> getBoolValue, Action<bool> setBoolValue)
		{
			return new CheckBoxElementInfo(displayName, getBoolValue, setBoolValue);
		}

		public static ButtonElementInfo CreateButton(string displayName, Action buttonAction)
		{
			return new ButtonElementInfo(displayName, buttonAction);
		}

		public static TextElementInfo CreateText(string displayName)
		{
			return new TextElementInfo(displayName);
		}

		public static ToggleParentElementInfo CreateToggleParent(string displayName, string toggleGroupId)
		{
			return new ToggleParentElementInfo(displayName, toggleGroupId);
		}

		public static ToggleElementInfo CreateToggle(string displayName, string toggleGroupId, int layoutPatternIndex)
		{
			return new ToggleElementInfo(displayName, toggleGroupId, layoutPatternIndex);
		}
	}
}
