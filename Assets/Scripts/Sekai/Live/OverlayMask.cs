using UnityEngine;

namespace Sekai.Live
{
    [ExecuteInEditMode]
    public class OverlayMask : MonoBehaviour
    {
        public Texture2D alphaTexture;
        public GameObject[] managedMaskingTarget;

        public bool MaskEnabled()
        {
            return enabled && gameObject.activeInHierarchy;
        }

        public void MaskOneGameObject(GameObject obj) { }
        public void ResetOneGameObject(GameObject obj) { }
        public void MaskAllChildren() { }
        public void ResetAllChildren() { }
        public void MaskAllManaged() { }
        public void ResetAllManaged() { }
    }
}
