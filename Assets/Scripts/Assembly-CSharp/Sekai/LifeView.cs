using System;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class LifeView : MonoBehaviour
	{
		[SerializeField]
		private Transform lifeDefault;

		[SerializeField]
		private Transform lifeDamage;

		[SerializeField]
		private SpriteRenderer lifeDefaultGauge;

		[SerializeField]
		private SpriteRenderer lifeDamageGauge;

		[SerializeField]
		private NumberView number;

		[SerializeField]
		private NumberView outlineNumber;

		[SerializeField]
		private SpriteRenderer pauseButton;

		[SerializeField]
		private SpriteRenderer pauseButtonDisable;

		[SerializeField]
		private SpriteRenderer skipButton;

		[SerializeField]
		private LiveSpriteButton pauseButtonController;

		[SerializeField]
		private LiveSpriteButton skipButtonController;

		private int maxLife;

		private Vector2 lifeDefaultGaugeSize;

		private Vector2 lifeDamageGaugeSize;

		private LiveBundleBuildData liveBundleBuildData;

		private void Awake()
		{
			liveBundleBuildData = Resources.Load<LiveBundleBuildData>(LiveConfig.ConfigBundleNamePath);
		}

		public void Setup(Action callback = null)
		{
			if (liveBundleBuildData == null)
			{
				liveBundleBuildData = Resources.Load<LiveBundleBuildData>(LiveConfig.ConfigBundleNamePath);
			}
			maxLife = liveBundleBuildData != null && liveBundleBuildData.Life > 0 ? liveBundleBuildData.Life : 1000;
			lifeDefaultGaugeSize = lifeDefaultGauge != null ? lifeDefaultGauge.size : Vector2.one;
			lifeDamageGaugeSize = lifeDamageGauge != null ? lifeDamageGauge.size : Vector2.one;
			if (number != null)
			{
				number.Setup("#ffffff", "#ffffff00");
			}
			if (outlineNumber != null)
			{
				outlineNumber.Setup("#ffffff", "#ffffff00");
			}
			SetupButton(callback);
			Clear();
		}

		private void SetupButton(Action callback = null)
		{
			bool enabled = callback != null;
			SetActive(pauseButton, enabled);
			SetActive(pauseButtonDisable, !enabled);
			SetActive(skipButton, false);
			if (enabled && pauseButtonController != null)
			{
				pauseButtonController.Setup(callback);
			}
			if (enabled && skipButtonController != null)
			{
				skipButtonController.Setup(callback);
			}
		}

		public void CalculateButtonBounds(Camera camera)
		{
			if (pauseButtonController != null)
			{
				pauseButtonController.CalculateBounds(camera);
			}
			if (skipButtonController != null)
			{
				skipButtonController.CalculateBounds(camera);
			}
		}

		public void Clear()
		{
			if (lifeDefault != null)
			{
				lifeDefault.localScale = Vector3.one;
			}
			if (lifeDamage != null)
			{
				lifeDamage.localScale = Vector3.zero;
			}
			if (lifeDefaultGauge != null)
			{
				lifeDefaultGauge.size = lifeDefaultGaugeSize;
			}
			if (lifeDamageGauge != null)
			{
				lifeDamageGauge.size = lifeDamageGaugeSize;
			}
			if (number != null)
			{
				number.UpdateNumber(maxLife);
			}
			if (outlineNumber != null)
			{
				outlineNumber.UpdateNumber(maxLife);
			}
		}

		public void Excute(int life)
		{
			float rate = maxLife <= 0 ? 0f : Mathf.Clamp01((float)life / maxLife);
			bool isDefault = rate > 0.2f;
			if (lifeDefault != null)
			{
				lifeDefault.localScale = isDefault ? Vector3.one : Vector3.zero;
			}
			if (lifeDamage != null)
			{
				lifeDamage.localScale = isDefault ? Vector3.zero : Vector3.one;
			}
			if (lifeDefaultGauge != null)
			{
				lifeDefaultGauge.size = new Vector2(lifeDefaultGaugeSize.x * rate, lifeDefaultGaugeSize.y);
			}
			if (lifeDamageGauge != null)
			{
				lifeDamageGauge.size = new Vector2(lifeDamageGaugeSize.x * rate, lifeDamageGaugeSize.y);
			}
			if (number != null)
			{
				number.UpdateNumber(life);
			}
			if (outlineNumber != null)
			{
				outlineNumber.UpdateNumber(life);
			}
		}

		public LifeView()
		{
		}

		private static void SetActive(SpriteRenderer renderer, bool active)
		{
			if (renderer != null)
			{
				renderer.gameObject.SetActive(active);
			}
		}
	}
}
