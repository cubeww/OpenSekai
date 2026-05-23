using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsMusicDifficulty : MonoBehaviour
	{
		[SerializeField]
		private CustomImage _defaultImage;

		[SerializeField]
		private GameObject _appendObject;

		public void Setup(MusicDifficulty musicDifficulty)
		{
			if (_appendObject != null)
			{
				_appendObject.SetActive(musicDifficulty == MusicDifficulty.append);
			}
			Show();
		}

		public void Setup(string musicDifficulty)
		{
			if (!System.Enum.TryParse(musicDifficulty, true, out MusicDifficulty parsed))
			{
				parsed = MusicDifficulty.none;
			}
			Setup(parsed);
		}

		public virtual void Show()
		{
			gameObject.SetActive(true);
			if (_defaultImage != null)
			{
				_defaultImage.gameObject.SetActive(true);
			}
		}

		public virtual void Hide()
		{
			gameObject.SetActive(false);
		}

		public UIPartsMusicDifficulty()
		{
		}
	}
}
