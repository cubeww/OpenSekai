namespace Sekai
{
	public class LoadingLiveBackgroundView : LoadingBackgroundViewBase
	{
		protected override string BackgroundImagePath
		{
			get
			{
				return "LiveResult/Images/bg_liveResult_test";
			}
		}

		protected override void Load()
		{
			// Original tries to load the last live result background from AssetManager.
			// OpenSekai can enter MusicScoreMaker directly without live boot data, so keep the default Resources path.
			base.Load();
		}

		public LoadingLiveBackgroundView()
		{
		}
	}
}
