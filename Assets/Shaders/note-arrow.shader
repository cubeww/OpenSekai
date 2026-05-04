Shader "Sekai/Live/Note/Arrow"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _NoteShowRate ("NoteShowRate", Range(0, 1)) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "IgnoreProjector" = "True"
            "CanUseSpriteAtlas" = "True"
        }

        Pass
        {
            Tags
            {
                "Queue" = "Transparent"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "IgnoreProjector" = "True"
                "CanUseSpriteAtlas" = "True"
            }

            Blend One OneMinusSrcAlpha, One OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _NoteShowRate;

            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float scroll = frac(_Time.z);
                float2 sampleUv = float2(i.uv.x, i.uv.y + 1.0 - scroll * 2.0);
                fixed4 tex = tex2D(_MainTex, sampleUv);

                float2 local = float2(frac(i.uv.x * 4.0), i.uv.y) - 0.5;
                float angle = radians(i.color.r * 180.0 - 90.0);
                float rotated = dot(float2(cos(angle), -sin(angle)), local);
                float band = saturate((1.0 - abs((rotated + 0.5) * 2.0 - 1.0)) * 10.0 - 4.0);
                fixed alpha = band * tex.a * _NoteShowRate;
                return fixed4(tex.rgb * alpha, alpha);
            }
            ENDCG
        }
    }
}
