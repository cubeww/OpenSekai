Shader "Sekai/Unlit/NoteLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _NoteShowRate ("NoteShowRate", Range(0, 1)) = 1
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
        ZTest LEqual
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
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float2 leftPoint : TEXCOORD1;
                float2 rightPoint : TEXCOORD2;
                float2 offsetLeft : TEXCOORD3;
                float2 offsetRight : TEXCOORD4;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float2 leftPoint : TEXCOORD1;
                float2 rightPoint : TEXCOORD2;
                float2 offsetLeft : TEXCOORD3;
                float2 offsetRight : TEXCOORD4;
                float2 localPosition : TEXCOORD5;
                float4 screenPosition : TEXCOORD6;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _NoteShowRate;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                o.texcoord = v.texcoord;
                o.color = v.color * _Color;
                o.leftPoint = v.leftPoint;
                o.rightPoint = v.rightPoint;
                o.offsetLeft = v.offsetLeft;
                o.offsetRight = v.offsetRight;
                o.localPosition = v.vertex.xy;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 screenUv = i.screenPosition.xy / i.screenPosition.w;
                clip(_NoteShowRate - screenUv.y);

                float left = i.leftPoint.y;
                float right = i.rightPoint.y;
                float lineRate = saturate((i.localPosition.x - left) / max(right - left, 0.0001));
                float textureX = lerp(i.offsetLeft.y, i.offsetRight.y, lineRate);
                float textureY = i.texcoord.y * 0.2 + i.offsetRight.x;

                fixed4 baseColor = tex2D(_MainTex, float2(textureX, textureY));
                fixed3 pressedColor = tex2D(_MainTex, float2(textureX, textureY + 0.25)).rgb;
                baseColor.rgb = lerp(baseColor.rgb, pressedColor, i.offsetLeft.x);
                return baseColor * i.color;
            }
            ENDCG
        }
    }
}
