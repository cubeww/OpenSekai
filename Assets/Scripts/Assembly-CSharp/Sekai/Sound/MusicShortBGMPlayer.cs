using System;
using JetBrains.Annotations;

namespace Sekai.Sound
{
	public sealed class MusicShortBGMPlayer : IDisposable
	{
		private readonly struct SoundInfo
		{
			public readonly string BundleName;

			public readonly string FileName;

			public SoundInfo(string bundleName, string fileName)
			{
				throw null;
			}
		}

		private readonly GenericBGMPlayer _genericBGMPlayer;

		public void Load([NotNull] MasterMusicAll masterMusicAll, bool isFadeLoop = false, float loopFadeTime = 0.5f)
		{
			throw null;
		}

		public void Reload([NotNull] MasterMusicAll masterMusicAll, bool isFadeLoop = false, float loopFadeTime = 0.5f)
		{
			throw null;
		}

		private bool TryGetSoundInfo([NotNull] MasterMusicAll masterMusicAll, out SoundInfo soundInfo)
		{
			throw null;
		}

		public void Unload()
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

		public MusicShortBGMPlayer()
		{
			throw null;
		}
	}
}
