using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
	public class ScreenLayerAssetRemoving : ScreenLayer
	{
		private readonly HashSet<Object> holdObjects = new HashSet<Object>();
		private int removeAssetNum;
		private int removedCount;

		public void Setup(int removeAssetNum)
		{
			this.removeAssetNum = Mathf.Max(0, removeAssetNum);
			removedCount = 0;
		}

		public void ResetGauge()
		{
			removedCount = 0;
		}

		public void UpdateGaugeProgress(int removeNum)
		{
			// TODO(original): drive the copied gauge view once its view scripts are restored.
			removedCount = Mathf.Clamp(removeNum, 0, removeAssetNum);
		}

		public void Release(Object holdObj)
		{
			if (holdObj != null)
			{
				holdObjects.Remove(holdObj);
			}
		}

		public void Hold(Object holdObj)
		{
			if (holdObj != null)
			{
				holdObjects.Add(holdObj);
			}
		}
	}
}
