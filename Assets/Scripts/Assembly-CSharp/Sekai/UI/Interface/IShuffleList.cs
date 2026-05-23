using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sekai.UI.Interface
{
	public interface IShuffleList
	{
		UniTask ExecuteShuffleAsync(float power = 10000f, CancellationToken ct = default(CancellationToken));

		UniTask ExecuteShuffleAsync<T>(T listViewItem, float power = 10000f, CancellationToken ct = default(CancellationToken));
	}
}
