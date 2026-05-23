using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sekai.MusicScoreMaker.OutGame.Common.Content;

namespace Sekai.MusicScoreMaker.OutGame.Common.Category
{
	public interface ICategoryPresenter : IContentNavigator, IDisposable
	{
		IScreenNavigator ScreenNavigator { get; }

		bool IsEnableBackContent { get; }

		UniTask SetupAsync(IScreenNavigator screenNavigator, Defines.ContentType defaultContentType, CancellationToken ct);

		UniTask SetupForCustomInitialContentAsync(IScreenNavigator screenNavigator, ContentNavigationData initialContent, ContentNavigationData[] contentsToAddToHistory, CancellationToken ct);

		UniTask OnEnterAsync(CancellationToken ct);

		UniTask OnExitAsync(CancellationToken ct);

		void OnWillExit();
	}
}
