using System;
using UnityEngine;

namespace Sekai.Live
{
	[Serializable]
	public struct LiveTouch
	{
		public int fingerId;

		public int touchId;

		public Vector2 delta;

		public Vector3 worldPosition;

		public TouchPhase phase;

		public float musicTime;

		public LiveTouch(int fingerId, int touchId, Vector2 delta, Vector3 worldPosition, TouchPhase phase, float musicTime)
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
