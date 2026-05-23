Shader "Hidden/AfterPostProcess/FadeOutBlend" {
	Properties {
		_FadeOutParams ("FadeOut Params", Vector) = (0,0,0,0)
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
			uniform 	vec4 _FadeOutParams;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			bool u_xlatb0;
			vec3 u_xlat1;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			vec3 u_xlat3;
			void main()
			{
			    u_xlatb0 = _FadeOutParams.y>=0.0;
			    u_xlat3.x = (u_xlatb0) ? 0.0 : 1.0;
			    u_xlat0.x = u_xlatb0 ? 1.0 : float(0.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat1.xyz = u_xlat16_1.xyz + _FadeOutParams.xxx;
			    u_xlat2.xyz = u_xlat1.xyz * _FadeOutParams.yyy;
			    u_xlat3.xyz = u_xlat3.xxx * u_xlat2.xyz;
			    u_xlat2.xyz = (-u_xlat1.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat2.xyz = u_xlat2.xyz * _FadeOutParams.yyy;
			    u_xlat0.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat3.xyz;
			    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}