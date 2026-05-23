using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai
{
	public class TouchController : MonoBehaviour
	{
		public Action<Vector2> OnTouch;

		public Action<bool> OnHit;

		private GameObject hitTarget;

		private bool _isCheckChild;

		private void Update()
		{
			CheckTouch();
		}

		private void CheckTouch()
		{
			if (Input.touchCount >= 1)
			{
				Touch touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began)
				{
					OnTouch?.Invoke(touch.position);
					CheckHit(touch.position);
				}

				return;
			}

			if (Input.GetMouseButtonDown(0))
			{
				Vector2 position = Input.mousePosition;
				OnTouch?.Invoke(position);
				CheckHit(position);
			}
		}

		public void SetHitTarget(GameObject target, bool isCheckChild = false)
		{
			hitTarget = target;
			_isCheckChild = isCheckChild;
		}

		private void CheckHit(Vector2 touchPoint)
		{
			if (EventSystem.current == null)
			{
				OnHit?.Invoke(false);
				return;
			}

			List<RaycastResult> raycastResults = new List<RaycastResult>();
			PointerEventData eventData = new PointerEventData(EventSystem.current)
			{
				position = touchPoint
			};
			EventSystem.current.RaycastAll(eventData, raycastResults);
			OnHit?.Invoke(IsHit(raycastResults));
		}

		private bool IsHit(List<RaycastResult> raycastResults)
		{
			if (hitTarget == null || raycastResults == null)
			{
				return false;
			}

			foreach (RaycastResult result in raycastResults)
			{
				GameObject target = result.gameObject;
				if (target == null)
				{
					continue;
				}

				if (_isCheckChild)
				{
					if (target.transform.IsChildOf(hitTarget.transform))
					{
						return true;
					}
				}
				else if (target == hitTarget)
				{
					return true;
				}
			}

			return false;
		}

		public TouchController()
		{
		}
	}
}
