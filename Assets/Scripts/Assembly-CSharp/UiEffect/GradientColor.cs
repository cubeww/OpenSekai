using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UiEffect
{
	[AddComponentMenu("UI/Effects/Gradient Color")]
	[RequireComponent(typeof(Graphic))]
	public class GradientColor : BaseMeshEffect
	{
		private const int ONE_TEXT_VERTEX = 6;

		[SerializeField]
		private Color m_colorTop;

		[SerializeField]
		private Color m_colorBottom;

		[SerializeField]
		private Color m_colorLeft;

		[SerializeField]
		private Color m_colorRight;

		[Range(-1f, 1f)]
		[SerializeField]
		private float m_gradientOffsetVertical;

		[Range(-1f, 1f)]
		[SerializeField]
		private float m_gradientOffsetHorizontal;

		[SerializeField]
		private bool m_splitTextGradient;

		public Color colorTop
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

		public Color colorBottom
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

		public Color colorLeft
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

		public Color colorRight
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

		public GradientColor()
		{
			throw null;
		}
	}
}
