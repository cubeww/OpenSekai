using UnityEngine;

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
			flickDistance = LiveUtility.ScreenDpiToInch(bundleBuildData != null ? bundleBuildData.FlickDistance : LiveBundleBuildData.DefaultFlickDistance);
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

			bool directionResult = allDirections || hasDirection;
			(NoteResult, NoteResultDescription) result = parentNote != null
				? LiveConfig.CalculateLongEndFlickNoteResult(this, musicTime, directionResult)
				: LiveConfig.CalculateFlickNoteResult(this, musicTime, directionResult);
			JudgeInfo = onUpdateResult != null ? onUpdateResult(result) : result;
			State = NoteState.Done;
			return true;
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Done)
			{
				return false;
			}
			if (IsLaneCheck(touch))
			{
				if (JudgeLaneStart > lane || JudgeLaneEnd < lane)
				{
					return false;
				}
				if (!LiveUtility.IsJudgmentTiming(LiveUtility.GetINoteToJudgeFrameType(this), MusicScoreInfo.time - touch.musicTime))
				{
					return false;
				}
			}
			return parentNote != null || State != NoteState.Playing || touch.phase == UnityEngine.InputSystem.TouchPhase.Began;
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			bool isUpdatedDelta = false;
			bool isTrueDirection = false;
			if (State == NoteState.Done)
			{
				return false;
			}
			if (State == NoteState.Playing)
			{
				State = NoteState.Input;
			}
			if (State != NoteState.Input)
			{
				return false;
			}

			if (parentNote == null)
			{
				if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
				{
					AddInputResult(ref touch, ref isUpdatedDelta, ref isTrueDirection);
				}
				else if (HasInputResult(touch.touchId))
				{
					UpdateDelta(touch.delta, ref isUpdatedDelta, ref isTrueDirection);
					if (isUpdatedDelta)
					{
						AddInputResult(ref touch, ref isUpdatedDelta, ref isTrueDirection);
					}
				}
				return isUpdatedDelta ? JudgmentFlick(touch.musicTime) : false;
			}

			UpdateDelta(touch.delta, ref isUpdatedDelta, ref isTrueDirection);
			if (isUpdatedDelta)
			{
				AddInputResult(ref touch, ref isUpdatedDelta, ref isTrueDirection);
			}
			lastInputFrame = Time.frameCount;
			if (MusicScoreInfo.time - touch.musicTime <= -LiveUtility.HalfFrameTime && isTrueDirection)
			{
				return JudgmentFlick(touch.musicTime);
			}
			return false;
		}

		public override NoteResult CalcNoteResult(float musicTime)
		{
			return LiveConfig.CalculateLongStartNoteResult(this, musicTime).Item1;
		}

		private bool IsLaneCheck(LiveTouch touch)
		{
			if (State != NoteState.Input)
			{
				return true;
			}
			for (int i = 0; i < inputResults.Length; i++)
			{
				if (inputResults[i].Item1 != 0 && inputResults[i].Item1 == touch.touchId)
				{
					return false;
				}
			}
			return true;
		}

		private void AddInputResult(ref LiveTouch touch, ref bool isUpdatedDelta, ref bool isTrueDirection)
		{
			for (int i = 0; i < inputResults.Length; i++)
			{
				if (inputResults[i].Item1 == 0 || inputResults[i].Item1 == touch.touchId)
				{
					inputResults[i] = (touch.touchId, isUpdatedDelta, isTrueDirection);
					return;
				}
			}
		}

		private void UpdateDelta(Vector2 touchDelta, ref bool isUpdatedDelta, ref bool isTrueDirection)
		{
			if (touchDelta == LiveUtility.Vector2Zero)
			{
				return;
			}

			float scale = Time.deltaTime / LiveUtility.OneFrameTime;
			if (scale > 0f)
			{
				touchDelta /= scale;
			}
			if (touchDelta.sqrMagnitude <= flickDistance * flickDistance)
			{
				isUpdatedDelta = false;
				isTrueDirection = false;
				return;
			}

			isUpdatedDelta = true;
			Vector2 expected = Vector2.up;
			if (Direction == NoteDirection.Left)
			{
				expected = LiveUtility.Vector2Left;
			}
			else if (Direction == NoteDirection.Right)
			{
				expected = LiveUtility.Vector2Right;
			}
			isTrueDirection = Direction == NoteDirection.Default || Vector2.Dot(expected.normalized, touchDelta.normalized) >= 0f;
		}

		private bool HasInputResult(int touchId)
		{
			for (int i = 0; i < inputResults.Length; i++)
			{
				if (inputResults[i].Item1 != 0 && inputResults[i].Item1 == touchId)
				{
					return true;
				}
			}
			return false;
		}
	}
}
