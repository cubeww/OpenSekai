using System;
using System.Collections.Generic;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Input;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class NotePreview : MonoBehaviour
	{
		[Serializable]
		private struct NotePreviewImage
		{
			public NoteType _noteType;

			public NoteCategory _noteCategory;

			public NoteDirection _noteDirection;

			public Sprite sprite;
		}

		private const int UNUSED_ID = int.MinValue;

		private const float NoInGameSpriteHitAreaPaddingPx = 10f;

		private static readonly Vector4 NoInGameSpriteRaycastPadding;

		[SerializeField]
		private float _centerWidth;

		[SerializeField]
		[FormerlySerializedAs("_toolInputButton")]
		private ToolInputHandler toolInputHandler;

		[SerializeField]
		private CustomImage _noteImage;

		[SerializeField]
		private CustomImage _arrowImage;

		[SerializeField]
		private CustomImage _connectionImage;

		[SerializeField]
		private Sprite _noInGameSprite;

		[SerializeField]
		private NotePreviewImage[] _notePreviewImages;

		[SerializeField]
		private NotePreviewImage[] _arrowPreviewImages;

		[SerializeField]
		private NotePreviewImage[] _connectionPreviewImages;

		[SerializeField]
		private RectTransform _rectTransform;

		private RectTransform _parentRectTransform;

		private int _noteId;

		public SelectedTargetOperation.NoteTapPosition noteTapPosition;

		[NonSerialized]
		private MusicScoreNoteBase _noteBase;

		private float _currentMusicScoreScale;

		private static Material _sharedNoteDefaultMaterial;

		private static Material _sharedNoteSelectedMaterial;

		private static Material _sharedArrowDefaultMaterial;

		private static Material _sharedArrowSelectedMaterial;

		private static Material _sharedConnectionDefaultMaterial;

		private static Material _sharedConnectionSelectedMaterial;

		private static int _sharedMaterialRefCount;

		private bool _isInteractive;

		private bool _hideNoInGameSprite;

		private NoteType _cachedNoteType;

		private NoteCategory _cachedNoteCategory;

		private NoteDirection _cachedNoteDirection;

		private bool _cachedIsSkip;

		private bool _imageParamsCached;

		private bool _cachedIsSelected;

		private float _cachedNoteImageHeight;

		private float _cachedSetRectWidth;

		private float _cachedYScale;

		private float _noteScaleMultiplier;

		public bool IsUsing
		{
			get
			{
				return NoteId != UNUSED_ID;
			}
		}

		public int NoteId
		{
			get
			{
				return _noteId;
			}
			private set
			{
				_noteId = value;
			}
		}

		public RectTransform RectTransform
		{
			get
			{
				return _rectTransform;
			}
		}

		public long Tick
		{
			get
			{
				return _noteBase?.ticks ?? 0L;
			}
		}

		public bool ContainsScreenPoint(Vector2 screenPosition)
		{
			if (NoteId == UNUSED_ID || _noteImage == null)
			{
				return false;
			}
			Canvas canvas = _noteImage.canvas;
			Camera worldCamera = canvas != null ? canvas.worldCamera : null;
			return RectTransformUtility.RectangleContainsScreenPoint(_noteImage.rectTransform, screenPosition, worldCamera);
		}

		public void Setup()
		{
			_rectTransform ??= GetComponent<RectTransform>();
			NoteId = UNUSED_ID;
			InitializeMaterials();
			if (toolInputHandler != null)
			{
				toolInputHandler.RemoveAllAndAddListener(OnClick, null, OnDrag, OnPointerDown, OnPointerUp);
			}
		}

		public void SetNoteScaleMultiplier(float multiplier)
		{
			_noteScaleMultiplier = multiplier;
		}

		public void SetInteractive(bool isInteractive)
		{
			_isInteractive = isInteractive;
		}

		public void SetHideNoInGameSprite(bool hide)
		{
			_hideNoInGameSprite = hide;
		}

		public void Dispose()
		{
			if (toolInputHandler != null)
			{
				toolInputHandler.RemoveAllListeners();
			}
			Release();
			DestroyMaterials();
		}

		private void InitializeMaterials()
		{
			_sharedMaterialRefCount++;
			if (_sharedMaterialRefCount != 1)
			{
				return;
			}

			InitializeSharedMaterials(_noteImage, ref _sharedNoteDefaultMaterial, ref _sharedNoteSelectedMaterial);
			InitializeSharedMaterials(_arrowImage, ref _sharedArrowDefaultMaterial, ref _sharedArrowSelectedMaterial);
			InitializeSharedMaterials(_connectionImage, ref _sharedConnectionDefaultMaterial, ref _sharedConnectionSelectedMaterial);
		}

		private void DestroyMaterials()
		{
			_sharedMaterialRefCount--;
			if (_sharedMaterialRefCount > 0)
			{
				return;
			}

			DestroySelectedMaterial(ref _sharedNoteSelectedMaterial);
			DestroySelectedMaterial(ref _sharedArrowSelectedMaterial);
			DestroySelectedMaterial(ref _sharedConnectionSelectedMaterial);
			_sharedNoteDefaultMaterial = null;
			_sharedArrowDefaultMaterial = null;
			_sharedConnectionDefaultMaterial = null;
			_sharedMaterialRefCount = 0;
		}

		public void Release()
		{
			NoteId = UNUSED_ID;
			_noteBase = null;
			_imageParamsCached = false;
			gameObject.SetActive(false);
		}

		public void Show(MusicScoreNoteBase noteBase, long startTicks, long endTicks, MusicScoreMakerData MusicScoreMakerData, RectTransform parentRectTransform, float currentMusicScoreScale = 1f, in ParentRectContext? parentRectContext = null)
		{
			if (noteBase == null || MusicScoreMakerData == null)
			{
				Release();
				return;
			}

			_parentRectTransform = parentRectTransform;
			_noteBase = noteBase;
			NoteId = noteBase.id;
			_currentMusicScoreScale = currentMusicScoreScale;
			noteTapPosition = SelectedTargetOperation.NoteTapPosition.center;
			gameObject.SetActive(true);
			SetImage(noteBase.type, noteBase.category, noteBase.direction, noteBase.isSkip);
			UpdateSelectedIndicator(noteBase, MusicScoreMakerData);
			if (noteBase.isSkip)
			{
				Dictionary<int, MusicScoreNoteBase> noteIdCache = MusicScoreMakerData.GetNoteIdCacheOrRebuild();
				MusicScoreNoteBase prevNote = noteBase.FindPrevNote(noteIdCache, true);
				MusicScoreNoteBase nextNote = noteBase.FindNextNote(noteIdCache, true);
				if (prevNote == null || nextNote == null)
				{
					gameObject.SetActive(false);
					return;
				}

				(long ticks, float laneStart, float laneEnd) = MusicScoreMakerUtility.CalcSkipNoteLane(noteBase, prevNote, nextNote, MusicScoreMakerData);
				UpdateRect(ticks, startTicks, endTicks, laneStart, laneEnd, parentRectContext);
			}
			else
			{
				long ticks = noteBase.ticks;
				int laneStart = noteBase.laneStart;
				int laneEnd = noteBase.laneEnd;
				MusicScoreMakerUtility.CalcNoteOperation(MusicScoreMakerData, ref ticks, ref laneStart, ref laneEnd, noteBase);
				UpdateRect(ticks, startTicks, endTicks, laneStart, laneEnd, parentRectContext);
			}
		}

		private void SetConnectionImage(NoteType noteType, NoteCategory noteCategory)
		{
			if (_connectionImage == null)
			{
				return;
			}
			Sprite sprite = FindSpriteExact(_connectionPreviewImages, noteType, noteCategory);
			_connectionImage.sprite = sprite;
			_connectionImage.SetActive(sprite != null);
		}

		public void UpdateSelectedIndicator(MusicScoreNoteBase noteBase, MusicScoreMakerData MusicScoreMakerData)
		{
			bool isSelected = MusicScoreMakerData?.SelectedNoteTargetIdSet?.Contains(noteBase.id) == true;
			UpdateNoteColor(isSelected);
		}

		private void UpdateNoteColor(bool isSelected)
		{
			if (_cachedIsSelected == isSelected && _imageParamsCached)
			{
				return;
			}
			_cachedIsSelected = isSelected;
			UpdateImageColorAndMaterial(_noteImage, _sharedNoteDefaultMaterial, _sharedNoteSelectedMaterial, isSelected);
			UpdateImageColorAndMaterial(_arrowImage, _sharedArrowDefaultMaterial, _sharedArrowSelectedMaterial, isSelected);
			UpdateImageColorAndMaterial(_connectionImage, _sharedConnectionDefaultMaterial, _sharedConnectionSelectedMaterial, isSelected);
		}

		private static void InitializeSharedMaterials(CustomImage image, ref Material defaultMaterial, ref Material selectedMaterial)
		{
			if (image == null || image.material == null)
			{
				return;
			}

			defaultMaterial = image.material;
			selectedMaterial = new Material(defaultMaterial);
			if (selectedMaterial.HasProperty("_Color"))
			{
				Color color = selectedMaterial.GetColor("_Color");
				color.r = 1.2f;
				color.g = 1.2f;
				color.b = 1.2f;
				selectedMaterial.SetColor("_Color", color);
			}
		}

		private static void DestroySelectedMaterial(ref Material material)
		{
			if (material != null)
			{
				Destroy(material);
				material = null;
			}
		}

		private static void UpdateImageColorAndMaterial(CustomImage image, Material defaultMaterial, Material selectedMaterial, bool isSelected)
		{
			if (image == null)
			{
				return;
			}

			Color color = image.color;
			float rgb = isSelected ? 1.2f : 1f;
			color.r = rgb;
			color.g = rgb;
			color.b = rgb;
			image.color = color;

			Material material = isSelected ? selectedMaterial : defaultMaterial;
			if (material != null)
			{
				image.material = material;
			}
		}

		private void UpdateRect(long ticks, long startTicks, long endTicks, int laneStart, int laneEnd, in ParentRectContext? parentRectContext = null)
		{
			UpdateRect(ticks, startTicks, endTicks, (float)laneStart, laneEnd, parentRectContext);
		}

		private void UpdateRect(long ticks, long startTicks, long endTicks, float laneStart, float laneEnd, in ParentRectContext? parentRectContext = null)
		{
			if (_rectTransform == null || _parentRectTransform == null)
			{
				return;
			}

			ParentRectContext context = parentRectContext ?? new ParentRectContext(_parentRectTransform.rect.width, _parentRectTransform.rect.height, _parentRectTransform.anchoredPosition);
			(float centerX, float width) = MusicScoreMakerUtility.CalcPreviewCenterXAndWidth(laneStart, laneEnd, context.Width, Vector2.zero);
			float showY = MusicScoreMakerUtility.CalcPreviewPositionYFromTicks(startTicks, endTicks, context.Size, Vector2.zero, ticks);
			SetRect(centerX, showY, width);
		}

		private void SetRect(float centerX, float showY, float width)
		{
			if (_rectTransform == null)
			{
				return;
			}
			_rectTransform.anchoredPosition = new Vector2(centerX, showY);
			float inverseNoteScale = Mathf.Approximately(_noteScaleMultiplier, 0f) ? 1f : 1f / _noteScaleMultiplier;
			if (!Mathf.Approximately(_cachedSetRectWidth, width))
			{
				_cachedSetRectWidth = width;
				float imageWidth = inverseNoteScale * width;
				if (_noteImage != null)
				{
					RectTransform noteRect = _noteImage.rectTransform;
					Vector2 noteSize = noteRect.sizeDelta;
					noteSize.x = imageWidth;
					if (_noteImage.sprite != null)
					{
						noteSize.y = _cachedNoteImageHeight;
					}
					noteRect.sizeDelta = noteSize;
				}
				if (_arrowImage != null)
				{
					RectTransform arrowRect = _arrowImage.rectTransform;
					Vector2 arrowSize = arrowRect.sizeDelta;
					arrowSize.x = imageWidth;
					if (_noteImage == null || _noteImage.sprite != null)
					{
						if (_arrowImage.gameObject.activeSelf)
						{
							arrowSize.y = 112f;
						}
					}
					arrowRect.sizeDelta = arrowSize;
				}
			}

			float yScale = MusicScoreMakerUtility.CalculateNoteYScale(_currentMusicScoreScale) * _noteScaleMultiplier;
			if (!Mathf.Approximately(_cachedYScale, yScale))
			{
				_cachedYScale = yScale;
				_rectTransform.localScale = new Vector3(_noteScaleMultiplier, yScale, 1f);
			}
		}

		public void SetImage(NoteType noteType, NoteCategory noteCategory, NoteDirection noteDirection, bool isSkip)
		{
			if (_imageParamsCached && _cachedNoteType == noteType && _cachedNoteCategory == noteCategory && _cachedNoteDirection == noteDirection && _cachedIsSkip == isSkip)
			{
				return;
			}
			_cachedNoteType = noteType;
			_cachedNoteCategory = noteCategory;
			_cachedNoteDirection = noteDirection;
			_cachedIsSkip = isSkip;
			_imageParamsCached = true;
			_cachedSetRectWidth = float.MinValue;

			if (_noteImage != null)
			{
				if (isSkip)
				{
					_noteImage.SetActive(false);
				}
				else
				{
					_noteImage.SetActive(true);
					Sprite sprite = FindSpriteExact(_notePreviewImages, noteType, noteCategory);
					if (sprite != null)
					{
						_noteImage.sprite = sprite;
						_cachedNoteImageHeight = sprite.rect.height / _noteImage.pixelsPerUnit;
						_noteImage.raycastPadding = Vector4.zero;
					}
					else
					{
						_noteImage.SetActive(!_hideNoInGameSprite);
						if (!_hideNoInGameSprite)
						{
							_noteImage.sprite = _noInGameSprite;
							_cachedNoteImageHeight = _noInGameSprite != null ? _noInGameSprite.rect.height / _noteImage.pixelsPerUnit : 0f;
							_noteImage.raycastPadding = _noInGameSprite != null ? NoInGameSpriteRaycastPadding : Vector4.zero;
						}
					}
				}
			}
			SetArrowImage(noteType, noteCategory, noteDirection);
			SetConnectionImage(noteType, noteCategory);
		}

		private void SetArrowImage(NoteType noteType, NoteCategory noteCategory, NoteDirection noteDirection)
		{
			if (_arrowImage == null)
			{
				return;
			}
			Sprite sprite = null;
			if (noteCategory == NoteCategory.Flick || noteCategory == NoteCategory.FrictionFlick)
			{
				sprite = FindArrowSprite(noteType, noteDirection);
			}
			if (sprite != null)
			{
				_arrowImage.sprite = sprite;
				_arrowImage.transform.localScale = new Vector3(noteDirection == NoteDirection.Left ? 1f : -1f, 1f, 1f);
				_arrowImage.gameObject.SetActive(true);
			}
			else
			{
				_arrowImage.gameObject.SetActive(false);
			}
		}

		private void OnClick(PointerEventData pointerEventData)
		{
			if (!_isInteractive || NoteId == UNUSED_ID)
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnNotePreviewClickEvent
			{
				NoteId = NoteId,
				PointerEventData = pointerEventData
			});
		}

		private void OnDrag(PointerEventData pointerEventData)
		{
			if (!_isInteractive || NoteId == UNUSED_ID)
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnNotePreviewDragEvent
			{
				NoteId = NoteId,
				NoteTapPosition = noteTapPosition,
				PointerEventData = pointerEventData
			});
		}

		private void OnPointerDown(PointerEventData pointerEventData)
		{
			if (_isInteractive && NoteId != UNUSED_ID)
			{
				noteTapPosition = SelectedTargetOperation.NoteTapPosition.center;
			}
		}

		private void OnPointerUp(PointerEventData pointerEventData, bool isLongPress, bool isDragging)
		{
			if (!_isInteractive || NoteId == UNUSED_ID)
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnNotePreviewPointerUpEvent
			{
				NoteId = NoteId,
				NoteTapPosition = noteTapPosition,
				PointerEventData = pointerEventData,
				IsLongPress = isLongPress,
				IsDragging = isDragging
			});
			noteTapPosition = SelectedTargetOperation.NoteTapPosition.none;
		}

		public NotePreview()
		{
			NoteId = UNUSED_ID;
			_noteScaleMultiplier = 1f;
		}

		private Sprite FindSpriteExact(NotePreviewImage[] images, NoteType noteType, NoteCategory noteCategory)
		{
			if (images == null)
			{
				return null;
			}

			foreach (NotePreviewImage image in images)
			{
				if (image._noteType == noteType && image._noteCategory == noteCategory)
				{
					return image.sprite;
				}
			}
			return null;
		}

		private Sprite FindArrowSprite(NoteType noteType, NoteDirection noteDirection)
		{
			if (_arrowPreviewImages == null)
			{
				return null;
			}

			foreach (NotePreviewImage image in _arrowPreviewImages)
			{
				if (image._noteType == noteType && image._noteDirection == noteDirection)
				{
					return image.sprite;
				}
			}
			return null;
		}

		static NotePreview()
		{
			// Original stores (0, -10, 0, -10): expand only the vertical hit area
			// for the thin no-in-game long-note connector preview sprite.
			NoInGameSpriteRaycastPadding = new Vector4(
				0f,
				-NoInGameSpriteHitAreaPaddingPx,
				0f,
				-NoInGameSpriteHitAreaPaddingPx);
		}
	}
}
