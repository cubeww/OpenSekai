namespace Sekai.Live
{
	public class FrictionLongNote : LongNote
	{
		protected bool isTouched;

		protected virtual NoteResult CompletedTouchResult => NoteResult.JustPerfect;

		public FrictionLongNote()
		{
			Category = NoteCategory.FrictionLong;
		}

		public FrictionLongNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, LiveBundleBuildData bundleBuildData, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, type, speedRatio, LiveUtility.GetLaneOffset(category, bundleBuildData), lineType)
		{
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			base.Excute(currentFrameInfo, offsetTime);
			if (State == NoteState.Done)
			{
				return;
			}

			if (JudgeInfo.result == NoteResult.None && State <= NoteState.Last && isTouched)
			{
				if (Progress < 1f)
				{
					return;
				}

				JudgeInfo = (CompletedTouchResult, NoteResultDescription.None);
				State = NoteState.InputBegan;
			}
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			return State != NoteState.Done
				&& JudgedLaneStart <= lane
				&& JudgedLaneEnd >= lane
				&& LiveUtility.IsJudgmentTiming(LiveUtility.GetINoteToJudgeFrameType(this), OffsetJudgeTime);
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Done)
			{
				return false;
			}

			if (State == NoteState.Release || State == NoteState.InputBegan)
			{
				State = NoteState.Input;
			}

			isTouched = true;
			lastInputFrame = UnityEngine.Time.frameCount;
			OffsetJudgeTime = 0f;
			ConnectionNoteJudgment(ref touch, lane);

			if (childNote != null
				&& childNote.State != NoteState.Wait
				&& LiveConfig.IsLongEndJudgeTime(childNote.OffsetJudgeTime)
				&& childNote.IsJudgment(ref touch, lane)
				&& childNote.Judgment(ref touch, lane))
			{
				State = NoteState.Done;
			}

			return true;
		}
	}
}
