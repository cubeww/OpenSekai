using UnityEngine;

namespace Sekai
{
	public class LoadingIndicatorAnimation : MonoBehaviour
	{
		public enum State
		{
			Loading = 0,
			Complete = 1
		}

		[SerializeField]
		private Animator animator;

		private State currentState;
		private bool hasPlayed;

		public void PlayAnimation(float progress)
		{
			State nextState = progress > 0.98f ? State.Complete : State.Loading;
			if (hasPlayed && currentState == nextState)
			{
				return;
			}

			if (animator != null)
			{
				animator.Play(nextState == State.Complete ? "clip_UIPartsDownloadIndicator_complete" : "clip_UIPartsDownloadIndicator", 0, 0f);
			}
			currentState = nextState;
			hasPlayed = true;
		}

		public LoadingIndicatorAnimation()
		{
		}
	}
}
