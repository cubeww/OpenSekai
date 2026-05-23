using System;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	[Serializable]
	public class CheckBoxElementInfo : IUIElementInfo
	{
		public UIElementType ElementType
		{
			get { return UIElementType.CheckBox; }
		}

		public string DisplayName { get; private set; }

		public Func<bool> GetBoolValue { get; private set; }

		public Action<bool> SetBoolValue { get; private set; }

		public CheckBoxElementInfo(string displayName, Func<bool> getBoolValue, Action<bool> setBoolValue)
		{
			DisplayName = displayName;
			GetBoolValue = getBoolValue;
			SetBoolValue = setBoolValue;
		}
	}
}
