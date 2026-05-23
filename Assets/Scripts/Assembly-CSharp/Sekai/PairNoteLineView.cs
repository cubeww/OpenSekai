using System.Collections.Generic;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class PairNoteLineView : MonoBehaviour
	{
		private const int PoolCount = 50;

		private Renderer renderer;
		private MeshFilter filter;
		private Mesh mesh;
		private Vector3[] vertices;
		private Vector2[] uv;
		private int[] triangles;
		private List<INote> pairNotes;

		public Material Material { get; private set; }

		public void Setup()
		{
			filter = gameObject.AddComponent<MeshFilter>();
			renderer = gameObject.AddComponent<MeshRenderer>();

			Material source = Resources.Load<Material>("Materials/Ingame/PairNoteLine")
				?? Resources.Load<Material>("materials/ingame/PairNoteLine");
			renderer.material = source != null ? new Material(source) : new Material(Shader.Find("Sprites/Default"));
			Material = renderer.material;

			Texture texture = AssetBundleUtility.LoadAsset<Texture>(LiveConfig.NoteBundleName, "simultaneousLine.png", false);
			if (texture != null)
			{
				Material.SetTexture("_MainTex", texture);
			}

			pairNotes = new List<INote>(PoolCount);
			mesh = new Mesh { name = name + "Mesh" };
			vertices = new Vector3[PoolCount * 4];
			uv = new Vector2[PoolCount * 4];
			triangles = new int[PoolCount * 6];

			for (int i = 0; i < PoolCount; i++)
			{
				int vertexIndex = i * 4;
				int triangleIndex = i * 6;
				uv[vertexIndex] = Vector2.zero;
				uv[vertexIndex + 1] = Vector2.right;
				uv[vertexIndex + 2] = Vector2.up;
				uv[vertexIndex + 3] = Vector2.one;

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
			filter.mesh = mesh;
		}

		public void Add(INote note)
		{
			if (note == null || note.PairNote == null)
			{
				return;
			}

			INote pair = note.PairNote;
			if (note.DefaultLeftLane >= pair.DefaultLeftLane)
			{
				return;
			}

			if (note.Category == NoteCategory.Hidden || pair.Category == NoteCategory.Hidden
				|| note.Category == NoteCategory.Connection || pair.Category == NoteCategory.Connection)
			{
				return;
			}

			if (note is NoteBase noteBase && pair is NoteBase pairBase
				&& !Mathf.Approximately(noteBase.MusicScoreInfo.time, pairBase.MusicScoreInfo.time))
			{
				return;
			}

			pairNotes.Add(note);
		}

		public void Clear()
		{
			pairNotes?.Clear();
			ClearVertex();
			ApplyMesh();
		}

		public void Execute()
		{
			if (mesh == null || pairNotes == null)
			{
				return;
			}

			for (int i = 0; i < pairNotes.Count; i++)
			{
				INote note = pairNotes[i];
				if (note == null || note.State > NoteState.Playing || note.PairNote == null || note.PairNote.State >= NoteState.InputBegan)
				{
					pairNotes.RemoveAt(i--);
				}
			}

			for (int i = 0; i < PoolCount; i++)
			{
				int vertexIndex = i * 4;
				if (i >= pairNotes.Count)
				{
					ClearVertex(vertexIndex);
					continue;
				}

				INote note = pairNotes[i];
				if (note.State == NoteState.Playing && note.Progress > 0f)
				{
					float progress = LiveConfig.GetNoteViewProgress(note.Progress);
					if (note.OffsetJudgeTime >= 0f && note is LongNote)
					{
						progress = 1f;
					}

					Vector2 start = LiveUtility.EarlyVec2Lerp(LiveConfig.SpawnPosition, LaneCenter(note), progress);
					Vector2 end = LiveUtility.EarlyVec2Lerp(LiveConfig.SpawnPosition, LaneCenter(note.PairNote), progress);
					float halfHeight = progress * 0.2f;
					vertices[vertexIndex] = new Vector3(start.x, start.y + halfHeight, 0f);
					vertices[vertexIndex + 1] = new Vector3(end.x, end.y + halfHeight, 0f);
					vertices[vertexIndex + 2] = new Vector3(start.x, start.y - halfHeight, 0f);
					vertices[vertexIndex + 3] = new Vector3(end.x, end.y - halfHeight, 0f);
				}
			}

			ApplyMesh();
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
			if (vertices == null || vertexIndex < 0 || vertexIndex + 3 >= vertices.Length)
			{
				return;
			}

			vertices[vertexIndex] = Vector3.zero;
			vertices[vertexIndex + 1] = Vector3.zero;
			vertices[vertexIndex + 2] = Vector3.zero;
			vertices[vertexIndex + 3] = Vector3.zero;
		}

		private void ApplyMesh()
		{
			mesh.vertices = vertices;
			filter.mesh = mesh;
		}

		private static Vector2 LaneCenter(INote note)
		{
			return (LanePosition(note.LaneStart) + LanePosition(note.LaneEnd)) * 0.5f;
		}

		private static Vector2 LanePosition(float lane)
		{
			Vector2[] positions = LiveConfig.JudgmentPositions;
			if (positions == null || positions.Length == 0)
			{
				return Vector2.zero;
			}

			float clamped = Mathf.Clamp(lane, 0f, positions.Length - 1);
			int left = Mathf.Clamp(Mathf.FloorToInt(clamped), 0, positions.Length - 1);
			int right = Mathf.Clamp(Mathf.CeilToInt(clamped), 0, positions.Length - 1);
			return LiveUtility.EarlyVec2Lerp(positions[left], positions[right], clamped - left);
		}
	}
}
