using UnityEngine;

namespace Sekai
{
    [ExecuteInEditMode]
    public class NumberView : MonoBehaviour
    {
        public enum Align
        {
            Right = 0,
            Left = 1
        }

        [SerializeField] private Align align;
        [SerializeField] private float numberSpacing;
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color surplusColor = Color.clear;
        [SerializeField] private Sprite plusIconSprite;
        [SerializeField] private Sprite minusiconSprite;
        [SerializeField] private Sprite[] numberSprites;

        private struct SpriteStatus
        {
            public const int IconSpriteId = -1;

            public int id;
            public bool isSurplus;
            public bool isColorDirty;
        }

        private SpriteRenderer[] numberSpriteRenderers;
        private int maxDigitCount;
        private int[] digitValues;
        private SpriteStatus[] spriteStatus;
        private float alpha = 1f;
        private bool hasIcon;
        private float x;
        private Vector3 pos0;
        private Vector3 pos1;
        private Vector3 pos2;
        private Vector3 pos3;
        private Vector3 pos4;

        private void Awake()
        {
            CacheBasePositions();
        }

        public void Setup(string defaultColorHtml, string surplusColorHtml = "#ffffff00")
        {
            Setup(ColorUtility.Create(defaultColorHtml), ColorUtility.Create(surplusColorHtml));
        }

        public void Setup(Color defaultColor, Color surplusColor)
        {
            this.defaultColor = defaultColor;
            this.surplusColor = surplusColor;
            alpha = 1f;
            hasIcon = plusIconSprite != null || minusiconSprite != null;
            CacheBasePositions();
            CacheRenderers();

            for (int i = 0; i < numberSpriteRenderers.Length; i++)
            {
                int positionIndex = align == Align.Left ? i : -i;
                numberSpriteRenderers[i].transform.localPosition = Vector3.right * numberSpacing * positionIndex;
                numberSpriteRenderers[i].sprite = GetNumberSprite(0);
                spriteStatus[i].id = 0;
                spriteStatus[i].isSurplus = true;
                spriteStatus[i].isColorDirty = true;
            }

            maxDigitCount = numberSpriteRenderers.Length - (hasIcon ? 1 : 0);
            UpdateColor();
        }

        public void SetupNew(Color defaultColor)
        {
            CacheRenderers();
            this.defaultColor = defaultColor;
            for (int i = 0; i < spriteStatus.Length; i++)
            {
                spriteStatus[i].isColorDirty = true;
            }
            UpdateColor();
        }

        public void UpdateNumber(int value)
        {
            CacheRenderers();
            if (numberSpriteRenderers.Length == 0)
            {
                return;
            }

            int digitCount = CalculateDigit(value);
            if (hasIcon)
            {
                int iconIndex = align == Align.Left ? 0 : digitCount;
                if (iconIndex >= 0 && iconIndex < numberSpriteRenderers.Length)
                {
                    Sprite icon = null;
                    if (value > 0)
                    {
                        icon = plusIconSprite;
                    }
                    else if (value < 0)
                    {
                        icon = minusiconSprite;
                    }

                    numberSpriteRenderers[iconIndex].sprite = icon;
                    spriteStatus[iconIndex].id = value != 0 ? SpriteStatus.IconSpriteId : 0;
                    if (spriteStatus[iconIndex].isSurplus)
                    {
                        spriteStatus[iconIndex].isSurplus = false;
                        spriteStatus[iconIndex].isColorDirty = true;
                    }
                }
            }

            int digitStart = align == Align.Left && hasIcon && value != 0 ? 1 : 0;
            for (int i = 0; i < digitCount; i++)
            {
                int valueIndex = align == Align.Left ? digitCount - i - 1 : i;
                int rendererIndex = digitStart + i;
                if (rendererIndex < 0 || rendererIndex >= numberSpriteRenderers.Length)
                {
                    continue;
                }

                int digit = digitValues[valueIndex];
                if (spriteStatus[rendererIndex].id != digit)
                {
                    numberSpriteRenderers[rendererIndex].sprite = GetNumberSprite(digit);
                    spriteStatus[rendererIndex].id = digit;
                }
                if (spriteStatus[rendererIndex].isSurplus)
                {
                    spriteStatus[rendererIndex].isSurplus = false;
                    spriteStatus[rendererIndex].isColorDirty = true;
                }
            }

            int usedCount = digitCount + (hasIcon && value != 0 ? 1 : 0);
            for (int i = usedCount; i < numberSpriteRenderers.Length; i++)
            {
                if (spriteStatus[i].id != 0)
                {
                    numberSpriteRenderers[i].sprite = GetNumberSprite(0);
                    spriteStatus[i].id = 0;
                }
                if (!spriteStatus[i].isSurplus)
                {
                    spriteStatus[i].isSurplus = true;
                    spriteStatus[i].isColorDirty = true;
                }
            }

            UpdateColor();
        }

