namespace Sekai.Live
{
	public class FrictionLongNote : LongNote
	{
		protected bool isTouched;

		public FrictionLongNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, LiveBundleBuildData bundleBuildData, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, type, bundleBuildData, speedRatio, lineType)
		{
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			this.offsetTime = offsetTime;
			if (State == NoteState.Wait)
			{
				State = NoteState.Playing;
			}
			if (State == NoteState.Last)
			{
				JudgeInfo = (NoteResult.Miss, NoteResultDescription.None);
				State = NoteState.Release;
				onDamage?.Invoke(this);
				onUpdateCombo?.Invoke(this);
				onJudgment?.Invoke(this);
			}

			OffsetJudgeTime = currentFrameInfo.time - MusicScoreInfo.time;
			Progress = CalcProgress(currentFrameInfo, offsetTime);
			ExecuteSubNotes(currentFrameInfo);
			UpdateExecutingLane(currentFrameInfo);

			if (State == NoteState.Playing && LiveConfig.JudgeTime < currentFrameInfo.time - MusicScoreInfo.time)
			{
				State = NoteState.Last;
				return;
			}
			if (Result == NoteResult.None && Description == NoteResultDescription.None && state <= NoteState.Last && isTouched)
			{
				if (Progress < 1f)
				{
					return;
				}
				JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
				State = NoteState.InputBegan;
			}
			if ((State == NoteState.InputBegan || State == NoteState.Input) && lastInputFrame + LiveConfig.LiveMasterData.MissMesh < UnityEngine.Time.frameCount)
			{
				State = NoteState.Release;
			}
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Done)
			{
				return false;
			}

			float laneStart = Result != NoteResult.None || Description != NoteResultDescription.None ? JudgedLaneStart : JudgeLaneStart;
			float laneEnd = Result != NoteResult.None || Description != NoteResultDescription.None ? JudgedLaneEnd : JudgeLaneEnd;
			return laneStart <= lane && laneEnd >= lane && IsJudgmentTime();
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
			if (childNote != null &&
				childNote.HasJudgment &&
				LiveConfig.IsLongEndJudgeTime(childNote.OffsetJudgeTime) &&
				childNote.IsJudgment(ref touch, lane) &&
				childNote.Judgment(ref touch, lane))
			{
				State = NoteState.Done;
			}
			return true;
		}
	}
}
