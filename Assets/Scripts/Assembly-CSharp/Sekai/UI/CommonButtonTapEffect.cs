using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
	public class CommonButtonTapEffect : ButtonViewInteractionBase
	{
		[SerializeField]
		private Graphic _buttonImage;

		[SerializeField]
		private CustomImage _glowButtonImage;

		[SerializeField]
		private CustomTextMesh _buttonTextMesh;

		[SerializeField]
		private List<CustomTextMesh> _additionalTextList;

		[SerializeField]
		private bool _autoSetEffectImage;

		private Color _defaultButtonColor;

		private const string DEFAULT_TEXT_COLOR_CODE = "#444466";

		private Color _effectButtonColor;

		private Color _defaultTextColor;

		private Color _effectTextColor;

		private Tweener _buttonImageTweener;

		private Tweener _glowButtonImageTweener;

		private Tweener _textTweener;

		private List<Tweener> _additionalTextTweener;

		public void Setup(string buttonTypeName, Color effectButtonColor, Color effectTextColor)
		{
			_effectButtonColor = effectButtonColor;
			_effectTextColor = effectTextColor;
			_defaultButtonColor = _buttonImage != null ? _buttonImage.color : Color.white;
			_defaultTextColor = _buttonTextMesh != null ? _buttonTextMesh.color : Color.white;
			if (_autoSetEffectImage)
			{
				SetGlowImage(buttonTypeName);
			}
		}

		private void SetGlowImage(string buttonTypeName)
		{
			if (_glowButtonImage != null)
			{
				_glowButtonImage.SpriteName = "eff_btn_round_h" + buttonTypeName + "_a70_wh";
			}
		}

		public override void OnPressed()
		{
			FadeIn(FadeInTime);
		}

		public override void OnReleased()
		{
			FadeOut(FadeOutTime);
		}

		public override void KillFadeTween()
		{
			_buttonImageTweener?.Kill(false);
			_glowButtonImageTweener?.Kill(false);
			_textTweener?.Kill(false);
			_buttonImageTweener = null;
			_glowButtonImageTweener = null;
			_textTweener = null;
			if (_additionalTextTweener == null)
			{
				return;
			}
			foreach (var tweener in _additionalTextTweener)
			{
				tweener?.Kill(false);
			}
			_additionalTextTweener.Clear();
		}

		private void FadeIn(float duration)
		{
			KillFadeTween();
			if (_buttonImage != null)
			{
				_buttonImageTweener = _buttonImage.DOColor(_effectButtonColor, duration);
			}
			if (_glowButtonImage != null)
			{
				_glowButtonImageTweener = _glowButtonImage.DOColor(_effectButtonColor, duration);
			}
			if (_buttonTextMesh != null)
			{
				_textTweener = _buttonTextMesh.DOColor(_effectTextColor, duration);
			}
			if (_additionalTextList != null)
			{
				_additionalTextTweener ??= new List<Tweener>();
				_additionalTextTweener.Clear();
				foreach (CustomTextMesh text in _additionalTextList)
				{
					if (text != null)
					{
						_additionalTextTweener.Add(text.DOColor(_effectTextColor, duration));
					}
				}
			}
		}

		private void FadeOut(float duration, Action onComplete = null)
		{
			KillFadeTween();
			if (_buttonImage != null)
			{
				_buttonImageTweener = _buttonImage.DOColor(_defaultButtonColor, duration).OnComplete(() => onComplete?.Invoke());
			}
			else
			{
				onComplete?.Invoke();
			}
			if (_glowButtonImage != null)
			{
				_glowButtonImageTweener = _glowButtonImage.DOColor(_defaultButtonColor, duration);
			}
			if (_buttonTextMesh != null)
			{
				_textTweener = _buttonTextMesh.DOColor(_defaultTextColor, duration);
			}
			if (_additionalTextList != null)
			{
				_additionalTextTweener ??= new List<Tweener>();
				foreach (CustomTextMesh text in _additionalTextList)
				{
					if (text != null)
					{
						_additionalTextTweener.Add(text.DOColor(_defaultTextColor, duration));
					}
				}
			}
		}

		public CommonButtonTapEffect()
		{
		}
	}
}