        public void UpdateNumberNew(int value)
        {
            CacheRenderers();
            int digitCount = CalculateDigit(value);
            hasIcon = plusIconSprite != null || minusiconSprite != null;
            if (value == 0 && numberSpriteRenderers.Length > 0)
            {
                hasIcon = false;
                spriteStatus[0].id = 0;
                spriteStatus[0].isSurplus = false;
                spriteStatus[0].isColorDirty = true;
                digitValues[0] = 0;
                numberSpriteRenderers[0].sprite = GetNumberSprite(0);
            }

            UpdateNumber(value);
            UpdatePosition(value == 0 ? digitCount : digitCount + 1);
        }

        public void UpdateAlpha(float alpha)
        {
            CacheRenderers();
            float value = Mathf.Clamp01(alpha);
            if (Mathf.Approximately(this.alpha, value))
            {
                return;
            }

            this.alpha = value;
            for (int i = 0; i < spriteStatus.Length; i++)
            {
                spriteStatus[i].isColorDirty = true;
            }
            UpdateColor();
        }

        private void CacheBasePositions()
        {
            x = transform.localPosition.x;
            Vector3 position = transform.localPosition;
            pos0 = position;
            pos1 = new Vector3(x - 0.13f, position.y, position.z);
            pos2 = new Vector3(x - 0.26f, position.y, position.z);
            pos3 = new Vector3(x - 0.39f, position.y, position.z);
            pos4 = new Vector3(x - 0.52f, position.y, position.z);
        }

        private void CacheRenderers()
        {
            if (numberSpriteRenderers != null && spriteStatus != null && digitValues != null)
            {
                return;
            }

            numberSpriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
            spriteStatus = new SpriteStatus[numberSpriteRenderers.Length];
            digitValues = new int[numberSpriteRenderers.Length];
            maxDigitCount = numberSpriteRenderers.Length - (hasIcon ? 1 : 0);
        }

        private void UpdatePosition(int count)
        {
            switch (count)
            {
                case 1:
                    transform.localPosition = pos0;
                    break;
                case 2:
                    transform.localPosition = pos1;
                    break;
                case 3:
                    transform.localPosition = pos2;
                    break;
                case 4:
                    transform.localPosition = pos3;
                    break;
                case 5:
                    transform.localPosition = pos4;
                    break;
                default:
                    Vector3 position = transform.localPosition;
                    position.x = x + (count - 1) * -0.13f;
                    transform.localPosition = position;
                    break;
            }
        }

        private void UpdateColor()
        {
            if (numberSpriteRenderers == null || spriteStatus == null)
            {
                return;
            }

            for (int i = 0; i < numberSpriteRenderers.Length; i++)
            {
                if (numberSpriteRenderers[i] == null || !spriteStatus[i].isColorDirty)
                {
                    continue;
                }

                Color color = spriteStatus[i].isSurplus ? surplusColor : defaultColor;
                color.a *= alpha;
                numberSpriteRenderers[i].color = color;
                spriteStatus[i].isColorDirty = false;
            }
        }

        private int CalculateDigit(int value)
        {
            if (digitValues == null || digitValues.Length == 0)
            {
                return 0;
            }
            if (value == 0)
            {
                digitValues[0] = 0;
                return 1;
            }

            int number = Mathf.Abs(value);
            int digit = 1;
            int threshold = 10;
            while (number >= threshold && threshold <= int.MaxValue / 10)
            {
                digit++;
                threshold *= 10;
            }

            if (digit > maxDigitCount)
            {
                do
                {
                    threshold /= 10;
                    digit--;
                    number %= Mathf.Max(1, threshold);
                }
                while (digit > maxDigitCount);
            }

            for (int i = digit - 1; i >= 0; i--)
            {
                threshold /= 10;
                int divisor = Mathf.Max(1, threshold);
                digitValues[i] = number / divisor;
                number -= digitValues[i] * divisor;
            }
            return digit;
        }

        private Sprite GetNumberSprite(int value)
        {
            if (numberSprites == null || numberSprites.Length == 0)
            {
                return null;
            }

            int index = Mathf.Clamp(value, 0, numberSprites.Length - 1);
            return numberSprites[index];
        }
    }
}
