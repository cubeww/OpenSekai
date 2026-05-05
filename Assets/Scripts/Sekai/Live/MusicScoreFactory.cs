using Sekai.SUS;

namespace Sekai.Live
{
	public static class MusicScoreFactory
	{
		public static MusicScore Create(string susData, LiveBundleBuildData bundleBuildData)
		{
			if (string.IsNullOrEmpty(susData))
			{
				return new MusicScore();
			}

			Converter converter = new Converter
			{
				LiveBundleBuildData = bundleBuildData,
				LongNoteComboBeat = bundleBuildData != null ? bundleBuildData.LongNoteComboBeat : 0
			};
			return converter.Convert(susData);
		}
	}
}
