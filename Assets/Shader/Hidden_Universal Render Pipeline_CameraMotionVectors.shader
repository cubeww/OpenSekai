Shader "Hidden/Universal Render Pipeline/CameraMotionVectors" {
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

			vec2 u_xlat0;
			uvec3 u_xlatu0;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    u_xlatu0.x =  uint(int(int_bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(uint(gl_VertexID) & 2u);
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    gl_Position.zw = vec2(-1.0, 1.0);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec2 _GlobalMipBias;
			uniform 	vec4 hlslcc_mtx4x4unity_MatrixInvVP[4];
			uniform 	vec4 hlslcc_mtx4x4_PrevViewProjMatrix[4];
			uniform 	vec4 hlslcc_mtx4x4_NonJitteredViewProjMatrix[4];
			UNITY_LOCATION(0) uniform highp sampler2D _CameraDepthTexture;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			vec4 u_xlat1;
			vec3 u_xlat2;
			mediump vec2 u_xlat16_3;
			float u_xlat4;
			vec2 u_xlat8;
			float u_xlat12;
			void main()
			{
			vec4 hlslcc_FragCoord = vec4(gl_FragCoord.xyz, 1.0/gl_FragCoord.w);
			    u_xlat0.xy = hlslcc_FragCoord.xy / _ScaledScreenParams.xy;
			    u_xlat8.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlat0.x = texture(_CameraDepthTexture, u_xlat0.xy, _GlobalMipBias.x).x;
			    u_xlat1 = u_xlat8.yyyy * hlslcc_mtx4x4unity_MatrixInvVP[1];
			    u_xlat1 = hlslcc_mtx4x4unity_MatrixInvVP[0] * u_xlat8.xxxx + u_xlat1;
			    u_xlat4 = u_xlat0.x * 2.0 + -1.0;
			    gl_FragDepth = u_xlat0.x;
			    u_xlat0 = hlslcc_mtx4x4unity_MatrixInvVP[2] * vec4(u_xlat4) + u_xlat1;
			    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_MatrixInvVP[3];
			    u_xlat0.xyz = u_xlat0.xyz / u_xlat0.www;
			    u_xlat1.xyz = u_xlat0.yyy * hlslcc_mtx4x4_PrevViewProjMatrix[1].xyw;
			    u_xlat1.xyz = hlslcc_mtx4x4_PrevViewProjMatrix[0].xyw * u_xlat0.xxx + u_xlat1.xyz;
			    u_xlat1.xyz = hlslcc_mtx4x4_PrevViewProjMatrix[2].xyw * u_xlat0.zzz + u_xlat1.xyz;
			    u_xlat1.xyz = u_xlat1.xyz + hlslcc_mtx4x4_PrevViewProjMatrix[3].xyw;
			    u_xlat12 = float(1.0) / float(u_xlat1.z);
			    u_xlat1.xy = vec2(u_xlat12) * u_xlat1.xy;
			    u_xlat2.xyz = u_xlat0.yyy * hlslcc_mtx4x4_NonJitteredViewProjMatrix[1].xyw;
			    u_xlat0.xyw = hlslcc_mtx4x4_NonJitteredViewProjMatrix[0].xyw * u_xlat0.xxx + u_xlat2.xyz;
			    u_xlat0.xyz = hlslcc_mtx4x4_NonJitteredViewProjMatrix[2].xyw * u_xlat0.zzz + u_xlat0.xyw;
			    u_xlat0.xyz = u_xlat0.xyz + hlslcc_mtx4x4_NonJitteredViewProjMatrix[3].xyw;
			    u_xlat8.x = float(1.0) / float(u_xlat0.z);
			    u_xlat16_3.xy = u_xlat0.xy * u_xlat8.xx + (-u_xlat1.xy);
			    u_xlat0.xy = u_xlat16_3.xy * vec2(0.5, 0.5);
			    SV_Target0.xy = u_xlat0.xy;
			    SV_Target0.zw = vec2(0.0, 0.0);
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}