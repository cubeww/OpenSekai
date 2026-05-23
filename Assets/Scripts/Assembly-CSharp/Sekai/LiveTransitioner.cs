using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sekai.MusicScoreMaker.OutGame;
using UnityEngine;

namespace Sekai
{
	public class LiveTransitioner : MonoBehaviour
	{
		public enum TransitionType
		{
			Live = 0,
			VirtualArea = 1,
			ForceWhite = 2,
			White = 3,
			MusicScoreMakerLoading = 4
		}

		private const string PrefabPath = "Common/Transition/LiveTransitioner";
		private const string MusicScoreMakerLoadingPrefabPath = "Prefabs/Common/MusicScoreMakerLoading";
		private const string DefaultTransitionSe = "SE_AREA_TRANSITION_SEKAI";
		private const string MusicScoreMakerLoadingSe = "SE_OPEN_PLAYLIST";
		private const float DefaultFadeDelay = 1f;
		private const float DefaultFadeDuration = 1f;

		private static LiveTransitioner instance;

		[SerializeField]
		private RectTransform contentRoot;

		[SerializeField]
		private ColorFader coverFader;

		[SerializeField]
		private GameObject effectPrefab;

		[SerializeField]
		private RectTransform effectEmitRoot;

		[SerializeField]
		private Canvas canvas;

		[SerializeField]
		private GameObject loadingContent;

		private ParticleSystem startParticle;
		private Coroutine destroyCoroutine;
		private MusicScoreMakerLoading musicScoreMakerLoading;
		private Func<float, UniTask> onTransitionBGMStop;

		public static LiveTransitioner Instance => instance;

		public static bool Exists => instance != null;

		public GameObject ContentObject => loadingContent;

		public static LiveTransitioner Create()
		{
			var prefab = Resources.Load<GameObject>(PrefabPath);
			if (prefab == null)
			{
				Debug.LogWarningFormat("LiveTransitioner prefab could not be loaded. path:{0}", PrefabPath);
				return null;
			}

			var transitioner = Instantiate(prefab);
			transitioner.name = prefab.name;
			return transitioner.GetComponent<LiveTransitioner>();
		}

		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(instance.gameObject);
			}

