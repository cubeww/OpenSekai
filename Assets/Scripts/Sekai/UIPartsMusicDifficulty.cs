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
            if (_appendObject != null) _appendObject.SetActive(musicDifficulty == MusicDifficulty.Append);
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
