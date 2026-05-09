using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai
{
    public sealed class DialogSizeFitter : MonoBehaviour
    {
        public enum ViewType
        {
            Default = 0,
            HasTab = 1
        }

        private static readonly Dictionary<DialogSize, float> SizeHeightByDialogSize = new Dictionary<DialogSize, float>
        {
            { DialogSize.Small, 520f },
            { DialogSize.Medium, 640f },
            { DialogSize.Large, 760f }
        };

        [SerializeField] private RectTransform windowRect;
        [SerializeField] private LayoutElement paddingLayoutElement;
        [SerializeField] private LayoutElement content;

        public void Setup(DialogSize dialogSize, ViewType viewType = ViewType.Default)
        {
            if (windowRect != null && SizeHeightByDialogSize.TryGetValue(dialogSize, out float height))
            {
                windowRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            }
        }
    }
}
