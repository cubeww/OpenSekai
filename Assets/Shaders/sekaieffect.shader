Shader "Sekai/SekaiEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_LightTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
		_LightVector ("LightVector", Vector) = (1, 1, 0, 0)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"IgnoreProjector" = "True"
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _LightTex;
			fixed4 _Color;
			float4 _LightVector;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				fixed4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 uv : TEXCOORD0;
				float lightRate : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.color = v.color;

				float3 lightDirection = normalize(_LightVector.xyz);
				o.lightRate = saturate(dot(v.normal, lightDirection));
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 mainColor = tex2D(_MainTex, i.uv);
				fixed4 lightColor = tex2D(_LightTex, i.uv);
				fixed4 color = mainColor * (1.0 - i.lightRate) + lightColor * i.lightRate;
				return color * i.color;
			}
			ENDCG
		}
	}
}
