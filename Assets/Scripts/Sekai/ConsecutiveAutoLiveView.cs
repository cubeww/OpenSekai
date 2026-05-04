using System;
using UnityEngine;

namespace Sekai
{
    public class ConsecutiveAutoLiveView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer pauseButton;
        [SerializeField] private SpriteRenderer pauseButtonDisable;
        [SerializeField] private LiveSpriteButton pauseButtonController;
        [SerializeField] private SpriteRenderer autoLivePauseButton;
        [SerializeField] private SpriteRenderer autoLivePauseButtonDisable;
        [SerializeField] private LiveSpriteButton autoLivePauseButtonController;
        [SerializeField] private SpriteRenderer autoLabel;

        public void Setup(Action onClickButton, Action onClickDialogResume, Action onClickDialogCancel, Camera baseCamera) { }
        public void Pause() { }
        public void CalculateButtonBounds(Camera camera) { }
        public void Show() { gameObject.SetActive(true); }
        public void Hide() { gameObject.SetActive(false); }
    }
}
