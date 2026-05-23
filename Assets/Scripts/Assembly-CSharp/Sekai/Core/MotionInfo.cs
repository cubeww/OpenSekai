using System;
using UnityEngine;

namespace Sekai.Core
{
	[Serializable]
	public struct MotionInfo
	{
		[SerializeField]
		[HideInInspector]
		private MotionType motionType;

		[HideInInspector]
		[SerializeField]
		private int[] uniqueCharacterIds;

		public MotionType MotionType
		{
			get
			{
				throw null;
			}
		}

		public int[] UniqueCharacterIds
		{
			get
			{
				throw null;
			}
		}
	}
}
