using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sekai
{
	public class ScreenLayerNetworkIndicator : ScreenLayer, IDisposable
	{
		public enum DisplayType
		{
			Landscape = 0,
			Portrait = 1
		}

		public enum LoadingMode
		{
			None = 0,
			Indicator = 1,
			Progress = 2
		}

		private readonly HashSet<Object> holdObjects = new HashSet<Object>();

		public static DisplayType CurrentDisplayType { get; set; } = DisplayType.Landscape;

		public void ShowCover()
		{
			gameObject.SetActive(true);
		}

		public void HideCover()
		{
			gameObject.SetActive(false);
		}

		public void UpdateDisplayType()
		{
			// TODO(original): swap landscape/portrait indicator roots.
		}

		public override void OnWillExit()
		{
			ScreenWillExitDone();
		}

		public void Hold(Object holdObject)
		{
			if (holdObject != null)
			{
				holdObjects.Add(holdObject);
			}
		}

		public void Release(Object holdObject)
		{
			if (holdObject != null)
			{
				holdObjects.Remove(holdObject);
			}
		}

		public void ForceRelease()
		{
			holdObjects.Clear();
		}

		public void UpdateDownloadProgress(float progress)
		{
			UpdateProgressGauge(progress, 1f);
		}

		public void UpdateProgressGauge(float currentVal, float maxVal)
		{
			// TODO(original): update progress gauge widgets.
		}

		public void Dispose()
		{
			ForceRelease();
		}
	}
}
