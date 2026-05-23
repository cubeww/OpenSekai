using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	[RequireComponent(typeof(CustomButton))]
	public class UIPartsFilterButton : MonoBehaviour
	{
		[SerializeField]
		private CustomButton _button;

		[SerializeField]
		protected CustomImage offImage;

		[SerializeField]
		protected CustomImage onImage;

		private Action onClick;

		private bool filter;

		public bool Filter
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public void Initialize(Action onClick)
		{
			throw null;
		}

		public void Setup(bool filter, Action onClick = null)
		{
			throw null;
		}

		public void SetCallback(Action onClick)
		{
			throw null;
		}

		public void SetDisplayEnableFilter(bool enabled)
		{
			throw null;
		}

		public void Show()
		{
			throw null;
		}

		public void Hide()
		{
			throw null;
		}

		public void EnableClick()
		{
			throw null;
		}

		public void DisableClick()
		{
			throw null;
		}

		public void SetEnabled(bool enabled)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		private void OnClick()
		{
			throw null;
		}

		private void UpdateImage()
		{
			throw null;
		}

		public UIPartsFilterButton()
		{
		}
	}
}
