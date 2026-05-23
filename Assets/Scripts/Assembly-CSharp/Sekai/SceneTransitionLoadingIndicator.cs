using UnityEngine;

namespace Sekai
{
	public class SceneTransitionLoadingIndicator : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
