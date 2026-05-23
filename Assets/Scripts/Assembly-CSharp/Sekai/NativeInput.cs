using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;

namespace Sekai
{
	public static class NativeInput
	{
		private static readonly UnityEngine.Touch[] NoTouches;

		private static readonly UnityEngine.Touch[] PcTouches;

		public static ReadOnlyArray<UnityEngine.InputSystem.EnhancedTouch.Touch> ActiveTouches
		{
			get
			{
				EnsureEnhancedTouchEnabled();
				return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
			}
		}

		public static ReadOnlyArray<Finger> Fingers
		{
			get
			{
				EnsureEnhancedTouchEnabled();
				return UnityEngine.InputSystem.EnhancedTouch.Touch.fingers;
			}
		}

		public static UnityEngine.Touch[] Touches => Input.touches;

		static NativeInput()
		{
			NoTouches = new UnityEngine.Touch[0];
			PcTouches = new UnityEngine.Touch[1];
			PcTouches[0].fingerId = 0;
		}

		public static void Enable()
		{
			EnsureEnhancedTouchEnabled();
			Input.ResetInputAxes();
			foreach (InputDevice device in UnityEngine.InputSystem.InputSystem.devices)
			{
				UnityEngine.InputSystem.InputSystem.ResetDevice(device, false);
			}
		}

		public static void Disable()
		{
			if (EnhancedTouchSupport.enabled)
			{
				EnhancedTouchSupport.Disable();
			}
		}

		private static void EnsureEnhancedTouchEnabled()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				EnhancedTouchSupport.Enable();
			}
		}
	}
}
