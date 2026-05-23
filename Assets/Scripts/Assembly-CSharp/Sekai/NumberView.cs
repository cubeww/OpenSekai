using UnityEngine;
using UnityEngine.Serialization;

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

		private struct SpriteStatus
		{
			public static readonly int IconSpriteId;

			public int id;

			public bool isSurplus;

			public bool isColorDirty;

			static SpriteStatus()
			{
				IconSpriteId = -1;
			}
		}

		[SerializeField]
		private Align align;

		[SerializeField]
		private float numberSpacing;

		[FormerlySerializedAs("color")]
		[SerializeField]
		private Color defaultColor;

		[SerializeField]
		private Color surplusColor;

		[FormerlySerializedAs("plusiconSprite")]
		[FormerlySerializedAs("iconSprite")]
		[SerializeField]
		private Sprite plusIconSprite;

		[SerializeField]
		private Sprite minusiconSprite;

		[SerializeField]
		private Sprite[] numberSprites;

		private SpriteRenderer[] numberSpriteRenderers;

		private int maxDigitCount;

		private int[] digitValues;

		private SpriteStatus[] spriteStatus;

		private float alpha;

		private bool hasIcon;

		public void Setup(string color, string surplusColor = "#ffffff00")
		{
			Setup(ColorUtility.Create(color), ColorUtility.Create(surplusColor));
		}

		public void Setup(Color defaultColor, Color surplusColor)
		{
			this.defaultColor = defaultColor;
			this.surplusColor = surplusColor;
			alpha = 1f;
			hasIcon = plusIconSprite != null || minusiconSprite != null;

			numberSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
			spriteStatus = new SpriteStatus[numberSpriteRenderers.Length];
			maxDigitCount = Mathf.Max(0, numberSpriteRenderers.Length - (hasIcon ? 1 : 0));
			digitValues = new int[Mathf.Max(1, numberSpriteRenderers.Length)];

			for (int i = 0; i < numberSpriteRenderers.Length; i++)
			{
				int alignIndex = align == Align.Left ? i : -i;
				Transform rendererTransform = numberSpriteRenderers[i].transform;
				rendererTransform.localPosition = Vector3.right * (numberSpacing * alignIndex);
				if (numberSprites != null && numberSprites.Length > 0)
				{
					numberSpriteRenderers[i].sprite = numberSprites[0];
				}
				spriteStatus[i].id = 0;
				spriteStatus[i].isSurplus = true;
				spriteStatus[i].isColorDirty = true;
			}
			UpdateColor();
		}

		public void UpdateNumber(int number)
		{
			if (numberSpriteRenderers == null || numberSpriteRenderers.Length == 0)
			{
				Setup(defaultColor, surplusColor);
			}
			if (numberSpriteRenderers == null || numberSpriteRenderers.Length == 0 || numberSprites == null || numberSprites.Length == 0)
			{
				return;
			}

			int digitCount = CalculateDigit(number);
			int iconIndex = align == Align.Left ? 0 : digitCount;
			if (hasIcon && iconIndex >= 0 && iconIndex < numberSpriteRenderers.Length)
			{
				SpriteRenderer renderer = numberSpriteRenderers[iconIndex];
				if (number > 0)
				{
					renderer.sprite = plusIconSprite;
					spriteStatus[iconIndex].id = SpriteStatus.IconSpriteId;
				}
				else if (number < 0)
				{
					renderer.sprite = minusiconSprite;
					spriteStatus[iconIndex].id = SpriteStatus.IconSpriteId;
				}
				else
				{
					renderer.sprite = null;
					spriteStatus[iconIndex].id = 0;
				}
				if (spriteStatus[iconIndex].isSurplus)
				{
					spriteStatus[iconIndex].isSurplus = false;
					spriteStatus[iconIndex].isColorDirty = true;
				}
			}

			int offset = align == Align.Left && hasIcon && number != 0 ? 1 : 0;
			for (int displayed = 0; displayed < digitCount; displayed++)
			{
				int digitIndex = align == Align.Left ? digitCount - displayed - 1 : displayed;
				int rendererIndex = offset + displayed;
				if (rendererIndex < 0 || rendererIndex >= numberSpriteRenderers.Length || digitIndex < 0 || digitIndex >= digitValues.Length)
				{
					continue;
				}

				int digit = Mathf.Clamp(digitValues[digitIndex], 0, numberSprites.Length - 1);
				if (spriteStatus[rendererIndex].id != digit)
				{
					numberSpriteRenderers[rendererIndex].sprite = numberSprites[digit];
					spriteStatus[rendererIndex].id = digit;
				}
				if (spriteStatus[rendererIndex].isSurplus)
				{
					spriteStatus[rendererIndex].isSurplus = false;
					spriteStatus[rendererIndex].isColorDirty = true;
				}
			}

			int usedCount = digitCount + (hasIcon && number != 0 ? 1 : 0);
			for (int i = usedCount; i < numberSpriteRenderers.Length; i++)
			{
				if (numberSprites.Length > 0 && spriteStatus[i].id != 0)
				{
					numberSpriteRenderers[i].sprite = numberSprites[0];
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

		public void UpdateAlpha(float alpha)
		{
			float clamped = Mathf.Clamp01(alpha);
			if (Mathf.Approximately(this.alpha, clamped))
			{
				return;
			}
			this.alpha = clamped;
			if (spriteStatus != null)
			{
				for (int i = 0; i < spriteStatus.Length; i++)
				{
					spriteStatus[i].isColorDirty = true;
				}
			}
			UpdateColor();
		}

		private void UpdateColor()
		{
			if (numberSpriteRenderers == null || spriteStatus == null)
			{
				return;
			}
			for (int i = 0; i < numberSpriteRenderers.Length && i < spriteStatus.Length; i++)
			{
				if (!spriteStatus[i].isColorDirty || numberSpriteRenderers[i] == null)
				{
					continue;
				}
				Color color = spriteStatus[i].isSurplus ? surplusColor : defaultColor;
				color.a *= alpha;
				numberSpriteRenderers[i].color = color;
				spriteStatus[i].isColorDirty = false;
			}
		}

		private int CalculateDigit(int number)
		{
			if (digitValues == null || digitValues.Length == 0)
			{
				digitValues = new int[Mathf.Max(1, maxDigitCount)];
			}
			if (number == 0)
			{
				digitValues[0] = 0;
				return 1;
			}

			int absNumber = Mathf.Abs(number);
			int digitCount = 0;
			int divisor = 1;
			do
			{
				divisor *= 10;
				digitCount++;
			}
			while (absNumber >= divisor && divisor > 0);

			if (digitCount > maxDigitCount)
			{
				CP.LogUtility.LogError("Not enough digits. Need to Add some SpriteRenderer.");
				int trimCount = digitCount - maxDigitCount;
				for (int i = 0; i < trimCount; i++)
				{
					absNumber %= Mathf.Max(1, divisor / 10);
					divisor /= 10;
				}
				digitCount = Mathf.Max(1, maxDigitCount);
			}

			int workingDivisor = 1;
			for (int i = 1; i < digitCount; i++)
			{
				workingDivisor *= 10;
			}
			for (int i = digitCount - 1; i >= 0; i--)
			{
				digitValues[i] = absNumber / workingDivisor;
				absNumber %= workingDivisor;
				workingDivisor = Mathf.Max(1, workingDivisor / 10);
			}
			return digitCount;
		}

		public NumberView()
		{
			align = Align.Right;
			numberSpacing = 0.95f;
			defaultColor = Color.white;
			surplusColor = new Color(0f, 0f, 0f, 0f);
		}
	}
}
