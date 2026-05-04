using System;
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

        public void Setup(Action onPause) { }
        public void CalculateButtonBounds(Camera camera) { }
    }
}
