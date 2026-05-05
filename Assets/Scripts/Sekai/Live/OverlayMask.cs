using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.Live
{
    [ExecuteInEditMode]
    public class OverlayMask : UIBehaviour
    {
        private const string maskedShaderName = "Sekai/OverlayMask";
        private static readonly int SoftMaskTexId = Shader.PropertyToID("_SoftMaskTex");
        private static readonly int SoftMaskRectId = Shader.PropertyToID("_SoftMaskRect");
        private static readonly int SoftMaskTransId = Shader.PropertyToID("_SoftMaskTrans");

        public Texture2D alphaTexture;
        public GameObject[] managedMaskingTarget;

        private RectTransform m_RectTransform;
        private Material m_MaskedMaterial;

        private RectTransform rectTransform
        {
            get
            {
                if (m_RectTransform == null)
                {
                    m_RectTransform = GetComponent<RectTransform>();
                }

                return m_RectTransform;
            }
        }

        private Vector4 softMaskRect
        {
            get
            {
                RectTransform rect = rectTransform;
                if (rect == null)
                {
                    return Vector4.zero;
                }

                Rect localRect = rect.rect;
                Vector2 pivot = rect.pivot;
                return new Vector4(localRect.width, localRect.height, pivot.x, pivot.y);
            }
        }

        private Material maskedMaterial
        {
            get
            {
                if (m_MaskedMaterial == null)
                {
                    Shader shader = Shader.Find(maskedShaderName);
                    if (shader == null)
                    {
                        Debug.LogWarning("OverlayMask shader not found: " + maskedShaderName, this);
                        return null;
                    }

                    m_MaskedMaterial = new Material(shader)
                    {
                        name = "OverlayMask Material",
                        hideFlags = HideFlags.DontSave
                    };
                    UpdateAlphaTexture();
                    UpdateTransformInfo();
                }

                return m_MaskedMaterial;
            }
        }

        private void UpdateAlphaTexture()
        {
            Material material = maskedMaterial;
            if (material != null)
            {
                material.SetTexture(SoftMaskTexId, alphaTexture);
            }
        }

        private void UpdateTransformInfo()
        {
            Material material = maskedMaterial;
            if (material == null)
            {
                return;
            }

            material.SetVector(SoftMaskRectId, softMaskRect);
            material.SetMatrix(SoftMaskTransId, transform.worldToLocalMatrix);
        }

        public bool MaskEnabled()
        {
            return IsActive() && alphaTexture != null;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (MaskEnabled())
            {
                UpdateAlphaTexture();
                UpdateTransformInfo();
                MaskAllChildren();
                MaskAllManaged();
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ResetAllChildren();
            ResetAllManaged();
        }

        public void MaskOneGameObject(GameObject obj)
        {
            if (obj == null)
            {
                return;
            }

            MaskableGraphic[] graphics = obj.GetComponentsInChildren<MaskableGraphic>();
            for (int i = 0; i < graphics.Length; i++)
            {
                if (graphics[i] != null)
                {
                    graphics[i].material = maskedMaterial;
                }
            }
        }

        public void ResetOneGameObject(GameObject obj)
        {
            if (obj == null)
            {
                return;
            }

            MaskableGraphic[] graphics = obj.GetComponentsInChildren<MaskableGraphic>();
            for (int i = 0; i < graphics.Length; i++)
            {
                if (graphics[i] != null)
                {
                    graphics[i].material = null;
                }
            }
        }

        public void MaskAllChildren()
        {
            Transform selfTransform = transform;
            for (int i = 0; i < selfTransform.childCount; i++)
            {
                Transform child = selfTransform.GetChild(i);
                if (child != null)
                {
                    MaskOneGameObject(child.gameObject);
                }
            }
        }

        public void ResetAllChildren()
        {
            Transform selfTransform = transform;
            for (int i = 0; i < selfTransform.childCount; i++)
            {
                Transform child = selfTransform.GetChild(i);
                if (child != null)
                {
                    ResetOneGameObject(child.gameObject);
                }
            }
        }

        public void MaskAllManaged()
        {
            if (managedMaskingTarget == null)
            {
                return;
            }

            for (int i = 0; i < managedMaskingTarget.Length; i++)
            {
                MaskOneGameObject(managedMaskingTarget[i]);
            }
        }

        public void ResetAllManaged()
        {
            if (managedMaskingTarget == null)
            {
                return;
            }

            for (int i = 0; i < managedMaskingTarget.Length; i++)
            {
                ResetOneGameObject(managedMaskingTarget[i]);
            }
        }

        private void Update()
        {
            if (transform.hasChanged)
            {
                UpdateTransformInfo();
                transform.hasChanged = false;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (m_MaskedMaterial == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                Destroy(m_MaskedMaterial);
            }
            else
            {
                DestroyImmediate(m_MaskedMaterial);
            }

            m_MaskedMaterial = null;
        }
    }
}
