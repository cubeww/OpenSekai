using Sekai.Live;

namespace Sekai.SUS
{
	public class NoteInfo
	{
		public int Bar { get; private set; }

		public float BarProgress { get; private set; }

		public int Lane { get; private set; }

		public int LongNo { get; private set; }

		public NoteCategory Category { get; private set; }

		public NoteType Type { get; private set; }

		public NoteDirection Direction { get; private set; }

		public NoteLineType LineType { get; private set; }

		public int Width { get; private set; }

		public bool IsSkip { get; private set; }

		public float SpeedRatio { get; set; }

		public NoteInfo(int bar, float barProgress, int lane, int width, NoteCategory category, NoteType type, NoteDirection direction, NoteLineType lineType, float speedRatio, int longNo = -1)
		{
			Bar = bar;
			BarProgress = barProgress;
			Lane = lane;
			LongNo = longNo;
			Category = category;
			Type = type;
			LineType = lineType;
			Width = width;
			Direction = direction;
			IsSkip = category == NoteCategory.Skip;
			SpeedRatio = speedRatio;
		}

		public void Update(NoteCategory category, NoteType type, NoteDirection direction, NoteLineType lineType, float speedRatio, int longNo = -1)
		{
			if (category != NoteCategory.Normal)
			{
				if (Category == NoteCategory.FrictionHide)
				{
					if (category == NoteCategory.Long)
					{
						category = NoteCategory.FrictionHideLong;
					}
				}
				else if (Category == NoteCategory.Friction)
				{
					if (category == NoteCategory.Long)
					{
						category = NoteCategory.FrictionLong;
					}
					else if (category == NoteCategory.Flick)
					{
						category = NoteCategory.FrictionFlick;
					}
				}
				Category = category;
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
			if (!1f.Equals(speedRatio))
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
