using System;
using UnityEngine;

namespace Sekai
{
    public class LiveSpriteButton : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float pressScale = 0.95f;
        [SerializeField] private float raycastPadding = 24f;

        public void Setup(Action callback, Camera camera = null) { }
        public void CalculateBounds(Camera camera) { }
    }
}
