using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sekai.Live
{
	public class FrictionFlickNote : NoteBase
	{
		private int lastInputFrame;

		private readonly (int id, bool distance, bool trueDirection)[] inputResults;

		private readonly float flickDistance;

		private bool isTrueDirection;

		public override NoteState State
		{
			get
			{
				return state;
			}
			protected set
			{
				if (value == NoteState.Done && State != NoteState.Done)
				{
					parentNote?.ForceTerminate();
					state = NoteState.Done;
					OnUnSpawnNote();
					return;
				}

				state = value;
				if (value == NoteState.Done)
				{
					OnUnSpawnNote();
				}
				else if (value == NoteState.Playing)
				{
					OnSpawnNote();
				}
			}
		}

		public FrictionFlickNote()
		{
			inputResults = new (int id, bool distance, bool trueDirection)[8];
			Category = NoteCategory.FrictionFlick;
		}

		public FrictionFlickNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, float speedRatio, NoteType type, NoteDirection direction, float screenDpiToInch, float laneOffset)
			: base(info, id, laneStart, laneEnd, category, speedRatio, laneOffset, type)
		{
			inputResults = new (int id, bool distance, bool trueDirection)[8];
			flickDistance = screenDpiToInch;
			Direction = direction;
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			base.Excute(currentFrameInfo, offsetTime);
			if ((isTrueDirection || (State == NoteState.Input && lastInputFrame + 1 < Time.frameCount))
				&& CalcProgress(currentFrameInfo, offsetTime) >= 1f)
			{
				JudgmentFlick(currentFrameInfo.time);
			}
		}

		private bool JudgmentFlick(float musicTime)
		{
			if (inputResults == null || inputResults.Length == 0)
			{
				return false;
			}

			bool hasInput = false;
			bool direction = false;
			foreach (var result in inputResults)
			{
				if (result.id != 0 && result.distance && result.trueDirection)
				{
					hasInput = true;
					direction = true;
					break;
				}
			}

			if (!hasInput)
			{
				foreach (var result in inputResults)
				{
					if (result.id != 0 && result.distance)
					{
						hasInput = true;
						direction = result.trueDirection;
						break;
					}
				}
			}

			if (!hasInput)
			{
				return false;
			}

			var judgeInfo = LiveConfig.CalculateFrictionFlickNoteResult(this, musicTime, direction);
			JudgeInfo = onUpdateResult != null ? onUpdateResult(judgeInfo) : judgeInfo;
			State = NoteState.Done;
			return true;
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Done)
			{
				return false;
			}

			if (!IsLaneCheck(touch))
			{
				return true;
			}

			if (JudgeLaneStart > lane || JudgeLaneEnd < lane)
			{
				return false;
			}

			var offset = MusicScoreInfo.time - touch.musicTime;
			return LiveUtility.IsJudgmentTiming(LiveUtility.GetINoteToJudgeFrameType(this), offset);
		}

		private bool IsLaneCheck(LiveTouch touch)
		{
			if (State != NoteState.Input)
			{
				return true;
			}

			foreach (var result in inputResults)
			{
				if (result.id != 0 && result.id == touch.touchId)
				{
					return false;
				}
			}

			return true;
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
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

			var isUpdatedDelta = false;
			var trueDirection = false;
			UpdateDelta(touch.delta, ref isUpdatedDelta, ref trueDirection);
			if (isUpdatedDelta)
			{
				AddInputResult(ref touch, ref isUpdatedDelta, ref trueDirection);
			}

			lastInputFrame = Time.frameCount;
			if (trueDirection && isUpdatedDelta && MusicScoreInfo.time - touch.musicTime <= -LiveConfig.JustPerfectJudgeTimeHalf)
			{
				return JudgmentFlick(touch.musicTime);
			}

			return false;
		}

		private void AddInputResult(ref LiveTouch touch, ref bool isUpdatedDelta, ref bool isTrueDirection)
		{
			for (var i = 0; i < inputResults.Length; i++)
			{
				if (inputResults[i].id == 0 || inputResults[i].id == touch.touchId)
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

			var frameScale = Time.deltaTime / 0.016667f;
			if (frameScale > 0f)
			{
				touchDelta /= frameScale;
			}

			isUpdatedDelta = touchDelta.sqrMagnitude > flickDistance * flickDistance;
			if (!isUpdatedDelta)
			{
				isTrueDirection = false;
				return;
			}

			Vector2 requiredDirection = Direction switch
			{
				NoteDirection.Left => new Vector2(-1f, 1f),
				NoteDirection.Right => new Vector2(1f, 1f),
				_ => Vector2.up
			};

			var dot = Vector2.Dot(touchDelta.normalized, requiredDirection.normalized);
			isTrueDirection = dot >= 0f || Direction == NoteDirection.Default;
			if (isTrueDirection)
			{
				this.isTrueDirection = true;
			}
		}

		public override NoteResult CalcNoteResult(float musicTime)
		{
			return LiveConfig.CalculateFrictionFlickNoteResult(this, musicTime, true).Item1;
		}

		public override void ResetNote()
		{
			base.ResetNote();
			Array.Clear(inputResults, 0, inputResults.Length);
			lastInputFrame = 0;
			isTrueDirection = false;
		}
	}
}
