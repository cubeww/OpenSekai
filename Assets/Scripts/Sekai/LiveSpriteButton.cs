using System;
using DG.Tweening;
using UnityEngine;

namespace Sekai
{
    public class LiveSpriteButton : MonoBehaviour
    {
        private struct ButtonBounds
        {
            public float left;
            public float bottom;
            public float right;
            public float top;
        }

        private static readonly int DisableId = int.MinValue;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] protected float pressScale = 0.95f;
        [SerializeField] protected float raycastPadding = 24f;

        private float scaleTime = 0.125f;
        private Action onClick;
        private bool isEnable;
        private int touchId;
        private ButtonBounds bounds;
        private Vector3 baseScale;
        private Tweener pressTween;

        public void Setup(Action onClick)
        {
            this.onClick = onClick;
            touchId = DisableId;
            baseScale = transform.localScale;
        }

        public void CalculateBounds(Camera camera)
        {
            isEnable = true;
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            if (spriteRenderer == null || camera == null)
            {
                return;
            }

            Bounds rendererBounds = spriteRenderer.bounds;
            Vector3 min = camera.WorldToScreenPoint(rendererBounds.min);
            Vector3 max = camera.WorldToScreenPoint(rendererBounds.max);
            bounds.left = min.x - raycastPadding;
            bounds.bottom = min.y - raycastPadding;
            bounds.right = max.x + raycastPadding;
            bounds.top = max.y + raycastPadding;
        }

        private void UpdateTouches()
        {
            if (!isEnable || NativeInput.GetTouchCount() < 1)
            {
                return;
            }

            for (int i = 0; i < NativeInput.GetTouchCount(); i++)
            {
                Touch touch = NativeInput.GetTouch(i);
                Vector2 position = touch.position;
                if (!IsValidScreenPosition(position))
                {
                    continue;
                }

                if (touch.phase == TouchPhase.Began)
                {
                    if (touchId == DisableId && Contains(position))
                    {
                        ScaleDown();
                        touchId = touch.fingerId;
                        return;
                    }
                }
                else if (touch.phase == TouchPhase.Ended && touchId == touch.fingerId)
                {
                    if (Contains(position))
                    {
                        OnClick();
                        ScaleUp();
                    }
                    touchId = DisableId;
                }
            }
        }

        private void ScaleDown()
        {
            KillPressTween();
            pressTween = transform.DOScale(baseScale * pressScale, scaleTime);
        }

        private void ScaleUp()
        {
            KillPressTween();
            pressTween = transform.DOScale(baseScale, scaleTime);
        }

        private void KillPressTween()
        {
            if (pressTween != null && pressTween.active)
            {
                pressTween.Kill();
            }
        }

        private void LateUpdate()
        {
            UpdateTouches();
        }

        public void OnClick()
        {
            onClick?.Invoke();
        }

        private bool Contains(Vector2 position)
        {
            return bounds.left <= position.x &&
                bounds.right >= position.x &&
                bounds.bottom <= position.y &&
                bounds.top >= position.y;
        }

        private static bool IsValidScreenPosition(Vector2 position)
        {
            return IsFinite(position.x) && IsFinite(position.y);
        }

        private static bool IsFinite(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value);
        }
    }
}
