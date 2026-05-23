using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using uPalette.Generated;

namespace Sekai.UI
{
	public class GraphicButtonTapEffect : ButtonViewInteractionBase
	{
		[SerializeField]
		protected Graphic _effectGraphic;

		[SerializeField]
		protected Graphic _effectGraphicIcon;

		[Header("デフォルトカラー選択(uPalette)")]
		[SerializeField]
		protected ColorEntry _defaultColorPalette;

		[SerializeField]
		[Header("エフェクトカラー選択(uPalette)")]
		protected ColorEntry _effectColorPalette;

		[Header("エフェクト透明度を個別指定するか")]
		[SerializeField]
		protected bool _useCustomAlpha;

		[SerializeField]
		[Header("個別指定のデフォルト透明度")]
		protected float _customDefaultAlpha;

		[Header("個別指定のエフェクト透明度")]
		[SerializeField]
		protected float _customEffectAlpha;

		[SerializeField]
		private bool _refreshColorWhenAwake;

		protected Color _defaultColor;

		protected Color _effectColor;

		private Tweener _effectTweener;

		private Tweener _effectIconTweener;

		protected virtual void Awake()
		{
			if (_refreshColorWhenAwake)
			{
				RefreshColor();
			}
			ResetInteraction();
		}

		public void RefreshColor()
		{
			_defaultColor = global::Sekai.PaletteUtility.GetColor(_defaultColorPalette);
			_effectColor = global::Sekai.PaletteUtility.GetColor(_effectColorPalette);

			if (_useCustomAlpha)
			{
				_defaultColor.a = _customDefaultAlpha;
				_effectColor.a = _customEffectAlpha;
			}
		}

		public void SetEffectGraphic(Graphic effectGraphic)
		{
			_effectGraphic = effectGraphic;
			ResetInteraction();
		}

		public override void KillFadeTween()
		{
			_effectTweener?.Kill(false);
			_effectIconTweener?.Kill(false);
			_effectTweener = null;
			_effectIconTweener = null;
		}

		public override void ResetInteraction()
		{
			KillFadeTween();
			if (_effectGraphic != null)
			{
				_effectGraphic.color = _defaultColor;
			}
			if (_effectGraphicIcon != null)
			{
				_effectGraphicIcon.color = _defaultColor;
			}
		}

		public void SetDefaultColor(Color defaultColor)
		{
			_defaultColor = defaultColor;
			if (_effectGraphic != null)
			{
				_effectGraphic.color = defaultColor;
			}
			if (_effectGraphicIcon != null)
			{
				_effectGraphicIcon.color = defaultColor;
			}
		}

		public void SetEffectColor(Color effectColor)
		{
			_effectColor = effectColor;
		}

		public override void OnPressed()
		{
			FadeIn(FadeInTime);
		}

		public override void OnReleased()
		{
			FadeOut(FadeOutTime);
		}

		private void FadeIn(float duration)
		{
			KillFadeTween();
			if (_effectGraphic != null)
			{
				_effectTweener = _effectGraphic.DOColor(_effectColor, duration);
			}
			if (_effectGraphicIcon != null)
			{
				_effectIconTweener = _effectGraphicIcon.DOColor(_effectColor, duration);
			}
		}

		private void FadeOut(float duration, Action onComplete = null)
		{
			KillFadeTween();
			if (_effectGraphic != null)
			{
				_effectTweener = _effectGraphic.DOColor(_defaultColor, duration).OnComplete(() => onComplete?.Invoke());
			}
			else
			{
				onComplete?.Invoke();
			}
			if (_effectGraphicIcon != null)
			{
				_effectIconTweener = _effectGraphicIcon.DOColor(_defaultColor, duration);
			}
		}

		public GraphicButtonTapEffect()
		{
		}
	}
}
