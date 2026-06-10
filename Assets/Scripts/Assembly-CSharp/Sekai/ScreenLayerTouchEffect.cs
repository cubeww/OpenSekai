using System.Collections.Generic;
using Sekai.Extensions;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class ScreenLayerTouchEffect : ScreenLayer
	{
		private static readonly string DefaultLayerName = "UI";
		private static readonly string EffectOverlayLayerName = "EffectOverlay";

		[SerializeField]
		private TouchEffect[] touchEffects;

		private Dictionary<int, TouchEffect> touchEffectMap;
		private int playTapEffectIndex;
		private Camera uiCamera;
		private bool mousePressed;
		private Vector3 lastMousePosition;

		protected override void OnBoot(BootArgBase bootArg)
		{
			ScreenBootDone();
		}

		protected override void OnInitComponent()
		{
			uiCamera = ScreenManager.Instance != null ? ScreenManager.Instance.GetUICamera() : Camera.main;
			touchEffectMap = new Dictionary<int, TouchEffect>();
			if (touchEffects == null || touchEffects.Length == 0)
			{
				touchEffects = GetComponentsInChildren<TouchEffect>(true);
			}
		}

		public override void OnWillExit()
		{
			ScreenWillExitDone();
		}

		private void Update()
		{
			if (!EnsureInitialized())
			{
				return;
			}

			var touchCount = Input.touchCount;
			if (touchCount > 0)
			{
				UpdateTouches(touchCount);
				return;
			}

			UpdateMouseFallback();
		}

		public void EndAllEffect()
		{
			EndAllTapEffect();
			EndAllFlollowEffect();
			EndAllLongTapEffect();
			touchEffectMap?.Clear();
			mousePressed = false;
		}

		public void SetEffectLayer()
		{
			var layer = LayerMask.NameToLayer(EffectOverlayLayerName);
			if (layer >= 0)
			{
				gameObject.SetLayer(layer);
			}
		}

		public void SetDefaultLayer()
		{
			var layer = LayerMask.NameToLayer(DefaultLayerName);
			if (layer >= 0)
			{
				gameObject.SetLayer(layer);
			}
		}

		private void UpdateTouches(int touchCount)
		{
			var count = Mathf.Min(touchCount, touchEffects.Length);
			for (var i = 0; i < count; i++)
			{
				var touch = Input.GetTouch(i);
				var fingerId = touch.fingerId;
				var screenPos = new Vector3(touch.position.x, touch.position.y, 0f);
				if (touch.phase == TouchPhase.Began)
				{
					PlayTapEffect(fingerId, screenPos);
				}
				else if (touch.phase == TouchPhase.Moved)
				{
					StartFollowEffect(fingerId, screenPos);
				}
				else
				{
					EndFollowEffect(fingerId);
				}

				if (touch.phase == TouchPhase.Stationary && CountLongTap(fingerId))
				{
					StartLongTapEffect(fingerId, screenPos);
				}
				else
				{
					EndLongTapEffect(fingerId);
				}

				if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
				{
					// Original keeps the tap particle alive after release so the click animation can finish.
					// Tap effects are stopped only when the layer is cleared or the effect slot is reused.
				}
			}
		}

		private void UpdateMouseFallback()
		{
			const int mouseFingerId = 0;
			var screenPos = Input.mousePosition;
			screenPos.z = 0f;

			if (Input.GetMouseButtonDown(0))
			{
				mousePressed = true;
				lastMousePosition = screenPos;
				PlayTapEffect(mouseFingerId, screenPos);
				return;
			}

			if (!mousePressed)
			{
				return;
			}

			if (Input.GetMouseButton(0))
			{
				if (touchEffectMap == null || !touchEffectMap.ContainsKey(mouseFingerId))
				{
					PlayTapEffect(mouseFingerId, screenPos);
				}

				var isMoved = screenPos != lastMousePosition;
				if (isMoved)
				{
					StartFollowEffect(mouseFingerId, screenPos);
				}

				if (!isMoved && CountLongTap(mouseFingerId))
				{
					StartLongTapEffect(mouseFingerId, screenPos);
				}
				else
				{
					EndLongTapEffect(mouseFingerId);
				}

				lastMousePosition = screenPos;
				return;
			}

			EndFollowEffect(mouseFingerId);
			EndLongTapEffect(mouseFingerId);
			mousePressed = false;
		}

		private void PlayTapEffect(int fingerId, Vector3 screenPos)
		{
			if (!EnsureInitialized())
			{
				return;
			}

			playTapEffectIndex = Wrap(playTapEffectIndex, 0, touchEffects.Length);
			var touchEffect = touchEffects[playTapEffectIndex];
			if (touchEffect == null)
			{
				playTapEffectIndex = Wrap(playTapEffectIndex + 1, 0, touchEffects.Length);
				return;
			}

			touchEffect.PlayTapEffect(uiCamera.ScreenToWorldPoint(screenPos));
			if (touchEffectMap.ContainsKey(fingerId))
			{
				touchEffectMap[fingerId] = touchEffect;
			}
			else
			{
				touchEffectMap.Add(fingerId, touchEffect);
			}

			playTapEffectIndex = Wrap(playTapEffectIndex + 1, 0, touchEffects.Length);
		}

		private bool EnsureInitialized()
		{
			if (uiCamera == null)
			{
				uiCamera = ScreenManager.Instance != null ? ScreenManager.Instance.GetUICamera() : Camera.main;
			}

			if (touchEffectMap == null)
			{
				touchEffectMap = new Dictionary<int, TouchEffect>();
			}

			if (touchEffects == null || touchEffects.Length == 0)
			{
				touchEffects = GetComponentsInChildren<TouchEffect>(true);
				playTapEffectIndex = 0;
			}

			return uiCamera != null && touchEffects != null && touchEffects.Length > 0;
		}

		private void EndTapEffect(int fingerId)
		{
			if (touchEffectMap != null && touchEffectMap.TryGetValue(fingerId, out var touchEffect))
			{
				touchEffect.StopTapEffect();
			}
		}

		private void EndAllTapEffect()
		{
			if (touchEffects == null)
			{
				return;
			}

			foreach (var touchEffect in touchEffects)
			{
				touchEffect?.StopTapEffect();
			}
		}

		private void StartFollowEffect(int fingerId, Vector3 screenPos)
		{
			if (uiCamera != null && touchEffectMap != null && touchEffectMap.TryGetValue(fingerId, out var touchEffect))
			{
				touchEffect.StartFollowEffect(uiCamera.ScreenToWorldPoint(screenPos));
			}
		}

		private void EndFollowEffect(int fingerId)
		{
			if (touchEffectMap != null && touchEffectMap.TryGetValue(fingerId, out var touchEffect))
			{
				touchEffect.EndFollowEffect();
			}
		}

		private void EndAllFlollowEffect()
		{
			if (touchEffects == null)
			{
				return;
			}

			foreach (var touchEffect in touchEffects)
			{
				touchEffect?.EndFollowEffect();
			}
		}

		private bool CountLongTap(int fingerId)
		{
			return touchEffectMap != null &&
			       touchEffectMap.TryGetValue(fingerId, out var touchEffect) &&
			       touchEffect.CountLongTap();
		}

		private void StartLongTapEffect(int fingerId, Vector3 screenPos)
		{
			if (uiCamera != null && touchEffectMap != null && touchEffectMap.TryGetValue(fingerId, out var touchEffect))
			{
				touchEffect.StartLongTapEffect(uiCamera.ScreenToWorldPoint(screenPos));
			}
		}

		private void EndLongTapEffect(int fingerId)
		{
			if (touchEffectMap != null && touchEffectMap.TryGetValue(fingerId, out var touchEffect))
			{
				touchEffect.EndLongTapEffect();
			}
		}

		private void EndAllLongTapEffect()
		{
			if (touchEffects == null)
			{
				return;
			}

			foreach (var touchEffect in touchEffects)
			{
				touchEffect?.EndLongTapEffect();
			}
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			EndAllEffect();
		}

		private static int Wrap(int value, int min, int max)
		{
			if (max <= min)
			{
				return min;
			}

			var range = max - min;
			return ((value - min) % range + range) % range + min;
		}
	}
}
