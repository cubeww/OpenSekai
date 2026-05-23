using System.Runtime.CompilerServices;

namespace Sekai.Live
{
	public struct JudgeTimeData
	{
		public readonly JudgeFrameType NoteType
		{
			[CompilerGenerated]
			get;
		}

		public float PerfectBeforeJudgeTime
		{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			private set;
		}

		public float PerfectAfterJudgeTime
		{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			private set;
		}

		public float GreatBeforeJudgeTime
		{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			private set;
		}

		public float GreatAfterJudgeTime
		{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			private set;
		}

		public float GoodBeforeJudgeTime
		{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			private set;
		}

		public float GoodAfterJudgeTime
		{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			private set;
		}

		public float BadBeforeJudgeTime
		{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			private set;
		}

		public float BadAfterJudgeTime
		{
			[CompilerGenerated]
			readonly get;
			[CompilerGenerated]
			private set;
		}

		public JudgeTimeData(MasterIngameJudgeFrame masterIngameJudgeFrame)
		{
			if (masterIngameJudgeFrame == null)
			{
				throw new System.ArgumentNullException(nameof(masterIngameJudgeFrame));
			}

			NoteType = masterIngameJudgeFrame.NoteType;
			PerfectBeforeJudgeTime = masterIngameJudgeFrame.PerfectBeforeJudgeTime;
			PerfectAfterJudgeTime = masterIngameJudgeFrame.PerfectAfterJudgeTime;
			GreatBeforeJudgeTime = masterIngameJudgeFrame.GreatBeforeJudgeTime;
			GreatAfterJudgeTime = masterIngameJudgeFrame.GreatAfterJudgeTime;
			GoodBeforeJudgeTime = masterIngameJudgeFrame.GoodBeforeJudgeTime;
			GoodAfterJudgeTime = masterIngameJudgeFrame.GoodAfterJudgeTime;
			BadBeforeJudgeTime = masterIngameJudgeFrame.BadBeforeJudgeTime;
			BadAfterJudgeTime = masterIngameJudgeFrame.BadAfterJudgeTime;
		}

		internal JudgeTimeData(JudgeFrameType noteType, float perfectBeforeJudgeTime, float perfectAfterJudgeTime, float greatBeforeJudgeTime, float greatAfterJudgeTime, float goodBeforeJudgeTime, float goodAfterJudgeTime, float badBeforeJudgeTime, float badAfterJudgeTime)
		{
			NoteType = noteType;
			PerfectBeforeJudgeTime = perfectBeforeJudgeTime;
			PerfectAfterJudgeTime = perfectAfterJudgeTime;
			GreatBeforeJudgeTime = greatBeforeJudgeTime;
			GreatAfterJudgeTime = greatAfterJudgeTime;
			GoodBeforeJudgeTime = goodBeforeJudgeTime;
			GoodAfterJudgeTime = goodAfterJudgeTime;
			BadBeforeJudgeTime = badBeforeJudgeTime;
			BadAfterJudgeTime = badAfterJudgeTime;
		}
	}
}
