using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.Live;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	[RequireComponent(typeof(CanvasRenderer))]
	public class LongNoteLinePreview : MaskableGraphic
	{
		public struct ViewData
		{
			public int ParentId { [CompilerGenerated] readonly get; [CompilerGenerated] set; }

			public Vector2 StartLeft { [CompilerGenerated] readonly get; [CompilerGenerated] set; }

			public Vector2 StartRight { [CompilerGenerated] readonly get; [CompilerGenerated] set; }

			public Vector2 EndLeft { [CompilerGenerated] readonly get; [CompilerGenerated] set; }

			public Vector2 EndRight { [CompilerGenerated] readonly get; [CompilerGenerated] set; }

			public NoteLineType LineType { [CompilerGenerated] readonly get; [CompilerGenerated] set; }

			public Sprite Sprite { [CompilerGenerated] readonly get; [CompilerGenerated] set; }

			public int Index { [CompilerGenerated] readonly get; [CompilerGenerated] set; }

			public int IndexMax { [CompilerGenerated] readonly get; [CompilerGenerated] set; }
		}

		private const float DefaultSegmentLength = 30f;

		[SerializeField]
		private float _segmentLength;

		private bool _isSegmentLengthInitialized;

		private ViewData _viewData;

		private ViewData _meshViewData;

		private bool _hasMeshData;

		private RectTransform _rectTransform;

		private static readonly Dictionary<Texture, Material> _sharedDefaultMaterials;

		private static readonly Dictionary<Texture, Material> _sharedSelectedMaterials;

		private static Shader _sharedLineShader;

		private static int _sharedLineMaterialRefCount;

		private bool? _cachedIsSelected;

		private Texture _cachedTexture;

		private Sprite _cachedSprite;

		private float _cachedUV_u0;

		private float _cachedUV_u1;

		private float _cachedUV_v0y;

		private float _cachedUV_v2y;

		public override Texture mainTexture
		{
			get
			{
				if (material != null && material.mainTexture != null)
				{
					return material.mainTexture;
				}
				return s_WhiteTexture;
			}
		}

		public int NoteId
		{
			get
			{
				return _viewData.ParentId;
			}
		}

		private void EnsureSegmentLengthFromConfig()
		{
			if (_isSegmentLengthInitialized)
			{
				return;
			}

			// TODO(original): load ClientConfig.MusicScoreMaker.LongNoteSegmentLength when ClientConfig is restored.
			if (_segmentLength <= 0f)
			{
				_segmentLength = DefaultSegmentLength;
			}
			_isSegmentLengthInitialized = true;
		}

		public void Setup()
		{
			gameObject.SetActive(false);
			raycastTarget = false;
			_sharedLineMaterialRefCount++;
			if (_sharedLineShader == null)
			{
				_sharedLineShader = Shader.Find("UI/Default");
			}
			_rectTransform = GetComponent<RectTransform>();
		}

		private new void OnDestroy()
		{
			_sharedLineMaterialRefCount--;
			if (_sharedLineMaterialRefCount > 0)
			{
				return;
			}

			foreach (Material material in _sharedDefaultMaterials.Values)
			{
				if (material != null)
				{
					Destroy(material);
				}
			}
			_sharedDefaultMaterials.Clear();

			foreach (Material material in _sharedSelectedMaterials.Values)
			{
				if (material != null)
				{
					Destroy(material);
				}
			}
			_sharedSelectedMaterials.Clear();
			_sharedLineShader = null;
			_sharedLineMaterialRefCount = 0;
		}

		private static Material GetOrCreateSharedMaterial(Texture texture, bool isSelected)
		{
			Dictionary<Texture, Material> materials = isSelected ? _sharedSelectedMaterials : _sharedDefaultMaterials;
			if (materials.TryGetValue(texture, out Material cachedMaterial) && cachedMaterial != null)
			{
				return cachedMaterial;
			}

			Shader shader = _sharedLineShader;
			if (shader == null)
			{
				shader = Shader.Find("UI/Default");
				_sharedLineShader = shader;
			}
			Material material = shader != null ? new Material(shader) : new Material(Graphic.defaultGraphicMaterial);
			material.mainTexture = texture;
			if (isSelected)
			{
				material.SetColor("_Color", new Color(1.2f, 1.2f, 1.2f, 1f));
			}
			materials[texture] = material;
			return material;
		}

		private bool IsScrollOnlyChange(in ViewData newData, in ViewData oldData, out float yDelta)
		{
			yDelta = newData.StartLeft.y - oldData.StartLeft.y;
			if (newData.StartLeft.x != oldData.StartLeft.x || newData.StartRight.x != oldData.StartRight.x || newData.EndLeft.x != oldData.EndLeft.x || newData.EndRight.x != oldData.EndRight.x)
			{
				return false;
			}
			if (!ApproximatelyDelta(yDelta, newData.StartRight.y - oldData.StartRight.y))
			{
				return false;
			}
			if (!ApproximatelyDelta(yDelta, newData.EndLeft.y - oldData.EndLeft.y))
			{
				return false;
			}
			if (!ApproximatelyDelta(yDelta, newData.EndRight.y - oldData.EndRight.y))
			{
				return false;
			}
			return newData.LineType == oldData.LineType && newData.Index == oldData.Index && newData.IndexMax == oldData.IndexMax && newData.Sprite == oldData.Sprite;
		}

		public void UpdateView(ViewData viewData, bool isSelected = false)
		{
			float yDelta;
			if (viewData.Sprite == null || viewData.Sprite.texture == null)
			{
				Debug.LogError("LongNoteLinePreview.UpdateView failed: sprite or texture is null.");
				SetActive(false);
				return;
			}

			SetActive(true);
			Texture texture = viewData.Sprite.texture;
			if (_cachedTexture != texture || !_cachedIsSelected.HasValue || _cachedIsSelected.Value != isSelected)
			{
				_cachedTexture = texture;
				_cachedIsSelected = isSelected;
				material = GetOrCreateSharedMaterial(texture, isSelected);
			}

			if (_hasMeshData && IsScrollOnlyChange(in viewData, in _viewData, out yDelta))
			{
				if (!Mathf.Approximately(yDelta, 0f) && _rectTransform != null)
				{
					Vector2 anchoredPosition = _rectTransform.anchoredPosition;
					anchoredPosition.y += yDelta;
					_rectTransform.anchoredPosition = anchoredPosition;
				}
				_viewData = viewData;
				return;
			}

			if (_rectTransform != null)
			{
				_rectTransform.anchoredPosition = Vector2.zero;
			}
			_meshViewData = viewData;
			_viewData = viewData;
			_hasMeshData = true;
			CacheSpriteUVs(viewData.Sprite);
			SetVerticesDirty();
		}

		private void CacheSpriteUVs(Sprite sprite)
		{
			if (_cachedSprite == sprite)
			{
				return;
			}

			_cachedSprite = sprite;
			Vector2[] uv = sprite.uv;
			_cachedUV_u0 = uv[0].x;
			_cachedUV_u1 = uv[1].x;
			_cachedUV_v0y = uv[0].y;
			_cachedUV_v2y = uv[2].y;
		}

		public void ResetForPool()
		{
			_cachedIsSelected = null;
			_cachedTexture = null;
			_cachedSprite = null;
			_hasMeshData = false;
			_meshViewData = default;
			_viewData = default;
			if (_rectTransform != null)
			{
				_rectTransform.anchoredPosition = Vector2.zero;
			}
		}

		public void UpdateColor(bool isSelected)
		{
			_cachedIsSelected = isSelected;
			color = Color.white;
			if (_cachedTexture != null)
			{
				material = GetOrCreateSharedMaterial(_cachedTexture, isSelected);
			}
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			if (_meshViewData.LineType == NoteLineType.Linear)
			{
				PopulateStraightMesh(vh);
			}
			else
			{
				PopulateEasedMesh(vh);
			}
		}

		private void PopulateStraightMesh(VertexHelper vh)
		{
			float startV = GetClampedSegmentRate(_meshViewData.IndexMax - _meshViewData.Index, _meshViewData.IndexMax);
			float endV = GetClampedSegmentRate(_meshViewData.IndexMax + ~_meshViewData.Index, _meshViewData.IndexMax);
			float v0 = Mathf.Lerp(_cachedUV_v0y, _cachedUV_v2y, startV);
			float v1 = Mathf.Lerp(_cachedUV_v0y, _cachedUV_v2y, endV);
			AddQuad(vh, _meshViewData.StartLeft, _meshViewData.StartRight, _meshViewData.EndLeft, _meshViewData.EndRight, v0, v1);
		}

		private void PopulateEasedMesh(VertexHelper vh)
		{
			int segmentCount = CalculateSegmentCount(_meshViewData.StartLeft, _meshViewData.EndLeft);
			int indexMax = _meshViewData.IndexMax;
			int index = _meshViewData.Index;
			float startRate = GetClampedSegmentRate(indexMax - index, indexMax);
			float endRate = GetClampedSegmentRate(indexMax + ~index, indexMax);
			float startV = Mathf.Lerp(_cachedUV_v0y, _cachedUV_v2y, startRate);
			float vLength = Mathf.Lerp(_cachedUV_v0y, _cachedUV_v2y, endRate) - startV;

			Vector2 currentLeft = _meshViewData.StartLeft;
			Vector2 currentRight = _meshViewData.StartRight;
			float segmentRate = 1f / segmentCount;
			for (int i = 0; i < segmentCount; i++)
			{
				float previousRawRate = segmentRate * i;
				float nextRawRate = segmentRate * (i + 1);
				float easedNextRate = Mathf.Clamp01(ApplyEasing(nextRawRate, _meshViewData.LineType));
				float previousUvRate = Mathf.Clamp01(previousRawRate);
				float nextUvRate = Mathf.Clamp01(nextRawRate);
				Vector2 nextLeft = new Vector2(
					Mathf.Lerp(_meshViewData.StartLeft.x, _meshViewData.EndLeft.x, easedNextRate),
					Mathf.Lerp(_meshViewData.StartLeft.y, _meshViewData.EndLeft.y, nextUvRate));
				Vector2 nextRight = new Vector2(
					Mathf.Lerp(_meshViewData.StartRight.x, _meshViewData.EndRight.x, easedNextRate),
					Mathf.Lerp(_meshViewData.StartRight.y, _meshViewData.EndRight.y, nextUvRate));
				AddQuad(vh, currentLeft, currentRight, nextLeft, nextRight, startV + vLength * previousUvRate, startV + vLength * nextUvRate);
				currentLeft = nextLeft;
				currentRight = nextRight;
			}
		}

		private int CalculateSegmentCount(Vector2 lineStartPoint, Vector2 lineEndPoint)
		{
			EnsureSegmentLengthFromConfig();
			int segmentCount = Mathf.CeilToInt(Vector2.Distance(lineStartPoint, lineEndPoint) / _segmentLength);
			return Mathf.Max(1, segmentCount);
		}

		public bool IsPointerOver(PointerEventData pointerEventData)
		{
			if (pointerEventData == null || _rectTransform == null || !_hasMeshData)
			{
				return false;
			}

			RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out Vector2 localPoint);
			if (_meshViewData.LineType == NoteLineType.Linear)
			{
				return IsPointInTriangle(localPoint, _meshViewData.StartLeft, _meshViewData.StartRight, _meshViewData.EndLeft)
					|| IsPointInTriangle(localPoint, _meshViewData.StartRight, _meshViewData.EndLeft, _meshViewData.EndRight);
			}

			int segmentCount = CalculateSegmentCount(_meshViewData.StartLeft, _meshViewData.EndLeft);
			Vector2 currentLeft = _meshViewData.StartLeft;
			Vector2 currentRight = _meshViewData.StartRight;
			float segmentRate = 1f / segmentCount;
			for (int i = 0; i < segmentCount; i++)
			{
				float nextRawRate = segmentRate * (i + 1);
				float easedNextRate = Mathf.Clamp01(ApplyEasing(nextRawRate, _meshViewData.LineType));
				float yRate = Mathf.Clamp01(nextRawRate);
				Vector2 nextLeft = new Vector2(
					Mathf.Lerp(_meshViewData.StartLeft.x, _meshViewData.EndLeft.x, easedNextRate),
					Mathf.Lerp(_meshViewData.StartLeft.y, _meshViewData.EndLeft.y, yRate));
				Vector2 nextRight = new Vector2(
					Mathf.Lerp(_meshViewData.StartRight.x, _meshViewData.EndRight.x, easedNextRate),
					Mathf.Lerp(_meshViewData.StartRight.y, _meshViewData.EndRight.y, yRate));
				if (IsPointInTriangle(localPoint, currentLeft, currentRight, nextLeft) || IsPointInTriangle(localPoint, currentRight, nextLeft, nextRight))
				{
					return true;
				}
				currentLeft = nextLeft;
				currentRight = nextRight;
			}
			return false;
		}

		private bool IsPointInTriangle(Vector2 point, Vector2 v1, Vector2 v2, Vector2 v3)
		{
			float sign1 = sign(point, v1, v2);
			float sign2 = sign(point, v2, v3);
			float sign3 = sign(point, v3, v1);
			bool hasNegative = sign1 < 0f || sign2 < 0f || sign3 < 0f;
			bool hasPositive = sign1 > 0f || sign2 > 0f || sign3 > 0f;
			return !(hasNegative && hasPositive);
		}

		private static float sign(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
		}

		private static bool ApproximatelyDelta(float expected, float actual)
		{
			float tolerance = Mathf.Max(Mathf.Max(Mathf.Abs(expected), Mathf.Abs(actual)) * 1E-06f, Mathf.Epsilon * 8f);
			return Mathf.Abs(expected - actual) < tolerance;
		}

		private static float GetClampedSegmentRate(int numerator, int denominator)
		{
			if (denominator == 0)
			{
				return 0f;
			}
			return Mathf.Clamp01((float)numerator / denominator);
		}

		private static float ApplyEasing(float rate, NoteLineType lineType)
		{
			return lineType switch
			{
				NoteLineType.EaseOut => 1f - (1f - rate) * (1f - rate),
				NoteLineType.EaseIn => rate * rate,
				_ => rate
			};
		}

		private void AddQuad(VertexHelper vh, Vector2 startLeft, Vector2 startRight, Vector2 endLeft, Vector2 endRight, float startV, float endV)
		{
			int currentVertCount = vh.currentVertCount;
			Color32 color32 = color;
			vh.AddVert(startLeft, color32, new Vector4(_cachedUV_u0, startV, 0f, 0f));
			vh.AddVert(startRight, color32, new Vector4(_cachedUV_u1, startV, 0f, 0f));
			vh.AddVert(endLeft, color32, new Vector4(_cachedUV_u0, endV, 0f, 0f));
			vh.AddVert(endRight, color32, new Vector4(_cachedUV_u1, endV, 0f, 0f));
			vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vh.AddTriangle(currentVertCount + 1, currentVertCount + 2, currentVertCount + 3);
		}

		public void SetActive(bool flag)
		{
			if (gameObject.activeSelf != flag)
			{
				gameObject.SetActive(flag);
			}
		}

		public LongNoteLinePreview()
		{
			_segmentLength = DefaultSegmentLength;
		}

		static LongNoteLinePreview()
		{
			_sharedDefaultMaterials = new Dictionary<Texture, Material>();
			_sharedSelectedMaterials = new Dictionary<Texture, Material>();
		}
	}
}
