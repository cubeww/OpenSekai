using System;
using System.Collections.Generic;
using System.Linq;
using Sekai.Live;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using InputTouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Sekai.Core.Live
{
	public class LiveLogic
	{
		private const int InputTmpCount = 128;
		private const int MaxFingerCount = 10;
		private const float TargetFrameTime = 1f / 60f;
		private const int MouseFallbackTouchId = -1;
		private const int MouseFallbackFingerId = 0;

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
				CandidateNotes = new List<NoteBase>(4);
			}

			public InputTmp(InputTmp source)
				: this()
			{
				if (source == null)
				{
					return;
				}

				Touch = source.Touch;
				Lane = source.Lane;
				CandidateNotes.AddRange(source.CandidateNotes);
			}

			public void Reset(ref LiveTouch touch, float lane)
			{
				Touch = touch;
				Lane = lane;
				CandidateNotes.Clear();
			}
		}

		[Serializable]
		public class InputLogInfo
		{
			public List<LiveTouch> buffer;

			public InputLogInfo(LiveTouch[] buffer)
			{
				this.buffer = buffer != null ? new List<LiveTouch>(buffer) : new List<LiveTouch>();
			}
		}

		private readonly LiveBundleBuildData liveBundleBuildData;
		private LiveBootDataBase bootData;
		private LiveViewBase[] liveViews;
		private MusicScore musicScore;
		private NoteBase[] noteArray = Array.Empty<NoteBase>();
		private NoteBase[] highSpeedNoteArray = Array.Empty<NoteBase>();
		private EventBase[] eventArray = Array.Empty<EventBase>();
		private int eventStartIndex;
		private readonly Dictionary<int, (double time, InputTouchPhase phase)> lastTouches;
		private readonly List<NoteBase> judgmentNoteList;
		private readonly InputTmp[] inputTmpArray;
		private readonly List<InputTmp> activeInputList;
		private Camera calcCamera;
		private InputLogInfo recordedInputLogger;
		private readonly List<NoteBase> connectionNoteList;
		private readonly Dictionary<int, (double time, Vector2 position)> touchPositionList;
		private readonly List<int> usedFingerList;
		private readonly Dictionary<int, double> InputedNormalNoteTouch;
		private float noteDisplayTimeOffset;
		private float seBaseVolume = 1f;
		private float seScoreVolume = 1f;

		public event Action OnFinished;
		public event Action OnFailure;

		public ScoreLogic scoreLogic;
		public SkillLogic skillLogic = new NullSkillLogic();
		public int noteStartIndex;
		public int highSpeedNoteStartIndex;
		public int result;
		public int TapCount { get; set; }
		public double currentGameTime;
		public MusicScoreInfo currentFrameInfo;

		public LiveScore Score => scoreLogic?.score ?? default;

		public bool IsNotesAllFinished => noteStartIndex >= noteArray.Length && highSpeedNoteStartIndex >= highSpeedNoteArray.Length;

		public bool IsPerfectCombo => scoreLogic?.IsPerfectCombo ?? false;

		public bool IsAllPerfectCombo => scoreLogic?.IsAllPerfectCombo ?? false;

		public LiveLogic(LiveBundleBuildData data)
		{
			NativeInput.Enable();
			int fingerCapacity = Mathf.Max(NativeInput.Fingers.Count, MaxFingerCount);
			lastTouches = new Dictionary<int, (double time, InputTouchPhase phase)>(fingerCapacity);
			judgmentNoteList = new List<NoteBase>();
			inputTmpArray = new InputTmp[InputTmpCount];
			for (int i = 0; i < inputTmpArray.Length; i++)
			{
				inputTmpArray[i] = new InputTmp();
			}
			activeInputList = new List<InputTmp>(InputTmpCount);
			connectionNoteList = new List<NoteBase>(5);
			touchPositionList = new Dictionary<int, (double time, Vector2 position)>(MaxFingerCount);
			usedFingerList = new List<int>(MaxFingerCount);
			InputedNormalNoteTouch = new Dictionary<int, double>(MaxFingerCount);
			seScoreVolume = 1f;
			liveBundleBuildData = data;
		}

		public void Setup(LiveBootDataBase bootData, LiveViewBase[] liveViews)
		{
			this.bootData = bootData;
			this.liveViews = liveViews;
			seBaseVolume = ApplicationLocalSettings.LoadFromStorage().LiveVolume?.Se ?? 1f;
			noteDisplayTimeOffset = LiveConfig.GetNoteDisplayOffsetTime(bootData?.LiveSettingData?.NoteSpeed ?? 6f);

			musicScore = bootData?.MusicData?.MusicScore ?? new MusicScore();
			if (bootData?.MusicData != null)
			{
				bootData.MusicData.MusicScore = musicScore;
			}

			musicScore.InjectSkillFeverForCreatorScore(bootData?.MusicData?.Vocal?.musicId ?? bootData?.MusicData?.Music?.id ?? 0);
			noteArray = SortNotes(musicScore.NoteArray);
			highSpeedNoteArray = noteArray.Where(note => note != null && !Mathf.Approximately(note.speedRatio, 1f)).ToArray();
			eventArray = SortEvents(musicScore.EventArray);
			BindNoteCallbacks(noteArray);
			BindEventCallbacks(eventArray);
			LiveViewExt.CreateNotePool(liveViews, LiveUtility.CalculateMaxSimultaneousNotes(musicScore, noteDisplayTimeOffset));
			calcCamera = CameraUtility.GetFrontCamera();
			if (calcCamera == null)
			{
				calcCamera = Camera.main ?? UnityEngine.Object.FindObjectOfType<Camera>();
			}
			if (result != 0)
			{
				result = 0;
			}
			RefreshInput();
		}

		public void SetSkillLogic(SkillLogic logic)
		{
			skillLogic = logic ?? new NullSkillLogic();
			skillLogic.Setup(bootData, scoreLogic?.score ?? default);
		}

		public void SetScoreLogic(ScoreLogic logic)
		{
			scoreLogic = logic;
			scoreLogic?.Setup(bootData, musicScore);
			LiveViewExt.SetupScore(liveViews, scoreLogic?.score ?? default);
		}

		public void RefreshInput()
		{
			NativeInput.Enable();
			lastTouches.Clear();
			activeInputList.Clear();
			touchPositionList.Clear();
			InputedNormalNoteTouch.Clear();
			noteStartIndex = 0;
			highSpeedNoteStartIndex = 0;
			eventStartIndex = 0;
			result = 0;
			TapCount = 0;
			musicScore?.ResetNote();
			musicScore?.ResetEvent();

			var activeTouches = NativeInput.ActiveTouches;
			for (int i = 0; i < activeTouches.Count; i++)
			{
				EnhancedTouch touch = activeTouches[i];
				lastTouches[touch.touchId] = (touch.time, touch.phase);
			}
		}

		public void OnUpdate(float scoreInfoTime, double currentGameTime)
		{
			if (result > 2)
			{
				return;
			}

			musicScore?.Update(scoreInfoTime);
			currentFrameInfo = MusicScore.CurrentFrameInfo;
			this.currentGameTime = currentGameTime;

			UpdateSeVolume();
			InitializeUpdate();
			UpdateEvent();
			UpdateSkill();
			UpdateNote();

			if (scoreLogic.HasValue() && scoreLogic.score.life <= 0)
			{
				Result = 2;
			}

			if (IsNotesAllFinished && result == 0)
			{
				result = 3;
				OnFinished?.Invoke();
			}
		}

		public int Result
		{
			get => result;
			set
			{
				if (result == value)
				{
					return;
				}

				result = value;
				if (value == 2)
				{
					OnFailure?.Invoke();
				}
				if (value >= 3)
				{
					OnFinished?.Invoke();
				}
			}
		}

		public void OnAutoInput()
		{
			int index = noteStartIndex;
			while (index < noteArray.Length)
			{
				NoteBase note = noteArray[index];
				if (note == null || note.State == NoteState.Done)
				{
					index++;
					continue;
				}
				if (note.MusicScoreInfo.time > currentFrameInfo.time)
				{
					return;
				}
				note.AutoJudgment(currentFrameInfo);
				index++;
			}
		}

		public void OnInput()
		{
			int activeInputCount = 0;
			activeInputList.Clear();
			EnhancedTouch[] activeTouches = NativeInput.ActiveTouches.ToArray();

			for (int i = 0; i < activeTouches.Length; i++)
			{
				EnhancedTouch touch = activeTouches[i];
				if (ShouldProcessTouch(touch))
				{
					TouchExecution(ref touch, ref activeInputCount);
					UpdateLastTouch(ref touch);
				}
			}

			for (int i = 0; i < activeTouches.Length; i++)
			{
				EnhancedTouch touch = activeTouches[i];
				UpdateTouchPositionCache(ref touch);
			}

#if UNITY_EDITOR || UNITY_STANDALONE
			ProcessMouseFallback(ref activeInputCount);
#endif
			Judgment();
		}

		public void Continue()
		{
			Result = 0;
		}

		private void InitializeUpdate()
		{
			judgmentNoteList.Clear();
		}

		private void UpdateEvent()
		{
			while (eventStartIndex < eventArray.Length)
			{
				EventBase eventBase = eventArray[eventStartIndex];
				if (eventBase == null || eventBase.State == EventState.Done)
				{
					eventStartIndex++;
					continue;
				}

				if (eventBase.MusicScoreInfo.time > currentFrameInfo.time)
				{
					return;
				}

				eventBase.Update(currentFrameInfo, ExcuteEvent);
				if (eventBase.State == EventState.Done)
				{
					eventStartIndex++;
				}
			}
		}

		private void UpdateSkill()
		{
			skillLogic?.Update(currentGameTime);
		}

		private void UpdateNote()
		{
			for (int i = noteStartIndex; i < noteArray.Length; i++)
			{
				NoteBase note = noteArray[i];
				if (note == null)
				{
					if (i == noteStartIndex)
					{
						noteStartIndex++;
					}
					continue;
				}

				if (note.State == NoteState.Done)
				{
					if (i == noteStartIndex)
					{
						noteStartIndex++;
					}
					continue;
				}

				if (!Mathf.Approximately(note.speedRatio, 1f))
				{
					if (i == noteStartIndex)
					{
						noteStartIndex++;
					}
					continue;
				}

				float offsetTime = CalcTimeOffset(note);
				if (IsJudgeTimingTimeOffsetHighSpeedNote(offsetTime, note))
				{
					break;
				}
				if (offsetTime < 0f && currentFrameInfo.time >= note.MusicScoreInfo.time)
				{
					offsetTime = -offsetTime;
				}
				note.Excute(currentFrameInfo, offsetTime);
				if (note.State == NoteState.Done && i == noteStartIndex)
				{
					noteStartIndex++;
				}
				else if (note.State != NoteState.Done)
				{
					UpdateJudgmentNoteArray(note);
				}
			}

			for (int i = highSpeedNoteStartIndex; i < highSpeedNoteArray.Length; i++)
			{
				NoteBase note = highSpeedNoteArray[i];
				if (note == null)
				{
					if (i == highSpeedNoteStartIndex)
					{
						highSpeedNoteStartIndex++;
					}
					continue;
				}

				if (note.State == NoteState.Done)
				{
					if (i == highSpeedNoteStartIndex)
					{
						highSpeedNoteStartIndex++;
					}
					continue;
				}

				float offsetTime = currentFrameInfo.time >= note.MusicScoreInfo.time
					? noteDisplayTimeOffset
					: CalcTimeOffset(note);
				if (IsJudgeTimingTimeOffsetNote(offsetTime, note))
				{
					break;
				}

				note.Excute(currentFrameInfo, offsetTime);
				if (note.State == NoteState.Done && i == highSpeedNoteStartIndex)
				{
					highSpeedNoteStartIndex++;
				}
				else if (note.State != NoteState.Done)
				{
					UpdateJudgmentNoteArray(note);
				}
			}
		}

		private void AdvanceFinishedNotes()
		{
			while (noteStartIndex < noteArray.Length)
			{
				NoteBase note = noteArray[noteStartIndex];
				if (note != null && note.State != NoteState.Done)
				{
					return;
				}
				noteStartIndex++;
			}
		}

		private bool IsJudgeTimingTimeOffsetHighSpeedNote(float timeOffset, NoteBase note)
		{
			return note != null && note.State != NoteState.Playing && IsJudgeTimingTimeOffsetNote(timeOffset, note);
		}

		private bool IsJudgeTimingTimeOffsetNote(float timeOffset, NoteBase note)
		{
			if (note == null || Mathf.Approximately(timeOffset, 0f))
			{
				return false;
			}

			return currentFrameInfo.time + timeOffset + TargetFrameTime <= note.MusicScoreInfo.time;
		}

		private void UpdateJudgmentNoteArray(NoteBase note)
		{
			if (note != null && note.HasJudgment)
			{
				judgmentNoteList.Add(note);
			}
		}

		private bool ShouldProcessTouch(EnhancedTouch touch)
		{
			if (!lastTouches.TryGetValue(touch.touchId, out var lastTouch))
			{
				return true;
			}

			if (touch.time < lastTouch.time)
			{
				return false;
			}

			return lastTouch.phase != InputTouchPhase.Ended;
		}

		private void UpdateLastTouch(ref EnhancedTouch touch)
		{
			lastTouches[touch.touchId] = (touch.time, touch.phase);
		}

		private void TouchExecution(ref EnhancedTouch touch, ref int activeInputCount)
		{
			int touchId = touch.touchId;
			InputTouchPhase phase = touch.phase;
			double time = touch.time;
			if (lastTouches.TryGetValue(touchId, out var lastTouch)
				&& time.Equals(lastTouch.time))
			{
				if (IsEndedOrCanceled(phase) || phase == InputTouchPhase.None)
				{
					return;
				}

				if (phase == InputTouchPhase.Began || phase == InputTouchPhase.Stationary)
				{
					phase = InputTouchPhase.Moved;
				}
			}

			float musicTime = (float)((time - currentGameTime) * TargetFrameTime + currentFrameInfo.time);
			CreateLiveTouch(touchId, musicTime, ref touch, out LiveTouch liveTouch, phase);
			OnProcessTouch(ref liveTouch, ref activeInputCount);
		}

		private void CreateLiveTouch(int touchId, float musicTime, ref EnhancedTouch processTouch, out LiveTouch liveTouch, InputTouchPhase phase)
		{
			Vector2 screenPosition = processTouch.screenPosition;
			Vector3 screenPoint = new Vector3(screenPosition.x, screenPosition.y, 0f);
			Vector3 worldPosition = calcCamera != null
				? calcCamera.ScreenToWorldPoint(screenPoint)
				: new Vector3(screenPosition.x, screenPosition.y, 0f);
			Vector2 delta = LiveUtility.Vector2Zero;

			if (processTouch.phase == InputTouchPhase.Began)
			{
				touchPositionList[touchId] = (processTouch.time, screenPosition);
			}

			if (touchPositionList.TryGetValue(touchId, out var cachedPosition))
			{
				if (processTouch.time >= cachedPosition.time)
				{
					delta = screenPosition - cachedPosition.position;
				}
			}
			else
			{
				delta = screenPosition;
			}

			int fingerId = processTouch.finger != null ? processTouch.finger.index : touchId;
			liveTouch = new LiveTouch(fingerId, touchId, delta, worldPosition, phase, musicTime);
		}

		private void OnProcessTouch(ref LiveTouch processTouch, ref int activeInputCount)
		{
			float judgmentLane = GetJudgmentLane(processTouch.worldPosition, true);
			if (activeInputCount <= InputTmpCount - 1)
			{
				InputTmp input = inputTmpArray[activeInputCount++];
				input.Reset(ref processTouch, judgmentLane);
				activeInputList.Add(input);
			}

			if (processTouch.phase == InputTouchPhase.Began)
			{
				TapCount++;
			}
		}