			instance = this;
			DontDestroyOnLoad(gameObject);
			ShowLoadingContent(false);
		}

		private void OnDestroy()
		{
			if (instance == this)
			{
				instance = null;
			}
		}

		public static void SafeFinish(Action onFinished)
		{
			SafeFinish(onFinished, null);
		}

		public static void SafeFinish(Action onFinished, Action onFinishFadeBGM)
		{
			if (instance == null)
			{
				return;
			}

			instance.Finish(onFinished, onFinishFadeBGM);
		}

		public static void SafeForceFinish(Action onFinished)
		{
			if (instance == null)
			{
				return;
			}

			instance.ForceFinish(onFinished);
		}

		public void Play(Action onFinished)
		{
			Play(onFinished, DefaultTransitionSe, false, 0f);
		}

		public void Play(Action onFinished, string seName, bool showsLoading, float timeout)
		{
			Play(onFinished, seName, showsLoading, timeout, DefaultFadeDelay, DefaultFadeDuration);
		}

		public void Play(TransitionType type, Action onFinished, string seName = DefaultTransitionSe, bool showsLoading = false, float timeout = 0f, float delay = DefaultFadeDelay, float duration = DefaultFadeDuration)
		{
			switch (type)
			{
				case TransitionType.ForceWhite:
					PlayForceWhite(onFinished, seName, showsLoading, timeout);
					break;
				case TransitionType.White:
					PlayWhiteOut(onFinished, showsLoading, timeout, delay, duration);
					break;
				case TransitionType.MusicScoreMakerLoading:
					PlayMusicScoreMakerLoadingTransition(onFinished, delay, duration);
					break;
				default:
					Play(onFinished, seName, showsLoading, timeout, delay, duration);
					break;
			}
		}

		private void Play(Action onFinished, string seName, bool showsLoading, float timeout, float delay, float duration)
		{
			gameObject.SetActive(true);
			ShowLoadingContent(false);

			if (coverFader != null)
			{
				coverFader.gameObject.SetActive(true);
				coverFader.Set(ColorUtility.WHITE_ALPHA_0);
			}

			if (effectPrefab != null && effectEmitRoot != null)
			{
				var effect = Instantiate(effectPrefab, effectEmitRoot);
				startParticle = effect.transform.childCount > 0
					? effect.transform.GetChild(0).GetComponent<ParticleSystem>()
					: effect.GetComponentInChildren<ParticleSystem>(true);
				if (startParticle != null)
				{
					startParticle.Play(true);
				}
			}

			if (!string.IsNullOrEmpty(seName))
			{
				SoundManager.Instance.PlaySEOneShot(seName);
			}

			coverFader?.Stop();
			Action finish = () =>
			{
				if (startParticle != null)
				{
					startParticle.Pause(true);
				}

				onFinished?.Invoke();
				ShowLoadingContent(showsLoading);
			};

			if (coverFader != null)
			{
				coverFader.Play(ColorUtility.WHITE_ALPHA_1, delay, duration, finish);
			}
			else
			{
				StartCoroutine(InvokeNextFrame(finish));
			}

			if (timeout > 0f)
			{
				StopDestroyCoroutine();
				destroyCoroutine = StartCoroutine(DestroyAfter(timeout));
			}
		}

		private void PlayForceWhite(Action onFinished, string seName = DefaultTransitionSe, bool showsLoading = false, float timeout = 0f)
		{
			gameObject.SetActive(true);
			if (coverFader != null)
			{
				coverFader.gameObject.SetActive(true);
				coverFader.Set(ColorUtility.WHITE_ALPHA_1);
			}

			if (!string.IsNullOrEmpty(seName))
			{
				SoundManager.Instance.PlaySEOneShot(seName);
			}

			onFinished?.Invoke();
			ShowLoadingContent(showsLoading);
			if (timeout > 0f)
			{
				StopDestroyCoroutine();
				destroyCoroutine = StartCoroutine(DestroyAfter(timeout));
			}
		}

		public void PlayWhiteOut(Action onFinished, bool showsLoading = false, float timeout = 0f, float delay = DefaultFadeDelay, float duration = DefaultFadeDuration)
		{
			gameObject.SetActive(true);
			ShowLoadingContent(false);
			if (coverFader != null)
			{
				coverFader.gameObject.SetActive(true);
				coverFader.Set(ColorUtility.WHITE_ALPHA_0);
				coverFader.Play(ColorUtility.WHITE_ALPHA_1, delay, duration, () =>
				{
					onFinished?.Invoke();
					ShowLoadingContent(showsLoading);
				});
			}
			else
			{
				StartCoroutine(InvokeNextFrame(onFinished));
			}

			if (timeout > 0f)
			{
				StopDestroyCoroutine();
				destroyCoroutine = StartCoroutine(DestroyAfter(timeout));
			}
		}

		private void PlayMusicScoreMakerLoadingTransition(Action onFinished, float delay, float duration)
		{
			gameObject.SetActive(true);
			ShowLoadingContent(false);
			if (coverFader != null)
			{
				coverFader.gameObject.SetActive(true);
				coverFader.Set(ColorUtility.WHITE_ALPHA_0);
				coverFader.Play(ColorUtility.WHITE_ALPHA_1, delay, duration, null);
			}

			SoundManager.Instance.PlaySEOneShot(MusicScoreMakerLoadingSe);

			var prefab = Resources.Load<GameObject>(MusicScoreMakerLoadingPrefabPath);
			if (prefab == null || contentRoot == null)
			{
				StartCoroutine(InvokeNextFrame(onFinished));
				return;
			}

			var loadingObject = Instantiate(prefab, contentRoot);
			loadingObject.name = prefab.name;
			musicScoreMakerLoading = loadingObject.GetComponent<MusicScoreMakerLoading>();
			if (musicScoreMakerLoading == null)
			{
				StartCoroutine(InvokeNextFrame(onFinished));
				return;
			}

			PlayMusicScoreMakerLoadingAnimationAsync(onFinished).Forget();
		}

		private async UniTask PlayMusicScoreMakerLoadingAnimationAsync(Action onFinished)
		{
			var token = this.GetCancellationTokenOnDestroy();
			musicScoreMakerLoading.FadeOutAsync(0f, 0f, token).Forget();
			await UniTask.Delay(100, cancellationToken: token).SuppressCancellationThrow();
			if (musicScoreMakerLoading == null || token.IsCancellationRequested)
			{
				onFinished?.Invoke();
				return;
			}

			musicScoreMakerLoading.PlayLoopAsync(token).Forget();
			await UniTask.Delay(500, cancellationToken: token).SuppressCancellationThrow();
			if (!token.IsCancellationRequested)
			{
				onFinished?.Invoke();
			}
		}

		public void Finish(Action onFinished = null, Action onFinishFadeBGM = null)
		{
			StopDestroyCoroutine();
			if (musicScoreMakerLoading != null)
			{
				DisposeMusicScoreMakerLoadingAsync(onFinished, onFinishFadeBGM).Forget();
				return;
			}

			if (loadingContent != null)
			{
				loadingContent.SetActive(false);
			}

			if (coverFader != null)
			{
				coverFader.Stop();
				coverFader.Play(ColorUtility.WHITE_ALPHA_0, 1f, 1f, onFinished);
			}
			else
			{
				onFinished?.Invoke();
			}

			ResumePausedStartParticle();
			ScheduleDestroy(5f);
		}

		public void ForceFinish(Action onFinished = null)
		{
			StopDestroyCoroutine();
			if (musicScoreMakerLoading != null)
			{
				DisposeMusicScoreMakerLoading();
			}

			if (loadingContent != null)
			{
				loadingContent.SetActive(false);
			}

			if (coverFader != null)
			{
				coverFader.Stop();
				coverFader.Set(ColorUtility.WHITE_ALPHA_0);
			}

			ResumePausedStartParticle();
			DestroySelf();
			onFinished?.Invoke();
		}

		public void ShowLoadingContent(bool visible)
		{
			if (loadingContent != null)
			{
				loadingContent.SetActive(visible);
			}
		}

		private void StopDestroyCoroutine()
		{
			if (destroyCoroutine == null)
			{
				return;
			}

			StopCoroutine(destroyCoroutine);
			destroyCoroutine = null;
		}

		private IEnumerator InvokeNextFrame(Action action)
		{
			yield return null;
			action?.Invoke();
		}

		private IEnumerator DestroyAfter(float timeout)
		{
			yield return new WaitForSeconds(timeout);
			destroyCoroutine = null;
			ForceFinish(null);
		}

		private void ScheduleDestroy(float delay)
		{
			StopDestroyCoroutine();
			destroyCoroutine = StartCoroutine(DestroySelfAfter(delay));
		}

		private IEnumerator DestroySelfAfter(float delay)
		{
			if (delay > 0f)
			{
				yield return new WaitForSeconds(delay);
			}
			else
			{
				yield return null;
			}

			destroyCoroutine = null;
			DestroySelf();
		}

		private void DestroySelf()
		{
			if (instance == this)
			{
				instance = null;
			}

			Destroy(gameObject);
		}

		private void ResumePausedStartParticle()
		{
			if (startParticle != null && startParticle.isPaused && startParticle.IsAlive(true))
			{
				startParticle.Play(true);
			}
		}

		private void DisposeMusicScoreMakerLoading()
		{
			DestroyMusicScoreMakerLoading();
			if (onTransitionBGMStop != null)
			{
				onTransitionBGMStop(0f).Forget();
			}

			if (coverFader != null)
			{
				coverFader.Stop();
				coverFader.Play(ColorUtility.WHITE_ALPHA_0, 0f, 0f, null);
			}

			ScheduleDestroy(0f);
		}

		private async UniTask DisposeMusicScoreMakerLoadingAsync(Action onFinished, Action onFinishFadeBGM)
		{
			var token = this.GetCancellationTokenOnDestroy();
			if (musicScoreMakerLoading != null)
			{
				musicScoreMakerLoading.PlayIdle();
			}

			if (onTransitionBGMStop != null)
			{
				await onTransitionBGMStop(0.5f).AttachExternalCancellation(token).SuppressCancellationThrow();
				if (!token.IsCancellationRequested)
				{
					onFinishFadeBGM?.Invoke();
				}
			}

			FadeOutMusicScoreMakerLoadingAsync(token).Forget();
			await UniTask.Delay(TimeSpan.FromSeconds(0.15f), cancellationToken: token).SuppressCancellationThrow();
			if (token.IsCancellationRequested)
			{
				return;
			}

			if (coverFader != null)
			{
				coverFader.Stop();
				coverFader.Play(ColorUtility.WHITE_ALPHA_0, 0f, 0.5f, onFinished);
			}
			else
			{
				onFinished?.Invoke();
			}

			ScheduleDestroy(5f);
		}

		private async UniTask FadeOutMusicScoreMakerLoadingAsync(CancellationToken token)
		{
			MusicScoreMakerLoading loading = musicScoreMakerLoading;
			if (loading != null)
			{
				await loading.FadeOutAsync(0f, 0.5f, token).SuppressCancellationThrow();
			}

			if (!token.IsCancellationRequested)
			{
				DestroyMusicScoreMakerLoading();
			}
		}

		private void DestroyMusicScoreMakerLoading()
		{
			if (musicScoreMakerLoading == null)
			{
				return;
			}

			Destroy(musicScoreMakerLoading.gameObject);
			musicScoreMakerLoading = null;
		}
	}
}
