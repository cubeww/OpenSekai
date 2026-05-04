using Sekai.UI;
using UnityEngine;

namespace Sekai
{
    public class UIPartsMusicCollaborationRibbon : MonoBehaviour
    {
        [SerializeField] private CustomTextMesh collaborationText;
        [SerializeField] private float collaborationTextPadding;
        [SerializeField] private float widthMax;
        [SerializeField] private CustomImage coverImage;

        public bool Lock { get; set; }

        public void SetText(string text)
        {
            if (collaborationText != null) collaborationText.text = text;
        }
    }
}
