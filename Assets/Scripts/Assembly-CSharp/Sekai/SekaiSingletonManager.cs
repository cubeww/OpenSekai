using CP;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai
{
	public class SekaiSingletonManager : MonoBehaviour
	{
		private const string EventSystemPrefabPath = "Common/Input/EventSystem";

		private static SekaiSingletonManager instance;
		private static float deltaK = 1f;

		private EventSystem eventSystem;

		public static float DeltaTimeK => deltaK;

		private void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(gameObject);
				return;
			}

			instance = this;
			DontDestroyOnLoad(gameObject);
			OnInitialize();
		}

		protected virtual void OnInitialize()
		{
			FramerateUtility.SetFrameRate();
			EnsureEventSystem();
			TextMeshProUtility.SetupBuiltinFontAsset();
			AssetBundleMetaManager.Instance.Initialize();
			AssetBundleManager.Instance.Initialize();
			SoundUtility.SetupGlobalSeSettings(GlobalSeSettings.Normal);
		}

		private void Update()
		{
			deltaK = Time.timeScale * Time.deltaTime * Application.targetFrameRate;
		}

		public virtual void OnFinalize()
		{
			if (eventSystem != null)
			{
				Destroy(eventSystem.gameObject);
				eventSystem = null;
			}
		}

		private void OnDestroy()
		{
			if (instance != this)
			{
				return;
			}

			OnFinalize();
			instance = null;
		}

		private void OnApplicationQuit()
		{
			instance = null;
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (!pauseStatus)
			{
				System.GC.Collect();
			}
		}

		private void EnsureEventSystem()
		{
			eventSystem = EventSystem.current != null ? EventSystem.current : FindObjectOfType<EventSystem>();
			if (eventSystem != null)
			{
				return;
			}

			GameObject prefab = Resources.Load<GameObject>(EventSystemPrefabPath);
			GameObject eventSystemObject = prefab != null
				? Instantiate(prefab)
				: new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));

			eventSystemObject.name = prefab != null ? prefab.name : "EventSystem";
			DontDestroyOnLoad(eventSystemObject);
			eventSystem = eventSystemObject.GetComponent<EventSystem>();
		}
	}
}
