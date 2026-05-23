using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Sekai.Live
{
	public class EventBase
	{
		public MusicScoreInfo MusicScoreInfo
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			protected set;
		}

		public EventState State
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			protected set;
		}

		public float OffsetJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			protected set;
		}

		public EventBase([JsonProperty("MusicScoreInfo")] MusicScoreInfo info)
		{
			MusicScoreInfo = info;
		}

		public void ResetEvent()
		{
			State = EventState.Wait;
			OffsetJudgeTime = 0f;
		}

		public virtual void Update(MusicScoreInfo currentFrameInfo, Action<EventBase> resultCallback)
		{
			if (State == EventState.Done)
			{
				return;
			}

			float eventTime = MusicScoreInfo.time;
			if (eventTime <= currentFrameInfo.time)
			{
				State = EventState.Done;
				OffsetJudgeTime = currentFrameInfo.time - eventTime;
				resultCallback?.Invoke(this);
			}
		}
	}
}
