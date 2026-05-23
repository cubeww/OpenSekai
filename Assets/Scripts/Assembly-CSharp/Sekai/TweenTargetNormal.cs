using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sekai
{
	public class TweenTargetNormal : ITweenTargetComponent
	{
		private Transform transform;
		private Color color;
		private float alpha;

		public bool IsExist
		{
			get
			{
				return transform != null;
			}
		}

		public Vector3 LocalPosition
		{
			get
			{
				return transform != null ? transform.localPosition : Vector3.zero;
			}
			set
			{
				if (transform != null)
				{
					transform.localPosition = value;
				}
			}
		}

		public Vector3 LocalRotation
		{
			get
			{
				return transform != null ? transform.localEulerAngles : Vector3.zero;
			}
			set
			{
				if (transform != null)
				{
					transform.localEulerAngles = value;
				}
			}
		}

		public Vector3 LocalScale
		{
			get
			{
				return transform != null ? transform.localScale : Vector3.one;
			}
			set
			{
				if (transform != null)
				{
					transform.localScale = value;
				}
			}
		}

		public Color Color
		{
			[CompilerGenerated]
			get
			{
				return color;
			}
			[CompilerGenerated]
			set
			{
				color = value;
			}
		}

		public float Alpha
		{
			[CompilerGenerated]
			get
			{
				return alpha;
			}
			[CompilerGenerated]
			set
			{
				alpha = value;
			}
		}

		public void Initialize(GameObject targetObject)
		{
			transform = targetObject != null ? targetObject.transform : null;
		}

		public TweenTargetNormal()
		{
			color = Color.white;
			alpha = 1f;
		}
	}
}
