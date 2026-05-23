namespace Sekai.Live
{
	public class ConnectionNote : NoteBase
	{
		public ConnectionNote()
		{
			Category = NoteCategory.Connection;
		}

		public ConnectionNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, float speedRatio, float laneOffset, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, speedRatio, laneOffset, type)
		{
			LineType = lineType;
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			this.offsetTime = offsetTime;
			if (State == NoteState.Done)
			{
				return;
			}

			if (State == NoteState.Last)
			{
				JudgeInfo = (NoteResult.Miss, NoteResultDescription.None);
				State = NoteState.Done;
				return;
			}

			if (State == NoteState.Wait)
			{
				State = NoteState.Playing;
			}

			Progress = CalcProgress(currentFrameInfo, offsetTime);
			if (MusicScoreInfo.time <= currentFrameInfo.time)
			{
				State = NoteState.Last;
			}
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			return State == NoteState.Last && JudgeLaneStart <= lane && JudgeLaneEnd >= lane;
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Last)
			{
				OffsetJudgeTime = 0f;
				Progress = 1f;
				JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
				State = NoteState.Done;
			}

			return true;
		}

		public override void Terminate()
		{
			if (State == NoteState.Done)
			{
				return;
			}

			JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
			State = NoteState.Done;
		}

		public override bool AutoJudgment(MusicScoreInfo currentFrameInfo)
		{
			if (State == NoteState.Done || MusicScoreInfo.time > currentFrameInfo.time)
			{
				return false;
			}

			OffsetJudgeTime = 0f;
			Progress = 1f;
			JudgeInfo = (NoteResult.Auto, NoteResultDescription.None);
			State = NoteState.Done;
			return true;
		}
	}
}
