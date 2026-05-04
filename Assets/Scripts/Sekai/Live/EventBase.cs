using System;

namespace Sekai.Live
{
	public class EventBase
	{
		public MusicScoreInfo MusicScoreInfo { get; protected set; }

		public EventState State { get; protected set; }

		public float OffsetJudgeTime { get; protected set; }

		public EventBase(MusicScoreInfo info)
		{
			MusicScoreInfo = info;
			State = EventState.Wait;
		}

		public virtual void Update(MusicScoreInfo currentFrameInfo, Action<EventBase> resultCallback)
		{
			if (State == EventState.Done)
			{
				return;
			}
			if (MusicScoreInfo.time <= currentFrameInfo.time)
			{
				State = EventState.Done;
				OffsetJudgeTime = currentFrameInfo.time - MusicScoreInfo.time;
				resultCallback?.Invoke(this);
			}
		}
	}
}
