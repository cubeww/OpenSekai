using System;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	public readonly struct NoteShapeFingerprint : IEquatable<NoteShapeFingerprint>
	{
		public readonly int LaneStart;

		public readonly int LaneEnd;

		public readonly NoteCategory Category;

		public readonly NoteType Type;

		public readonly MusicScoreNoteBase.NoteBaseType NoteBaseType;

		public readonly NoteDirection Direction;

		public readonly float SpeedRatio;

		public readonly NoteLineType NoteLineType;

		public readonly bool IsSkip;

		public NoteShapeFingerprint(int laneStart, int laneEnd, NoteCategory category, NoteType type, MusicScoreNoteBase.NoteBaseType noteBaseType, NoteDirection direction, float speedRatio, NoteLineType noteLineType, bool isSkip)
		{
			LaneStart = laneStart;
			LaneEnd = laneEnd;
			Category = category;
			Type = type;
			NoteBaseType = noteBaseType;
			Direction = direction;
			SpeedRatio = speedRatio;
			NoteLineType = noteLineType;
			IsSkip = isSkip;
		}

		public static NoteShapeFingerprint FromNote(MusicScoreNoteBase note)
		{
			return new NoteShapeFingerprint(note.laneStart, note.laneEnd, note.category, note.type, note.noteBaseType, note.direction, note.speedRatio, note.noteLineType, note.isSkip);
		}

		public NoteShapeFingerprint ToPositionOnly()
		{
			return new NoteShapeFingerprint(LaneStart, LaneEnd, default, default, default, default, 1f, default, false);
		}

		public NoteShapeFingerprint Mirror(int laneCount)
		{
			NoteDirection direction = Direction;
			if (direction == NoteDirection.Left)
			{
				direction = NoteDirection.Right;
			}
			else if (direction == NoteDirection.Right)
			{
				direction = NoteDirection.Left;
			}
			return new NoteShapeFingerprint(laneCount - 1 - LaneEnd, laneCount - 1 - LaneStart, Category, Type, NoteBaseType, direction, SpeedRatio, NoteLineType, IsSkip);
		}

		public bool Equals(NoteShapeFingerprint other)
		{
			return LaneStart == other.LaneStart
				&& LaneEnd == other.LaneEnd
				&& Category == other.Category
				&& Type == other.Type
				&& NoteBaseType == other.NoteBaseType
				&& Direction == other.Direction
				&& FloatBits(SpeedRatio) == FloatBits(other.SpeedRatio)
				&& NoteLineType == other.NoteLineType
				&& IsSkip == other.IsSkip;
		}

		public override bool Equals(object obj)
		{
			return obj is NoteShapeFingerprint other && Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return 31 * (31 * (31 * (31 * (31 * (31 * (31 * (31 * LaneStart + LaneEnd) + (int)Category) + (int)Type) + (int)NoteBaseType) + (int)Direction) + FloatBits(SpeedRatio)) + (int)NoteLineType) + (IsSkip ? 1 : 0) + 954237711;
			}
		}

		public static bool operator ==(NoteShapeFingerprint left, NoteShapeFingerprint right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(NoteShapeFingerprint left, NoteShapeFingerprint right)
		{
			return !left.Equals(right);
		}

		private static int FloatBits(float value)
		{
			return BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
		}
	}
}
