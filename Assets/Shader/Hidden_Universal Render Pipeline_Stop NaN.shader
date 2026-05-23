Shader "Hidden/Universal Render Pipeline/Stop NaN" {
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
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			uvec3 u_xlatu1;
			bvec3 u_xlatb1;
			bvec3 u_xlatb2;
			bool u_xlatb9;
			void main()
			{
			    u_xlat0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlatu1.xyz = uvec3(uint(floatBitsToUint(u_xlat0.x)) & uint(2147483647u), uint(floatBitsToUint(u_xlat0.y)) & uint(2147483647u), uint(floatBitsToUint(u_xlat0.z)) & uint(2147483647u));
			    u_xlatb2.xyz = lessThan(uvec4(2139095040u, 2139095040u, 2139095040u, uint(0u)), u_xlatu1.xyzx).xyz;
			    u_xlatb1.xyz = equal(ivec4(u_xlatu1.xyzx), ivec4(int(0x7F800000u), int(0x7F800000u), int(0x7F800000u), 0)).xyz;
			    u_xlatb9 = u_xlatb2.y || u_xlatb2.x;
			    u_xlatb9 = u_xlatb2.z || u_xlatb9;
			    u_xlatb1.x = u_xlatb1.y || u_xlatb1.x;
			    u_xlatb1.x = u_xlatb1.z || u_xlatb1.x;
			    u_xlatb9 = u_xlatb9 || u_xlatb1.x;
			    SV_Target0.xyz = (bool(u_xlatb9)) ? vec3(0.0, 0.0, 0.0) : u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}