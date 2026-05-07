using System.Collections.Generic;
using UnityEngine;

namespace Sekai.Core.Live
{
    public class BaseLiveController : MonoBehaviour
    {
        [SerializeField] private RenderTexture backgroundTexture;

        [SerializeField] private Camera baseCamera;

        public readonly List<ParticleSystemController> ParticleSystemControllers = new List<ParticleSystemController>();
        private int previousVsyncCount;
        private bool hasPreviousVsyncCount;

        public RenderTexture BackgroundTexture
        {
            get { return backgroundTexture; }
            protected set { backgroundTexture = value; }
        }

        public LiveBootDataBase BootData { get; protected set; }

        public LiveSettingData Settings
        {
            get { return BootData != null ? BootData.LiveSettingData : null; }
        }

        public Camera BaseCamera
        {
            get { return baseCamera; }
            set { baseCamera = value; }
        }

        public void SetupBootDataForPreview(LiveBootDataBase bootData)
        {
            BootData = bootData;
        }

        protected void ApplyLiveFrameRateSettings()
        {
            LiveSettingData settings = Settings;
            if (settings == null)
            {
                return;
            }

            Application.targetFrameRate = settings.Use120FPS ? 120 : 60;
            if (!hasPreviousVsyncCount)
            {
                previousVsyncCount = QualitySettings.vSyncCount;
                hasPreviousVsyncCount = true;
            }

            QualitySettings.vSyncCount = settings.UseVSync.GetValueOrDefault() ? 1 : 0;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public void RegisterParticleSystemController(ParticleSystemController controller)
        {
            if (controller == null || ParticleSystemControllers.Contains(controller))
            {
                return;
            }

            ParticleSystemControllers.Add(controller);
        }

        public void UnregisterParticleSystemController(ParticleSystemController controller)
        {
            if (controller == null)
            {
                return;
            }

            ParticleSystemControllers.Remove(controller);
        }

        public virtual void Pause()
        {
        }

        public virtual void Resume()
        {
        }

        public virtual void Continue(float time)
        {
        }

        protected virtual void OnUpdate()
        {
            if (Time.frameCount % 5 != 0)
            {
                return;
            }

            for (int i = ParticleSystemControllers.Count - 1; i >= 0; i--)
            {
                ParticleSystemController controller = ParticleSystemControllers[i];
                if (controller == null)
                {
                    ParticleSystemControllers.RemoveAt(i);
                    continue;
                }

                controller.OnUpdate();
            }
        }

        protected virtual void OnDestroy()
        {
            if (hasPreviousVsyncCount)
            {
                QualitySettings.vSyncCount = previousVsyncCount;
                hasPreviousVsyncCount = false;
            }

            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
    }
}
