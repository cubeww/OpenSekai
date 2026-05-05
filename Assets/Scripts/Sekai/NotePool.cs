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

		public void Setup(int poolCount, GameObject notePrefab)
		{
			this.notePrefab = notePrefab;
			nextSpawinIndex = 0;
			noteArray = new List<BaseNoteView>();
			for (int i = 0; i < poolCount; i++)
			{
				CreateNote();
			}
		}

		private BaseNoteView CreateNote()
		{
			if (notePrefab == null)
			{
				return null;
			}

			GameObject noteObject = Instantiate(notePrefab, transform);
			BaseNoteView noteView = noteObject != null ? noteObject.GetComponent<BaseNoteView>() : null;
			if (noteView == null)
			{
				if (noteObject != null)
				{
					Destroy(noteObject);
				}
				return null;
			}

			noteView.Setup(LiveConfig.SpawnPosition, LiveConfig.JudgmentPositions);
			noteView.Unspawn();
			noteArray.Add(noteView);
			return noteView;
		}

		public BaseNoteView Spawn(INote note)
		{
			if (noteArray == null)
			{
				noteArray = new List<BaseNoteView>();
			}

			if (noteArray.Count == 0)
			{
				CreateNote();
			}
			if (noteArray.Count == 0)
			{
				return null;
			}

			BaseNoteView noteView = noteArray[nextSpawinIndex];
			nextSpawinIndex = (nextSpawinIndex + 1) % noteArray.Count;
			if (noteView != null && noteView.gameObject.activeSelf)
			{
				noteView = CreateNote();
			}

			if (noteView == null)
			{
				return null;
			}

			float z = note.MusicScoreInfo.time * 0.001f + note.DefaultLeftLane * 0.0000001f;
			noteView.Spawn(note, z);
			return noteView;
		}
	}
}
