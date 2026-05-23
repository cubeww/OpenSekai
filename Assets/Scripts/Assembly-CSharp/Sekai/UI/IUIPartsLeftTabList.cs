using System;

namespace Sekai.UI
{
	public interface IUIPartsLeftTabList
	{
		Action<UIPartsLeftTabListCellBase.ViewData> OnSelectCell { get; set; }

		void Initialize(UIPartsLeftTabListCellBase.ViewData[] datas, UIPartsLeftTabListCellBase.ViewData selectedViewData);

		void Refresh(bool resetContentPosition);
	}
}
