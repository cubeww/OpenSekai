using UnityEngine.UI;

namespace CP
{
    public class ClickDetector : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}
