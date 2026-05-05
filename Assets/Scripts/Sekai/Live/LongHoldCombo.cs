namespace Sekai.Live
{
	public class LongHoldCombo : NoteBase
	{
		private readonly float laneOffset;

		public float LaneOffset
		{
			get { return laneOffset; }
		}

		public LongHoldCombo(MusicScoreInfo info, NoteType type, LiveBundleBuildData liveBundleBuildData, float speedRatio, float laneOffset)
			: base(info, 0, 0, 0, NoteCategory.Combo, liveBundleBuildData, speedRatio, type)
		{
			this.laneOffset = laneOffset;
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
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
			return State == NoteState.Last &&
				parentNote != null &&
				parentNote.JudgedLaneStart - laneOffset <= lane &&
				parentNote.JudgedLaneEnd + laneOffset >= lane;
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Last)
			{
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

			JudgeInfo = IsChildSuccessful()
				? (NoteResult.JustPerfect, NoteResultDescription.None)
				: (NoteResult.Miss, NoteResultDescription.None);
			State = NoteState.Done;
		}

		protected override void OnSpawnNote()
		{
		}

		protected override void OnUnSpawnNote()
		{
		}

		public override bool AutoJudgment(MusicScoreInfo currentFrameInfo)
		{
			if (State == NoteState.Done || MusicScoreInfo.time > currentFrameInfo.time)
			{
				return false;
			}

			JudgeInfo = (NoteResult.Auto, NoteResultDescription.None);
			State = NoteState.Done;
			return true;
		}

		private bool ShouldWaitForInput()
		{
			INote parentChild = parentNote?.ChildNote;
			return parentChild != null && parentChild.Result <= NoteResult.Pass && parentChild.State != NoteState.Input;
		}

		private bool IsChildSuccessful()
		{
			INote parentChild = parentNote?.ChildNote;
			return parentChild != null && (parentChild.Result > NoteResult.Pass || parentChild.State == NoteState.Input);
		}
	}
}
