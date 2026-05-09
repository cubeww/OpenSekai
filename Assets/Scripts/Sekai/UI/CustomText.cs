using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
    public class CustomText : Text, ICustomText
    {
        [SerializeField] private bool useWordingKey;
        [SerializeField] protected string wordingKey;

        public string Text => text;
        public RectTransform RectTransform => rectTransform;
        public float PreferredHeight => preferredHeight;

        public float FontSize
        {
            get { return fontSize; }
            set { fontSize = (int)value; }
        }

        public string WordingKey
        {
            get { return wordingKey; }
            set { wordingKey = value; }
        }

        public bool UseWordingKey
        {
            get { return useWordingKey; }
            set { useWordingKey = value; }
        }

        public void UpdateWordingText()
        {
            if (useWordingKey && !string.IsNullOrEmpty(wordingKey))
            {
                text = WordingManager.Get(wordingKey);
            }
        }

        public void SetWordingKey(string key)
        {
            wordingKey = key;
        }

        public void SetWordingText(string key)
        {
            wordingKey = key;
            text = WordingManager.Get(key);
        }

        public void SetWordingKey(string key, params object[] args)
        {
            wordingKey = key;
        }

        public void SetWordingText(string key, params object[] args)
        {
            wordingKey = key;
            string value = WordingManager.Get(key);
            text = args == null || args.Length == 0 ? value : string.Format(value, args);
        }

        public void SetText(string value, bool breakSpace = false)
        {
            text = value;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
