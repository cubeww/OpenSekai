using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.Core.Live
{
	public class BaseSkillEffect : ISkillEffect
	{
		private int _Level_k__BackingField;

		private float _LifeTime_k__BackingField;

		private int _Order_k__BackingField;

		private bool _IsEncore_k__BackingField;

		public virtual SkillType Type
		{
			get
			{
				return SkillType.ScoreUp;
			}
		}

		public virtual bool IsInstant
		{
			get
			{
				return false;
			}
		}

		public int Level
		{
			[CompilerGenerated]
			get
			{
				return _Level_k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				_Level_k__BackingField = value;
			}
		}

		public float LifeTime
		{
			[CompilerGenerated]
			get
			{
				return _LifeTime_k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				_LifeTime_k__BackingField = value;
			}
		}

		public int Order
		{
			[CompilerGenerated]
			get
			{
				return _Order_k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				_Order_k__BackingField = value;
			}
		}

		public bool IsEncore
		{
			[CompilerGenerated]
			get
			{
				return _IsEncore_k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_IsEncore_k__BackingField = value;
			}
		}

		public virtual void Activate()
		{
		}

		public virtual (NoteResult, NoteResultDescription) CalculateJudgementResult((NoteResult result, NoteResultDescription description) result)
		{
			return result;
		}

		public virtual float CalculateAdditiveScore(INote note, LiveScore score)
		{
			return 0f;
		}

		public virtual float CalculateScoreFactor(INote note, LiveScore score)
		{
			return 0f;
		}

		public virtual float GetScoreFactor(LiveScore score)
		{
			return 0f;
		}

		public BaseSkillEffect()
		{
		}
	}
}
