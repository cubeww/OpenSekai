Shader "Sekai/UI/UIGaussianBlur"
{
    Properties
    {
        _SamplingDistance ("Sampling Distance", Float) = 1.5
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
        }

        ZTest Always
        ZWrite Off
        Cull Off

        Pass
        {
            Name "Horizontal"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment FragHorizontal
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                uint vertexID : SV_VertexID;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            float _SamplingDistance;
            float4 _BlurResolutionParams;
            TEXTURE2D_X(_BlitTexture);
            SAMPLER(sampler_LinearClamp);

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
                output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
                return output;
            }

            half4 SampleBlur(float2 uv, float2 stepUv)
            {
                half4 color = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv) * 0.2270270270;
                color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv + stepUv * 1.3846153846) * 0.3162162162;
                color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv - stepUv * 1.3846153846) * 0.3162162162;
                color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv + stepUv * 3.2307692308) * 0.0702702703;
                color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv - stepUv * 3.2307692308) * 0.0702702703;
                return color;
            }

            half4 FragHorizontal(Varyings input) : SV_Target
            {
                return SampleBlur(input.texcoord, float2(_BlurResolutionParams.x * _SamplingDistance, 0.0));
            }
            ENDHLSL
        }

        Pass
        {
            Name "Vertical"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment FragVertical
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                uint vertexID : SV_VertexID;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            float _SamplingDistance;
            float4 _BlurResolutionParams;
            TEXTURE2D_X(_BlitTexture);
            SAMPLER(sampler_LinearClamp);

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
                output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
                return output;
            }

            half4 SampleBlur(float2 uv, float2 stepUv)
            {
                half4 color = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv) * 0.2270270270;
                color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv + stepUv * 1.3846153846) * 0.3162162162;
                color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv - stepUv * 1.3846153846) * 0.3162162162;
                color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv + stepUv * 3.2307692308) * 0.0702702703;
                color += SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv - stepUv * 3.2307692308) * 0.0702702703;
                return color;
            }

            half4 FragVertical(Varyings input) : SV_Target
            {
                return SampleBlur(input.texcoord, float2(0.0, _BlurResolutionParams.y * _SamplingDistance));
            }
            ENDHLSL
        }
    }
}
