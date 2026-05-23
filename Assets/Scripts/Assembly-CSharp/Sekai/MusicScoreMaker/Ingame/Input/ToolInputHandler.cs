using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Input
{
	public class ToolInputHandler : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler, IDragHandler
	{
		public delegate void OnLongPressEvent(PointerEventData eventData);

		public delegate void OnClickEvent(PointerEventData eventData);

		public delegate void OnDragEvent(PointerEventData eventData);

		public delegate void OnPointerDownEvent(PointerEventData eventData);

		public delegate void OnPointerUpEvent(PointerEventData eventData, bool isLongPress, bool isDragging);

		public delegate void OnPinchEvent(float pinchDelta);

		private bool _isPointerDown;

		private bool _isLongPress;

		private bool _isDragging;

		private bool _isPinching;

		private bool _wasPinching;

		private float _pointerDownTimer;

		private float _requiredHoldTime;

		private PointerEventData _pointerDownEventData;

		private RectTransform _rectTransform;

		private float _previousPinchDistance;

		private int _touchCount;

		private int _activePointerId;

		private event OnLongPressEvent _onLongPressEvent;

		private event OnClickEvent _onClickEvent;

		private event OnDragEvent _onDragEvent;

		private event OnPointerDownEvent _onPointerDownEvent;

		private event OnPointerUpEvent _onPointerUpEvent;

		private event OnPinchEvent _onPinchEvent;

		public void RemoveAllAndAddListener(OnClickEvent onClickEvent = null, OnLongPressEvent onLongPressEvent = null, OnDragEvent onDragEvent = null, OnPointerDownEvent onPointerDownEvent = null, OnPointerUpEvent onPointerUpEvent = null, OnPinchEvent onPinchEvent = null)
		{
			RemoveAllListeners();
			AddListener(onClickEvent, onLongPressEvent, onDragEvent, onPointerDownEvent, onPointerUpEvent, onPinchEvent);
		}

		public void AddListener(OnClickEvent onClickEvent = null, OnLongPressEvent onLongPressEvent = null, OnDragEvent onDragEvent = null, OnPointerDownEvent onPointerDownEvent = null, OnPointerUpEvent onPointerUpEvent = null, OnPinchEvent onPinchEvent = null)
		{
			if (onClickEvent != null)
			{
				_onClickEvent += onClickEvent;
			}
			if (onLongPressEvent != null)
			{
				_onLongPressEvent += onLongPressEvent;
			}
			if (onDragEvent != null)
			{
				_onDragEvent += onDragEvent;
			}
			if (onPointerDownEvent != null)
			{
				_onPointerDownEvent += onPointerDownEvent;
			}
			if (onPointerUpEvent != null)
			{
				_onPointerUpEvent += onPointerUpEvent;
			}
			if (onPinchEvent != null)
			{
				_onPinchEvent += onPinchEvent;
			}
		}

		public void RemoveAllListeners()
		{
			_onClickEvent = null;
			_onLongPressEvent = null;
			_onDragEvent = null;
			_onPointerDownEvent = null;
			_onPointerUpEvent = null;
			_onPinchEvent = null;
		}

		private void OnDisable()
		{
			ResetPointerEventData();
		}

		private void Update()
		{
			OnLongPress();
			DetectPinch();
		}

		private void OnLongPress()
		{
			if (_onLongPressEvent == null || !_isPointerDown || _isLongPress || _isDragging)
			{
				return;
			}
			_pointerDownTimer += Time.deltaTime;
			if (_pointerDownTimer >= _requiredHoldTime)
			{
				_isLongPress = true;
				_onLongPressEvent?.Invoke(_pointerDownEventData);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (_isPointerDown)
			{
				return;
			}
			_onPointerDownEvent?.Invoke(eventData);
			SetPointerEventData(eventData);
		}

		private void SetPointerEventData(PointerEventData eventData)
		{
			if (eventData == null)
			{
				throw new ArgumentNullException(nameof(eventData));
			}
			_pointerDownEventData = eventData;
			_isPointerDown = true;
			_isLongPress = false;
			_isDragging = false;
			_wasPinching = false;
			_pointerDownTimer = 0f;
			_touchCount++;
			_activePointerId = eventData.pointerId;
		}

		private void ResetPointerEventData()
		{
			_pointerDownEventData = null;
			_isPointerDown = false;
			_isLongPress = false;
			_isDragging = false;
			_isPinching = false;
			_wasPinching = false;
			_pointerDownTimer = 0f;
			_previousPinchDistance = 0f;
			_touchCount = 0;
			_activePointerId = int.MinValue;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData == null)
			{
				throw new ArgumentNullException(nameof(eventData));
			}
			int pointerId = eventData.pointerId;
			_touchCount = Mathf.Max(_touchCount - 1, 0);
			if (_touchCount <= 1)
			{
				_isPinching = false;
				_previousPinchDistance = 0f;
			}
			if (pointerId == _activePointerId)
			{
				_onPointerUpEvent?.Invoke(eventData, _isLongPress, _isDragging);
				_isPointerDown = false;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData == null)
			{
				throw new ArgumentNullException(nameof(eventData));
			}
			if (eventData.pointerId != _activePointerId)
			{
				return;
			}
			if (!_isDragging && !_isLongPress && !_wasPinching)
			{
				_onClickEvent?.Invoke(eventData);
			}
			_wasPinching = false;
			_isPointerDown = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (_isPinching || !_isPointerDown)
			{
				return;
			}
			if (_wasPinching)
			{
				_wasPinching = false;
				return;
			}
			if (_isLongPress)
			{
				return;
			}
			_isDragging = true;
			_onDragEvent?.Invoke(eventData);
		}

		private void DetectPinch()
		{
			if (UnityEngine.Input.touchSupported && UnityEngine.Input.touchCount > 1 && UnityEngine.Input.touchCount == 2)
			{
				Touch touch0 = UnityEngine.Input.GetTouch(0);
				Touch touch1 = UnityEngine.Input.GetTouch(1);
				float distance = Vector2.Distance(touch0.position, touch1.position);
				if (!_isPinching)
				{
					_isPinching = true;
					_wasPinching = true;
					_previousPinchDistance = distance;
					return;
				}
				if (Mathf.Abs(distance - _previousPinchDistance) > 1f)
				{
					_onPinchEvent?.Invoke(distance - _previousPinchDistance);
					_previousPinchDistance = distance;
				}
			}
			else if (_isPinching)
			{
				_isPinching = false;
				_previousPinchDistance = 0f;
			}
		}

		public void SetActive(bool value)
		{
			gameObject.SetActive(value);
		}

		public ToolInputHandler()
		{
			_requiredHoldTime = 0.3f;
			_activePointerId = int.MinValue;
		}
	}
}
