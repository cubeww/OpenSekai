using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sekai
{
	public class TweenTargetColorNormal : ITweenTargetComponent
	{
		private SpriteRenderer sprite;
		private Vector3 localPosition;
		private Vector3 localRotation;
		private Vector3 localScale;

		public bool IsExist
		{
			get
			{
				return sprite != null;
			}
		}

		public Vector3 LocalPosition
		{
			[CompilerGenerated]
			get
			{
				return localPosition;
			}
			[CompilerGenerated]
			set
			{
				localPosition = value;
			}
		}

		public Vector3 LocalRotation
		{
			[CompilerGenerated]
			get
			{
				return localRotation;
			}
			[CompilerGenerated]
			set
			{
				localRotation = value;
			}
		}

		public Vector3 LocalScale
		{
			[CompilerGenerated]
			get
			{
				return localScale;
			}
			[CompilerGenerated]
			set
			{
				localScale = value;
			}
		}

		public Color Color
		{
			get
			{
				return sprite != null ? sprite.color : Color.white;
			}
			set
			{
				if (sprite != null)
				{
					sprite.color = value;
				}
			}
		}

		public float Alpha
		{
			get
			{
				return Color.a;
			}
			set
			{
				Color current = Color;
				current.a = value;
				Color = current;
			}
		}

		public void Initialize(GameObject targetObject)
		{
			sprite = targetObject != null ? targetObject.GetComponent<SpriteRenderer>() : null;
		}

		public TweenTargetColorNormal()
		{
			localScale = Vector3.one;
		}
	}
}
