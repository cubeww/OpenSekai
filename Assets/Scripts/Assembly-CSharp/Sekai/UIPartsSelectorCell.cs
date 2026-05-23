using System;
using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsSelectorCell : UIPartsSelectorCellBase<UIPartsSelectorCell.ViewData>
	{
		public class ViewData : ViewDataBase
		{
			public ViewData(int index)
				: base(index)
			{
			}
		}

		private static readonly Lazy<Dictionary<bool, Color>> ActiveTextColorTable;

		protected override Color GetTextColor(bool isSelected)
		{
			return ActiveTextColorTable.Value[isSelected];
		}

		protected override Color GetIconColor(bool isSelected)
		{
			return ActiveTextColorTable.Value[isSelected];
		}

		public UIPartsSelectorCell()
		{
		}

		static UIPartsSelectorCell()
		{
			ActiveTextColorTable = new Lazy<Dictionary<bool, Color>>(() => new Dictionary<bool, Color>
			{
				{ true, Color.white },
				{ false, new Color(0.2666667f, 0.2666667f, 0.4f, 1f) }
			});
		}
	}
}
