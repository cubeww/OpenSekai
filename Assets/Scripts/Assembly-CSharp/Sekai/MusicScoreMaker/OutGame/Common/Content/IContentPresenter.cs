using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sekai.MusicScoreMaker.OutGame.Common.Content
{
	public interface IContentPresenter : IDisposable
	{
		bool IsBooted { get; }

		bool IsPaused { get; }

		UniTask SetupAsync(IScreenNavigator screenNavigator, IContentNavigator contentNavigator, CancellationToken ct);

		void SetBootData(IContentBootData bootData);

		IContentBootData GetBootData();

		IContentBootData CreateDefaultBootData();

		UniTask BootAsync(Defines.ApplyContentType applyContentType, bool enableBack, CancellationToken ct);

		UniTask ExitAsync(CancellationToken ct);

		void OnWillExit();

		UniTask PauseAsync(CancellationToken ct);

		UniTask ResumeAsync(CancellationToken ct);
	}
}
