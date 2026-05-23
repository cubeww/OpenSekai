using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.UI
{
	public class DropdownItemClickHandler : MonoBehaviour, IPointerClickHandler
	{
		private Action onClickCallback;

		public void SetCallback(Action callback)
		{
			onClickCallback = callback;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			onClickCallback?.Invoke();
		}
	}
}
