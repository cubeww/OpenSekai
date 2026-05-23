using System;
using System.Collections;
using DG.Tweening;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class ScreenLayerLiveLoading : ScreenLayer
	{
		[SerializeField]
		private GameObject transitionerPrefab;

		[SerializeField]
		private SekaiBG background;

		[SerializeField]
		private CanvasGroup frontGroup;

		[SerializeField]
		private CustomImage progressBar;

		[SerializeField]
		private LoadingIndicatorAnimation gaugeAnimation;

		[SerializeField]
		private CustomImage whiteOutImage;

		private bool transitionStarted;
		private LiveTransitioner transitioner;

		protected override void OnInitComponent()
		{
			base.OnInitComponent();
			if (frontGroup != null)
			{
				frontGroup.alpha = 0f;
				DOTween.To(() => frontGroup.alpha, value => frontGroup.alpha = value, 1f, 0.2f).SetDelay(0.1f);
			}
			if (whiteOutImage != null)
			{
				Color color = whiteOutImage.color;
				color.a = 1f;
				whiteOutImage.color = color;
			}
			if (background != null)
			{
				background.Initialize();
				background.gameObject.SetActive(false);
			}
			UpdateProgress(0f);
			if (gaugeAnimation != null)
			{
				var gaugeCanvas = gaugeAnimation.GetComponent<Canvas>();
				if (gaugeCanvas != null)
				{
					gaugeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
					gaugeCanvas.overrideSorting = true;
					gaugeCanvas.sortingOrder = 1001;
				}
			}

			StartCoroutine(PlayStartWhiteOut());
			StartTransition(OnFinishTransitionFadeOut);
			StartCoroutine(CompleteGaugeAfterStartAnimation());
		}

		private IEnumerator PlayStartWhiteOut()
		{
			yield return new WaitForSeconds(0.2f);
			if (background != null)
			{
				background.gameObject.SetActive(true);
			}

			if (whiteOutImage == null)
			{
				yield break;
			}

			const float duration = 0.8f;
			var color = whiteOutImage.color;
			var elapsed = 0f;
			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;
				color.a = Mathf.Lerp(1f, 0f, Mathf.Clamp01(elapsed / duration));
				whiteOutImage.color = color;
				yield return null;
			}

			color.a = 0f;
			whiteOutImage.color = color;
		}

		private IEnumerator CompleteGaugeAfterStartAnimation()
		{
			yield return new WaitForSeconds(1.2f);
			UpdateProgress(1f);
		}

		private void UpdateProgress(float progress)
		{
			progress = Mathf.Clamp01(progress);
			if (progressBar != null)
			{
				progressBar.fillAmount = progress;
			}

			if (gaugeAnimation != null)
			{
				gaugeAnimation.PlayAnimation(progress);
			}
		}

		private void StartTransition(Action onFinished)
		{
			if (transitionStarted)
			{
				return;
			}

			transitionStarted = true;
			if (background != null)
			{
				background.Transition();
			}

			if (transitionerPrefab == null)
			{
				onFinished?.Invoke();
				return;
			}

			GameObject transitionerObject = Instantiate(transitionerPrefab);
			transitionerObject.name = transitionerPrefab.name;
			transitioner = transitionerObject.GetComponent<LiveTransitioner>();
			if (transitioner == null)
			{
				Destroy(transitionerObject);
				onFinished?.Invoke();
				return;
			}

			transitioner.Play(onFinished, "SE_AREA_TRANSITION_SEKAI", false, 0f);
		}

		private void OnFinishTransitionFadeOut()
		{
			SceneManager.Instance.RequestScene(SceneManager.Scene.Core);
		}
	}
}
