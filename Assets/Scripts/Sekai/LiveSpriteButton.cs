using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Sekai
{
    public class LiveSpriteButton : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float pressScale = 0.95f;
        [SerializeField] private float raycastPadding = 24f;

        private Action callback;
        private Camera targetCamera;
        private Bounds worldBounds;
        private Vector3 defaultScale;
        private bool isPressed;

        public void Setup(Action callback, Camera camera = null)
        {
            this.callback = callback;
            targetCamera = camera;
            defaultScale = transform.localScale;
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            CalculateBounds(camera);
        }

        public void CalculateBounds(Camera camera)
        {
            if (camera != null)
            {
                targetCamera = camera;
            }
            if (spriteRenderer != null)
            {
                worldBounds = spriteRenderer.bounds;
                float padding = GetWorldPadding();
                worldBounds.Expand(new Vector3(padding, padding, 0f));
            }
        }

        private void Update()
        {
            if (callback == null || spriteRenderer == null)
            {
                return;
            }

            ProcessMouseInput();
            ProcessTouchInput();
        }

        private void ProcessMouseInput()
        {
            Mouse mouse = Mouse.current;
            if (mouse == null)
            {
                return;
            }

            Vector2 screenPosition = mouse.position.ReadValue();
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Press(screenPosition);
            }
            else if (mouse.leftButton.wasReleasedThisFrame)
            {
                Release(screenPosition);
            }
        }

        private void ProcessTouchInput()
        {
            Touchscreen touchscreen = Touchscreen.current;
            if (touchscreen == null)
            {
                return;
            }

            foreach (TouchControl touch in touchscreen.touches)
            {
                Vector2 screenPosition = touch.position.ReadValue();
                if (touch.press.wasPressedThisFrame)
                {
                    Press(screenPosition);
                }
                else if (touch.press.wasReleasedThisFrame)
                {
                    Release(screenPosition);
                }
            }
        }

        private void Press(Vector3 screenPosition)
        {
            if (Contains(screenPosition))
            {
                isPressed = true;
                transform.localScale = defaultScale * pressScale;
            }
        }

        private void Release(Vector3 screenPosition)
        {
            bool execute = isPressed && Contains(screenPosition);
            isPressed = false;
            transform.localScale = defaultScale;
            if (execute)
            {
                callback();
            }
        }

        private bool Contains(Vector3 screenPosition)
        {
            Camera camera = targetCamera != null ? targetCamera : Camera.main;
            if (camera == null)
            {
                return false;
            }

            Vector3 worldPosition = camera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = worldBounds.center.z;
            return worldBounds.Contains(worldPosition);
        }

        private float GetWorldPadding()
        {
            Camera camera = targetCamera != null ? targetCamera : Camera.main;
            if (camera == null || camera.pixelHeight <= 0)
            {
                return 0f;
            }

            return camera.orthographic
                ? raycastPadding * camera.orthographicSize * 2f / camera.pixelHeight
                : 0f;
        }
    }
}
