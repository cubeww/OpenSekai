using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public interface IScreenNavigator
	{
		bool GetShowingHeader();

		void ShowHeaderImmediate();

		UniTask ShowHeaderAsync(CancellationToken ct);

		UniTask HideHeaderAsync(CancellationToken ct);

		UniTask<bool> OnBackBeforeContentAsync();

		MenuScreenType GetScreenType();
	}
}
