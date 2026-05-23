namespace Sekai.Live
{
	public struct MusicScoreInfo
	{
		public int bar;

		public float barProgress;

		public float time;

		public float bpm;

		public float timeSignature;

		public float speedRatio;

		public float seVolume;

		public MusicScoreInfo(int bar, float barProgress, float time, float bpm, float timeSignature, float speedRatio, float seVolume)
		{
			this.bar = bar;
			this.barProgress = barProgress;
			this.time = time;
			this.bpm = bpm;
			this.timeSignature = timeSignature;
			this.speedRatio = speedRatio;
			this.seVolume = seVolume;
		}
	}
}
