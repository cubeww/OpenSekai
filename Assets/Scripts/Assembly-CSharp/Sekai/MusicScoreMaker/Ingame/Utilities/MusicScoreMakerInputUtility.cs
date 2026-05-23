namespace Sekai.MusicScoreMaker.Ingame.Utilities
{
	using UnityEngine;

	public static class MusicScoreMakerInputUtility
	{
		public static bool IsInputStarted()
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			{
				return true;
			}
			for (int i = 0; i < Input.touchCount; i++)
			{
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsInputEnded()
		{
			if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				return true;
			}
			if (Input.touchCount == 0)
			{
				return true;
			}
			for (int i = 0; i < Input.touchCount; i++)
			{
				TouchPhase phase = Input.GetTouch(i).phase;
				if (phase == TouchPhase.Ended || phase == TouchPhase.Canceled)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsMultipleInputStarted()
		{
			if ((Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
				|| (Input.GetMouseButtonDown(1) && Input.GetMouseButton(0)))
			{
				return true;
			}
			if (!Input.touchSupported || Input.touchCount < 2)
			{
				return false;
			}
			for (int i = 0; i < Input.touchCount; i++)
			{
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsMultipleInputActive()
		{
			if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
			{
				return true;
			}
			return Input.touchSupported && Input.touchCount > 1;
		}
	}
}
