using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Sekai.Live;
using UnityEngine;

namespace Sekai.SUS
{
	public class Converter
	{
		private static readonly Regex EventNotesRegex = new Regex("^#(?<bar>\\d{3})(?<event_type>[0-9a-z]{2}):(?<data>.+)");

		private static readonly Regex NotesSpeedRatioRegex = new Regex("(?<data>[0-9a-z]{2}),(?<speedRatio>[-]*[0-9]+.?[0-9]*) ");

		private static readonly Regex EventLongNotesRegex = new Regex("^#(?<bar>\\d{3})(?<noteType>\\d{1})(?<lineNo>[0-9a-z]{1})(?<longNo>[0-9a-z]{1}):(?<data>.+)");

		private static readonly Regex BpmRegex = new Regex("^#BPM(?<bpm_id>[0-9A-Z]{2}):(?<bpm>.+)");

		private static readonly Regex VolumeRegex = new Regex("(?<barIndex>\\d*)'(?<tickOffset>[0-9]*):(?<volume>[-+]?\\d*.\\d*)[\\r]");

		private static readonly Regex TicksPerBeatRegex = new Regex("^#REQUEST ticks_per_beat (?<ticks_per_beat>\\d+)");

		private static readonly Regex HighSpeedRegex = new Regex("(?<barIndex>\\d*)'(?<tickOffset>[0-9]*):(?<speedRatio>[-+]?\\d*.\\d*)[\\r]");

		private Dictionary<float, Dictionary<int, NoteInfo>> noteInfoDict;

		private Dictionary<float, Dictionary<int, NoteInfo>> guideInfoDict;

		private Dictionary<float, List<EventInfo>> eventInfoDict;

		private List<EventBase> musicScoreEventList;

		private List<NoteBase> musicScoreNoteList;

		private Dictionary<int, LongNote> tmpLong;

		private Dictionary<int, GuideNote> tmpGuide;

		private int ticksPerBeat;

		private List<TimeSignatureInfo> timeSignaturesInfos;

		private Dictionary<string, float> bpmMap;

		private List<BpmInfo> bpmInfos;

		private List<HighSpeedInfo> highSpeedInfos;

		private List<VolumeInfo> volumeInfos;

		private NoteBase tmpPair;

		private FeverBeginEvent tmpFeverBeginEvent;

		private MusicScore musicScore;

		private LiveBundleBuildData liveBundleBuildData;

		private bool isMirror;

		private bool isShowPair;

		private int longNo;

		public LiveBundleBuildData LiveBundleBuildData { get; set; }

		public bool IsMirror { get; set; }

		public bool IsShowPair { get; set; }

		public int LongNoteComboBeat { get; set; }

		public Converter()
		{
			IsShowPair = true;
			LongNoteComboBeat = 0;
			Initialize();
		}

		public MusicScore Convert(string musicScoreData)
		{
			Initialize();
			liveBundleBuildData = LiveBundleBuildData;
			isMirror = IsMirror;
			isShowPair = IsShowPair;
			musicScore = new MusicScore();
			longNo = 0;
			Load(musicScoreData);
			ConvertMusicEventList();
			ConvertMusicScoreNoteList();
			ResettingNote();
			musicScore.AddNoteArray(musicScoreNoteList.ToArray());
			ConvertEventList();
			musicScore.AddEventArray(musicScoreEventList.ToArray());
			return musicScore;
		}

		private void Initialize()
		{
			noteInfoDict = new Dictionary<float, Dictionary<int, NoteInfo>>();
			guideInfoDict = new Dictionary<float, Dictionary<int, NoteInfo>>();
			eventInfoDict = new Dictionary<float, List<EventInfo>>();
			musicScoreEventList = new List<EventBase>();
			musicScoreNoteList = new List<NoteBase>();
			tmpLong = new Dictionary<int, LongNote>();
			tmpGuide = new Dictionary<int, GuideNote>();
			ticksPerBeat = 480;
			timeSignaturesInfos = new List<TimeSignatureInfo>();
			bpmMap = new Dictionary<string, float>();
			bpmInfos = new List<BpmInfo>();
			highSpeedInfos = new List<HighSpeedInfo>();
			volumeInfos = new List<VolumeInfo>();
			tmpPair = null;
			tmpFeverBeginEvent = null;
		}

