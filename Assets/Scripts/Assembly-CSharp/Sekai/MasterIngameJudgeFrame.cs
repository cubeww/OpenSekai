using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterIngameJudgeFrame : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("ingameNoteType")]
		public string ingameNoteType;

		[Key("perfectBefore")]
		public float perfectBefore;

		[Key("perfectAfter")]
		public float perfectAfter;

		[Key("greatBefore")]
		public float greatBefore;

		[Key("greatAfter")]
		public float greatAfter;

		[Key("goodBefore")]
		public float goodBefore;

		[Key("goodAfter")]
		public float goodAfter;

		[Key("badBefore")]
		public float badBefore;

		[Key("badAfter")]
		public float badAfter;

		[IgnoreMember]
		public JudgeFrameType NoteType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		[IgnoreMember]
		public float PerfectBeforeJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		[IgnoreMember]
		public float PerfectAfterJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		[IgnoreMember]
		public float GreatBeforeJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		[IgnoreMember]
		public float GreatAfterJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		[IgnoreMember]
		public float GoodBeforeJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		[IgnoreMember]
		public float GoodAfterJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		[IgnoreMember]
		public float BadBeforeJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		[IgnoreMember]
		public float BadAfterJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
		{
			var titleCaseType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ingameNoteType ?? string.Empty);
			Enum.TryParse(titleCaseType, true, out JudgeFrameType noteType);
			NoteType = noteType;

			PerfectBeforeJudgeTime = perfectBefore / 60f;
			PerfectAfterJudgeTime = perfectAfter / 60f;
			GreatBeforeJudgeTime = greatBefore / 60f;
			GreatAfterJudgeTime = greatAfter / 60f;
			GoodBeforeJudgeTime = goodBefore / 60f;
			GoodAfterJudgeTime = goodAfter / 60f;
			BadBeforeJudgeTime = badBefore / 60f;
			BadAfterJudgeTime = badAfter / 60f;
		}

		void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		public MasterIngameJudgeFrame()
		{
		}
	}
}
