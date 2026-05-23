using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sekai.Core
{
	[Serializable]
	public struct CharacterInfo
	{
		[HideInInspector]
		[SerializeField]
		private int id;

		[HideInInspector]
		[SerializeField]
		private string headOptional;

		[HideInInspector]
		[SerializeField]
		private string face;

		[HideInInspector]
		[SerializeField]
		private string body;

		[HideInInspector]
		[SerializeField]
		private string colorVariation;

		[HideInInspector]
		[SerializeField]
		private string headOptionalColorVariation;

		[HideInInspector]
		[SerializeField]
		private ModelPrefabType prefabType;

		[SerializeField]
		[HideInInspector]
		private bool isHeelOffset;

		[HideInInspector]
		[SerializeField]
		private float defaultHeelOffset;

		[SerializeField]
		[HideInInspector]
		private MotionInfo motionInfo;

		[HideInInspector]
		[SerializeField]
		private MusicItemInfo[] musicItemInfos;

		[HideInInspector]
		[SerializeField]
		public bool useHairShadow;

		[SerializeField]
		[HideInInspector]
		private bool disinheritCharacterInfo;

		[SerializeField]
		[HideInInspector]
		private bool isLoadInActive;

		[HideInInspector]
		[SerializeField]
		private bool isInsertCharacter;

		[HideInInspector]
		[SerializeField]
		private bool isUseSpringBoneController;

		public int Id
		{
			get
			{
				throw null;
			}
		}

		public string HeadOptional
		{
			get
			{
				throw null;
			}
		}

		public string Face
		{
			get
			{
				throw null;
			}
		}

		public string Body
		{
			get
			{
				throw null;
			}
		}

		public string ColorVariation
		{
			get
			{
				throw null;
			}
		}

		public string HeadOptionalColorVariation
		{
			get
			{
				throw null;
			}
		}

		public ModelPrefabType PrefabType
		{
			get
			{
				throw null;
			}
		}

		public FaceModelType FaceModelType
		{
			[CompilerGenerated]
			readonly get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public bool IsHeelOffset
		{
			get
			{
				throw null;
			}
		}

		public float DefaultHeelOffset
		{
			get
			{
				throw null;
			}
		}

		public MotionInfo MotionInfo
		{
			get
			{
				throw null;
			}
		}

		public MusicItemInfo[] MusicItemInfos
		{
			get
			{
				throw null;
			}
		}

		public bool UseHairShadow
		{
			get
			{
				throw null;
			}
		}

		public bool DisinheritCharacterInfo
		{
			get
			{
				throw null;
			}
		}

		public bool IsLoadInActive
		{
			get
			{
				throw null;
			}
		}

		public bool IsInsertCharacter
		{
			get
			{
				throw null;
			}
		}

		public bool IsUseSpringBoneController
		{
			get
			{
				throw null;
			}
		}

		public CharacterInfo(int id = 1)
		{
			throw null;
		}

		public void SetDefaultHeelOffset(float heelOffset)
		{
			throw null;
		}
	}
}