		private void ConvertMusicEventList()
		{
			List<BpmInfo> orderedBpmInfos = bpmInfos.OrderBy(x => x.bar).ThenBy(x => x.barProgress).ToList();
			List<TimeSignatureInfo> timeSignatureList = timeSignaturesInfos.OrderBy(x => x.StartBarIndex).ToList();
			List<HighSpeedInfo> orderedHighSpeedInfos = highSpeedInfos.OrderBy(x => x.BarIndex).ThenBy(x => x.TickOffset).ToList();
			List<VolumeInfo> orderedVolumeInfos = volumeInfos.OrderBy(x => x.BarIndex).ThenBy(x => x.TickOffset).ToList();
			List<MusicBaseEventData> musicBaseEventDataList = new List<MusicBaseEventData>();

			foreach (TimeSignatureInfo info in timeSignatureList)
			{
				musicBaseEventDataList.Add(new TimeSignatureEventData(info.StartBarIndex, info.TimeSignature));
			}
			foreach (HighSpeedInfo info in orderedHighSpeedInfos)
			{
				musicBaseEventDataList.Add(new HighSpeedEventData(info.BarIndex, info.TickOffset, info.SpeedRatio));
			}
			foreach (VolumeInfo info in orderedVolumeInfos)
			{
				musicBaseEventDataList.Add(new VolumeEventData(info.BarIndex, info.TickOffset, info.Volume));
			}
			foreach (BpmInfo info in orderedBpmInfos)
			{
				musicBaseEventDataList.Add(new BpmEventData(info.bar, info.barProgress, info.bpm));
			}

			MusicBaseEventData[] array = musicBaseEventDataList.OrderBy(x => x.barIndex).ThenBy(x => GetBarProgress(x, timeSignatureList)).ToArray();
			List<MusicScoreInfo> scoreInfoList = new List<MusicScoreInfo>(array.Length);
			float bpm = 120f;
			float timeSignature = 4f;
			float speedRatio = 1f;
			float seVolume = 1f;
			int bar = 0;
			float barProgress = 0f;

			for (int i = 0; i < array.Length; i++)
			{
				MusicBaseEventData musicBaseEventData = array[i];
				float eventBarProgress = GetBarProgress(musicBaseEventData, timeSignatureList);
				if (eventBarProgress < 0f)
				{
					eventBarProgress = 0f;
				}

				MusicScoreInfo found = scoreInfoList.Find(x => x.bar == musicBaseEventData.barIndex && x.barProgress.Equals(eventBarProgress));
				if (scoreInfoList.Contains(found) && found.bar == musicBaseEventData.barIndex && found.barProgress.Equals(eventBarProgress))
				{
					scoreInfoList.Remove(found);
				}

				if (musicBaseEventData is TimeSignatureEventData timeSignatureEventData)
				{
					bar = timeSignatureEventData.barIndex;
					barProgress = 0f;
					timeSignature = timeSignatureEventData.timeSignature;
				}
				else if (musicBaseEventData is HighSpeedEventData highSpeedEventData)
				{
					bar = highSpeedEventData.barIndex;
					barProgress = highSpeedEventData.tickOffset / timeSignature / ticksPerBeat;
					speedRatio = highSpeedEventData.speedRatio;
				}
				else if (musicBaseEventData is VolumeEventData volumeEventData)
				{
					bar = volumeEventData.barIndex;
					barProgress = volumeEventData.tickOffset / timeSignature / ticksPerBeat;
					seVolume = volumeEventData.seVolume;
				}
				else if (musicBaseEventData is BpmEventData bpmEventData)
				{
					bar = bpmEventData.barIndex;
					barProgress = bpmEventData.barProgress;
					bpm = bpmEventData.bpm;
				}
				else
				{
					bar = musicBaseEventData.barIndex;
					barProgress = 0f;
					Debug.LogError("Unknown music base event data.");
				}

				scoreInfoList.Add(new MusicScoreInfo(bar, barProgress, 0f, bpm, timeSignature, speedRatio, seVolume));
			}

			if (scoreInfoList.Count == 0)
			{
				scoreInfoList.Add(new MusicScoreInfo(0, 0f, 0f, bpm, timeSignature, speedRatio, seVolume));
			}

			float time = 0f;
			for (int j = 0; j < scoreInfoList.Count; j++)
			{
				MusicScoreInfo current = scoreInfoList[j];
				if (j == 0)
				{
					float secondsPerBar = current.timeSignature * 60f / current.bpm;
					time += secondsPerBar * current.bar + current.barProgress * secondsPerBar;
				}
				else
				{
					MusicScoreInfo previous = scoreInfoList[j - 1];
					float secondsPerBar = current.timeSignature * 60f / previous.bpm;
					time += secondsPerBar * (current.bar - previous.bar) + secondsPerBar * (current.barProgress - previous.barProgress);
				}
				current.time = time;
				scoreInfoList[j] = current;
			}
			musicScore.SetMusicScoreInfo(scoreInfoList.ToArray());
		}

		private float GetBarProgress(MusicBaseEventData musicBaseEventData, List<TimeSignatureInfo> timeSignatureList)
		{
			if (musicBaseEventData is TimeSignatureEventData)
			{
				return -100f;
			}
			if (musicBaseEventData is HighSpeedEventData highSpeedEventData)
			{
				return highSpeedEventData.tickOffset / GetTimeSignatureForBar(highSpeedEventData.barIndex, timeSignatureList) / ticksPerBeat;
			}
			if (musicBaseEventData is VolumeEventData volumeEventData)
			{
				return volumeEventData.tickOffset / GetTimeSignatureForBar(volumeEventData.barIndex, timeSignatureList) / ticksPerBeat;
			}
			if (musicBaseEventData is BpmEventData bpmEventData)
			{
				return bpmEventData.barProgress;
			}
			return 0f;
		}

		private void Load(string musicScoreData)
		{
			string[] array = musicScoreData.Split(new char[1] { '\n' });
			foreach (string line in array)
			{
				if (!LeadHeader(line))
				{
					LoadEvent(line);
				}
			}
		}

		private void ConvertMusicScoreNoteList()
		{
			musicScoreNoteList.Clear();
			ConvertNoteInfoDictionary(ref noteInfoDict);
			ConvertNoteInfoDictionary(ref guideInfoDict);
			musicScoreNoteList = musicScoreNoteList.OrderBy(x => x.MusicScoreInfo.time).ToList();
		}

