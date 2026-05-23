using System;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	[Serializable]
	public class ToggleElementInfo : IUIElementInfo
	{
		public UIElementType ElementType
		{
			get { return UIElementType.Toggle; }
		}

		public string DisplayName { get; private set; }

		public string ToggleGroupId { get; private set; }

		public int LayoutPatternIndex { get; private set; }

		public ToggleElementInfo(string displayName, string toggleGroupId, int layoutPatternIndex)
		{
			DisplayName = displayName;
			ToggleGroupId = toggleGroupId;
			LayoutPatternIndex = layoutPatternIndex;
		}
	}
}
