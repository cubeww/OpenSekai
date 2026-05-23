using UnityEngine;
using Sekai.Rendering;

[ExecuteAlways]
public class UIBlurController : MonoBehaviour
{
	[SerializeField]
	[Range(0.1f, 2f)]
	private float m_BlurSamplingDistance = 1.5f;

	[SerializeField]
	[Range(0.2f, 1f)]
	private float m_CaptureResolutionScale = 0.5f;

	public float BlurSamplingDistance
	{
		get => m_BlurSamplingDistance;
		set => m_BlurSamplingDistance = Mathf.Clamp(value, 0.1f, 2f);
	}

	public float CaptureResolutionScale
	{
		get => m_CaptureResolutionScale;
		set => m_CaptureResolutionScale = Mathf.Clamp(value, 0.2f, 1f);
	}

	private void Update()
	{
		Apply();
	}

	private void OnEnable()
	{
		Apply();
	}

	private void Apply()
	{
		SekaiUIEffectSettings.Blur.BlurSamplingDistance = m_BlurSamplingDistance;
		SekaiUIEffectSettings.Blur.CaptureResolutionScale = m_CaptureResolutionScale;
	}
}
