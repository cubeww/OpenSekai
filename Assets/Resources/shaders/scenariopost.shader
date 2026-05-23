Shader "Hidden/Sekai/Scenario/Post" {
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
			uniform 	mediump float _Influence;
			uniform 	mediump vec3 _Monochrome;
			uniform 	mediump vec3 _ToneRatio;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump vec4 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			mediump vec3 u_xlat16_2;
			mediump float u_xlat16_10;
			void main()
			{
			    u_xlat16_0 = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x);
			    u_xlat16_1.x = dot(u_xlat16_0.xyz, vec3(_Monochrome.x, _Monochrome.y, _Monochrome.z));
			    u_xlat16_1.x = u_xlat16_0.w * u_xlat16_1.x;
			    u_xlat16_1.xyz = u_xlat16_1.xxx * _ToneRatio.xyz;
			    u_xlat16_10 = (-_Influence) + 1.0;
			    u_xlat16_2.xyz = u_xlat16_0.xyz * vec3(u_xlat16_10);
			    SV_Target0.xyz = u_xlat16_1.xyz * vec3(_Influence) + u_xlat16_2.xyz;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			uniform 	float _BlurSize;
			out mediump vec2 vs_TEXCOORD0;
			out mediump vec2 vs_TEXCOORD1;
			out mediump vec2 vs_TEXCOORD2;
			out mediump vec2 vs_TEXCOORD3;
			out mediump vec2 vs_TEXCOORD4;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			vec4 u_xlat1;
			vec2 u_xlat4;
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
			    u_xlat0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy;
			    u_xlat1.x = 0.0;
			    u_xlat1.y = _BlitTexture_TexelSize.y * _BlurSize;
			    u_xlat4.xy = u_xlat0.xy + u_xlat1.xy;
			    u_xlat1.xw = u_xlat0.xy + (-u_xlat1.xy);
			    vs_TEXCOORD2.xy = u_xlat1.xw;
			    vs_TEXCOORD1.xy = u_xlat4.xy;
			    u_xlat1.z = _BlurSize;
			    u_xlat4.xy = u_xlat1.zy * vec2(0.0, 2.0) + u_xlat0.xy;
			    u_xlat0.xy = (-u_xlat1.zy) * vec2(0.0, 2.0) + u_xlat0.xy;
			    vs_TEXCOORD4.xy = u_xlat0.xy;
			    vs_TEXCOORD3.xy = u_xlat4.xy;
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
			in mediump vec2 vs_TEXCOORD0;
			in mediump vec2 vs_TEXCOORD1;
			in mediump vec2 vs_TEXCOORD2;
			in mediump vec2 vs_TEXCOORD3;
			in mediump vec2 vs_TEXCOORD4;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD1.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_0.xyz * vec3(0.244200006, 0.244200006, 0.244200006);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_1.xyz * vec3(0.40259999, 0.40259999, 0.40259999) + u_xlat0.xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD2.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_1.xyz * vec3(0.244200006, 0.244200006, 0.244200006) + u_xlat0.xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD3.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_1.xyz * vec3(0.0544999987, 0.0544999987, 0.0544999987) + u_xlat0.xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD4.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_1.xyz * vec3(0.0544999987, 0.0544999987, 0.0544999987) + u_xlat0.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
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
			uniform 	vec4 _BlitTexture_TexelSize;
			uniform 	float _BlurSize;
			out mediump vec2 vs_TEXCOORD0;
			out mediump vec2 vs_TEXCOORD1;
			out mediump vec2 vs_TEXCOORD2;
			out mediump vec2 vs_TEXCOORD3;
			out mediump vec2 vs_TEXCOORD4;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			vec4 u_xlat1;
			vec4 u_xlat2;
			vec2 u_xlat6;
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
			    u_xlat0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy;
			    u_xlat1.x = _BlitTexture_TexelSize.x * _BlurSize;
			    u_xlat1.z = u_xlat1.x * 2.0;
			    u_xlat1.y = float(0.0);
			    u_xlat1.w = float(0.0);
			    u_xlat2 = u_xlat0.xyxy + u_xlat1;
			    u_xlat6.xy = u_xlat0.xy + (-u_xlat1.zw);
			    u_xlat0.xy = u_xlat0.xy + (-u_xlat1.xy);
			    vs_TEXCOORD2.xy = u_xlat0.xy;
			    vs_TEXCOORD4.xy = u_xlat6.xy;
			    vs_TEXCOORD1.xy = u_xlat2.xy;
			    vs_TEXCOORD3.xy = u_xlat2.zw;
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
			in mediump vec2 vs_TEXCOORD0;
			in mediump vec2 vs_TEXCOORD1;
			in mediump vec2 vs_TEXCOORD2;
			in mediump vec2 vs_TEXCOORD3;
			in mediump vec2 vs_TEXCOORD4;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD1.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_0.xyz * vec3(0.244200006, 0.244200006, 0.244200006);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_1.xyz * vec3(0.40259999, 0.40259999, 0.40259999) + u_xlat0.xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD2.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_1.xyz * vec3(0.244200006, 0.244200006, 0.244200006) + u_xlat0.xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD3.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_1.xyz * vec3(0.0544999987, 0.0544999987, 0.0544999987) + u_xlat0.xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD4.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat16_1.xyz * vec3(0.0544999987, 0.0544999987, 0.0544999987) + u_xlat0.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}