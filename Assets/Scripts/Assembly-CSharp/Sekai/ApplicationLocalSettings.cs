namespace Sekai
{
	public class ApplicationLocalSettings
	{
		public class VolumeSettings
		{
			public float Bgm = 1f;
			public float Se = 1f;
			public float Voice = 1f;
		}

		public VolumeSettings SystemVolume = new VolumeSettings();
		public VolumeSettings LiveVolume;

		public static ApplicationLocalSettings LoadFromStorage()
		{
			// TODO(original): deserialize persisted local settings.
			return new ApplicationLocalSettings();
		}

		public VolumeSettings SetupLiveVolume()
		{
			LiveVolume = new VolumeSettings();
			return LiveVolume;
		}
	}
}
