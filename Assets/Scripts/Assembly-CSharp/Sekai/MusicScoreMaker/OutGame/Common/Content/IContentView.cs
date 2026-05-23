using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sekai.MusicScoreMaker.OutGame.Common.Content
{
	public interface IContentView : IDisposable
	{
		void SetActive(bool value);

		UniTask PlayInAnimationAsync(CancellationToken ct);

		UniTask PlayOutAnimationAsync(CancellationToken ct);

		UniTask PlayPrePauseAnimationAsync(CancellationToken ct);

		UniTask PlayPostResumeAnimationAsync(CancellationToken ct);

		UniTask RefreshAsync();
	}
}
