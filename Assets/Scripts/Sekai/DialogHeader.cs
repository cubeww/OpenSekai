using Sekai.UI;
using UnityEngine;

namespace Sekai
{
    public class DialogHeader : MonoBehaviour
    {
        [SerializeField] private CustomTextMesh titleTextMesh;
        [SerializeField] private CustomText titleText;

        public void SetWordingKey(string key)
        {
            if (titleTextMesh != null)
            {
                titleTextMesh.SetWordingText(key);
            }

            if (titleText != null)
            {
                titleText.SetWordingText(key);
            }
        }

        public void SetText(string text)
        {
            if (titleTextMesh != null)
            {
                titleTextMesh.SetText(text);
            }

            if (titleText != null)
            {
                titleText.SetText(text);
            }
        }
    }
}
