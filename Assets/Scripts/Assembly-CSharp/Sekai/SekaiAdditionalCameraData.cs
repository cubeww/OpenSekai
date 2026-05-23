using UnityEngine;

namespace UnityEngine.Rendering.Universal
{
    public class SekaiAdditionalCameraData : MonoBehaviour
    {
        public bool IsFinalCamera { get; private set; }

        public void Setup(int cameraIndex, int cameraCount)
        {
            // Original Project Sekai URP calls this once per camera before rendering.
            IsFinalCamera = cameraCount - 1 == cameraIndex;
        }
    }

    public static class SekaiCameraExtensions
    {
        public static SekaiAdditionalCameraData GetSekaiAdditionalCameraData(this Camera camera)
        {
            if (camera == null)
            {
                return null;
            }

            return camera.TryGetComponent<SekaiAdditionalCameraData>(out var data)
                ? data
                : camera.gameObject.AddComponent<SekaiAdditionalCameraData>();
        }
    }
}
