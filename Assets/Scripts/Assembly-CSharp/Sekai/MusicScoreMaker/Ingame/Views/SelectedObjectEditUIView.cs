using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Input;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class SelectedObjectEditUIView : MonoBehaviour
	{
		[SerializeField]
		private DispatcherEventBaseButton _mirrorButton;

		[SerializeField]
		private DispatcherEventBaseButton _invertButton;

		[SerializeField]
		private DispatcherEventBaseButton _copyButton;

		[SerializeField]
		private DispatcherEventBaseButton _changeButton;

		[SerializeField]
		private DispatcherEventBaseButton _deleteButton;

		[SerializeField]
		private DispatcherEventBaseButton _selectAllConnectedNotesButton;

		[SerializeField]
		private ToolInputHandler _expandLeftInputHandler;

		[SerializeField]
		private ToolInputHandler _expandRightInputHandler;

		[SerializeField]
		private ToolInputHandler _moveInputHandler;

		[SerializeField]
		private RectTransform _buttonsContainer;

		[SerializeField]
		private float _screenMargin;

		private RectTransform _rectTransform;

		private RectTransform _parentCanvasRectTransform;

		private Vector2 _initialContainerPosition;

		private bool _isInitialPositionSet;

		private Vector3[] _worldCornersCache;

		private Vector3[] _canvasWorldCornersCache;

		private Vector3 _tempVector3Cache;

		private SelectedTargetOperation _selectedTargetOperation;

		private Vector2 _leftPressPosition;

		private Vector2 _rightPressPosition;

		private int _moveDragNoteId;

		private int _moveDragEventId;

		private bool _isLeftExpandInputDragging;

		private bool _isRightExpandInputDragging;

		private static bool _sIsExpandInputDragging;

		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
			Canvas canvas = GetComponentInParent<Canvas>();
			if (canvas != null)
			{
				_parentCanvasRectTransform = canvas.GetComponent<RectTransform>();
			}
			if (_expandLeftInputHandler != null)
			{
				_expandLeftInputHandler.AddListener(null, null, OnLeftExpandInputDrag, OnLeftExpandInputPointerDown, OnLeftExpandInputPointerUp);
			}
			if (_expandRightInputHandler != null)
			{
				_expandRightInputHandler.AddListener(null, null, OnRightExpandInputDrag, OnRightExpandInputPointerDown, OnRightExpandInputPointerUp);
			}
			if (_moveInputHandler != null)
			{
				_moveInputHandler.AddListener(OnMoveInputClick, null, OnMoveInputDrag, OnMoveInputPointerDown, OnMoveInputPointerUp);
			}
			SetupEventDispatcher();
		}

		private void OnDestroy()
		{
			if (_expandLeftInputHandler != null)
			{
				_expandLeftInputHandler.RemoveAllListeners();
			}
			if (_expandRightInputHandler != null)
			{
				_expandRightInputHandler.RemoveAllListeners();
			}
			if (_moveInputHandler != null)
			{
				_moveInputHandler.RemoveAllListeners();
			}
			RemoveEventDispatcher();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Register<ShowSelectedObjectEditUIViewEvent>(Show);
		}

		private void RemoveEventDispatcher()
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Remove<ShowSelectedObjectEditUIViewEvent>(Show);
			}
		}

		private void Show(ShowSelectedObjectEditUIViewEvent eventData)
		{
			if (eventData == null)
			{
				return;
			}
			if (!eventData.isShow)
			{
				gameObject.SetActive(false);
				_selectedTargetOperation = null;
				_isLeftExpandInputDragging = false;
				_isRightExpandInputDragging = false;
				_sIsExpandInputDragging = false;
				_moveDragNoteId = -1;
				_moveDragEventId = -1;
				return;
			}
			if (_rectTransform != null)
			{
				if (eventData.anchoredPosition.HasValue)
				{
					Vector2 position = eventData.anchoredPosition.Value;
					if (eventData.coordinateSpaceTransform != null && _rectTransform.parent is RectTransform parentRect)
					{
						Vector3 world = eventData.coordinateSpaceTransform.TransformPoint(new Vector3(position.x, position.y, 0f));
						position = parentRect.InverseTransformPoint(world);
					}
					_rectTransform.anchoredPosition = position;
				}
				if (eventData.sizeDelta.HasValue)
				{
					_rectTransform.sizeDelta = eventData.sizeDelta.Value;
				}
			}
			_selectedTargetOperation = eventData.selectedTargetOperation;
			_mirrorButton.SetActive(eventData.isMirror);
			_invertButton.SetActive(eventData.isInvert);
			_changeButton.SetActive(eventData.isChange);
			_copyButton.SetActive(eventData.isCopy);
			_deleteButton.SetActive(eventData.isDelete);
			_selectAllConnectedNotesButton.SetActive(eventData.isSelectAllConnectedNotes);
			_expandLeftInputHandler.SetActive(eventData.isLeftExpand);
			_expandRightInputHandler.SetActive(eventData.isRightExpand);
			gameObject.SetActive(eventData.isShow);
			if (!_isInitialPositionSet && _buttonsContainer != null)
			{
				_initialContainerPosition = _buttonsContainer.anchoredPosition;
				_isInitialPositionSet = true;
			}
			AdjustPositionToFitScreen();
		}

		private void OnMoveInputClick(PointerEventData eventData)
		{
			MusicScoreMakerData data = MusicScoreMakerUtility.GetMusicScoreMakerData();
			if (data?.SelectedNoteIdList != null && data.SelectedNoteIdList.Count == 1)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new OnNotePreviewClickEvent
				{
					NoteId = data.SelectedNoteIdList[0],
					PointerEventData = eventData
				});
			}
		}

		private void OnMoveInputPointerDown(PointerEventData eventData)
		{
			_moveDragNoteId = -1;
			_moveDragEventId = -1;
			MusicScoreMakerData data = MusicScoreMakerUtility.GetMusicScoreMakerData();
			if (data == null)
			{
				return;
			}
			if (data.SelectedNoteIdList != null && data.SelectedNoteIdList.Count > 0)
			{
				_moveDragNoteId = data.SelectedNoteIdList[0];
			}
			else if (data.SelectedTemporaryNoteIdList != null && data.SelectedTemporaryNoteIdList.Count > 0)
			{
				_moveDragNoteId = data.SelectedTemporaryNoteIdList[0];
			}
			else if (data.SelectedEventIdList != null && data.SelectedEventIdList.Count > 0)
			{
				_moveDragEventId = data.SelectedEventIdList[0];
			}
			else if (data.SelectedTemporaryEventIdList != null && data.SelectedTemporaryEventIdList.Count > 0)
			{
				_moveDragEventId = data.SelectedTemporaryEventIdList[0];
			}
		}

		private void OnMoveInputDrag(PointerEventData eventData)
		{
			if (_moveDragNoteId != -1)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new OnNotePreviewDragEvent
				{
					NoteId = _moveDragNoteId,
					NoteTapPosition = SelectedTargetOperation.NoteTapPosition.center,
					PointerEventData = eventData
				});
			}
			else if (_moveDragEventId != -1)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new OnEventPreviewDragEvent
				{
					Id = _moveDragEventId,
					PointerEventData = eventData
				});
			}
		}

		private void OnMoveInputPointerUp(PointerEventData eventData, bool isLongPress, bool isDragging)
		{
			if (_moveDragNoteId != -1)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new OnNotePreviewPointerUpEvent
				{
					NoteId = _moveDragNoteId,
					NoteTapPosition = SelectedTargetOperation.NoteTapPosition.center,
					PointerEventData = eventData,
					IsLongPress = isLongPress,
					IsDragging = isDragging
				});
			}
			else if (_moveDragEventId != -1)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new OnEventPreviewPointerUpEvent
				{
					Id = _moveDragEventId,
					PointerEventData = eventData,
					IsLongPress = isLongPress,
					IsDragging = isDragging
				});
			}
			_moveDragNoteId = -1;
			_moveDragEventId = -1;
		}

		public void SetSelectedTargetOperation(SelectedTargetOperation operation)
		{
			_selectedTargetOperation = operation;
		}

		private void OnLeftExpandInputPointerDown(PointerEventData eventData)
		{
			_leftPressPosition = eventData.position;
		}

		private void OnRightExpandInputPointerDown(PointerEventData eventData)
		{
			_rightPressPosition = eventData.position;
		}

		private void OnLeftExpandInputDrag(PointerEventData eventData)
		{
			_isLeftExpandInputDragging = true;
			_sIsExpandInputDragging = true;
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnExpandInputDragEvent
			{
				NoteTapPosition = SelectedTargetOperation.NoteTapPosition.left,
				PointerEventData = eventData,
				PressPosition = _leftPressPosition
			});
		}

		private void OnLeftExpandInputPointerUp(PointerEventData eventData, bool isLongPress, bool isDragging)
		{
			_isLeftExpandInputDragging = false;
			if (!_isRightExpandInputDragging)
			{
				_sIsExpandInputDragging = false;
			}
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnExpandInputPointerUpEvent
			{
				NoteTapPosition = SelectedTargetOperation.NoteTapPosition.left,
				PointerEventData = eventData,
				PressPosition = _leftPressPosition,
				IsLongPress = isLongPress,
				IsDragging = isDragging
			});
		}

		private void OnRightExpandInputDrag(PointerEventData eventData)
		{
			_isRightExpandInputDragging = true;
			_sIsExpandInputDragging = true;
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnExpandInputDragEvent
			{
				NoteTapPosition = SelectedTargetOperation.NoteTapPosition.right,
				PointerEventData = eventData,
				PressPosition = _rightPressPosition
			});
		}

		private void OnRightExpandInputPointerUp(PointerEventData eventData, bool isLongPress, bool isDragging)
		{
			_isRightExpandInputDragging = false;
			if (!_isLeftExpandInputDragging)
			{
				_sIsExpandInputDragging = false;
			}
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnExpandInputPointerUpEvent
			{
				NoteTapPosition = SelectedTargetOperation.NoteTapPosition.right,
				PointerEventData = eventData,
				PressPosition = _rightPressPosition,
				IsLongPress = isLongPress,
				IsDragging = isDragging
			});
		}

		public static bool IsExpandInputDragging()
		{
			return _sIsExpandInputDragging;
		}

		private void AdjustPositionToFitScreen()
		{
			if (_buttonsContainer == null || _parentCanvasRectTransform == null || !_isInitialPositionSet)
			{
				return;
			}
			_buttonsContainer.anchoredPosition = _initialContainerPosition;
			Canvas.ForceUpdateCanvases();
			if (CheckPositionFitsInScreen())
			{
				return;
			}
			_buttonsContainer.GetWorldCorners(_worldCornersCache);
			float bottom = _parentCanvasRectTransform.InverseTransformPoint(_worldCornersCache[0]).y;
			float top = _parentCanvasRectTransform.InverseTransformPoint(_worldCornersCache[2]).y;
			Rect canvasRect = _parentCanvasRectTransform.rect;
			float deltaY = 0f;
			float maxY = canvasRect.yMin + canvasRect.height - _screenMargin;
			if (top > maxY)
			{
				deltaY = maxY - top;
			}
			else
			{
				float minY = canvasRect.yMin + _screenMargin;
				if (bottom < minY)
				{
					deltaY = minY - bottom;
				}
			}
			if (!Mathf.Approximately(deltaY, 0f) && _buttonsContainer.parent is RectTransform parent)
			{
				Vector3 worldDelta = _parentCanvasRectTransform.TransformVector(new Vector3(0f, deltaY, 0f));
				float localDelta = parent.InverseTransformVector(worldDelta).y;
				Vector2 position = _buttonsContainer.anchoredPosition;
				position.y += localDelta;
				_buttonsContainer.anchoredPosition = position;
			}
		}

		private bool CheckPositionFitsInScreen()
		{
			if (_buttonsContainer == null || _parentCanvasRectTransform == null)
			{
				return true;
			}
			_buttonsContainer.GetWorldCorners(_worldCornersCache);
			float bottom = _parentCanvasRectTransform.InverseTransformPoint(_worldCornersCache[0]).y;
			float top = _parentCanvasRectTransform.InverseTransformPoint(_worldCornersCache[2]).y;
			Rect rect = _parentCanvasRectTransform.rect;
			return bottom >= rect.yMin + _screenMargin && top <= rect.yMin + rect.height - _screenMargin;
		}

		public SelectedObjectEditUIView()
		{
			_screenMargin = 10f;
			_worldCornersCache = new Vector3[4];
			_canvasWorldCornersCache = new Vector3[4];
			_moveDragNoteId = -1;
			_moveDragEventId = -1;
		}
	}
}
