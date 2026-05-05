namespace Sekai.Live
{
	public class ConnectionNote : NoteBase
	{
		private readonly NoteLineType lineType;

		public override NoteLineType LineType
		{
			get { return lineType; }
		}

		public ConnectionNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, NoteType type, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, speedRatio, type)
		{
			this.lineType = lineType;
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
			if (MusicScoreInfo.time > currentFrameInfo.time)
			{
				return;
			}

			if (ShouldWaitForInput())
			{
				State = NoteState.Last;
				return;
			}

			JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
			State = NoteState.Done;
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

			NoteBase parentChild = parentNote?.ChildNote as NoteBase;
			if (parentChild != null && parentChild.Result <= NoteResult.Pass && parentChild.State != NoteState.Input)
			{
				parentChild.Terminate();
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

		protected bool ShouldWaitForInput()
		{
			INote parentChild = parentNote?.ChildNote;
			return parentChild != null && parentChild.Result <= NoteResult.Pass && parentChild.State != NoteState.Input;
		}
	}
}
