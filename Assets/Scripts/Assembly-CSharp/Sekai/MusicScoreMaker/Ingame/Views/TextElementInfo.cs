using System;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	[Serializable]
	public class TextElementInfo : IUIElementInfo
	{
		public UIElementType ElementType
		{
			get { return UIElementType.Text; }
		}

		public string DisplayName { get; private set; }

		public TextElementInfo(string displayName)
		{
			DisplayName = displayName;
		}
	}
}
