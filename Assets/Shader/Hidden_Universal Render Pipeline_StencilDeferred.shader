Shader "Hidden/Universal Render Pipeline/StencilDeferred" {
	Properties {
		_StencilRef ("StencilRef", Float) = 0
		_StencilReadMask ("StencilReadMask", Float) = 0
		_StencilWriteMask ("StencilWriteMask", Float) = 0
		_LitPunctualStencilRef ("LitPunctualStencilWriteMask", Float) = 0
		_LitPunctualStencilReadMask ("LitPunctualStencilReadMask", Float) = 0
		_LitPunctualStencilWriteMask ("LitPunctualStencilWriteMask", Float) = 0
		_SimpleLitPunctualStencilRef ("SimpleLitPunctualStencilWriteMask", Float) = 0
		_SimpleLitPunctualStencilReadMask ("SimpleLitPunctualStencilReadMask", Float) = 0
		_SimpleLitPunctualStencilWriteMask ("SimpleLitPunctualStencilWriteMask", Float) = 0
		_LitDirStencilRef ("LitDirStencilRef", Float) = 0
		_LitDirStencilReadMask ("LitDirStencilReadMask", Float) = 0
		_LitDirStencilWriteMask ("LitDirStencilWriteMask", Float) = 0
		_SimpleLitDirStencilRef ("SimpleLitDirStencilRef", Float) = 0
		_SimpleLitDirStencilReadMask ("SimpleLitDirStencilReadMask", Float) = 0
		_SimpleLitDirStencilWriteMask ("SimpleLitDirStencilWriteMask", Float) = 0
		_ClearStencilRef ("ClearStencilRef", Float) = 0
		_ClearStencilReadMask ("ClearStencilReadMask", Float) = 0
		_ClearStencilWriteMask ("ClearStencilWriteMask", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
	Fallback "Hidden/Universal Render Pipeline/FallbackError"
}