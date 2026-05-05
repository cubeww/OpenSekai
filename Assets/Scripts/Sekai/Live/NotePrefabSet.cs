using System;
using UnityEngine;

namespace Sekai.Live
{
	[Serializable]
	public class NotePrefabSet
	{
		public NoteCategory category;

		public GameObject defaultPrefab;

		public GameObject criticalPrefab;
	}
}
