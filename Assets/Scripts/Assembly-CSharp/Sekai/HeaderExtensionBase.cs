using System;
using UnityEngine;

namespace Sekai
{
	public abstract class HeaderExtensionBase : MonoBehaviour
	{
		public Action<HeaderExtensionBase> OnInitialize;

		public abstract HeaderCategory HeaderCategory { get; }

		public virtual void Initialize()
		{
			OnInitialize?.Invoke(this);
		}

		public virtual void UpdateInfo()
		{
		}
	}
}
