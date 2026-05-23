using UnityEngine;

public class UIBlurAnimator : MonoBehaviour
{
	private const float AnimationInterval = 2f;

	private UIBlurController controller;
	private float baseSamplingDistance;
	private float elapsed;

	private void Start()
	{
		controller = GetComponent<UIBlurController>();
		baseSamplingDistance = controller != null ? controller.BlurSamplingDistance : 1.5f;
	}

	private void Update()
	{
		if (controller == null)
		{
			return;
		}

		elapsed += Time.deltaTime;
		var t = Mathf.Sin(elapsed / AnimationInterval * Mathf.PI * 2f) * 0.5f + 0.5f;
		controller.BlurSamplingDistance = Mathf.Lerp(baseSamplingDistance * 0.9f, baseSamplingDistance * 1.1f, t);
	}
}
