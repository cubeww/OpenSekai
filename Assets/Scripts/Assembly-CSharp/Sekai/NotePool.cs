using System.Collections.Generic;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class NotePool : MonoBehaviour
	{
		private GameObject notePrefab;

		private List<BaseNoteView> noteArray;

		private int nextSpawinIndex;

		private float noteShowRate;

		public void Setup(int poolCount, GameObject notePrefab, float noteShowRate)
		{
			this.notePrefab = notePrefab;
			this.noteShowRate = noteShowRate;
			nextSpawinIndex = 0;
			noteArray = new List<BaseNoteView>();

			int count = Mathf.Max(poolCount, 1);
			for (int i = 0; i < count; i++)
			{
				CreateNote(noteShowRate);
			}
		}

		public void SetupNoteMask(float NoteShowRate)
		{
			if (noteArray == null)
			{
				return;
			}

			foreach (BaseNoteView noteView in noteArray)
			{
				if (noteView != null)
				{
					noteView.Setup(LiveConfig.SpawnPosition, LiveConfig.JudgmentPositions, NoteShowRate);
				}
			}
		}

		private BaseNoteView CreateNote(float noteShowRate)
		{
			if (notePrefab == null)
			{
				Debug.LogWarningFormat("NotePool {0} has no note prefab.", name);
				return null;
			}

			GameObject instance = Instantiate(notePrefab, transform);
			BaseNoteView noteView = instance.GetComponent<BaseNoteView>();
			if (noteView == null)
			{
				Debug.LogWarningFormat("Note prefab {0} does not contain BaseNoteView.", notePrefab.name);
				Destroy(instance);
				return null;
			}

			noteView.Setup(LiveConfig.SpawnPosition, LiveConfig.JudgmentPositions, noteShowRate);
			noteView.Unspawn();
			noteArray.Add(noteView);
			return noteView;
		}

		public BaseNoteView Spawn(INote note)
		{
			if (noteArray == null || noteArray.Count == 0)
			{
				CreateNote(noteShowRate);
			}

			if (noteArray == null || noteArray.Count == 0)
			{
				return null;
			}

			BaseNoteView noteView = noteArray[nextSpawinIndex];
			nextSpawinIndex = (nextSpawinIndex + 1) % noteArray.Count;
			if (noteView == null || noteView.gameObject.activeSelf)
			{
				noteView = CreateNote(noteShowRate);
			}

			if (noteView == null || note == null)
			{
				return null;
			}

			float z = note.MusicScoreInfo.time * 0.001f + note.LaneStart * 0.0000001f;
			noteView.Spawn(note, z);
			return noteView;
		}

		public NotePool()
		{
		}
	}
}
