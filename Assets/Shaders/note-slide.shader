Shader "Sekai/Live/Note/Slide"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _NoteShowRate ("NoteShowRate", Range(0, 1)) = 1
        _SlideRange ("SlideRange", Float) = 0.6
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
            float _SlideRange;

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
                float stripe = frac(v.uv.y * 7.0) * 2.0 - 1.0;
                float side = -sign(stripe);
                float localX = v.uv.x * 2.0 - 1.0;
                float slide = ((v.color.r * 2.0 - 1.0) + v.color.g * localX) * v.color.b * 0.6 * side;
                v.vertex.x += slide;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                fixed alpha = tex.a * i.color.a * _NoteShowRate;
                return fixed4(tex.rgb * alpha, tex.a * _NoteShowRate);
            }
            ENDCG
        }
    }
}
