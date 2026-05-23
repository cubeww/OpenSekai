using UnityEngine;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class LaneLinePreview : MonoBehaviour
	{
		[SerializeField]
		private GameObject[] _laneLines;

		private RectTransform _rectTransform;

		public void Setup()
		{
			if (_rectTransform == null)
			{
				_rectTransform = GetComponent<RectTransform>();
			}
		}

		public void UpdateView(float scale, float centerX)
		{
			if (_rectTransform == null)
			{
				Setup();
			}
			if (_rectTransform == null || _laneLines == null)
			{
				return;
			}
			float width = _rectTransform.rect.width * scale;
			int laneCount = MusicScoreMakerModel.LaneCount;
			for (int i = 0; i <= laneCount && i < _laneLines.Length; i++)
			{
				GameObject laneLine = _laneLines[i];
				if (laneLine == null)
				{
					continue;
				}
				Vector3 localPosition = laneLine.transform.localPosition;
				localPosition.x = width * (i + laneCount * -0.5f) / laneCount + centerX;
				localPosition.y = _rectTransform.localPosition.y;
				localPosition.z = 0f;
				laneLine.transform.localPosition = localPosition;
			}
		}

		public LaneLinePreview()
		{
		}
	}
}
