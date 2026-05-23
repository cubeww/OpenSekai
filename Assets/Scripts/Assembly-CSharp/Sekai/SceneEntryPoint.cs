using UnityEngine;

namespace Sekai
{
	public class SceneEntryPoint : MonoBehaviour
	{
		protected virtual void Awake()
		{
			SceneManager.Instance.OnExitScene += ExitScene;
		}

		protected virtual void Start()
		{
		}

		protected virtual void ExitScene()
		{
			SceneManager.Instance.OnExitScene -= ExitScene;
		}
	}
}
