using UnityEngine;

namespace Sekai.Core.Live
{
    public class BaseLiveController : MonoBehaviour
    {
        [SerializeField] private RenderTexture backgroundTexture;

        [SerializeField] private Camera baseCamera;

        public RenderTexture BackgroundTexture
        {
            get { return backgroundTexture; }
            protected set { backgroundTexture = value; }
        }

        public LiveBootDataBase BootData { get; protected set; }

        public Camera BaseCamera
        {
            get { return baseCamera; }
            set { baseCamera = value; }
        }

        public void SetupBootDataForPreview(LiveBootDataBase bootData)
        {
            BootData = bootData;
        }
    }
}
