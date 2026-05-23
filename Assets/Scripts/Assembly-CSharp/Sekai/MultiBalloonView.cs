using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Sekai
{
	public class MultiBalloonView : MonoBehaviour
	{
		[SerializeField]
		protected UIPartsBalloon originalBalloonPrefab;

		private List<UIPartsBalloon> balloonList;

		private int currentBalloonIndex;

		private Sequence sequence;

		public void Setup(List<string> comments, float changeDuration = 3f)
		{
			throw null;
		}

		private void PlayChangeBalloonSequence(float changeDuration)
		{
			throw null;
		}

		private void ChangeNextBalloon()
		{
			throw null;
		}

		protected virtual UIPartsBalloon CreateBalloon(string comment)
		{
			throw null;
		}

		public void DeleteAllBalloon(float duration = 0f)
		{
			throw null;
		}

		public MultiBalloonView()
		{
		}
	}
}
