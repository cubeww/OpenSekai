using System;
using System.Collections.Generic;
using System.Linq;
using Sekai;
using Sekai.Live;
using UnityEngine;
using InputTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using InputTouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Sekai.Core.Live
{
	public class LiveLogic
	{
		[Serializable]
		public class InputTmp
		{
			public LiveTouch Touch;

			[NonSerialized]
			public float Lane;

			[NonSerialized]
			public List<NoteBase> CandidateNotes;

			public InputTmp()
			{
				Touch = default;
				CandidateNotes = new List<NoteBase>(4);
			}

			public InputTmp(InputTmp source)
			{
				Touch = source.Touch;
				Lane = source.Lane;
				CandidateNotes = new List<NoteBase>(4);
			}
		}

		private const int InputTmpCount = 128;

		private const int MaxFingerCount = 10;

		private LiveBootDataBase bootData;

		private LiveViewBase[] liveViews;

		private MusicScore musicScore;

		private EventBase[] eventArray;

		private NoteBase[] noteArray;

		private NoteBase[] highSpeedNoteArray;

		private ScoreLogic scoreLogic;

		private int noteStartIndex;

		private int highSpeedNoteStartIndex;

		private readonly Dictionary<int, (double, InputTouchPhase)> lastTouches;

		private MusicScoreInfo currentFrameInfo;

		private float noteDisplayTimeOffset;

		private readonly List<NoteBase> judgmentNoteList;

		private readonly LiveBundleBuildData liveBundleBuildData;

		private Action<EventBase> noteEventResultCallback;

		private LiveResult result;

		private readonly InputTmp[] inputTmpArray;

		private readonly List<InputTmp> activeInputList;

		private Camera calcCamera;

		private double currentGameTime;

		private readonly List<NoteBase> connectionNoteList;

		private readonly Dictionary<int, (double, Vector2)> touchPositionList;

		private readonly List<int> usedFingerList;

		private readonly Dictionary<int, double> InputedNormalNoteTouch;

		public LiveLogic(LiveBundleBuildData data)
		{
			lastTouches = new Dictionary<int, (double, InputTouchPhase)>(NativeInput.Fingers.Count);
			judgmentNoteList = new List<NoteBase>();
			inputTmpArray = new InputTmp[InputTmpCount];
			activeInputList = new List<InputTmp>(InputTmpCount);
			connectionNoteList = new List<NoteBase>(5);
			touchPositionList = new Dictionary<int, (double, Vector2)>(MaxFingerCount);
			usedFingerList = new List<int>(MaxFingerCount);
			InputedNormalNoteTouch = new Dictionary<int, double>(MaxFingerCount);
			liveBundleBuildData = data;
		}

		public LiveScore Score
		{
			get { return scoreLogic != null ? scoreLogic.Score : default(LiveScore); }
		}

		public bool IsAllPerfectCombo
		{
			get { return scoreLogic != null && scoreLogic.IsAllPerfectCombo; }
		}

		public bool IsPerfectCombo
		{
			get { return scoreLogic != null && scoreLogic.IsPerfectCombo; }
		}

		public bool IsNotesAllFinished
		{
			get
			{
				int noteCount = noteArray != null ? noteArray.Length : 0;
				int highSpeedNoteCount = highSpeedNoteArray != null ? highSpeedNoteArray.Length : 0;
				return noteStartIndex >= noteCount && highSpeedNoteStartIndex >= highSpeedNoteCount;
			}
		}

		public LiveResult Result
		{
			get { return result; }
			private set
			{
				if (result == value)
				{
					return;
				}

				result = value;
				if (value == LiveResult.Failure)
				{
					OnFailure?.Invoke();
					value = result;
				}
				if (value >= LiveResult.Clear)
				{
					OnFinished?.Invoke();
				}
			}
		}

		public int TapCount { get; private set; }

		public event Action OnFailure;

		public event Action OnFinished;

		public void Setup(LiveBootDataBase bootData, LiveViewBase[] liveViews, MusicScore musicScore, Camera inputCamera = null)
		{
			this.liveViews = liveViews;
			this.bootData = bootData;
			this.musicScore = musicScore ?? new MusicScore();
			LiveConfig.CacheSpeedTime = 0f;

			float noteSpeed = bootData != null && bootData.LiveSettingData != null
				? bootData.LiveSettingData.NoteSpeed
				: LiveConfig.DefaultNoteSpeed;
			noteDisplayTimeOffset = LiveConfig.GetNoteDisplayOffsetTime(noteSpeed);

			for (int i = 0; liveViews != null && i < liveViews.Length; i++)
			{
				liveViews[i]?.CreateNotePool(LiveUtility.CalculateMaxSimultaneousNotes(this.musicScore, noteDisplayTimeOffset));
			}

			this.musicScore.Update(0f);
			currentFrameInfo = MusicScore.CurrentFrameInfo;
			eventArray = this.musicScore.EventArray ?? Array.Empty<EventBase>();
			noteArray = this.musicScore.NoteArray ?? Array.Empty<NoteBase>();
			highSpeedNoteArray = noteArray.Where(note => note != null && !note.speedRatio.Equals(1f)).ToArray();

			for (int i = 0; i < noteArray.Length; i++)
			{
				NoteBase note = noteArray[i];
				if (note == null)
				{
					continue;
				}

				note.OnSpawn = OnSpawnNote;
				note.OnJudgment = OnJudgmentNote;
				note.OnUnspawn = OnUnSpawnNote;
				note.OnUpdateScore = OnUpdateScore;
				note.OnUpdateCombo = OnUpdateCombo;
				note.OnDamage = OnDamage;
				note.OnUpdateResult = OnUpdateResult;
				note.CalcTimeOffset = CalcTimeOffset;

				for (int j = 0; note.NoteList != null && j < note.NoteList.Count; j++)
				{
					note.NoteList[j].CalcTimeOffset = CalcTimeOffset;
				}
			}

			for (int i = 0; i < inputTmpArray.Length; i++)
			{
				inputTmpArray[i] = new InputTmp();
			}

			noteStartIndex = 0;
			highSpeedNoteStartIndex = 0;
			TapCount = 0;
			noteEventResultCallback = ExcuteEvent;
			SetScoreLogic(new ScoreLogic(liveBundleBuildData));
			calcCamera = inputCamera != null ? inputCamera : Camera.main;
			Result = LiveResult.None;
			RefreshInput();
		}

		public void SetScoreLogic(ScoreLogic scoreLogic)
		{
			this.scoreLogic = scoreLogic;
			this.scoreLogic?.Setup(bootData, noteArray, eventArray, liveViews);
		}

		public void Continue(float time)
		{
			Result = LiveResult.None;
			RefreshInput();
			scoreLogic?.Continue(time);
		}

		public void OnUpdate(float scoreInfoTime, double currentGameTime)
		{
			if (Result > LiveResult.Failure)
			{
				return;
			}

			if (musicScore == null)
			{
				return;
			}

			musicScore.Update(scoreInfoTime);
			currentFrameInfo = MusicScore.CurrentFrameInfo;
			this.currentGameTime = currentGameTime;
			InitializeUpdate();
			UpdateEvent();
			UpdateNote();

			if (Score.life == 0)
			{
				Result = LiveResult.Failure;
			}
			if (IsNotesAllFinished && Result == LiveResult.None)
			{
				Result = LiveResult.Clear;
			}
		}

		private void InitializeUpdate()
		{
			judgmentNoteList.Clear();
		}

		private void UpdateEvent()
		{
			if (eventArray == null)
			{
				return;
			}

			for (int i = 0; i < eventArray.Length; i++)
			{
				eventArray[i]?.Update(currentFrameInfo, noteEventResultCallback);
			}
		}

		private void UpdateNote()
		{
			for (int i = noteStartIndex; noteArray != null && i < noteArray.Length; i++)
			{
				NoteBase note = noteArray[i];
				if (note == null)
				{
					continue;
				}

				if (note.State == NoteState.Done)
				{
					if (i == noteStartIndex)
					{
						noteStartIndex = i + 1;
					}
					continue;
				}

				if (!note.speedRatio.Equals(1f))
				{
					continue;
				}

				float timeOffset = CalcTimeOffset(note);
				if (timeOffset >= 0f && IsJudgeTimingTimeOffsetHighSpeedNote(timeOffset, ref note))
				{
					break;
				}
				if (timeOffset < 0f && currentFrameInfo.time >= note.MusicScoreInfo.time)
				{
					timeOffset = -timeOffset;
				}

				note.Excute(currentFrameInfo, timeOffset);
				if (note.State != NoteState.Done)
				{
					UpdateJudgmentNoteArray(ref note);
				}
			}

			for (int i = highSpeedNoteStartIndex; highSpeedNoteArray != null && i < highSpeedNoteArray.Length; i++)
			{
				NoteBase note = highSpeedNoteArray[i];
				if (note == null)
				{
					continue;
				}

				if (note.State == NoteState.Done)
				{
					if (i == highSpeedNoteStartIndex)
					{
						highSpeedNoteStartIndex = i + 1;
					}
					continue;
				}

				float timeOffset = currentFrameInfo.time >= note.MusicScoreInfo.time ? noteDisplayTimeOffset : CalcTimeOffset(note);
				if (IsJudgeTimingTimeOffsetNote(timeOffset, ref note))
				{
					continue;
				}

				note.Excute(currentFrameInfo, timeOffset);
				if (note.State != NoteState.Done)
				{
					UpdateJudgmentNoteArray(ref note);
				}
			}
		}

		private bool IsJudgeTimingTimeOffsetNote(float timeOffset, ref NoteBase note)
		{
			if (timeOffset.Equals(0f))
			{
				return false;
			}
			return timeOffset + currentFrameInfo.time + LiveUtility.OneFrameTime <= note.MusicScoreInfo.time;
		}

		private bool IsJudgeTimingTimeOffsetHighSpeedNote(float timeOffset, ref NoteBase note)
		{
			return note.State != NoteState.Playing && IsJudgeTimingTimeOffsetNote(timeOffset, ref note);
		}

		private float CalcTimeOffset(NoteBase noteInfo)
		{
			if (noteInfo == null)
			{
				return 0f;
			}

			float currentProgress = currentFrameInfo.bar + currentFrameInfo.barProgress;
			float noteProgress = noteInfo.MusicScoreInfo.bar + noteInfo.MusicScoreInfo.barProgress;
			float speedRatio = CalcNoteSpeedRatio(currentProgress, noteProgress);
			if (speedRatio.Equals(0f))
			{
				return 0f;
			}
			return noteDisplayTimeOffset / speedRatio / noteInfo.speedRatio;
		}

		private float CalcNoteSpeedRatio(float currentProgress, float noteProgress)
		{
			if (noteProgress <= currentProgress)
			{
				return currentFrameInfo.speedRatio;
			}

			MusicScoreInfo[] infos = musicScore != null ? musicScore.musicScoreInfoArray : null;
			if (infos == null || infos.Length == 0)
			{
				return currentFrameInfo.speedRatio;
			}

			float weighted = 0f;
			float cursor = currentProgress;
			float ratio = currentFrameInfo.speedRatio;
			bool started = false;

			for (int i = 0; i < infos.Length; i++)
			{
				float eventProgress = infos[i].bar + infos[i].barProgress;
				if (eventProgress <= currentProgress)
				{
					ratio = infos[i].speedRatio;
					cursor = currentProgress;
					continue;
				}

				if (!started)
				{
					started = true;
				}
				if (eventProgress >= noteProgress)
				{
					break;
				}

				weighted += ratio * (eventProgress - cursor);
				cursor = eventProgress;
				ratio = infos[i].speedRatio;
			}

			weighted += ratio * (noteProgress - cursor);
			float length = noteProgress - currentProgress;
			if (length.Equals(0f))
			{
				return currentFrameInfo.speedRatio;
			}
			return weighted / length;
		}

		private void UpdateJudgmentNoteArray(ref NoteBase noteInfo)
		{
			if (noteInfo.HasJudgment)
			{
				judgmentNoteList.Add(noteInfo);
			}
		}

		private void OnSpawnNote(NoteBase noteInfo)
		{
			ForEachLiveView(view => view.SpawnNote(noteInfo));
		}

		private void OnJudgmentNote(NoteBase noteInfo)
		{
			scoreLogic?.UpdateNoteResult(noteInfo);
		}

		private void OnUnSpawnNote(NoteBase noteInfo)
		{
			ForEachLiveView(view => view.UnspawnNote(noteInfo));
		}

		private void ExcuteEvent(EventBase eventInfo)
		{
			scoreLogic?.ExcuteEvent(eventInfo);
		}

		private (NoteResult, NoteResultDescription) OnUpdateResult((NoteResult, NoteResultDescription) result)
		{
			return scoreLogic != null ? scoreLogic.UpdateResult(result) : result;
		}

		private void OnUpdateScore(NoteBase noteInfo)
		{
			scoreLogic?.CalculateScore(noteInfo, Score.life == 0);
		}

		private void OnUpdateCombo(NoteBase noteInfo)
		{
			scoreLogic?.UpdateCombo(noteInfo);
		}

		private void OnDamage(NoteBase noteInfo)
		{
			scoreLogic?.Damage(noteInfo);
		}

		public void OnAutoInput()
		{
			for (int i = noteStartIndex; noteArray != null && i < noteArray.Length; i++)
			{
				NoteBase note = noteArray[i];
				if (note == null)
				{
					continue;
				}
				if (note.MusicScoreInfo.time > currentFrameInfo.time)
				{
					break;
				}
				if (note.State != NoteState.Done)
				{
					note.AutoJudgment(currentFrameInfo);
				}
			}
		}

		public void RefreshInput()
		{
			NativeInput.Reset();

			lastTouches.Clear();
			activeInputList.Clear();
			touchPositionList.Clear();
			InputedNormalNoteTouch.Clear();

			foreach (InputTouch touch in NativeInput.ActiveTouches)
			{
				lastTouches[touch.touchId] = (touch.time, touch.phase);
			}
		}

		public void OnInput()
		{
			int activeInputCount = 0;
			activeInputList.Clear();

			InputTouch[] touches = NativeInput.ActiveTouches.ToArray();
			for (int i = 0; i < touches.Length; i++)
			{
				InputTouch touch = touches[i];
				if (lastTouches.TryGetValue(touch.touchId, out (double, InputTouchPhase) lastTouch) &&
					IsEndedOrCanceled(lastTouch.Item2))
				{
					if (touch.phase == InputTouchPhase.Began && touch.time > lastTouch.Item1)
					{
						lastTouches.Remove(touch.touchId);
						touchPositionList.Remove(touch.touchId);
						InputedNormalNoteTouch.Remove(touch.touchId);
					}
					else
					{
						continue;
					}
				}

				if (!IsValidScreenPosition(touch.screenPosition))
				{
					if (IsEndedOrCanceled(touch.phase))
					{
						lastTouches[touch.touchId] = (touch.time, touch.phase);
						touchPositionList.Remove(touch.touchId);
						InputedNormalNoteTouch.Remove(touch.touchId);
					}
					continue;
				}

				if (!lastTouches.ContainsKey(touch.touchId))
				{
					TouchExecution(ref touch, ref activeInputCount);
					lastTouches.Add(touch.touchId, (touch.time, touch.phase));
					continue;
				}

				(double lastTime, InputTouchPhase lastPhase) = lastTouches[touch.touchId];
				if (touch.time >= lastTime && !IsEndedOrCanceled(lastPhase))
				{
					TouchExecution(ref touch, ref activeInputCount);
					lastTouches[touch.touchId] = (touch.time, touch.phase);
				}
			}

			for (int i = 0; i < touches.Length; i++)
			{
				InputTouch touch = touches[i];
				Vector2 screenPosition = touch.screenPosition;
				if (!IsValidScreenPosition(screenPosition))
				{
					if (IsEndedOrCanceled(touch.phase))
					{
						touchPositionList.Remove(touch.touchId);
					}
					continue;
				}

				if (touchPositionList.ContainsKey(touch.touchId))
				{
					if (IsEndedOrCanceled(touch.phase))
					{
						touchPositionList.Remove(touch.touchId);
					}
					else if (touch.time > touchPositionList[touch.touchId].Item1)
					{
						touchPositionList[touch.touchId] = (touch.time, screenPosition);
					}
				}
				else
				{
					touchPositionList.Add(touch.touchId, (touch.time, screenPosition));
				}
			}

			Judgment();
		}

		private void TouchExecution(ref InputTouch touch, ref int activeInputCount)
		{
			int touchId = touch.touchId;
			InputTouchPhase phase = touch.phase;
			double time = touch.time;

			if (lastTouches.ContainsKey(touchId) && lastTouches[touchId].Item1.Equals(time))
			{
				int phaseValue = (int)phase;
				if (phaseValue <= 5)
				{
					if (((1 << phaseValue) & 0x19) != 0)
					{
						return;
					}
					if (((1 << phaseValue) & 0x22) != 0)
					{
						phase = InputTouchPhase.Moved;
					}
				}
			}

			float musicTime = (float)((time - currentGameTime) * LiveUtility.OneFrameTime + currentFrameInfo.time);
			CreateLiveTouch(touchId, musicTime, ref touch, out LiveTouch liveTouch, phase);
			OnProcessTouch(ref liveTouch, ref activeInputCount);
		}

		private void CreateLiveTouch(int touchId, float musicTime, ref InputTouch processTouch, out LiveTouch liveTouch, InputTouchPhase phase)
		{
			Vector2 screenPosition = processTouch.screenPosition;
			Camera camera = calcCamera != null ? calcCamera : Camera.main;
			Vector3 worldPosition = camera != null
				? camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0f))
				: Vector3.zero;
			Vector2 delta = LiveUtility.Vector2Zero;

			if (touchPositionList.ContainsKey(touchId) && processTouch.time >= touchPositionList[touchId].Item1)
			{
				delta = screenPosition - touchPositionList[touchId].Item2;
			}

			if (processTouch.phase == InputTouchPhase.Began)
			{
				touchPositionList[touchId] = (processTouch.time, screenPosition);
			}

			liveTouch = default;
			liveTouch.fingerId = processTouch.finger.index;
			liveTouch.touchId = touchId;
			liveTouch.delta = delta;
			liveTouch.worldPosition = worldPosition;
			liveTouch.phase = phase;
			liveTouch.musicTime = musicTime;
		}

		private void OnProcessTouch(ref LiveTouch processTouch, ref int activeInputCount)
		{
			float lane = GetJudgmentLane(processTouch.worldPosition, true);
			if (activeInputCount <= InputTmpCount - 1)
			{
				InputTmp input = inputTmpArray[activeInputCount++];
				input.CandidateNotes.Clear();
				input.Lane = lane;
				input.Touch = processTouch;
				activeInputList.Add(input);
			}

			if (processTouch.phase == InputTouchPhase.Began)
			{
				TapCount++;
			}
		}

		private void Judgment()
		{
			connectionNoteList.Clear();
			AllocateInputJudgableEachNotes();
			OverlapJudgmentSorting();
			LeaveNotesFastGeneration();
			PickUpNotBeginInputNotes();
			EvaluateJudgment();
		}

		private void AllocateInputJudgableEachNotes()
		{
			for (int i = 0; i < judgmentNoteList.Count; i++)
			{
				NoteBase judgmentNote = judgmentNoteList[i];
				bool isConnection = true;
				bool isJudged = false;

				for (int j = activeInputList.Count - 1; j >= 0; j--)
				{
					InputTmp activeInput = activeInputList[j];
					float lane = ConvertLaneBy(ref judgmentNote, ref activeInput);
					if (lane >= 0f && judgmentNote.IsJudgment(ref activeInput.Touch, lane))
					{
						isJudged = true;
						int touchId = activeInput.Touch.touchId;
						NoteBase note = judgmentNote.NoteList[judgmentNote.NoteList.Count - 1];
						if (judgmentNote.NoteList.Count == 1)
						{
							AddCheckSingleInputedNormalNoteTouch(ref judgmentNote, ref note, ref activeInput, ref isConnection, ref isJudged, ref touchId);
						}
						else
						{
							AddCheckLongInputedNormalNoteTouch(ref judgmentNote, ref note, ref activeInput, ref isConnection, ref isJudged, ref touchId);
						}
					}
					else if (judgmentNote.NoteList.Count > 0)
					{
						NoteBase firstNote = judgmentNote.NoteList[0];
						if ((firstNote is FrictionLongNote || firstNote is FrictionHideLongNote || firstNote is LongNote) &&
							firstNote.IsJudgment(ref activeInput.Touch, ConvertLaneBy(ref firstNote, ref activeInput)))
						{
							isConnection = true;
							isJudged = true;
						}
					}
				}

				if (isJudged && isConnection)
				{
					connectionNoteList.Add(judgmentNote);
				}
			}
		}

		private void OverlapJudgmentSorting()
		{
			for (int i = activeInputList.Count - 1; i >= 0; i--)
			{
				InputTmp input = activeInputList[i];
				for (int j = input.CandidateNotes.Count - 1; j >= 0; j--)
				{
					NoteBase note = input.CandidateNotes[j];
					for (int k = activeInputList.Count - 1; k >= 0; k--)
					{
						if (i == k)
						{
							continue;
						}

						InputTmp other = activeInputList[k];
						if (!other.CandidateNotes.Contains(note))
						{
							continue;
						}

						float inputDistance = note.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref note, ref input));
						float otherDistance = note.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref note, ref other));
						if (inputDistance <= otherDistance)
						{
							other.CandidateNotes.Remove(note);
						}
						else
						{
							input.CandidateNotes.Remove(note);
							break;
						}
					}
				}
			}
		}

		private void LeaveNotesFastGeneration()
		{
			for (int i = activeInputList.Count - 1; i >= 0; i--)
			{
				InputTmp input = activeInputList[i];
				while (input.CandidateNotes.Count >= 2)
				{
					NoteBase first = SelectComparableNote(input.CandidateNotes[0], input);
					NoteBase last = SelectComparableNote(input.CandidateNotes[input.CandidateNotes.Count - 1], input);
					bool removeLast;

					if (first.MusicScoreInfo.time.Equals(last.MusicScoreInfo.time))
					{
						float firstDistance = first.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref first, ref input));
						float lastDistance = last.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref last, ref input));
						removeLast = firstDistance < lastDistance;
					}
					else
					{
						NoteResult lastResult = last.CalcNoteResult(input.Touch.musicTime);
						NoteResult firstResult = first.CalcNoteResult(input.Touch.musicTime);
						removeLast = lastResult < NoteResult.Great || firstResult >= NoteResult.Great || first.MusicScoreInfo.time < last.MusicScoreInfo.time;
					}

					if (removeLast)
					{
						input.CandidateNotes.RemoveAt(input.CandidateNotes.Count - 1);
					}
					else
					{
						input.CandidateNotes.RemoveAt(0);
					}
				}
			}
		}

		private void PickUpNotBeginInputNotes()
		{
			for (int i = activeInputList.Count - 1; i >= 0; i--)
			{
				InputTmp input = activeInputList[i];
				for (int j = connectionNoteList.Count - 1; j >= 0; j--)
				{
					NoteBase note = connectionNoteList[j];
					float lane = ConvertLaneBy(ref note, ref input);
					if (lane >= 0f && note.IsJudgment(ref input.Touch, lane))
					{
						input.CandidateNotes.Add(note);
						connectionNoteList.RemoveAt(j);
					}
				}
			}
		}

		private void EvaluateJudgment()
		{
			usedFingerList.Clear();
			for (int i = 0; i < activeInputList.Count; i++)
			{
				InputTmp input = activeInputList[i];
				if (input.CandidateNotes.Count > 0)
				{
					for (int j = 0; j < input.CandidateNotes.Count; j++)
					{
						NoteBase note = input.CandidateNotes[j];
						float lane = ConvertLaneBy(ref note, ref input);
						note.Judgment(ref input.Touch, lane);
						if (note.Result == NoteResult.None || note.State == NoteState.Input)
						{
							InputedNormalNoteTouch[input.Touch.touchId] = input.Touch.musicTime;
						}
						judgmentNoteList.Remove(note);
					}

					input.CandidateNotes.Clear();
					usedFingerList.Add(input.Touch.fingerId);
				}
			}

			for (int i = 0; i < activeInputList.Count; i++)
			{
				InputTmp input = activeInputList[i];
				bool unused = true;
				for (int j = usedFingerList.Count - 1; j >= 0; j--)
				{
					unused &= usedFingerList[j] != input.Touch.fingerId;
				}

				if (unused && input.Lane >= 0f)
				{
					liveViews.Unpicked(Mathf.RoundToInt(input.Lane), ref input.Touch);
				}
				if (IsEndedOrCanceled(input.Touch.phase))
				{
					InputedNormalNoteTouch.Remove(input.Touch.touchId);
				}
			}

			activeInputList.Clear();
		}

		private void AddCheckLongInputedNormalNoteTouch(ref NoteBase judgmentNote, ref NoteBase note, ref InputTmp activeInput, ref bool isConnection, ref bool isJudged, ref int touchId)
		{
			float lane = ConvertLaneBy(ref note, ref activeInput);
			bool isInputedNormalNote = InputedNormalNoteTouch.ContainsKey(touchId);
			bool isNoteJudgment = note.IsJudgment(ref activeInput.Touch, lane);

			if (isInputedNormalNote)
			{
				if (isNoteJudgment &&
					(note.CalcNoteResult(activeInput.Touch.musicTime) > NoteResult.Great ||
					 activeInput.Touch.musicTime - InputedNormalNoteTouch[touchId] > LiveUtility.NoteEndIgnoreFrameTime))
				{
					activeInput.CandidateNotes.Add(judgmentNote);
				}
				return;
			}

			if (isNoteJudgment)
			{
				activeInput.CandidateNotes.Add(judgmentNote);
				isConnection = false;
				return;
			}

			note = judgmentNote.NoteList[0];
			if ((note is FrictionLongNote || note is FrictionHideLongNote || note is LongNote) &&
				note.IsJudgment(ref activeInput.Touch, ConvertLaneBy(ref note, ref activeInput)))
			{
				isConnection = true;
				isJudged = true;
			}
		}

		private void AddCheckSingleInputedNormalNoteTouch(ref NoteBase judgmentNote, ref NoteBase note, ref InputTmp activeInput, ref bool isConnection, ref bool isJudged, ref int touchId)
		{
			if (judgmentNote is FrictionNote && judgmentNote.IsJudgment(ref activeInput.Touch, ConvertLaneBy(ref note, ref activeInput)))
			{
				isConnection = true;
				isJudged = true;
				return;
			}

			if ((note is FrictionLongNote || note is FrictionHideLongNote) && note.IsJudgment(ref activeInput.Touch, ConvertLaneBy(ref note, ref activeInput)))
			{
				isConnection = true;
				isJudged = true;
				return;
			}

			activeInput.CandidateNotes.Add(judgmentNote);
			isConnection = false;
			InputedNormalNoteTouch[touchId] = activeInput.Touch.musicTime;
		}

		private float ConvertLaneBy(ref NoteBase note, ref InputTmp input)
		{
			if ((note.Category != NoteCategory.Flick && note.Category != NoteCategory.FrictionFlick) || note.State != NoteState.Playing)
			{
				return input.Lane;
			}
			return GetJudgmentLane(input.Touch.worldPosition, true);
		}

		private float GetJudgmentLane(Vector3 touchPosition, bool checkJudgmentY)
		{
			Vector2[] judgmentPositions = LiveConfig.JudgmentPositions;
			if (judgmentPositions == null || judgmentPositions.Length == 0)
			{
				return -1f;
			}

			float offsetX = liveBundleBuildData != null ? liveBundleBuildData.JudgmentOffsetX : 1.8f;
			float offsetY = liveBundleBuildData != null ? liveBundleBuildData.JudgmentOffsetY : 2.5f;
			if (checkJudgmentY && offsetY < touchPosition.y - judgmentPositions[0].y)
			{
				return -1f;
			}
			if (offsetX < judgmentPositions[0].x - touchPosition.x)
			{
				return -1f;
			}
			if (offsetX < touchPosition.x - judgmentPositions[judgmentPositions.Length - 1].x)
			{
				return -1f;
			}

			for (int i = 0; i < judgmentPositions.Length; i++)
			{
				if (touchPosition.x - judgmentPositions[i].x <= 0f)
				{
					if (i == 0)
					{
						return 0f;
					}
					return (touchPosition.x - judgmentPositions[i - 1].x) / (judgmentPositions[i].x - judgmentPositions[i - 1].x) + i - 1;
				}
			}
			return judgmentPositions.Length - 1;
		}

		private static bool IsEndedOrCanceled(InputTouchPhase phase)
		{
			return phase == InputTouchPhase.Ended || phase == InputTouchPhase.Canceled;
		}

		private static bool IsValidScreenPosition(Vector2 screenPosition)
		{
			return IsFinite(screenPosition.x) && IsFinite(screenPosition.y);
		}

		private static bool IsFinite(float value)
		{
			return !float.IsNaN(value) && !float.IsInfinity(value);
		}

		private static NoteBase SelectComparableNote(NoteBase note, InputTmp input)
		{
			if (note.NoteList.Count == 1)
			{
				return note;
			}
			if (note.Result == NoteResult.None && input.Touch.phase == InputTouchPhase.Began)
			{
				return note.NoteList[0];
			}
			return note.NoteList[note.NoteList.Count - 1];
		}

		private void ForEachLiveView(Action<LiveViewBase> action)
		{
			if (liveViews == null || action == null)
			{
				return;
			}

			for (int i = 0; i < liveViews.Length; i++)
			{
				if (liveViews[i] != null)
				{
					action(liveViews[i]);
				}
			}
		}
	}
}
