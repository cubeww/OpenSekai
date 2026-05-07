using System;

namespace Sekai
{
	[Serializable]
	public class MasterIngameJudgeFrame
	{
		public int id;

		public string ingameNoteType;

		public float perfectBefore;

		public float perfectAfter;

		public float greatBefore;

		public float greatAfter;

		public float goodBefore;

		public float goodAfter;

		public float badBefore;

		public float badAfter;

		public JudgeFrameType NoteType { get; private set; }

		public float PerfectBeforeJudgeTime { get; private set; }

		public float PerfectAfterJudgeTime { get; private set; }

		public float GreatBeforeJudgeTime { get; private set; }

		public float GreatAfterJudgeTime { get; private set; }

		public float GoodBeforeJudgeTime { get; private set; }

		public float GoodAfterJudgeTime { get; private set; }

		public float BadBeforeJudgeTime { get; private set; }

		public float BadAfterJudgeTime { get; private set; }

		public MasterIngameJudgeFrame()
		{
		}

		public MasterIngameJudgeFrame(
			JudgeFrameType noteType,
			string ingameNoteType,
			float perfectBefore,
			float perfectAfter,
			float greatBefore,
			float greatAfter,
			float goodBefore,
			float goodAfter,
			float badBefore,
			float badAfter)
		{
			id = (int)noteType;
			this.ingameNoteType = ingameNoteType;
			this.perfectBefore = perfectBefore;
			this.perfectAfter = perfectAfter;
			this.greatBefore = greatBefore;
			this.greatAfter = greatAfter;
			this.goodBefore = goodBefore;
			this.goodAfter = goodAfter;
			this.badBefore = badBefore;
			this.badAfter = badAfter;
			RefreshCalculatedValues();
		}

		public void RefreshCalculatedValues()
		{
			NoteType = (JudgeFrameType)id;
			PerfectBeforeJudgeTime = FramesToSeconds(perfectBefore);
			PerfectAfterJudgeTime = FramesToSeconds(perfectAfter);
			GreatBeforeJudgeTime = FramesToSeconds(greatBefore);
			GreatAfterJudgeTime = FramesToSeconds(greatAfter);
			GoodBeforeJudgeTime = FramesToSeconds(goodBefore);
			GoodAfterJudgeTime = FramesToSeconds(goodAfter);
			BadBeforeJudgeTime = FramesToSeconds(badBefore);
			BadAfterJudgeTime = FramesToSeconds(badAfter);
		}

		private static float FramesToSeconds(float frame)
		{
			return frame * Live.LiveUtility.OneFrameTime;
		}
	}
}
