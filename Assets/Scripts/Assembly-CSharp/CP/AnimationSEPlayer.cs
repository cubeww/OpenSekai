using UnityEngine;

namespace CP
{
	public class AnimationSEPlayer : MonoBehaviour
	{
		public void Play(string cueName)
		{
			if (!string.IsNullOrEmpty(cueName))
			{
				Sekai.SoundManager.Instance.PlaySEOneShot(cueName);
			}
		}

		// Restored animation clips use this exact event name.
		public void PLaySE(string cueName)
		{
			Play(cueName);
		}
	}
}
