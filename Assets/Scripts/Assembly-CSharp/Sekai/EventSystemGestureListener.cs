using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Sekai
{
	public class EventSystemGestureListener : MonoBehaviour
	{
		[Serializable]
		public class GestureEvent : UnityEvent<PointerGestureData>
		{
			public GestureEvent()
			{
			}
		}

		public struct DragInfo
		{
			public Vector2 StartPosition;

			public float StartTime;

			public DragInfo(Vector3 pos, float time)
			{
				StartPosition = pos;
				StartTime = time;
			}
		}

		[SerializeField]
		private List<EventSystemGestureHandler> gestureHandlers;

		[SerializeField]
		private GestureEvent onPinchScale;

		[SerializeField]
		private GestureEvent onDrag;

		[SerializeField]
		private GestureEvent onDragEnd;

		[SerializeField]
		private GestureEvent onFlick;

		[SerializeField]
		private float singleDragThreshold;

		[SerializeField]
		private float flickThresholdTime;

		private List<PointerEventData> pointerList;

		private PointerGestureData gestureData;

		private float screenDistance;

		private bool isDragging;

		private bool isPrevFrameDragging;

		private Dictionary<int, DragInfo> dragInfo;

		private Action _onPinchScaleEnd;

		public int TouchCount
		{
			get
			{
				return pointerList?.Count ?? 0;
			}
		}

		public GestureEvent OnPinchScale
		{
			get
			{
				return onPinchScale;
			}
		}

		public GestureEvent OnSingleDrag
		{
			get
			{
				return onDrag;
			}
		}

		public GestureEvent OnDragEndEvent
		{
			get
			{
				return onDragEnd;
			}
		}

		public GestureEvent OnFlick
		{
			get
			{
				return onFlick;
			}
		}

		public float FlickThresholdTime
		{
			get
			{
				return flickThresholdTime;
			}
			set
			{
				flickThresholdTime = value;
			}
		}

		private void Awake()
		{
			Initialize();
		}

		public void AddGestureHandler(EventSystemGestureHandler handler)
		{
			if (handler == null)
			{
				return;
			}

			gestureHandlers ??= new List<EventSystemGestureHandler>();
			if (gestureHandlers.Contains(handler))
			{
				return;
			}

			gestureHandlers.Add(handler);
			RegisterCallBack(handler);
		}

		public void RemoveGestureHandler(EventSystemGestureHandler handler)
		{
			if (handler == null || gestureHandlers == null)
			{
				return;
			}

			RemoveCallBack(handler);
			gestureHandlers.Remove(handler);
		}

		public void CleanUpGestureHandler()
		{
			if (gestureHandlers == null)
			{
				return;
			}

			for (int i = gestureHandlers.Count - 1; i >= 0; i--)
			{
				if (gestureHandlers[i] == null)
				{
					gestureHandlers.RemoveAt(i);
				}
			}
		}

		public void ClearGestureHandler()
		{
			if (gestureHandlers == null)
			{
				return;
			}

			foreach (EventSystemGestureHandler handler in gestureHandlers)
			{
				RemoveCallBack(handler);
			}
			gestureHandlers.Clear();
		}

		public void SetOnPinchScaleEnd(Action onPinchScaleEnd)
		{
			_onPinchScaleEnd = onPinchScaleEnd;
		}

		private void RegisterCallBack(EventSystemGestureHandler handler)
		{
			if (handler == null)
			{
				return;
			}

			RemoveCallBack(handler);
			handler.OnPointerDownEvent?.AddListener(OnPointerDown);
			handler.OnPointerUpEvent?.AddListener(OnPointerUp);
			handler.OnBeginDragEvent?.AddListener(OnBeginDrag);
			handler.OnDragEvent?.AddListener(OnDrag);
			handler.OnEndDragEvent?.AddListener(OnEndDrag);
		}

		private void RemoveCallBack(EventSystemGestureHandler handler)
		{
			if (handler == null)
			{
				return;
			}

			handler.OnPointerDownEvent?.RemoveListener(OnPointerDown);
			handler.OnPointerUpEvent?.RemoveListener(OnPointerUp);
			handler.OnBeginDragEvent?.RemoveListener(OnBeginDrag);
			handler.OnDragEvent?.RemoveListener(OnDrag);
			handler.OnEndDragEvent?.RemoveListener(OnEndDrag);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			UpdatePointerInfo(eventData);
			if (eventData != null)
			{
				dragInfo[eventData.pointerId] = new DragInfo(eventData.position, Time.realtimeSinceStartup);
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			UpdatePointerInfo(eventData);
			if (eventData != null && dragInfo.TryGetValue(eventData.pointerId, out DragInfo info))
			{
				Vector2 flickVector = eventData.position - info.StartPosition;
				float elapsed = Time.realtimeSinceStartup - info.StartTime;
				gestureData.FlickVector = elapsed <= flickThresholdTime ? flickVector : Vector2.zero;
				gestureData.PointerEventData = eventData;
				if (gestureData.IsFlickLeft || gestureData.IsFlickRight || gestureData.IsFlickTop || gestureData.IsFlickBottom)
				{
					onFlick?.Invoke(gestureData);
				}
				dragInfo.Remove(eventData.pointerId);
			}

			if (pointerList.Count < 2)
			{
				_onPinchScaleEnd?.Invoke();
			}
		}

		[Skip]
		public void OnBeginDrag(PointerEventData eventData)
		{
			isDragging = true;
			UpdatePointerInfo(eventData);
		}

		[Skip]
		public void OnDrag(PointerEventData eventData)
		{
			UpdatePointerInfo(eventData);
			UpdateDragGestureData();
		}

		[Skip]
		public void OnEndDrag(PointerEventData eventData)
		{
			UpdatePointerInfo(eventData);
			UpdateDragGestureData();
			onDragEnd?.Invoke(gestureData);
			isDragging = false;
		}

		private void UpdatePointerInfo(PointerEventData eventData)
		{
			if (eventData == null)
			{
				return;
			}

			int index = pointerList.FindIndex(pointer => pointer != null && pointer.pointerId == eventData.pointerId);
			if (eventData.eligibleForClick || eventData.dragging || eventData.pointerPress != null)
			{
				if (index >= 0)
				{
					pointerList[index] = eventData;
				}
				else
				{
					pointerList.Add(eventData);
				}
			}
			else if (index >= 0)
			{
				pointerList.RemoveAt(index);
			}
		}

		private void UpdateDragGestureData()
		{
			gestureData.PrevPointerCount = gestureData.PointerCount;
			gestureData.PointerCount = pointerList.Count;
			gestureData.PointerEventData = pointerList.Count > 0 ? pointerList[0] : null;

			for (int i = 0; i < gestureData.ScreenPosition.Length; i++)
			{
				if (i < pointerList.Count && pointerList[i] != null)
				{
					gestureData.DragDelta[i] = pointerList[i].delta;
					gestureData.ScreenPosition[i] = pointerList[i].position;
				}
				else
				{
					gestureData.DragDelta[i] = Vector2.zero;
					gestureData.ScreenPosition[i] = Vector2.zero;
				}
			}

			if (pointerList.Count >= 2)
			{
				gestureData.PrevDistance = screenDistance;
				screenDistance = Vector2.Distance(pointerList[0].position, pointerList[1].position);
				gestureData.Distance = screenDistance;
				gestureData.DeltaScale = gestureData.PrevDistance > 0f ? screenDistance / gestureData.PrevDistance : 1f;
				gestureData.NormalizeDeltaScale = gestureData.DeltaScale - 1f;
				onPinchScale?.Invoke(gestureData);
			}
			else if (pointerList.Count == 1 && pointerList[0].delta.sqrMagnitude >= singleDragThreshold * singleDragThreshold)
			{
				onDrag?.Invoke(gestureData);
			}
		}

		private void Update()
		{
			isPrevFrameDragging = isDragging;
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				Initialize();
			}
		}

		public void Initialize()
		{
			gestureHandlers ??= new List<EventSystemGestureHandler>();
			onPinchScale ??= new GestureEvent();
			onDrag ??= new GestureEvent();
			onDragEnd ??= new GestureEvent();
			onFlick ??= new GestureEvent();
			pointerList ??= new List<PointerEventData>(2);
			dragInfo ??= new Dictionary<int, DragInfo>();
			pointerList.Clear();
			dragInfo.Clear();
			gestureData = new PointerGestureData();
			screenDistance = 0f;
			isDragging = false;
			isPrevFrameDragging = false;

			foreach (EventSystemGestureHandler handler in gestureHandlers.ToArray())
			{
				RegisterCallBack(handler);
			}
		}

		public EventSystemGestureListener()
		{
			singleDragThreshold = 10f;
			flickThresholdTime = 1f;
			pointerList = new List<PointerEventData>(2);
			dragInfo = new Dictionary<int, DragInfo>();
			onPinchScale = new GestureEvent();
			onDrag = new GestureEvent();
			onDragEnd = new GestureEvent();
			onFlick = new GestureEvent();
		}
	}
}
