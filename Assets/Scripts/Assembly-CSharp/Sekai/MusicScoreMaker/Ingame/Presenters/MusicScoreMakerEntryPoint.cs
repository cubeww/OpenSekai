using CP;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

namespace Sekai.MusicScoreMaker.Ingame.Presenters
{
	public class MusicScoreMakerEntryPoint : SceneEntryPoint
	{
		private const string EventSystemPrefabPath = "Common/Input/EventSystem";

		public class MusicScoreMakerBootData
		{
			public ScreenLayerMusicScoreMaker.BootArg bootData { get; set; }

			public MusicScoreMakerBootData()
			{
			}
		}

		public static MusicScoreMakerBootData BootData { get; set; }

		public ApplicationLocalSettings LocalSettings { get; set; }

		protected override void Awake()
		{
			LogUtility.WriteLog(1, "\u8b5c\u9762\u30c4\u30fc\u30eb\u3078\u9077\u79fb\u3057\u307e\u3057\u305f", System.Array.Empty<object>());
			base.Awake();
			EnsureEventSystem();
			Sekai.TextMeshProUtility.SetupBuiltinFontAsset();
			FramerateUtility.SetFrameRate();
			GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;

			BootData ??= new MusicScoreMakerBootData();

			AssetBundleMetaManager.Instance.Initialize();
			AssetBundleManager.Instance.Initialize();
			SoundUtility.SetupGlobalSeSettings(GlobalSeSettings.Normal);
		}

		protected override void Start()
		{
			LocalSettings = ApplicationLocalSettings.LoadFromStorage();
			if (LocalSettings.LiveVolume == null)
			{
				LocalSettings.LiveVolume = LocalSettings.SetupLiveVolume();
			}

			SoundManager.Instance.SetupVolume(
				1f,
				LocalSettings.SystemVolume.Bgm,
				LocalSettings.SystemVolume.Se,
				LocalSettings.SystemVolume.Voice);

			const string menuCommonSoundBundle = "sound/menu/menu_common";
			if (!SoundManager.Instance.IsLoadedSoundBundle(menuCommonSoundBundle))
			{
				SoundManager.Instance.LoadSoundBundle(menuCommonSoundBundle, true);
			}

			ScreenManager.Instance.CreateEmptyBaseCamera();

			SceneManager sceneManager = SceneManager.Instance;
			bool isEnteredFromTransitionBlank =
				sceneManager.CurrentScene == SceneManager.Scene.TransitionBlank ||
				sceneManager.PrevScene == SceneManager.Scene.TransitionBlank;
			if (isEnteredFromTransitionBlank)
			{
				ScreenManager.Instance.SetScreenCoverDirect(Color.black);
				ScreenManager.Instance.FadeIn(0f, 0.3f, null);
			}
			else
			{
				ScreenManager.Instance.FadeIn(0f, 0f, null);
			}

			ScreenManager.Instance.AddScreen(MenuScreenType.Header);
			ScreenManager.Instance.AddScreen(MenuScreenType.TouchEffect);
			ScreenManager.Instance.AddScreen(MenuScreenType.InsertNoti);

			var bootArg = BootData?.bootData;
			if (bootArg?.FinishTransitionCallback == null)
			{
				LiveTransitioner.SafeForceFinish(null);
			}

			if (bootArg == null)
			{
				ScreenManager.Instance.PushUIScreen(MenuScreenType.MusicScoreMakerTop, false);
				return;
			}

			ScreenManager.Instance.PushUIScreen(MenuScreenType.MusicScoreMaker, bootArg, false);
		}

		protected override void ExitScene()
		{
			base.ExitScene();
		}

		private static void EnsureEventSystem()
		{
			if (EventSystem.current != null || FindObjectOfType<EventSystem>() != null)
			{
				return;
			}

			// Original boot flow creates this in SekaiSingletonManager.OnInitialize.
			// MusicScoreMaker.unity is entered directly in OpenSekai, so mirror just that required input setup here.
			GameObject prefab = Resources.Load<GameObject>(EventSystemPrefabPath);
			GameObject eventSystemObject = prefab != null
				? Instantiate(prefab)
				: new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
			eventSystemObject.name = "EventSystem";
			DontDestroyOnLoad(eventSystemObject);
		}

		public void OnApplicationPause(bool pause)
		{
			if (pause)
			{
				SoundManager.Instance.Pause();
			}
			else
			{
				SoundManager.Instance.Resume();
			}
		}

		public MusicScoreMakerEntryPoint()
		{
		}
	}
}
