using System;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	[Serializable]
	public class ToggleParentElementInfo : IUIElementInfo
	{
		public UIElementType ElementType
		{
			get { return UIElementType.ToggleParent; }
		}

		public string DisplayName { get; private set; }

		public string ToggleGroupId { get; private set; }

		public ToggleParentElementInfo(string displayName, string toggleGroupId)
		{
			DisplayName = displayName;
			ToggleGroupId = toggleGroupId;
		}
	}
}
