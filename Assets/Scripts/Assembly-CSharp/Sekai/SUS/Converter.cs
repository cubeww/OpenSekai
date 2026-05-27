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
		private const float DefaultBpm = 120f;
		private const float DefaultTimeSignature = 4f;
		private const float DefaultSpeedRatio = 1f;
		private const float DefaultSeVolume = 1f;
		private const int DefaultTicksPerBeat = 480;
		private const int LaneMaxIndex = 11;

		private static LiveBundleBuildData _cachedLiveBundleBuildData;

		private static LiveSettingData _cachedLiveSettingData;

		private static readonly Regex EventNotesRegex;

		private static readonly Regex EventLongNotesRegex;

		private static readonly Regex BpmRegex;

		private static readonly Regex VolumeRegex;

		private static readonly Regex TicksPerBeatRegex;

		private static readonly Regex HighSpeedRegex;

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

		public static void InvalidateLiveSettingCache()
		{
			_cachedLiveSettingData = null;
		}

		public MusicScore Convert(string musicScoreData, bool isNeedCombo = true, bool? overrideMirror = null)
		{
			ResetState();
			musicScore = new MusicScore();
			liveBundleBuildData = LoadLiveBundleBuildData();
			_cachedLiveSettingData ??= LiveSettingData.LoadFromStorage();
			isMirror = overrideMirror ?? (_cachedLiveSettingData?.IsMirror ?? false);
			isShowPair = _cachedLiveSettingData?.UseSimultaneousPushingLine ?? true;

			Load(musicScoreData ?? string.Empty);
			ConvertMusicEventList();
			ConvertMusicScoreNoteList(isNeedCombo);
			ResettingNote();
			musicScore.AddNoteArray(musicScoreNoteList.ToArray());
			ConvertEventList();
			musicScore.AddEventArray(musicScoreEventList.ToArray());
			return musicScore;
		}

		private void ConvertMusicEventList()
		{
			List<MusicBaseEventData> events = new List<MusicBaseEventData>();
			if (!timeSignaturesInfos.Any(info => info.StartBarIndex == 0))
			{
				events.Add(new TimeSignatureEventData(0, DefaultTimeSignature));
			}
			foreach (TimeSignatureInfo info in timeSignaturesInfos)
			{
				events.Add(new TimeSignatureEventData(info.StartBarIndex, info.TimeSignature));
			}
			if (!bpmInfos.Any(info => info.bar == 0 && Mathf.Approximately(info.barProgress, 0f)))
			{
				events.Add(new BpmEventData(0, 0f, DefaultBpm));
			}
			foreach (BpmInfo info in bpmInfos)
			{
				events.Add(new BpmEventData(info.bar, info.barProgress, info.bpm));
			}
			foreach (HighSpeedInfo info in highSpeedInfos)
			{
				events.Add(new HighSpeedEventData(info.BarIndex, info.TickOffset, info.SpeedRatio));
			}
			foreach (VolumeInfo info in volumeInfos)
			{
				events.Add(new VolumeEventData(info.BarIndex, info.TickOffset, info.Volume));
			}

			List<MusicScoreInfo> scoreInfos = ConvertMusicInfo(events.ToArray(), new MusicScoreInfo(0, 0f, 0f, DefaultBpm, DefaultTimeSignature, DefaultSpeedRatio, DefaultSeVolume));
			musicScore.SetMusicScoreInfo(scoreInfos.ToArray());
		}

		private List<MusicScoreInfo> ConvertMusicInfo(MusicBaseEventData[] array, MusicScoreInfo scoreInfo)
		{
			List<(MusicBaseEventData data, float progress)> events = new List<(MusicBaseEventData, float)>();
			if (array != null)
			{
				foreach (MusicBaseEventData data in array)
				{
					if (data != null)
					{
						events.Add((data, Mathf.Max(0f, GetBarProgress(data, timeSignaturesInfos))));
					}
				}
			}

			if (events.Count == 0)
			{
				return new List<MusicScoreInfo> { scoreInfo };
			}

			events.Sort((left, right) =>
			{
				int compare = left.data.barIndex.CompareTo(right.data.barIndex);
				if (compare != 0)
				{
					return compare;
				}
				compare = left.progress.CompareTo(right.progress);
				return compare != 0 ? compare : GetEventPriority(left.data).CompareTo(GetEventPriority(right.data));
			});

			List<MusicScoreInfo> scoreInfoList = new List<MusicScoreInfo>();
			float bpm = scoreInfo.bpm;
			float timeSignature = scoreInfo.timeSignature;
			float speedRatio = scoreInfo.speedRatio;
			float seVolume = scoreInfo.seVolume;
			int index = 0;
			while (index < events.Count)
			{
				int bar = events[index].data.barIndex;
				float progress = events[index].progress;
				while (index < events.Count && events[index].data.barIndex == bar && Mathf.Approximately(events[index].progress, progress))
				{
					MusicBaseEventData data = events[index].data;
					switch (data)
					{
					case TimeSignatureEventData timeSignatureEventData:
						timeSignature = timeSignatureEventData.timeSignature;
						break;
					case BpmEventData bpmEventData:
						bpm = bpmEventData.bpm;
						break;
					case HighSpeedEventData highSpeedEventData:
						speedRatio = highSpeedEventData.speedRatio;
						break;
					case VolumeEventData volumeEventData:
						seVolume = volumeEventData.seVolume;
						break;
					}
					index++;
				}
				scoreInfoList.Add(new MusicScoreInfo(bar, progress, 0f, bpm, timeSignature, speedRatio, seVolume));
			}

			if (scoreInfoList.Count == 0 || scoreInfoList[0].bar != 0 || !Mathf.Approximately(scoreInfoList[0].barProgress, 0f))
			{
				scoreInfoList.Insert(0, scoreInfo);
			}
			CalcTime(scoreInfoList);
			return scoreInfoList;
		}

		private static void CalcTime(List<MusicScoreInfo> scoreInfoList)
		{
			if (scoreInfoList == null || scoreInfoList.Count == 0)
			{
				return;
			}

			scoreInfoList.Sort(CompareMusicScoreInfoPosition);
			float time = 0f;
			MusicScoreInfo previous = scoreInfoList[0];
			previous.time = 0f;
			scoreInfoList[0] = previous;
			for (int i = 1; i < scoreInfoList.Count; i++)
			{
				MusicScoreInfo current = scoreInfoList[i];
				float deltaBars = current.bar - previous.bar + current.barProgress - previous.barProgress;
				float bpm = previous.bpm <= 0f ? DefaultBpm : previous.bpm;
				time += deltaBars * previous.timeSignature * 60f / bpm;
				current.time = time;
				scoreInfoList[i] = current;
				previous = current;
			}
		}

		private float GetBarProgress(MusicBaseEventData musicBaseEventData, List<TimeSignatureInfo> timeSignatureList)
		{
			switch (musicBaseEventData)
			{
			case TimeSignatureEventData:
				return -100f;
			case BpmEventData bpmEventData:
				return bpmEventData.barProgress;
			case HighSpeedEventData highSpeedEventData:
				return GetTickOffsetBarProgress(highSpeedEventData.barIndex, highSpeedEventData.tickOffset, timeSignatureList);
			case VolumeEventData volumeEventData:
				return GetTickOffsetBarProgress(volumeEventData.barIndex, volumeEventData.tickOffset, timeSignatureList);
			default:
				return 0f;
			}
		}

		private void Load(string musicScoreData)
		{
			string[] lines = musicScoreData.Split('\n');
			foreach (string line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					continue;
				}
				if (!LeadHeader(line))
				{
					LoadEvent(line);
				}
			}
		}

		private void ConvertMusicScoreNoteList(bool isNeedCombo)
		{
			ConvertNoteInfoDictionary(ref noteInfoDict, isNeedCombo);
			ConvertNoteInfoDictionary(ref guideInfoDict, false);
			foreach (LongNote note in tmpLong.Values)
			{
				note.SortNoteList();
			}
			foreach (GuideNote note in tmpGuide.Values)
			{
				note.SortNoteList();
			}
			musicScoreNoteList.Sort((left, right) => left.MusicScoreInfo.time.CompareTo(right.MusicScoreInfo.time));
			SetPairNotes();
		}

		private void ConvertNoteInfoDictionary(ref Dictionary<float, Dictionary<int, NoteInfo>> noteInfoDict, bool isNeedCombo)
		{
			int id = musicScoreNoteList.Count;
			foreach (float key in noteInfoDict.Keys.OrderBy(value => value).ToArray())
			{
				Dictionary<int, NoteInfo> laneDict = noteInfoDict[key];
				foreach (int laneKey in laneDict.Keys.OrderBy(value => value).ToArray())
				{
					NoteInfo noteInfo = laneDict[laneKey];
					MusicScoreInfo info = MusicScore.GenerateNoteMusicScoreInfo(noteInfo.Bar, noteInfo.BarProgress, musicScore.musicScoreInfoArray);
					int laneStart = noteInfo.Lane;
					int laneEnd = noteInfo.Lane + noteInfo.Width - 1;
					if (isMirror)
					{
						int mirroredStart = LaneMaxIndex - laneEnd;
						int mirroredEnd = LaneMaxIndex - laneStart;
						laneStart = mirroredStart;
						laneEnd = mirroredEnd;
					}
					NoteCategory category = noteInfo.Category;
					switch (category)
					{
					case NoteCategory.Normal:
					case NoteCategory.Skip:
						ConvertNormalNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category, isNeedCombo);
						break;
					case NoteCategory.Flick:
						ConvertFlickNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category, isNeedCombo);
						break;
					case NoteCategory.Long:
						ConvertLongNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category, isNeedCombo);
						break;
					case NoteCategory.Connection:
						ConvertConnectionNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
						break;
					case NoteCategory.Hidden:
						ConvertHiddenNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category);
						break;
					case NoteCategory.Friction:
						ConvertFrictionNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category, isNeedCombo);
						break;
					case NoteCategory.FrictionHide:
						ConvertFrictionHideNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category, isNeedCombo);
						break;
					case NoteCategory.FrictionLong:
						ConvertFrictionLongNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category, isNeedCombo);
						break;
					case NoteCategory.FrictionHideLong:
						ConvertFrictionHideLongNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category, isNeedCombo);
						break;
					case NoteCategory.FrictionFlick:
						ConvertFrictionFlickNote(ref id, ref noteInfo, ref info, ref laneStart, ref laneEnd, ref category, isNeedCombo);
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
					}
				}
			}
		}

		private void ConvertNormalNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category, bool isNeedCombo)
		{
			NoteBase note = noteInfo.IsSkip
				? new HiddenConnectionNote(info, id++, laneStart, laneEnd, category, noteInfo.Type, noteInfo.SpeedRatio, global::Sekai.LiveUtility.GetLaneOffset(category, liveBundleBuildData), noteInfo.LineType)
				: new NormalNote(info, id++, laneStart, laneEnd, category, noteInfo.SpeedRatio, noteInfo.Type, global::Sekai.LiveUtility.GetLaneOffset(category, liveBundleBuildData));
			if (noteInfo.IsSkip)
			{
				note.SetSkip(true);
			}
			LongJoinCheck(ref noteInfo, ref note, isNeedCombo);
		}

		private void ConvertFlickNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category, bool isNeedCombo)
		{
			float laneOffset = global::Sekai.LiveUtility.GetLaneOffset(category, liveBundleBuildData);
			float flickDistance = global::Sekai.LiveUtility.ScreenDpiToInch(liveBundleBuildData == null ? 0f : liveBundleBuildData.FlickDistance);
			NoteBase note = new FlickNote(info, id++, laneStart, laneEnd, category, noteInfo.SpeedRatio, noteInfo.Type, noteInfo.Direction, laneOffset, flickDistance);
			LongJoinCheck(ref noteInfo, ref note, isNeedCombo);
		}

		private void ConvertLongNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category, bool isNeedCombo)
		{
			NoteBase note = new LongNote(info, id++, laneStart, laneEnd, category, noteInfo.Type, noteInfo.SpeedRatio, global::Sekai.LiveUtility.GetLaneOffset(category, liveBundleBuildData), noteInfo.LineType);
			LongJoinCheck(ref noteInfo, ref note, isNeedCombo);
		}

		private void ConvertGuideNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new GuideNote(info, id++, laneStart, laneEnd, category, noteInfo.Type, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.LineType);
			GuideJoinCheck(ref noteInfo, ref note);
		}

		private void ConvertGuideEndNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new GuideEndNote(info, id++, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.Type, noteInfo.SpeedRatio, noteInfo.LineType);
			GuideJoinCheck(ref noteInfo, ref note);
		}

		private void ConvertGuideHiddenNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new GuideHiddenConnectionNote(info, id++, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.Type, noteInfo.SpeedRatio, noteInfo.LineType);
			GuideJoinCheck(ref noteInfo, ref note);
		}

		private void ConvertConnectionNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new ConnectionNote(info, id++, laneStart, laneEnd, category, noteInfo.Type, noteInfo.SpeedRatio, global::Sekai.LiveUtility.GetLaneOffset(category, liveBundleBuildData), noteInfo.LineType);
			LongConnectionCheck(noteInfo, note);
		}

		private void ConvertHiddenNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category)
		{
			NoteBase note = new HiddenConnectionNote(info, id++, laneStart, laneEnd, category, noteInfo.Type, noteInfo.SpeedRatio, global::Sekai.LiveUtility.GetLaneOffset(category, liveBundleBuildData), noteInfo.LineType);
			LongConnectionCheck(noteInfo, note);
		}

		private void ConvertFrictionNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category, bool isNeedCombo)
		{
			NoteBase note = new FrictionNote(info, id++, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.Type, noteInfo.SpeedRatio, noteInfo.LineType);
			LongJoinCheck(ref noteInfo, ref note, isNeedCombo);
		}

		private void ConvertFrictionHideNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category, bool isNeedCombo)
		{
			NoteBase note = new FrictionHideNote(info, id++, laneStart, laneEnd, category, liveBundleBuildData, noteInfo.Type, noteInfo.SpeedRatio, noteInfo.LineType);
			LongJoinCheck(ref noteInfo, ref note, isNeedCombo);
		}

		private void ConvertFrictionLongNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category, bool isNeedCombo)
		{
			NoteBase note = new FrictionLongNote(info, id++, laneStart, laneEnd, category, noteInfo.Type, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.LineType);
			LongJoinCheck(ref noteInfo, ref note, isNeedCombo);
		}

		private void ConvertFrictionHideLongNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category, bool isNeedCombo)
		{
			NoteBase note = new FrictionHideLongNote(info, id++, laneStart, laneEnd, category, noteInfo.Type, liveBundleBuildData, noteInfo.SpeedRatio, noteInfo.LineType);
			LongJoinCheck(ref noteInfo, ref note, isNeedCombo);
		}

		private void ConvertFrictionFlickNote(ref int id, ref NoteInfo noteInfo, ref MusicScoreInfo info, ref int laneStart, ref int laneEnd, ref NoteCategory category, bool isNeedCombo)
		{
			float laneOffset = global::Sekai.LiveUtility.GetLaneOffset(category, liveBundleBuildData);
			float flickDistance = global::Sekai.LiveUtility.ScreenDpiToInch(liveBundleBuildData == null ? 0f : liveBundleBuildData.FlickDistance);
			NoteBase note = new FrictionFlickNote(info, id++, laneStart, laneEnd, category, noteInfo.SpeedRatio, noteInfo.Type, noteInfo.Direction, flickDistance, laneOffset);
			LongJoinCheck(ref noteInfo, ref note, isNeedCombo);
		}

		private void ConvertEventList()
		{
			foreach (float key in eventInfoDict.Keys.OrderBy(value => value))
			{
				foreach (EventInfo eventInfo in eventInfoDict[key].OrderBy(value => value.BarProgress))
				{
					MusicScoreInfo info = MusicScore.GenerateNoteMusicScoreInfo(eventInfo.Bar, eventInfo.BarProgress, musicScore.musicScoreInfoArray);
					switch (eventInfo.EventType)
					{
					case EventType.Skill:
						musicScoreEventList.Add(new SkillEvent(info));
						break;
					case EventType.FeverBegin:
						tmpFeverBeginEvent = new FeverBeginEvent(info);
						musicScoreEventList.Add(tmpFeverBeginEvent);
						break;
					case EventType.FeverStart:
						if (tmpFeverBeginEvent != null)
						{
							tmpFeverBeginEvent.Setup(CountJudgmentNotesBetween(tmpFeverBeginEvent.MusicScoreInfo.time, info.time));
							tmpFeverBeginEvent = null;
						}
						musicScoreEventList.Add(new FeverStartEvent(info));
						break;
					}
				}
			}
			musicScoreEventList.Sort((left, right) => left.MusicScoreInfo.time.CompareTo(right.MusicScoreInfo.time));
		}

		private void LongJoinCheck(ref NoteInfo noteInfo, ref NoteBase note, bool isNeedCombo)
		{
			if (noteInfo.LongNo == -1)
			{
				SameTimingCheck(ref note);
				musicScoreNoteList.Add(note);
				return;
			}

			if (note is LongNote longRoot && (noteInfo.Category == NoteCategory.Long || noteInfo.Category == NoteCategory.FrictionLong || noteInfo.Category == NoteCategory.FrictionHideLong))
			{
				tmpLong[noteInfo.LongNo] = longRoot;
				SameTimingCheck(ref note);
				musicScoreNoteList.Add(note);
				return;
			}

			LongConnectionCheck(noteInfo, note);
			if (note.ParentNote is LongNote parent)
			{
				AddComboNote(noteInfo, note, isNeedCombo, parent, liveBundleBuildData == null ? 0 : liveBundleBuildData.LongNoteComboBeat, liveBundleBuildData, musicScore.musicScoreInfoArray);
			}
		}

		private static void AddComboNote(NoteInfo noteInfo, NoteBase note, bool isNeedCombo, LongNote longNote, int longNoteComboBeat, LiveBundleBuildData liveBundleBuildData, MusicScoreInfo[] musicScoreInfoArray)
		{
			if (!isNeedCombo || note == null || longNote == null || longNoteComboBeat <= 0)
			{
				return;
			}

			int startIndex = Mathf.FloorToInt(longNote.MusicScoreInfo.barProgress * longNoteComboBeat) + 1 + longNote.MusicScoreInfo.bar * longNoteComboBeat;
			int endIndex = Mathf.CeilToInt(note.MusicScoreInfo.barProgress * longNoteComboBeat) - 1 + note.MusicScoreInfo.bar * longNoteComboBeat;
			for (int i = startIndex; i <= endIndex; i++)
			{
				MusicScoreInfo comboInfo = MusicScore.GenerateNoteMusicScoreInfo(i / longNoteComboBeat, i % longNoteComboBeat / (float)longNoteComboBeat, musicScoreInfoArray);
				LongHoldCombo combo = new LongHoldCombo(comboInfo, longNote.Type, 0, longNote.speedRatio, global::Sekai.LiveUtility.GetLaneOffset(NoteCategory.Combo, liveBundleBuildData));
				combo.SetSkip(true);
				longNote.AddHoldCombo(combo);
			}
			longNote.SortNoteList();
		}

		private void GuideJoinCheck(ref NoteInfo noteInfo, ref NoteBase note)
		{
			if (noteInfo.LongNo == -1)
			{
				musicScoreNoteList.Add(note);
				return;
			}

			if (note is GuideNote guideRoot && noteInfo.Category == NoteCategory.Guide)
			{
				tmpGuide[noteInfo.LongNo] = guideRoot;
				musicScoreNoteList.Add(note);
				return;
			}
			GuideConnectionCheck(noteInfo, note);
		}

		private void LongConnectionCheck(NoteInfo noteInfo, NoteBase note)
		{
			if (noteInfo.LongNo == -1 || !tmpLong.TryGetValue(noteInfo.LongNo, out LongNote parent))
			{
				musicScoreNoteList.Add(note);
				return;
			}

			if (noteInfo.Category == NoteCategory.Connection || noteInfo.Category == NoteCategory.Hidden || noteInfo.Category == NoteCategory.Skip)
			{
				parent.AddConnectionNote(note);
				if (noteInfo.IsSkip)
				{
					note.SetSkip(true);
				}
			}
			else
			{
				note.SetParentNote(parent);
				parent.AddNoteListAndSetupChildNote(note);
			}
			parent.SortNoteList();
		}

		private void GuideConnectionCheck(NoteInfo noteInfo, NoteBase note)
		{
			if (noteInfo.LongNo == -1 || !tmpGuide.TryGetValue(noteInfo.LongNo, out GuideNote parent))
			{
				musicScoreNoteList.Add(note);
				return;
			}

			if (noteInfo.Category == NoteCategory.GuideHidden)
			{
				parent.AddConnectionNote(note);
			}
			else
			{
				note.SetParentNote(parent);
				parent.AddNoteListAndSetupChildNote(note);
			}
			parent.SortNoteList();
		}

		private void SameTimingCheck(ref NoteBase note)
		{
			if (!isShowPair)
			{
				return;
			}
			if (tmpPair != null)
			{
				SameTimingCheck(ref note, ref tmpPair);
			}
			tmpPair = note;
		}

		private void SameTimingCheck(ref NoteBase note, ref NoteBase usedNote)
		{
			if (!isShowPair || note == null || usedNote == null)
			{
				return;
			}
			if (Mathf.Approximately(note.MusicScoreInfo.time, usedNote.MusicScoreInfo.time))
			{
				note.SetPairNote(usedNote);
				usedNote.SetPairNote(note);
			}
		}

		private bool LeadHeader(string line)
		{
			if (line.Contains("#REQUEST \"ticks_per_beat"))
			{
				Match match = TicksPerBeatRegex.Match(line.Replace("\"", string.Empty));
				if (match.Success)
				{
					ticksPerBeat = ParseInt(match.Groups["ticks_per_beat"].Value, ticksPerBeat);
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
				return false;
			}
			return false;
		}

		private void LoadBpmInfo(string lineStr)
		{
			Match match = BpmRegex.Match(lineStr);
			if (match.Success)
			{
				bpmMap[match.Groups["bpm_id"].Value] = ParseFloat(match.Groups["bpm"].Value, DefaultBpm);
			}
		}

		private void LoadHighSpeedInfo(string lineStr)
		{
			string normalized = lineStr.Replace("\"", string.Empty).Replace("#TIL00:", string.Empty).Replace(",", "\r").Trim();
			foreach (Match match in HighSpeedRegex.Matches(normalized))
			{
				highSpeedInfos.Add(new HighSpeedInfo(
					ParseInt(match.Groups["barIndex"].Value, 0),
					ParseInt(match.Groups["tickOffset"].Value, 0),
					ParseFloat(match.Groups["speedRatio"].Value, DefaultSpeedRatio)));
			}
		}

		private void LoadVolumeInfo(string lineStr)
		{
			string normalized = lineStr.Replace("\"", string.Empty).Replace("#VOLUME:", string.Empty).Replace(",", "\r").Trim();
			foreach (Match match in VolumeRegex.Matches(normalized))
			{
				volumeInfos.Add(new VolumeInfo(
					ParseInt(match.Groups["barIndex"].Value, 0),
					ParseInt(match.Groups["tickOffset"].Value, 0),
					ParseFloat(match.Groups["volume"].Value, DefaultSeVolume)));
			}
		}

		private void LoadEvent(string lineStr)
		{
			Match longMatch = EventLongNotesRegex.Match(lineStr);
			if (longMatch.Success)
			{
				int bar = ParseInt(longMatch.Groups["bar"].Value, 0);
				int laneType = ParseSusDigit(longMatch.Groups["noteType"].Value[0]);
				int lane = GetNoteLine(longMatch.Groups["lineNo"].Value);
				int parsedLongNo = ParseSusDigit(longMatch.Groups["longNo"].Value[0]);
				ParseNoteValues(longMatch.Groups["data"].Value, out string[] values, out float[] speedRatios);
				CreateNoteBarData(bar, laneType, lane, values, speedRatios, parsedLongNo);
				return;
			}

			Match eventMatch = EventNotesRegex.Match(lineStr);
			if (!eventMatch.Success)
			{
				return;
			}

			int eventBar = ParseInt(eventMatch.Groups["bar"].Value, 0);
			string eventType = eventMatch.Groups["event_type"].Value;
			string data = eventMatch.Groups["data"].Value;
			if (eventType == "02")
			{
				CreateTimeSignaturesEventBarData(eventBar, data);
				return;
			}
			if (eventType == "08")
			{
				ParseNoteValues(data, out string[] bpmValues, out _);
				CreateBpmEventBarData(eventBar, bpmValues);
				return;
			}

			if (eventType.Length < 2)
			{
				return;
			}
			int laneTypeValue = ParseSusDigit(eventType[0]);
			int laneValue = GetNoteLine(eventType[1].ToString());
			ParseNoteValues(data, out string[] noteValues, out float[] noteSpeedRatios);
			CreateBarData(eventBar, laneTypeValue, laneValue, noteValues, noteSpeedRatios);
		}

		private void CreateTimeSignaturesEventBarData(int bar, string data)
		{
			if (string.IsNullOrWhiteSpace(data))
			{
				return;
			}
			timeSignaturesInfos.Add(new TimeSignatureInfo(ParseFloat(data, DefaultTimeSignature), bar));
		}

		private void CreateBpmEventBarData(int bar, string[] values)
		{
			if (values == null || values.Length == 0)
			{
				return;
			}
			for (int i = 0; i < values.Length; i++)
			{
				string value = values[i];
				if (value == "00")
				{
					continue;
				}
				if (bpmMap.TryGetValue(value.ToUpperInvariant(), out float bpm) || bpmMap.TryGetValue(value, out bpm))
				{
					bpmInfos.Add(new BpmInfo(bpm, bar, i / (float)values.Length));
				}
			}
		}

		private void CreateBarData(int bar, int laneType, int lane, string[] values, float[] speedRatios, int longNo = -1)
		{
			CreateNoteBarData(bar, laneType, lane, values, speedRatios, longNo);
		}

		private void CreateNoteBarData(int bar, int laneType, int lane, string[] values, float[] speedRatios, int longNo = -1)
		{
			if (values == null || values.Length == 0)
			{
				return;
			}
			for (int i = 0; i < values.Length; i++)
			{
				string value = values[i];
				if (string.IsNullOrEmpty(value) || value == "00")
				{
					continue;
				}
				int noteCode = ParseSusDigit(value[0]);
				int width = value.Length > 1 ? GetNoteWidth(value[1].ToString()) : -1;
				float barProgress = i / (float)values.Length;
				float speedRatio = speedRatios != null && i < speedRatios.Length ? speedRatios[i] : DefaultSpeedRatio;

				if (lane == 13 && laneType == 1)
				{
					if (noteCode == 1)
					{
						AddEventInfo(bar, barProgress, EventType.FeverBegin);
					}
					else if (noteCode == 2)
					{
						AddEventInfo(bar, barProgress, EventType.FeverStart);
					}
					continue;
				}

				if (longNo == -1 && laneType == 1)
				{
					switch (noteCode)
					{
					case 1:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Normal, speedRatio);
						break;
					case 2:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Normal, speedRatio, NoteType.Critical);
						break;
					case 3:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Skip, speedRatio);
						break;
					case 4:
						AddEventInfo(bar, barProgress, EventType.Skill);
						break;
					case 5:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Friction, speedRatio);
						break;
					case 6:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Friction, speedRatio, NoteType.Critical);
						break;
					case 7:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.FrictionHide, speedRatio);
						break;
					case 8:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.FrictionHide, speedRatio, NoteType.Critical);
						break;
					}
				}
				else if (longNo == -1 && laneType == 5)
				{
					switch (noteCode)
					{
					case 1:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Flick, speedRatio);
						break;
					case 2:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Normal, speedRatio, lineType: NoteLineType.EaseIn);
						break;
					case 3:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Flick, speedRatio, direction: NoteDirection.Left);
						break;
					case 4:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Flick, speedRatio, direction: NoteDirection.Right);
						break;
					case 5:
					case 6:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Normal, speedRatio, lineType: NoteLineType.EaseOut);
						break;
					}
				}
				else if (longNo != -1 && laneType == 3)
				{
					switch (noteCode)
					{
					case 1:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Long, speedRatio, longNo: longNo);
						break;
					case 2:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Normal, speedRatio, longNo: longNo);
						break;
					case 3:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Connection, speedRatio, longNo: longNo);
						break;
					case 5:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Hidden, speedRatio, longNo: longNo);
						break;
					}
				}
				else if (longNo != -1 && laneType == 9)
				{
					switch (noteCode)
					{
					case 1:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.Guide, speedRatio, longNo: longNo);
						break;
					case 2:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.GuideEnd, speedRatio, longNo: longNo);
						break;
					case 3:
					case 5:
						AddNoteInfo(bar, barProgress, lane, width, NoteCategory.GuideHidden, speedRatio, longNo: longNo);
						break;
					}
				}
			}
		}

		private void AddEventInfo(int bar, float barProgress, EventType eventType)
		{
			float key = bar + barProgress;
			if (!eventInfoDict.TryGetValue(key, out List<EventInfo> infos))
			{
				infos = new List<EventInfo>();
				eventInfoDict[key] = infos;
			}
			infos.Add(new EventInfo(bar, barProgress, eventType));
		}

		private void AddNoteInfo(int bar, float barProgress, int lane, int width, NoteCategory category, float speedRatio, NoteType type = NoteType.Default, NoteDirection direction = NoteDirection.Default, NoteLineType lineType = NoteLineType.Linear, int longNo = -1)
		{
			if (width == -1 || lane < 0 || width + lane >= 13)
			{
				return;
			}
			if (IsGuideCategory(category))
			{
				AddGuideInfo(bar, barProgress, lane, width, category, speedRatio, type, direction, lineType, longNo);
			}
			else
			{
				AddNormalNoteInfo(bar, barProgress, lane, width, category, speedRatio, type, direction, lineType, longNo);
				UpdateGuideFromNormalNote(bar, barProgress, lane, category, type, direction, lineType, speedRatio, longNo);
			}
		}

		private void AddNormalNoteInfo(int bar, float barProgress, int lane, int width, NoteCategory category, float speedRatio, NoteType type = NoteType.Default, NoteDirection direction = NoteDirection.Default, NoteLineType lineType = NoteLineType.Linear, int longNo = -1)
		{
			AddNoteInfoToDictionary(noteInfoDict, bar, barProgress, lane, width, category, speedRatio, type, direction, lineType, longNo);
		}

		private void AddGuideInfo(int bar, float barProgress, int lane, int width, NoteCategory category, float speedRatio, NoteType type = NoteType.Default, NoteDirection direction = NoteDirection.Default, NoteLineType lineType = NoteLineType.Linear, int longNo = -1)
		{
			NoteInfo inherited = FindNoteInfo(noteInfoDict, bar + barProgress, lane);
			if (inherited != null)
			{
				if (type == NoteType.Default)
				{
					type = inherited.Type;
				}
				if (direction == NoteDirection.Default)
				{
					direction = inherited.Direction;
				}
				if (lineType == NoteLineType.Linear)
				{
					lineType = inherited.LineType;
				}
			}
			AddNoteInfoToDictionary(guideInfoDict, bar, barProgress, lane, width, category, speedRatio, type, direction, lineType, longNo);
		}

		private int GetNoteLine(string lineNo)
		{
			return lineNo switch
			{
				"2" => 0,
				"3" => 1,
				"4" => 2,
				"5" => 3,
				"6" => 4,
				"7" => 5,
				"8" => 6,
				"9" => 7,
				"a" => 8,
				"b" => 9,
				"c" => 10,
				"d" => 11,
				"f" => 13,
				"e" => -2,
				_ => -1
			};
		}

		private int GetNoteWidth(string noteWidth)
		{
			return noteWidth switch
			{
				"1" => 1,
				"2" => 2,
				"3" => 3,
				"4" => 4,
				"5" => 5,
				"6" => 6,
				"7" => 7,
				"8" => 8,
				"9" => 9,
				"a" => 10,
				"b" => 11,
				"c" => 12,
				"d" => 13,
				"e" => 14,
				_ => -1
			};
		}

		private void ResettingNote()
		{
			foreach (NoteBase root in musicScoreNoteList)
			{
				NoteBase rootRef = root;
				Resetting(ref rootRef);
			}
		}

		private void Resetting(ref NoteBase data)
		{
			if (data?.NoteList == null)
			{
				return;
			}
			foreach (NoteBase note in data.NoteList)
			{
				if (note == null || !note.IsSkip)
				{
					continue;
				}
				NoteBase noteRef = note;
				GetSkipData(data, data.ChildNote, ref noteRef);
			}
		}

		private void GetSkipData(INote startNote, INote endNote, ref NoteBase data)
		{
			if (data == null || data.ParentNote is not LongNote parent || parent.NoteList == null)
			{
				return;
			}
			int index = parent.NoteList.IndexOf(data);
			if (index < 0)
			{
				return;
			}

			NoteBase previous = null;
			for (int i = index - 1; i >= 0; i--)
			{
				NoteBase candidate = parent.NoteList[i];
				if (candidate != null && !candidate.IsSkip && candidate.Category != NoteCategory.Combo)
				{
					previous = candidate;
					break;
				}
			}
			NoteBase next = null;
			for (int i = index + 1; i < parent.NoteList.Count; i++)
			{
				NoteBase candidate = parent.NoteList[i];
				if (candidate != null && !candidate.IsSkip && candidate.Category != NoteCategory.Combo)
				{
					next = candidate;
					break;
				}
			}
			if (previous == null || next == null)
			{
				return;
			}

			float previousPosition = previous.MusicScoreInfo.bar + previous.MusicScoreInfo.barProgress;
			float nextPosition = next.MusicScoreInfo.bar + next.MusicScoreInfo.barProgress;
			float dataPosition = data.MusicScoreInfo.bar + data.MusicScoreInfo.barProgress;
			float rate = Mathf.Approximately(nextPosition, previousPosition) ? 0f : Mathf.Clamp01((dataPosition - previousPosition) / (nextPosition - previousPosition));
			rate = ApplyLineEase(rate, previous.LineType);
			float laneStart = Mathf.Lerp(previous.LaneStartF, next.LaneStartF, rate);
			float laneEnd = Mathf.Lerp(previous.LaneEndF, next.LaneEndF, rate);
			data.LaneStartF = laneStart;
			data.LaneEndF = laneEnd;
			data.LaneStart = Mathf.FloorToInt(laneStart);
			data.LaneEnd = Mathf.FloorToInt(laneEnd);
			float laneOffset = global::Sekai.LiveUtility.GetLaneOffset(NoteCategory.Combo, liveBundleBuildData);
			data.JudgeLaneStart = laneStart - laneOffset;
			data.JudgeLaneEnd = laneEnd + laneOffset;
		}

		public Converter()
		{
			ResetState();
		}

		static Converter()
		{
			string floatPattern = @"[-+]?(?:\d+(?:\.\d*)?|\.\d+)";
			EventNotesRegex = new Regex("^#(?<bar>\\d{3})(?<event_type>[0-9a-z]{2}):(?<data>.+)", RegexOptions.Compiled);
			EventLongNotesRegex = new Regex("^#(?<bar>\\d{3})(?<noteType>\\d{1})(?<lineNo>[0-9a-z]{1})(?<longNo>[0-9a-z]{1}):(?<data>.+)", RegexOptions.Compiled);
			BpmRegex = new Regex("^#BPM(?<bpm_id>[0-9A-Z]{2}):(?<bpm>.+)", RegexOptions.Compiled);
			VolumeRegex = new Regex($@"(?<barIndex>\d+)'(?<tickOffset>\d+):(?<volume>{floatPattern})(?:\r|$)", RegexOptions.Compiled);
			TicksPerBeatRegex = new Regex("^#REQUEST ticks_per_beat (?<ticks_per_beat>\\d+)", RegexOptions.Compiled);
			HighSpeedRegex = new Regex($@"(?<barIndex>\d+)'(?<tickOffset>\d+):(?<speedRatio>{floatPattern})(?:\r|$)", RegexOptions.Compiled);
		}

		private void ResetState()
		{
			noteInfoDict = new Dictionary<float, Dictionary<int, NoteInfo>>();
			guideInfoDict = new Dictionary<float, Dictionary<int, NoteInfo>>();
			eventInfoDict = new Dictionary<float, List<EventInfo>>();
			musicScoreEventList = new List<EventBase>();
			musicScoreNoteList = new List<NoteBase>();
			tmpLong = new Dictionary<int, LongNote>();
			tmpGuide = new Dictionary<int, GuideNote>();
			ticksPerBeat = DefaultTicksPerBeat;
			timeSignaturesInfos = new List<TimeSignatureInfo>();
			bpmMap = new Dictionary<string, float>();
			bpmInfos = new List<BpmInfo>();
			highSpeedInfos = new List<HighSpeedInfo>();
			volumeInfos = new List<VolumeInfo>();
			tmpPair = null;
			tmpFeverBeginEvent = null;
		}

		private static LiveBundleBuildData LoadLiveBundleBuildData()
		{
			if (_cachedLiveBundleBuildData == null)
			{
				_cachedLiveBundleBuildData = Resources.Load<LiveBundleBuildData>(LiveConfig.ConfigBundleNamePath);
			}
			return _cachedLiveBundleBuildData;
		}

		private static int GetEventPriority(MusicBaseEventData data)
		{
			return data switch
			{
				TimeSignatureEventData => 0,
				BpmEventData => 1,
				HighSpeedEventData => 2,
				VolumeEventData => 3,
				_ => 4
			};
		}

		private static int CompareMusicScoreInfoPosition(MusicScoreInfo left, MusicScoreInfo right)
		{
			int compare = left.bar.CompareTo(right.bar);
			return compare != 0 ? compare : left.barProgress.CompareTo(right.barProgress);
		}

		private float GetTickOffsetBarProgress(int barIndex, int tickOffset, List<TimeSignatureInfo> timeSignatureList)
		{
			float timeSignature = DefaultTimeSignature;
			if (timeSignatureList != null)
			{
				foreach (TimeSignatureInfo info in timeSignatureList.OrderBy(value => value.StartBarIndex))
				{
					if (info.StartBarIndex > barIndex)
					{
						break;
					}
					timeSignature = info.TimeSignature;
				}
			}
			float barTicks = timeSignature * ticksPerBeat;
			return barTicks <= 0f ? 0f : tickOffset / barTicks;
		}

		private void AddNoteInfoToDictionary(Dictionary<float, Dictionary<int, NoteInfo>> target, int bar, float barProgress, int lane, int width, NoteCategory category, float speedRatio, NoteType type, NoteDirection direction, NoteLineType lineType, int longNoValue)
		{
			float key = bar + barProgress;
			if (!target.TryGetValue(key, out Dictionary<int, NoteInfo> laneDict))
			{
				laneDict = new Dictionary<int, NoteInfo>();
				target[key] = laneDict;
			}
			if (laneDict.TryGetValue(lane, out NoteInfo noteInfo))
			{
				noteInfo.Update(category, type, direction, lineType, speedRatio, longNoValue);
			}
			else
			{
				laneDict[lane] = new NoteInfo(bar, barProgress, lane, width, category, type, direction, lineType, speedRatio, longNoValue);
			}
		}

		private void UpdateGuideFromNormalNote(int bar, float barProgress, int lane, NoteCategory category, NoteType type, NoteDirection direction, NoteLineType lineType, float speedRatio, int longNoValue)
		{
			NoteInfo guideInfo = FindNoteInfo(guideInfoDict, bar + barProgress, lane);
			guideInfo?.Update(category, type, direction, lineType, speedRatio, longNoValue);
		}

		private static NoteInfo FindNoteInfo(Dictionary<float, Dictionary<int, NoteInfo>> source, float key, int lane)
		{
			return source.TryGetValue(key, out Dictionary<int, NoteInfo> laneDict) && laneDict.TryGetValue(lane, out NoteInfo noteInfo) ? noteInfo : null;
		}

		private static bool IsGuideCategory(NoteCategory category)
		{
			return category == NoteCategory.Guide || category == NoteCategory.GuideEnd || category == NoteCategory.GuideHidden;
		}

		private static void ParseNoteValues(string data, out string[] values, out float[] speedRatios)
		{
			List<string> valueList = new List<string>();
			List<float> speedList = new List<float>();
			if (string.IsNullOrEmpty(data))
			{
				values = Array.Empty<string>();
				speedRatios = Array.Empty<float>();
				return;
			}

			int i = 0;
			while (i < data.Length)
			{
				while (i < data.Length && char.IsWhiteSpace(data[i]))
				{
					i++;
				}
				if (i + 1 >= data.Length || !IsSusChar(data[i]) || !IsSusChar(data[i + 1]))
				{
					i++;
					continue;
				}

				valueList.Add(data.Substring(i, 2).ToLowerInvariant());
				i += 2;
				float speedRatio = DefaultSpeedRatio;
				if (i < data.Length && data[i] == ',')
				{
					i++;
					int start = i;
					while (i < data.Length && !char.IsWhiteSpace(data[i]))
					{
						i++;
					}
					speedRatio = ParseFloat(data.Substring(start, i - start), DefaultSpeedRatio);
				}
				speedList.Add(speedRatio);
			}
			values = valueList.ToArray();
			speedRatios = speedList.ToArray();
		}

		private static bool IsSusChar(char value)
		{
			value = char.ToLowerInvariant(value);
			return (value >= '0' && value <= '9') || (value >= 'a' && value <= 'z');
		}

		private static int ParseSusDigit(char value)
		{
			value = char.ToLowerInvariant(value);
			if (value >= '0' && value <= '9')
			{
				return value - '0';
			}
			if (value >= 'a' && value <= 'z')
			{
				return value - 'a' + 10;
			}
			return -1;
		}

		private static int ParseInt(string value, int defaultValue)
		{
			return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result) ? result : defaultValue;
		}

		private static float ParseFloat(string value, float defaultValue)
		{
			value = value?.Trim();
			return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result) ? result : defaultValue;
		}

		private void SetPairNotes()
		{
			if (!isShowPair)
			{
				return;
			}
			Dictionary<float, List<NoteBase>> notesByTime = new Dictionary<float, List<NoteBase>>();
			foreach (NoteBase root in musicScoreNoteList)
			{
				if (root?.NoteList == null)
				{
					continue;
				}
				foreach (NoteBase note in root.NoteList)
				{
					if (note == null || note.Category == NoteCategory.Combo || note.IsSkip || !note.HasJudgment)
					{
						continue;
					}
					float key = note.MusicScoreInfo.time;
					if (!notesByTime.TryGetValue(key, out List<NoteBase> list))
					{
						list = new List<NoteBase>();
						notesByTime[key] = list;
					}
					list.Add(note);
				}
			}
			foreach (List<NoteBase> list in notesByTime.Values)
			{
				if (list.Count != 2)
				{
					continue;
				}
				list[0].SetPairNote(list[1]);
				list[1].SetPairNote(list[0]);
			}
		}

		private int CountJudgmentNotesBetween(float startTime, float endTime)
		{
			int count = 0;
			foreach (NoteBase root in musicScoreNoteList)
			{
				if (root?.NoteList == null)
				{
					continue;
				}
				foreach (NoteBase note in root.NoteList)
				{
					if (note != null && note.HasJudgment && note.MusicScoreInfo.time >= startTime && note.MusicScoreInfo.time < endTime)
					{
						count++;
					}
				}
			}
			return count;
		}

		private static float ApplyLineEase(float rate, NoteLineType lineType)
		{
			rate = Mathf.Clamp01(rate);
			return lineType switch
			{
				NoteLineType.EaseIn => rate * rate,
				NoteLineType.EaseOut => 1f - (1f - rate) * (1f - rate),
				_ => rate
			};
		}
	}
}
