using UnityEngine;

namespace Sekai
{
	public class TweenScale : TweenBase
	{
		[SerializeField]
		private Vector3 from;

		[SerializeField]
		private Vector3 to;

		public Vector3 From
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

		public Vector3 To
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

		public bool IsExist
		{
			get
			{
				return tweenTarget != null && tweenTarget.IsExist;
			}
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
			Vector3 start = playDirection == PlayDirection.Forward ? from : to;
			Vector3 end = playDirection == PlayDirection.Forward ? to : from;
			tweenTarget.LocalScale = start;
			tweener = DG.Tweening.DOTween.To(() => tweenTarget.LocalScale, value => tweenTarget.LocalScale = value, end, duration);
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
			tweenTarget.LocalScale = Vector3.LerpUnclamped(from, to, Mathf.Clamp01(t));
		}

		protected override void OnReference()
		{
			if (reference?.TweenTarget != null && tweenTarget != null)
			{
				tweenTarget.LocalScale = reference.TweenTarget.LocalScale;
			}
		}

		public TweenScale()
		{
		}
	}
}
