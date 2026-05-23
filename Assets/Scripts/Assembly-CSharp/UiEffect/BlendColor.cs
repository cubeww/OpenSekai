using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UiEffect
{
	[RequireComponent(typeof(Graphic))]
	[AddComponentMenu("UI/Effects/Blend Color")]
	public class BlendColor : BaseMeshEffect
	{
		public enum BlendMode
		{
			Multiply = 0,
			Additive = 1,
			Subtractive = 2,
			Override = 3
		}

		[SerializeField]
		private BlendMode m_blendMode;

		[SerializeField]
		private Color m_color;

		public BlendMode blendMode
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

		public Color color
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

		public BlendColor()
		{
			throw null;
		}
	}
}
