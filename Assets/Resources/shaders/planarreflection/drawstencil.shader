Shader "Sekai/Live/DrawStencil" {
	Properties {
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		// Extracted GLSL subprograms. Variant and pass grouping is approximate.
		Pass {
			Name "GLSL_0"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 300 es

			#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			#define UNITY_UNIFORM
			#else
			#define UNITY_UNIFORM uniform
			#endif
			#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
			#if UNITY_SUPPORTS_UNIFORM_LOCATION
			#define UNITY_LOCATION(x) layout(location = x)
			#define UNITY_BINDING(x) layout(binding = x, std140)
			#else
			#define UNITY_LOCATION(x)
			#define UNITY_BINDING(x) layout(std140)
			#endif
			uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(0) uniform UnityPerDraw {
			#endif
				UNITY_UNIFORM vec4                hlslcc_mtx4x4unity_ObjectToWorld[4];
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXhlslcc_mtx4x4unity_WorldToObject[4];
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_LODFade;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_WorldTransformParams;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_RenderingLayer;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_LightData;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_LightIndices[2];
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_ProbesOcclusion;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_SpecCube0_HDR;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_SpecCube1_HDR;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_SpecCube0_BoxMax;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_SpecCube0_BoxMin;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_SpecCube0_ProbePosition;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_SpecCube1_BoxMax;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_SpecCube1_BoxMin;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_SpecCube1_ProbePosition;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_LightmapST;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_DynamicLightmapST;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_SHAr;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_SHAg;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_SHAb;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_SHBr;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_SHBg;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_SHBb;
				UNITY_UNIFORM mediump vec4 Xhlslcc_UnusedXunity_SHC;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_RendererBounds_Min;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_RendererBounds_Max;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXhlslcc_mtx4x4unity_MatrixPreviousM[4];
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXhlslcc_mtx4x4unity_MatrixPreviousMI[4];
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXunity_MotionVectorsParams;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			in highp vec4 in_POSITION0;
			vec4 u_xlat0;
			vec4 u_xlat1;
			void main()
			{
			    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
			    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
			    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
			    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
			    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
			    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
			    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
			    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 300 es

			precision highp float;
			precision highp int;
			layout(location = 0) out mediump vec4 SV_Target0;
			void main()
			{
			    SV_Target0 = vec4(1.0, 1.0, 1.0, 1.0);
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}