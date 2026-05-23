using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.Live
{
	[ExecuteInEditMode]
	public class OverlayMask : UIBehaviour
	{
		private const string maskedShaderName = "Sekai/OverlayMask";

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
					if (shader != null)
					{
						m_MaskedMaterial = new Material(shader);
						UpdateAlphaTexture();
					}
				}
				return m_MaskedMaterial;
			}
		}

		protected OverlayMask()
		{
		}

		private void UpdateAlphaTexture()
		{
			if (maskedMaterial != null)
			{
				maskedMaterial.SetTexture("_SoftMaskTex", alphaTexture);
			}
		}

		private void UpdateTransformInfo()
		{
			if (maskedMaterial == null)
			{
				return;
			}
			maskedMaterial.SetVector("_SoftMaskRect", softMaskRect);
			maskedMaterial.SetMatrix("_SoftMaskTrans", transform.worldToLocalMatrix);
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
			if (obj == null || maskedMaterial == null)
			{
				return;
			}
			MaskableGraphic[] graphics = obj.GetComponentsInChildren<MaskableGraphic>(true);
			for (int i = 0; i < graphics.Length; i++)
			{
				graphics[i].material = maskedMaterial;
			}
		}

		public void ResetOneGameObject(GameObject obj)
		{
			if (obj == null)
			{
				return;
			}
			MaskableGraphic[] graphics = obj.GetComponentsInChildren<MaskableGraphic>(true);
			for (int i = 0; i < graphics.Length; i++)
			{
				graphics[i].material = null;
			}
		}

		public void MaskAllChildren()
		{
			foreach (Transform child in transform)
			{
				MaskOneGameObject(child.gameObject);
			}
		}

		public void ResetAllChildren()
		{
			foreach (Transform child in transform)
			{
				ResetOneGameObject(child.gameObject);
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
	}
}
