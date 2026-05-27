using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class SelectedNoteDataButton : MonoBehaviour
	{
		public enum ButtonType
		{
			SetSelected = 0,
			Change = 1
		}

		[SerializeField]
		private ButtonType _buttonType;

		[SerializeField]
		private NoteCategory _noteCategory;

		[SerializeField]
		private NoteDirection _noteDirection;

		[SerializeField]
		private NoteType _noteTypes;

		[SerializeField]
		private NoteLineType _noteLineType;

		private NoteGroupType _noteGroupType;

		[SerializeField]
		private bool _isSkip;

		[SerializeField]
		private CustomImage _iconImage;

		[SerializeField]
		private CustomButton _button;

		[SerializeField]
		private GameObject _selectedIndicator;

		public void SetData(ButtonType buttonType, NoteCategory noteCategory, NoteDirection noteDirection, NoteType noteTypes, NoteLineType noteLineType, bool isSkip, NoteGroupType groupType)
		{
			_buttonType = buttonType;
			_noteCategory = noteCategory;
			_noteDirection = noteDirection;
			_noteTypes = noteTypes;
			_noteLineType = noteLineType;
			_noteGroupType = groupType;
			_isSkip = isSkip;
			SetSprite();
		}

		private void Awake()
		{
			Setup();
		}

		private void Setup()
		{
			SetSprite();
			if (_button != null)
			{
				_button.onClick.RemoveListener(OnClick);
				_button.onClick.AddListener(OnClick);
			}
			SetupEventDispatcher();
			UpdateSelectedState();
		}

		private void SetSprite()
		{
			if (_iconImage == null)
			{
				return;
			}

			NoteGroupType groupType = _noteGroupType;
			if (_buttonType == ButtonType.SetSelected
				&& (_noteCategory == NoteCategory.Long
					|| _noteCategory == NoteCategory.Flick
					|| _noteCategory == NoteCategory.Friction
					|| _noteCategory == NoteCategory.FrictionHide
					|| _noteCategory == NoteCategory.FrictionLong
					|| _noteCategory == NoteCategory.FrictionHideLong
					|| _noteCategory == NoteCategory.FrictionFlick
					|| _noteCategory == NoteCategory.Guide
					|| _noteCategory == NoteCategory.GuideHidden))
			{
				groupType = NoteGroupType.Unknown;
			}

			string spriteName = _isSkip
				? "notes_icon_long_among" + (_noteTypes == NoteType.Critical ? "_crtc" : string.Empty)
				: MusicScoreMakerUtility.GetNoteTypeIconSpriteName(_noteCategory, _noteDirection, _noteTypes, _noteLineType, groupType, _buttonType);
			if (_iconImage.SpriteName != spriteName)
			{
				_iconImage.SpriteName = spriteName;
			}

			RectTransform iconTransform = _iconImage.GetComponent<RectTransform>();
			if (iconTransform != null)
			{
				Vector3 scale = iconTransform.localScale;
				scale.x = !_isSkip && _noteCategory == NoteCategory.Flick && _noteDirection == NoteDirection.Right ? -1f : 1f;
				iconTransform.localScale = scale;
			}
		}

		private void OnDestroy()
		{
			Dispose();
		}

		private void Dispose()
		{
			if (_button != null)
			{
				_button.onClick.RemoveListener(OnClick);
			}
			DisposeEventDispatcher();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<SetSelectedNoteDataEvent>(OnSetSelectedNoteDataEvent);
			dispatcher.Remove<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
			dispatcher.Register<SetSelectedNoteDataEvent>(OnSetSelectedNoteDataEvent);
			dispatcher.Register<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
		}

		private void DisposeEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<SetSelectedNoteDataEvent>(OnSetSelectedNoteDataEvent);
			dispatcher.Remove<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
		}

		private void OnUpdateButtonSelectionState(UpdateButtonSelectionStateEvent obj)
		{
			UpdateSelectedState();
		}

		private void OnSetSelectedNoteDataEvent(SetSelectedNoteDataEvent obj)
		{
			UpdateSelectedState();
		}

		private void UpdateSelectedState()
		{
			if (_selectedIndicator == null)
			{
				return;
			}

			bool isEventSettingMode = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetIsEventSettingModeEvent, bool>(new GetIsEventSettingModeEvent());
			if (isEventSettingMode)
			{
				SetSelected(false);
				return;
			}
			SetSelected(_buttonType == ButtonType.Change ? CheckSelectedStateForChange() : CheckSelectedStateForSetSelected());
		}

		public void SetSelected(bool isSelected)
		{
			if (_selectedIndicator != null)
			{
				_selectedIndicator.SetActive(isSelected);
			}
		}

		private bool CheckSelectedStateForSetSelected()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			if (!dispatcher.PublishFirst<GetIsNoteDataSelectedEvent, bool>(new GetIsNoteDataSelectedEvent()))
			{
				return false;
			}

			NoteCategory category = dispatcher.PublishFirst<GetSelectedNoteCategoryEvent, NoteCategory>(new GetSelectedNoteCategoryEvent());
			NoteDirection direction = dispatcher.PublishFirst<GetSelectedNoteDirectionEvent, NoteDirection>(new GetSelectedNoteDirectionEvent());
			NoteType type = dispatcher.PublishFirst<GetSelectedNoteTypeEvent, NoteType>(new GetSelectedNoteTypeEvent());
			NoteLineType lineType = dispatcher.PublishFirst<GetSelectedNoteLineTypeEvent, NoteLineType>(new GetSelectedNoteLineTypeEvent());
			bool isSkip = dispatcher.PublishFirst<GetSelectedIsSkipEvent, bool>(new GetSelectedIsSkipEvent());
			return IsNoteDataMatch(category, direction, type, lineType, isSkip);
		}

		private bool CheckSelectedStateForChange()
		{
			MusicScoreMakerData data = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetMusicScoreMakerDataEvent, MusicScoreMakerData>(new GetMusicScoreMakerDataEvent());
			if (data == null || data.SelectedNoteIdList == null || data.SelectedNoteIdList.Count == 0)
			{
				return false;
			}

			for (int i = 0; i < data.SelectedNoteIdList.Count; i++)
			{
				MusicScoreNoteBase note = data.FindNote(data.SelectedNoteIdList[i]);
				if (!IsSameNote(note))
				{
					return false;
				}
			}
			return true;
		}

		private bool IsNoteDataMatch(NoteCategory category, NoteDirection direction, NoteType type, NoteLineType lineType, bool isSkip)
		{
			return _noteCategory == category
				&& _noteDirection == direction
				&& _noteTypes == type
				&& _noteLineType == lineType
				&& _isSkip == isSkip;
		}

		private void OnClick()
		{
			PublishEvent();
		}

		private void PublishEvent()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			if (_buttonType == ButtonType.Change)
			{
				dispatcher.Publish(new ChangeNoteDataEvent
				{
					NoteCategory = _noteCategory,
					NoteDirection = _noteDirection,
					NoteType = _noteTypes,
					NoteLineType = _noteLineType,
					IsSkip = _isSkip
				});
				return;
			}

			dispatcher.Publish(new SetSelectedNoteDataEvent
			{
				NoteCategory = _noteCategory,
				NoteDirection = _noteDirection,
				NoteType = _noteTypes,
				NoteLineType = _noteLineType,
				IsSkip = _isSkip
			});
		}

		public bool IsSameNote(MusicScoreNoteBase selectedNote)
		{
			return selectedNote != null
				&& _noteCategory == selectedNote.category
				&& _noteDirection == selectedNote.direction
				&& _noteTypes == selectedNote.type
				&& _noteLineType == selectedNote.noteLineType
				&& _isSkip == selectedNote.isSkip;
		}

		public SelectedNoteDataButton()
		{
		}
	}
}
