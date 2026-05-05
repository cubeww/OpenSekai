Shader "Sekai/Note/Particles/Additive+AlphaBlend"
{
	Properties
	{
		_MainTex ("Particle Texture", 2D) = "white" {}
		[Toggle] _UseFlipMatrix ("UseFlipMatrix", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"IgnoreProjector" = "True"
		}

		Pass
		{
			Tags
			{
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"IgnoreProjector" = "True"
			}

			Blend One OneMinusSrcAlpha, One OneMinusSrcAlpha
			ColorMask RGB
			ZTest Off
			ZWrite Off
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4x4 _TapEffectViewMatrix;
			float4x4 _TapEffectProjectionMatrix;
			float _UseFlipMatrix;

			struct appdata
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float3 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 uv : TEXCOORD0;
				float blendSelector : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				float4 worldPosition = mul(unity_ObjectToWorld, v.vertex);
				float4 tapPosition = mul(_TapEffectProjectionMatrix, mul(_TapEffectViewMatrix, worldPosition));
				float4 defaultPosition = UnityObjectToClipPos(v.vertex);
				o.vertex = _UseFlipMatrix == 1.0 ? tapPosition : defaultPosition;
				o.color = v.color;
				o.uv = TRANSFORM_TEX(v.uv.xy, _MainTex);
				o.blendSelector = v.uv.z;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv) * i.color;
				fixed alpha = color.a * step(i.blendSelector, 0.5);
				return fixed4(color.rgb * color.a, alpha);
			}
			ENDCG
		}
	}
}
