using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using LegacyTouch = UnityEngine.Touch;
using LegacyTouchPhase = UnityEngine.TouchPhase;

namespace Sekai
{
	public static class NativeInput
	{
		private static readonly LegacyTouch[] NoTouches = new LegacyTouch[0];

		public static ReadOnlyArray<Touch> ActiveTouches
		{
			get
			{
				EnsureEnabled();
				return Touch.activeTouches;
			}
		}

		public static ReadOnlyArray<Finger> Fingers
		{
			get
			{
				EnsureEnabled();
				return Touch.fingers;
			}
		}

		public static LegacyTouch[] Touches
		{
			get
			{
				ReadOnlyArray<Touch> touches = ActiveTouches;
				if (touches.Count == 0)
				{
					return NoTouches;
				}

				LegacyTouch[] result = new LegacyTouch[touches.Count];
				for (int i = 0; i < touches.Count; i++)
				{
					result[i] = ToLegacyTouch(touches[i]);
				}
				return result;
			}
		}

		public static int GetTouchCount()
		{
			return ActiveTouches.Count;
		}

		public static LegacyTouch GetTouch(int index)
		{
			return ToLegacyTouch(ActiveTouches[index]);
		}

		public static void Enable()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				EnhancedTouchSupport.Enable();
			}
			UnityEngine.Input.ResetInputAxes();
			ResetDevices();
		}

		public static void Disable()
		{
			if (EnhancedTouchSupport.enabled)
			{
				EnhancedTouchSupport.Disable();
			}
		}

		public static void Reset()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				EnhancedTouchSupport.Enable();
			}
			UnityEngine.Input.ResetInputAxes();
			ResetDevices();
		}

		private static void ResetDevices()
		{
			foreach (InputDevice device in InputSystem.devices)
			{
				InputSystem.ResetDevice(device);
			}
		}

		private static void EnsureEnabled()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				EnhancedTouchSupport.Enable();
			}
		}

		private static LegacyTouch ToLegacyTouch(Touch touch)
		{
			LegacyTouch result = default;
			result.fingerId = touch.touchId;
			result.position = touch.screenPosition;
			result.rawPosition = touch.screenPosition;
			result.deltaPosition = touch.delta;
			result.deltaTime = UnityEngine.Time.unscaledDeltaTime;
			result.tapCount = touch.tapCount;
			result.phase = ToLegacyTouchPhase(touch.phase);
			result.pressure = touch.pressure;
			return result;
		}

		private static LegacyTouchPhase ToLegacyTouchPhase(UnityEngine.InputSystem.TouchPhase phase)
		{
			switch (phase)
			{
				case UnityEngine.InputSystem.TouchPhase.Began:
					return LegacyTouchPhase.Began;
				case UnityEngine.InputSystem.TouchPhase.Moved:
					return LegacyTouchPhase.Moved;
				case UnityEngine.InputSystem.TouchPhase.Stationary:
					return LegacyTouchPhase.Stationary;
				case UnityEngine.InputSystem.TouchPhase.Ended:
					return LegacyTouchPhase.Ended;
				case UnityEngine.InputSystem.TouchPhase.Canceled:
					return LegacyTouchPhase.Canceled;
				default:
					return LegacyTouchPhase.Canceled;
			}
		}
	}
}
