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
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always
        Blend One OneMinusSrcAlpha

        Pass
        {
            Name "Default"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float3 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                half blendGate : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _UseFlipMatrix;
            float4x4 _TapEffectViewMatrix;
            float4x4 _TapEffectProjectionMatrix;

            v2f vert(appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
                float4 viewPos = mul(_TapEffectViewMatrix, worldPos);
                float projectionYSign = UNITY_MATRIX_P[1][1] < 0.0 ? -1.0 : 1.0;
                float4x4 tapProjection = _TapEffectProjectionMatrix;
                tapProjection._m11 *= projectionYSign;
                float4 customClip = mul(tapProjection, viewPos);
                float4 unityClip = UnityObjectToClipPos(v.vertex);
                o.vertex = lerp(unityClip, customClip, step(0.5, _UseFlipMatrix));
                o.color = v.color;
                o.texcoord = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
                o.blendGate = v.texcoord.z;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.texcoord) * i.color;
                fixed originalAlpha = color.a;
                color.rgb *= originalAlpha;
                color.a = originalAlpha * step(i.blendGate, 0.5);
                return color;
            }
            ENDCG
        }
    }
}