		private void ConvertNoteInfoDictionary(ref Dictionary<float, Dictionary<int, NoteInfo>> noteInfoDictionary)
		{
			int id = 0;
			foreach (float key in noteInfoDictionary.Keys.OrderBy(x => x))
			{
				foreach (NoteInfo noteInfoValue in noteInfoDictionary[key].Values)
				{
					NoteInfo noteInfo = noteInfoValue;
					MusicScoreInfo info = musicScore.GenerateNoteMusicScoreInfo(noteInfo.Bar, noteInfo.BarProgress);
					int laneStart;
					int laneEnd;
					if (isMirror)
					{
						laneStart = 12 - noteInfo.Lane - noteInfo.Width;
						laneEnd = 11 - noteInfo.Lane;
					}
					else
					{
						laneStart = noteInfo.Lane;
						laneEnd = noteInfo.Lane + noteInfo.Width - 1;
					}

					NoteCategory category = noteInfo.Category;
					switch (category)
					{
						case NoteCategory.Normal:
							ConvertNormalNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.Long:
							ConvertLongNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.Connection:
							ConvertConnectionNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.Flick:
							ConvertFlickNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.Friction:
							ConvertFrictionNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.FrictionHide:
							ConvertFrictionHideNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.FrictionLong:
							ConvertFrictionLongNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.FrictionHideLong:
							ConvertFrictionHideLongNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.FrictionFlick:
							ConvertFrictionFlickNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.Guide:
							ConvertGuideNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.GuideEnd:
							ConvertGuideEndNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.GuideHidden:
							ConvertGuideHiddenNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
						case NoteCategory.Hidden:
							ConvertHiddenNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
							break;
					}
					id++;
				}
			}
		}

		private void ConvertNormalNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new NormalNote(info, id, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.Type);
			LongJoinCheck(ref noteInfo, ref note);
			if (note.ParentNote == null)
			{
				musicScoreNoteList.Add(note);
			}
		}

