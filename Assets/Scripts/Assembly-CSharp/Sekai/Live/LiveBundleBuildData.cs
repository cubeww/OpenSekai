using UnityEngine;

namespace Sekai.Live
{
	public class LiveBundleBuildData : ScriptableObject
	{
		[SerializeField]
		private float judgmentOffsetX;

		[SerializeField]
		private float judgmentOffsetY;

		[SerializeField]
		private int longNoteComboBeat;

		[SerializeField]
		private int life;

		[SerializeField]
		private int maxLife;

		[SerializeField]
		private int dyingLife;

		[SerializeField]
		private float cutinDelayTime;

		[Tooltip("ノーマルノーツ、ロングノーツ終点")]
		[SerializeField]
		private float normalNoteOffsetX;

		[Tooltip("フリック開始、フリック終点")]
		[SerializeField]
		private float flickNoteOffsetX;

		[Tooltip("ノングノーツ開始")]
		[SerializeField]
		private float longNoteOffsetX;

		[SerializeField]
		private float flickDistance;

		private readonly float initLaneOffsetX;

		public float JudgmentOffsetX
		{
			get
			{
				return judgmentOffsetX;
			}
		}

		public float JudgmentOffsetY
		{
			get
			{
				return judgmentOffsetY;
			}
		}

		public int LongNoteComboBeat
		{
			get
			{
				return longNoteComboBeat;
			}
		}

		public int Life
		{
			get
			{
				return life;
			}
		}

		public int MaxLife
		{
			get
			{
				return maxLife;
			}
		}

		public int DyingLife
		{
			get
			{
				return dyingLife;
			}
		}

		public int CutinDelayTime
		{
			get
			{
				return Mathf.RoundToInt(cutinDelayTime);
			}
		}

		public float NormalNoteOffsetX
		{
			get
			{
				return normalNoteOffsetX;
			}
			set
			{
				normalNoteOffsetX = value;
			}
		}

		public float FlickNoteOffsetX
		{
			get
			{
				return flickNoteOffsetX;
			}
			set
			{
				flickNoteOffsetX = value;
			}
		}

		public float LongNoteOffsetX
		{
			get
			{
				return longNoteOffsetX;
			}
			set
			{
				longNoteOffsetX = value;
			}
		}

		public float FlickDistance
		{
			get
			{
				return flickDistance;
			}
			set
			{
				flickDistance = value;
			}
		}

		public LiveBundleBuildData()
		{
		}
	}
}
