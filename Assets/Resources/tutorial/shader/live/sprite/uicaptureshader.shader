Shader "Sekai/Sprites/Capture"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _MulColor ("Mul Color", Color) = (1,1,1,1)
        _AddColor ("Add Color", Color) = (0,0,0,0)
        _AlphaBlendColor ("Alpha Blend Color", Color) = (0,0,0,0)

        [HideInInspector] _StencilComp ("Stencil Comparison", Float) = 8
        [HideInInspector] _Stencil ("Stencil ID", Float) = 0
        [HideInInspector] _StencilOp ("Stencil Operation", Float) = 0
        [HideInInspector] _StencilWriteMask ("Stencil Write Mask", Float) = 255
        [HideInInspector] _StencilReadMask ("Stencil Read Mask", Float) = 255
        [HideInInspector] _ColorMask ("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
            "RenderPipeline" = "UniversalPipeline"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float4 worldPosition : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _Color;
                half4 _MulColor;
                half4 _AddColor;
                half4 _AlphaBlendColor;
                float4 _ClipRect;
            CBUFFER_END

            TEXTURE2D_X(_UIBlurTex);
            SAMPLER(sampler_UIBlurTex);

            Varyings vert(Attributes input)
            {
                Varyings output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                output.worldPosition = input.positionOS;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.screenPos = ComputeScreenPos(output.positionCS);
                output.uv = input.uv;
                output.color = input.color * _Color;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float2 screenUv = input.screenPos.xy / input.screenPos.w;
                half4 blur = SAMPLE_TEXTURE2D_X(_UIBlurTex, sampler_UIBlurTex, screenUv);
                blur.rgb = blur.rgb * _MulColor.rgb + _AddColor.rgb;
                blur.rgb = lerp(blur.rgb, _AlphaBlendColor.rgb, _AlphaBlendColor.a);
                blur *= input.color;

                #ifdef UNITY_UI_CLIP_RECT
                float2 inside = step(_ClipRect.xy, input.worldPosition.xy) * step(input.worldPosition.xy, _ClipRect.zw);
                blur.a *= inside.x * inside.y;
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip(blur.a - 0.001);
                #endif

                return blur;
            }
            ENDHLSL
        }
    }
}
