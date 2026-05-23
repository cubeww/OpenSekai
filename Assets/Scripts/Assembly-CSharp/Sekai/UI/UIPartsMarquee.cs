using System;
using System.Runtime.CompilerServices;
using Coffee.UISoftMask;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
	public class UIPartsMarquee : MonoBehaviour
	{
		[Header("回転を考慮しなくてもよい場合はRectMask2D")]
		[SerializeField]
		private RectMask2D mask2D;

		[Header("回転を考慮する場合はSoftMask")]
		[SerializeField]
		private SoftMask softMask;

		[SerializeField]
		private RectTransform marqueeTarget;

		[SerializeField]
		private float spd;

		[SerializeField]
		private float wait_sec;

		private float wait_sec_cnt;

		private bool isWait;

		private bool isWaitSec;

		[SerializeField]
		private bool enableMarquee;

		[SerializeField]
		private bool unconditionalMarquee;

		[SerializeField]
		private bool waitBasePosition;

		[SerializeField]
		private bool defaultFadeIn;

		[SerializeField]
		private float waitBeforeSpd;

		private bool isPlay;

		private bool isMove;

		private bool isMarqueeGroup;

		private float initPositionX;

		private float restartPositionX;

		private float endPositionX;

		private int loopNum;

		public Action OnCompleted
		{
			[CompilerGenerated]
			get
			{
				return _OnCompleted;
			}
			[CompilerGenerated]
			set
			{
				_OnCompleted = value;
			}
		}

		public Func<RectTransform, float> GetMarqueeTargetWidthHandler
		{
			[CompilerGenerated]
			get
			{
				return _GetMarqueeTargetWidthHandler;
			}
			[CompilerGenerated]
			set
			{
				_GetMarqueeTargetWidthHandler = value;
			}
		}

		[CompilerGenerated]
		private Action _OnCompleted;

		[CompilerGenerated]
		private Func<RectTransform, float> _GetMarqueeTargetWidthHandler;

		public bool EnableMarquee
		{
			get
			{
				return enableMarquee;
			}
			set
			{
				if (value && !enableMarquee)
				{
					Init();
				}

				if (enableMarquee != value && marqueeTarget != null)
				{
					var position = marqueeTarget.anchoredPosition;
					position.x = GetInitPositionX();
					marqueeTarget.anchoredPosition = position;
				}

				enableMarquee = value;
				SetEnabledMask(value);
			}
		}

		public bool IsMove
		{
			get
			{
				return isMove;
			}
		}

		public bool IsMarqueeGroup
		{
			get
			{
				return isMarqueeGroup;
			}
			set
			{
				isMarqueeGroup = value;
			}
		}

		public bool IsPlay
		{
			get
			{
				return isPlay;
			}
			set
			{
				isPlay = value;
			}
		}

		public void Enable()
		{
			enabled = true;
		}

		public void Disable()
		{
			enabled = false;
		}

		public void SetEnabledMask(bool enable)
		{
			if (mask2D != null)
			{
				mask2D.enabled = enable;
			}

			if (softMask != null)
			{
				softMask.enabled = enable;
			}
		}

		private void Awake()
		{
			if (GetMarqueeTargetWidthHandler == null)
			{
				GetMarqueeTargetWidthHandler = GetMarqueeTargetWidthDefault;
			}
			if (enableMarquee)
			{
				Init();
			}
		}

		private void OnEnable()
		{
			if (enableMarquee)
			{
				Init();
			}
		}

		public void SetUnconditionalMarquee(bool flag)
		{
			unconditionalMarquee = flag;
		}

		public void Init()
		{
			if (marqueeTarget == null)
			{
				marqueeTarget = transform as RectTransform;
			}

			if (marqueeTarget == null)
			{
				return;
			}

			if (NeedsMarqueeMoveWidth() || unconditionalMarquee)
			{
				isMove = true;
				initPositionX = GetInitPositionX();
				restartPositionX = GetRestartPositionX();
				endPositionX = GetEndPositionX();
				if (waitBasePosition)
				{
					wait_sec_cnt = 0f;
					isWait = true;
				}

				if (defaultFadeIn)
				{
					isWait = false;
					Restart();
					return;
				}
			}
			else
			{
				isMove = false;
				initPositionX = GetInitPositionX();
			}

			var position = marqueeTarget.anchoredPosition;
			position.x = initPositionX;
			marqueeTarget.anchoredPosition = position;
		}

		public void Restart()
		{
			if (!(NeedsMarqueeMoveWidth() || unconditionalMarquee) || marqueeTarget == null)
			{
				return;
			}

			isMove = true;
			var position = marqueeTarget.anchoredPosition;
			position.x = restartPositionX;
			marqueeTarget.anchoredPosition = position;
			if (waitBasePosition)
			{
				wait_sec_cnt = 0f;
			}
		}

		public bool NeedsMarqueeMoveWidth()
		{
			if (marqueeTarget == null)
			{
				return false;
			}

			var handler = GetMarqueeTargetWidthHandler ?? GetMarqueeTargetWidthDefault;
			return GetMaskRect().width < handler(marqueeTarget);
		}

		public void SetActive(bool isActive)
		{
			gameObject.SetActive(isActive);
		}

		private void End()
		{
			if (isMarqueeGroup)
			{
				isMove = false;
			}
			else
			{
				Restart();
			}

			OnCompleted?.Invoke();
		}

		private void Update()
		{
			if (enableMarquee)
			{
				UpdateMarquee();
			}
		}

		private void UpdateMarquee()
		{
			if (!(NeedsMarqueeMoveWidth() || unconditionalMarquee) || !isMove || !isPlay || marqueeTarget == null)
			{
				return;
			}

			if (isWait)
			{
				wait_sec_cnt += Time.deltaTime;
				if (wait_sec_cnt < wait_sec)
				{
					return;
				}

				isWait = false;
			}

			var position = marqueeTarget.anchoredPosition;
			var moveSpeed = waitBasePosition && position.x > 0f ? waitBeforeSpd : spd;
			position.x -= moveSpeed * Time.deltaTime;
			if (position.x >= endPositionX)
			{
				if (wait_sec_cnt < wait_sec && marqueeTarget.anchoredPosition.x <= 0f)
				{
					position.x = 0f;
					isWait = true;
				}

				marqueeTarget.anchoredPosition = position;
				return;
			}

			position.x = endPositionX;
			marqueeTarget.anchoredPosition = position;
			End();
		}

		private Rect GetMaskRect()
		{
			if (mask2D != null)
			{
				return mask2D.rectTransform.rect;
			}

			if (softMask != null && softMask.transform is RectTransform rectTransform)
			{
				return rectTransform.rect;
			}

			return marqueeTarget != null ? marqueeTarget.rect : default;
		}

		private float GetInitPositionX()
		{
			if (marqueeTarget == null)
			{
				return 0f;
			}

			var handler = GetMarqueeTargetWidthHandler ?? GetMarqueeTargetWidthDefault;
			return marqueeTarget.pivot.x * handler(marqueeTarget);
		}

		private float GetRestartPositionX()
		{
			return GetInitPositionX() + GetMaskRect().width;
		}

		private float GetEndPositionX()
		{
			var handler = GetMarqueeTargetWidthHandler ?? GetMarqueeTargetWidthDefault;
			return marqueeTarget != null ? GetInitPositionX() - handler(marqueeTarget) : 0f;
		}

		private float GetMarqueeTargetWidthDefault(RectTransform target)
		{
			return target != null ? target.rect.width : 0f;
		}

		public UIPartsMarquee()
		{
			spd = 200f;
			wait_sec = 0.5f;
			waitBeforeSpd = 400f;
			isPlay = true;
		}
	}
}
