using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai
{
	public class TweenTargetColorUnityUI : ITweenTargetComponent
	{
		private Graphic graphic;
		private Vector3 localPosition;
		private Vector3 localRotation;
		private Vector3 localScale;

		public bool IsExist
		{
			get
			{
				return graphic != null;
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
				return graphic != null ? graphic.color : Color.white;
			}
			set
			{
				if (graphic != null)
				{
					graphic.color = value;
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
			graphic = targetObject != null ? targetObject.GetComponent<Graphic>() : null;
		}

		public TweenTargetColorUnityUI()
		{
			localScale = Vector3.one;
		}
	}
}
