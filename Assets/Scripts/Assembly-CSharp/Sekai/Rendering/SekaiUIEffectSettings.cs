using System.Collections.Generic;

namespace Sekai.Rendering
{
	public static class SekaiUIEffectSettings
	{
		private static List<SekaiUIEffectParameter> parameters = new List<SekaiUIEffectParameter>();

		public static readonly SekaiUIBlurParameter Blur = new SekaiUIBlurParameter();

		public static IReadOnlyList<SekaiUIEffectParameter> Parameters => parameters;

		public static void SetParameters(List<SekaiUIEffectParameter> parameters)
		{
			SekaiUIEffectSettings.parameters = parameters ?? new List<SekaiUIEffectParameter>();
			Blur.IsActive = false;
			foreach (var parameter in SekaiUIEffectSettings.parameters)
			{
				if ((parameter.EffectFlags & SekaiUIEffectFlags.Blur) == 0)
				{
					continue;
				}

				Blur.IsActive = true;
				Blur.CaptureResolutionScale = parameter.CaptureResolutionScale;
				Blur.BlurSamplingDistance = parameter.BlurSamplingDistance;
				break;
			}
		}
	}
}
