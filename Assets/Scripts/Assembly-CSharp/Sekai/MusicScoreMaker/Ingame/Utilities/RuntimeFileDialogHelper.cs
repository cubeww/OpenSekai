using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Utilities
{
	public static class RuntimeFileDialogHelper
	{
		public static string ShowOpenFileDialog(string title, string filter = "All Files (*.*)|*.*", string initialDirectory = "")
		{
			Debug.LogWarning("現在のプラットフォームではランタイムファイル選択はサポートされていません。");
			return string.Empty;
		}

		public static string ShowSaveFileDialog(string title, string defaultFileName = "All Files (*.*)|*.*", string defaultExt = "")
		{
			Debug.LogWarning("現在のプラットフォームではランタイムファイル保存はサポートされていません。");
			return string.Empty;
		}
	}
}
