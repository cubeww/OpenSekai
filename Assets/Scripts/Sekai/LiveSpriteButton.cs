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
        private int pressedPointerId = -1;

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

            if (!ProcessTouchInput())
            {
                ProcessMouseInput();
            }
        }

        private void ProcessMouseInput()
        {
            Mouse mouse = Mouse.current;
            if (mouse == null)
            {
                return;
            }

            Vector2 screenPosition = mouse.position.ReadValue();
            if (!IsValidScreenPosition(screenPosition))
            {
                return;
            }
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Press(-1, screenPosition);
            }
            else if (mouse.leftButton.wasReleasedThisFrame)
            {
                Release(-1, screenPosition);
            }
        }

        private bool ProcessTouchInput()
        {
            Touchscreen touchscreen = Touchscreen.current;
            if (touchscreen == null)
            {
                return false;
            }

            bool handled = false;
            foreach (TouchControl touch in touchscreen.touches)
            {
                bool pressedThisFrame = touch.press.wasPressedThisFrame;
                bool releasedThisFrame = touch.press.wasReleasedThisFrame;
                if (!pressedThisFrame && !releasedThisFrame)
                {
                    continue;
                }

                Vector2 screenPosition = touch.position.ReadValue();
                if (!IsValidScreenPosition(screenPosition))
                {
                    continue;
                }

                if (pressedThisFrame)
                {
                    Press(touch.touchId.ReadValue(), screenPosition);
                    handled = true;
                }
                else if (releasedThisFrame)
                {
                    Release(touch.touchId.ReadValue(), screenPosition);
                    handled = true;
                }
            }
            return handled;
        }

        private void Press(int pointerId, Vector3 screenPosition)
        {
            if (isPressed)
            {
                return;
            }

            bool contains = Contains(screenPosition);
            if (contains)
            {
                isPressed = true;
                pressedPointerId = pointerId;
                transform.localScale = defaultScale * pressScale;
            }
        }

        private void Release(int pointerId, Vector3 screenPosition)
        {
            if (!isPressed || pressedPointerId != pointerId)
            {
                return;
            }

            bool execute = isPressed && Contains(screenPosition);
            isPressed = false;
            pressedPointerId = -1;
            transform.localScale = defaultScale;
            if (execute)
            {
                callback();
            }
        }

        private bool Contains(Vector3 screenPosition)
        {
            Camera camera = targetCamera != null ? targetCamera : Camera.main;
            if (camera == null || !IsValidScreenPosition(screenPosition))
            {
                return false;
            }

            if (!TryScreenToWorldPosition(camera, screenPosition, out Vector3 worldPosition))
            {
                return false;
            }

            worldPosition.z = worldBounds.center.z;
            return worldBounds.Contains(worldPosition);
        }

        private bool TryScreenToWorldPosition(Camera camera, Vector3 screenPosition, out Vector3 worldPosition)
        {
            worldPosition = default;
            if (camera == null ||
                camera.pixelWidth <= 0 ||
                camera.pixelHeight <= 0 ||
                !IsValidScreenPosition(screenPosition))
            {
                return false;
            }

            // Unity Simulator can leave inactive virtual touches with invalid screen points.
            // Ray-plane hit testing avoids ScreenToWorldPoint throwing on those frames.
            Plane plane = new Plane(Vector3.forward, new Vector3(0f, 0f, worldBounds.center.z));
            Ray ray = camera.ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y, 0f));
            if (!IsValidWorldPosition(ray.origin) ||
                !IsValidWorldPosition(ray.direction) ||
                !plane.Raycast(ray, out float enter) ||
                !IsFinite(enter))
            {
                return false;
            }

            worldPosition = ray.GetPoint(enter);
            return IsValidWorldPosition(worldPosition);
        }

        private static bool IsValidScreenPosition(Vector3 screenPosition)
        {
            return IsFinite(screenPosition.x) &&
                IsFinite(screenPosition.y) &&
                IsFinite(screenPosition.z);
        }

        private static bool IsValidWorldPosition(Vector3 worldPosition)
        {
            return IsFinite(worldPosition.x) &&
                IsFinite(worldPosition.y) &&
                IsFinite(worldPosition.z);
        }

        private static bool IsFinite(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value);
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
