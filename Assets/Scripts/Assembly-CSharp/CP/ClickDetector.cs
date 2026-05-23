using UnityEngine;
using UnityEngine.UI;

namespace CP
{
	[RequireComponent(typeof(CanvasRenderer))]
	public class ClickDetector : Graphic
	{
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
		}

		public ClickDetector()
		{
		}
	}
}
