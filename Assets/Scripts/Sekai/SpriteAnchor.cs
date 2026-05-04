using UnityEngine;

namespace Sekai
{
    public class SpriteAnchor : MonoBehaviour
    {
        public enum VerticalAnchor
        {
            Top = 0,
            Middle = 1,
            Bottom = 2
        }

        public enum HoraizontalAnchor
        {
            Left = 0,
            Center = 1,
            Right = 2
        }

        public const float SAFEAREA_WIDTH = 60f;

        [SerializeField] private VerticalAnchor verticalAnchor;
        [SerializeField] private HoraizontalAnchor horaizontalAnchor;
    }
}
