Shader "Hidden/Universal Render Pipeline/TemporalAA" {
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
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(0) uniform TemporalAAData {
			#endif
				UNITY_UNIFORM vec4                _BlitTexture_TexelSize;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedX_TaaMotionVectorTex_TexelSize;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedX_TaaAccumulationTex_TexelSize;
				UNITY_UNIFORM float Xhlslcc_UnusedX_TaaFilterWeights[9];
				UNITY_UNIFORM mediump float                _TaaFrameInfluence;
				UNITY_UNIFORM mediump float Xhlslcc_UnusedX_TaaVarianceClampScale;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _TaaMotionVectorTex;
			UNITY_LOCATION(2) uniform mediump sampler2D _TaaAccumulationTex;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			mediump vec3 u_xlat16_2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			float u_xlat18;
			mediump float u_xlat16_22;
			void main()
			{
			    u_xlat16_0.xy = texture(_TaaMotionVectorTex, vs_TEXCOORD0.xy, _GlobalMipBias.x).xy;
			    u_xlat0.xy = (-u_xlat16_0.xy) + vs_TEXCOORD0.xy;
			    u_xlat16_0.xyz = texture(_TaaAccumulationTex, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat1 = _BlitTexture_TexelSize.xyxy * vec4(0.0, -1.0, -1.0, 0.0) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_4.xyz = min(u_xlat16_1.xyz, u_xlat16_3.xyz);
			    u_xlat16_5.xyz = max(u_xlat16_1.xyz, u_xlat16_3.xyz);
			    u_xlat16_5.xyz = max(u_xlat16_2.xyz, u_xlat16_5.xyz);
			    u_xlat16_4.xyz = min(u_xlat16_2.xyz, u_xlat16_4.xyz);
			    u_xlat1 = _BlitTexture_TexelSize.xyxy * vec4(1.0, 0.0, 0.0, 1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_4.xyz = min(u_xlat16_4.xyz, u_xlat16_2.xyz);
			    u_xlat16_5.xyz = max(u_xlat16_5.xyz, u_xlat16_2.xyz);
			    u_xlat16_5.xyz = max(u_xlat16_1.xyz, u_xlat16_5.xyz);
			    u_xlat16_4.xyz = min(u_xlat16_1.xyz, u_xlat16_4.xyz);
			    u_xlat16_4.xyz = max(u_xlat16_0.xyz, u_xlat16_4.xyz);
			    u_xlat16_4.xyz = min(u_xlat16_5.xyz, u_xlat16_4.xyz);
			    u_xlat16_22 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat0.x = u_xlat16_22 + 1.0;
			    u_xlat0.x = float(1.0) / float(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat16_4.xyz;
			    u_xlat16_4.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat18 = u_xlat16_4.x + 1.0;
			    u_xlat18 = float(1.0) / float(u_xlat18);
			    u_xlat1.xyz = u_xlat16_3.xyz * vec3(u_xlat18) + (-u_xlat0.xyz);
			    u_xlat0.xyz = vec3(_TaaFrameInfluence) * u_xlat1.xyz + u_xlat0.xyz;
			    u_xlat16_4.x = dot(u_xlat0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat18 = (-u_xlat16_4.x) + 1.0;
			    u_xlat18 = float(1.0) / float(u_xlat18);
			    u_xlat0.xyz = vec3(u_xlat18) * u_xlat0.xyz;
			    SV_Target0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
			    SV_Target0.w = 1.0;
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
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(0) uniform TemporalAAData {
			#endif
				UNITY_UNIFORM vec4                _BlitTexture_TexelSize;
				UNITY_UNIFORM vec4                _TaaMotionVectorTex_TexelSize;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedX_TaaAccumulationTex_TexelSize;
				UNITY_UNIFORM float Xhlslcc_UnusedX_TaaFilterWeights[9];
				UNITY_UNIFORM mediump float                _TaaFrameInfluence;
				UNITY_UNIFORM mediump float Xhlslcc_UnusedX_TaaVarianceClampScale;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(2) uniform mediump sampler2D _TaaMotionVectorTex;
			UNITY_LOCATION(3) uniform mediump sampler2D _TaaAccumulationTex;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			vec2 u_xlat3;
			mediump vec2 u_xlat16_3;
			bvec2 u_xlatb3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			mediump vec3 u_xlat16_7;
			mediump vec2 u_xlat16_9;
			bool u_xlatb10;
			bool u_xlatb11;
			vec2 u_xlat19;
			float u_xlat24;
			bool u_xlatb24;
			mediump float u_xlat16_25;
			void main()
			{
			    u_xlat0.x = texture(_CameraDepthTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).x;
			    u_xlat16_1.x = min(u_xlat0.x, 1.0);
			    u_xlat0 = _BlitTexture_TexelSize.xyxy * vec4(1.0, 0.0, 0.0, 1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat2.x = texture(_CameraDepthTexture, u_xlat0.xy, _GlobalMipBias.x).x;
			    u_xlatb10 = u_xlat2.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat2.x);
			    u_xlat16_9.x = (u_xlatb10) ? 1.0 : 0.0;
			    u_xlat2 = _BlitTexture_TexelSize.xyxy * vec4(0.0, -1.0, -1.0, 0.0) + vs_TEXCOORD0.xyxy;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat2.xy, _GlobalMipBias.x).x;
			    u_xlatb11 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_9.x = (u_xlatb11) ? 0.0 : u_xlat16_9.x;
			    u_xlat16_9.y = (u_xlatb11) ? -1.0 : 0.0;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat2.zw, _GlobalMipBias.x).x;
			    u_xlatb11 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_9.xy = (bool(u_xlatb11)) ? vec2(-1.0, 0.0) : u_xlat16_9.xy;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlatb3.x = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = (u_xlatb3.x) ? 0.0 : u_xlat16_9.x;
			    u_xlat16_1.y = (u_xlatb3.x) ? 1.0 : u_xlat16_9.y;
			    u_xlat3.xy = _TaaMotionVectorTex_TexelSize.xy * u_xlat16_1.xy + vs_TEXCOORD0.xy;
			    u_xlat16_3.xy = texture(_TaaMotionVectorTex, u_xlat3.xy, _GlobalMipBias.x).xy;
			    u_xlat19.xy = (-u_xlat16_3.xy) + vs_TEXCOORD0.xy;
			    u_xlat16_4.xyz = texture(_TaaAccumulationTex, u_xlat19.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_5.xyz = texture(_BlitTexture, u_xlat2.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat2.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_2.xyz, u_xlat16_6.xyz);
			    u_xlat16_7.xyz = max(u_xlat16_2.xyz, u_xlat16_6.xyz);
			    u_xlat16_7.xyz = max(u_xlat16_5.xyz, u_xlat16_7.xyz);
			    u_xlat16_1.xyz = min(u_xlat16_1.xyz, u_xlat16_5.xyz);
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_1.xyz, u_xlat16_2.xyz);
			    u_xlat16_7.xyz = max(u_xlat16_7.xyz, u_xlat16_2.xyz);
			    u_xlat16_7.xyz = max(u_xlat16_0.xyz, u_xlat16_7.xyz);
			    u_xlat16_1.xyz = min(u_xlat16_0.xyz, u_xlat16_1.xyz);
			    u_xlat16_1.xyz = max(u_xlat16_1.xyz, u_xlat16_4.xyz);
			    u_xlat16_1.xyz = min(u_xlat16_7.xyz, u_xlat16_1.xyz);
			    u_xlat16_25 = dot(u_xlat16_1.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat0.x = u_xlat16_25 + 1.0;
			    u_xlat0.x = float(1.0) / float(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat16_1.xyz;
			    u_xlat16_1.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat24 = u_xlat16_1.x + 1.0;
			    u_xlat24 = float(1.0) / float(u_xlat24);
			    u_xlat2.xyz = u_xlat16_6.xyz * vec3(u_xlat24) + (-u_xlat0.xyz);
			    u_xlat19.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat3.xy = (-u_xlat16_3.xy) + u_xlat19.xy;
			    u_xlatb3.xy = lessThan(vec4(0.5, 0.5, 0.0, 0.0), abs(u_xlat3.xyxx)).xy;
			    u_xlatb24 = u_xlatb3.y || u_xlatb3.x;
			    u_xlat16_1.x = (u_xlatb24) ? 1.0 : _TaaFrameInfluence;
			    u_xlat0.xyz = u_xlat16_1.xxx * u_xlat2.xyz + u_xlat0.xyz;
			    u_xlat16_1.x = dot(u_xlat0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat24 = (-u_xlat16_1.x) + 1.0;
			    u_xlat24 = float(1.0) / float(u_xlat24);
			    u_xlat0.xyz = vec3(u_xlat24) * u_xlat0.xyz;
			    SV_Target0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
			    SV_Target0.w = 1.0;
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
			uniform 	vec2 _GlobalMipBias;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(0) uniform TemporalAAData {
			#endif
				UNITY_UNIFORM vec4                _BlitTexture_TexelSize;
				UNITY_UNIFORM vec4                _TaaMotionVectorTex_TexelSize;
				UNITY_UNIFORM vec4 Xhlslcc_UnusedX_TaaAccumulationTex_TexelSize;
				UNITY_UNIFORM float Xhlslcc_UnusedX_TaaFilterWeights[9];
				UNITY_UNIFORM mediump float                _TaaFrameInfluence;
				UNITY_UNIFORM mediump float                _TaaVarianceClampScale;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(2) uniform mediump sampler2D _TaaMotionVectorTex;
			UNITY_LOCATION(3) uniform mediump sampler2D _TaaAccumulationTex;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			vec2 u_xlat3;
			mediump vec4 u_xlat16_3;
			bvec2 u_xlatb3;
			bool u_xlatb4;
			vec4 u_xlat5;
			vec2 u_xlat6;
			mediump vec2 u_xlat16_6;
			mediump vec3 u_xlat16_7;
			mediump vec3 u_xlat16_8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump vec3 u_xlat16_13;
			mediump vec3 u_xlat16_14;
			mediump vec3 u_xlat16_15;
			mediump vec3 u_xlat16_16;
			mediump vec3 u_xlat16_17;
			mediump vec3 u_xlat16_18;
			mediump vec3 u_xlat16_19;
			mediump vec2 u_xlat16_21;
			bool u_xlatb22;
			bool u_xlatb23;
			vec2 u_xlat24;
			mediump vec3 u_xlat16_24;
			bool u_xlatb24;
			mediump float u_xlat16_41;
			float u_xlat43;
			bool u_xlatb43;
			vec2 u_xlat46;
			float u_xlat60;
			bool u_xlatb60;
			mediump float u_xlat16_61;
			void main()
			{
			    u_xlat0.x = texture(_CameraDepthTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).x;
			    u_xlat16_1.x = min(u_xlat0.x, 1.0);
			    u_xlat0 = _BlitTexture_TexelSize.xyxy * vec4(1.0, 0.0, 0.0, 1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat2.x = texture(_CameraDepthTexture, u_xlat0.xy, _GlobalMipBias.x).x;
			    u_xlatb22 = u_xlat2.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat2.x);
			    u_xlat16_21.x = (u_xlatb22) ? 1.0 : 0.0;
			    u_xlat2 = _BlitTexture_TexelSize.xyxy * vec4(0.0, -1.0, -1.0, 0.0) + vs_TEXCOORD0.xyxy;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat2.xy, _GlobalMipBias.x).x;
			    u_xlatb23 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_21.x = (u_xlatb23) ? 0.0 : u_xlat16_21.x;
			    u_xlat16_21.y = (u_xlatb23) ? -1.0 : 0.0;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat2.zw, _GlobalMipBias.x).x;
			    u_xlatb23 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_21.xy = (bool(u_xlatb23)) ? vec2(-1.0, 0.0) : u_xlat16_21.xy;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlatb23 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_21.xy = (bool(u_xlatb23)) ? vec2(0.0, 1.0) : u_xlat16_21.xy;
			    u_xlat3.xy = vs_TEXCOORD0.xy + (-_BlitTexture_TexelSize.xy);
			    u_xlat43 = texture(_CameraDepthTexture, u_xlat3.xy, _GlobalMipBias.x).x;
			    u_xlat16_3.xyw = texture(_BlitTexture, u_xlat3.xy, _GlobalMipBias.x).xyz;
			    u_xlatb4 = u_xlat43<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat43);
			    u_xlat16_21.x = (u_xlatb4) ? -1.0 : u_xlat16_21.x;
			    u_xlat5 = _BlitTexture_TexelSize.xyxy * vec4(1.0, -1.0, -1.0, 1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat43 = texture(_CameraDepthTexture, u_xlat5.xy, _GlobalMipBias.x).x;
			    u_xlatb24 = u_xlat43<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat43);
			    u_xlat16_21.x = (u_xlatb24) ? 1.0 : u_xlat16_21.x;
			    u_xlatb43 = u_xlatb24 || u_xlatb4;
			    u_xlat16_41 = (u_xlatb43) ? -1.0 : u_xlat16_21.y;
			    u_xlat43 = texture(_CameraDepthTexture, u_xlat5.zw, _GlobalMipBias.x).x;
			    u_xlatb4 = u_xlat43<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat43);
			    u_xlat16_21.x = (u_xlatb4) ? -1.0 : u_xlat16_21.x;
			    u_xlat24.xy = vs_TEXCOORD0.xy + _BlitTexture_TexelSize.xy;
			    u_xlat43 = texture(_CameraDepthTexture, u_xlat24.xy, _GlobalMipBias.x).x;
			    u_xlat16_24.xyz = texture(_BlitTexture, u_xlat24.xy, _GlobalMipBias.x).xyz;
			    u_xlatb43 = u_xlat43<u_xlat16_1.x;
			    u_xlat16_1.x = (u_xlatb43) ? 1.0 : u_xlat16_21.x;
			    u_xlatb43 = u_xlatb43 || u_xlatb4;
			    u_xlat16_1.y = (u_xlatb43) ? 1.0 : u_xlat16_41;
			    u_xlat6.xy = _TaaMotionVectorTex_TexelSize.xy * u_xlat16_1.xy + vs_TEXCOORD0.xy;
			    u_xlat16_6.xy = texture(_TaaMotionVectorTex, u_xlat6.xy, _GlobalMipBias.x).xy;
			    u_xlat46.xy = (-u_xlat16_6.xy) + vs_TEXCOORD0.xy;
			    u_xlat16_7.xyz = texture(_TaaAccumulationTex, u_xlat46.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.x = dot(u_xlat16_7.xz, vec2(0.5, -0.5));
			    u_xlat16_1.y = u_xlat16_1.x + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_7.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_1.x = dot(u_xlat16_7.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_1.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_24.xz, vec2(0.5, -0.5));
			    u_xlat16_8.y = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_24.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_8.x = dot(u_xlat16_24.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_8.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_3.xw, vec2(0.5, -0.5));
			    u_xlat16_9.y = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_3.xwy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_9.x = dot(u_xlat16_3.xwy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_9.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_61 = dot(u_xlat16_3.xz, vec2(0.5, -0.5));
			    u_xlat16_10.y = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_3.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_10.x = dot(u_xlat16_3.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_10.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_0.xz, vec2(0.5, -0.5));
			    u_xlat16_11.y = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_11.x = dot(u_xlat16_0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_11.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat2.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat2.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_61 = dot(u_xlat16_0.xz, vec2(0.5, -0.5));
			    u_xlat16_12.y = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_12.x = dot(u_xlat16_0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_12.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_2.xz, vec2(0.5, -0.5));
			    u_xlat16_13.y = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_2.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_13.x = dot(u_xlat16_2.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_13.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_14.xyz = u_xlat16_13.xyz * u_xlat16_13.xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_61 = dot(u_xlat16_0.xz, vec2(0.5, -0.5));
			    u_xlat16_15.y = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_15.x = dot(u_xlat16_0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_15.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_14.xyz = u_xlat16_15.xyz * u_xlat16_15.xyz + u_xlat16_14.xyz;
			    u_xlat16_14.xyz = u_xlat16_12.xyz * u_xlat16_12.xyz + u_xlat16_14.xyz;
			    u_xlat16_14.xyz = u_xlat16_11.xyz * u_xlat16_11.xyz + u_xlat16_14.xyz;
			    u_xlat16_14.xyz = u_xlat16_10.xyz * u_xlat16_10.xyz + u_xlat16_14.xyz;
			    u_xlat16_14.xyz = u_xlat16_9.xyz * u_xlat16_9.xyz + u_xlat16_14.xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat5.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat5.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_61 = dot(u_xlat16_0.xz, vec2(0.5, -0.5));
			    u_xlat16_16.y = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_16.x = dot(u_xlat16_0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_16.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_14.xyz = u_xlat16_16.xyz * u_xlat16_16.xyz + u_xlat16_14.xyz;
			    u_xlat16_61 = dot(u_xlat16_2.xz, vec2(0.5, -0.5));
			    u_xlat16_17.y = u_xlat16_61 + 0.501960814;
			    u_xlat16_61 = dot(u_xlat16_2.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_17.x = dot(u_xlat16_2.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_17.z = u_xlat16_61 + 0.501960814;
			    u_xlat16_14.xyz = u_xlat16_17.xyz * u_xlat16_17.xyz + u_xlat16_14.xyz;
			    u_xlat16_14.xyz = u_xlat16_8.xyz * u_xlat16_8.xyz + u_xlat16_14.xyz;
			    u_xlat16_18.xyz = u_xlat16_13.xyz + u_xlat16_15.xyz;
			    u_xlat16_18.xyz = u_xlat16_12.xyz + u_xlat16_18.xyz;
			    u_xlat16_18.xyz = u_xlat16_11.xyz + u_xlat16_18.xyz;
			    u_xlat16_18.xyz = u_xlat16_10.xyz + u_xlat16_18.xyz;
			    u_xlat16_18.xyz = u_xlat16_9.xyz + u_xlat16_18.xyz;
			    u_xlat16_18.xyz = u_xlat16_16.xyz + u_xlat16_18.xyz;
			    u_xlat16_18.xyz = u_xlat16_17.xyz + u_xlat16_18.xyz;
			    u_xlat16_18.xyz = u_xlat16_8.xyz + u_xlat16_18.xyz;
			    u_xlat16_19.xyz = u_xlat16_18.xyz * vec3(0.111111112, 0.111111112, 0.111111112);
			    u_xlat16_19.xyz = u_xlat16_19.xyz * u_xlat16_19.xyz;
			    u_xlat16_14.xyz = u_xlat16_14.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + (-u_xlat16_19.xyz);
			    u_xlat16_14.xyz = sqrt(abs(u_xlat16_14.xyz));
			    u_xlat16_14.xyz = u_xlat16_14.xyz * vec3(vec3(_TaaVarianceClampScale, _TaaVarianceClampScale, _TaaVarianceClampScale));
			    u_xlat16_19.xyz = u_xlat16_18.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + (-u_xlat16_14.xyz);
			    u_xlat16_14.xyz = u_xlat16_18.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + u_xlat16_14.xyz;
			    u_xlat16_18.xyz = min(u_xlat16_13.xyz, u_xlat16_15.xyz);
			    u_xlat16_13.xyz = max(u_xlat16_13.xyz, u_xlat16_15.xyz);
			    u_xlat16_13.xyz = max(u_xlat16_12.xyz, u_xlat16_13.xyz);
			    u_xlat16_12.xyz = min(u_xlat16_12.xyz, u_xlat16_18.xyz);
			    u_xlat16_12.xyz = min(u_xlat16_11.xyz, u_xlat16_12.xyz);
			    u_xlat16_11.xyz = max(u_xlat16_11.xyz, u_xlat16_13.xyz);
			    u_xlat16_11.xyz = max(u_xlat16_10.xyz, u_xlat16_11.xyz);
			    u_xlat16_10.xyz = min(u_xlat16_10.xyz, u_xlat16_12.xyz);
			    u_xlat16_10.xyz = min(u_xlat16_9.xyz, u_xlat16_10.xyz);
			    u_xlat16_9.xyz = max(u_xlat16_9.xyz, u_xlat16_11.xyz);
			    u_xlat16_9.xyz = max(u_xlat16_9.xyz, u_xlat16_16.xyz);
			    u_xlat16_10.xyz = min(u_xlat16_10.xyz, u_xlat16_16.xyz);
			    u_xlat16_10.xyz = min(u_xlat16_10.xyz, u_xlat16_17.xyz);
			    u_xlat16_9.xyz = max(u_xlat16_9.xyz, u_xlat16_17.xyz);
			    u_xlat16_9.xyz = max(u_xlat16_8.xyz, u_xlat16_9.xyz);
			    u_xlat16_8.xyz = min(u_xlat16_8.xyz, u_xlat16_10.xyz);
			    u_xlat16_8.xyz = max(u_xlat16_19.xyz, u_xlat16_8.xyz);
			    u_xlat16_1.xyz = max(u_xlat16_1.xyz, u_xlat16_8.xyz);
			    u_xlat16_8.xyz = min(u_xlat16_14.xyz, u_xlat16_9.xyz);
			    u_xlat16_1.xyz = min(u_xlat16_1.xyz, u_xlat16_8.xyz);
			    u_xlat0.x = u_xlat16_1.x + 1.0;
			    u_xlat0.x = float(1.0) / float(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat16_1.xyz;
			    u_xlat60 = u_xlat16_15.x + 1.0;
			    u_xlat60 = float(1.0) / float(u_xlat60);
			    u_xlat2.xyz = u_xlat16_15.xyz * vec3(u_xlat60) + (-u_xlat0.xyz);
			    u_xlat3.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat3.xy = (-u_xlat16_6.xy) + u_xlat3.xy;
			    u_xlatb3.xy = lessThan(vec4(0.5, 0.5, 0.0, 0.0), abs(u_xlat3.xyxx)).xy;
			    u_xlatb60 = u_xlatb3.y || u_xlatb3.x;
			    u_xlat16_1.x = (u_xlatb60) ? 1.0 : _TaaFrameInfluence;
			    u_xlat0.xyz = u_xlat16_1.xxx * u_xlat2.xyz + u_xlat0.xyz;
			    u_xlat60 = (-u_xlat0.x) + 1.0;
			    u_xlat60 = float(1.0) / float(u_xlat60);
			    u_xlat16_1.xy = u_xlat0.zy * vec2(u_xlat60) + vec2(-0.501960814, -0.501960814);
			    u_xlat16_41 = u_xlat0.x * u_xlat60 + (-u_xlat16_1.y);
			    u_xlat16_0.yz = u_xlat0.xx * vec2(u_xlat60) + u_xlat16_1.yx;
			    u_xlat16_0.w = (-u_xlat16_1.x) + u_xlat16_41;
			    u_xlat16_0.x = (-u_xlat16_1.x) + u_xlat16_0.y;
			    SV_Target0.xyz = max(u_xlat16_0.xzw, vec3(0.0, 0.0, 0.0));
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
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(0) uniform TemporalAAData {
			#endif
				UNITY_UNIFORM vec4                _BlitTexture_TexelSize;
				UNITY_UNIFORM vec4                _TaaMotionVectorTex_TexelSize;
				UNITY_UNIFORM vec4                _TaaAccumulationTex_TexelSize;
				UNITY_UNIFORM float Xhlslcc_UnusedX_TaaFilterWeights[9];
				UNITY_UNIFORM mediump float                _TaaFrameInfluence;
				UNITY_UNIFORM mediump float                _TaaVarianceClampScale;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(2) uniform mediump sampler2D _TaaMotionVectorTex;
			UNITY_LOCATION(3) uniform mediump sampler2D _TaaAccumulationTex;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			vec4 u_xlat1;
			mediump vec2 u_xlat16_1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			vec2 u_xlat3;
			mediump vec4 u_xlat16_3;
			bvec2 u_xlatb3;
			bool u_xlatb4;
			vec4 u_xlat5;
			vec2 u_xlat6;
			mediump vec2 u_xlat16_6;
			vec4 u_xlat7;
			mediump vec3 u_xlat16_7;
			mediump vec3 u_xlat16_8;
			mediump vec4 u_xlat16_9;
			mediump vec4 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump vec3 u_xlat16_13;
			mediump vec3 u_xlat16_14;
			mediump vec3 u_xlat16_15;
			mediump vec3 u_xlat16_16;
			mediump vec3 u_xlat16_17;
			mediump vec3 u_xlat16_18;
			mediump vec3 u_xlat16_19;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_21;
			mediump vec3 u_xlat16_22;
			mediump vec2 u_xlat16_24;
			bool u_xlatb25;
			bool u_xlatb26;
			vec2 u_xlat27;
			mediump vec3 u_xlat16_27;
			bool u_xlatb27;
			mediump vec3 u_xlat16_31;
			mediump float u_xlat16_47;
			float u_xlat49;
			bool u_xlatb49;
			vec2 u_xlat52;
			mediump vec2 u_xlat16_54;
			float u_xlat69;
			bool u_xlatb69;
			mediump float u_xlat16_77;
			mediump float u_xlat16_78;
			void main()
			{
			    u_xlat0.x = texture(_CameraDepthTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).x;
			    u_xlat16_1.x = min(u_xlat0.x, 1.0);
			    u_xlat0 = _BlitTexture_TexelSize.xyxy * vec4(1.0, 0.0, 0.0, 1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat2.x = texture(_CameraDepthTexture, u_xlat0.xy, _GlobalMipBias.x).x;
			    u_xlatb25 = u_xlat2.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat2.x);
			    u_xlat16_24.x = (u_xlatb25) ? 1.0 : 0.0;
			    u_xlat2 = _BlitTexture_TexelSize.xyxy * vec4(0.0, -1.0, -1.0, 0.0) + vs_TEXCOORD0.xyxy;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat2.xy, _GlobalMipBias.x).x;
			    u_xlatb26 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_24.x = (u_xlatb26) ? 0.0 : u_xlat16_24.x;
			    u_xlat16_24.y = (u_xlatb26) ? -1.0 : 0.0;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat2.zw, _GlobalMipBias.x).x;
			    u_xlatb26 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_24.xy = (bool(u_xlatb26)) ? vec2(-1.0, 0.0) : u_xlat16_24.xy;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlatb26 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_24.xy = (bool(u_xlatb26)) ? vec2(0.0, 1.0) : u_xlat16_24.xy;
			    u_xlat3.xy = vs_TEXCOORD0.xy + (-_BlitTexture_TexelSize.xy);
			    u_xlat49 = texture(_CameraDepthTexture, u_xlat3.xy, _GlobalMipBias.x).x;
			    u_xlat16_3.xyw = texture(_BlitTexture, u_xlat3.xy, _GlobalMipBias.x).xyz;
			    u_xlatb4 = u_xlat49<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat49);
			    u_xlat16_24.x = (u_xlatb4) ? -1.0 : u_xlat16_24.x;
			    u_xlat5 = _BlitTexture_TexelSize.xyxy * vec4(1.0, -1.0, -1.0, 1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat49 = texture(_CameraDepthTexture, u_xlat5.xy, _GlobalMipBias.x).x;
			    u_xlatb27 = u_xlat49<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat49);
			    u_xlat16_24.x = (u_xlatb27) ? 1.0 : u_xlat16_24.x;
			    u_xlatb49 = u_xlatb27 || u_xlatb4;
			    u_xlat16_47 = (u_xlatb49) ? -1.0 : u_xlat16_24.y;
			    u_xlat49 = texture(_CameraDepthTexture, u_xlat5.zw, _GlobalMipBias.x).x;
			    u_xlatb4 = u_xlat49<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat49);
			    u_xlat16_24.x = (u_xlatb4) ? -1.0 : u_xlat16_24.x;
			    u_xlat27.xy = vs_TEXCOORD0.xy + _BlitTexture_TexelSize.xy;
			    u_xlat49 = texture(_CameraDepthTexture, u_xlat27.xy, _GlobalMipBias.x).x;
			    u_xlat16_27.xyz = texture(_BlitTexture, u_xlat27.xy, _GlobalMipBias.x).xyz;
			    u_xlatb49 = u_xlat49<u_xlat16_1.x;
			    u_xlat16_1.x = (u_xlatb49) ? 1.0 : u_xlat16_24.x;
			    u_xlatb49 = u_xlatb49 || u_xlatb4;
			    u_xlat16_1.y = (u_xlatb49) ? 1.0 : u_xlat16_47;
			    u_xlat6.xy = _TaaMotionVectorTex_TexelSize.xy * u_xlat16_1.xy + vs_TEXCOORD0.xy;
			    u_xlat16_6.xy = texture(_TaaMotionVectorTex, u_xlat6.xy, _GlobalMipBias.x).xy;
			    u_xlat52.xy = (-u_xlat16_6.xy) + vs_TEXCOORD0.xy;
			    u_xlat7.xy = u_xlat52.xy * _TaaAccumulationTex_TexelSize.zw + vec2(-0.5, -0.5);
			    u_xlat7.xy = floor(u_xlat7.xy);
			    u_xlat1 = u_xlat7.xyxy + vec4(0.5, 0.5, -0.5, -0.5);
			    u_xlat7.xy = u_xlat7.xy + vec2(2.5, 2.5);
			    u_xlat7.xy = u_xlat7.xy * _TaaAccumulationTex_TexelSize.xy;
			    u_xlat52.xy = u_xlat52.xy * _TaaAccumulationTex_TexelSize.zw + (-u_xlat1.xy);
			    u_xlat16_8.xy = u_xlat52.xy * u_xlat52.xy;
			    u_xlat16_9 = u_xlat52.xyxy * u_xlat16_8.xyxy;
			    u_xlat16_54.xy = u_xlat16_9.wz * vec2(-0.5, -0.5) + u_xlat16_8.yx;
			    u_xlat16_54.xy = (-u_xlat52.yx) * vec2(0.5, 0.5) + u_xlat16_54.xy;
			    u_xlat16_9.xy = u_xlat16_9.xy * vec2(-1.5, -1.5);
			    u_xlat16_9.xy = u_xlat16_8.xy * vec2(2.0, 2.0) + u_xlat16_9.xy;
			    u_xlat16_10 = u_xlat16_8.xyxy * vec4(2.5, 2.5, 0.5, 0.5);
			    u_xlat16_8.xy = u_xlat52.xy * vec2(0.5, 0.5) + u_xlat16_9.xy;
			    u_xlat16_9.xy = u_xlat16_9.wz * vec2(1.5, 1.5) + (-u_xlat16_10.yx);
			    u_xlat16_9.zw = u_xlat16_9.zw * vec2(0.5, 0.5) + (-u_xlat16_10.zw);
			    u_xlat16_9.xy = u_xlat16_9.xy + vec2(1.0, 1.0);
			    u_xlat16_9.xy = u_xlat16_8.yx + u_xlat16_9.xy;
			    u_xlat16_8.xy = u_xlat16_8.xy / u_xlat16_9.yx;
			    u_xlat52.xy = u_xlat1.xy + u_xlat16_8.xy;
			    u_xlat1.zw = u_xlat1.zw * _TaaAccumulationTex_TexelSize.xy;
			    u_xlat1.xy = u_xlat52.xy * _TaaAccumulationTex_TexelSize.xy;
			    u_xlat16_8.xy = u_xlat16_54.xy * u_xlat16_9.yx;
			    u_xlat16_11.xyz = texture(_TaaAccumulationTex, u_xlat1.zy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = texture(_TaaAccumulationTex, u_xlat1.xw, _GlobalMipBias.x).xyz;
			    u_xlat16_54.x = dot(u_xlat16_11.xz, vec2(0.5, -0.5));
			    u_xlat16_10.y = u_xlat16_54.x + 0.501960814;
			    u_xlat16_54.x = dot(u_xlat16_11.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_10.x = dot(u_xlat16_11.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_10.z = u_xlat16_54.x + 0.501960814;
			    u_xlat16_10.xyz = u_xlat16_8.yyy * u_xlat16_10.xyz;
			    u_xlat16_54.x = dot(u_xlat16_12.xz, vec2(0.5, -0.5));
			    u_xlat16_13.y = u_xlat16_54.x + 0.501960814;
			    u_xlat16_54.x = dot(u_xlat16_12.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_13.x = dot(u_xlat16_12.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_13.z = u_xlat16_54.x + 0.501960814;
			    u_xlat16_10.xyz = u_xlat16_13.xyz * u_xlat16_8.xxx + u_xlat16_10.xyz;
			    u_xlat16_8.x = u_xlat16_8.y + u_xlat16_8.x;
			    u_xlat16_8.x = u_xlat16_9.y * u_xlat16_9.x + u_xlat16_8.x;
			    u_xlat16_8.x = u_xlat16_9.z * u_xlat16_9.x + u_xlat16_8.x;
			    u_xlat16_8.x = u_xlat16_9.w * u_xlat16_9.y + u_xlat16_8.x;
			    u_xlat16_31.xyz = u_xlat16_9.xyx * u_xlat16_9.zwy;
			    u_xlat16_8.x = float(1.0) / float(u_xlat16_8.x);
			    u_xlat16_11.xyz = texture(_TaaAccumulationTex, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat7.zw = u_xlat1.yx;
			    u_xlat16_9.x = dot(u_xlat16_11.xz, vec2(0.5, -0.5));
			    u_xlat16_9.y = u_xlat16_9.x + 0.501960814;
			    u_xlat16_78 = dot(u_xlat16_11.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_9.x = dot(u_xlat16_11.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_9.z = u_xlat16_78 + 0.501960814;
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_31.zzz + u_xlat16_10.xyz;
			    u_xlat16_11.xyz = texture(_TaaAccumulationTex, u_xlat7.xz, _GlobalMipBias.x).xyz;
			    u_xlat16_7.xyz = texture(_TaaAccumulationTex, u_xlat7.wy, _GlobalMipBias.x).xyz;
			    u_xlat16_77 = dot(u_xlat16_11.xz, vec2(0.5, -0.5));
			    u_xlat16_10.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_11.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_10.x = dot(u_xlat16_11.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_10.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_9.xyz = u_xlat16_10.xyz * u_xlat16_31.xxx + u_xlat16_9.xyz;
			    u_xlat16_31.x = dot(u_xlat16_7.xz, vec2(0.5, -0.5));
			    u_xlat16_10.y = u_xlat16_31.x + 0.501960814;
			    u_xlat16_31.x = dot(u_xlat16_7.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_10.x = dot(u_xlat16_7.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_10.z = u_xlat16_31.x + 0.501960814;
			    u_xlat16_31.xyz = u_xlat16_10.xyz * u_xlat16_31.yyy + u_xlat16_9.xyz;
			    u_xlat16_8.xyz = u_xlat16_8.xxx * u_xlat16_31.xyz;
			    u_xlat16_77 = dot(u_xlat16_27.xz, vec2(0.5, -0.5));
			    u_xlat16_9.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_27.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_9.x = dot(u_xlat16_27.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_9.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_3.xw, vec2(0.5, -0.5));
			    u_xlat16_10.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_3.xwy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_10.x = dot(u_xlat16_3.xwy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_10.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_77 = dot(u_xlat16_3.xz, vec2(0.5, -0.5));
			    u_xlat16_13.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_3.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_13.x = dot(u_xlat16_3.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_13.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_0.xz, vec2(0.5, -0.5));
			    u_xlat16_14.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_14.x = dot(u_xlat16_0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_14.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat2.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat2.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_77 = dot(u_xlat16_0.xz, vec2(0.5, -0.5));
			    u_xlat16_15.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_15.x = dot(u_xlat16_0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_15.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_2.xz, vec2(0.5, -0.5));
			    u_xlat16_16.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_2.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_16.x = dot(u_xlat16_2.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_16.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_17.xyz = u_xlat16_16.xyz * u_xlat16_16.xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_77 = dot(u_xlat16_0.xz, vec2(0.5, -0.5));
			    u_xlat16_18.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_18.x = dot(u_xlat16_0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_18.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_17.xyz = u_xlat16_18.xyz * u_xlat16_18.xyz + u_xlat16_17.xyz;
			    u_xlat16_17.xyz = u_xlat16_15.xyz * u_xlat16_15.xyz + u_xlat16_17.xyz;
			    u_xlat16_17.xyz = u_xlat16_14.xyz * u_xlat16_14.xyz + u_xlat16_17.xyz;
			    u_xlat16_17.xyz = u_xlat16_13.xyz * u_xlat16_13.xyz + u_xlat16_17.xyz;
			    u_xlat16_17.xyz = u_xlat16_10.xyz * u_xlat16_10.xyz + u_xlat16_17.xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat5.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat5.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_77 = dot(u_xlat16_0.xz, vec2(0.5, -0.5));
			    u_xlat16_19.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_19.x = dot(u_xlat16_0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_19.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_17.xyz = u_xlat16_19.xyz * u_xlat16_19.xyz + u_xlat16_17.xyz;
			    u_xlat16_77 = dot(u_xlat16_2.xz, vec2(0.5, -0.5));
			    u_xlat16_20.y = u_xlat16_77 + 0.501960814;
			    u_xlat16_77 = dot(u_xlat16_2.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_20.x = dot(u_xlat16_2.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_20.z = u_xlat16_77 + 0.501960814;
			    u_xlat16_17.xyz = u_xlat16_20.xyz * u_xlat16_20.xyz + u_xlat16_17.xyz;
			    u_xlat16_17.xyz = u_xlat16_9.xyz * u_xlat16_9.xyz + u_xlat16_17.xyz;
			    u_xlat16_21.xyz = u_xlat16_16.xyz + u_xlat16_18.xyz;
			    u_xlat16_21.xyz = u_xlat16_15.xyz + u_xlat16_21.xyz;
			    u_xlat16_21.xyz = u_xlat16_14.xyz + u_xlat16_21.xyz;
			    u_xlat16_21.xyz = u_xlat16_13.xyz + u_xlat16_21.xyz;
			    u_xlat16_21.xyz = u_xlat16_10.xyz + u_xlat16_21.xyz;
			    u_xlat16_21.xyz = u_xlat16_19.xyz + u_xlat16_21.xyz;
			    u_xlat16_21.xyz = u_xlat16_20.xyz + u_xlat16_21.xyz;
			    u_xlat16_21.xyz = u_xlat16_9.xyz + u_xlat16_21.xyz;
			    u_xlat16_22.xyz = u_xlat16_21.xyz * vec3(0.111111112, 0.111111112, 0.111111112);
			    u_xlat16_22.xyz = u_xlat16_22.xyz * u_xlat16_22.xyz;
			    u_xlat16_17.xyz = u_xlat16_17.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + (-u_xlat16_22.xyz);
			    u_xlat16_17.xyz = sqrt(abs(u_xlat16_17.xyz));
			    u_xlat16_17.xyz = u_xlat16_17.xyz * vec3(vec3(_TaaVarianceClampScale, _TaaVarianceClampScale, _TaaVarianceClampScale));
			    u_xlat16_22.xyz = u_xlat16_21.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + (-u_xlat16_17.xyz);
			    u_xlat16_17.xyz = u_xlat16_21.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + u_xlat16_17.xyz;
			    u_xlat16_21.xyz = min(u_xlat16_16.xyz, u_xlat16_18.xyz);
			    u_xlat16_16.xyz = max(u_xlat16_16.xyz, u_xlat16_18.xyz);
			    u_xlat16_16.xyz = max(u_xlat16_15.xyz, u_xlat16_16.xyz);
			    u_xlat16_15.xyz = min(u_xlat16_15.xyz, u_xlat16_21.xyz);
			    u_xlat16_15.xyz = min(u_xlat16_14.xyz, u_xlat16_15.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_14.xyz, u_xlat16_16.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_13.xyz, u_xlat16_14.xyz);
			    u_xlat16_13.xyz = min(u_xlat16_13.xyz, u_xlat16_15.xyz);
			    u_xlat16_13.xyz = min(u_xlat16_10.xyz, u_xlat16_13.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_14.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_19.xyz);
			    u_xlat16_13.xyz = min(u_xlat16_13.xyz, u_xlat16_19.xyz);
			    u_xlat16_13.xyz = min(u_xlat16_13.xyz, u_xlat16_20.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_20.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_9.xyz, u_xlat16_10.xyz);
			    u_xlat16_9.xyz = min(u_xlat16_9.xyz, u_xlat16_13.xyz);
			    u_xlat16_9.xyz = max(u_xlat16_22.xyz, u_xlat16_9.xyz);
			    u_xlat16_8.xyz = max(u_xlat16_8.xyz, u_xlat16_9.xyz);
			    u_xlat16_9.xyz = min(u_xlat16_17.xyz, u_xlat16_10.xyz);
			    u_xlat16_8.xyz = min(u_xlat16_8.xyz, u_xlat16_9.xyz);
			    u_xlat0.x = u_xlat16_8.x + 1.0;
			    u_xlat0.x = float(1.0) / float(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat16_8.xyz;
			    u_xlat69 = u_xlat16_18.x + 1.0;
			    u_xlat69 = float(1.0) / float(u_xlat69);
			    u_xlat2.xyz = u_xlat16_18.xyz * vec3(u_xlat69) + (-u_xlat0.xyz);
			    u_xlat3.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat3.xy = (-u_xlat16_6.xy) + u_xlat3.xy;
			    u_xlatb3.xy = lessThan(vec4(0.5, 0.5, 0.0, 0.0), abs(u_xlat3.xyxx)).xy;
			    u_xlatb69 = u_xlatb3.y || u_xlatb3.x;
			    u_xlat16_8.x = (u_xlatb69) ? 1.0 : _TaaFrameInfluence;
			    u_xlat0.xyz = u_xlat16_8.xxx * u_xlat2.xyz + u_xlat0.xyz;
			    u_xlat69 = (-u_xlat0.x) + 1.0;
			    u_xlat69 = float(1.0) / float(u_xlat69);
			    u_xlat16_8.xy = u_xlat0.zy * vec2(u_xlat69) + vec2(-0.501960814, -0.501960814);
			    u_xlat16_54.x = u_xlat0.x * u_xlat69 + (-u_xlat16_8.y);
			    u_xlat16_0.yz = u_xlat0.xx * vec2(u_xlat69) + u_xlat16_8.yx;
			    u_xlat16_0.w = (-u_xlat16_8.x) + u_xlat16_54.x;
			    u_xlat16_0.x = (-u_xlat16_8.x) + u_xlat16_0.y;
			    SV_Target0.xyz = max(u_xlat16_0.xzw, vec3(0.0, 0.0, 0.0));
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
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(0) uniform TemporalAAData {
			#endif
				UNITY_UNIFORM vec4                _BlitTexture_TexelSize;
				UNITY_UNIFORM vec4                _TaaMotionVectorTex_TexelSize;
				UNITY_UNIFORM vec4                _TaaAccumulationTex_TexelSize;
				UNITY_UNIFORM float                _TaaFilterWeights[9];
				UNITY_UNIFORM mediump float                _TaaFrameInfluence;
				UNITY_UNIFORM mediump float                _TaaVarianceClampScale;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(2) uniform mediump sampler2D _TaaMotionVectorTex;
			UNITY_LOCATION(3) uniform mediump sampler2D _TaaAccumulationTex;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec4 u_xlat16_0;
			bvec2 u_xlatb0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			vec3 u_xlat3;
			mediump vec4 u_xlat16_3;
			vec4 u_xlat4;
			mediump vec4 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			vec4 u_xlat7;
			mediump vec3 u_xlat16_8;
			mediump vec3 u_xlat16_9;
			vec2 u_xlat10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump vec3 u_xlat16_13;
			mediump vec3 u_xlat16_14;
			mediump vec3 u_xlat16_15;
			mediump vec3 u_xlat16_16;
			mediump vec4 u_xlat16_17;
			mediump vec3 u_xlat16_18;
			mediump vec3 u_xlat16_19;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_21;
			mediump vec3 u_xlat16_22;
			mediump vec3 u_xlat16_23;
			mediump vec3 u_xlat16_24;
			float u_xlat25;
			bool u_xlatb25;
			vec2 u_xlat50;
			bool u_xlatb50;
			mediump float u_xlat16_63;
			mediump vec2 u_xlat16_67;
			mediump vec2 u_xlat16_68;
			float u_xlat75;
			float u_xlat77;
			mediump float u_xlat16_88;
			mediump float u_xlat16_89;
			mediump float u_xlat16_90;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat1 = _BlitTexture_TexelSize.xyxy * vec4(0.0, 1.0, 1.0, 0.0) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat3.xyz = u_xlat16_2.xyz * vec3(_TaaFilterWeights[1]);
			    u_xlat0.xyz = vec3(_TaaFilterWeights[0]) * u_xlat16_0.xyz + u_xlat3.xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = vec3(_TaaFilterWeights[2]) * u_xlat16_3.xyz + u_xlat0.xyz;
			    u_xlat4 = _BlitTexture_TexelSize.xyxy * vec4(-1.0, 0.0, 0.0, -1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat16_5.xyz = texture(_BlitTexture, u_xlat4.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = vec3(_TaaFilterWeights[3]) * u_xlat16_5.xyz + u_xlat0.xyz;
			    u_xlat16_6.xyz = texture(_BlitTexture, u_xlat4.zw, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = vec3(_TaaFilterWeights[4]) * u_xlat16_6.xyz + u_xlat0.xyz;
			    u_xlat7 = _BlitTexture_TexelSize.xyxy * vec4(-1.0, 1.0, 1.0, -1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat16_8.xyz = texture(_BlitTexture, u_xlat7.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = vec3(_TaaFilterWeights[5]) * u_xlat16_8.xyz + u_xlat0.xyz;
			    u_xlat16_9.xyz = texture(_BlitTexture, u_xlat7.zw, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = vec3(_TaaFilterWeights[6]) * u_xlat16_9.xyz + u_xlat0.xyz;
			    u_xlat10.xy = vs_TEXCOORD0.xy + _BlitTexture_TexelSize.xy;
			    u_xlat16_11.xyz = texture(_BlitTexture, u_xlat10.xy, _GlobalMipBias.x).xyz;
			    u_xlat75 = texture(_CameraDepthTexture, u_xlat10.xy, _GlobalMipBias.x).x;
			    u_xlat0.xyz = vec3(_TaaFilterWeights[7]) * u_xlat16_11.xyz + u_xlat0.xyz;
			    u_xlat10.xy = vs_TEXCOORD0.xy + (-_BlitTexture_TexelSize.xy);
			    u_xlat16_12.xyz = texture(_BlitTexture, u_xlat10.xy, _GlobalMipBias.x).xyz;
			    u_xlat77 = texture(_CameraDepthTexture, u_xlat10.xy, _GlobalMipBias.x).x;
			    u_xlat0.xyz = vec3(_TaaFilterWeights[8]) * u_xlat16_12.xyz + u_xlat0.xyz;
			    u_xlat16_13.x = dot(u_xlat0.xz, vec2(0.5, -0.5));
			    u_xlat16_13.y = u_xlat16_13.x + 0.501960814;
			    u_xlat16_88 = dot(u_xlat0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_13.x = dot(u_xlat0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_13.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_6.xz, vec2(0.5, -0.5));
			    u_xlat16_14.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_6.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_14.x = dot(u_xlat16_6.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_14.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_15.xyz = u_xlat16_14.xyz * u_xlat16_14.xyz;
			    u_xlat16_15.xyz = u_xlat16_13.xyz * u_xlat16_13.xyz + u_xlat16_15.xyz;
			    u_xlat16_88 = dot(u_xlat16_5.xz, vec2(0.5, -0.5));
			    u_xlat16_16.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_5.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_16.x = dot(u_xlat16_5.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_16.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_15.xyz = u_xlat16_16.xyz * u_xlat16_16.xyz + u_xlat16_15.xyz;
			    u_xlat16_88 = dot(u_xlat16_3.xz, vec2(0.5, -0.5));
			    u_xlat16_17.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_3.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_17.x = dot(u_xlat16_3.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_17.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_15.xyz = u_xlat16_17.xyz * u_xlat16_17.xyz + u_xlat16_15.xyz;
			    u_xlat16_88 = dot(u_xlat16_2.xz, vec2(0.5, -0.5));
			    u_xlat16_18.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_2.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_18.x = dot(u_xlat16_2.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_18.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_15.xyz = u_xlat16_18.xyz * u_xlat16_18.xyz + u_xlat16_15.xyz;
			    u_xlat16_88 = dot(u_xlat16_12.xz, vec2(0.5, -0.5));
			    u_xlat16_19.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_12.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_19.x = dot(u_xlat16_12.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_19.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_15.xyz = u_xlat16_19.xyz * u_xlat16_19.xyz + u_xlat16_15.xyz;
			    u_xlat16_88 = dot(u_xlat16_9.xz, vec2(0.5, -0.5));
			    u_xlat16_20.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_9.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_20.x = dot(u_xlat16_9.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_20.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_15.xyz = u_xlat16_20.xyz * u_xlat16_20.xyz + u_xlat16_15.xyz;
			    u_xlat16_88 = dot(u_xlat16_8.xz, vec2(0.5, -0.5));
			    u_xlat16_21.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_8.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_21.x = dot(u_xlat16_8.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_21.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_15.xyz = u_xlat16_21.xyz * u_xlat16_21.xyz + u_xlat16_15.xyz;
			    u_xlat16_88 = dot(u_xlat16_11.xz, vec2(0.5, -0.5));
			    u_xlat16_22.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_11.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_22.x = dot(u_xlat16_11.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_22.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_15.xyz = u_xlat16_22.xyz * u_xlat16_22.xyz + u_xlat16_15.xyz;
			    u_xlat16_23.xyz = u_xlat16_13.xyz + u_xlat16_14.xyz;
			    u_xlat16_23.xyz = u_xlat16_16.xyz + u_xlat16_23.xyz;
			    u_xlat16_23.xyz = u_xlat16_17.xyz + u_xlat16_23.xyz;
			    u_xlat16_23.xyz = u_xlat16_18.xyz + u_xlat16_23.xyz;
			    u_xlat16_23.xyz = u_xlat16_19.xyz + u_xlat16_23.xyz;
			    u_xlat16_23.xyz = u_xlat16_20.xyz + u_xlat16_23.xyz;
			    u_xlat16_23.xyz = u_xlat16_21.xyz + u_xlat16_23.xyz;
			    u_xlat16_23.xyz = u_xlat16_22.xyz + u_xlat16_23.xyz;
			    u_xlat16_24.xyz = u_xlat16_23.xyz * vec3(0.111111112, 0.111111112, 0.111111112);
			    u_xlat16_24.xyz = u_xlat16_24.xyz * u_xlat16_24.xyz;
			    u_xlat16_15.xyz = u_xlat16_15.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + (-u_xlat16_24.xyz);
			    u_xlat16_15.xyz = sqrt(abs(u_xlat16_15.xyz));
			    u_xlat16_15.xyz = u_xlat16_15.xyz * vec3(vec3(_TaaVarianceClampScale, _TaaVarianceClampScale, _TaaVarianceClampScale));
			    u_xlat16_24.xyz = u_xlat16_23.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + (-u_xlat16_15.xyz);
			    u_xlat16_15.xyz = u_xlat16_23.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + u_xlat16_15.xyz;
			    u_xlat16_23.xyz = min(u_xlat16_13.xyz, u_xlat16_14.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_13.xyz, u_xlat16_14.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_14.xyz, u_xlat16_16.xyz);
			    u_xlat16_16.xyz = min(u_xlat16_16.xyz, u_xlat16_23.xyz);
			    u_xlat16_16.xyz = min(u_xlat16_16.xyz, u_xlat16_17.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_14.xyz, u_xlat16_17.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_14.xyz, u_xlat16_18.xyz);
			    u_xlat16_16.xyz = min(u_xlat16_16.xyz, u_xlat16_18.xyz);
			    u_xlat16_16.xyz = min(u_xlat16_16.xyz, u_xlat16_19.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_14.xyz, u_xlat16_19.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_14.xyz, u_xlat16_20.xyz);
			    u_xlat16_16.xyz = min(u_xlat16_16.xyz, u_xlat16_20.xyz);
			    u_xlat16_16.xyz = min(u_xlat16_16.xyz, u_xlat16_21.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_14.xyz, u_xlat16_21.xyz);
			    u_xlat16_14.xyz = max(u_xlat16_14.xyz, u_xlat16_22.xyz);
			    u_xlat16_16.xyz = min(u_xlat16_16.xyz, u_xlat16_22.xyz);
			    u_xlat16_16.xyz = max(u_xlat16_24.xyz, u_xlat16_16.xyz);
			    u_xlat16_14.xyz = min(u_xlat16_15.xyz, u_xlat16_14.xyz);
			    u_xlat16_15.xyz = u_xlat16_16.xyz + u_xlat16_14.xyz;
			    u_xlat16_14.xyz = (-u_xlat16_16.xyz) + u_xlat16_14.xyz;
			    u_xlat16_14.xyz = u_xlat16_14.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_14.xyz = max(u_xlat16_14.xyz, vec3(6.10351562e-05, 6.10351562e-05, 6.10351562e-05));
			    u_xlat16_16.xyz = u_xlat16_15.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat0.x = texture(_CameraDepthTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).x;
			    u_xlat16_88 = min(u_xlat0.x, 1.0);
			    u_xlat0.x = texture(_CameraDepthTexture, u_xlat1.zw, _GlobalMipBias.x).x;
			    u_xlat25 = texture(_CameraDepthTexture, u_xlat1.xy, _GlobalMipBias.x).x;
			    u_xlatb50 = u_xlat0.x<u_xlat16_88;
			    u_xlat16_88 = min(u_xlat0.x, u_xlat16_88);
			    u_xlat16_89 = (u_xlatb50) ? 1.0 : 0.0;
			    u_xlat0.x = texture(_CameraDepthTexture, u_xlat4.zw, _GlobalMipBias.x).x;
			    u_xlat50.x = texture(_CameraDepthTexture, u_xlat4.xy, _GlobalMipBias.x).x;
			    u_xlatb1 = u_xlat0.x<u_xlat16_88;
			    u_xlat16_88 = min(u_xlat0.x, u_xlat16_88);
			    u_xlat16_89 = (u_xlatb1) ? 0.0 : u_xlat16_89;
			    u_xlat16_90 = (u_xlatb1) ? -1.0 : 0.0;
			    u_xlatb0.x = u_xlat50.x<u_xlat16_88;
			    u_xlat16_88 = min(u_xlat50.x, u_xlat16_88);
			    u_xlat16_89 = (u_xlatb0.x) ? -1.0 : u_xlat16_89;
			    u_xlat16_90 = (u_xlatb0.x) ? 0.0 : u_xlat16_90;
			    u_xlatb0.x = u_xlat25<u_xlat16_88;
			    u_xlat16_88 = min(u_xlat25, u_xlat16_88);
			    u_xlat16_89 = (u_xlatb0.x) ? 0.0 : u_xlat16_89;
			    u_xlat16_90 = (u_xlatb0.x) ? 1.0 : u_xlat16_90;
			    u_xlatb0.x = u_xlat77<u_xlat16_88;
			    u_xlat16_88 = min(u_xlat77, u_xlat16_88);
			    u_xlat16_89 = (u_xlatb0.x) ? -1.0 : u_xlat16_89;
			    u_xlat25 = texture(_CameraDepthTexture, u_xlat7.zw, _GlobalMipBias.x).x;
			    u_xlat50.x = texture(_CameraDepthTexture, u_xlat7.xy, _GlobalMipBias.x).x;
			    u_xlatb1 = u_xlat25<u_xlat16_88;
			    u_xlat16_88 = min(u_xlat25, u_xlat16_88);
			    u_xlat16_89 = (u_xlatb1) ? 1.0 : u_xlat16_89;
			    u_xlatb0.x = u_xlatb0.x || u_xlatb1;
			    u_xlat16_90 = (u_xlatb0.x) ? -1.0 : u_xlat16_90;
			    u_xlatb0.x = u_xlat50.x<u_xlat16_88;
			    u_xlat16_88 = min(u_xlat50.x, u_xlat16_88);
			    u_xlatb25 = u_xlat75<u_xlat16_88;
			    u_xlat16_88 = (u_xlatb0.x) ? -1.0 : u_xlat16_89;
			    u_xlatb0.x = u_xlatb25 || u_xlatb0.x;
			    u_xlat16_17.x = (u_xlatb25) ? 1.0 : u_xlat16_88;
			    u_xlat16_17.y = (u_xlatb0.x) ? 1.0 : u_xlat16_90;
			    u_xlat0.xy = _TaaMotionVectorTex_TexelSize.xy * u_xlat16_17.xy + vs_TEXCOORD0.xy;
			    u_xlat16_0.xy = texture(_TaaMotionVectorTex, u_xlat0.xy, _GlobalMipBias.x).xy;
			    u_xlat50.xy = (-u_xlat16_0.xy) + vs_TEXCOORD0.xy;
			    u_xlat1.xy = u_xlat50.xy * _TaaAccumulationTex_TexelSize.zw + vec2(-0.5, -0.5);
			    u_xlat1.xy = floor(u_xlat1.xy);
			    u_xlat2 = u_xlat1.xyxy + vec4(0.5, 0.5, -0.5, -0.5);
			    u_xlat1.xy = u_xlat1.xy + vec2(2.5, 2.5);
			    u_xlat1.xy = u_xlat1.xy * _TaaAccumulationTex_TexelSize.xy;
			    u_xlat50.xy = u_xlat50.xy * _TaaAccumulationTex_TexelSize.zw + (-u_xlat2.xy);
			    u_xlat16_17.xy = u_xlat50.xy * u_xlat50.xy;
			    u_xlat16_3 = u_xlat50.xyxy * u_xlat16_17.xyxy;
			    u_xlat16_67.xy = u_xlat16_3.wz * vec2(-0.5, -0.5) + u_xlat16_17.yx;
			    u_xlat16_67.xy = (-u_xlat50.yx) * vec2(0.5, 0.5) + u_xlat16_67.xy;
			    u_xlat16_18.xy = u_xlat16_3.xy * vec2(-1.5, -1.5);
			    u_xlat16_18.xy = u_xlat16_17.xy * vec2(2.0, 2.0) + u_xlat16_18.xy;
			    u_xlat16_4 = u_xlat16_17.xyxy * vec4(2.5, 2.5, 0.5, 0.5);
			    u_xlat16_17.xy = u_xlat50.xy * vec2(0.5, 0.5) + u_xlat16_18.xy;
			    u_xlat16_18.xy = u_xlat16_3.wz * vec2(1.5, 1.5) + (-u_xlat16_4.yx);
			    u_xlat16_68.xy = u_xlat16_3.zw * vec2(0.5, 0.5) + (-u_xlat16_4.zw);
			    u_xlat16_18.xy = u_xlat16_18.xy + vec2(1.0, 1.0);
			    u_xlat16_18.xy = u_xlat16_17.yx + u_xlat16_18.xy;
			    u_xlat16_17.xy = u_xlat16_17.xy / u_xlat16_18.yx;
			    u_xlat50.xy = u_xlat2.xy + u_xlat16_17.xy;
			    u_xlat2.zw = u_xlat2.zw * _TaaAccumulationTex_TexelSize.xy;
			    u_xlat2.xy = u_xlat50.xy * _TaaAccumulationTex_TexelSize.xy;
			    u_xlat16_17.xy = u_xlat16_67.xy * u_xlat16_18.yx;
			    u_xlat16_5.xyz = texture(_TaaAccumulationTex, u_xlat2.zy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = texture(_TaaAccumulationTex, u_xlat2.xw, _GlobalMipBias.x).xyz;
			    u_xlat16_88 = dot(u_xlat16_5.xz, vec2(0.5, -0.5));
			    u_xlat16_19.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_5.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_19.x = dot(u_xlat16_5.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_19.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_19.xyz = u_xlat16_17.yyy * u_xlat16_19.xyz;
			    u_xlat16_88 = dot(u_xlat16_6.xz, vec2(0.5, -0.5));
			    u_xlat16_20.y = u_xlat16_88 + 0.501960814;
			    u_xlat16_88 = dot(u_xlat16_6.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_20.x = dot(u_xlat16_6.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_20.z = u_xlat16_88 + 0.501960814;
			    u_xlat16_19.xyz = u_xlat16_20.xyz * u_xlat16_17.xxx + u_xlat16_19.xyz;
			    u_xlat16_88 = u_xlat16_17.y + u_xlat16_17.x;
			    u_xlat16_88 = u_xlat16_18.y * u_xlat16_18.x + u_xlat16_88;
			    u_xlat16_88 = u_xlat16_68.x * u_xlat16_18.x + u_xlat16_88;
			    u_xlat16_88 = u_xlat16_68.y * u_xlat16_18.y + u_xlat16_88;
			    u_xlat16_17.xy = u_xlat16_18.xy * u_xlat16_68.xy;
			    u_xlat16_89 = u_xlat16_18.x * u_xlat16_18.y;
			    u_xlat16_88 = float(1.0) / float(u_xlat16_88);
			    u_xlat16_5.xyz = texture(_TaaAccumulationTex, u_xlat2.xy, _GlobalMipBias.x).xyz;
			    u_xlat1.zw = u_xlat2.yx;
			    u_xlat16_90 = dot(u_xlat16_5.xz, vec2(0.5, -0.5));
			    u_xlat16_18.y = u_xlat16_90 + 0.501960814;
			    u_xlat16_90 = dot(u_xlat16_5.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_18.x = dot(u_xlat16_5.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_18.z = u_xlat16_90 + 0.501960814;
			    u_xlat16_18.xyz = u_xlat16_18.xyz * vec3(u_xlat16_89) + u_xlat16_19.xyz;
			    u_xlat16_2.xyz = texture(_TaaAccumulationTex, u_xlat1.xz, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_TaaAccumulationTex, u_xlat1.wy, _GlobalMipBias.x).xyz;
			    u_xlat16_89 = dot(u_xlat16_2.xz, vec2(0.5, -0.5));
			    u_xlat16_19.y = u_xlat16_89 + 0.501960814;
			    u_xlat16_89 = dot(u_xlat16_2.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_19.x = dot(u_xlat16_2.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_19.z = u_xlat16_89 + 0.501960814;
			    u_xlat16_17.xzw = u_xlat16_19.xyz * u_xlat16_17.xxx + u_xlat16_18.xyz;
			    u_xlat16_89 = dot(u_xlat16_1.xz, vec2(0.5, -0.5));
			    u_xlat16_18.y = u_xlat16_89 + 0.501960814;
			    u_xlat16_89 = dot(u_xlat16_1.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_18.x = dot(u_xlat16_1.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_18.z = u_xlat16_89 + 0.501960814;
			    u_xlat16_17.xyz = u_xlat16_18.xyz * u_xlat16_17.yyy + u_xlat16_17.xzw;
			    u_xlat16_16.xyz = u_xlat16_17.xyz * vec3(u_xlat16_88) + (-u_xlat16_16.xyz);
			    u_xlat16_17.xyz = vec3(u_xlat16_88) * u_xlat16_17.xyz;
			    u_xlat16_14.xyz = u_xlat16_16.xyz / u_xlat16_14.xyz;
			    u_xlat16_88 = max(abs(u_xlat16_14.y), abs(u_xlat16_14.x));
			    u_xlat16_88 = max(abs(u_xlat16_14.z), u_xlat16_88);
			    u_xlat16_14.xyz = u_xlat16_16.xyz / vec3(u_xlat16_88);
			    u_xlatb50 = 1.0<u_xlat16_88;
			    u_xlat16_14.xyz = u_xlat16_15.xyz * vec3(0.5, 0.5, 0.5) + u_xlat16_14.xyz;
			    u_xlat16_14.xyz = (bool(u_xlatb50)) ? u_xlat16_14.xyz : u_xlat16_17.xyz;
			    u_xlat50.x = u_xlat16_14.x + 1.0;
			    u_xlat50.x = float(1.0) / float(u_xlat50.x);
			    u_xlat1.xyz = u_xlat50.xxx * u_xlat16_14.xyz;
			    u_xlat50.x = u_xlat16_13.x + 1.0;
			    u_xlat50.x = float(1.0) / float(u_xlat50.x);
			    u_xlat2.xyz = u_xlat16_13.xyz * u_xlat50.xxx + (-u_xlat1.xyz);
			    u_xlat50.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat0.xy = (-u_xlat16_0.xy) + u_xlat50.xy;
			    u_xlatb0.xy = lessThan(vec4(0.5, 0.5, 0.0, 0.0), abs(u_xlat0.xyxx)).xy;
			    u_xlatb0.x = u_xlatb0.y || u_xlatb0.x;
			    u_xlat16_13.x = (u_xlatb0.x) ? 1.0 : _TaaFrameInfluence;
			    u_xlat0.xyz = u_xlat16_13.xxx * u_xlat2.xyz + u_xlat1.xyz;
			    u_xlat75 = (-u_xlat0.x) + 1.0;
			    u_xlat75 = float(1.0) / float(u_xlat75);
			    u_xlat16_13.xy = u_xlat0.zy * vec2(u_xlat75) + vec2(-0.501960814, -0.501960814);
			    u_xlat16_63 = u_xlat0.x * u_xlat75 + (-u_xlat16_13.y);
			    u_xlat16_0.yz = u_xlat0.xx * vec2(u_xlat75) + u_xlat16_13.yx;
			    u_xlat16_0.w = (-u_xlat16_13.x) + u_xlat16_63;
			    u_xlat16_0.x = (-u_xlat16_13.x) + u_xlat16_0.y;
			    SV_Target0.xyz = max(u_xlat16_0.xzw, vec3(0.0, 0.0, 0.0));
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
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			UNITY_BINDING(0) uniform TemporalAAData {
			#endif
				UNITY_UNIFORM vec4                _BlitTexture_TexelSize;
				UNITY_UNIFORM vec4                _TaaMotionVectorTex_TexelSize;
				UNITY_UNIFORM vec4                _TaaAccumulationTex_TexelSize;
				UNITY_UNIFORM float                _TaaFilterWeights[9];
				UNITY_UNIFORM mediump float                _TaaFrameInfluence;
				UNITY_UNIFORM mediump float                _TaaVarianceClampScale;
			#if HLSLCC_ENABLE_UNIFORM_BUFFERS
			};
			#endif
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(2) uniform mediump sampler2D _TaaMotionVectorTex;
			UNITY_LOCATION(3) uniform mediump sampler2D _TaaAccumulationTex;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			vec4 u_xlat1;
			mediump vec2 u_xlat16_1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			vec2 u_xlat3;
			mediump vec4 u_xlat16_3;
			bvec2 u_xlatb3;
			bool u_xlatb4;
			vec4 u_xlat5;
			mediump vec3 u_xlat16_5;
			vec2 u_xlat6;
			mediump vec2 u_xlat16_6;
			vec4 u_xlat7;
			mediump vec3 u_xlat16_7;
			mediump vec3 u_xlat16_8;
			mediump vec4 u_xlat16_9;
			mediump vec4 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			vec3 u_xlat12;
			mediump vec3 u_xlat16_12;
			mediump vec3 u_xlat16_13;
			mediump vec3 u_xlat16_14;
			mediump vec3 u_xlat16_15;
			mediump vec3 u_xlat16_16;
			mediump vec3 u_xlat16_17;
			mediump vec3 u_xlat16_18;
			mediump vec3 u_xlat16_19;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_21;
			mediump vec3 u_xlat16_22;
			mediump vec3 u_xlat16_23;
			mediump vec2 u_xlat16_25;
			bool u_xlatb26;
			bool u_xlatb27;
			vec2 u_xlat28;
			mediump vec3 u_xlat16_28;
			bool u_xlatb28;
			mediump vec3 u_xlat16_32;
			mediump float u_xlat16_49;
			float u_xlat51;
			bool u_xlatb51;
			vec2 u_xlat54;
			mediump vec2 u_xlat16_56;
			float u_xlat72;
			bool u_xlatb72;
			mediump float u_xlat16_80;
			mediump float u_xlat16_81;
			void main()
			{
			    u_xlat0.x = texture(_CameraDepthTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).x;
			    u_xlat16_1.x = min(u_xlat0.x, 1.0);
			    u_xlat0 = _BlitTexture_TexelSize.xyxy * vec4(0.0, 1.0, 1.0, 0.0) + vs_TEXCOORD0.xyxy;
			    u_xlat2.x = texture(_CameraDepthTexture, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlatb26 = u_xlat2.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat2.x);
			    u_xlat16_25.x = (u_xlatb26) ? 1.0 : 0.0;
			    u_xlat2 = _BlitTexture_TexelSize.xyxy * vec4(-1.0, 0.0, 0.0, -1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat2.zw, _GlobalMipBias.x).x;
			    u_xlatb27 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_25.x = (u_xlatb27) ? 0.0 : u_xlat16_25.x;
			    u_xlat16_25.y = (u_xlatb27) ? -1.0 : 0.0;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat2.xy, _GlobalMipBias.x).x;
			    u_xlatb27 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_25.xy = (bool(u_xlatb27)) ? vec2(-1.0, 0.0) : u_xlat16_25.xy;
			    u_xlat3.x = texture(_CameraDepthTexture, u_xlat0.xy, _GlobalMipBias.x).x;
			    u_xlatb27 = u_xlat3.x<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat3.x);
			    u_xlat16_25.xy = (bool(u_xlatb27)) ? vec2(0.0, 1.0) : u_xlat16_25.xy;
			    u_xlat3.xy = vs_TEXCOORD0.xy + (-_BlitTexture_TexelSize.xy);
			    u_xlat51 = texture(_CameraDepthTexture, u_xlat3.xy, _GlobalMipBias.x).x;
			    u_xlat16_3.xyw = texture(_BlitTexture, u_xlat3.xy, _GlobalMipBias.x).xyz;
			    u_xlatb4 = u_xlat51<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat51);
			    u_xlat16_25.x = (u_xlatb4) ? -1.0 : u_xlat16_25.x;
			    u_xlat5 = _BlitTexture_TexelSize.xyxy * vec4(-1.0, 1.0, 1.0, -1.0) + vs_TEXCOORD0.xyxy;
			    u_xlat51 = texture(_CameraDepthTexture, u_xlat5.zw, _GlobalMipBias.x).x;
			    u_xlatb28 = u_xlat51<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat51);
			    u_xlat16_25.x = (u_xlatb28) ? 1.0 : u_xlat16_25.x;
			    u_xlatb51 = u_xlatb28 || u_xlatb4;
			    u_xlat16_49 = (u_xlatb51) ? -1.0 : u_xlat16_25.y;
			    u_xlat51 = texture(_CameraDepthTexture, u_xlat5.xy, _GlobalMipBias.x).x;
			    u_xlatb4 = u_xlat51<u_xlat16_1.x;
			    u_xlat16_1.x = min(u_xlat16_1.x, u_xlat51);
			    u_xlat16_25.x = (u_xlatb4) ? -1.0 : u_xlat16_25.x;
			    u_xlat28.xy = vs_TEXCOORD0.xy + _BlitTexture_TexelSize.xy;
			    u_xlat51 = texture(_CameraDepthTexture, u_xlat28.xy, _GlobalMipBias.x).x;
			    u_xlat16_28.xyz = texture(_BlitTexture, u_xlat28.xy, _GlobalMipBias.x).xyz;
			    u_xlatb51 = u_xlat51<u_xlat16_1.x;
			    u_xlat16_1.x = (u_xlatb51) ? 1.0 : u_xlat16_25.x;
			    u_xlatb51 = u_xlatb51 || u_xlatb4;
			    u_xlat16_1.y = (u_xlatb51) ? 1.0 : u_xlat16_49;
			    u_xlat6.xy = _TaaMotionVectorTex_TexelSize.xy * u_xlat16_1.xy + vs_TEXCOORD0.xy;
			    u_xlat16_6.xy = texture(_TaaMotionVectorTex, u_xlat6.xy, _GlobalMipBias.x).xy;
			    u_xlat54.xy = (-u_xlat16_6.xy) + vs_TEXCOORD0.xy;
			    u_xlat7.xy = u_xlat54.xy * _TaaAccumulationTex_TexelSize.zw + vec2(-0.5, -0.5);
			    u_xlat7.xy = floor(u_xlat7.xy);
			    u_xlat1 = u_xlat7.xyxy + vec4(0.5, 0.5, -0.5, -0.5);
			    u_xlat7.xy = u_xlat7.xy + vec2(2.5, 2.5);
			    u_xlat7.xy = u_xlat7.xy * _TaaAccumulationTex_TexelSize.xy;
			    u_xlat54.xy = u_xlat54.xy * _TaaAccumulationTex_TexelSize.zw + (-u_xlat1.xy);
			    u_xlat16_8.xy = u_xlat54.xy * u_xlat54.xy;
			    u_xlat16_9 = u_xlat54.xyxy * u_xlat16_8.xyxy;
			    u_xlat16_56.xy = u_xlat16_9.wz * vec2(-0.5, -0.5) + u_xlat16_8.yx;
			    u_xlat16_56.xy = (-u_xlat54.yx) * vec2(0.5, 0.5) + u_xlat16_56.xy;
			    u_xlat16_9.xy = u_xlat16_9.xy * vec2(-1.5, -1.5);
			    u_xlat16_9.xy = u_xlat16_8.xy * vec2(2.0, 2.0) + u_xlat16_9.xy;
			    u_xlat16_10 = u_xlat16_8.xyxy * vec4(2.5, 2.5, 0.5, 0.5);
			    u_xlat16_8.xy = u_xlat54.xy * vec2(0.5, 0.5) + u_xlat16_9.xy;
			    u_xlat16_9.xy = u_xlat16_9.wz * vec2(1.5, 1.5) + (-u_xlat16_10.yx);
			    u_xlat16_9.zw = u_xlat16_9.zw * vec2(0.5, 0.5) + (-u_xlat16_10.zw);
			    u_xlat16_9.xy = u_xlat16_9.xy + vec2(1.0, 1.0);
			    u_xlat16_9.xy = u_xlat16_8.yx + u_xlat16_9.xy;
			    u_xlat16_8.xy = u_xlat16_8.xy / u_xlat16_9.yx;
			    u_xlat54.xy = u_xlat1.xy + u_xlat16_8.xy;
			    u_xlat1.zw = u_xlat1.zw * _TaaAccumulationTex_TexelSize.xy;
			    u_xlat1.xy = u_xlat54.xy * _TaaAccumulationTex_TexelSize.xy;
			    u_xlat16_8.xy = u_xlat16_56.xy * u_xlat16_9.yx;
			    u_xlat16_11.xyz = texture(_TaaAccumulationTex, u_xlat1.zy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = texture(_TaaAccumulationTex, u_xlat1.xw, _GlobalMipBias.x).xyz;
			    u_xlat16_56.x = dot(u_xlat16_11.xz, vec2(0.5, -0.5));
			    u_xlat16_10.y = u_xlat16_56.x + 0.501960814;
			    u_xlat16_56.x = dot(u_xlat16_11.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_10.x = dot(u_xlat16_11.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_10.z = u_xlat16_56.x + 0.501960814;
			    u_xlat16_10.xyz = u_xlat16_8.yyy * u_xlat16_10.xyz;
			    u_xlat16_56.x = dot(u_xlat16_12.xz, vec2(0.5, -0.5));
			    u_xlat16_13.y = u_xlat16_56.x + 0.501960814;
			    u_xlat16_56.x = dot(u_xlat16_12.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_13.x = dot(u_xlat16_12.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_13.z = u_xlat16_56.x + 0.501960814;
			    u_xlat16_10.xyz = u_xlat16_13.xyz * u_xlat16_8.xxx + u_xlat16_10.xyz;
			    u_xlat16_8.x = u_xlat16_8.y + u_xlat16_8.x;
			    u_xlat16_8.x = u_xlat16_9.y * u_xlat16_9.x + u_xlat16_8.x;
			    u_xlat16_8.x = u_xlat16_9.z * u_xlat16_9.x + u_xlat16_8.x;
			    u_xlat16_8.x = u_xlat16_9.w * u_xlat16_9.y + u_xlat16_8.x;
			    u_xlat16_32.xyz = u_xlat16_9.xyx * u_xlat16_9.zwy;
			    u_xlat16_8.x = float(1.0) / float(u_xlat16_8.x);
			    u_xlat16_11.xyz = texture(_TaaAccumulationTex, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat7.zw = u_xlat1.yx;
			    u_xlat16_9.x = dot(u_xlat16_11.xz, vec2(0.5, -0.5));
			    u_xlat16_9.y = u_xlat16_9.x + 0.501960814;
			    u_xlat16_81 = dot(u_xlat16_11.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_9.x = dot(u_xlat16_11.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_9.z = u_xlat16_81 + 0.501960814;
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_32.zzz + u_xlat16_10.xyz;
			    u_xlat16_11.xyz = texture(_TaaAccumulationTex, u_xlat7.xz, _GlobalMipBias.x).xyz;
			    u_xlat16_7.xyz = texture(_TaaAccumulationTex, u_xlat7.wy, _GlobalMipBias.x).xyz;
			    u_xlat16_80 = dot(u_xlat16_11.xz, vec2(0.5, -0.5));
			    u_xlat16_10.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_11.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_10.x = dot(u_xlat16_11.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_10.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_9.xyz = u_xlat16_10.xyz * u_xlat16_32.xxx + u_xlat16_9.xyz;
			    u_xlat16_32.x = dot(u_xlat16_7.xz, vec2(0.5, -0.5));
			    u_xlat16_10.y = u_xlat16_32.x + 0.501960814;
			    u_xlat16_32.x = dot(u_xlat16_7.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_10.x = dot(u_xlat16_7.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_10.z = u_xlat16_32.x + 0.501960814;
			    u_xlat16_32.xyz = u_xlat16_10.xyz * u_xlat16_32.yyy + u_xlat16_9.xyz;
			    u_xlat16_8.xyz = u_xlat16_8.xxx * u_xlat16_32.xyz;
			    u_xlat16_7.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_0.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat12.xyz = u_xlat16_11.xyz * vec3(_TaaFilterWeights[1]);
			    u_xlat7.xyz = vec3(_TaaFilterWeights[0]) * u_xlat16_7.xyz + u_xlat12.xyz;
			    u_xlat7.xyz = vec3(_TaaFilterWeights[2]) * u_xlat16_0.xyz + u_xlat7.xyz;
			    u_xlat16_12.xyz = texture(_BlitTexture, u_xlat2.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat2.zw, _GlobalMipBias.x).xyz;
			    u_xlat7.xyz = vec3(_TaaFilterWeights[3]) * u_xlat16_12.xyz + u_xlat7.xyz;
			    u_xlat7.xyz = vec3(_TaaFilterWeights[4]) * u_xlat16_2.xyz + u_xlat7.xyz;
			    u_xlat16_14.xyz = texture(_BlitTexture, u_xlat5.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_5.xyz = texture(_BlitTexture, u_xlat5.zw, _GlobalMipBias.x).xyz;
			    u_xlat7.xyz = vec3(_TaaFilterWeights[5]) * u_xlat16_14.xyz + u_xlat7.xyz;
			    u_xlat7.xyz = vec3(_TaaFilterWeights[6]) * u_xlat16_5.xyz + u_xlat7.xyz;
			    u_xlat7.xyz = vec3(_TaaFilterWeights[7]) * u_xlat16_28.xyz + u_xlat7.xyz;
			    u_xlat7.xyz = vec3(_TaaFilterWeights[8]) * u_xlat16_3.xyw + u_xlat7.xyz;
			    u_xlat16_80 = dot(u_xlat7.xz, vec2(0.5, -0.5));
			    u_xlat16_9.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat7.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_9.x = dot(u_xlat7.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_9.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_2.xz, vec2(0.5, -0.5));
			    u_xlat16_10.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_2.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_10.x = dot(u_xlat16_2.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_10.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_13.xyz = u_xlat16_10.xyz * u_xlat16_10.xyz;
			    u_xlat16_13.xyz = u_xlat16_9.xyz * u_xlat16_9.xyz + u_xlat16_13.xyz;
			    u_xlat16_80 = dot(u_xlat16_12.xz, vec2(0.5, -0.5));
			    u_xlat16_15.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_12.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_15.x = dot(u_xlat16_12.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_15.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_13.xyz = u_xlat16_15.xyz * u_xlat16_15.xyz + u_xlat16_13.xyz;
			    u_xlat16_80 = dot(u_xlat16_0.xz, vec2(0.5, -0.5));
			    u_xlat16_16.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_0.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_16.x = dot(u_xlat16_0.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_16.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_13.xyz = u_xlat16_16.xyz * u_xlat16_16.xyz + u_xlat16_13.xyz;
			    u_xlat16_80 = dot(u_xlat16_11.xz, vec2(0.5, -0.5));
			    u_xlat16_17.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_11.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_17.x = dot(u_xlat16_11.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_17.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_13.xyz = u_xlat16_17.xyz * u_xlat16_17.xyz + u_xlat16_13.xyz;
			    u_xlat16_80 = dot(u_xlat16_3.xw, vec2(0.5, -0.5));
			    u_xlat16_18.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_3.xwy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_18.x = dot(u_xlat16_3.xwy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_18.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_13.xyz = u_xlat16_18.xyz * u_xlat16_18.xyz + u_xlat16_13.xyz;
			    u_xlat16_80 = dot(u_xlat16_5.xz, vec2(0.5, -0.5));
			    u_xlat16_19.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_5.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_19.x = dot(u_xlat16_5.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_19.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_13.xyz = u_xlat16_19.xyz * u_xlat16_19.xyz + u_xlat16_13.xyz;
			    u_xlat16_80 = dot(u_xlat16_14.xz, vec2(0.5, -0.5));
			    u_xlat16_20.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_14.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_20.x = dot(u_xlat16_14.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_20.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_13.xyz = u_xlat16_20.xyz * u_xlat16_20.xyz + u_xlat16_13.xyz;
			    u_xlat16_80 = dot(u_xlat16_28.xz, vec2(0.5, -0.5));
			    u_xlat16_21.y = u_xlat16_80 + 0.501960814;
			    u_xlat16_80 = dot(u_xlat16_28.xzy, vec3(-0.25, -0.25, 0.5));
			    u_xlat16_21.x = dot(u_xlat16_28.xzy, vec3(0.25, 0.25, 0.5));
			    u_xlat16_21.z = u_xlat16_80 + 0.501960814;
			    u_xlat16_13.xyz = u_xlat16_21.xyz * u_xlat16_21.xyz + u_xlat16_13.xyz;
			    u_xlat16_22.xyz = u_xlat16_9.xyz + u_xlat16_10.xyz;
			    u_xlat16_22.xyz = u_xlat16_15.xyz + u_xlat16_22.xyz;
			    u_xlat16_22.xyz = u_xlat16_16.xyz + u_xlat16_22.xyz;
			    u_xlat16_22.xyz = u_xlat16_17.xyz + u_xlat16_22.xyz;
			    u_xlat16_22.xyz = u_xlat16_18.xyz + u_xlat16_22.xyz;
			    u_xlat16_22.xyz = u_xlat16_19.xyz + u_xlat16_22.xyz;
			    u_xlat16_22.xyz = u_xlat16_20.xyz + u_xlat16_22.xyz;
			    u_xlat16_22.xyz = u_xlat16_21.xyz + u_xlat16_22.xyz;
			    u_xlat16_23.xyz = u_xlat16_22.xyz * vec3(0.111111112, 0.111111112, 0.111111112);
			    u_xlat16_23.xyz = u_xlat16_23.xyz * u_xlat16_23.xyz;
			    u_xlat16_13.xyz = u_xlat16_13.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + (-u_xlat16_23.xyz);
			    u_xlat16_13.xyz = sqrt(abs(u_xlat16_13.xyz));
			    u_xlat16_13.xyz = u_xlat16_13.xyz * vec3(vec3(_TaaVarianceClampScale, _TaaVarianceClampScale, _TaaVarianceClampScale));
			    u_xlat16_23.xyz = u_xlat16_22.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + (-u_xlat16_13.xyz);
			    u_xlat16_13.xyz = u_xlat16_22.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + u_xlat16_13.xyz;
			    u_xlat16_22.xyz = min(u_xlat16_9.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_9.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_15.xyz);
			    u_xlat16_15.xyz = min(u_xlat16_15.xyz, u_xlat16_22.xyz);
			    u_xlat16_15.xyz = min(u_xlat16_15.xyz, u_xlat16_16.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_16.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_17.xyz);
			    u_xlat16_15.xyz = min(u_xlat16_15.xyz, u_xlat16_17.xyz);
			    u_xlat16_15.xyz = min(u_xlat16_15.xyz, u_xlat16_18.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_18.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_19.xyz);
			    u_xlat16_15.xyz = min(u_xlat16_15.xyz, u_xlat16_19.xyz);
			    u_xlat16_15.xyz = min(u_xlat16_15.xyz, u_xlat16_20.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_20.xyz);
			    u_xlat16_10.xyz = max(u_xlat16_10.xyz, u_xlat16_21.xyz);
			    u_xlat16_15.xyz = min(u_xlat16_15.xyz, u_xlat16_21.xyz);
			    u_xlat16_15.xyz = max(u_xlat16_23.xyz, u_xlat16_15.xyz);
			    u_xlat16_8.xyz = max(u_xlat16_8.xyz, u_xlat16_15.xyz);
			    u_xlat16_10.xyz = min(u_xlat16_13.xyz, u_xlat16_10.xyz);
			    u_xlat16_8.xyz = min(u_xlat16_8.xyz, u_xlat16_10.xyz);
			    u_xlat0.x = u_xlat16_8.x + 1.0;
			    u_xlat0.x = float(1.0) / float(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat16_8.xyz;
			    u_xlat72 = u_xlat16_9.x + 1.0;
			    u_xlat72 = float(1.0) / float(u_xlat72);
			    u_xlat2.xyz = u_xlat16_9.xyz * vec3(u_xlat72) + (-u_xlat0.xyz);
			    u_xlat3.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat3.xy = (-u_xlat16_6.xy) + u_xlat3.xy;
			    u_xlatb3.xy = lessThan(vec4(0.5, 0.5, 0.0, 0.0), abs(u_xlat3.xyxx)).xy;
			    u_xlatb72 = u_xlatb3.y || u_xlatb3.x;
			    u_xlat16_8.x = (u_xlatb72) ? 1.0 : _TaaFrameInfluence;
			    u_xlat0.xyz = u_xlat16_8.xxx * u_xlat2.xyz + u_xlat0.xyz;
			    u_xlat72 = (-u_xlat0.x) + 1.0;
			    u_xlat72 = float(1.0) / float(u_xlat72);
			    u_xlat16_8.xy = u_xlat0.zy * vec2(u_xlat72) + vec2(-0.501960814, -0.501960814);
			    u_xlat16_56.x = u_xlat0.x * u_xlat72 + (-u_xlat16_8.y);
			    u_xlat16_0.yz = u_xlat0.xx * vec2(u_xlat72) + u_xlat16_8.yx;
			    u_xlat16_0.w = (-u_xlat16_8.x) + u_xlat16_56.x;
			    u_xlat16_0.x = (-u_xlat16_8.x) + u_xlat16_0.y;
			    SV_Target0.xyz = max(u_xlat16_0.xzw, vec3(0.0, 0.0, 0.0));
			    SV_Target0.w = 1.0;
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
			uniform 	vec2 _GlobalMipBias;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump vec3 u_xlat16_0;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    SV_Target0.xyz = u_xlat16_0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}