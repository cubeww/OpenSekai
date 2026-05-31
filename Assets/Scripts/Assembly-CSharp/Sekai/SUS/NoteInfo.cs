using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.SUS
{
	public class NoteInfo
	{
		public int Bar
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public float BarProgress
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public int Lane
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public int LongNo
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public NoteCategory Category
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public NoteType Type
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public NoteDirection Direction
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public NoteLineType LineType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public int Width
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public bool IsSkip
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public float SpeedRatio
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteInfo(int bar, float barProgress, int lane, int width, NoteCategory category, NoteType type, NoteDirection direction, NoteLineType lineType, float speedRatio, int longNo = -1)
		{
			Bar = bar;
			BarProgress = barProgress;
			Lane = lane;
			Width = width;
			Category = category;
			Type = type;
			Direction = direction;
			LineType = lineType;
			SpeedRatio = speedRatio;
			LongNo = longNo;
			IsSkip = category == NoteCategory.Skip;
		}

		public void Update(NoteCategory category, NoteType type, NoteDirection direction, NoteLineType lineType, float speedRatio, int longNo = -1)
		{
			if (category != NoteCategory.Normal)
			{
				if (category == NoteCategory.Long && Category == NoteCategory.Friction)
				{
					Category = NoteCategory.FrictionLong;
				}
				else if (category == NoteCategory.Flick && Category == NoteCategory.Friction)
				{
					Category = NoteCategory.FrictionFlick;
				}
				else if (category == NoteCategory.Long && Category == NoteCategory.FrictionHide)
				{
					Category = NoteCategory.FrictionHideLong;
				}
				else
				{
					Category = category;
				}
				// Match the original SUS importer: the skip flag is latched when the
				// NoteInfo is first created. Official scores overlay normal skip rows
				// with long connection rows to mark relay points that must not steer
				// the long-note path.
			}
			if (type != NoteType.Default)
			{
				Type = type;
			}
			if (direction != NoteDirection.Default)
			{
				Direction = direction;
			}
			if (lineType != NoteLineType.Linear)
			{
				LineType = lineType;
			}
			if (speedRatio != 1f)
			{
				SpeedRatio = speedRatio;
			}
			if (longNo != -1)
			{
				LongNo = longNo;
			}
		}
	}
}
