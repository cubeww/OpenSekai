using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
    public class CommonButtonTapEffect : ButtonViewInteractionBase
    {
        [SerializeField] private Graphic _buttonImage;
        [SerializeField] private CustomImage _glowButtonImage;
        [SerializeField] private CustomTextMesh _buttonTextMesh;
        [SerializeField] private List<CustomTextMesh> _additionalTextList;

        private Color defaultButtonColor = Color.white;
        private Color defaultTextColor = Color.white;
        private Color effectButtonColor = new Color(1f, 1f, 1f, 0.7f);
        private Color effectTextColor = Color.white;

        private void Awake()
        {
            if (_buttonImage != null)
            {
                defaultButtonColor = _buttonImage.color;
            }

            if (_buttonTextMesh != null)
            {
                defaultTextColor = _buttonTextMesh.color;
            }

            if (_glowButtonImage != null)
            {
                _glowButtonImage.Alpha = 0f;
            }
        }

        public void Setup(string buttonTypeName, Color buttonColor, Color textColor)
        {
            effectButtonColor = buttonColor;
            effectTextColor = textColor;
        }

        public override void OnPressed()
        {
            SetEffectState(true);
        }

        public override void OnReleased()
        {
            SetEffectState(false);
        }

        private void SetEffectState(bool pressed)
        {
            if (_buttonImage != null)
            {
                _buttonImage.color = pressed ? effectButtonColor : defaultButtonColor;
            }

            if (_glowButtonImage != null)
            {
                _glowButtonImage.Alpha = pressed ? 1f : 0f;
            }

            SetTextColor(_buttonTextMesh, pressed ? effectTextColor : defaultTextColor);

            if (_additionalTextList == null)
            {
                return;
            }

            for (int i = 0; i < _additionalTextList.Count; i++)
            {
                SetTextColor(_additionalTextList[i], pressed ? effectTextColor : defaultTextColor);
            }
        }

        private static void SetTextColor(CustomTextMesh textMesh, Color color)
        {
            if (textMesh != null)
            {
                textMesh.color = color;
            }
        }
    }
}
