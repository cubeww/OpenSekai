using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sekai.Core
{
	public class EntryPoint : MonoBehaviour
	{
		public static PlayMode PlayMode { get; set; }

		private void Awake()
		{
			CreateController().Forget();
		}

		private async UniTask<BaseController> CreateController()
		{
			await UniTask.WaitUntil(
				() => ShaderPreloader.Status != ShaderPreloader.PreloadStatus.Loading
					&& ShaderPreloader.Status != ShaderPreloader.PreloadStatus.Compiling,
				PlayerLoopTiming.Update);

			ShaderPreloader.Status = ShaderPreloader.PreloadStatus.None;
			SceneTransitionLoadingIndicator loadingIndicator = FindObjectOfType<SceneTransitionLoadingIndicator>();
			if (loadingIndicator != null)
			{
				Destroy(loadingIndicator.gameObject);
			}

			BaseController controller = null;
			switch (PlayMode)
			{
				case PlayMode.SoloLive:
					controller = gameObject.AddComponent<Live.SoloLiveController>();
					break;
				default:
					Debug.LogWarning($"Unsupported Core PlayMode: {PlayMode}");
					break;
			}

			return controller;
		}
	}
}
