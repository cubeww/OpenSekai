namespace Sekai.Live
{
	public enum NoteCategory
	{
		Normal = 0,
		Long = 1,
		Connection = 2,
		Flick = 3,
		Friction = 4,
		FrictionHide = 5,
		FrictionLong = 6,
		FrictionHideLong = 7,
		FrictionFlick = 8,
		Guide = 9,
		GuideEnd = 10,
		GuideHidden = 11,
		Combo = 12,
		Hidden = 13,
		Skip = 14,
		Error = 15
	}

	public enum NoteType
	{
		Default = 0,
		Critical = 1
	}

	public enum NoteDirection
	{
		Default = 0,
		Left = 1,
		Right = 2
	}

	public enum NoteLineType
	{
		Linear = 0,
		EaseIn = 1,
		EaseOut = 2
	}

	public enum NoteLongType
	{
		Hold = 0,
		Release = 1
	}

	public enum NoteState
	{
		Wait = 0,
		Playing = 1,
		InputBegan = 2,
		Input = 3,
		Release = 4,
		Last = 5,
		Done = 6
	}

	public enum NoteResult
	{
		None = 0,
		Miss = 1,
		Pass = 2,
		Bad = 3,
		Auto = 4,
		Good = 5,
		Great = 6,
		Perfect = 7,
		JustPerfect = 8
	}

	public enum NoteResultDescription
	{
		None = 0,
		Fast = 1,
		Late = 2,
		Just = 3,
		FlickMiss = 4
	}
}
