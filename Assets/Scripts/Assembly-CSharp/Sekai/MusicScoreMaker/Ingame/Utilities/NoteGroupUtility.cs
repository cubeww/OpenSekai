using System.Collections.Generic;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.Ingame.Utilities
{
	public static class NoteGroupUtility
	{
		public static NoteGroupType GetNoteGroupType(MusicScoreNoteBase note)
		{
			return GetNoteGroupType(note, null);
		}

		public static NoteGroupType GetNoteGroupType(MusicScoreNoteBase note, Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			if (note == null)
			{
				return NoteGroupType.Unknown;
			}
			if (note.previousConnectionId == -1)
			{
				if (note.nextConnectionId == -1)
				{
					return IsSingleNote(note) ? NoteGroupType.Single : NoteGroupType.Unknown;
				}
				return IsLongStartNote(note) ? NoteGroupType.LongStart : NoteGroupType.Unknown;
			}
			if (note.nextConnectionId != -1)
			{
				return IsGuideMiddleNote(note) ? NoteGroupType.Guide : NoteGroupType.Connection;
			}
			if (!IsLongEndNote(note))
			{
				return NoteGroupType.Unknown;
			}
			return IsLongStartCritical(note, noteIdCache) ? NoteGroupType.CriticalLongEnd : NoteGroupType.LongEnd;
		}

		private static bool IsSingleNote(MusicScoreNoteBase note)
		{
			if (note == null)
			{
				return false;
			}
			return note.previousConnectionId == -1
				&& note.nextConnectionId == -1
				&& (note.category == NoteCategory.Normal
					|| note.category == NoteCategory.Flick
					|| note.category == NoteCategory.Friction
					|| note.category == NoteCategory.FrictionHide
					|| note.category == NoteCategory.FrictionFlick);
		}

		private static bool IsGuideMiddleNote(MusicScoreNoteBase note)
		{
			return note != null
				&& note.previousConnectionId != -1
				&& note.nextConnectionId != -1
				&& note.category == NoteCategory.GuideHidden;
		}

		private static bool IsLongStartNote(MusicScoreNoteBase note)
		{
			if (note == null || note.previousConnectionId != -1 || note.nextConnectionId == -1)
			{
				return false;
			}
			return note.category == NoteCategory.Long
				|| note.category == NoteCategory.FrictionLong
				|| note.category == NoteCategory.FrictionHideLong
				|| note.category == NoteCategory.Guide;
		}

		private static bool IsLongEndNote(MusicScoreNoteBase note)
		{
			if (note == null || note.previousConnectionId == -1 || note.nextConnectionId != -1)
			{
				return false;
			}
			return note.category == NoteCategory.Long
				|| note.category == NoteCategory.Flick
				|| note.category == NoteCategory.Friction
				|| note.category == NoteCategory.FrictionHide
				|| note.category == NoteCategory.FrictionLong
				|| note.category == NoteCategory.FrictionHideLong
				|| note.category == NoteCategory.FrictionFlick;
		}

		private static bool IsConnectionNote(MusicScoreNoteBase note)
		{
			return note != null && note.previousConnectionId != -1 && note.nextConnectionId != -1;
		}

		private static bool IsLongStartCritical(MusicScoreNoteBase endNote, Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			MusicScoreNoteBase startNote = endNote?.FindStartNote(noteIdCache);
			return startNote != null && startNote.type == NoteType.Critical;
		}

		public static bool IsSameNoteGroup(List<MusicScoreNoteBase> notes, Dictionary<int, MusicScoreNoteBase> noteIdCache, out NoteGroupType groupType)
		{
			groupType = NoteGroupType.Unknown;
			if (notes == null || notes.Count == 0)
			{
				return false;
			}
			groupType = GetNoteGroupType(notes[0], noteIdCache);
			if (groupType == NoteGroupType.Unknown)
			{
				return false;
			}
			for (int i = 0; i < notes.Count; i++)
			{
				if (GetNoteGroupType(notes[i], noteIdCache) != groupType)
				{
					groupType = NoteGroupType.Unknown;
					return false;
				}
			}
			return true;
		}

		public static bool IsSameNoteGroup(MusicScoreNoteBase[] notes, Dictionary<int, MusicScoreNoteBase> noteIdCache, out NoteGroupType groupType)
		{
			groupType = NoteGroupType.Unknown;
			if (notes == null || notes.Length == 0)
			{
				return false;
			}
			groupType = GetNoteGroupType(notes[0], noteIdCache);
			for (int i = 0; i < notes.Length; i++)
			{
				if (GetNoteGroupType(notes[i], noteIdCache) != groupType)
				{
					groupType = NoteGroupType.Unknown;
					return false;
				}
			}
			return true;
		}

		public static List<NoteVariation> GetAvailableVariations(NoteGroupType groupType, NoteType currentType, NoteDirection currentDirection, NoteLineType currentLineType)
		{
			List<NoteVariation> variations = new List<NoteVariation>();
			switch (groupType)
			{
			case NoteGroupType.Single:
				AddSingleNoteVariations(variations);
				break;
			case NoteGroupType.LongStart:
				AddLongStartNoteVariations(variations);
				break;
			case NoteGroupType.Guide:
				AddGuideNoteVariations(variations);
				break;
			case NoteGroupType.LongEnd:
				AddLongEndNoteVariations(variations);
				break;
			case NoteGroupType.CriticalLongEnd:
				AddCriticalLongEndNoteVariations(variations);
				break;
			case NoteGroupType.Connection:
				AddConnectionNoteVariations(variations);
				break;
			}
			return variations;
		}

		private static void AddSingleNoteVariations(List<NoteVariation> variations)
		{
			variations.Add(new NoteVariation(NoteCategory.Normal, NoteType.Default, NoteDirection.Default, NoteLineType.Linear));
			AddDirectionalVariations(variations, NoteCategory.Flick, NoteType.Default);
			variations.Add(new NoteVariation(NoteCategory.Friction, NoteType.Default, NoteDirection.Default, NoteLineType.Linear));
			AddDirectionalVariations(variations, NoteCategory.FrictionFlick, NoteType.Default);
			variations.Add(new NoteVariation(NoteCategory.Normal, NoteType.Critical, NoteDirection.Default, NoteLineType.Linear));
			AddDirectionalVariations(variations, NoteCategory.Flick, NoteType.Critical);
			variations.Add(new NoteVariation(NoteCategory.Friction, NoteType.Critical, NoteDirection.Default, NoteLineType.Linear));
			AddDirectionalVariations(variations, NoteCategory.FrictionFlick, NoteType.Critical);
		}

		private static void AddGuideNoteVariations(List<NoteVariation> variations)
		{
			AddLineVariations(variations, NoteCategory.GuideHidden, NoteType.Default);
			AddLineVariations(variations, NoteCategory.GuideHidden, NoteType.Critical);
		}

		private static void AddLongStartNoteVariations(List<NoteVariation> variations)
		{
			AddLineVariations(variations, NoteCategory.Long, NoteType.Default);
			AddLineVariations(variations, NoteCategory.FrictionHideLong, NoteType.Default);
			AddLineVariations(variations, NoteCategory.FrictionLong, NoteType.Default);
			AddLineVariations(variations, NoteCategory.Guide, NoteType.Default);
			AddLineVariations(variations, NoteCategory.Long, NoteType.Critical);
			AddLineVariations(variations, NoteCategory.FrictionHideLong, NoteType.Critical);
			AddLineVariations(variations, NoteCategory.FrictionLong, NoteType.Critical);
			AddLineVariations(variations, NoteCategory.Guide, NoteType.Critical);
		}

		private static void AddLongEndNoteVariations(List<NoteVariation> variations)
		{
			variations.Add(new NoteVariation(NoteCategory.Long, NoteType.Default, NoteDirection.Default, NoteLineType.Linear));
			variations.Add(new NoteVariation(NoteCategory.FrictionHide, NoteType.Default, NoteDirection.Default, NoteLineType.Linear));
			variations.Add(new NoteVariation(NoteCategory.Friction, NoteType.Default, NoteDirection.Default, NoteLineType.Linear));
			AddDirectionalVariations(variations, NoteCategory.Flick, NoteType.Default);
			AddDirectionalVariations(variations, NoteCategory.FrictionFlick, NoteType.Default);
			variations.Add(new NoteVariation(NoteCategory.Friction, NoteType.Critical, NoteDirection.Default, NoteLineType.Linear));
			AddDirectionalVariations(variations, NoteCategory.Flick, NoteType.Critical);
			AddDirectionalVariations(variations, NoteCategory.FrictionFlick, NoteType.Critical);
		}

		private static void AddCriticalLongEndNoteVariations(List<NoteVariation> variations)
		{
			variations.Add(new NoteVariation(NoteCategory.Long, NoteType.Critical, NoteDirection.Default, NoteLineType.Linear));
			variations.Add(new NoteVariation(NoteCategory.FrictionHide, NoteType.Critical, NoteDirection.Default, NoteLineType.Linear));
			variations.Add(new NoteVariation(NoteCategory.Friction, NoteType.Critical, NoteDirection.Default, NoteLineType.Linear));
			AddDirectionalVariations(variations, NoteCategory.Flick, NoteType.Critical);
			AddDirectionalVariations(variations, NoteCategory.FrictionFlick, NoteType.Critical);
		}

		private static void AddConnectionNoteVariations(List<NoteVariation> variations)
		{
			variations.Add(new NoteVariation(NoteCategory.Connection, NoteType.Default, NoteDirection.Default, NoteLineType.Linear, true));
			AddLineVariations(variations, NoteCategory.Connection, NoteType.Default);
			AddLineVariations(variations, NoteCategory.Hidden, NoteType.Default);
			variations.Add(new NoteVariation(NoteCategory.Connection, NoteType.Critical, NoteDirection.Default, NoteLineType.Linear, true));
			AddLineVariations(variations, NoteCategory.Connection, NoteType.Critical);
			AddLineVariations(variations, NoteCategory.Hidden, NoteType.Critical);
		}

		private static void AddDirectionalVariations(List<NoteVariation> variations, NoteCategory category, NoteType type)
		{
			variations.Add(new NoteVariation(category, type, NoteDirection.Default, NoteLineType.Linear));
			variations.Add(new NoteVariation(category, type, NoteDirection.Left, NoteLineType.Linear));
			variations.Add(new NoteVariation(category, type, NoteDirection.Right, NoteLineType.Linear));
		}

		private static void AddLineVariations(List<NoteVariation> variations, NoteCategory category, NoteType type)
		{
			variations.Add(new NoteVariation(category, type, NoteDirection.Default, NoteLineType.Linear));
			variations.Add(new NoteVariation(category, type, NoteDirection.Default, NoteLineType.EaseOut));
			variations.Add(new NoteVariation(category, type, NoteDirection.Default, NoteLineType.EaseIn));
		}
	}
}
