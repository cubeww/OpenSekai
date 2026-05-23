using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai.RankLive
{
	public class NumberView : MonoBehaviour
	{
		private enum OutOfDigitRangeView
		{
			Normal = 0,
			Gray = 1,
			Invisible = 2
		}

		[SerializeField]
		private int num;

		[SerializeField]
		private string prefix;

		[SerializeField]
		private string postfix;

		[SerializeField]
		private List<CustomImage> digits;

		[SerializeField]
		private OutOfDigitRangeView outOfDigitViewMode;

		private int digitCount;

		private Color defaultColor;

		private void Start()
		{
			throw null;
		}

		public void Setup()
		{
			throw null;
		}

		public void SetColor(Color color, bool needOverrideDefaultColor = false)
		{
			throw null;
		}

		public void UpdateNumber(int number, Vector2[] manualTextureSizes = null)
		{
			throw null;
		}

		public NumberView()
		{
			throw null;
		}
	}
}
