using System;
using UnityEngine;
using InputTouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Sekai.Live
{
	[Serializable]
	public struct LiveTouch
	{
		public int fingerId;

		public int touchId;

		public Vector2 delta;

		public Vector3 worldPosition;

		public InputTouchPhase phase;

		public float musicTime;

		public LiveTouch(int fingerId, int touchId, Vector2 delta, Vector3 worldPosition, InputTouchPhase phase, float musicTime)
		{
			this.fingerId = fingerId;
			this.touchId = touchId;
			this.delta = delta;
			this.worldPosition = worldPosition;
			this.phase = phase;
			this.musicTime = musicTime;
		}
	}
}
