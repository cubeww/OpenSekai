using System;
using System.Collections.Generic;
using System.Linq;
using Sekai.Live;

namespace Sekai.Core.Live
{
	public class LiveLogic
	{
		private LiveBootDataBase bootData;

		private LiveViewBase[] liveViews;

		private MusicScore musicScore;

		private EventBase[] eventArray;

		private NoteBase[] noteArray;

		private NoteBase[] highSpeedNoteArray;

		private ScoreLogic scoreLogic;

		private int noteStartIndex;

		private int highSpeedNoteStartIndex;

		private MusicScoreInfo currentFrameInfo;

		private float noteDisplayTimeOffset;

		private readonly List<NoteBase> judgmentNoteList;

		private readonly LiveBundleBuildData liveBundleBuildData;

		private Action<EventBase> noteEventResultCallback;

		private LiveResult result;

		public LiveLogic(LiveBundleBuildData data)
		{
			judgmentNoteList = new List<NoteBase>();
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

		public event Action OnFailure;

		public event Action OnFinished;

		public void Setup(LiveBootDataBase bootData, LiveViewBase[] liveViews, MusicScore musicScore)
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

			noteStartIndex = 0;
			highSpeedNoteStartIndex = 0;
			noteEventResultCallback = ExcuteEvent;
			SetScoreLogic(new ScoreLogic(liveBundleBuildData));
			Result = LiveResult.None;
		}

		public void SetScoreLogic(ScoreLogic scoreLogic)
		{
			this.scoreLogic = scoreLogic;
			this.scoreLogic?.Setup(bootData, noteArray, eventArray, liveViews);
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
