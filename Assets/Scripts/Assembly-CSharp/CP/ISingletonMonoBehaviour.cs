using UnityEngine;

namespace CP
{
	public interface ISingletonMonoBehaviour
	{
		MonoBehaviour MonoBehaviour { get; }

		void SetupInstance();

		void OnFinalize();
	}
}
