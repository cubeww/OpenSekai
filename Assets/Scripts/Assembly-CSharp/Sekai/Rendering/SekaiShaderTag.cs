namespace Sekai.Rendering
{
	public static class SekaiShaderTag
	{
		public static string GetTag(SekaiShaderTagType shaderTagType)
		{
			switch (shaderTagType)
			{
				case SekaiShaderTagType.OpaqueOutline:
					return "SekaiOutline";
				case SekaiShaderTagType.OpaqueReflection:
					return "SekaiReflection";
				case SekaiShaderTagType.TransparentBase:
					return "SekaiTransparentBase";
				case SekaiShaderTagType.TransparentOutline:
					return "SekaiTransparentOutline";
				case SekaiShaderTagType.TransparentReflection:
					return "SekaiTransparentReflection";
				case SekaiShaderTagType.MeshFlarePara:
					return "SekaiMeshFlarePara";
				case SekaiShaderTagType.Monitor:
					return "SekaiMonitor";
				case SekaiShaderTagType.Eyelash:
					return "SekaiEyelash";
				default:
					return "SRPDefaultUnlit";
			}
		}

		public static string GetBeforePostProcessTag(SekaiShaderTagType shaderTagType)
		{
			return shaderTagType == SekaiShaderTagType.MeshFlarePara
				? "SekaiMeshFlarePara"
				: "SRPDefaultUnlit";
		}
	}
}
