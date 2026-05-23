using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class SekaiBG : MonoBehaviour
	{
		public enum Status
		{
			None = 0,
			Showing = 1,
			Show = 2,
			Closing = 3
		}

		[SerializeField]
		private CustomImage background;

		[SerializeField]
		private GameObject triParticle;

		private Status status;

		public bool IsActive => status == Status.Show || status == Status.Showing;

		public bool IsDuringTransition => status == Status.Showing || status == Status.Closing;

		public Status CurrentStatus => status;

		public void Initialize()
		{
			status = Status.None;
			SetTriParticleActive(false);
		}

		public void Show(System.Action onFinished, System.Action onShowUI)
		{
			status = Status.Showing;
			gameObject.SetActive(true);
			SetTriParticleActive(true);
			onShowUI?.Invoke();
			status = Status.Show;
			onFinished?.Invoke();
		}

		public void Transition()
		{
			gameObject.SetActive(true);
			SetTriParticleActive(true);
		}

		public void Hidden(System.Action onFinished)
		{
			status = Status.Closing;
			SetTriParticleActive(false);
			status = Status.None;
			onFinished?.Invoke();
		}

		public void SetColor(Color color)
		{
			if (background != null)
			{
				background.color = color;
			}
		}

		public void SetTriParticleActive(bool active)
		{
			if (triParticle != null)
			{
				triParticle.SetActive(active);
			}
		}
	}
}
