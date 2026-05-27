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
			bool isAppend = musicDifficulty == MusicDifficulty.append;
			if (isAppend)
			{
				if (_defaultImage != null)
				{
					_defaultImage.SetActive(false);
				}
				if (_appendObject != null)
				{
					_appendObject.SetActive(true);
				}
				return;
			}

			if (_defaultImage != null)
			{
				_defaultImage.color = ColorUtility.GetDifficultyColor(musicDifficulty);
				_defaultImage.SetActive(true);
			}
			if (_appendObject != null)
			{
				_appendObject.SetActive(false);
			}
		}

		public void Setup(string musicDifficulty)
		{
			if (!System.Enum.TryParse(musicDifficulty, true, out MusicDifficulty parsed))
			{
				return;
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
