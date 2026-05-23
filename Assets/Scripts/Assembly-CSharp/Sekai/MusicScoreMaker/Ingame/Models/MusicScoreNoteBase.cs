using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MessagePack;
using Newtonsoft.Json;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Utilities;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	[Serializable]
	[MessagePackObject(false)]
	public class MusicScoreNoteBase
	{
		public enum NoteBaseType
		{
			Base = 0,
			Normal = 1,
			Long = 2,
			Flick = 3,
			FrictionFlick = 4,
			Connection = 5,
			HiddenConnection = 6,
			LongHoldCombo = 7,
			FrictionLong = 8,
			FrictionHideLong = 9,
			Guide = 10,
			Friction = 11,
			FrictionHide = 12,
			GuideEnd = 13,
			GuideHiddenConnection = 14
		}

		[Key(0)]
		public int id;

		[Key(1)]
		public long ticks;

		[Key(2)]
		public int laneStart;

		[Key(3)]
		public int laneEnd;

		[Key(4)]
		public NoteCategory category;

		[Key(5)]
		public NoteType type;

		[Key(6)]
		public float speedRatio;

		[Key(7)]
		public NoteLineType noteLineType;

		[Key(8)]
		public NoteBaseType noteBaseType;

		[Key(9)]
		public int previousConnectionId;

		[Key(10)]
		public int nextConnectionId;

		[Key(11)]
		public NoteDirection direction;

		[Key(12)]
		public bool isSkip;

		[JsonIgnore]
		[IgnoreMember]
		[field: NonSerialized]
		public List<MusicScoreNoteBase> ConnectedNotes { get; private set; }

		[IgnoreMember]
		public bool IsSingle
		{
			get
			{
				return previousConnectionId == -1 && nextConnectionId == -1;
			}
		}

		[IgnoreMember]
		public bool IsConnectedFirst
		{
			get
			{
				return previousConnectionId == -1 && nextConnectionId != -1;
			}
		}

		[IgnoreMember]
		public bool IsConnectedLast
		{
			get
			{
				return previousConnectionId != -1 && nextConnectionId == -1;
			}
		}

		public MusicScoreNoteBase()
		{
			previousConnectionId = -1;
			nextConnectionId = -1;
			ConnectedNotes = new List<MusicScoreNoteBase>();
		}

		public MusicScoreNoteBase(int id, long ticks, int laneStart, int laneEnd, NoteCategory category, NoteType type = NoteType.Default, float speedRatio = 1f, NoteLineType noteLineType = NoteLineType.Linear, NoteBaseType noteBaseType = NoteBaseType.Normal, bool isSkip = false, NoteDirection direction = NoteDirection.Default, int previousConnectionId = -1, int nextConnectionId = -1)
		{
			this.id = id;
			this.ticks = ticks;
			this.laneStart = laneStart;
			this.laneEnd = laneEnd;
			this.category = category;
			this.type = type;
			this.speedRatio = speedRatio;
			this.noteLineType = noteLineType;
			this.noteBaseType = noteBaseType;
			this.previousConnectionId = previousConnectionId;
			this.nextConnectionId = nextConnectionId;
			this.direction = direction;
			this.isSkip = isSkip;
			ConnectedNotes = new List<MusicScoreNoteBase>();
		}

		public static MusicScoreNoteBase[] FromNoteBaseArray(NoteBase noteBase, Func<int> getNewId, MusicScoreInfo[] musicScoreInfoArray)
		{
			return FromNoteBaseArray(noteBase, getNewId, MusicScoreMakerUtility.CreateSortedMusicScoreInfoList(musicScoreInfoArray));
		}

		public static MusicScoreNoteBase[] FromNoteBaseArray(NoteBase noteBase, Func<int> getNewId, List<MusicScoreInfo> sortedMusicScoreInfos)
		{
			if (noteBase == null)
			{
				return Array.Empty<MusicScoreNoteBase>();
			}
			MusicScoreNoteBase firstNote = FromNoteBase(noteBase, getNewId, sortedMusicScoreInfos);
			if (noteBase.NoteList == null || noteBase.NoteList.Count < 2)
			{
				return new[] { firstNote };
			}
			MusicScoreNoteBase[] result = new MusicScoreNoteBase[noteBase.NoteList.Count];
			result[0] = firstNote;
			MusicScoreNoteBase previousNote = firstNote;
			for (int i = 1; i < noteBase.NoteList.Count; i++)
			{
				MusicScoreNoteBase currentNote = FromNoteBase(noteBase.NoteList[i], getNewId, sortedMusicScoreInfos);
				previousNote.nextConnectionId = currentNote.id;
				currentNote.previousConnectionId = previousNote.id;
				result[i] = currentNote;
				previousNote = currentNote;
			}
			return result;
		}

		private static MusicScoreNoteBase FromNoteBase(NoteBase noteBase, Func<int> newId, List<MusicScoreInfo> sortedMusicScoreInfos)
		{
			if (noteBase == null)
			{
				throw new ArgumentNullException(nameof(noteBase));
			}
			if (newId == null)
			{
				throw new ArgumentNullException(nameof(newId));
			}
			long ticks = MusicScoreMakerUtility.CalculateTicksFromBarAndProgressSorted(noteBase.MusicScoreInfo.bar, noteBase.MusicScoreInfo.barProgress, sortedMusicScoreInfos);
			NoteBaseType baseType = noteBase switch
			{
				GuideEndNote => NoteBaseType.GuideEnd,
				FrictionHideNote => NoteBaseType.FrictionHide,
				FrictionNote => NoteBaseType.Friction,
				GuideHiddenConnectionNote => NoteBaseType.GuideHiddenConnection,
				HiddenConnectionNote => NoteBaseType.HiddenConnection,
				FrictionHideLongNote => NoteBaseType.FrictionHideLong,
				GuideNote => NoteBaseType.Guide,
				FrictionLongNote => NoteBaseType.FrictionLong,
				LongNote => NoteBaseType.Long,
				ConnectionNote => NoteBaseType.Connection,
				FrictionFlickNote => NoteBaseType.FrictionFlick,
				FlickNote => NoteBaseType.Flick,
				LongHoldCombo => NoteBaseType.LongHoldCombo,
				NormalNote => NoteBaseType.Normal,
				_ => NoteBaseType.Normal
			};
			NoteCategory noteCategory = noteBase.Category;
			if (noteBase is FrictionHideNote && noteCategory == NoteCategory.FrictionHideLong)
			{
				noteCategory = NoteCategory.FrictionHide;
			}
			else if (noteBase is FrictionNote && noteCategory == NoteCategory.FrictionLong)
			{
				noteCategory = NoteCategory.Friction;
			}
			return new MusicScoreNoteBase(
				newId(),
				ticks,
				noteBase.LaneStart,
				noteBase.LaneEnd,
				noteCategory,
				noteBase.Type,
				noteBase.speedRatio,
				noteBase.LineType,
				baseType,
				noteBase.IsSkip,
				noteBase.Direction);
		}

		public NoteBase ToNoteBase(LiveBundleBuildData bundleBuildData, List<MusicScoreNoteBase> noteArray, MusicScoreInfo[] musicScoreInfos, MusicScoreMakerData musicScoreMakerData)
		{
			if (previousConnectionId != -1)
			{
				return null;
			}
			MusicScoreInfo musicScoreInfo = MusicScoreMakerUtility.GenerateMusicScoreInfoFromTicks(ticks, musicScoreInfos, musicScoreMakerData);
			NoteBase rootNote = GenerateNote(this, bundleBuildData, musicScoreInfo);
			if (nextConnectionId == -1)
			{
				return rootNote;
			}
			if (rootNote is not LongNote longNote || noteArray == null)
			{
				return rootNote;
			}
			int targetConnectionId = nextConnectionId;
			while (targetConnectionId != -1)
			{
				MusicScoreNoteBase childNote = noteArray.Find(note => note != null && note.id == targetConnectionId);
				if (childNote == null)
				{
					return rootNote;
				}
				musicScoreInfo = MusicScoreMakerUtility.GenerateMusicScoreInfoFromTicks(childNote.ticks, musicScoreInfos, musicScoreMakerData);
				GenerateNote(childNote, bundleBuildData, musicScoreInfo, longNote);
				targetConnectionId = childNote.nextConnectionId;
			}
			return rootNote;
		}

		private static NoteBase GenerateNote(MusicScoreNoteBase noteBase, LiveBundleBuildData bundleBuildData, MusicScoreInfo musicScoreInfo, LongNote longNote = null)
		{
			if (noteBase == null)
			{
				throw new ArgumentNullException(nameof(noteBase));
			}
			NoteBase note;
			switch (noteBase.noteBaseType)
			{
			case NoteBaseType.Normal:
				note = new NormalNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, noteBase.speedRatio, noteBase.type, LiveUtility.GetLaneOffset(noteBase.category, bundleBuildData));
				break;
			case NoteBaseType.Long:
				note = new LongNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, noteBase.type, noteBase.speedRatio, LiveUtility.GetLaneOffset(noteBase.category, bundleBuildData), noteBase.noteLineType);
				break;
			case NoteBaseType.Flick:
				note = new FlickNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, noteBase.speedRatio, noteBase.type, noteBase.direction, LiveUtility.GetLaneOffset(noteBase.category, bundleBuildData), bundleBuildData == null ? 0f : bundleBuildData.FlickDistance);
				break;
			case NoteBaseType.FrictionFlick:
				note = new FrictionFlickNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, noteBase.speedRatio, noteBase.type, noteBase.direction, LiveUtility.ScreenDpiToInch(bundleBuildData == null ? 0f : bundleBuildData.FlickDistance), LiveUtility.GetLaneOffset(noteBase.category, bundleBuildData));
				break;
			case NoteBaseType.Connection:
				note = new ConnectionNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, noteBase.type, noteBase.speedRatio, LiveUtility.GetLaneOffset(noteBase.category, bundleBuildData), noteBase.noteLineType);
				note.SetSkip(noteBase.isSkip);
				longNote?.AddConnectionNote(note);
				return note;
			case NoteBaseType.HiddenConnection:
				note = new HiddenConnectionNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, noteBase.type, noteBase.speedRatio, LiveUtility.GetLaneOffset(noteBase.category, bundleBuildData), noteBase.noteLineType);
				note.SetSkip(noteBase.isSkip);
				longNote?.AddConnectionNote(note);
				return note;
			case NoteBaseType.LongHoldCombo:
				note = new LongHoldCombo(musicScoreInfo, noteBase.type, noteBase.speedRatio, LiveUtility.GetLaneOffset(noteBase.category, bundleBuildData));
				note.SetSkip(noteBase.isSkip);
				longNote?.AddHoldCombo((LongHoldCombo)note);
				return note;
			case NoteBaseType.FrictionLong:
				note = new FrictionLongNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, noteBase.type, bundleBuildData, noteBase.speedRatio, noteBase.noteLineType);
				break;
			case NoteBaseType.FrictionHideLong:
				note = new FrictionHideLongNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, noteBase.type, bundleBuildData, noteBase.speedRatio, noteBase.noteLineType);
				break;
			case NoteBaseType.Guide:
				if (noteBase.previousConnectionId != -1 && noteBase.nextConnectionId == -1)
				{
					note = new GuideEndNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, bundleBuildData, noteBase.type, noteBase.speedRatio, noteBase.noteLineType);
				}
				else
				{
					note = new GuideNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, noteBase.type, bundleBuildData, noteBase.speedRatio, noteBase.noteLineType);
				}
				break;
			case NoteBaseType.Friction:
				note = new FrictionNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, bundleBuildData, noteBase.type, noteBase.speedRatio, noteBase.noteLineType);
				break;
			case NoteBaseType.FrictionHide:
				note = new FrictionHideNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, bundleBuildData, noteBase.type, noteBase.speedRatio, noteBase.noteLineType);
				break;
			case NoteBaseType.GuideEnd:
				note = new GuideEndNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, bundleBuildData, noteBase.type, noteBase.speedRatio, noteBase.noteLineType);
				break;
			case NoteBaseType.GuideHiddenConnection:
				note = new GuideHiddenConnectionNote(musicScoreInfo, 0, noteBase.laneStart, noteBase.laneEnd, noteBase.category, bundleBuildData, noteBase.type, noteBase.speedRatio, noteBase.noteLineType);
				note.SetSkip(noteBase.isSkip);
				longNote?.AddConnectionNote(note);
				return note;
			default:
				throw new ArgumentOutOfRangeException(nameof(noteBase.noteBaseType), noteBase.noteBaseType, null);
			}
			note.SetSkip(noteBase.isSkip);
			SetParent(longNote, note);
			return note;
		}

		private static void SetParent(LongNote longNote, NoteBase note)
		{
			if (longNote == null)
			{
				return;
			}
			longNote.AddNoteListAndSetupChildNote(note);
			note?.SetParentNote(longNote);
		}

		public NoteOperation GetCurrentNoteOperation()
		{
			return new NoteOperation(id, laneStart, laneEnd, ticks);
		}

		public NoteOperation CalcMoveOperation(SelectedTargetOperation selectedTargetOperation, MusicScoreMakerData MusicScoreMakerData)
		{
			long ticks = this.ticks;
			if (selectedTargetOperation.deltaTicks != 0)
			{
				MusicScoreMakerUtility.ApplyNoteTicksDeltaWithConnectionConstraint(MusicScoreMakerData, ref ticks, this, selectedTargetOperation);
			}
			int deltaLane = isSkip ? 0 : selectedTargetOperation.deltaLane;
			int startLane;
			int endLane;
			switch (selectedTargetOperation.noteTapPosition)
			{
			case SelectedTargetOperation.NoteTapPosition.none:
			case SelectedTargetOperation.NoteTapPosition.center:
				startLane = MusicScoreMakerUtility.ClampLaneStart(laneStart + deltaLane);
				endLane = MusicScoreMakerUtility.ClampLaneEnd(laneEnd + deltaLane);
				break;
			case SelectedTargetOperation.NoteTapPosition.left:
				startLane = MusicScoreMakerUtility.ClampLaneStart(laneStart + deltaLane, laneEnd);
				endLane = laneEnd;
				break;
			case SelectedTargetOperation.NoteTapPosition.right:
				startLane = laneStart;
				endLane = MusicScoreMakerUtility.ClampLaneEnd(laneEnd + deltaLane, laneStart);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return new NoteOperation(id, startLane, endLane, ticks);
		}

		public void SetData(NoteOperation noteData)
		{
			id = noteData.Id;
			laneStart = noteData.StartLane;
			laneEnd = noteData.EndLane;
			ticks = noteData.Ticks;
		}

		public MusicScoreNoteBase FindNextNote(Dictionary<int, MusicScoreNoteBase> noteIdCache, bool isSkip = false)
		{
			if (nextConnectionId == -1)
			{
				return null;
			}
			if (!noteIdCache.TryGetValue(nextConnectionId, out MusicScoreNoteBase nextNote))
			{
				return null;
			}
			if (isSkip && nextNote.isSkip)
			{
				return nextNote.FindNextNote(noteIdCache, true);
			}
			return nextNote;
		}

		public MusicScoreNoteBase FindPrevNote(Dictionary<int, MusicScoreNoteBase> noteIdCache, bool isSkip = false)
		{
			if (previousConnectionId == -1)
			{
				return null;
			}
			if (!noteIdCache.TryGetValue(previousConnectionId, out MusicScoreNoteBase previousNote))
			{
				return null;
			}
			if (isSkip && previousNote.isSkip)
			{
				return previousNote.FindPrevNote(noteIdCache, true);
			}
			return previousNote;
		}

		public void UpdateConnectedNotes(Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			FindConnectedNotes(noteIdCache, ConnectedNotes);
		}

		public void FindConnectedNotes(Dictionary<int, MusicScoreNoteBase> noteIdCache, List<MusicScoreNoteBase> result, bool isSkip = false)
		{
			result.Clear();
			MusicScoreNoteBase note = this;
			if (previousConnectionId != -1)
			{
				do
				{
					note = note.FindPrevNote(noteIdCache, isSkip);
					if (note == null)
					{
						break;
					}
					result.Add(note);
				}
				while (note.previousConnectionId != -1);
			}
			if (result.Count >= 2)
			{
				result.Reverse();
			}
			result.Add(this);
			note = this;
			while (note.nextConnectionId != -1)
			{
				note = note.FindNextNote(noteIdCache, isSkip);
				if (note == null)
				{
					break;
				}
				result.Add(note);
			}
		}

		public MusicScoreNoteBase FindEndNote(Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			MusicScoreNoteBase note = this;
			while (note != null && note.nextConnectionId != -1)
			{
				note = note.FindNextNote(noteIdCache, true);
			}
			return note;
		}

		public MusicScoreNoteBase FindStartNote(Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			MusicScoreNoteBase note = this;
			while (note != null && note.previousConnectionId != -1)
			{
				note = note.FindPrevNote(noteIdCache, true);
			}
			return note;
		}

		public MusicScoreNoteBase Clone()
		{
			return new MusicScoreNoteBase
			{
				id = id,
				ticks = ticks,
				laneStart = laneStart,
				laneEnd = laneEnd,
				category = category,
				type = type,
				speedRatio = speedRatio,
				noteLineType = noteLineType,
				noteBaseType = noteBaseType,
				previousConnectionId = previousConnectionId,
				nextConnectionId = nextConnectionId,
				direction = direction,
				isSkip = isSkip,
				ConnectedNotes = new List<MusicScoreNoteBase>(ConnectedNotes)
			};
		}
	}
}
