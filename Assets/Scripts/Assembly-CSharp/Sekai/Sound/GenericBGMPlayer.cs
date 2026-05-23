using System;
using JetBrains.Annotations;

namespace Sekai.Sound
{
	public class GenericBGMPlayer : IDisposable
	{
		private string _soundBundleName;

		private string _soundFileName;

		private bool _isDisposed;

		private bool _isPushed;

		private bool _isFadeLoop;

		private float _loopFadeTime;

		private Action<bool> _onLoadCompleted;

		private void PushBGM()
		{
			throw null;
		}

		private void PopBGM()
		{
			throw null;
		}

		public void Load([NotNull] string soundBundleName, [NotNull] string soundFileName, Action<bool> onLoadCompleted = null, bool isFadeLoop = false, float loopFadeTime = 0.5f)
		{
			throw null;
		}

		private void OnFinishedLoad(bool isSuccess)
		{
			throw null;
		}

		public void Reload([NotNull] string soundBundleName, [NotNull] string soundFileName, Action<bool> onPushCompleted = null, bool isFadeLoop = false, float loopFadeTime = 0.5f)
		{
			throw null;
		}

		public void Unload(float delay = 0.25f, bool isPopBGM = true)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public void DisposeWithoutPopBGM()
		{
			throw null;
		}

		private void DisposeInternal(bool isPopBGM)
		{
			throw null;
		}

		public GenericBGMPlayer()
		{
			throw null;
		}
	}
}
