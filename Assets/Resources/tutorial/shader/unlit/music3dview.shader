Shader "Sekai/Unlit/Music3DView"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _CutInTransitionColor ("CutIn Transition Color", Color) = (1,1,1,1)
        _CutInTransition ("CutIn Transition", Range(0,1)) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Geometry"
            "RenderType" = "Opaque"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One Zero

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
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _CutInTransitionColor;
            half _CutInTransition;

            v2f vert(appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 tex = tex2D(_MainTex, i.texcoord).rgb;
                fixed3 color = lerp(tex, _CutInTransitionColor.rgb, saturate(_CutInTransition));
                return fixed4(color * _Color.rgb, 1);
            }
            ENDCG
        }
    }
}
