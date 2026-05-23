using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Sekai
{
	public class SceneManager : MonoBehaviour
	{
		public enum Scene
		{
			None = 0,
			Title = 0,
			OutGame = 1,
			VRMode = 2,
			TransitionBlank = 3,
			Core = 4,
			VirtualLiveLobby = 5,
			Splash = 6,
			SuperVirtualLiveLobby = 7,
			Mysekai = 8,
			MusicScoreMaker = 9
		}

		private static SceneManager instance;

		public event System.Action OnExitScene;

		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(gameObject);
				return;
			}

			instance = this;
			DontDestroyOnLoad(gameObject);
			CurrentScene = GetActiveScene();
			PrevScene = CurrentScene;
			NextScene = CurrentScene;
		}

		public static SceneManager Instance
		{
			get
			{
				if (instance == null)
				{
					var go = GameObject.Find("SceneManager") ?? new GameObject("SceneManager");
					instance = go.GetComponent<SceneManager>() ?? go.AddComponent<SceneManager>();
				}

				return instance;
			}
		}

		public Scene CurrentScene { get; private set; } = Scene.MusicScoreMaker;
		public Scene PrevScene { get; private set; } = Scene.None;
		public Scene NextScene { get; private set; } = Scene.None;

		public void RequestScene(Scene scene)
		{
			NextScene = scene;
			Scene loadScene = ResolveLoadScene(scene);

			if (scene == Scene.MusicScoreMaker)
			{
				SoundManager.Instance.StopIngame();
			}

			string sceneName = GetSceneName(loadScene);
			if (!string.IsNullOrEmpty(sceneName))
			{
				if (ScreenManager.ExistsInstance && ScreenManager.Instance != null)
				{
					ScreenManager.Instance.ExitScene();
				}

				OnExitScene?.Invoke();
				UnitySceneManager.LoadScene(sceneName);
				PrevScene = CurrentScene;
				CurrentScene = loadScene;
			}
		}

		private Scene ResolveLoadScene(Scene requestedScene)
		{
			if (string.IsNullOrEmpty(GetSceneName(requestedScene)))
			{
				return requestedScene;
			}

			if (CurrentScene == Scene.TransitionBlank)
			{
				return requestedScene;
			}

			if (IsTransitionBlankSource(CurrentScene)
				&& (requestedScene == Scene.OutGame || requestedScene == Scene.MusicScoreMaker))
			{
				return Scene.TransitionBlank;
			}

			if (CurrentScene == Scene.OutGame && IsOutGameTransitionDestination(requestedScene))
			{
				return Scene.TransitionBlank;
			}

			if (CurrentScene == Scene.Mysekai
				&& (requestedScene == Scene.Title || requestedScene == Scene.OutGame || requestedScene == Scene.Mysekai))
			{
				return Scene.TransitionBlank;
			}

			return requestedScene;
		}

		private static bool IsTransitionBlankSource(Scene scene)
		{
			switch (scene)
			{
				case Scene.VRMode:
				case Scene.Core:
				case Scene.VirtualLiveLobby:
				case Scene.SuperVirtualLiveLobby:
				case Scene.MusicScoreMaker:
					return true;
				default:
					return false;
			}
		}

		private static bool IsOutGameTransitionDestination(Scene scene)
		{
			switch (scene)
			{
				case Scene.VRMode:
				case Scene.Core:
				case Scene.VirtualLiveLobby:
				case Scene.SuperVirtualLiveLobby:
				case Scene.Mysekai:
				case Scene.MusicScoreMaker:
					return true;
				default:
					return false;
			}
		}

		private static string GetSceneName(Scene scene)
		{
			switch (scene)
			{
				case Scene.Core:
					return "Core";
				case Scene.TransitionBlank:
					return "TransitionBlank";
				case Scene.MusicScoreMaker:
					return "MusicScoreMaker";
				default:
					return string.Empty;
			}
		}

		private static Scene GetActiveScene()
		{
			string sceneName = UnitySceneManager.GetActiveScene().name;
			if (System.Enum.TryParse(sceneName, out Scene scene))
			{
				return scene;
			}

			return Scene.None;
		}
	}
}
