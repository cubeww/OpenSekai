using System;
using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai
{
	public class BackKeyChecker
	{
		private Action<List<RaycastResult>> onBackKey;

		public CustomButton BackButton { get; set; }
		public bool EnableQuit { get; set; }

		public void Initialize(Action<List<RaycastResult>> onBackKey)
		{
			this.onBackKey = onBackKey;
		}

		public void Check()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ExecuteBackKey();
			}
		}

		private void ExecuteBackKey()
		{
			var results = new List<RaycastResult>();
			if (EventSystem.current != null)
			{
				var pointer = new PointerEventData(EventSystem.current)
				{
					position = Input.mousePosition
				};
				EventSystem.current.RaycastAll(pointer, results);
			}

			onBackKey?.Invoke(results);
		}
	}
}
