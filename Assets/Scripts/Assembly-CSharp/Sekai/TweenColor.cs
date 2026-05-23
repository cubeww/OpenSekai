using UnityEngine;

namespace Sekai
{
	public class TweenColor : TweenBase
	{
		[SerializeField]
		private Color from;

		[SerializeField]
		private Color to;

		public Color From
		{
			get
			{
				return from;
			}
			set
			{
				from = value;
			}
		}

		public Color To
		{
			get
			{
				return to;
			}
			set
			{
				to = value;
			}
		}

		public override void SetupTarget()
		{
			if (componentType == PlayComponentType.Normal)
			{
				tweenTarget = new TweenTargetColorNormal();
			}
			else
			{
				tweenTarget = new TweenTargetColorUnityUI();
			}
			tweenTarget.Initialize(gameObject);
		}

		public override void PlayCore(PlayDirection playDirection)
		{
			direction = playDirection;
			if (tweenTarget == null)
			{
				SetupTarget();
			}
			if (tweenTarget == null || !tweenTarget.IsExist)
			{
				return;
			}
			Color start = playDirection == PlayDirection.Forward ? from : to;
			Color end = playDirection == PlayDirection.Forward ? to : from;
			tweenTarget.Color = start;
			tweener = DG.Tweening.DOTween.To(() => tweenTarget.Color, value => tweenTarget.Color = value, end, duration);
			SetTweenOptionCommon();
		}

		protected override void ToTween(float normalizeTime)
		{
			KillTween();
			if (tweenTarget == null || !tweenTarget.IsExist)
			{
				return;
			}
			float t = direction == PlayDirection.Forward ? normalizeTime : 1f - normalizeTime;
			if (curve != null)
			{
				t = curve.Evaluate(t);
			}
			tweenTarget.Color = Color.LerpUnclamped(from, to, Mathf.Clamp01(t));
		}

		protected override void OnReference()
		{
			if (reference?.TweenTarget != null && tweenTarget != null)
			{
				tweenTarget.Color = reference.TweenTarget.Color;
			}
		}

		public TweenColor()
		{
		}
	}
}
