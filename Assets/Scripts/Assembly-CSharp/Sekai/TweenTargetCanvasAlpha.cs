using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sekai
{
	public class TweenTargetCanvasAlpha : ITweenTargetComponent
	{
		private CanvasGroup canvasGroup;
		private Vector3 localPosition;
		private Vector3 localRotation;
		private Vector3 localScale;
		private Color color;

		public bool IsExist
		{
			get
			{
				return canvasGroup != null;
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
			get
			{
				return canvasGroup != null ? canvasGroup.alpha : 0f;
			}
			set
			{
				if (canvasGroup != null)
				{
					canvasGroup.alpha = value;
				}
			}
		}

		public void Initialize(GameObject targetObject)
		{
			canvasGroup = targetObject != null ? targetObject.GetComponent<CanvasGroup>() : null;
		}

		public TweenTargetCanvasAlpha()
		{
			localScale = Vector3.one;
			color = Color.white;
		}
	}
}
