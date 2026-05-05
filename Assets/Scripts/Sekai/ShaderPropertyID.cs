using UnityEngine;

namespace Sekai
{
	public static class ShaderPropertyID
	{
		public static readonly int TAP_EFFECT_PROJECTION_MATRIX_ID = Shader.PropertyToID("_TapEffectProjectionMatrix");

		public static readonly int TAP_EFFECT_VIEW_MATRIX_ID = Shader.PropertyToID("_TapEffectViewMatrix");
	}
}
