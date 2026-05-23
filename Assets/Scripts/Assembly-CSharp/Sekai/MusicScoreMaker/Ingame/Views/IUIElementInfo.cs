namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public interface IUIElementInfo
	{
		UIElementType ElementType { get; }

		string DisplayName { get; }
	}
}
