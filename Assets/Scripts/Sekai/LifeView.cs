using System;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
    public class LifeView : MonoBehaviour
    {
        [SerializeField] private Transform lifeDefault;
        [SerializeField] private Transform lifeDamage;
        [SerializeField] private SpriteRenderer lifeDefaultGauge;
        [SerializeField] private SpriteRenderer lifeDamageGauge;
        [SerializeField] private NumberView number;
        [SerializeField] private NumberView outlineNumber;
        [SerializeField] private SpriteRenderer pauseButton;
        [SerializeField] private SpriteRenderer pauseButtonDisable;
        [SerializeField] private SpriteRenderer skipButton;
        [SerializeField] private LiveSpriteButton pauseButtonController;
        [SerializeField] private LiveSpriteButton skipButtonController;

        private int maxLife;
        private Vector2 lifeDefaultGaugeSize;
        private Vector2 lifeDamageGaugeSize;

        public void Setup(Action onPause = null, int maxLife = -1)
        {
            this.maxLife = maxLife > 0 ? maxLife : LiveConfig.Life;
            if (lifeDefaultGauge != null)
            {
                lifeDefaultGaugeSize = lifeDefaultGauge.size;
            }
            if (lifeDamageGauge != null)
            {
                lifeDamageGaugeSize = lifeDamageGauge.size;
            }

            number?.Setup("#ffffff", "#ffffff00");
            outlineNumber?.Setup("#ffffff", "#ffffff00");
            SetupButton(onPause);
            Clear();
        }

        public void CalculateButtonBounds(Camera camera)
        {
            pauseButtonController?.CalculateBounds(camera);
            skipButtonController?.CalculateBounds(camera);
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

            number?.UpdateNumber(maxLife);
            outlineNumber?.UpdateNumber(maxLife);
        }

        public void Excute(int life)
        {
            float rate = maxLife > 0 ? Mathf.Clamp01((float)life / maxLife) : 0f;
            if (lifeDefault != null)
            {
                lifeDefault.localScale = rate <= 0.2f ? Vector3.zero : Vector3.one;
            }
            if (lifeDamage != null)
            {
                lifeDamage.localScale = rate <= 0.2f ? Vector3.one : Vector3.zero;
            }
            if (lifeDefaultGauge != null)
            {
                lifeDefaultGauge.size = new Vector2(rate * lifeDefaultGaugeSize.x, lifeDefaultGaugeSize.y);
            }
            if (lifeDamageGauge != null)
            {
                lifeDamageGauge.size = new Vector2(rate * lifeDamageGaugeSize.x, lifeDamageGaugeSize.y);
            }

            number?.UpdateNumber(life);
            outlineNumber?.UpdateNumber(life);
        }

        private void SetupButton(Action callback)
        {
            bool hasCallback = callback != null;
            if (pauseButton != null)
            {
                pauseButton.gameObject.SetActive(hasCallback);
            }
            if (pauseButtonDisable != null)
            {
                pauseButtonDisable.gameObject.SetActive(!hasCallback);
            }
            if (skipButton != null)
            {
                skipButton.gameObject.SetActive(false);
            }
            if (hasCallback)
            {
                pauseButtonController?.Setup(callback);
                skipButtonController?.Setup(callback);
            }
        }
    }
}
