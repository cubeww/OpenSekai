using Sekai.Mysekai;

namespace Sekai.UI.Interface
{
	public interface ICanvasMemberListFilterSort
	{
		CardListFilterSortSettingData SettingResult { get; }

		CanvasMemberListFilterDialog.FilterCanvasFinishType CanvasFinishType { get; set; }
	}
}
