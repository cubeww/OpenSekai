Shader "Sekai/SekaiEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightTex ("Light Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _LightVector ("LightVector", Vector) = (1,1,0,0)
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
        Blend SrcAlpha OneMinusSrcAlpha

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
                float3 normal : NORMAL;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                half lightAmount : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _LightTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float4 _LightVector;

            v2f vert(appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                float3 lightDir = normalize(_LightVector.xyz);
                o.lightAmount = saturate(dot(v.normal, lightDir));
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.texcoord);
                fixed4 lightColor = tex2D(_LightTex, i.texcoord);
                return lerp(mainColor, lightColor, i.lightAmount) * i.color;
            }
            ENDCG
        }
    }
}
