using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
    public class CustomImage : Image
    {
        [SerializeField] private Object atlas;
        [SerializeField] private string spriteName;
        [SerializeField] private bool useSharedSprite = true;

        public float Alpha
        {
            get { return color.a; }
            set
            {
                Color c = color;
                c.a = value;
                color = c;
            }
        }

        public void SnapSize()
        {
            SetNativeSize();
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
