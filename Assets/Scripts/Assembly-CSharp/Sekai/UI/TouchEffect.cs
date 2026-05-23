using UnityEngine;

namespace Sekai.UI
{
	public class TouchEffect : MonoBehaviour
	{
		[SerializeField]
		private TouchTapEffect tapEffect;

		[SerializeField]
		private TouchLongTapEffect longTapEffect;

		[SerializeField]
		private TouchFollowEffect followEffect;

		public void PlayTapEffect(Vector3 worldPos)
		{
			UpdatePosition(tapEffect != null ? tapEffect.Root : null, worldPos);
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}

			tapEffect?.StartEffect();
			followEffect?.StopEffect();
			longTapEffect?.StopEffect();
		}

		public void StopTapEffect()
		{
			tapEffect?.StopEffect();
			gameObject.SetActive(false);
		}

		public void StartFollowEffect(Vector3 worldPos)
		{
			UpdatePosition(followEffect != null ? followEffect.Root : null, worldPos);
			followEffect?.StartEffect();
		}

		public void EndFollowEffect()
		{
			followEffect?.EndEffect();
		}

		private void StopFollowEffect()
		{
			followEffect?.StopEffect();
		}

		public bool CountLongTap()
		{
			return longTapEffect != null && longTapEffect.CountLongTap();
		}

		public void StartLongTapEffect(Vector3 worldPos)
		{
			UpdatePosition(followEffect != null ? followEffect.Root : null, worldPos);
			longTapEffect?.StartEffect();
		}

		public void EndLongTapEffect()
		{
			longTapEffect?.EndEffect();
		}

		private void StopLongTapEffect()
		{
			longTapEffect?.StopEffect();
		}

		private void UpdatePosition(Transform root, Vector3 worldPos)
		{
			if (root != null)
			{
				root.position = worldPos;
				var localPosition = root.localPosition;
				localPosition.z = 0f;
				root.localPosition = localPosition;
			}
		}

		public TouchEffect()
		{
		}
	}
}
