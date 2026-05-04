using UnityEngine;

namespace SafeArea
{
    public class SafeAreaAdjuster : MonoBehaviour
    {
        [SerializeField] private bool isAutoScale;
        [SerializeField] private bool isStretchX;
        [SerializeField] private bool isSafeNochArea;
        [SerializeField] private bool isIgnoreDefaultAdjustmentRate;
        [SerializeField] private bool isStretchY;

        private RectTransform panelRectTrans;

        public RectTransform PanelRectTrans
        {
            get
            {
                if (panelRectTrans == null)
                {
                    panelRectTrans = transform as RectTransform;
                }
                return panelRectTrans;
            }
        }

        public void Setup()
        {
            panelRectTrans = transform as RectTransform;
        }

        public void Apply()
        {
        }

        public void ApplyAndroid()
        {
            Apply();
        }
    }
}
