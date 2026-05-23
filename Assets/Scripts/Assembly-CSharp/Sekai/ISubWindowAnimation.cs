using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sekai
{
	public interface ISubWindowAnimation
	{
		void InitializeAnimation();

		UniTask OpenAnimation(CancellationToken ct = default(CancellationToken));

		UniTask CloseAnimation(CancellationToken ct = default(CancellationToken));

		void OpenImmediately();

		void CloseImmediately();

		void Complete();
	}
}