#if UNITY_EDITOR || UNITY_STANDALONE
		private void ProcessMouseFallback(ref int activeInputCount)
		{
			Mouse mouse = Mouse.current;
			if (mouse == null)
			{
				return;
			}

			InputTouchPhase phase;
			if (mouse.leftButton.wasPressedThisFrame)
			{
				phase = InputTouchPhase.Began;
			}
			else if (mouse.leftButton.wasReleasedThisFrame)
			{
				phase = InputTouchPhase.Ended;
			}
			else if (mouse.leftButton.isPressed)
			{
				Vector2 mouseDelta = mouse.delta.ReadValue();
				phase = mouseDelta.sqrMagnitude > 0f ? InputTouchPhase.Moved : InputTouchPhase.Stationary;
			}
			else
			{
				return;
			}

			Vector2 screenPosition = mouse.position.ReadValue();
			double time = Time.realtimeSinceStartupAsDouble;
			Vector2 delta = mouse.delta.ReadValue();
			if (phase == InputTouchPhase.Began)
			{
				touchPositionList[MouseFallbackTouchId] = (time, screenPosition);
				delta = LiveUtility.Vector2Zero;
			}
			else if (touchPositionList.TryGetValue(MouseFallbackTouchId, out var cachedPosition))
			{
				delta = screenPosition - cachedPosition.position;
			}

			Vector3 screenPoint = new Vector3(screenPosition.x, screenPosition.y, 0f);
			Vector3 worldPosition = calcCamera != null
				? calcCamera.ScreenToWorldPoint(screenPoint)
				: new Vector3(screenPosition.x, screenPosition.y, 0f);
			float musicTime = (float)((time - currentGameTime) * TargetFrameTime + currentFrameInfo.time);
			var liveTouch = new LiveTouch(MouseFallbackFingerId, MouseFallbackTouchId, delta, worldPosition, phase, musicTime);
			OnProcessTouch(ref liveTouch, ref activeInputCount);

			if (IsEndedOrCanceled(phase))
			{
				touchPositionList.Remove(MouseFallbackTouchId);
				lastTouches.Remove(MouseFallbackTouchId);
			}
			else
			{
				touchPositionList[MouseFallbackTouchId] = (time, screenPosition);
				lastTouches[MouseFallbackTouchId] = (time, phase);
			}
		}
