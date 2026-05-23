using UnityEngine;

namespace Sekai
{
	public class TweenAlpha : TweenBase
	{
		[Range(0f, 1f)]
		[SerializeField]
		private float from;

		[Range(0f, 1f)]
		[SerializeField]
		private float to;

		private float current;

		public float From
		{
			get
			{
				return from;
			}
			set
			{
				from = Mathf.Clamp01(value);
			}
		}

		public float To
		{
			get
			{
				return to;
			}
			set
			{
				to = Mathf.Clamp01(value);
			}
		}

		public override void SetupTarget()
		{
			if (componentType == PlayComponentType.CanvasGroup)
			{
				tweenTarget = new TweenTargetCanvasAlpha();
			}
			else if (componentType == PlayComponentType.Normal)
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
			float start = playDirection == PlayDirection.Forward ? from : to;
			float end = playDirection == PlayDirection.Forward ? to : from;
			current = start;
			tweenTarget.Alpha = start;
			tweener = DG.Tweening.DOTween.To(() => current, value =>
			{
				current = value;
				tweenTarget.Alpha = value;
			}, end, duration);
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
			current = Mathf.LerpUnclamped(from, to, Mathf.Clamp01(t));
			tweenTarget.Alpha = current;
		}

		protected override void OnReference()
		{
			if (reference?.TweenTarget != null && tweenTarget != null)
			{
				current = reference.TweenTarget.Alpha;
				tweenTarget.Alpha = current;
			}
		}

		public TweenAlpha()
		{
		}
	}
}
