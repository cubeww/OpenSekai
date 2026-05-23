using System;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	[Serializable]
	public class ButtonElementInfo : IUIElementInfo
	{
		public UIElementType ElementType
		{
			get { return UIElementType.Button; }
		}

		public string DisplayName { get; private set; }

		public Action ButtonAction { get; private set; }

		public ButtonElementInfo(string displayName, Action buttonAction)
		{
			DisplayName = displayName;
			ButtonAction = buttonAction;
		}
	}
}
