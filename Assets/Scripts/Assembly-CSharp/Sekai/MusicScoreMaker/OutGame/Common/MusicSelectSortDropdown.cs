using System;
using System.Collections.Generic;
using Sekai.MusicShop;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	[RequireComponent(typeof(CustomDropdown))]
	public class MusicSelectSortDropdown : MonoBehaviour
	{
		[SerializeField]
		private CustomDropdown dropdown;

		private Action<SortType> _onCallback;

		private static Dictionary<SortType, string> SortWordDictionary
		{
			get
			{
				throw null;
			}
		}

		public void Setup(SortType sortType, Action<SortType> onCallback)
		{
			throw null;
		}

		private void OnValueChanged(int x)
		{
			throw null;
		}

		public MusicSelectSortDropdown()
		{
			throw null;
		}
	}
}
