Shader "Sekai/Unlit/NoteLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 leftPoint : TEXCOORD1;
                float2 rightPoint : TEXCOORD2;
                float2 uvOffsetLeft : TEXCOORD3;
                float2 uvOffsetRight : TEXCOORD4;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 leftPoint : TEXCOORD1;
                float2 rightPoint : TEXCOORD2;
                float2 uvOffsetLeft : TEXCOORD3;
                float2 uvOffsetRight : TEXCOORD4;
                float2 localPosition : TEXCOORD5;
                fixed4 color : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.leftPoint = v.leftPoint;
                o.rightPoint = v.rightPoint;
                o.uvOffsetLeft = v.uvOffsetLeft;
                o.uvOffsetRight = v.uvOffsetRight;
                o.localPosition = v.vertex.xy;
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float leftDistance = i.localPosition.x - i.leftPoint.y;
                float rightDistance = i.rightPoint.y - i.localPosition.x;
                float laneT = leftDistance / max(leftDistance + rightDistance, 0.0001);
                float sampleX = lerp(i.uvOffsetLeft.y, i.uvOffsetRight.y, laneT);
                float sampleY = i.uv.y * 0.2 + i.uvOffsetRight.x;
                fixed4 baseColor = tex2D(_MainTex, float2(sampleX, sampleY));
                fixed3 shiftColor = tex2D(_MainTex, float2(sampleX, sampleY + 0.25)).rgb;
                baseColor.rgb = lerp(baseColor.rgb, shiftColor, i.uvOffsetLeft.x);
                return baseColor * i.color;
            }
            ENDCG
        }
    }
}
