using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame.Common
{
	public sealed class BookmarkTag : MonoBehaviour
	{
		public enum BookmarkStatus
		{
			None = 0,
			Off = 1,
			On = 2
		}

		public const string ANIM_ACTIVATING_STATE_NAME = "Activating";

		public const string ANIM_CANCEL_STATE_NAME = "Cancel";

		public const string ANIM_ON_STATE_NAME = "On";

		public const string ANIM_OFF_STATE_NAME = "Off";

		[SerializeField]
		private Animator _animator;

		private BookmarkStatus _previousBookmarkStatus;

		public BookmarkStatus CurrentStatus
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		private void OnEnable()
		{
			throw null;
		}

		public void Initialize(bool isBookmarked)
		{
			throw null;
		}

		public void UpdateBookmarkState(bool isBookmarked)
		{
			throw null;
		}

		public void ApplyBookmarkState(bool isBookmarked)
		{
			throw null;
		}

		public void ApplyStatus(BookmarkStatus status)
		{
			throw null;
		}

		public void PlayActivatingAnimation()
		{
			throw null;
		}

		public void PlayCancelAnimation()
		{
			throw null;
		}

		private void PlayAnimation(string stateName)
		{
			throw null;
		}

		public BookmarkTag()
		{
			throw null;
		}
	}
}
