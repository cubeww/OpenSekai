Shader "Area/Post" {
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
			uniform 	vec4 _BlitTexture_TexelSize;
			out highp vec2 vs_TEXCOORD0;
			out highp vec2 vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			vec2 u_xlat2;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    gl_Position.zw = vec2(-1.0, 1.0);
			    u_xlatu0.x =  uint(int(int_bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(uint(gl_VertexID) & 2u);
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    u_xlat2.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.xy = u_xlat2.xy;
			    u_xlat0.x = u_xlat2.x * _BlitTexture_TexelSize.y;
			    vs_TEXCOORD1.y = u_xlat2.y;
			    vs_TEXCOORD1.x = u_xlat0.x * _BlitTexture_TexelSize.z;
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
			uniform 	mediump vec4 _FilterParams;
			uniform 	float _BrightnessIntensity;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump vec3 u_xlat16_0;
			mediump vec2 u_xlat16_1;
			float u_xlat6;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.x = dot(vec3(0.212599993, 0.715200007, 0.0722000003), u_xlat16_0.xyz);
			    u_xlat6 = log2(u_xlat16_1.x);
			    u_xlat6 = u_xlat6 * _BrightnessIntensity;
			    u_xlat6 = exp2(u_xlat6);
			    u_xlat16_1.xy = vec2(u_xlat6) + (-_FilterParams.yx);
			    u_xlat16_1.x = max(u_xlat16_1.x, 0.0);
			    u_xlat16_1.x = min(u_xlat16_1.x, _FilterParams.z);
			    u_xlat16_1.x = u_xlat16_1.x * u_xlat16_1.x;
			    u_xlat16_1.x = u_xlat16_1.x * _FilterParams.w;
			    u_xlat16_1.x = max(u_xlat16_1.y, u_xlat16_1.x);
			    SV_Target0.xyz = u_xlat16_0.xyz * u_xlat16_1.xxx;
			    SV_Target0.w = u_xlat16_1.x;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			out highp vec2 vs_TEXCOORD0;
			out highp vec2 vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			vec2 u_xlat2;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    gl_Position.zw = vec2(-1.0, 1.0);
			    u_xlatu0.x =  uint(int(int_bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(uint(gl_VertexID) & 2u);
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    u_xlat2.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.xy = u_xlat2.xy;
			    u_xlat0.x = u_xlat2.x * _BlitTexture_TexelSize.y;
			    vs_TEXCOORD1.y = u_xlat2.y;
			    vs_TEXCOORD1.x = u_xlat0.x * _BlitTexture_TexelSize.z;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			uniform 	float _DownSamplingDelta;
			uniform 	mediump vec4 _BloomColor;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec3 u_xlat16_0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			mediump vec3 u_xlat16_2;
			mediump vec3 u_xlat16_3;
			void main()
			{
			    u_xlat0 = vec4(vec4(_DownSamplingDelta, _DownSamplingDelta, _DownSamplingDelta, _DownSamplingDelta)) * vec4(-1.0, -1.0, 1.0, 1.0);
			    u_xlat1 = _BlitTexture_TexelSize.xyxy * u_xlat0.xyzy + vs_TEXCOORD0.xyxy;
			    u_xlat0 = _BlitTexture_TexelSize.xyxy * u_xlat0.xwzw + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = u_xlat16_1.xyz + u_xlat16_2.xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = u_xlat16_1.xyz + u_xlat16_3.xyz;
			    u_xlat16_3.xyz = u_xlat16_0.xyz + u_xlat16_3.xyz;
			    u_xlat16_3.xyz = u_xlat16_3.xyz * _BloomColor.xyz;
			    SV_Target0.xyz = u_xlat16_3.xyz * vec3(0.25, 0.25, 0.25);
			    SV_Target0.w = _BloomColor.w;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			out highp vec2 vs_TEXCOORD0;
			out highp vec2 vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			vec2 u_xlat2;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    gl_Position.zw = vec2(-1.0, 1.0);
			    u_xlatu0.x =  uint(int(int_bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(uint(gl_VertexID) & 2u);
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    u_xlat2.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.xy = u_xlat2.xy;
			    u_xlat0.x = u_xlat2.x * _BlitTexture_TexelSize.y;
			    vs_TEXCOORD1.y = u_xlat2.y;
			    vs_TEXCOORD1.x = u_xlat0.x * _BlitTexture_TexelSize.z;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			mediump vec3 u_xlat16_2;
			void main()
			{
			    u_xlat0 = _BlitTexture_TexelSize.xyxy * vec4(-0.5, -0.5, 0.5, -0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = u_xlat16_0.xyz + u_xlat16_1.xyz;
			    u_xlat0 = _BlitTexture_TexelSize.xyxy * vec4(-0.5, 0.5, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = u_xlat16_1.xyz + u_xlat16_2.xyz;
			    u_xlat16_2.xyz = u_xlat16_0.xyz + u_xlat16_2.xyz;
			    SV_Target0.xyz = u_xlat16_2.xyz * vec3(0.25, 0.25, 0.25);
			    SV_Target0.w = 1.0;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			out highp vec2 vs_TEXCOORD0;
			out highp vec2 vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			vec2 u_xlat2;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    gl_Position.zw = vec2(-1.0, 1.0);
			    u_xlatu0.x =  uint(int(int_bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(uint(gl_VertexID) & 2u);
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    u_xlat2.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.xy = u_xlat2.xy;
			    u_xlat0.x = u_xlat2.x * _BlitTexture_TexelSize.y;
			    vs_TEXCOORD1.y = u_xlat2.y;
			    vs_TEXCOORD1.x = u_xlat0.x * _BlitTexture_TexelSize.z;
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
			uniform 	float _Intensity;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_0.xyz * vec3(_Intensity);
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			out highp vec2 vs_TEXCOORD0;
			out highp vec2 vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			vec2 u_xlat2;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    gl_Position.zw = vec2(-1.0, 1.0);
			    u_xlatu0.x =  uint(int(int_bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(uint(gl_VertexID) & 2u);
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    u_xlat2.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.xy = u_xlat2.xy;
			    u_xlat0.x = u_xlat2.x * _BlitTexture_TexelSize.y;
			    vs_TEXCOORD1.y = u_xlat2.y;
			    vs_TEXCOORD1.x = u_xlat0.x * _BlitTexture_TexelSize.z;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			uniform 	float _Intensity;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			mediump vec3 u_xlat16_2;
			void main()
			{
			    u_xlat0 = _BlitTexture_TexelSize.xyxy * vec4(-0.5, -0.5, 0.5, -0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = u_xlat16_0.xyz + u_xlat16_1.xyz;
			    u_xlat0 = _BlitTexture_TexelSize.xyxy * vec4(-0.5, 0.5, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = u_xlat16_1.xyz + u_xlat16_2.xyz;
			    u_xlat16_2.xyz = u_xlat16_0.xyz + u_xlat16_2.xyz;
			    u_xlat16_2.xyz = u_xlat16_2.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat0.xyz = u_xlat16_2.xyz * vec3(_Intensity);
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			out highp vec2 vs_TEXCOORD0;
			out highp vec2 vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			vec2 u_xlat2;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    gl_Position.zw = vec2(-1.0, 1.0);
			    u_xlatu0.x =  uint(int(int_bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(uint(gl_VertexID) & 2u);
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    u_xlat2.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.xy = u_xlat2.xy;
			    u_xlat0.x = u_xlat2.x * _BlitTexture_TexelSize.y;
			    vs_TEXCOORD1.y = u_xlat2.y;
			    vs_TEXCOORD1.x = u_xlat0.x * _BlitTexture_TexelSize.z;
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
			uniform 	float _VignettePower;
			uniform 	mediump vec4 _VignetteColor;
			uniform 	float _VignetteTop;
			uniform 	float _VignetteBottom;
			in highp vec2 vs_TEXCOORD0;
			in highp vec2 vs_TEXCOORD1;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump float u_xlat16_1;
			float u_xlat2;
			void main()
			{
			    u_xlat0.x = (-_VignetteBottom) + _VignetteTop;
			    u_xlat0.x = vs_TEXCOORD0.y * u_xlat0.x + _VignetteBottom;
			    u_xlat16_1 = dot(vs_TEXCOORD1.xy, vs_TEXCOORD1.xy);
			    u_xlat2 = u_xlat16_1 * _VignettePower;
			    u_xlat0.x = u_xlat0.x * u_xlat2;
			    u_xlat0 = u_xlat0.xxxx * _VignetteColor;
			    SV_Target0 = u_xlat0;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			out highp vec2 vs_TEXCOORD0;
			out highp vec2 vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			vec2 u_xlat2;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    gl_Position.zw = vec2(-1.0, 1.0);
			    u_xlatu0.x =  uint(int(int_bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(uint(gl_VertexID) & 2u);
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    u_xlat2.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.xy = u_xlat2.xy;
			    u_xlat0.x = u_xlat2.x * _BlitTexture_TexelSize.y;
			    vs_TEXCOORD1.y = u_xlat2.y;
			    vs_TEXCOORD1.x = u_xlat0.x * _BlitTexture_TexelSize.z;
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
			uniform 	float _VignettePower;
			uniform 	mediump vec4 _VignetteColor;
			uniform 	float _VignetteTop;
			uniform 	float _VignetteBottom;
			in highp vec2 vs_TEXCOORD0;
			in highp vec2 vs_TEXCOORD1;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			vec4 u_xlat1;
			mediump float u_xlat16_1;
			float u_xlat2;
			void main()
			{
			    u_xlat0.x = (-_VignetteBottom) + _VignetteTop;
			    u_xlat0.x = vs_TEXCOORD0.y * u_xlat0.x + _VignetteBottom;
			    u_xlat16_1 = dot(vs_TEXCOORD1.xy, vs_TEXCOORD1.xy);
			    u_xlat2 = u_xlat16_1 * _VignettePower;
			    u_xlat0.x = u_xlat0.x * u_xlat2;
			    u_xlat1 = _VignetteColor + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat0 = u_xlat0.xxxx * u_xlat1 + vec4(1.0, 1.0, 1.0, 1.0);
			    SV_Target0 = u_xlat0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}