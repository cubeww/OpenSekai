using TMPro;
using UnityEngine;

namespace Sekai.UI
{
    public class CustomTextMesh : TextMeshProUGUI
    {
        [SerializeField] private bool useWordingKey;
        [SerializeField] protected string wordingKey;
        [SerializeField] protected uint maxValueUpToAutoSize;

        public string Text => text;
        public RectTransform RectTransform => rectTransform;
        public float PreferredHeight => preferredHeight;

        public float FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
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

        public void UpdateWordingText() { }

        public void SetWordingKey(string key)
        {
            wordingKey = key;
        }

        public void SetWordingText(string key)
        {
            wordingKey = key;
            text = key;
        }

        public void SetWordingKey(string key, params object[] args)
        {
            wordingKey = key;
        }

        public void SetWordingText(string key, params object[] args)
        {
            wordingKey = key;
            text = args == null || args.Length == 0 ? key : string.Format(key, args);
        }

        public void SetDefaultFontDB() { }
        public void SetDefaultFontEB() { }

        public void SetText(string value, bool breakSpace = false)
        {
            text = value;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetMaterialOutlineColor(Color newColor) { }
        public void UpdateMaxValueUpToAutoSize() { }
    }
}
