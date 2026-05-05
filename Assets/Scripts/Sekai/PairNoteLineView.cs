using System.Collections.Generic;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class PairNoteLineView : MonoBehaviour
	{
		private const int PoolCount = 50;

		private const float LineHalfWidth = 0.2f;

		private MeshFilter filter;

		private MeshRenderer meshRenderer;

		private Mesh mesh;

		private Vector3[] vertices;

		private Vector2[] uv;

		private int[] triangles;

		private List<INote> pairNotes;

		public void Setup(Texture texture, Material sourceMaterial)
		{
			filter = GetComponent<MeshFilter>();
			if (filter == null)
			{
				filter = gameObject.AddComponent<MeshFilter>();
			}
			meshRenderer = GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = gameObject.AddComponent<MeshRenderer>();
			}

			Material material = sourceMaterial != null ? new Material(sourceMaterial) : CreateFallbackMaterial();
			if (material != null)
			{
				if (texture != null)
				{
					if (material.HasProperty("Texture"))
					{
						material.SetTexture("Texture", texture);
					}
					material.SetTexture("_MainTex", texture);
					material.mainTexture = texture;
				}
				meshRenderer.material = material;
				meshRenderer.sortingOrder = 28;
			}

			pairNotes = new List<INote>(PoolCount);
			mesh = new Mesh { name = "PairNoteLineMesh" };
			vertices = new Vector3[PoolCount * 4];
			uv = new Vector2[PoolCount * 4];
			triangles = new int[PoolCount * 6];
			ClearVertex();

			for (int i = 0; i < PoolCount; i++)
			{
				int vertexIndex = i * 4;
				uv[vertexIndex] = Vector2.zero;
				uv[vertexIndex + 1] = new Vector2(1f, 0f);
				uv[vertexIndex + 2] = new Vector2(0f, 1f);
				uv[vertexIndex + 3] = Vector2.one;

				int triangleIndex = i * 6;
				triangles[triangleIndex] = vertexIndex;
				triangles[triangleIndex + 1] = vertexIndex + 1;
				triangles[triangleIndex + 2] = vertexIndex + 2;
				triangles[triangleIndex + 3] = vertexIndex + 2;
				triangles[triangleIndex + 4] = vertexIndex + 1;
				triangles[triangleIndex + 5] = vertexIndex + 3;
			}

			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;
			mesh.MarkDynamic();
			filter.sharedMesh = mesh;
		}

		public void Add(INote note)
		{
			if (!ShouldAdd(note))
			{
				return;
			}

			if (pairNotes == null)
			{
				pairNotes = new List<INote>(PoolCount);
			}
			if (!pairNotes.Contains(note) && pairNotes.Count < PoolCount)
			{
				pairNotes.Add(note);
			}
		}

		public void Clear()
		{
			pairNotes?.Clear();
			ClearVertex();
			ApplyVertices();
		}

		public void Execute()
		{
			if (pairNotes == null || vertices == null)
			{
				return;
			}

			for (int i = 0; i < pairNotes.Count; i++)
			{
				INote note = pairNotes[i];
				if (ShouldRemove(note))
				{
					pairNotes.RemoveAt(i);
					i--;
				}
			}

			for (int i = 0; i < PoolCount; i++)
			{
				int vertexIndex = i * 4;
				if (i >= pairNotes.Count || !UpdateLine(vertexIndex, pairNotes[i]))
				{
					ClearVertex(vertexIndex);
				}
			}

			ApplyVertices();
		}

		private bool ShouldAdd(INote note)
		{
			INote pairNote = note != null ? note.PairNote : null;
			if (pairNote == null)
			{
				return false;
			}
			if (note.LaneStart >= pairNote.LaneStart)
			{
				return false;
			}
			if (IsPairLineHiddenCategory(note.Category) || IsPairLineHiddenCategory(pairNote.Category))
			{
				return false;
			}
			return IsSamePairTiming(note, pairNote);
		}

		private static bool ShouldRemove(INote note)
		{
			INote pairNote = note != null ? note.PairNote : null;
			return note == null ||
				pairNote == null ||
				note.State > NoteState.Playing ||
				pairNote.State >= NoteState.InputBegan;
		}

		private static bool IsPairLineHiddenCategory(NoteCategory category)
		{
			return category == NoteCategory.Connection || category == NoteCategory.Hidden;
		}

		private static bool IsSamePairTiming(INote note, INote pairNote)
		{
			NoteBase noteBase = note as NoteBase;
			NoteBase pairBase = pairNote as NoteBase;
			if (noteBase != null && pairBase != null)
			{
				return noteBase.speedRatio.Equals(pairBase.speedRatio);
			}
			return Mathf.Approximately(note.MusicScoreInfo.time, pairNote.MusicScoreInfo.time);
		}

		private bool UpdateLine(int vertexIndex, INote note)
		{
			if (note == null || note.State != NoteState.Playing || note.Progress <= 0f)
			{
				return false;
			}

			INote pairNote = note.PairNote;
			if (pairNote == null || LiveConfig.JudgmentPositions == null)
			{
				return false;
			}

			float progress = LiveConfig.GetNoteViewProgress(note.Progress);
			if (note.OffsetJudgeTime >= 0f && note is LongNote)
			{
				progress = 1f;
			}

			Vector2 noteCenter = GetNoteCenterPosition(note);
			Vector2 pairCenter = GetNoteCenterPosition(pairNote);
			Vector2 start = LiveUtility.EarlyVec2Lerp(LiveConfig.SpawnPosition, noteCenter, progress);
			Vector2 end = LiveUtility.EarlyVec2Lerp(LiveConfig.SpawnPosition, pairCenter, progress);
			float halfWidth = progress * LineHalfWidth;

			vertices[vertexIndex] = new Vector3(start.x, start.y + halfWidth, 0f);
			vertices[vertexIndex + 1] = new Vector3(end.x, end.y + halfWidth, 0f);
			vertices[vertexIndex + 2] = new Vector3(start.x, start.y - halfWidth, 0f);
			vertices[vertexIndex + 3] = new Vector3(end.x, end.y - halfWidth, 0f);
			return true;
		}

		private static Vector2 GetNoteCenterPosition(INote note)
		{
			Vector2 left = GetLanePosition(note.LaneStart);
			Vector2 right = GetLanePosition(note.LaneEnd);
			return Vector2.Lerp(left, right, Mathf.Clamp01(0.5f));
		}

		private static Vector2 GetLanePosition(int lane)
		{
			Vector2[] positions = LiveConfig.JudgmentPositions;
			if (positions == null || positions.Length == 0)
			{
				return Vector2.zero;
			}

			int index = Mathf.Clamp(lane, 0, positions.Length - 1);
			return positions[index];
		}

		private void ClearVertex()
		{
			if (vertices == null)
			{
				return;
			}
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = Vector3.zero;
			}
		}

		private void ClearVertex(int vertexIndex)
		{
			if (vertices == null || vertexIndex + 3 >= vertices.Length)
			{
				return;
			}
			vertices[vertexIndex] = Vector3.zero;
			vertices[vertexIndex + 1] = Vector3.zero;
			vertices[vertexIndex + 2] = Vector3.zero;
			vertices[vertexIndex + 3] = Vector3.zero;
		}

		private void ApplyVertices()
		{
			if (mesh == null)
			{
				return;
			}
			mesh.vertices = vertices;
			mesh.RecalculateBounds();
		}

		private static Material CreateFallbackMaterial()
		{
			Shader shader = Shader.Find("Sekai/Unlit/NotePairLine");
			if (shader == null)
			{
				shader = Shader.Find("Sprites/Default");
			}
			return shader != null ? new Material(shader) : null;
		}
	}
}
