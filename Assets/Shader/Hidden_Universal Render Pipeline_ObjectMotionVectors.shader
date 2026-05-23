Shader "Hidden/Universal Render Pipeline/ObjectMotionVectors" {
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
			uniform 	vec4 hlslcc_mtx4x4_PrevViewProjMatrix[4];
			uniform 	vec4 hlslcc_mtx4x4_NonJitteredViewProjMatrix[4];
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
				UNITY_UNIFORM vec4                hlslcc_mtx4x4unity_MatrixPreviousM[4];
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXhlslcc_mtx4x4unity_MatrixPreviousMI[4];
				UNITY_UNIFORM vec4                unity_MotionVectorsParams;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			in highp vec4 in_POSITION0;
			in highp vec3 in_TEXCOORD4;
			out highp vec4 vs_TEXCOORD0;
			out highp vec4 vs_TEXCOORD1;
			vec4 u_xlat0;
			bool u_xlatb0;
			vec4 u_xlat1;
			void main()
			{
			    u_xlat0.xyz = in_POSITION0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
			    u_xlat0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * in_POSITION0.xxx + u_xlat0.xyz;
			    u_xlat0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * in_POSITION0.zzz + u_xlat0.xyz;
			    u_xlat0.xyz = u_xlat0.xyz + hlslcc_mtx4x4unity_ObjectToWorld[3].xyz;
			    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
			    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
			    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
			    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_MatrixVP[3];
			    gl_Position.z = unity_MotionVectorsParams.z * u_xlat0.w + u_xlat0.z;
			    gl_Position.xyw = u_xlat0.xyw;
			    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
			    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
			    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
			    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[3] * in_POSITION0.wwww + u_xlat0;
			    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4_NonJitteredViewProjMatrix[1];
			    u_xlat1 = hlslcc_mtx4x4_NonJitteredViewProjMatrix[0] * u_xlat0.xxxx + u_xlat1;
			    u_xlat1 = hlslcc_mtx4x4_NonJitteredViewProjMatrix[2] * u_xlat0.zzzz + u_xlat1;
			    vs_TEXCOORD0 = hlslcc_mtx4x4_NonJitteredViewProjMatrix[3] * u_xlat0.wwww + u_xlat1;
			    u_xlatb0 = unity_MotionVectorsParams.x==1.0;
			    u_xlat1.xyz = in_TEXCOORD4.xyz;
			    u_xlat1.w = 1.0;
			    u_xlat0 = (bool(u_xlatb0)) ? u_xlat1 : in_POSITION0;
			    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixPreviousM[1];
			    u_xlat1 = hlslcc_mtx4x4unity_MatrixPreviousM[0] * u_xlat0.xxxx + u_xlat1;
			    u_xlat1 = hlslcc_mtx4x4unity_MatrixPreviousM[2] * u_xlat0.zzzz + u_xlat1;
			    u_xlat0 = hlslcc_mtx4x4unity_MatrixPreviousM[3] * u_xlat0.wwww + u_xlat1;
			    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4_PrevViewProjMatrix[1];
			    u_xlat1 = hlslcc_mtx4x4_PrevViewProjMatrix[0] * u_xlat0.xxxx + u_xlat1;
			    u_xlat1 = hlslcc_mtx4x4_PrevViewProjMatrix[2] * u_xlat0.zzzz + u_xlat1;
			    vs_TEXCOORD1 = hlslcc_mtx4x4_PrevViewProjMatrix[3] * u_xlat0.wwww + u_xlat1;
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 300 es

			precision highp float;
			precision highp int;
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
				UNITY_UNIFORM vec4                hlslcc_mtx4x4unity_MatrixPreviousM[4];
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXhlslcc_mtx4x4unity_MatrixPreviousMI[4];
				UNITY_UNIFORM vec4                unity_MotionVectorsParams;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			in highp vec4 vs_TEXCOORD0;
			in highp vec4 vs_TEXCOORD1;
			layout(location = 0) out mediump vec4 SV_Target0;
			float u_xlat0;
			bool u_xlatb0;
			mediump vec2 u_xlat16_1;
			vec2 u_xlat2;
			void main()
			{
			    u_xlatb0 = unity_MotionVectorsParams.y==0.0;
			    if(u_xlatb0){
			        SV_Target0 = vec4(0.0, 0.0, 0.0, 0.0);
			        return;
			    }
			    u_xlat0 = float(1.0) / float(vs_TEXCOORD0.w);
			    u_xlat2.x = float(1.0) / float(vs_TEXCOORD1.w);
			    u_xlat2.xy = u_xlat2.xx * vs_TEXCOORD1.xy;
			    u_xlat16_1.xy = vs_TEXCOORD0.xy * vec2(u_xlat0) + (-u_xlat2.xy);
			    SV_Target0.xy = u_xlat16_1.xy * vec2(0.5, 0.5);
			    SV_Target0.zw = vec2(0.0, 0.0);
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_1"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 300 es
			#ifndef UNITY_RUNTIME_INSTANCING_ARRAY_SIZE
				#define UNITY_RUNTIME_INSTANCING_ARRAY_SIZE 2
			#endif

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
			uniform 	vec4 hlslcc_mtx4x4_PrevViewProjMatrix[4];
			uniform 	vec4 hlslcc_mtx4x4_NonJitteredViewProjMatrix[4];
			uniform 	int unity_BaseInstanceID;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(0) uniform UnityPerDraw {
			#endif
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXhlslcc_mtx4x4unity_ObjectToWorld[4];
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
				UNITY_UNIFORM vec4                unity_MotionVectorsParams;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			struct unity_Builtins0Array_Type {
				vec4 hlslcc_mtx4x4unity_ObjectToWorldArray[4];
				vec4 hlslcc_mtx4x4unity_WorldToObjectArray[4];
			};
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(1) uniform UnityInstancing_PerDraw0 {
			#endif
				UNITY_UNIFORM unity_Builtins0Array_Type                unity_Builtins0Array[UNITY_RUNTIME_INSTANCING_ARRAY_SIZE];
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			struct unity_Builtins3Array_Type {
				vec4 hlslcc_mtx4x4unity_PrevObjectToWorldArray[4];
				vec4 hlslcc_mtx4x4unity_PrevWorldToObjectArray[4];
			};
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(2) uniform UnityInstancing_PerDraw3 {
			#endif
				UNITY_UNIFORM unity_Builtins3Array_Type                unity_Builtins3Array[UNITY_RUNTIME_INSTANCING_ARRAY_SIZE];
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			in highp vec4 in_POSITION0;
			in highp vec3 in_TEXCOORD4;
			out highp vec4 vs_TEXCOORD0;
			out highp vec4 vs_TEXCOORD1;
			flat out highp uint vs_SV_InstanceID0;
			vec4 u_xlat0;
			int u_xlati0;
			vec4 u_xlat1;
			vec4 u_xlat2;
			vec3 u_xlat3;
			bool u_xlatb3;
			void main()
			{
			    u_xlati0 = gl_InstanceID + unity_BaseInstanceID;
			    u_xlati0 = int(u_xlati0 << (3 & int(0x1F)));
			    u_xlat3.xyz = in_POSITION0.yyy * unity_Builtins0Array[u_xlati0 / 8].hlslcc_mtx4x4unity_ObjectToWorldArray[1].xyz;
			    u_xlat3.xyz = unity_Builtins0Array[u_xlati0 / 8].hlslcc_mtx4x4unity_ObjectToWorldArray[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
			    u_xlat3.xyz = unity_Builtins0Array[u_xlati0 / 8].hlslcc_mtx4x4unity_ObjectToWorldArray[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
			    u_xlat3.xyz = u_xlat3.xyz + unity_Builtins0Array[u_xlati0 / 8].hlslcc_mtx4x4unity_ObjectToWorldArray[3].xyz;
			    u_xlat1 = u_xlat3.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
			    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat3.xxxx + u_xlat1;
			    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat3.zzzz + u_xlat1;
			    u_xlat1 = u_xlat1 + hlslcc_mtx4x4unity_MatrixVP[3];
			    gl_Position.z = unity_MotionVectorsParams.z * u_xlat1.w + u_xlat1.z;
			    gl_Position.xyw = u_xlat1.xyw;
			    u_xlat1 = in_POSITION0.yyyy * unity_Builtins0Array[u_xlati0 / 8].hlslcc_mtx4x4unity_ObjectToWorldArray[1];
			    u_xlat1 = unity_Builtins0Array[u_xlati0 / 8].hlslcc_mtx4x4unity_ObjectToWorldArray[0] * in_POSITION0.xxxx + u_xlat1;
			    u_xlat1 = unity_Builtins0Array[u_xlati0 / 8].hlslcc_mtx4x4unity_ObjectToWorldArray[2] * in_POSITION0.zzzz + u_xlat1;
			    u_xlat1 = unity_Builtins0Array[u_xlati0 / 8].hlslcc_mtx4x4unity_ObjectToWorldArray[3] * in_POSITION0.wwww + u_xlat1;
			    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4_NonJitteredViewProjMatrix[1];
			    u_xlat2 = hlslcc_mtx4x4_NonJitteredViewProjMatrix[0] * u_xlat1.xxxx + u_xlat2;
			    u_xlat2 = hlslcc_mtx4x4_NonJitteredViewProjMatrix[2] * u_xlat1.zzzz + u_xlat2;
			    vs_TEXCOORD0 = hlslcc_mtx4x4_NonJitteredViewProjMatrix[3] * u_xlat1.wwww + u_xlat2;
			    u_xlatb3 = unity_MotionVectorsParams.x==1.0;
			    u_xlat1.xyz = in_TEXCOORD4.xyz;
			    u_xlat1.w = 1.0;
			    u_xlat1 = (bool(u_xlatb3)) ? u_xlat1 : in_POSITION0;
			    u_xlat2 = u_xlat1.yyyy * unity_Builtins3Array[u_xlati0 / 8].hlslcc_mtx4x4unity_PrevObjectToWorldArray[1];
			    u_xlat2 = unity_Builtins3Array[u_xlati0 / 8].hlslcc_mtx4x4unity_PrevObjectToWorldArray[0] * u_xlat1.xxxx + u_xlat2;
			    u_xlat2 = unity_Builtins3Array[u_xlati0 / 8].hlslcc_mtx4x4unity_PrevObjectToWorldArray[2] * u_xlat1.zzzz + u_xlat2;
			    u_xlat0 = unity_Builtins3Array[u_xlati0 / 8].hlslcc_mtx4x4unity_PrevObjectToWorldArray[3] * u_xlat1.wwww + u_xlat2;
			    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4_PrevViewProjMatrix[1];
			    u_xlat1 = hlslcc_mtx4x4_PrevViewProjMatrix[0] * u_xlat0.xxxx + u_xlat1;
			    u_xlat1 = hlslcc_mtx4x4_PrevViewProjMatrix[2] * u_xlat0.zzzz + u_xlat1;
			    vs_TEXCOORD1 = hlslcc_mtx4x4_PrevViewProjMatrix[3] * u_xlat0.wwww + u_xlat1;
			    vs_SV_InstanceID0 =  uint(gl_InstanceID);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 300 es

			precision highp float;
			precision highp int;
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
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(0) uniform UnityPerDraw {
			#endif
				UNITY_UNIFORM vec4 Xhlslcc_UnusedXhlslcc_mtx4x4unity_ObjectToWorld[4];
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
				UNITY_UNIFORM vec4                unity_MotionVectorsParams;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			in highp vec4 vs_TEXCOORD0;
			in highp vec4 vs_TEXCOORD1;
			layout(location = 0) out mediump vec4 SV_Target0;
			float u_xlat0;
			bool u_xlatb0;
			mediump vec2 u_xlat16_1;
			vec2 u_xlat2;
			void main()
			{
			    u_xlatb0 = unity_MotionVectorsParams.y==0.0;
			    if(u_xlatb0){
			        SV_Target0 = vec4(0.0, 0.0, 0.0, 0.0);
			        return;
			    }
			    u_xlat0 = float(1.0) / float(vs_TEXCOORD0.w);
			    u_xlat2.x = float(1.0) / float(vs_TEXCOORD1.w);
			    u_xlat2.xy = u_xlat2.xx * vs_TEXCOORD1.xy;
			    u_xlat16_1.xy = vs_TEXCOORD0.xy * vec2(u_xlat0) + (-u_xlat2.xy);
			    SV_Target0.xy = u_xlat16_1.xy * vec2(0.5, 0.5);
			    SV_Target0.zw = vec2(0.0, 0.0);
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}