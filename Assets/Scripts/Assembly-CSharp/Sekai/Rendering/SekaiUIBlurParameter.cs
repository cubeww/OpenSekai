namespace Sekai.Rendering
{
	public class SekaiUIBlurParameter
	{
		public bool IsActive { get; set; }
		public float CaptureResolutionScale { get; set; }
		public float BlurSamplingDistance { get; set; }

		public SekaiUIBlurParameter()
		{
			CaptureResolutionScale = 0.5f;
			BlurSamplingDistance = 1.5f;
		}

		public SekaiUIBlurParameter(SekaiUIEffectParameter parameter)
		{
			IsActive = (parameter.EffectFlags & SekaiUIEffectFlags.Blur) != 0;
			CaptureResolutionScale = parameter.CaptureResolutionScale;
			BlurSamplingDistance = parameter.BlurSamplingDistance;
		}
	}
}
