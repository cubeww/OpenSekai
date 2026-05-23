using UnityEngine;

namespace Sekai.UI
{
	public class ListViewItem : MonoBehaviour
	{
		[SerializeField]
		private float sizeX;

		[SerializeField]
		private float sizeY;

		protected int currentIndex;

		public Vector3 SettingAnchoredPosition { get; set; }

		public float SizeX
		{
			get
			{
				return sizeX;
			}
		}

		public float SizeY
		{
			get
			{
				return sizeY;
			}
		}

		public int CurrentIndex
		{
			get
			{
				return currentIndex;
			}
		}

		public virtual void SetData(int index)
		{
			currentIndex = index;
		}

		public ListViewItem()
		{
		}
	}
}
