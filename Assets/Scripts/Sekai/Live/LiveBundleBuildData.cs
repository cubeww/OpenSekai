using UnityEngine;

namespace Sekai.Live
{
	public class LiveBundleBuildData : ScriptableObject
	{
		public const float DefaultFlickDistance = 0.083333f;

		[SerializeField]
		private float judgmentOffsetX = 1.8f;

		[SerializeField]
		private float judgmentOffsetY = 2.5f;

		[SerializeField]
		private int longNoteComboBeat = 8;

		[SerializeField]
		private int life = 1000;

		[SerializeField]
		private int maxLife = 2000;

		[SerializeField]
		private int dyingLife = 200;

		[SerializeField]
		private float cutinDelayTime = 2.5f;

		[SerializeField]
		private float normalNoteOffsetX = 1.25f;

		[SerializeField]
		private float flickNoteOffsetX = 1.5f;

		[SerializeField]
		private float longNoteOffsetX = 1.5f;

		[SerializeField]
		private float flickDistance = DefaultFlickDistance;

		private readonly float initLaneOffsetX = 1.25f;

		public float JudgmentOffsetX
		{
			get { return judgmentOffsetX; }
		}

		public float JudgmentOffsetY
		{
			get { return judgmentOffsetY; }
		}

		public int LongNoteComboBeat
		{
			get { return longNoteComboBeat; }
		}

		public int Life
		{
			get { return life; }
		}

		public int MaxLife
		{
			get { return maxLife; }
		}

		public int DyingLife
		{
			get { return dyingLife; }
		}

		public int CutinDelayTime
		{
			get
			{
				float milliseconds = cutinDelayTime * 1000f;
				return float.IsPositiveInfinity(milliseconds) ? (int)(0f - milliseconds) : (int)milliseconds;
			}
		}

		public float NormalNoteOffsetX
		{
			get { return normalNoteOffsetX <= 0f ? initLaneOffsetX : normalNoteOffsetX; }
			set { normalNoteOffsetX = value; }
		}

		public float FlickNoteOffsetX
		{
			get { return flickNoteOffsetX <= 0f ? initLaneOffsetX : flickNoteOffsetX; }
			set { flickNoteOffsetX = value; }
		}

		public float LongNoteOffsetX
		{
			get { return longNoteOffsetX <= 0f ? initLaneOffsetX : longNoteOffsetX; }
			set { longNoteOffsetX = value; }
		}

		public float FlickDistance
		{
			get { return flickDistance; }
			set { flickDistance = value; }
		}
	}
}
