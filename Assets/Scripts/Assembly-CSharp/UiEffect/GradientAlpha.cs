using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UiEffect
{
	[AddComponentMenu("UI/Effects/Gradient Alpha")]
	[RequireComponent(typeof(Graphic))]
	public class GradientAlpha : BaseMeshEffect
	{
		private const int ONE_TEXT_VERTEX = 6;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_alphaTop;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_alphaBottom;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_alphaLeft;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_alphaRight;

		[SerializeField]
		[Range(-1f, 1f)]
		private float m_gradientOffsetVertical;

		[SerializeField]
		[Range(-1f, 1f)]
		private float m_gradientOffsetHorizontal;

		[SerializeField]
		private bool m_splitTextGradient;

		public float alphaTop
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public float alphaBottom
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public float alphaLeft
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public float alphaRight
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public float gradientOffsetVertical
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public float gradientOffsetHorizontal
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public bool splitTextGradient
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public override void ModifyMesh(VertexHelper vh)
		{
			throw null;
		}

		private void ModifyVertices(List<UIVertex> vList)
		{
			throw null;
		}

		private void Refresh()
		{
			throw null;
		}

		public GradientAlpha()
		{
			throw null;
		}
	}
}
