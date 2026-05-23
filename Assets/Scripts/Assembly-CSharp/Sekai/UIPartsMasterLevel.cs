using System.Runtime.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsMasterLevel : MonoBehaviour
	{
		public enum Size
		{
			S = 0,
			L = 1
		}

		[SerializeField]
		private CustomImage lvImage;

		[SerializeField]
		private TweenPosition effectTweenPosition;

		[SerializeField]
		private TweenScale effectTweenScale;

		[SerializeField]
		private TweenAlpha effectTweenAlpha;

		public bool IsShowZero
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public void SetReferenceTween(UIPartsMasterLevel master)
		{
			throw null;
		}

		private void SetReferenceTween(TweenPosition tween)
		{
			throw null;
		}

		private void SetReferenceTween(TweenScale tween)
		{
			throw null;
		}

		private void SetReferenceTween(TweenAlpha tween)
		{
			throw null;
		}

		public void Setup(Size size, int masterLv)
		{
			throw null;
		}

		public void SetupTween()
		{
			throw null;
		}

		public void OnEnable()
		{
			throw null;
		}

		public UIPartsMasterLevel()
		{
			throw null;
		}
	}
}
