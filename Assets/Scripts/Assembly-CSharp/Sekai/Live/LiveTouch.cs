using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sekai.Live
{
	[Serializable]
	public struct LiveTouch
	{
		public int fingerId;

		public int touchId;

		public Vector2 delta;

		public Vector3 worldPosition;

		public UnityEngine.InputSystem.TouchPhase phase;

		public float musicTime;

		public LiveTouch(int fingerId, int touchId, Vector2 delta, Vector3 worldPosition, UnityEngine.InputSystem.TouchPhase phase, float musicTime)
		{
			this.fingerId = fingerId;
			this.touchId = touchId;
			this.delta = delta;
			this.worldPosition = worldPosition;
			this.phase = phase;
			this.musicTime = musicTime;
		}

		public LiveTouch(int fingerId, int touchId, Vector2 delta, Vector3 worldPosition, UnityEngine.TouchPhase phase, float musicTime)
		{
			this.fingerId = fingerId;
			this.touchId = touchId;
			this.delta = delta;
			this.worldPosition = worldPosition;
			this.phase = phase switch
			{
				UnityEngine.TouchPhase.Began => UnityEngine.InputSystem.TouchPhase.Began,
				UnityEngine.TouchPhase.Moved => UnityEngine.InputSystem.TouchPhase.Moved,
				UnityEngine.TouchPhase.Stationary => UnityEngine.InputSystem.TouchPhase.Stationary,
				UnityEngine.TouchPhase.Ended => UnityEngine.InputSystem.TouchPhase.Ended,
				UnityEngine.TouchPhase.Canceled => UnityEngine.InputSystem.TouchPhase.Canceled,
				_ => UnityEngine.InputSystem.TouchPhase.None
			};
			this.musicTime = musicTime;
		}
	}
}
