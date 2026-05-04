using UnityEngine;

namespace Sekai
{
    public class NumberView : MonoBehaviour
    {
        public enum Align
        {
            Right = 0,
            Left = 1
        }

        [SerializeField] private Align align;
        [SerializeField] private float numberSpacing;
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color surplusColor = Color.clear;
        [SerializeField] private Sprite plusIconSprite;
        [SerializeField] private Sprite minusiconSprite;
        [SerializeField] private Sprite[] numberSprites;

        public void Setup(string defaultColorHtml, string surplusColorHtml) { }
        public void UpdateNumber(int value) { }
        public void UpdateAlpha(float alpha) { }
    }
}
