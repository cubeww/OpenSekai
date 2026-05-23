using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sekai
{
	public abstract class ScreenInOutAnimBase : MonoBehaviour, IScreenInOutAnim
	{
		protected GameObject attachObject;

		protected Action callback;

		public virtual void Play(Action callback)
		{
			this.callback = callback;
			OnFinished();
		}

		public virtual UniTask Play(CancellationToken ct = default(CancellationToken))
		{
			return UniTask.CompletedTask;
		}

		public virtual void Setup(GameObject obj)
		{
			attachObject = obj;
		}

		public virtual void SetVector(Vector3 start, Vector3 end)
		{
		}

		public virtual void SetValue(float start, float end)
		{
		}

		public virtual void Destory()
		{
		}

		protected void OnFinished()
		{
			callback?.Invoke();
			callback = null;
		}

		protected ScreenInOutAnimBase()
		{
		}
	}
}
