namespace Sekai.Live
{
	public class FrictionHideNote : FrictionNote
	{
		public override bool HasJudgment
		{
			get
			{
				return false;
			}
		}

		public FrictionHideNote()
		{
			Category = NoteCategory.FrictionHide;
		}

		public FrictionHideNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, NoteType type, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, type, speedRatio, lineType)
		{
		}

		public override void SetParentNote(LongNote note)
		{
			base.SetParentNote(note);
			Category = NoteCategory.FrictionHideLong;
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
				JudgeInfo = (NoteResult.Pass, NoteResultDescription.None);
				State = NoteState.Done;
				return;
			}

			OffsetJudgeTime = currentFrameInfo.time - MusicScoreInfo.time;
			if (State == NoteState.Wait)
			{
				State = NoteState.Playing;
			}

			var progress = CalcProgress(currentFrameInfo, offsetTime);
			if (progress < 0f && state != NoteState.Playing)
			{
				return;
			}

			Progress = progress;
			if ((State == NoteState.Input || isTouched) && Progress >= 1f)
			{
				OffsetJudgeTime = 0f;
				Progress = 1f;
				JudgeInfo = (NoteResult.Pass, NoteResultDescription.None);
				State = NoteState.Done;
				return;
			}

			if (OffsetJudgeTime > LiveConfig.noteTypeJudgeData.JudgeTimeAfter)
			{
				State = NoteState.Last;
			}
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			return State != NoteState.Done
				&& JudgeLaneStart <= lane
				&& JudgeLaneEnd >= lane
				&& LiveUtility.IsJudgmentTiming(LiveUtility.GetINoteToJudgeFrameType(this), MusicScoreInfo.time - touch.musicTime);
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Last)
			{
				OffsetJudgeTime = 0f;
				Progress = 1f;
				JudgeInfo = (NoteResult.Pass, NoteResultDescription.None);
				State = NoteState.Done;
				return true;
			}

			isTouched = true;
			State = NoteState.Input;
			return false;
		}

		protected override void OnSpawnNote()
		{
		}

		protected override void OnUnSpawnNote()
		{
			onUnspawn?.Invoke(this);
		}
	}
}
