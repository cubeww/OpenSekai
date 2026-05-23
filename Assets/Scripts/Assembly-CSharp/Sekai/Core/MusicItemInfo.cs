using System;
using UnityEngine;

namespace Sekai.Core
{
	[Serializable]
	public struct MusicItemInfo
	{
		[SerializeField]
		[HideInInspector]
		private int id;

		[SerializeField]
		[HideInInspector]
		private bool useNonDefaultShader;

		public int Id
		{
			get
			{
				throw null;
			}
		}

		public bool UseNonDefaultShader
		{
			get
			{
				throw null;
			}
		}

		public MusicItemInfo(int id = 1)
		{
			throw null;
		}
	}
}
