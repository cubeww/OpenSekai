using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sekai
{
	public class SceneTransitionBlank : SceneEntryPoint
	{
		[SerializeField]
		private SceneTransitionLoadingIndicator loadingIndicatorPrefab;

		protected override void Start()
		{
			switch (SceneManager.Instance.NextScene)
			{
				case SceneManager.Scene.Core:
					StartCoroutine(ToInGame());
					break;
				case SceneManager.Scene.MusicScoreMaker:
					ToMusicScoreMaker().Forget();
					break;
				case SceneManager.Scene.OutGame:
				case SceneManager.Scene.Title:
				case SceneManager.Scene.VRMode:
				case SceneManager.Scene.VirtualLiveLobby:
				case SceneManager.Scene.SuperVirtualLiveLobby:
				case SceneManager.Scene.Mysekai:
					StartCoroutine(ToScene());
					break;
				default:
					SceneManager.Instance.RequestScene(SceneManager.Instance.NextScene);
					break;
			}
		}

		private IEnumerator ToInGame()
		{
			CreateLoadingIndicator();
			CleanUpMemory();
			yield return null;
			SceneManager.Instance.RequestScene(SceneManager.Instance.NextScene);
		}

		private IEnumerator ToScene()
		{
			CleanUpMemory();
			yield return null;
			SceneManager.Instance.RequestScene(SceneManager.Instance.NextScene);
		}

		private async UniTask ToMusicScoreMaker()
		{
			await CleanUpMemoryAsync();
			SceneManager.Instance.RequestScene(SceneManager.Instance.NextScene);
		}

		private void CreateLoadingIndicator()
		{
			if (loadingIndicatorPrefab != null)
			{
				Instantiate(loadingIndicatorPrefab);
			}
		}

		private void CleanUpMemory()
		{
			Resources.UnloadUnusedAssets();
			ForceGC();
		}

		private async UniTask CleanUpMemoryAsync()
		{
			AsyncOperation unloadOperation = Resources.UnloadUnusedAssets();
			while (unloadOperation != null && !unloadOperation.isDone)
			{
				await UniTask.Yield();
			}

			await ForceGCAsync();
		}

		private void ForceGC()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		private async UniTask ForceGCAsync()
		{
			await UniTask.Yield();
			ForceGC();
			await UniTask.Yield();
		}
	}
}
