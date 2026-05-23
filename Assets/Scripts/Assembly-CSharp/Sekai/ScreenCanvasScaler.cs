using UnityEngine.UI;

namespace Sekai
{
	public class ScreenCanvasScaler : CanvasScaler
	{
		public void ForceUpdate()
		{
			OnEnable();
			Handle();
		}

		public ScreenCanvasScaler()
		{
		}
	}
}
