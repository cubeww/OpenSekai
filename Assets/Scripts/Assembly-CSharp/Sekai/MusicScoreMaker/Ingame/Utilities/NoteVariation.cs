using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Utilities
{
	public class NoteVariation
	{
		public NoteCategory Category
		{
			[CompilerGenerated]
			get;
		}

		public NoteType Type
		{
			[CompilerGenerated]
			get;
		}

		public NoteDirection Direction
		{
			[CompilerGenerated]
			get;
		}

		public NoteLineType LineType
		{
			[CompilerGenerated]
			get;
		}

		public bool IsSkip
		{
			[CompilerGenerated]
			get;
		}

		public NoteVariation(NoteCategory category, NoteType type, NoteDirection direction, NoteLineType lineType, bool isSkip = false)
		{
			Category = category;
			Type = type;
			Direction = direction;
			LineType = lineType;
			IsSkip = isSkip;
		}
	}
}
