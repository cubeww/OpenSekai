using System;
using UnityEngine;

namespace Sekai
{
	public class CrossFader : MonoBehaviour
	{
		public enum FinishedStatus
		{
			Complete = 0,
			Cancel = 1
		}

		public void FadeIn(float duration, Action<FinishedStatus> onFinished = null)
		{
			onFinished?.Invoke(FinishedStatus.Complete);
		}

		public void FadeOut(float duration, Action<FinishedStatus> onFinished = null)
		{
			onFinished?.Invoke(FinishedStatus.Complete);
		}
	}
}
