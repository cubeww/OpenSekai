using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public abstract class BaseNoteView : MonoBehaviour
	{
		protected Vector2 spawnPosition;

		protected Vector2[] judgmentPositions;

		[SerializeField]
		protected SpriteRenderer spriteRenderer;

		protected INote note;

		protected float posZ;

		public virtual void Setup(Vector2 spawnPosition, Vector2[] judgmentPositions)
		{
			this.spawnPosition = spawnPosition;
			this.judgmentPositions = judgmentPositions;
		}

		public virtual void Move()
		{
			if (note == null)
			{
				return;
			}

			if (note.Progress <= 0f)
			{
				transform.localScale = Vector3.zero;
				return;
			}

			float progress = LiveConfig.GetNoteViewProgress(note.Progress);
			transform.localPosition = CalcNotePosition(ref progress);
			transform.localScale = progress * LiveConfig.Vector3One;
			UpdateMaterialParameters(progress);
		}

		public virtual void Spawn(INote note, float posZ)
		{
			this.note = note;
			this.posZ = posZ;
			gameObject.SetActive(true);

			if (note == null || judgmentPositions == null || spriteRenderer == null)
			{
				return;
			}

			float width = note.DefaultRightLane - note.DefaultLeftLane;
			float laneCount = judgmentPositions.Length;
			float center = (note.DefaultLeftLane + note.DefaultRightLane) * 0.5f;
			Color color = spriteRenderer.color;
			color.r = (center + 0.5f) / laneCount;
			color.g = (width + 0.5f) / laneCount;
			color.b = 0f;
			spriteRenderer.color = EncodeMaterialParameterColor(color);
			spriteRenderer.size = LiveUtility.CalcSpriteRendererSize(width);
		}

		public virtual void Unspawn()
		{
			note = null;
			gameObject.SetActive(false);
			transform.localScale = Vector3.zero;
		}

		public virtual void Change(Vector2 vector)
		{
		}

		protected void UpdateMaterialParameters(float progress)
		{
			UpdateMaterialParameters(spriteRenderer, progress);
		}

		protected void UpdateMaterialParameters(SpriteRenderer target, float progress)
		{
			if (note == null || judgmentPositions == null || target == null)
			{
				return;
			}

			target.color = CreateMaterialParameterColor(target.color.a, progress);
			Change(new Vector2(progress, 1f));
		}

		protected Color CreateMaterialParameterColor(float alpha, float progress)
		{
			float width = note.DefaultRightLane - note.DefaultLeftLane;
			float laneCount = judgmentPositions.Length;
			float center = (note.DefaultLeftLane + note.DefaultRightLane) * 0.5f;
			Color color = new Color(1f, 1f, progress, alpha);
			color.r = (center + 0.5f) / laneCount;
			color.g = (width + 0.5f) / laneCount;
			return EncodeMaterialParameterColor(color);
		}

		private static Color EncodeMaterialParameterColor(Color color)
		{
			if (QualitySettings.activeColorSpace != ColorSpace.Linear)
			{
				return color;
			}

			// Note shaders use SpriteRenderer vertex RGB as packed parameters, not display color.
			// Counter Unity's sRGB-to-linear vertex color conversion so the shader receives original values.
			color.r = Mathf.LinearToGammaSpace(color.r);
			color.g = Mathf.LinearToGammaSpace(color.g);
			color.b = Mathf.LinearToGammaSpace(color.b);
			return color;
		}

		protected Vector2 GetLanePosition(float lane)
		{
			if (judgmentPositions == null || judgmentPositions.Length == 0)
			{
				return Vector2.zero;
			}

			float clampedLane = Mathf.Clamp(lane, 0f, judgmentPositions.Length - 1);
			int start = Mathf.FloorToInt(clampedLane);
			int end = Mathf.CeilToInt(clampedLane);
			float t = Mathf.Clamp01(clampedLane - start);
			return Vector2.Lerp(judgmentPositions[start], judgmentPositions[end], t);
		}

		protected Vector3 CalcLaneInterpolatedNotePosition(ref float progress, bool flipZWhenInput)
		{
			if (note == null)
			{
				return new Vector3(spawnPosition.x, spawnPosition.y, posZ);
			}

			Vector2 left = GetLanePosition(note.LaneStartF);
			Vector2 right = GetLanePosition(note.LaneEndF);
			Vector2 target = Vector2.Lerp(left, right, 0.5f);
			Vector2 position = LiveUtility.EarlyVec2Lerp(spawnPosition, target, progress);
			float z = flipZWhenInput && note.State >= NoteState.InputBegan ? 3f - posZ : posZ;
			return new Vector3(position.x, position.y, z);
		}

		protected void UpdateLongSpriteSize()
		{
			if (note == null || spriteRenderer == null)
			{
				return;
			}

			// JudgeLaneStart/End include the input tolerance offset. Keep that for judgment only;
			// the rendered LongNote head should stay the same width as a normal note of this lane width.
			spriteRenderer.size = LiveUtility.CalcSpriteRendererSize(note.DefaultRightLane - note.DefaultLeftLane);
		}

		protected static bool IsLongBodyCategory(NoteCategory category)
		{
			return category == NoteCategory.Long ||
				category == NoteCategory.FrictionLong ||
				category == NoteCategory.FrictionHideLong;
		}

		protected virtual Vector3 CalcNotePosition(ref float progress)
		{
			int laneStart = note.DefaultLeftLane;
			int laneEnd = note.DefaultRightLane;
			Vector2 start = judgmentPositions[laneStart];
			Vector2 end = judgmentPositions[laneEnd];
			Vector2 target = Vector2.Lerp(start, end, Mathf.Clamp01(0.5f));
			Vector2 position = LiveUtility.EarlyVec2Lerp(spawnPosition, target, progress);
			return new Vector3(position.x, position.y, posZ);
		}
	}
}
