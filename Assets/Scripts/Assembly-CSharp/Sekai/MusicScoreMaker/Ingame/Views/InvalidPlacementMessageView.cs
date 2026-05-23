using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class InvalidPlacementMessageView : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CShowMessageCoroutine_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public InvalidPlacementMessageView _003C_003E4__this;

			private Vector2 _003CstartPos_003E5__2;

			private float _003CelapsedTime_003E5__3;

			private Vector2 _003CendPos_003E5__4;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CShowMessageCoroutine_003Ed__15(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		[SerializeField]
		private CustomTextMesh _messageText;

		[SerializeField]
		private RectTransform _messageContainer;

		[SerializeField]
		private CanvasGroup _canvasGroup;

		private const float SLIDE_IN_DURATION = 0.3f;

		private const float SLIDE_OUT_DURATION = 0.25f;

		private const float DISPLAY_DURATION = 2.5f;

		private const float OFF_SCREEN_OFFSET = 500f;

		private Coroutine _animationCoroutine;

		private Vector2 _originalAnchoredPosition;

		private bool _isInitialized;

		public void Setup()
		{
			if (_isInitialized)
			{
				return;
			}
			if (_messageContainer != null)
			{
				_originalAnchoredPosition = _messageContainer.anchoredPosition;
			}
			HideMessageImmediate();
			_isInitialized = true;
			MusicScoreMakerEventDispatcher.Instance.Register<ShowInvalidPlacementMessageEvent>(OnShowMessage);
		}

		private void OnEnable()
		{
			if (_isInitialized)
			{
				HideMessageImmediate();
			}
		}

		private void OnDisable()
		{
			if (_animationCoroutine != null)
			{
				StopCoroutine(_animationCoroutine);
				_animationCoroutine = null;
			}
		}

		private void OnShowMessage(ShowInvalidPlacementMessageEvent evt)
		{
			if (evt != null)
			{
				ShowMessage(evt.Info);
			}
		}

		public void ShowMessage(InvalidPlacementInfo info)
		{
			if (!_isInitialized)
			{
				Setup();
			}
			string message = GenerateMessage(info);
			if (string.IsNullOrEmpty(message))
			{
				return;
			}
			if (_messageText != null)
			{
				_messageText.SetText(message);
			}
			if (_animationCoroutine != null)
			{
				StopCoroutine(_animationCoroutine);
			}
			_animationCoroutine = StartCoroutine(ShowMessageCoroutine());
		}

		[IteratorStateMachine(typeof(_003CShowMessageCoroutine_003Ed__15))]
		private IEnumerator ShowMessageCoroutine()
		{
			return ShowMessageCoroutineCore();
		}

		public void HideMessageImmediate()
		{
			if (_animationCoroutine != null)
			{
				StopCoroutine(_animationCoroutine);
				_animationCoroutine = null;
			}
			if (_messageContainer != null)
			{
				_messageContainer.anchoredPosition = _originalAnchoredPosition + new Vector2(OFF_SCREEN_OFFSET, 0f);
				_messageContainer.gameObject.SetActive(false);
			}
			if (_canvasGroup != null)
			{
				_canvasGroup.alpha = 0f;
			}
		}

		private string GenerateMessage(InvalidPlacementInfo info)
		{
			if (info == null)
			{
				return string.Empty;
			}

			switch (info.Type)
			{
			case InvalidPlacementType.LaneOverlap:
				return WordingManager.Get("WORD_MSM_INVALID_PLACEMENT_LANE_OVERLAP");
			case InvalidPlacementType.JudgmentNoteGap:
				return WordingManager.Get("WORD_MSM_INVALID_PLACEMENT_JUDGMENT_NOTE_GAP");
			case InvalidPlacementType.NoteDensityOverflow:
				return WordingManager.GetFormat("WORD_MSM_INVALID_PLACEMENT_NOTE_DENSITY_OVERFLOW", info.NoteDensityWindowSec, info.NoteDensityThreshold);
			case InvalidPlacementType.ComboCountUnderflow:
				return WordingManager.GetFormat("MSG_MSM_INVALID_PLACEMENT_COMBO_COUNT_UNDERFLOW", info.ComboCountMinimum, info.ActualComboCount);
			case InvalidPlacementType.ComboCountOverflow:
				return WordingManager.GetFormat("MSG_MSM_INVALID_PLACEMENT_COMBO_COUNT_OVERFLOW", info.ComboCountMaximum, info.ActualComboCount);
			case InvalidPlacementType.TimeSignatureOffset:
				return WordingManager.Get("WORD_MSM_INVALID_PLACEMENT_TIME_SIGNATURE_OFFSET");
			case InvalidPlacementType.LongNoteMeshOverflow:
				return WordingManager.Get("WORD_MSM_INVALID_PLACEMENT_LONG_NOTE_MESH_OVERFLOW");
			case InvalidPlacementType.GuideMeshOverflow:
				return WordingManager.Get("WORD_MSM_INVALID_PLACEMENT_GUIDE_MESH_OVERFLOW");
			case InvalidPlacementType.TapNotesUnderflow:
				return WordingManager.GetFormat("MSG_MSM_INVALID_PLACEMENT_TAP_NOTES_UNDERFLOW", info.TapNotesMinimum, info.ActualTapNotesCount);
			default:
				UnityEngine.Debug.LogError($"Unknown invalid placement type: {info.Type}");
				return string.Empty;
			}
		}

		private IEnumerator ShowMessageCoroutineCore()
		{
			if (_messageContainer == null)
			{
				yield break;
			}

			_messageContainer.gameObject.SetActive(true);
			Vector2 hiddenPos = _originalAnchoredPosition + new Vector2(OFF_SCREEN_OFFSET, 0f);
			yield return AnimateMessage(hiddenPos, _originalAnchoredPosition, SLIDE_IN_DURATION, 0f, 1f);
			yield return new WaitForSeconds(DISPLAY_DURATION);
			yield return AnimateMessage(_originalAnchoredPosition, hiddenPos, SLIDE_OUT_DURATION, 1f, 0f);
			_messageContainer.gameObject.SetActive(false);
			_animationCoroutine = null;
		}

		private IEnumerator AnimateMessage(Vector2 startPos, Vector2 endPos, float duration, float startAlpha, float endAlpha)
		{
			float elapsedTime = 0f;
			while (elapsedTime < duration)
			{
				float rate = duration <= 0f ? 1f : Mathf.Clamp01(elapsedTime / duration);
				if (_messageContainer != null)
				{
					_messageContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, rate);
				}
				if (_canvasGroup != null)
				{
					_canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, rate);
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			if (_messageContainer != null)
			{
				_messageContainer.anchoredPosition = endPos;
			}
			if (_canvasGroup != null)
			{
				_canvasGroup.alpha = endAlpha;
			}
		}

		public InvalidPlacementMessageView()
		{
		}
	}
}
