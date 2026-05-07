namespace Sekai.Live
{
	public class NormalNote : NoteBase
	{
		public NormalNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, float speedRatio, NoteType type)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, speedRatio, type)
		{
		}

		public override NoteState State
		{
			get { return base.State; }
			protected set
			{
				if (value == NoteState.Done && State != NoteState.Done && parentNote != null)
				{
					parentNote.ForceTerminate();
				}
				base.State = value;
			}
		}

		public override void SetParentNote(LongNote note)
		{
			base.SetParentNote(note);
			if (parentNote != null && (parentNote.Type == NoteType.Critical || Type == NoteType.Critical))
			{
				Type = NoteType.Critical;
			}
			Category = NoteCategory.Long;
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Done || JudgeLaneStart > lane || JudgeLaneEnd < lane)
			{
				return false;
			}

			JudgeFrameType frameType = LiveUtility.GetINoteToJudgeFrameType(this);
			if (!LiveUtility.IsJudgmentTiming(frameType, MusicScoreInfo.time - touch.musicTime))
			{
				return false;
			}
			if (Category == NoteCategory.Normal)
			{
				return touch.phase == UnityEngine.InputSystem.TouchPhase.Began;
			}
			return Category == NoteCategory.Long && IsEndedOrCanceled(touch.phase);
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Done)
			{
				return false;
			}
			if ((Category != NoteCategory.Normal || touch.phase != UnityEngine.InputSystem.TouchPhase.Began) &&
				(Category != NoteCategory.Long || !IsEndedOrCanceled(touch.phase)))
			{
				return false;
			}

			(NoteResult, NoteResultDescription) result = parentNote != null
				? LiveConfig.CalculateLongEndNoteResult(this, touch.musicTime)
				: LiveConfig.CalculateDefaultNoteResult(this, touch.musicTime);
			JudgeInfo = onUpdateResult != null ? onUpdateResult(result) : result;
			State = NoteState.Done;
			return true;
		}

		public override NoteResult CalcNoteResult(float musicTime)
		{
			return (parentNote != null
				? LiveConfig.CalculateLongEndNoteResult(this, musicTime)
				: LiveConfig.CalculateDefaultNoteResult(this, musicTime)).Item1;
		}

		private static bool IsEndedOrCanceled(UnityEngine.InputSystem.TouchPhase phase)
		{
			return phase == UnityEngine.InputSystem.TouchPhase.Ended || phase == UnityEngine.InputSystem.TouchPhase.Canceled;
		}
	}
}