#endif

		private void UpdateTouchPositionCache(ref EnhancedTouch touch)
		{
			int touchId = touch.touchId;
			if (touchPositionList.ContainsKey(touchId))
			{
				if (IsEndedOrCanceled(touch.phase))
				{
					touchPositionList.Remove(touchId);
					return;
				}

				if (touch.time > touchPositionList[touchId].time)
				{
					touchPositionList[touchId] = (touch.time, touch.screenPosition);
				}
				return;
			}

			touchPositionList.Add(touchId, (touch.time, touch.screenPosition));
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
			for (int noteIndex = 0; noteIndex < judgmentNoteList.Count; noteIndex++)
			{
				NoteBase judgmentNote = judgmentNoteList[noteIndex];
				if (judgmentNote == null)
				{
					continue;
				}

				bool isConnection = true;
				bool isJudged = false;
				for (int inputIndex = activeInputList.Count - 1; inputIndex >= 0; inputIndex--)
				{
					InputTmp activeInput = activeInputList[inputIndex];
					NoteBase note = judgmentNote;
					float lane = ConvertLaneBy(ref note, ref activeInput);
					if (lane >= 0f && note.IsJudgment(ref activeInput.Touch, lane))
					{
						isJudged = true;
						int touchId = activeInput.Touch.touchId;
						NoteBase lastNote = GetLastNote(judgmentNote);
						if (judgmentNote.NoteList != null && judgmentNote.NoteList.Count == 1)
						{
							AddCheckSingleInputedNormalNoteTouch(ref judgmentNote, ref lastNote, ref activeInput, ref isConnection, ref isJudged, ref touchId);
						}
						else
						{
							AddCheckLongInputedNormalNoteTouch(ref judgmentNote, ref lastNote, ref activeInput, ref isConnection, ref isJudged, ref touchId);
						}
						continue;
					}

					NoteBase firstNote = GetFirstNote(judgmentNote);
					if (firstNote != null && TryAddConnectionCandidate(firstNote, activeInput))
					{
						isConnection = true;
						isJudged = true;
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
			for (int inputIndex = activeInputList.Count - 1; inputIndex >= 0; inputIndex--)
			{
				InputTmp input = activeInputList[inputIndex];
				if (input.CandidateNotes.Count <= 1)
				{
					continue;
				}

				input.CandidateNotes.Sort((a, b) =>
				{
					if (ReferenceEquals(a, b))
					{
						return 0;
					}

					int resultCompare = a.CalcNoteResult(input.Touch.musicTime).CompareTo(b.CalcNoteResult(input.Touch.musicTime));
					if (resultCompare != 0)
					{
						return resultCompare;
					}

					int timeCompare = a.MusicScoreInfo.time.CompareTo(b.MusicScoreInfo.time);
					if (timeCompare != 0)
					{
						return timeCompare;
					}

					float aDistance = a.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref a, ref input));
					float bDistance = b.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref b, ref input));
					return aDistance.CompareTo(bDistance);
				});

				for (int i = input.CandidateNotes.Count - 1; i > 0; i--)
				{
					NoteBase current = input.CandidateNotes[i];
					NoteBase previous = input.CandidateNotes[i - 1];
					if (current == null || previous == null || !IsSameJudgmentTime(current, previous))
					{
						continue;
					}

					float currentDistance = current.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref current, ref input));
					float previousDistance = previous.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref previous, ref input));
					if (previousDistance <= currentDistance)
					{
						input.CandidateNotes.RemoveAt(i);
					}
					else
					{
						input.CandidateNotes.RemoveAt(i - 1);
					}
				}
			}

			ResolveOverlappingCandidatesAcrossInputs();
		}

		private void ResolveOverlappingCandidatesAcrossInputs()
		{
			for (int inputIndex = activeInputList.Count - 1; inputIndex >= 0; inputIndex--)
			{
				InputTmp input = activeInputList[inputIndex];
				if (input.CandidateNotes.Count == 0)
				{
					continue;
				}

				for (int otherInputIndex = inputIndex - 1; otherInputIndex >= 0; otherInputIndex--)
				{
					InputTmp otherInput = activeInputList[otherInputIndex];
					if (otherInput.CandidateNotes.Count == 0)
					{
						continue;
					}

					for (int candidateIndex = input.CandidateNotes.Count - 1; candidateIndex >= 0; candidateIndex--)
					{
						NoteBase candidate = input.CandidateNotes[candidateIndex];
						int otherCandidateIndex = otherInput.CandidateNotes.IndexOf(candidate);
						if (candidate == null || otherCandidateIndex < 0)
						{
							continue;
						}

						NoteBase inputNote = ResolveJudgmentNote(candidate, input);
						NoteBase otherNote = ResolveJudgmentNote(candidate, otherInput);
						float inputDistance = inputNote != null
							? inputNote.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref inputNote, ref input))
							: float.MaxValue;
						float otherDistance = otherNote != null
							? otherNote.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref otherNote, ref otherInput))
							: float.MaxValue;

						if (otherDistance <= inputDistance)
						{
							input.CandidateNotes.RemoveAt(candidateIndex);
						}
						else
						{
							otherInput.CandidateNotes.RemoveAt(otherCandidateIndex);
						}
					}
				}
			}
		}

		private void LeaveNotesFastGeneration()
		{
			for (int inputIndex = activeInputList.Count - 1; inputIndex >= 0; inputIndex--)
			{
				InputTmp input = activeInputList[inputIndex];
				if (input.CandidateNotes.Count < 2)
				{
					continue;
				}

				for (int i = input.CandidateNotes.Count - 1; i > 0; i--)
				{
					NoteBase first = ResolveJudgmentNote(input.CandidateNotes[0], input);
					NoteBase current = ResolveJudgmentNote(input.CandidateNotes[i], input);
					if (first == null || current == null)
					{
						continue;
					}

					if (IsSameJudgmentTime(first, current))
					{
						float firstDistance = first.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref first, ref input));
						float currentDistance = current.LaneDistance(ref currentFrameInfo, ConvertLaneBy(ref current, ref input));
						if (firstDistance <= currentDistance)
						{
							input.CandidateNotes.RemoveAt(i);
						}
						else
						{
							input.CandidateNotes.RemoveAt(0);
						}
						continue;
					}

					if (current.CalcNoteResult(input.Touch.musicTime) <= NoteResult.Good
						&& first.CalcNoteResult(input.Touch.musicTime) > NoteResult.Good)
					{
						input.CandidateNotes.RemoveAt(0);
					}
					else if (first.CalcNoteResult(input.Touch.musicTime) <= NoteResult.Good
						&& current.CalcNoteResult(input.Touch.musicTime) > NoteResult.Good)
					{
						input.CandidateNotes.RemoveAt(i);
					}
					else if (first.MusicScoreInfo.time <= current.MusicScoreInfo.time)
					{
						input.CandidateNotes.RemoveAt(i);
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
			for (int inputIndex = activeInputList.Count - 1; inputIndex >= 0; inputIndex--)
			{
				InputTmp input = activeInputList[inputIndex];
				for (int noteIndex = connectionNoteList.Count - 1; noteIndex >= 0; noteIndex--)
				{
					NoteBase note = connectionNoteList[noteIndex];
					float lane = ConvertLaneBy(ref note, ref input);
					if (lane >= 0f && note != null && note.IsJudgment(ref input.Touch, lane))
					{
						input.CandidateNotes.Add(note);
						connectionNoteList.RemoveAt(noteIndex);
					}
				}
			}
		}

		private void EvaluateJudgment()
		{
			usedFingerList.Clear();
			for (int inputIndex = 0; inputIndex < activeInputList.Count; inputIndex++)
			{
				InputTmp input = activeInputList[inputIndex];
				for (int candidateIndex = 0; candidateIndex < input.CandidateNotes.Count; candidateIndex++)
				{
					NoteBase note = input.CandidateNotes[candidateIndex];
					float lane = ConvertLaneBy(ref note, ref input);
					if (note == null)
					{
						continue;
					}

					note.Judgment(ref input.Touch, lane);
					if (note.Result == NoteResult.None || note.Result == NoteResult.Bad)
					{
						InputedNormalNoteTouch[input.Touch.touchId] = input.Touch.musicTime;
					}

					judgmentNoteList.Remove(note);
					input.CandidateNotes.Clear();
					usedFingerList.Add(input.Touch.fingerId);
					break;
				}
			}

			for (int inputIndex = 0; inputIndex < activeInputList.Count; inputIndex++)
			{
				InputTmp input = activeInputList[inputIndex];
				bool used = false;
				for (int i = usedFingerList.Count - 1; i >= 0; i--)
				{
					used |= usedFingerList[i] == input.Touch.fingerId;
				}

				if (!used && input.Lane >= 0f)
				{
					LiveViewExt.Unpicked(liveViews, Mathf.RoundToInt(input.Lane), ref input.Touch);
				}

				if (IsEndedOrCanceled(input.Touch.phase))
				{
					InputedNormalNoteTouch.Remove(input.Touch.touchId);
				}
			}

			activeInputList.Clear();
		}

		private void AddCheckSingleInputedNormalNoteTouch(ref NoteBase judgmentNote, ref NoteBase note, ref InputTmp activeInput, ref bool isConnection, ref bool isJudged, ref int touchId)
		{
			if (judgmentNote is FrictionNote)
			{
				float lane = ConvertLaneBy(ref note, ref activeInput);
				if (judgmentNote.IsJudgment(ref activeInput.Touch, lane))
				{
					isConnection = true;
					isJudged = true;
					return;
				}
			}

			if (note is FrictionLongNote or FrictionHideLongNote)
			{
				float lane = ConvertLaneBy(ref note, ref activeInput);
				if (note.IsJudgment(ref activeInput.Touch, lane))
				{
					isConnection = true;
					isJudged = true;
					return;
				}
			}

			activeInput.CandidateNotes.Add(judgmentNote);
			isConnection = false;
			InputedNormalNoteTouch[touchId] = activeInput.Touch.musicTime;
		}

		private void AddCheckLongInputedNormalNoteTouch(ref NoteBase judgmentNote, ref NoteBase note, ref InputTmp activeInput, ref bool isConnection, ref bool isJudged, ref int touchId)
		{
			float lane = ConvertLaneBy(ref note, ref activeInput);
			bool hadInput = InputedNormalNoteTouch.ContainsKey(touchId);
			bool isLastNoteJudgment = note != null && note.IsJudgment(ref activeInput.Touch, lane);
			if (hadInput)
			{
				if (isLastNoteJudgment)
				{
					isConnection = false;
					if (note.CalcNoteResult(activeInput.Touch.musicTime) > NoteResult.Great
						|| activeInput.Touch.musicTime - InputedNormalNoteTouch[touchId] > 1f / 6f)
					{
						activeInput.CandidateNotes.Add(judgmentNote);
						return;
					}
				}
			}
			else if (isLastNoteJudgment)
			{
				activeInput.CandidateNotes.Add(judgmentNote);
				isConnection = false;
				return;
			}

			note = GetFirstNote(judgmentNote);
			lane = ConvertLaneBy(ref note, ref activeInput);
			if ((note is FrictionLongNote or FrictionHideLongNote)
				&& note.IsJudgment(ref activeInput.Touch, lane))
			{
				isConnection = true;
				isJudged = true;
				return;
			}

			if (note != null && note.Result == NoteResult.None)
			{
				lane = ConvertLaneBy(ref note, ref activeInput);
				if (note.IsJudgment(ref activeInput.Touch, lane))
				{
					activeInput.CandidateNotes.Add(judgmentNote);
					isConnection = false;
					return;
				}
			}

			if (note is LongNote)
			{
				lane = ConvertLaneBy(ref note, ref activeInput);
				if (note.IsJudgment(ref activeInput.Touch, lane))
				{
					isConnection = true;
					isJudged = true;
				}
			}
		}

		private float ConvertLaneBy(ref NoteBase note, ref InputTmp input)
		{
			if (note == null || input == null)
			{
				return input?.Lane ?? -1f;
			}

			if ((note.Category == NoteCategory.Flick || note.Category == NoteCategory.FrictionFlick)
				&& note.Direction == NoteDirection.Left)
			{
				return GetJudgmentLane(input.Touch.worldPosition, !note.FinishFirstAction);
			}

			return input.Lane;
		}

		private float GetJudgmentLane(Vector3 touchPosition, bool checkJudgmentY)
		{
			Vector2[] positions = LiveConfig.JudgmentPositions;
			if (positions == null || positions.Length == 0)
			{
				return -1f;
			}

			float judgmentOffsetX = liveBundleBuildData?.JudgmentOffsetX ?? 0f;
			float judgmentOffsetY = liveBundleBuildData?.JudgmentOffsetY ?? 0f;
			if (checkJudgmentY && touchPosition.y - positions[0].y > judgmentOffsetY)
			{
				return -1f;
			}

			if (positions[0].x - touchPosition.x > judgmentOffsetX)
			{
				return -1f;
			}

			int lastIndex = positions.Length - 1;
			if (touchPosition.x - positions[lastIndex].x > judgmentOffsetX)
			{
				return -1f;
			}

			for (int i = 0; i < positions.Length; i++)
			{
				if (touchPosition.x - positions[i].x <= 0f)
				{
					if (i == 0)
					{
						return 0f;
					}

					float previousX = positions[i - 1].x;
					float currentX = positions[i].x;
					return (touchPosition.x - previousX) / (currentX - previousX) + i - 1;
				}
			}

			return lastIndex;
		}

		private bool TryAddConnectionCandidate(NoteBase note, InputTmp input)
		{
			if (note is not LongNote and not FrictionLongNote and not FrictionHideLongNote)
			{
				return false;
			}

			float lane = ConvertLaneBy(ref note, ref input);
			if (lane < 0f || !note.IsJudgment(ref input.Touch, lane))
			{
				return false;
			}

			input.CandidateNotes.Add(note);
			return true;
		}

		private NoteBase ResolveJudgmentNote(NoteBase note, InputTmp input)
		{
			if (note?.NoteList == null || note.NoteList.Count == 1)
			{
				return note;
			}

			if (note.Result != NoteResult.None || input.Touch.phase != InputTouchPhase.Began)
			{
				return GetLastNote(note);
			}

			return GetFirstNote(note);
		}

		private static NoteBase GetFirstNote(NoteBase note)
		{
			return note?.NoteList != null && note.NoteList.Count > 0 ? note.NoteList[0] : note;
		}

		private static NoteBase GetLastNote(NoteBase note)
		{
			return note?.NoteList != null && note.NoteList.Count > 0 ? note.NoteList[note.NoteList.Count - 1] : note;
		}

		private static bool IsSameJudgmentTime(NoteBase a, NoteBase b)
		{
			return a != null && b != null && Mathf.Approximately(a.MusicScoreInfo.time, b.MusicScoreInfo.time);
		}

		private static bool IsEndedOrCanceled(InputTouchPhase phase)
		{
			return phase == InputTouchPhase.Ended || phase == InputTouchPhase.Canceled;
		}

		private void ExcuteEvent(EventBase eventBase)
		{
			scoreLogic?.ExcuteEvent(eventBase);
			skillLogic?.ExcuteEvent(eventBase);
		}

		private void OnSpawnNote(NoteBase note)
		{
			LiveViewExt.SpawnNote(liveViews, note);
		}

		private void OnUnSpawnNote(NoteBase note)
		{
			LiveViewExt.UnspawnNote(liveViews, note);
		}

		private void OnJudgmentNote(NoteBase note)
		{
			LiveViewExt.JudgmentNote(liveViews, note);
		}

		private void OnUpdateCombo(NoteBase note)
		{
			scoreLogic?.UpdateCombo(note);
			LiveViewExt.UpdateCombo(liveViews, scoreLogic?.score ?? default);
		}

		private void OnUpdateScore(NoteBase note)
		{
			int addScore = scoreLogic?.CalculateAddScore(note, skillLogic?.ScoreFactor(note) ?? 1f) ?? 0;
			LiveScore score = scoreLogic?.score ?? default;
			LiveViewExt.UpdateScore(liveViews, ref score, addScore);
		}

		private void OnDamage(NoteBase note)
		{
			scoreLogic?.Damage(note);
			LiveViewExt.UpdateLife(liveViews, scoreLogic?.score ?? default);
		}

		private (NoteResult result, NoteResultDescription description) OnUpdateResult((NoteResult result, NoteResultDescription description) result)
		{
			return skillLogic?.UpdateResult(result) ?? result;
		}

		private float CalcTimeOffset(NoteBase note)
		{
			if (note == null)
			{
				return 0f;
			}

			float currentProgress = currentFrameInfo.bar + currentFrameInfo.barProgress;
			float noteProgress = note.MusicScoreInfo.bar + note.MusicScoreInfo.barProgress;
			float speedRatio = CalcNoteSpeedRatio(currentProgress, noteProgress);
			if (Mathf.Approximately(speedRatio, 0f))
			{
				return 0f;
			}

			return noteDisplayTimeOffset / speedRatio / note.speedRatio;
		}

		private float CalcNoteSpeedRatio(float currentProgress, float noteProgress)
		{
			if (noteProgress <= currentProgress)
			{
				return currentFrameInfo.speedRatio;
			}

			MusicScoreInfo[] scoreInfos = musicScore?.musicScoreInfoArray;
			if (scoreInfos == null || scoreInfos.Length == 0)
			{
				return currentFrameInfo.speedRatio;
			}

			float accumulated = 0f;
			bool hasRange = false;
			float previousProgress = scoreInfos[0].bar + scoreInfos[0].barProgress;
			float previousSpeedRatio = scoreInfos[0].speedRatio;

			for (int i = 0; i <= scoreInfos.Length; i++)
			{
				float segmentProgress = i < scoreInfos.Length
					? scoreInfos[i].bar + scoreInfos[i].barProgress
					: noteProgress;
				float segmentSpeedRatio = i < scoreInfos.Length ? scoreInfos[i].speedRatio : previousSpeedRatio;

				if (segmentProgress >= currentProgress)
				{
					if (hasRange)
					{
						if (segmentProgress > noteProgress)
						{
							accumulated += (noteProgress - previousProgress) * previousSpeedRatio;
							break;
						}

						accumulated += (segmentProgress - previousProgress) * previousSpeedRatio;
					}
					else if (i > 0)
					{
						if (segmentProgress > noteProgress)
						{
							return currentFrameInfo.speedRatio;
						}

						accumulated += (segmentProgress - currentProgress) * scoreInfos[i - 1].speedRatio;
					}
					else
					{
						accumulated += segmentProgress - currentProgress;
					}

					hasRange = true;
				}

				if (segmentProgress >= noteProgress)
				{
					break;
				}

				previousProgress = segmentProgress;
				previousSpeedRatio = segmentSpeedRatio;
			}

			if (!hasRange)
			{
				return previousSpeedRatio;
			}

			float range = noteProgress - currentProgress;
			return Mathf.Approximately(range, 0f) ? currentFrameInfo.speedRatio : accumulated / range;
		}

		private void BindNoteCallbacks(NoteBase[] notes)
		{
			if (notes == null)
			{
				return;
			}

			HashSet<NoteBase> visited = new HashSet<NoteBase>();

			foreach (NoteBase note in notes)
			{
				BindNoteCallbacks(note, visited);
			}
		}

		private void BindNoteCallbacks(NoteBase note, HashSet<NoteBase> visited)
		{
			if (note == null || !visited.Add(note))
			{
				return;
			}

			note.OnSpawn = OnSpawnNote;
			note.OnUnspawn = OnUnSpawnNote;
			note.OnJudgment = OnJudgmentNote;
			note.OnUpdateCombo = OnUpdateCombo;
			note.OnUpdateScore = OnUpdateScore;
			note.OnDamage = OnDamage;
			note.OnUpdateResult = OnUpdateResult;
			note.CalcTimeOffset = CalcTimeOffset;

			if (note.NoteList == null)
			{
				return;
			}

			foreach (NoteBase childNote in note.NoteList)
			{
				BindNoteCallbacks(childNote, visited);
			}
		}

		private static void BindEventCallbacks(EventBase[] events)
		{
		}

		private void UpdateSeVolume()
		{
			if (Mathf.Approximately(seScoreVolume, currentFrameInfo.seVolume))
			{
				return;
			}

			seScoreVolume = currentFrameInfo.seVolume;
			// TODO(original): route to SoundManager.SetVolumeSEPlayer once the CRI player pool is fully mirrored.
		}

		private static NoteBase[] SortNotes(NoteBase[] notes)
		{
			return notes == null
				? Array.Empty<NoteBase>()
				: notes.Where(note => note != null).OrderBy(note => note.MusicScoreInfo.time).ToArray();
		}

		private static EventBase[] SortEvents(EventBase[] events)
		{
			return events == null
				? Array.Empty<EventBase>()
				: events.Where(eventBase => eventBase != null).OrderBy(eventBase => eventBase.MusicScoreInfo.time).ToArray();
		}
	}

	internal static class ScoreLogicExtensions
	{
		public static bool HasValue(this ScoreLogic logic)
		{
			return logic != null;
		}
	}
}
