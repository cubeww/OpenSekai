namespace Sekai.Live
{
	public class FlickNote : NoteBase
	{
		private int lastInputFrame;

		private readonly (int, bool, bool)[] inputResults;

		private readonly float flickDistance;

		public FlickNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, float speedRatio, NoteType type, NoteDirection direction)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, speedRatio, type)
		{
			inputResults = new (int, bool, bool)[8];
			flickDistance = bundleBuildData != null ? LiveUtility.ScreenDpiToInch(bundleBuildData.FlickDistance) : 0f;
			Direction = direction;
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

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			base.Excute(currentFrameInfo, offsetTime);
			if (State == NoteState.Input && lastInputFrame + 1 < UnityEngine.Time.frameCount && CalcProgress(currentFrameInfo, offsetTime) >= 1f)
			{
				JudgmentFlick(currentFrameInfo.time);
			}
		}

		private bool JudgmentFlick(float musicTime)
		{
			bool hasInput = false;
			bool hasDirection = false;
			bool allDirections = true;
			for (int i = 0; i < inputResults.Length; i++)
			{
				(int id, bool updated, bool trueDirection) = inputResults[i];
				if (id == 0 || !updated)
				{
					continue;
				}
				hasInput = true;
				hasDirection |= trueDirection;
				allDirections &= trueDirection;
			}
			if (!hasInput)
			{
				return false;
			}

			JudgeInfo = (allDirections || hasDirection)
				? (NoteResult.JustPerfect, NoteResultDescription.None)
				: (NoteResult.Great, NoteResultDescription.FlickMiss);
			State = NoteState.Done;
			return true;
		}
	}
}
