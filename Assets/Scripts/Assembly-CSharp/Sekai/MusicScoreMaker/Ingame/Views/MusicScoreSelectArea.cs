using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreSelectArea : MonoBehaviour
	{
		private static readonly Vector2 CENTER_VECTOR;

		[SerializeField]
		private RectTransform _selectAreaView;

		[SerializeField]
		private RectTransform _startMarker;

		[SerializeField]
		private RectTransform _cursorMarker;

		private RectTransform _notesViewRectTransform;

		private PointerEventData _eventData;

		public void Setup(MusicScorePreview musicScorePreview)
		{
			_notesViewRectTransform = musicScorePreview != null ? musicScorePreview.NotesViewRectTransform : null;
			if (_selectAreaView != null)
			{
				_selectAreaView.anchorMin = CENTER_VECTOR;
				_selectAreaView.anchorMax = CENTER_VECTOR;
				_selectAreaView.pivot = Vector2.zero;
				_selectAreaView.gameObject.SetActive(false);
			}
			SetupMarkers();
			SetupEventDispatcher();
		}

		public void Dispose()
		{
			DisposeEventDispatcher();
			_eventData = null;
			SetActiveSelectArea(false);
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Register<UpdateMusicScoreSelectAreaEvent>(UpdateMusicScoreSelectArea);
		}

		private void DisposeEventDispatcher()
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Remove<UpdateMusicScoreSelectAreaEvent>(UpdateMusicScoreSelectArea);
			}
		}

		private void UpdateMusicScoreSelectArea(UpdateMusicScoreSelectAreaEvent obj)
		{
			UpdateAreaRect();
		}

		public void SetSelectAreaRect(PointerEventData eventData)
		{
			_eventData = eventData;
			SetSelectAreaRect();
		}

		public void SetSelectAreaRect()
		{
			if (_eventData == null || _selectAreaView == null || _notesViewRectTransform == null)
			{
				SetActiveSelectArea(false);
				return;
			}
			if ((_eventData.pressPosition - _eventData.position).sqrMagnitude < 1.0e-10f)
			{
				SetActiveSelectArea(false);
				return;
			}

			Rect selectionRect = CalcSelectionRect(out Vector2 startPos, out Vector2 endPos);
			_selectAreaView.anchoredPosition = selectionRect.position;
			_selectAreaView.sizeDelta = selectionRect.size;
			UpdateMarkerPositionsFromRect(selectionRect, startPos, endPos);
			SetActiveSelectArea(true);
		}

		private Rect CalcSelectionRect(out Vector2 startPos, out Vector2 endPos)
		{
			RectTransform parentRect = _selectAreaView != null ? _selectAreaView.parent as RectTransform : null;
			float scrollDiffY = MusicScoreMakerUtility.CalcScrollDiffTicksToPreviewY(_notesViewRectTransform);
			startPos = MusicScoreMakerUtility.CalcLocalPoint(_eventData, _eventData.pressPosition, parentRect);
			endPos = MusicScoreMakerUtility.CalcLocalPoint(_eventData, _eventData.position, parentRect);
			startPos.y -= scrollDiffY;

			float xMin = Mathf.Min(startPos.x, endPos.x);
			float xMax = Mathf.Max(startPos.x, endPos.x);
			float yMin = Mathf.Min(startPos.y, endPos.y);
			float yMax = Mathf.Max(startPos.y, endPos.y);
			return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
		}

		public void SetActiveSelectArea(bool value)
		{
			if (_selectAreaView != null)
			{
				_selectAreaView.gameObject.SetActive(value);
			}
			if (!value)
			{
				_eventData = null;
			}
			if (_startMarker != null)
			{
				_startMarker.gameObject.SetActive(value);
			}
			if (_cursorMarker != null)
			{
				_cursorMarker.gameObject.SetActive(value);
			}
		}

		public void UpdateAreaRect()
		{
			if (_eventData != null && _selectAreaView != null && _selectAreaView.gameObject.activeSelf)
			{
				SetSelectAreaRect();
			}
		}

		private void SetupMarkers()
		{
			RectTransform parent = _selectAreaView != null ? _selectAreaView.parent as RectTransform : null;
			SetupMarker(_startMarker, parent);
			SetupMarker(_cursorMarker, parent);
		}

		private void UpdateMarkerPositionsFromRect(Rect selectionRect, Vector2 startPos, Vector2 endPos)
		{
			bool startIsLeft = startPos.x <= endPos.x;
			bool startIsBottom = startPos.y <= endPos.y;
			Vector2 startMarkerPosition = new Vector2(startIsLeft ? selectionRect.xMin : selectionRect.xMax, startIsBottom ? selectionRect.yMin : selectionRect.yMax);
			Vector2 cursorMarkerPosition = new Vector2(startIsLeft ? selectionRect.xMax : selectionRect.xMin, startIsBottom ? selectionRect.yMax : selectionRect.yMin);
			if (_startMarker != null)
			{
				_startMarker.anchoredPosition = startMarkerPosition;
			}
			if (_cursorMarker != null)
			{
				_cursorMarker.anchoredPosition = cursorMarkerPosition;
			}
		}

		private static void SetupMarker(RectTransform marker, RectTransform parent)
		{
			if (marker == null)
			{
				return;
			}
			if (parent != null)
			{
				marker.SetParent(parent, true);
			}
			marker.anchorMin = CENTER_VECTOR;
			marker.anchorMax = CENTER_VECTOR;
			marker.pivot = CENTER_VECTOR;
			marker.anchoredPosition3D = Vector3.zero;
			marker.localRotation = Quaternion.identity;
			marker.localScale = Vector3.one;
			marker.gameObject.SetActive(false);
		}

		public MusicScoreSelectArea()
		{
		}

		static MusicScoreSelectArea()
		{
			CENTER_VECTOR = new Vector2(0.5f, 0.5f);
		}
	}
}
