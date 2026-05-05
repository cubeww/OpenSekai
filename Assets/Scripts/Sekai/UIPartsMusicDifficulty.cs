using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
    public class UIPartsMusicDifficulty : MonoBehaviour
    {
        [SerializeField] private CustomImage _defaultImage;
        [SerializeField] private GameObject _appendObject;

        public void Setup(MusicDifficulty musicDifficulty)
        {
            if (musicDifficulty == MusicDifficulty.Append)
            {
                if (_defaultImage != null) _defaultImage.SetActive(false);
                if (_appendObject != null) _appendObject.SetActive(true);
                return;
            }

            if (_defaultImage != null)
            {
                _defaultImage.color = ColorUtility.GetDifficultyColor(musicDifficulty);
                _defaultImage.SetActive(true);
            }

            if (_appendObject != null) _appendObject.SetActive(false);
        }

        public void Setup(string musicDifficulty)
        {
            if (Enum.TryParse(musicDifficulty, true, out MusicDifficulty parsed))
            {
                Setup(parsed);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
