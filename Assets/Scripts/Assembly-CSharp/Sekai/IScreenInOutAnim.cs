using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sekai
{
	public interface IScreenInOutAnim
	{
		void Play(Action callback);

		UniTask Play(CancellationToken ct = default(CancellationToken));

		void Setup(GameObject obj);

		void SetVector(Vector3 start, Vector3 end);

		void SetValue(float start, float end);

		void Destory();
	}
}
