using System;

namespace Sekai
{
	public static class DialogUtility
	{
		public static SubWindowDialog ShowCommonSubWindowDialog(string messageBody, Action onClose = null)
		{
			return ScreenManager.Instance?.ShowSubWindowDialog<SubWindowDialog>(
				messageBody,
				onClose,
				true,
				DialogType.SubWindowDialog,
				DisplayLayerType.Layer_Dialog);
		}
	}
}
