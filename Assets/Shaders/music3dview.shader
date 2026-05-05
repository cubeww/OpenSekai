Shader "Sekai/Unlit/Music3DView"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1, 1, 1, 1)
        _CutInTransitionColor ("CutIn Transition Color", Color) = (1, 1, 1, 1)
        _CutInTransition ("CutIn Transition", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            Tags { "RenderType" = "Opaque" }
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _CutInTransitionColor;
            float _CutInTransition;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 color = tex2D(_MainTex, i.uv).rgb;
                color = lerp(color, _CutInTransitionColor.rgb, saturate(_CutInTransition));
                return fixed4(color * _Color.rgb, 1.0);
            }
            ENDCG
        }
    }

    Fallback "Legacy Shaders/VertexLit"
}
