using System;

namespace Sekai
{
	public class ScreenLayerBackground : ScreenLayer
	{
		public enum BackgroundType
		{
			Unknown = 0,
			Default = 1
		}

		public BackgroundType CurrentBackgroundType { get; private set; } = BackgroundType.Unknown;

		public void Show(BackgroundType type, Action<CrossFader.FinishedStatus> onFinished = null)
		{
			// TODO(original): restore background bundle load and crossfade.
			CurrentBackgroundType = type;
			onFinished?.Invoke(CrossFader.FinishedStatus.Complete);
		}

		public void Hide(Action<CrossFader.FinishedStatus> onFinished = null)
		{
			onFinished?.Invoke(CrossFader.FinishedStatus.Complete);
		}
	}
}