		private void ConvertFlickNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteDirection direction = GetMirroredDirection(noteInfo.Direction);
			NoteBase note = new FlickNote(info, id, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.Type, direction);
			LongJoinCheck(ref noteInfo, ref note);
			if (note.ParentNote == null)
			{
				musicScoreNoteList.Add(note);
			}
		}

		private void ConvertLongNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new LongNote(info, id, laneStart, laneEnd, category, noteInfo.Type, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.LineType);
			if (tmpLong.ContainsKey(noteInfo.LongNo) && tmpLong[noteInfo.LongNo] != null)
			{
				Debug.LogError(string.Format("Duplicate long note line: {0}", noteInfo.LongNo));
			}
			LongJoinCheck(ref noteInfo, ref note);
			tmpLong[noteInfo.LongNo] = note as LongNote;
			musicScoreNoteList.Add(note);
		}

		private void ConvertGuideNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new GuideNote(info, id, laneStart, laneEnd, category, noteInfo.Type, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.LineType);
			if (tmpGuide.ContainsKey(noteInfo.LongNo) && tmpGuide[noteInfo.LongNo] != null)
			{
				Debug.LogError(string.Format("Duplicate guide note line: {0}", noteInfo.LongNo));
			}
			GuideJoinCheck(ref noteInfo, ref note);
			tmpGuide[noteInfo.LongNo] = note as GuideNote;
			musicScoreNoteList.Add(note);
		}

		private void ConvertGuideEndNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new GuideEndNote(info, id, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.Type, noteInfo.SpeedRatio, NoteLineType.Linear);
			GuideJoinCheck(ref noteInfo, ref note);
			if (note.ParentNote == null)
			{
				musicScoreNoteList.Add(note);
			}
		}

		private void ConvertGuideHiddenNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteType type = GetGuideConnectionType(noteInfo);
			NoteBase note = new GuideHiddenConnectionNote(info, id, laneStart, laneEnd, category, liveBundleBuildData, type, noteInfo.SpeedRatio, noteInfo.LineType);
			note.SetSkip(noteInfo.IsSkip);
			GuideConnectionCheck(noteInfo, note);
		}

		private void ConvertConnectionNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteType type = GetLongConnectionType(noteInfo);
			NoteBase note = new ConnectionNote(info, id, laneStart, laneEnd, category, liveBundleBuildData, type, noteInfo.SpeedRatio, noteInfo.LineType);
			note.SetSkip(noteInfo.IsSkip);
			LongConnectionCheck(noteInfo, note);
		}

		private void ConvertHiddenNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteType type = GetLongConnectionType(noteInfo);
			NoteBase note = new HiddenConnectionNote(info, id, laneStart, laneEnd, category, liveBundleBuildData, type, noteInfo.SpeedRatio, noteInfo.LineType);
			note.SetSkip(noteInfo.IsSkip);
			LongConnectionCheck(noteInfo, note);
		}

		private void ConvertFrictionNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new FrictionNote(info, id, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.Type, noteInfo.SpeedRatio, NoteLineType.Linear);
			LongJoinCheck(ref noteInfo, ref note);
			if (note.ParentNote == null)
			{
				musicScoreNoteList.Add(note);
			}
		}

		private void ConvertFrictionHideNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new FrictionHideNote(info, id, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.Type, noteInfo.SpeedRatio, NoteLineType.Linear);
			LongJoinCheck(ref noteInfo, ref note);
			if (note.ParentNote == null)
			{
				musicScoreNoteList.Add(note);
			}
		}

		private void ConvertFrictionLongNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new FrictionLongNote(info, id, laneStart, laneEnd, category, noteInfo.Type, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.LineType);
			if (tmpLong.ContainsKey(noteInfo.LongNo) && tmpLong[noteInfo.LongNo] != null)
			{
				Debug.LogError(string.Format("Duplicate long note line: {0}", noteInfo.LongNo));
			}
			LongJoinCheck(ref noteInfo, ref note);
			tmpLong[noteInfo.LongNo] = note as LongNote;
			musicScoreNoteList.Add(note);
		}

		private void ConvertFrictionHideLongNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new FrictionHideLongNote(info, id, laneStart, laneEnd, category, noteInfo.Type, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.LineType);
			if (tmpLong.ContainsKey(noteInfo.LongNo) && tmpLong[noteInfo.LongNo] != null)
			{
				Debug.LogError(string.Format("Duplicate long note line: {0}", noteInfo.LongNo));
			}
			LongJoinCheck(ref noteInfo, ref note);
			tmpLong[noteInfo.LongNo] = note as LongNote;
			musicScoreNoteList.Add(note);
		}

		private void ConvertFrictionFlickNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteDirection direction = GetMirroredDirection(noteInfo.Direction);
			NoteBase note = new FrictionFlickNote(info, id, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.Type, direction);
			LongJoinCheck(ref noteInfo, ref note);
			if (note.ParentNote == null)
			{
				musicScoreNoteList.Add(note);
			}
		}

		private void ConvertEventList()
		{
			foreach (float key in eventInfoDict.Keys.OrderBy(x => x))
			{
				foreach (EventInfo eventInfo in eventInfoDict[key])
				{
					MusicScoreInfo info = musicScore.GenerateNoteMusicScoreInfo(eventInfo.Bar, eventInfo.BarProgress);
					if (eventInfo.EventType == EventType.Skill)
					{
						musicScoreEventList.Add(new SkillEvent(info));
					}
					else if (eventInfo.EventType == EventType.FeverBegin)
					{
						tmpFeverBeginEvent = new FeverBeginEvent(info);
					}
					else if (eventInfo.EventType == EventType.FeverStart && tmpFeverBeginEvent != null)
					{
						FeverStartEvent feverStartEvent = new FeverStartEvent(info);
						float feverTime = (feverStartEvent.MusicScoreInfo.time - tmpFeverBeginEvent.MusicScoreInfo.time) * 0.9f;
						List<NoteBase> orderedNotes = musicScoreNoteList.SelectMany(x => x.NoteList).Where(x => x.HasJudgment).OrderBy(x => x.MusicScoreInfo.time).ToList();
						List<NoteBase> feverNotes = orderedNotes.Where(x => tmpFeverBeginEvent.MusicScoreInfo.time <= x.MusicScoreInfo.time && x.MusicScoreInfo.time < tmpFeverBeginEvent.MusicScoreInfo.time + feverTime).ToList();
						tmpFeverBeginEvent.Setup(feverNotes.Count);
						foreach (NoteBase note in feverNotes)
						{
							note.SetFever(true);
						}
						NoteBase feverEndNote = orderedNotes.Where(x => feverStartEvent.MusicScoreInfo.time <= x.MusicScoreInfo.time).Take(orderedNotes.Count / 10).LastOrDefault();
						if (feverEndNote != null)
						{
							musicScoreEventList.Add(tmpFeverBeginEvent);
							musicScoreEventList.Add(feverStartEvent);
							musicScoreEventList.Add(new FeverEndEvent(feverEndNote.MusicScoreInfo));
						}
						tmpFeverBeginEvent = null;
					}
				}
			}
			musicScoreEventList = musicScoreEventList.OrderBy(x => x.MusicScoreInfo.time).ToList();
		}

		private void LongJoinCheck(ref NoteInfo noteInfo, ref NoteBase note)
		{
			int comboBeat = LongNoteComboBeat > 0 ? LongNoteComboBeat : liveBundleBuildData != null ? liveBundleBuildData.LongNoteComboBeat : 8;
			if (tmpLong.ContainsKey(noteInfo.LongNo) && tmpLong[noteInfo.LongNo] != null)
			{
				LongNote longNote = tmpLong[noteInfo.LongNo];
				int start = Mathf.FloorToInt(longNote.MusicScoreInfo.barProgress * comboBeat) + 1 + longNote.MusicScoreInfo.bar * comboBeat;
				int end = Mathf.CeilToInt(note.MusicScoreInfo.barProgress * comboBeat) + note.MusicScoreInfo.bar * comboBeat - 1;
				for (int i = start; i <= end; i++)
				{
					MusicScoreInfo info = musicScore.GenerateNoteMusicScoreInfo(i / comboBeat, (float)(i - comboBeat * (i / comboBeat)) / comboBeat);
					LongHoldCombo longHoldCombo = new LongHoldCombo(info, longNote.Type, liveBundleBuildData, noteInfo.SpeedRatio, LiveUtility.GetLaneOffset(NoteCategory.Combo, liveBundleBuildData));
					longNote.AddHoldCombo(longHoldCombo);
				}
				longNote.SetChildNote(note);
				note.SetParentNote(longNote);
				tmpLong[noteInfo.LongNo] = null;
			}
			if (!(note is FrictionHideNote) && !(note is FrictionHideLongNote))
			{
				SameTimingCheck(ref note);
			}
		}

		private void GuideJoinCheck(ref NoteInfo noteInfo, ref NoteBase note)
		{
			if (tmpGuide.ContainsKey(noteInfo.LongNo) && tmpGuide[noteInfo.LongNo] != null)
			{
				GuideNote guideNote = tmpGuide[noteInfo.LongNo];
				guideNote.SetChildNote(note);
				note.SetParentNote(guideNote);
				tmpGuide[noteInfo.LongNo] = null;
			}
		}

		private void LongConnectionCheck(NoteInfo noteInfo, NoteBase note)
		{
			if (tmpLong.ContainsKey(noteInfo.LongNo) && tmpLong[noteInfo.LongNo] != null)
			{
				tmpLong[noteInfo.LongNo].AddConnectionNote(note);
			}
			else
			{
				Debug.LogError("Connectable long note was not found.");
			}
		}

		private void GuideConnectionCheck(NoteInfo noteInfo, NoteBase note)
		{
			if (tmpGuide.ContainsKey(noteInfo.LongNo) && tmpGuide[noteInfo.LongNo] != null)
			{
				tmpGuide[noteInfo.LongNo].AddConnectionNote(note);
			}
			else
			{
				Debug.LogError("Connectable guide note was not found.");
			}
		}

		private void SameTimingCheck(ref NoteBase note)
		{
			if (tmpPair != null && tmpPair.MusicScoreInfo.time.Equals(note.MusicScoreInfo.time))
			{
				if (note.PairNote == null)
				{
					if (tmpPair.PairNote is NoteBase usedNote)
					{
						SameTimingCheck(ref note, ref usedNote);
						return;
					}
					if (isShowPair)
					{
						note.SetPairNote(tmpPair);
						tmpPair.SetPairNote(note);
					}
					tmpPair = note;
					return;
				}
				NoteBase pairNote = note.PairNote as NoteBase;
				SameTimingCheck(ref note, ref pairNote);
				return;
			}
			tmpPair = note;
		}

		private void SameTimingCheck(ref NoteBase note, ref NoteBase usedNote)
		{
			NoteBase[] array = new NoteBase[3] { note, usedNote, tmpPair }.OrderBy(x => x.Id).ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].PairNote is NoteBase pairNote && isShowPair)
				{
					array[i].SetPairNote(null);
					pairNote.SetPairNote(null);
				}
			}
			NoteBase noteBase = array[array.Length - 1];
			if (isShowPair)
			{
				array[0].SetPairNote(noteBase);
				noteBase.SetPairNote(array[0]);
			}
			tmpPair = noteBase;
		}

		private bool LeadHeader(string line)
		{
			if (line.Contains("#REQUEST \"ticks_per_beat"))
			{
				Match match = TicksPerBeatRegex.Match(line.Replace("\"", string.Empty));
				if (match.Success)
				{
					ticksPerBeat = int.Parse(match.Groups["ticks_per_beat"].Value, CultureInfo.InvariantCulture);
				}
				return false;
			}
			if (line.Contains("#TIL00"))
			{
				LoadHighSpeedInfo(line);
				return true;
			}
			if (line.Contains("#VOLUME"))
			{
				LoadVolumeInfo(line);
				return true;
			}
			if (line.Contains("#BPM"))
			{
				LoadBpmInfo(line);
			}
			return false;
		}

		private void LoadBpmInfo(string lineStr)
		{
			Match match = BpmRegex.Match(lineStr);
			if (match.Success)
			{
				bpmMap[match.Groups["bpm_id"].Value] = float.Parse(match.Groups["bpm"].Value, CultureInfo.InvariantCulture);
			}
		}

		private void LoadHighSpeedInfo(string lineStr)
		{
			string input = lineStr.Replace("\"", string.Empty).Replace("#TIL00: ", string.Empty).Replace(", ", "\r");
			MatchCollection matchCollection = HighSpeedRegex.Matches(input);
			List<HighSpeedInfo> list = new List<HighSpeedInfo>(matchCollection.Count);
			foreach (Match match in matchCollection)
			{
				if (match.Success)
				{
					list.Add(new HighSpeedInfo(
						int.Parse(match.Groups["barIndex"].Value, CultureInfo.InvariantCulture),
						int.Parse(match.Groups["tickOffset"].Value, CultureInfo.InvariantCulture),
						float.Parse(match.Groups["speedRatio"].Value, CultureInfo.InvariantCulture)));
				}
			}
			highSpeedInfos = list;
		}

		private void LoadVolumeInfo(string lineStr)
		{
			string input = lineStr.Replace("\"", string.Empty).Replace("#VOLUME: ", string.Empty).Replace(", ", "\r");
			MatchCollection matchCollection = VolumeRegex.Matches(input);
			List<VolumeInfo> list = new List<VolumeInfo>(matchCollection.Count);
			foreach (Match match in matchCollection)
			{
				if (match.Success)
				{
					list.Add(new VolumeInfo(
						int.Parse(match.Groups["barIndex"].Value, CultureInfo.InvariantCulture),
						int.Parse(match.Groups["tickOffset"].Value, CultureInfo.InvariantCulture),
						float.Parse(match.Groups["volume"].Value, CultureInfo.InvariantCulture)));
				}
			}
			volumeInfos = list;
		}

		private void LoadEvent(string lineStr)
		{
			Match match = EventNotesRegex.Match(lineStr);
			if (match.Success)
			{
				int bar = int.Parse(match.Groups["bar"].Value, CultureInfo.InvariantCulture);
				string eventType = match.Groups["event_type"].Value;
				string data = match.Groups["data"].Value.Replace("\r", string.Empty);
				if (data.Contains(","))
				{
					string[] values;
					float[] speedRatios;
					GetSpeedRatioValues(data, out values, out speedRatios);
					if (eventType != "02" && eventType != "08")
					{
						string[] laneData = SplitAtCount(eventType, 1);
						CreateBarData(bar, int.Parse(laneData[0], CultureInfo.InvariantCulture), GetNoteLine(laneData[1]), values, speedRatios);
					}
				}
				else
				{
					string normalizedData = data.Replace(" ", string.Empty);
					string[] values = SplitAtCount(normalizedData, 2);
					if (eventType == "02")
					{
						CreateTimeSignaturesEventBarData(bar, normalizedData);
					}
					else if (eventType == "08")
					{
						CreateBpmEventBarData(bar, values);
					}
					else
					{
						string[] laneData = SplitAtCount(eventType, 1);
						CreateBarData(bar, int.Parse(laneData[0], CultureInfo.InvariantCulture), GetNoteLine(laneData[1]), values, null);
					}
				}
			}

			Match match2 = EventLongNotesRegex.Match(lineStr);
			if (match2.Success)
			{
				int bar = int.Parse(match2.Groups["bar"].Value, CultureInfo.InvariantCulture);
				int laneType = int.Parse(match2.Groups["noteType"].Value, CultureInfo.InvariantCulture);
				int lane = GetNoteLine(match2.Groups["lineNo"].Value);
				int longNo = int.Parse(match2.Groups["longNo"].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				string data = match2.Groups["data"].Value.Replace("\r", string.Empty);
				if (data.Contains(","))
				{
					string[] values;
					float[] speedRatios;
					GetSpeedRatioValues(data, out values, out speedRatios);
					CreateBarData(bar, laneType, lane, values, speedRatios, longNo);
				}
				else
				{
					CreateBarData(bar, laneType, lane, SplitAtCount(data, 2), null, longNo);
				}
			}
		}

		private void CreateTimeSignaturesEventBarData(int bar, string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				Debug.LogError(string.Format("Invalid time signature: {0}. bar:{1}", data, bar));
				return;
			}
			timeSignaturesInfos.Add(new TimeSignatureInfo(float.Parse(data, CultureInfo.InvariantCulture), bar));
		}

		private void CreateBpmEventBarData(int bar, string[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				string text = values[i];
				if (text == "00")
				{
					continue;
				}
				if (bpmMap.ContainsKey(text))
				{
					bpmInfos.Add(new BpmInfo(bpmMap[text], bar, (float)i / values.Length));
				}
				else
				{
					Debug.LogError(string.Format("Invalid BPM setting {0}. bar:{1}", text, bar));
				}
			}
		}

		private void CreateBarData(int bar, int laneType, int lane, string[] values, float[] speedRatios, int longNo = -1)
		{
			if ((uint)laneType <= 9u && ((1 << laneType) & 0x22E) != 0)
			{
				CreateNoteBarData(bar, laneType, lane, values, speedRatios, longNo);
			}
		}

		private void CreateNoteBarData(int bar, int laneType, int lane, string[] values, float[] speedRatios, int longNo = -1)
		{
			for (int i = 0; i < values.Length; i++)
			{
				string text = values[i];
				if (text == "00")
				{
					continue;
				}
				float speedRatio = speedRatios == null ? 1f : speedRatios[i];
				string[] array = SplitAtCount(text, 1);
				int num = int.Parse(array[0], CultureInfo.InvariantCulture);
				int noteWidth = GetNoteWidth(array[1]);
				float barProgress = (float)i / values.Length;

				if (lane == 13)
				{
					if (laneType == 1)
					{
						if (num == 1)
						{
							AddEventInfo(bar, barProgress, EventType.FeverBegin);
						}
						else if (num == 2)
						{
							AddEventInfo(bar, barProgress, EventType.FeverStart);
						}
					}
					continue;
				}

				if (longNo == -1)
				{
					if (laneType == 5)
					{
						switch (num)
						{
							case 1:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Flick, speedRatio);
								break;
							case 2:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Normal, speedRatio, NoteType.Default, NoteDirection.Default, NoteLineType.EaseOut);
								break;
							case 3:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Flick, speedRatio, NoteType.Default, NoteDirection.Left);
								break;
							case 4:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Flick, speedRatio, NoteType.Default, NoteDirection.Right);
								break;
							case 5:
							case 6:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Normal, speedRatio, NoteType.Default, NoteDirection.Default, NoteLineType.EaseIn);
								break;
						}
					}
					else if (laneType == 1)
					{
						switch (num)
						{
							case 1:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Normal, speedRatio);
								break;
							case 2:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Normal, speedRatio, NoteType.Critical);
								break;
							case 3:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Skip, speedRatio);
								break;
							case 4:
								AddEventInfo(bar, barProgress, EventType.Skill);
								break;
							case 5:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Friction, speedRatio);
								break;
							case 6:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Friction, speedRatio, NoteType.Critical);
								break;
							case 7:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.FrictionHide, speedRatio);
								break;
							case 8:
								AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.FrictionHide, speedRatio, NoteType.Critical);
								break;
						}
					}
				}
				else if (laneType == 9)
				{
					switch (num)
					{
						case 1:
							AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Guide, speedRatio, NoteType.Default, NoteDirection.Default, NoteLineType.Linear, longNo);
							break;
						case 2:
							AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.GuideEnd, speedRatio, NoteType.Default, NoteDirection.Default, NoteLineType.Linear, longNo);
							break;
						case 3:
						case 5:
							AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.GuideHidden, speedRatio, NoteType.Default, NoteDirection.Default, NoteLineType.Linear, longNo);
							break;
					}
				}
				else if (laneType == 3)
				{
					switch (num)
					{
						case 1:
							AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Long, speedRatio, NoteType.Default, NoteDirection.Default, NoteLineType.Linear, longNo);
							break;
						case 2:
							AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Normal, speedRatio, NoteType.Default, NoteDirection.Default, NoteLineType.Linear, longNo);
							break;
						case 3:
							AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Connection, speedRatio, NoteType.Default, NoteDirection.Default, NoteLineType.Linear, longNo);
							break;
						case 5:
							AddNoteInfo(bar, barProgress, lane, noteWidth, NoteCategory.Hidden, speedRatio, NoteType.Default, NoteDirection.Default, NoteLineType.Linear, longNo);
							break;
					}
				}
			}
		}

		private void AddEventInfo(int bar, float barProgress, EventType eventType)
		{
			float key = bar + barProgress;
			if (!eventInfoDict.ContainsKey(key))
			{
				eventInfoDict.Add(key, new List<EventInfo>());
			}
			eventInfoDict[key].Add(new EventInfo(bar, barProgress, eventType));
		}

		private void AddNoteInfo(int bar, float barProgress, int lane, int width, NoteCategory category, float speedRatio, NoteType type = NoteType.Default, NoteDirection direction = NoteDirection.Default, NoteLineType lineType = NoteLineType.Linear, int longNo = -1)
		{
			if (width == -1)
			{
				Debug.LogError(string.Format("Invalid note width. bar:{0}", bar));
				return;
			}
			if (lane < 0)
			{
				Debug.LogError(string.Format("Invalid lane. bar:{0}", bar));
				return;
			}
			if (width + lane >= 13)
			{
				Debug.LogError(string.Format("Invalid note right lane. bar:{0}", bar));
				return;
			}
			if ((uint)((int)category - 9) >= 3u)
			{
				AddNormalNoteInfo(bar, barProgress, lane, width, category, speedRatio, type, direction, lineType, longNo);
			}
			AddGuideInfo(bar, barProgress, lane, width, category, speedRatio, type, direction, lineType, longNo);
		}

		private void AddNormalNoteInfo(int bar, float barProgress, int lane, int width, NoteCategory category, float speedRatio, NoteType type = NoteType.Default, NoteDirection direction = NoteDirection.Default, NoteLineType lineType = NoteLineType.Linear, int longNo = -1)
		{
			float key = bar + barProgress;
			if (!noteInfoDict.ContainsKey(key))
			{
				noteInfoDict.Add(key, new Dictionary<int, NoteInfo>());
			}
			if (noteInfoDict[key].ContainsKey(lane))
			{
				noteInfoDict[key][lane].Update(category, type, direction, lineType, speedRatio, longNo);
			}
			else
			{
				noteInfoDict[key].Add(lane, new NoteInfo(bar, barProgress, lane, width, category, type, direction, lineType, speedRatio, longNo));
			}
		}

		private void AddGuideInfo(int bar, float barProgress, int lane, int width, NoteCategory category, float speedRatio, NoteType type = NoteType.Default, NoteDirection direction = NoteDirection.Default, NoteLineType lineType = NoteLineType.Linear, int longNo = -1)
		{
			float key = bar + barProgress;
			if (noteInfoDict.ContainsKey(key) && noteInfoDict[key].ContainsKey(lane))
			{
				NoteInfo noteInfo = noteInfoDict[key][lane];
				if (noteInfo.Type == NoteType.Critical)
				{
					type = NoteType.Critical;
				}
				if (noteInfo.Type != NoteType.Default)
				{
					type = noteInfo.Type;
				}
				if (noteInfo.Direction != NoteDirection.Default)
				{
					direction = noteInfo.Direction;
				}
				if (noteInfo.LineType != NoteLineType.Linear)
				{
					lineType = noteInfo.LineType;
				}
			}
			if ((uint)((int)category - 9) <= 2u)
			{
				if (!guideInfoDict.ContainsKey(key))
				{
					guideInfoDict.Add(key, new Dictionary<int, NoteInfo>());
				}
				if (!guideInfoDict[key].ContainsKey(lane))
				{
					guideInfoDict[key].Add(lane, new NoteInfo(bar, barProgress, lane, width, category, type, direction, lineType, speedRatio, longNo));
				}
			}
			if (guideInfoDict.ContainsKey(key) && guideInfoDict[key].ContainsKey(lane))
			{
				guideInfoDict[key][lane].Update(category, type, direction, lineType, speedRatio, longNo);
			}
		}

		private int GetNoteLine(string lineNo)
		{
			switch (lineNo)
			{
				case "2":
					return 0;
				case "3":
					return 1;
				case "4":
					return 2;
				case "5":
					return 3;
				case "6":
					return 4;
				case "7":
					return 5;
				case "8":
					return 6;
				case "9":
					return 7;
				case "a":
					return 8;
				case "b":
					return 9;
				case "c":
					return 10;
				case "d":
					return 11;
				case "e":
					return -2;
				case "f":
					return 13;
				default:
					return -1;
			}
		}

		private int GetNoteWidth(string noteWidth)
		{
			switch (noteWidth)
			{
				case "1":
					return 1;
				case "2":
					return 2;
				case "3":
					return 3;
				case "4":
					return 4;
				case "5":
					return 5;
				case "6":
					return 6;
				case "7":
					return 7;
				case "8":
					return 8;
				case "9":
					return 9;
				case "a":
					return 10;
				case "b":
					return 11;
				case "c":
					return 12;
				case "d":
					return 13;
				case "e":
					return 14;
				default:
					return -1;
			}
		}

		private void ResettingNote()
		{
			List<NoteBase> list = musicScoreNoteList.ToList();
			for (int i = 0; i < list.Count; i++)
			{
				NoteBase data = list[i];
				if (data.Category == NoteCategory.Long || data.Category == NoteCategory.FrictionLong || data.Category == NoteCategory.FrictionHideLong)
				{
					Resetting(ref data);
					musicScoreNoteList[i] = data;
				}
			}
		}

		private void Resetting(ref NoteBase data)
		{
			List<INote> list = data.ViewNoteList.Where(x => !x.IsSkip).ToList();
			for (int i = 0; i < list.Count - 1; i++)
			{
				GetSkipData(list[i], list[i + 1], ref data);
			}
		}

		private void GetSkipData(INote startNote, INote endNote, ref NoteBase data)
		{
			bool foundStart = false;
			foreach (INote note in data.ViewNoteList)
			{
				if (note.Id == startNote.Id)
				{
					foundStart = true;
					continue;
				}
				if (note.Id == endNote.Id)
				{
					return;
				}
				if (!foundStart || !note.IsSkip)
				{
					continue;
				}

				float startTime = startNote.MusicScoreInfo.time;
				float endTime = endNote.MusicScoreInfo.time;
				float timeProgress = Mathf.Approximately(endTime, startTime) ? 0f : Mathf.Clamp01((note.MusicScoreInfo.time - endTime) / (startTime - endTime));
				float parentProgress = LiveConfig.GetNoteLineParentProgress(note.MusicScoreInfo.time, startNote, endNote);
				float center = Mathf.Lerp((startNote.LaneStartF + startNote.LaneEndF) * 0.5f, (endNote.LaneStartF + endNote.LaneEndF) * 0.5f, parentProgress);
				float halfWidth = Mathf.Lerp(startNote.LaneEndF - startNote.LaneStartF, endNote.LaneEndF - endNote.LaneStartF, 1f - timeProgress) * 0.5f;
				float laneStartF = Mathf.Clamp(center - halfWidth, 0f, 11f);
				float laneEndF = Mathf.Clamp(center + halfWidth, 0f, 11f);
				note.LaneStart = Mathf.FloorToInt(laneStartF);
				note.LaneEnd = Mathf.FloorToInt(laneEndF);
				note.LaneStartF = laneStartF;
				note.LaneEndF = laneEndF;
				float laneOffset = LiveUtility.GetLaneOffset(data.Category, liveBundleBuildData);
				note.JudgeLaneStart = laneStartF - laneOffset;
				note.JudgeLaneEnd = laneEndF + laneOffset;
			}
		}

		private static string[] SplitAtCount(string value, int count)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < value.Length; i += count)
			{
				list.Add(value.Substring(i, Math.Min(count, value.Length - i)));
			}
			return list.ToArray();
		}

		private static void GetSpeedRatioValues(string data, out string[] values, out float[] speedRatios)
		{
			List<string> valueList = new List<string>();
			List<float> speedRatioList = new List<float>();
			string input = data.EndsWith(" ", StringComparison.Ordinal) ? data : data + " ";
			foreach (Match match in NotesSpeedRatioRegex.Matches(input))
			{
				if (match.Success)
				{
					valueList.Add(match.Groups["data"].Value);
					speedRatioList.Add(float.Parse(match.Groups["speedRatio"].Value, CultureInfo.InvariantCulture));
				}
			}
			values = valueList.ToArray();
			speedRatios = speedRatioList.ToArray();
		}

		private static float GetTimeSignatureForBar(int barIndex, List<TimeSignatureInfo> timeSignatureList)
		{
			TimeSignatureInfo timeSignatureInfo = timeSignatureList.FirstOrDefault();
			for (int i = 0; i < timeSignatureList.Count; i++)
			{
				if (timeSignatureList[i].StartBarIndex > barIndex)
				{
					break;
				}
				timeSignatureInfo = timeSignatureList[i];
			}
			return timeSignatureInfo != null ? timeSignatureInfo.TimeSignature : 4f;
		}

		private NoteDirection GetMirroredDirection(NoteDirection direction)
		{
			if (!isMirror)
			{
				return direction;
			}
			if (direction == NoteDirection.Right)
			{
				return NoteDirection.Left;
			}
			if (direction == NoteDirection.Left)
			{
				return NoteDirection.Right;
			}
			return NoteDirection.Default;
		}

		private NoteType GetLongConnectionType(NoteInfo noteInfo)
		{
			if (tmpLong.ContainsKey(noteInfo.LongNo) && tmpLong[noteInfo.LongNo] != null)
			{
				return tmpLong[noteInfo.LongNo].Type;
			}
			return noteInfo.Type;
		}

		private NoteType GetGuideConnectionType(NoteInfo noteInfo)
		{
			if (tmpGuide.ContainsKey(noteInfo.LongNo) && tmpGuide[noteInfo.LongNo] != null)
			{
				return tmpGuide[noteInfo.LongNo].Type;
			}
			return noteInfo.Type;
		}
	}
}
