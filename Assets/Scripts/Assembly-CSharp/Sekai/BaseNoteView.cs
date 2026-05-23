using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public abstract class BaseNoteView : MonoBehaviour
	{
		private static readonly int NoteShowRateId = Shader.PropertyToID("_NoteShowRate");

		[SerializeField]
		protected SpriteRenderer spriteRenderer;

		[SerializeField]
		protected Renderer[] noteRenderers;

		protected Vector2 spawnPosition;

		protected Vector2[] judgmentPositions;

		protected INote note;

		protected float posZ;

		public virtual void Setup(Vector2 spawnPosition, Vector2[] judgmentPositions, float noteShowRate)
		{
			this.spawnPosition = spawnPosition;
			this.judgmentPositions = judgmentPositions;
			if (spriteRenderer == null)
			{
				spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
			}
			if (noteRenderers == null || noteRenderers.Length == 0)
			{
				noteRenderers = GetComponentsInChildren<Renderer>(true);
			}

			transform.localScale = Vector3.zero;
			ApplyNoteShowRate(noteShowRate);
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
			transform.localScale = Vector3.one * progress;
			UpdateSpritePayload(progress);
		}

		public virtual void Spawn(INote note, float posZ)
		{
			this.note = note;
			this.posZ = posZ;
			gameObject.SetActive(true);
			if (spriteRenderer != null)
			{
				spriteRenderer.size = LiveUtility.CalcSpriteRendererSize(note.LaneEnd - note.LaneStart);
			}
			UpdateSpritePayload(0f);
		}

		public virtual void Unspawn()
		{
			note = null;
			transform.localScale = Vector3.zero;
			gameObject.SetActive(false);
		}

		public virtual void Change(Vector2 vector)
		{
		}

		protected virtual Vector3 CalcNotePosition(ref float progress)
		{
			if (note == null)
			{
				return Vector3.zero;
			}

			Vector2 center = GetLaneCenter(note.LaneStart, note.LaneEnd);
			Vector2 position = Vector2.LerpUnclamped(spawnPosition, center, progress);
			return new Vector3(position.x, position.y, posZ);
		}

		protected BaseNoteView()
		{
		}

		protected Vector2 GetLaneCenter(float laneStart, float laneEnd)
		{
			return (GetLanePosition(laneStart) + GetLanePosition(laneEnd)) * 0.5f;
		}

		protected Vector2 GetLanePosition(float lane)
		{
			if (judgmentPositions == null || judgmentPositions.Length == 0)
			{
				return Vector2.zero;
			}

			float clamped = Mathf.Clamp(lane, 0f, judgmentPositions.Length - 1);
			int leftIndex = Mathf.Clamp(Mathf.FloorToInt(clamped), 0, judgmentPositions.Length - 1);
			int rightIndex = Mathf.Clamp(Mathf.CeilToInt(clamped), 0, judgmentPositions.Length - 1);
			float t = Mathf.Clamp01(clamped - leftIndex);
			return Vector2.LerpUnclamped(judgmentPositions[leftIndex], judgmentPositions[rightIndex], t);
		}

		protected void UpdateSpritePayload(float progress)
		{
			if (spriteRenderer == null || note == null || judgmentPositions == null || judgmentPositions.Length == 0)
			{
				return;
			}

			Color color = spriteRenderer.color;
			color.r = (((float)note.LaneStart + note.LaneEnd) * 0.5f + 0.5f) / judgmentPositions.Length;
			color.g = ((float)(note.LaneEnd - note.LaneStart) + 0.5f) / judgmentPositions.Length;
			color.b = progress;
			spriteRenderer.color = color;
		}

		protected void ApplyNoteShowRate(float noteShowRate)
		{
			if (noteRenderers == null)
			{
				return;
			}

			foreach (Renderer renderer in noteRenderers)
			{
				if (renderer == null)
				{
					continue;
				}

				Material material = renderer.material;
				if (material != null)
				{
					material.SetFloat(NoteShowRateId, noteShowRate);
				}
			}
		}
	}
}
