using System.Collections.Generic;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class NoteLineView : MonoBehaviour
	{
		private struct TwoPoint
		{
			public Vector2 Left;

			public Vector2 Right;
		}

		private struct Setting
		{
			public float CurrentViewProgress;

			public TwoPoint LanePosition;

			public float LineProgress;
		}

		private struct Trapezoid
		{
			public TwoPoint Top;

			public TwoPoint Bottom;
		}

		private static readonly int TCount = 3;

		private static int SplitMeshCount = 100;

		private static int PoolCount = SplitMeshCount * 10;

		private static readonly int TrapezoidVertexCount = 4 * TCount - 4;

		private static readonly int TrapezoidIndexCount = 6 * TCount;

		private static readonly Color PressColor = Color.white;

		private static readonly Color ReleaseColor = Color.Lerp(Color.white, new Color(0f, 0.2666f, 0.1333f, 1f), 0.4f);

		private static readonly Vector2[] OffsetValue =
		{
			new Vector2(0f, 0.125f),
			new Vector2(0.875f, 1f)
		};

		private MeshFilter filter;

		private MeshRenderer meshRenderer;

		private Mesh mesh;

		private Vector3[] vertices;

		private Vector2[] uv;

		private Vector2[] leftPoint;

		private Vector2[] rightPoint;

		private Vector2[] uvOffsetLeft;

		private Vector2[] uvOffsetRight;

		private Color[] colors;

		private int[] triangles;

		private List<NoteBase> longNotes;

		private Vector2 statusValue;

		private Setting[] meshSettingBuffer;

		private Trapezoid[] trapezoidVertexInfo;

		private int meshCount;

		private LiveBundleBuildData bundleBuildData;

		public void Setup(LiveBundleBuildData bundleBuildData, Texture texture, Material sourceMaterial)
		{
			this.bundleBuildData = bundleBuildData;

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
					material.mainTexture = texture;
				}
				material.color = new Color(1f, 1f, 1f, LiveConfig.LongNoteAlpha);
				meshRenderer.material = material;
				meshRenderer.sortingOrder = 29;
			}

			longNotes = new List<NoteBase>(64);
			trapezoidVertexInfo = new Trapezoid[2];
			meshSettingBuffer = new Setting[SplitMeshCount + 10];
			SetupVertexArray();
			mesh = new Mesh { name = "NoteLineMesh" };
			mesh.MarkDynamic();
			ApplyMeshData();
			filter.sharedMesh = mesh;
			meshCount = 0;
		}

		public void Add(LongNote note)
		{
			if (note != null && longNotes != null && !longNotes.Contains(note))
			{
				longNotes.Add(note);
			}
		}

		public void Remove(LongNote note)
		{
			if (note != null && longNotes != null)
			{
				longNotes.Remove(note);
			}
		}

		public void Add(GuideNote note)
		{
			if (note != null && longNotes != null && !longNotes.Contains(note))
			{
				longNotes.Add(note);
			}
		}

		public void Remove(GuideNote note)
		{
			if (note != null && longNotes != null)
			{
				longNotes.Remove(note);
			}
		}

		public void Clear()
		{
			longNotes?.Clear();
			ClearVertex(0, PoolCount);
			meshCount = 0;
			ApplyMeshData();
		}

		public void Excute(float time)
		{
			if (longNotes == null)
			{
				return;
			}

			int previousMeshCount = meshCount;
			meshCount = 0;
			for (int i = 0; i < longNotes.Count; i++)
			{
				NextExecute(longNotes[i], time, ref meshCount);
				if (meshCount >= PoolCount)
				{
					meshCount = PoolCount;
					break;
				}
			}

			if (previousMeshCount > meshCount)
			{
				ClearVertex(meshCount, previousMeshCount - meshCount);
			}

			ApplyMeshData();
		}

		private void NextExecute(INote baseNote, float time, ref int currentMeshCount)
		{
			List<INote> viewNotes = baseNote != null ? baseNote.ViewNoteList : null;
			if (viewNotes == null || viewNotes.Count < 2)
			{
				return;
			}

			INote startNote = viewNotes[0];
			for (int i = 1; i < viewNotes.Count; i++)
			{
				INote endNote = viewNotes[i];
				if (endNote == null || endNote.IsSkip)
				{
					continue;
				}
				if (startNote == null)
				{
					startNote = endNote;
					continue;
				}

				if (startNote.Progress > endNote.Progress)
				{
					int meshDataCount = GetCenterPosition(time, startNote, endNote);
					if (meshDataCount >= 2)
					{
						if (currentMeshCount + meshDataCount > PoolCount)
						{
							currentMeshCount += meshDataCount;
							return;
						}
						MeshCreate(ref currentMeshCount, time, baseNote, meshDataCount);
					}
				}
				startNote = endNote;
			}
		}

		private void MeshCreate(ref int currentMeshCount, float time, INote baseNote, int meshDataCount)
		{
			if (meshSettingBuffer == null || meshDataCount < 2)
			{
				return;
			}

			SetStatusValue(time, baseNote);
			Color color = baseNote != null && baseNote.State == NoteState.Release ? ReleaseColor : PressColor;
			for (int i = 0; i < meshDataCount - 1; i++)
			{
				if (currentMeshCount >= PoolCount)
				{
					return;
				}

				int vertexIndex = currentMeshCount * TrapezoidVertexCount;
				CalculateTrapezoidVertex(meshSettingBuffer[i], meshSettingBuffer[i + 1]);
				UpdateVertex(vertexIndex, color);
				UpdateUV(vertexIndex, i);
				currentMeshCount++;
			}
		}

		private int GetCenterPosition(float time, INote startNote, INote endNote)
		{
			if (startNote == null || endNote == null || meshSettingBuffer == null)
			{
				return 0;
			}

			NoteBase endNoteBase = endNote as NoteBase;
			float endDisplayOffset = 0f;
			if (endNote.State != NoteState.Wait)
			{
				endDisplayOffset = endNoteBase != null ? endNoteBase.offsetTime : 0f;
			}
			else if (endNoteBase != null && endNoteBase.CalcTimeOffset != null)
			{
				endDisplayOffset = endNoteBase.CalcTimeOffset(endNoteBase);
			}

			float startTime = startNote.MusicScoreInfo.time;
			float endTime = endNote.MusicScoreInfo.time;
			float duration = endTime - startTime;
			if (Mathf.Approximately(duration, 0f))
			{
				return 0;
			}

			float visibleStart = 1f - Mathf.Clamp01((endTime - time) / duration);
			float visibleEnd = 1f - Mathf.Clamp01((endTime - endDisplayOffset - time) / duration);
			float startProgress = Mathf.Min(startNote.Progress, 1f);
			float endProgress = Mathf.Min(endNote.Progress, 1f);
			int splitCount = Mathf.CeilToInt((startProgress - endProgress) * SplitMeshCount);
			if (splitCount < 0)
			{
				return splitCount + 1;
			}

			float progressDistance = endProgress - startProgress;
			float lineT = visibleStart;
			float lineStep = splitCount == 0 ? 0f : (visibleEnd - visibleStart) / splitCount;
			float startCenterLane = (startNote.DefaultLeftLane + startNote.DefaultRightLane) * 0.5f;
			float endCenterLane = (endNote.DefaultLeftLane + endNote.DefaultRightLane) * 0.5f;
			float startLaneWidth = startNote.DefaultRightLane - startNote.DefaultLeftLane;
			float endLaneWidth = endNote.DefaultRightLane - endNote.DefaultLeftLane;
			List<INote> viewNotes = startNote.ParentNote != null ? startNote.ParentNote.ViewNoteList : startNote.ViewNoteList;
			int viewNoteIndex = viewNotes != null ? Mathf.Max(viewNotes.IndexOf(startNote), 0) : 0;
			int viewNoteMaxIndex = viewNotes != null ? viewNotes.Count - 1 : 0;

			for (int i = 0; i <= splitCount && i < meshSettingBuffer.Length; i++)
			{
				float currentLineT = lineT;
				float lineProgress = LiveConfig.GetNoteLineCenterProgress(currentLineT, startNote);
				float viewProgress = LiveConfig.GetNoteViewProgress(startProgress + progressDistance * (splitCount == 0 ? 0f : (float)i / splitCount));
				if (startNote.OffsetJudgeTime <= 0f && viewProgress > 1f)
				{
					viewProgress = 1f;
				}

				float centerLane = Mathf.Lerp(startCenterLane, endCenterLane, lineProgress);
				Vector2 centerPosition = GetJudgmentPosition(centerLane);
				centerPosition = LiveUtility.EarlyVec2Lerp(LiveConfig.SpawnPosition, centerPosition, viewProgress);

				float lineWidth = Mathf.Lerp(startLaneWidth, endLaneWidth, lineProgress);
				float halfWidth = viewProgress * (((lineWidth + 1f) * LiveConfig.widthX) - 0.1f) * 0.5f;
				meshSettingBuffer[i] = new Setting
				{
					CurrentViewProgress = viewProgress,
					LanePosition = new TwoPoint
					{
						Left = new Vector2(centerPosition.x - halfWidth, centerPosition.y),
						Right = new Vector2(centerPosition.x + halfWidth, centerPosition.y)
					},
					LineProgress = viewNoteMaxIndex > 0 ? (currentLineT + viewNoteIndex) / viewNoteMaxIndex : 0f
				};

				lineT = lineStep + currentLineT <= visibleEnd ? lineStep + currentLineT : visibleEnd;
			}

			return splitCount + 1;
		}

		private void SetupVertexArray()
		{
			int vertexCount = PoolCount * TrapezoidVertexCount;
			vertices = new Vector3[vertexCount];
			leftPoint = new Vector2[vertexCount];
			rightPoint = new Vector2[vertexCount];
			uvOffsetLeft = new Vector2[vertexCount];
			uvOffsetRight = new Vector2[vertexCount];
			uv = new Vector2[vertices.Length];
			colors = new Color[vertices.Length];
			triangles = new int[PoolCount * TrapezoidIndexCount];
			ClearVertex(0, PoolCount);

			for (int i = 0; i < PoolCount; i++)
			{
				int vertexBase = i * TrapezoidVertexCount;
				for (int j = 0; j < TrapezoidVertexCount; j++)
				{
					uv[vertexBase + j] = new Vector2(0f, j & 1);
					colors[vertexBase + j] = Color.white;
				}

				int indexBase = i * TrapezoidIndexCount;
				int triangleVertex = vertexBase + 3;
				for (int j = 0; j < TCount; j++)
				{
					int triangleBase = indexBase + j * 6;
					triangles[triangleBase] = triangleVertex - 3;
					triangles[triangleBase + 1] = triangleVertex - 2;
					triangles[triangleBase + 2] = triangleVertex - 1;
					triangles[triangleBase + 3] = triangleVertex - 2;
					triangles[triangleBase + 4] = triangleVertex;
					triangles[triangleBase + 5] = triangleVertex - 1;
					triangleVertex += 2;
				}
			}
		}

		private void SetStatusValue(float time, INote baseNote)
		{
			if (baseNote == null)
			{
				statusValue = Vector2.zero;
				return;
			}

			float status = 0f;
			if (baseNote.State >= NoteState.Input)
			{
				float t = (time - baseNote.MusicScoreInfo.time) * Mathf.PI * 4f;
				status = 0.5f - Mathf.Cos(t) * 0.5f;
			}

			statusValue.x = status;
			// longNoteLine.png is arranged top-to-bottom as: default dark/light, critical dark/light.
			// Texture V starts at the bottom, so critical samples the lower band and default samples the upper band.
			statusValue.y = baseNote.Type == NoteType.Critical ? 0.025f : 0.525f;
		}

		private void CalculateTrapezoidVertex(Setting startSetting, Setting endSetting)
		{
			if (trapezoidVertexInfo == null || trapezoidVertexInfo.Length < 2)
			{
				trapezoidVertexInfo = new Trapezoid[2];
			}

			float startWidth = startSetting.CurrentViewProgress * 0.4f;
			float endWidth = endSetting.CurrentViewProgress * 0.4f;

			trapezoidVertexInfo[0].Top.Right = endSetting.LanePosition.Left;
			trapezoidVertexInfo[0].Top.Left = endSetting.LanePosition.Left + new Vector2(-endWidth, 0f);
			trapezoidVertexInfo[0].Bottom.Right = startSetting.LanePosition.Left;
			trapezoidVertexInfo[0].Bottom.Left = startSetting.LanePosition.Left + new Vector2(-startWidth, 0f);

			trapezoidVertexInfo[1].Top.Right = endSetting.LanePosition.Right + new Vector2(endWidth, 0f);
			trapezoidVertexInfo[1].Top.Left = endSetting.LanePosition.Right;
			trapezoidVertexInfo[1].Bottom.Right = startSetting.LanePosition.Right + new Vector2(startWidth, 0f);
			trapezoidVertexInfo[1].Bottom.Left = startSetting.LanePosition.Right;
		}

		private void UpdateVertex(int vertexIndex, Color color)
		{
			if (trapezoidVertexInfo == null)
			{
				return;
			}

			for (int i = 0; i < trapezoidVertexInfo.Length; i++)
			{
				int index = vertexIndex + i * 4;
				if (index + 3 >= vertices.Length)
				{
					return;
				}

				Trapezoid trapezoid = trapezoidVertexInfo[i];
				SetVertex(index + 3, trapezoid.Top.Right, trapezoid.Top.Left.x, trapezoid.Top.Right.x, i, color);
				SetVertex(index + 1, trapezoid.Top.Left, trapezoid.Top.Left.x, trapezoid.Top.Right.x, i, color);
				SetVertex(index + 2, trapezoid.Bottom.Right, trapezoid.Bottom.Left.x, trapezoid.Bottom.Right.x, i, color);
				SetVertex(index, trapezoid.Bottom.Left, trapezoid.Bottom.Left.x, trapezoid.Bottom.Right.x, i, color);
			}
		}

		private void UpdateUV(int vertexIndex, int meshDataIndex)
		{
			if (uv == null || meshSettingBuffer == null || meshDataIndex + 1 >= meshSettingBuffer.Length)
			{
				return;
			}

			float start = meshSettingBuffer[meshDataIndex].LineProgress;
			float end = meshSettingBuffer[meshDataIndex + 1].LineProgress;
			for (int i = 0; i < 2; i++)
			{
				int index = vertexIndex + i * 4;
				if (index + 3 >= uv.Length)
				{
					return;
				}

				Vector2 v0 = uv[index];
				Vector2 v1 = uv[index + 1];
				Vector2 v2 = uv[index + 2];
				Vector2 v3 = uv[index + 3];
				v0.y = start;
				v2.y = start;
				v1.y = end;
				v3.y = end;
				uv[index] = v0;
				uv[index + 1] = v1;
				uv[index + 2] = v2;
				uv[index + 3] = v3;
			}
		}

		private void SetVertex(int index, Vector2 position, float left, float right, int sideIndex, Color color)
		{
			vertices[index] = new Vector3(position.x, position.y, 0f);
			if (leftPoint != null && index < leftPoint.Length)
			{
				leftPoint[index] = new Vector2(0f, left);
			}
			if (rightPoint != null && index < rightPoint.Length)
			{
				rightPoint[index] = new Vector2(0f, right);
			}

			Vector2 offset = OffsetValue != null && sideIndex < OffsetValue.Length ? OffsetValue[sideIndex] : Vector2.zero;
			if (uvOffsetLeft != null && index < uvOffsetLeft.Length)
			{
				uvOffsetLeft[index] = new Vector2(statusValue.x, offset.x);
			}
			if (uvOffsetRight != null && index < uvOffsetRight.Length)
			{
				uvOffsetRight[index] = new Vector2(statusValue.y, offset.y);
			}
			if (colors != null && index < colors.Length)
			{
				colors[index] = color;
			}
		}

		private void ClearVertex(int startIndex, int length)
		{
			if (vertices == null)
			{
				return;
			}

			int vertexStart = Mathf.Clamp(startIndex, 0, PoolCount) * TrapezoidVertexCount;
			int vertexLength = Mathf.Clamp(length, 0, PoolCount - startIndex) * TrapezoidVertexCount;
			for (int i = vertexStart; i < vertexStart + vertexLength && i < vertices.Length; i++)
			{
				vertices[i] = Vector3.zero;
				if (leftPoint != null && i < leftPoint.Length)
				{
					leftPoint[i] = Vector2.zero;
				}
				if (rightPoint != null && i < rightPoint.Length)
				{
					rightPoint[i] = Vector2.zero;
				}
				if (uvOffsetLeft != null && i < uvOffsetLeft.Length)
				{
					uvOffsetLeft[i] = Vector2.zero;
				}
				if (uvOffsetRight != null && i < uvOffsetRight.Length)
				{
					uvOffsetRight[i] = Vector2.zero;
				}
				if (colors != null && i < colors.Length)
				{
					colors[i] = Color.clear;
				}
			}
		}

		private void ApplyMeshData()
		{
			if (mesh == null)
			{
				return;
			}

			mesh.vertices = vertices;
			mesh.colors = colors;
			mesh.uv = uv;
			mesh.uv2 = leftPoint;
			mesh.uv3 = rightPoint;
			mesh.uv4 = uvOffsetLeft;
			mesh.uv5 = uvOffsetRight;
			mesh.triangles = triangles;
			mesh.RecalculateBounds();
			if (filter != null)
			{
				filter.sharedMesh = mesh;
			}
		}

		private static Vector2 GetJudgmentPosition(float lane)
		{
			Vector2[] positions = LiveConfig.JudgmentPositions;
			if (positions == null || positions.Length == 0)
			{
				return Vector2.zero;
			}

			float clampedLane = Mathf.Clamp(lane, 0f, positions.Length - 1);
			int start = Mathf.FloorToInt(clampedLane);
			int end = Mathf.CeilToInt(clampedLane);
			float t = Mathf.Clamp01(clampedLane - start);
			return LiveUtility.EarlyVec2Lerp(positions[start], positions[end], t);
		}

		private static Material CreateFallbackMaterial()
		{
			Shader shader = Shader.Find("Sekai/Unlit/NoteLine");
			if (shader == null)
			{
				shader = Shader.Find("Sprites/Default");
			}
			return shader != null ? new Material(shader) : null;
		}
	}
}
