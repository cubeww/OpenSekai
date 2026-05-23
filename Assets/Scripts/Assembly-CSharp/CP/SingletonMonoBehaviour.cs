using UnityEngine;

namespace CP
{
	public class SingletonMonoBehaviour<T> : MonoBehaviour, ISingletonMonoBehaviour where T : SingletonMonoBehaviour<T>
	{
		private static T instance;

		public static T Instance
		{
			get
			{
				if (instance != null)
				{
					return instance;
				}

				instance = Object.FindObjectOfType<T>();
				if (instance != null)
				{
					instance.SetupInstance(instance);
					return instance;
				}

				GameObject gameObject = new GameObject(typeof(T).Name);
				instance = gameObject.AddComponent<T>();
				instance.SetupInstance(instance);
				return instance;
			}
		}

		public static bool ExistsInstance
		{
			get
			{
				return instance != null;
			}
		}

		public MonoBehaviour MonoBehaviour
		{
			get
			{
				return this;
			}
		}

		protected virtual void OnInitialize()
		{
		}

		public virtual void OnFinalize()
		{
		}

		public virtual bool IsDontDestroy()
		{
			return true;
		}

		public void SetupInstance()
		{
			SetupInstance((T)this);
		}

		private void SetupInstance(T target)
		{
			if (target == null)
			{
				return;
			}

			if (instance != null && instance != target)
			{
				Destroy(target.gameObject);
				return;
			}

			Initialize(target);
		}

		private void Initialize(T target)
		{
			instance = target;
			if (target.IsDontDestroy())
			{
				DontDestroyOnLoad(target.gameObject);
			}
			target.OnInitialize();
		}

		private void Destroyed(T target)
		{
			if (instance != target)
			{
				return;
			}

			target.OnFinalize();
			instance = null;
		}

		private void Awake()
		{
			SetupInstance((T)this);
		}

		protected virtual void OnDestroy()
		{
			Destroyed((T)this);
		}

		private void OnApplicationQuit()
		{
			if (instance == this)
			{
				instance = null;
			}
		}

		public SingletonMonoBehaviour()
		{
			}
	}
}
