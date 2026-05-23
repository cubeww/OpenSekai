using UnityEngine;

namespace Sekai
{
	public class LaneView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer defaultLaneBase;

		[SerializeField]
		private SpriteRenderer defaultLaneLine;

		[SerializeField]
		private SpriteRenderer defaultJudgeLine;

		public void Setup(LiveSettingData liveSetting)
		{
			if (liveSetting == null)
			{
				return;
			}
			float alpha = liveSetting.LaneTransparent;
			if (defaultLaneBase != null)
			{
				defaultLaneBase.color = new Color(1f, 1f, 1f, alpha);
				defaultLaneBase.enabled = alpha > 0f;
			}
			if (defaultLaneLine != null)
			{
				defaultLaneLine.enabled = true;
			}
			if (defaultJudgeLine != null)
			{
				defaultJudgeLine.enabled = true;
			}
		}

		public LaneView()
		{
		}
	}
}
