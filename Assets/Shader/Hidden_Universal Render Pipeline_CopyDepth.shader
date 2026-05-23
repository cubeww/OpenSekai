Shader "Hidden/Universal Render Pipeline/CopyDepth" {
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
			uniform 	vec4 _BlitScaleBias;
			out highp vec2 vs_TEXCOORD0;
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
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	vec2 _GlobalMipBias;
			UNITY_LOCATION(0) uniform highp sampler2D _CameraDepthAttachment;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp float SV_Target0;
			float u_xlat0;
			void main()
			{
			    u_xlat0 = texture(_CameraDepthAttachment, vs_TEXCOORD0.xy, _GlobalMipBias.x).x;
			    SV_Target0 = u_xlat0;
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
			uniform 	vec4 _BlitScaleBias;
			out highp vec2 vs_TEXCOORD0;
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
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	vec2 _GlobalMipBias;
			UNITY_LOCATION(0) uniform highp sampler2D _CameraDepthAttachment;
			in highp vec2 vs_TEXCOORD0;
			float u_xlat0;
			void main()
			{
			    u_xlat0 = texture(_CameraDepthAttachment, vs_TEXCOORD0.xy, _GlobalMipBias.x).x;
			    gl_FragDepth = u_xlat0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_2"
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
			uniform 	vec4 _BlitScaleBias;
			out highp vec2 vs_TEXCOORD0;
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
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	vec4 _CameraDepthAttachment_TexelSize;
			UNITY_LOCATION(0) uniform highp sampler2DMS _CameraDepthAttachment;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp float SV_Target0;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			float u_xlat1;
			float u_xlat2;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _CameraDepthAttachment_TexelSize.zw;
			    u_xlatu0.xy =  uvec2(ivec2(u_xlat0.xy));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat1 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 0).x;
			    u_xlat0.x = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 1).x;
			    u_xlat2 = max(u_xlat1, 0.0);
			    SV_Target0 = max(u_xlat2, u_xlat0.x);
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_3"
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
			uniform 	vec4 _BlitScaleBias;
			out highp vec2 vs_TEXCOORD0;
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
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	vec4 _CameraDepthAttachment_TexelSize;
			UNITY_LOCATION(0) uniform highp sampler2DMS _CameraDepthAttachment;
			in highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			float u_xlat1;
			float u_xlat2;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _CameraDepthAttachment_TexelSize.zw;
			    u_xlatu0.xy =  uvec2(ivec2(u_xlat0.xy));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat1 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 0).x;
			    u_xlat0.x = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 1).x;
			    u_xlat2 = max(u_xlat1, 0.0);
			    gl_FragDepth = max(u_xlat2, u_xlat0.x);
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_4"
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
			uniform 	vec4 _BlitScaleBias;
			out highp vec2 vs_TEXCOORD0;
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
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	vec4 _CameraDepthAttachment_TexelSize;
			UNITY_LOCATION(0) uniform highp sampler2DMS _CameraDepthAttachment;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp float SV_Target0;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			float u_xlat1;
			float u_xlat2;
			float u_xlat3;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _CameraDepthAttachment_TexelSize.zw;
			    u_xlatu0.xy =  uvec2(ivec2(u_xlat0.xy));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat1 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 0).x;
			    u_xlat1 = max(u_xlat1, 0.0);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 1).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 2).x;
			    u_xlat0.x = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 3).x;
			    u_xlat2 = max(u_xlat1, u_xlat3);
			    SV_Target0 = max(u_xlat2, u_xlat0.x);
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_5"
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
			uniform 	vec4 _BlitScaleBias;
			out highp vec2 vs_TEXCOORD0;
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
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	vec4 _CameraDepthAttachment_TexelSize;
			UNITY_LOCATION(0) uniform highp sampler2DMS _CameraDepthAttachment;
			in highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			float u_xlat1;
			float u_xlat2;
			float u_xlat3;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _CameraDepthAttachment_TexelSize.zw;
			    u_xlatu0.xy =  uvec2(ivec2(u_xlat0.xy));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat1 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 0).x;
			    u_xlat1 = max(u_xlat1, 0.0);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 1).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 2).x;
			    u_xlat0.x = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 3).x;
			    u_xlat2 = max(u_xlat1, u_xlat3);
			    gl_FragDepth = max(u_xlat2, u_xlat0.x);
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_6"
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
			uniform 	vec4 _BlitScaleBias;
			out highp vec2 vs_TEXCOORD0;
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
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	vec4 _CameraDepthAttachment_TexelSize;
			UNITY_LOCATION(0) uniform highp sampler2DMS _CameraDepthAttachment;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp float SV_Target0;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			float u_xlat1;
			float u_xlat2;
			float u_xlat3;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _CameraDepthAttachment_TexelSize.zw;
			    u_xlatu0.xy =  uvec2(ivec2(u_xlat0.xy));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat1 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 0).x;
			    u_xlat1 = max(u_xlat1, 0.0);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 1).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 2).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 3).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 4).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 5).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 6).x;
			    u_xlat0.x = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 7).x;
			    u_xlat2 = max(u_xlat1, u_xlat3);
			    SV_Target0 = max(u_xlat2, u_xlat0.x);
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_7"
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
			uniform 	vec4 _BlitScaleBias;
			out highp vec2 vs_TEXCOORD0;
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
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	vec4 _CameraDepthAttachment_TexelSize;
			UNITY_LOCATION(0) uniform highp sampler2DMS _CameraDepthAttachment;
			in highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			float u_xlat1;
			float u_xlat2;
			float u_xlat3;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _CameraDepthAttachment_TexelSize.zw;
			    u_xlatu0.xy =  uvec2(ivec2(u_xlat0.xy));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat1 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 0).x;
			    u_xlat1 = max(u_xlat1, 0.0);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 1).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 2).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 3).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 4).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 5).x;
			    u_xlat1 = max(u_xlat1, u_xlat3);
			    u_xlat3 = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 6).x;
			    u_xlat0.x = texelFetch(_CameraDepthAttachment, ivec2(u_xlatu0.xy), 7).x;
			    u_xlat2 = max(u_xlat1, u_xlat3);
			    gl_FragDepth = max(u_xlat2, u_xlat0.x);
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}