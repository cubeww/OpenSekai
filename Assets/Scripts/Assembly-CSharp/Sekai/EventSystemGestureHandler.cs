using System;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Sekai
{
	public class EventSystemGestureHandler : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		[Serializable]
		public class GestureEvent : UnityEvent<PointerEventData>
		{
			public GestureEvent()
			{
			}
		}

		[SerializeField]
		private UnityEvent onClick;

		[SerializeField]
		private GestureEvent onClickDetail;

		[SerializeField]
		private GestureEvent onPointerDown;

		[SerializeField]
		private GestureEvent onPointerUp;

		[SerializeField]
		private GestureEvent onBeginDrag;

		[SerializeField]
		private GestureEvent onDrag;

		[SerializeField]
		private GestureEvent onEndDrag;

		[SerializeField]
		private bool enableDoubleClick;

		private float pressTime;

		private bool isDragging;

		public float PressTime
		{
			get
			{
				return pressTime;
			}
		}

		public UnityEvent OnClickEvent
		{
			get
			{
				return onClick;
			}
		}

		public GestureEvent OnClickDetailEvent
		{
			get
			{
				return onClickDetail;
			}
		}

		public GestureEvent OnPointerDownEvent
		{
			get
			{
				return onPointerDown;
			}
		}

		public GestureEvent OnPointerUpEvent
		{
			get
			{
				return onPointerUp;
			}
		}

		public GestureEvent OnBeginDragEvent
		{
			get
			{
				return onBeginDrag;
			}
		}

		public GestureEvent OnDragEvent
		{
			get
			{
				return onDrag;
			}
		}

		public GestureEvent OnEndDragEvent
		{
			get
			{
				return onEndDrag;
			}
		}

		private void Awake()
		{
		}

		[Skip]
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData == null)
			{
				return;
			}

			if (eventData.dragging || isDragging || Time.realtimeSinceStartup - pressTime > 0.45f)
			{
				return;
			}

			if (!enableDoubleClick && eventData.clickCount > 1)
			{
				return;
			}

			onClick?.Invoke();
			onClickDetail?.Invoke(eventData);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			pressTime = Time.realtimeSinceStartup;
			isDragging = false;
			onPointerDown?.Invoke(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			onPointerUp?.Invoke(eventData);
		}

		[Skip]
		public void OnBeginDrag(PointerEventData eventData)
		{
			isDragging = true;
			onBeginDrag?.Invoke(eventData);
		}

		[Skip]
		public void OnDrag(PointerEventData eventData)
		{
			onDrag?.Invoke(eventData);
		}

		[Skip]
		public void OnEndDrag(PointerEventData eventData)
		{
			isDragging = false;
			onEndDrag?.Invoke(eventData);
		}

		private void OnDestroy()
		{
			onClick?.RemoveAllListeners();
			onClickDetail?.RemoveAllListeners();
			onPointerDown?.RemoveAllListeners();
			onPointerUp?.RemoveAllListeners();
			onBeginDrag?.RemoveAllListeners();
			onDrag?.RemoveAllListeners();
			onEndDrag?.RemoveAllListeners();
		}

		public EventSystemGestureHandler()
		{
			onClick = new UnityEvent();
			onClickDetail = new GestureEvent();
			onPointerDown = new GestureEvent();
			onPointerUp = new GestureEvent();
			onBeginDrag = new GestureEvent();
			onDrag = new GestureEvent();
			onEndDrag = new GestureEvent();
		}
	}
}
