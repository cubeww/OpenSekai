using System.Collections.Generic;

namespace Sekai.Rendering
{
	public static class SekaiMusicItemSettings
	{
		private static readonly List<ISekaiMusicItem> currentTransparentMusicItems = new List<ISekaiMusicItem>();

		public static void ClearTransparentMusicItem()
		{
			currentTransparentMusicItems.Clear();
		}

		public static void RegisterTransparentMusicItem(ISekaiMusicItem musicItem)
		{
			if (musicItem != null && !currentTransparentMusicItems.Contains(musicItem))
			{
				currentTransparentMusicItems.Add(musicItem);
			}
		}

		public static void UnregisterTransparentMusicItem(ISekaiMusicItem musicItem)
		{
			if (musicItem != null)
			{
				currentTransparentMusicItems.Remove(musicItem);
			}
		}

		public static bool ExistTransparentMusicItem()
		{
			return currentTransparentMusicItems.Count > 0;
		}
	}
}
