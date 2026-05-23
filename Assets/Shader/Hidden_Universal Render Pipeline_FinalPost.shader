Shader "Hidden/Universal Render Pipeline/FinalPost" {
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
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
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
		Pass {
			Name "GLSL_1"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			vec3 u_xlat1;
			bvec3 u_xlatb2;
			mediump vec3 u_xlat16_3;
			void main()
			{
			    u_xlat0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat1.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb2.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.x = (u_xlatb2.x) ? u_xlat16_3.x : u_xlat1.x;
			    u_xlat0.y = (u_xlatb2.y) ? u_xlat16_3.y : u_xlat1.y;
			    u_xlat0.z = (u_xlatb2.z) ? u_xlat16_3.z : u_xlat1.z;
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
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump float u_xlat16_0;
			bool u_xlatb0;
			float u_xlat1;
			mediump vec3 u_xlat16_1;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_0 = texture(_BlueNoise_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat0.x = u_xlat16_0 * 2.0 + -1.0;
			    u_xlat1 = -abs(u_xlat0.x) + 1.0;
			    u_xlatb0 = u_xlat0.x>=0.0;
			    u_xlat0.x = (u_xlatb0) ? 1.0 : -1.0;
			    u_xlat1 = sqrt(u_xlat1);
			    u_xlat1 = (-u_xlat1) + 1.0;
			    u_xlat0.x = u_xlat1 * u_xlat0.x;
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat0.xxx * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat16_1.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
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
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump float u_xlat16_0;
			bool u_xlatb0;
			vec3 u_xlat1;
			bvec3 u_xlatb2;
			mediump vec3 u_xlat16_3;
			vec3 u_xlat4;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_0 = texture(_BlueNoise_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat0.x = u_xlat16_0 * 2.0 + -1.0;
			    u_xlat4.x = -abs(u_xlat0.x) + 1.0;
			    u_xlatb0 = u_xlat0.x>=0.0;
			    u_xlat0.x = (u_xlatb0) ? 1.0 : -1.0;
			    u_xlat4.x = sqrt(u_xlat4.x);
			    u_xlat4.x = (-u_xlat4.x) + 1.0;
			    u_xlat0.x = u_xlat4.x * u_xlat0.x;
			    u_xlat4.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat1.xyz = log2(abs(u_xlat4.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb2.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat4.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat4.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat4.x = (u_xlatb2.x) ? u_xlat16_3.x : u_xlat1.x;
			    u_xlat4.y = (u_xlatb2.y) ? u_xlat16_3.y : u_xlat1.y;
			    u_xlat4.z = (u_xlatb2.z) ? u_xlat16_3.z : u_xlat1.z;
			    u_xlat0.xyz = u_xlat0.xxx * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat4.xyz;
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
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			float u_xlat9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_0.x = texture(_Grain_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat16_1.x = u_xlat16_0.x + -0.5;
			    u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.x;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_0.xyz;
			    u_xlat2.xyz = u_xlat16_1.xyz * _Grain_Params.xxx;
			    u_xlat16_1.x = dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat9 = sqrt(u_xlat16_1.x);
			    u_xlat9 = _Grain_Params.y * (-u_xlat9) + 1.0;
			    u_xlat0.xyz = u_xlat2.xyz * vec3(u_xlat9) + u_xlat16_0.xyz;
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
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			bvec3 u_xlatb3;
			float u_xlat12;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_0.x = texture(_Grain_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat16_1.x = u_xlat16_0.x + -0.5;
			    u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.x;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_0.xyz;
			    u_xlat2.xyz = u_xlat16_1.xyz * _Grain_Params.xxx;
			    u_xlat16_1.x = dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat12 = sqrt(u_xlat16_1.x);
			    u_xlat12 = _Grain_Params.y * (-u_xlat12) + 1.0;
			    u_xlat0.xyz = u_xlat2.xyz * vec3(u_xlat12) + u_xlat16_0.xyz;
			    u_xlat2.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat2.xyz = u_xlat2.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat2.xyz = exp2(u_xlat2.xyz);
			    u_xlat2.xyz = u_xlat2.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.x = (u_xlatb3.x) ? u_xlat16_1.x : u_xlat2.x;
			    u_xlat0.y = (u_xlatb3.y) ? u_xlat16_1.y : u_xlat2.y;
			    u_xlat0.z = (u_xlatb3.z) ? u_xlat16_1.z : u_xlat2.z;
			    SV_Target0.xyz = u_xlat0.xyz;
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
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			float u_xlat9;
			mediump float u_xlat16_9;
			bool u_xlatb9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_0.x = texture(_Grain_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat16_1.x = u_xlat16_0.x + -0.5;
			    u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.x;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_0.xyz;
			    u_xlat2.xyz = u_xlat16_1.xyz * _Grain_Params.xxx;
			    u_xlat16_1.x = dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat9 = sqrt(u_xlat16_1.x);
			    u_xlat9 = _Grain_Params.y * (-u_xlat9) + 1.0;
			    u_xlat0.xyz = u_xlat2.xyz * vec3(u_xlat9) + u_xlat16_0.xyz;
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_9 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat9 = u_xlat16_9 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat9) + 1.0;
			    u_xlatb9 = u_xlat9>=0.0;
			    u_xlat9 = (u_xlatb9) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat9 = u_xlat9 * u_xlat2.x;
			    u_xlat0.xyz = vec3(u_xlat9) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat0.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
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
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			bvec3 u_xlatb3;
			float u_xlat12;
			mediump float u_xlat16_12;
			bool u_xlatb12;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_0.x = texture(_Grain_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat16_1.x = u_xlat16_0.x + -0.5;
			    u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.x;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_0.xyz;
			    u_xlat2.xyz = u_xlat16_1.xyz * _Grain_Params.xxx;
			    u_xlat16_1.x = dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat12 = sqrt(u_xlat16_1.x);
			    u_xlat12 = _Grain_Params.y * (-u_xlat12) + 1.0;
			    u_xlat0.xyz = u_xlat2.xyz * vec3(u_xlat12) + u_xlat16_0.xyz;
			    u_xlat2.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat2.xyz = u_xlat2.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat2.xyz = exp2(u_xlat2.xyz);
			    u_xlat2.xyz = u_xlat2.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.x = (u_xlatb3.x) ? u_xlat16_1.x : u_xlat2.x;
			    u_xlat0.y = (u_xlatb3.y) ? u_xlat16_1.y : u_xlat2.y;
			    u_xlat0.z = (u_xlatb3.z) ? u_xlat16_1.z : u_xlat2.z;
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_12 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat12 = u_xlat16_12 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat12) + 1.0;
			    u_xlatb12 = u_xlat12>=0.0;
			    u_xlat12 = (u_xlatb12) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat12 = u_xlat12 * u_xlat2.x;
			    u_xlat0.xyz = vec3(u_xlat12) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat0.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_8"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    SV_Target0.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_9"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			bvec3 u_xlatb2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xyz = log2(u_xlat16_3.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb2.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_3.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec4 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? u_xlat16_3.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? u_xlat16_3.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb2.z) ? u_xlat16_3.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_10"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			float u_xlat8;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_1.x = texture(_BlueNoise_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat1.x = u_xlat16_1.x * 2.0 + -1.0;
			    u_xlat8 = -abs(u_xlat1.x) + 1.0;
			    u_xlatb1 = u_xlat1.x>=0.0;
			    u_xlat1.x = (u_xlatb1) ? 1.0 : -1.0;
			    u_xlat8 = sqrt(u_xlat8);
			    u_xlat8 = (-u_xlat8) + 1.0;
			    u_xlat1.x = u_xlat8 * u_xlat1.x;
			    u_xlat1.xyz = u_xlat1.xxx * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat16_3.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_11"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			bvec3 u_xlatb2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			float u_xlat22;
			mediump float u_xlat16_22;
			bool u_xlatb22;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xyz = log2(u_xlat16_3.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb2.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_3.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec4 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? u_xlat16_3.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? u_xlat16_3.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb2.z) ? u_xlat16_3.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_22 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat22 = u_xlat16_22 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat22) + 1.0;
			    u_xlatb22 = u_xlat22>=0.0;
			    u_xlat22 = (u_xlatb22) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat22 = u_xlat22 * u_xlat2.x;
			    u_xlat1.xyz = vec3(u_xlat22) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_12"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			float u_xlat22;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_24 = u_xlat16_1.x + -0.5;
			    u_xlat16_24 = u_xlat16_24 + u_xlat16_24;
			    u_xlat16_4.xyz = vec3(u_xlat16_24) * u_xlat16_3.xyz;
			    u_xlat1.xyz = u_xlat16_4.xyz * _Grain_Params.xxx;
			    u_xlat16_24 = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat22 = sqrt(u_xlat16_24);
			    u_xlat22 = _Grain_Params.y * (-u_xlat22) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat22) + u_xlat16_3.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_13"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bvec3 u_xlatb7;
			bool u_xlatb9;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump float u_xlat16_19;
			float u_xlat25;
			mediump float u_xlat16_27;
			mediump float u_xlat16_29;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_11.xyz = u_xlat2.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_11.x = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_19 = u_xlat16_11.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_27 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_12.x = u_xlat16_27 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_19) + (-u_xlat16_12.xx);
			    u_xlat16_19 = u_xlat16_27 + u_xlat16_3.x;
			    u_xlat16_12.x = u_xlat16_11.x + u_xlat16_4.x;
			    u_xlat16_12.x = u_xlat16_19 + (-u_xlat16_12.x);
			    u_xlat16_19 = u_xlat16_11.x + u_xlat16_19;
			    u_xlat16_19 = u_xlat16_4.x + u_xlat16_19;
			    u_xlat16_19 = u_xlat16_19 * 0.03125;
			    u_xlat16_19 = max(u_xlat16_19, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_12.x));
			    u_xlat16_0.xz = (-u_xlat16_12.xx);
			    u_xlat1.x = u_xlat16_19 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_12.xyz = u_xlat16_12.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_12.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_12.xyz = u_xlat16_12.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_19 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_29 = min(u_xlat16_11.x, u_xlat16_27);
			    u_xlat16_11.x = max(u_xlat16_11.x, u_xlat16_27);
			    u_xlat16_11.x = max(u_xlat16_4.x, u_xlat16_11.x);
			    u_xlat16_27 = min(u_xlat16_4.x, u_xlat16_29);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_29 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_11.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_19;
			    u_xlat16_3.x = min(u_xlat16_27, u_xlat16_29);
			    u_xlatb9 = u_xlat16_19<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb9;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_12.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_27 = u_xlat16_1.x + -0.5;
			    u_xlat16_27 = u_xlat16_27 + u_xlat16_27;
			    u_xlat16_4.xyz = vec3(u_xlat16_27) * u_xlat16_3.xyz;
			    u_xlat1.xyz = u_xlat16_4.xyz * _Grain_Params.xxx;
			    u_xlat16_27 = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat25 = sqrt(u_xlat16_27);
			    u_xlat25 = _Grain_Params.y * (-u_xlat25) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat25) + u_xlat16_3.xyz;
			    u_xlat2.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat2.xyz = u_xlat2.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat2.xyz = exp2(u_xlat2.xyz);
			    u_xlat2.xyz = u_xlat2.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb7.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb7.x) ? u_xlat16_3.x : u_xlat2.x;
			    u_xlat1.y = (u_xlatb7.y) ? u_xlat16_3.y : u_xlat2.y;
			    u_xlat1.z = (u_xlatb7.z) ? u_xlat16_3.z : u_xlat2.z;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_14"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			float u_xlat22;
			mediump float u_xlat16_22;
			bool u_xlatb22;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_24 = u_xlat16_1.x + -0.5;
			    u_xlat16_24 = u_xlat16_24 + u_xlat16_24;
			    u_xlat16_4.xyz = vec3(u_xlat16_24) * u_xlat16_3.xyz;
			    u_xlat1.xyz = u_xlat16_4.xyz * _Grain_Params.xxx;
			    u_xlat16_24 = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat22 = sqrt(u_xlat16_24);
			    u_xlat22 = _Grain_Params.y * (-u_xlat22) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat22) + u_xlat16_3.xyz;
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_22 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat22 = u_xlat16_22 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat22) + 1.0;
			    u_xlatb22 = u_xlat22>=0.0;
			    u_xlat22 = (u_xlatb22) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat22 = u_xlat22 * u_xlat2.x;
			    u_xlat1.xyz = vec3(u_xlat22) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_15"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bvec3 u_xlatb7;
			bool u_xlatb9;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump float u_xlat16_19;
			float u_xlat25;
			mediump float u_xlat16_25;
			bool u_xlatb25;
			mediump float u_xlat16_27;
			mediump float u_xlat16_29;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_11.xyz = u_xlat2.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_11.x = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_19 = u_xlat16_11.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_27 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_12.x = u_xlat16_27 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_19) + (-u_xlat16_12.xx);
			    u_xlat16_19 = u_xlat16_27 + u_xlat16_3.x;
			    u_xlat16_12.x = u_xlat16_11.x + u_xlat16_4.x;
			    u_xlat16_12.x = u_xlat16_19 + (-u_xlat16_12.x);
			    u_xlat16_19 = u_xlat16_11.x + u_xlat16_19;
			    u_xlat16_19 = u_xlat16_4.x + u_xlat16_19;
			    u_xlat16_19 = u_xlat16_19 * 0.03125;
			    u_xlat16_19 = max(u_xlat16_19, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_12.x));
			    u_xlat16_0.xz = (-u_xlat16_12.xx);
			    u_xlat1.x = u_xlat16_19 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_12.xyz = u_xlat16_12.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_12.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_12.xyz = u_xlat16_12.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_19 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_29 = min(u_xlat16_11.x, u_xlat16_27);
			    u_xlat16_11.x = max(u_xlat16_11.x, u_xlat16_27);
			    u_xlat16_11.x = max(u_xlat16_4.x, u_xlat16_11.x);
			    u_xlat16_27 = min(u_xlat16_4.x, u_xlat16_29);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_29 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_11.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_19;
			    u_xlat16_3.x = min(u_xlat16_27, u_xlat16_29);
			    u_xlatb9 = u_xlat16_19<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb9;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_12.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_27 = u_xlat16_1.x + -0.5;
			    u_xlat16_27 = u_xlat16_27 + u_xlat16_27;
			    u_xlat16_4.xyz = vec3(u_xlat16_27) * u_xlat16_3.xyz;
			    u_xlat1.xyz = u_xlat16_4.xyz * _Grain_Params.xxx;
			    u_xlat16_27 = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat25 = sqrt(u_xlat16_27);
			    u_xlat25 = _Grain_Params.y * (-u_xlat25) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat25) + u_xlat16_3.xyz;
			    u_xlat2.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat2.xyz = u_xlat2.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat2.xyz = exp2(u_xlat2.xyz);
			    u_xlat2.xyz = u_xlat2.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb7.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb7.x) ? u_xlat16_3.x : u_xlat2.x;
			    u_xlat1.y = (u_xlatb7.y) ? u_xlat16_3.y : u_xlat2.y;
			    u_xlat1.z = (u_xlatb7.z) ? u_xlat16_3.z : u_xlat2.z;
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_25 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat25 = u_xlat16_25 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat25) + 1.0;
			    u_xlatb25 = u_xlat25>=0.0;
			    u_xlat25 = (u_xlatb25) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat25 = u_xlat25 * u_xlat2.x;
			    u_xlat1.xyz = vec3(u_xlat25) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_16"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump ivec4 u_xlati16_0;
			vec3 u_xlat1;
			uvec4 u_xlatu1;
			mediump ivec4 u_xlati16_2;
			vec3 u_xlat3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump uint u_xlatu16_12;
			mediump vec3 u_xlat16_19;
			float u_xlat40;
			uint u_xlatu40;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_4.xy = u_xlati16_0.zw;
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_9.xyz = max(u_xlat1.xyz, u_xlat16_6.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_9.xyz = (-u_xlat16_9.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_10.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_10.xyz = min(u_xlat7.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = min(u_xlat8.xyz, u_xlat16_10.xyz);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_10.xyz = min(u_xlat1.xyz, u_xlat16_10.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_10.xyz;
			    u_xlat16_10.xyz = vec3(1.0) / vec3(u_xlat16_11.xyz);
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_10.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_9.xyz);
			    u_xlat16_19.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_19.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu40 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat40 = unpackHalf2x16(u_xlatu40).x;
			    u_xlat16_6.x = u_xlat40 * u_xlat16_6.x;
			    u_xlat16_19.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_19.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_19.xyz = u_xlat1.xyz + u_xlat16_19.xyz;
			    u_xlatu1.x = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_12 =  uint((-int(u_xlatu1.x)) + 30605);
			    u_xlat1.x = unpackHalf2x16(u_xlatu16_12).x;
			    u_xlat16_6.x = (-u_xlat1.x) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat1.x * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_19.xyz;
			    u_xlat1.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_17"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump ivec4 u_xlati16_0;
			vec3 u_xlat1;
			uvec4 u_xlatu1;
			mediump ivec4 u_xlati16_2;
			vec3 u_xlat3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump uint u_xlatu16_12;
			mediump vec3 u_xlat16_19;
			float u_xlat40;
			uint u_xlatu40;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_4.xy = u_xlati16_0.zw;
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_9.xyz = max(u_xlat1.xyz, u_xlat16_6.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_9.xyz = (-u_xlat16_9.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_10.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_10.xyz = min(u_xlat7.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = min(u_xlat8.xyz, u_xlat16_10.xyz);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_10.xyz = min(u_xlat1.xyz, u_xlat16_10.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_10.xyz;
			    u_xlat16_10.xyz = vec3(1.0) / vec3(u_xlat16_11.xyz);
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_10.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_9.xyz);
			    u_xlat16_19.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_19.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu40 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat40 = unpackHalf2x16(u_xlatu40).x;
			    u_xlat16_6.x = u_xlat40 * u_xlat16_6.x;
			    u_xlat16_19.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_19.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_19.xyz = u_xlat1.xyz + u_xlat16_19.xyz;
			    u_xlatu1.x = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_12 =  uint((-int(u_xlatu1.x)) + 30605);
			    u_xlat1.x = unpackHalf2x16(u_xlatu16_12).x;
			    u_xlat16_6.x = (-u_xlat1.x) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat1.x * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_19.xyz;
			    u_xlat1.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat3.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			    u_xlat1.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			    u_xlat1.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_18"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _Dithering_Params;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump ivec4 u_xlati16_0;
			vec3 u_xlat1;
			uvec4 u_xlatu1;
			mediump ivec4 u_xlati16_2;
			vec3 u_xlat3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump uint u_xlatu16_12;
			mediump vec3 u_xlat16_19;
			float u_xlat40;
			mediump float u_xlat16_40;
			uint u_xlatu40;
			bool u_xlatb40;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_4.xy = u_xlati16_0.zw;
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_9.xyz = max(u_xlat1.xyz, u_xlat16_6.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_9.xyz = (-u_xlat16_9.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_10.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_10.xyz = min(u_xlat7.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = min(u_xlat8.xyz, u_xlat16_10.xyz);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_10.xyz = min(u_xlat1.xyz, u_xlat16_10.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_10.xyz;
			    u_xlat16_10.xyz = vec3(1.0) / vec3(u_xlat16_11.xyz);
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_10.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_9.xyz);
			    u_xlat16_19.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_19.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu40 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat40 = unpackHalf2x16(u_xlatu40).x;
			    u_xlat16_6.x = u_xlat40 * u_xlat16_6.x;
			    u_xlat16_19.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_19.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_19.xyz = u_xlat1.xyz + u_xlat16_19.xyz;
			    u_xlatu1.x = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_12 =  uint((-int(u_xlatu1.x)) + 30605);
			    u_xlat1.x = unpackHalf2x16(u_xlatu16_12).x;
			    u_xlat16_6.x = (-u_xlat1.x) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat1.x * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_19.xyz;
			    u_xlat1.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_40 = texture(_BlueNoise_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat40 = u_xlat16_40 * 2.0 + -1.0;
			    u_xlat3.x = -abs(u_xlat40) + 1.0;
			    u_xlatb40 = u_xlat40>=0.0;
			    u_xlat40 = (u_xlatb40) ? 1.0 : -1.0;
			    u_xlat3.x = sqrt(u_xlat3.x);
			    u_xlat3.x = (-u_xlat3.x) + 1.0;
			    u_xlat40 = u_xlat40 * u_xlat3.x;
			    u_xlat1.xyz = vec3(u_xlat40) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_19"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _Dithering_Params;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump ivec4 u_xlati16_0;
			vec3 u_xlat1;
			uvec4 u_xlatu1;
			mediump ivec4 u_xlati16_2;
			vec3 u_xlat3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump uint u_xlatu16_12;
			mediump vec3 u_xlat16_19;
			float u_xlat40;
			mediump float u_xlat16_40;
			uint u_xlatu40;
			bool u_xlatb40;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_4.xy = u_xlati16_0.zw;
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_9.xyz = max(u_xlat1.xyz, u_xlat16_6.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_9.xyz = (-u_xlat16_9.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_10.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_10.xyz = min(u_xlat7.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = min(u_xlat8.xyz, u_xlat16_10.xyz);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_10.xyz = min(u_xlat1.xyz, u_xlat16_10.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_10.xyz;
			    u_xlat16_10.xyz = vec3(1.0) / vec3(u_xlat16_11.xyz);
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_10.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_9.xyz);
			    u_xlat16_19.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_19.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu40 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat40 = unpackHalf2x16(u_xlatu40).x;
			    u_xlat16_6.x = u_xlat40 * u_xlat16_6.x;
			    u_xlat16_19.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_19.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_19.xyz = u_xlat1.xyz + u_xlat16_19.xyz;
			    u_xlatu1.x = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_12 =  uint((-int(u_xlatu1.x)) + 30605);
			    u_xlat1.x = unpackHalf2x16(u_xlatu16_12).x;
			    u_xlat16_6.x = (-u_xlat1.x) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat1.x * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_19.xyz;
			    u_xlat1.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat3.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			    u_xlat1.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			    u_xlat1.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_40 = texture(_BlueNoise_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat40 = u_xlat16_40 * 2.0 + -1.0;
			    u_xlat3.x = -abs(u_xlat40) + 1.0;
			    u_xlatb40 = u_xlat40>=0.0;
			    u_xlat40 = (u_xlatb40) ? 1.0 : -1.0;
			    u_xlat3.x = sqrt(u_xlat3.x);
			    u_xlat3.x = (-u_xlat3.x) + 1.0;
			    u_xlat40 = u_xlat40 * u_xlat3.x;
			    u_xlat1.xyz = vec3(u_xlat40) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_20"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump ivec4 u_xlati16_0;
			vec3 u_xlat1;
			uvec4 u_xlatu1;
			mediump ivec4 u_xlati16_2;
			vec3 u_xlat3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump uint u_xlatu16_12;
			mediump vec3 u_xlat16_19;
			float u_xlat40;
			mediump float u_xlat16_40;
			uint u_xlatu40;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_4.xy = u_xlati16_0.zw;
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_9.xyz = max(u_xlat1.xyz, u_xlat16_6.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_9.xyz = (-u_xlat16_9.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_10.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_10.xyz = min(u_xlat7.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = min(u_xlat8.xyz, u_xlat16_10.xyz);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_10.xyz = min(u_xlat1.xyz, u_xlat16_10.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_10.xyz;
			    u_xlat16_10.xyz = vec3(1.0) / vec3(u_xlat16_11.xyz);
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_10.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_9.xyz);
			    u_xlat16_19.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_19.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu40 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat40 = unpackHalf2x16(u_xlatu40).x;
			    u_xlat16_6.x = u_xlat40 * u_xlat16_6.x;
			    u_xlat16_19.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_19.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_19.xyz = u_xlat1.xyz + u_xlat16_19.xyz;
			    u_xlatu1.x = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_12 =  uint((-int(u_xlatu1.x)) + 30605);
			    u_xlat1.x = unpackHalf2x16(u_xlatu16_12).x;
			    u_xlat16_6.x = (-u_xlat1.x) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat1.x * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_19.xyz;
			    u_xlat1.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_40 = texture(_Grain_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat16_6.x = u_xlat16_40 + -0.5;
			    u_xlat16_6.x = u_xlat16_6.x + u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat1.xyz * u_xlat16_6.xxx;
			    u_xlat3.xyz = u_xlat16_6.xyz * _Grain_Params.xxx;
			    u_xlat16_6.x = dot(u_xlat1.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat40 = sqrt(u_xlat16_6.x);
			    u_xlat40 = _Grain_Params.y * (-u_xlat40) + 1.0;
			    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat40) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_21"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump ivec4 u_xlati16_0;
			vec3 u_xlat1;
			uvec4 u_xlatu1;
			mediump ivec4 u_xlati16_2;
			vec3 u_xlat3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump uint u_xlatu16_12;
			mediump vec3 u_xlat16_19;
			float u_xlat40;
			mediump float u_xlat16_40;
			uint u_xlatu40;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_4.xy = u_xlati16_0.zw;
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_9.xyz = max(u_xlat1.xyz, u_xlat16_6.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_9.xyz = (-u_xlat16_9.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_10.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_10.xyz = min(u_xlat7.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = min(u_xlat8.xyz, u_xlat16_10.xyz);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_10.xyz = min(u_xlat1.xyz, u_xlat16_10.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_10.xyz;
			    u_xlat16_10.xyz = vec3(1.0) / vec3(u_xlat16_11.xyz);
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_10.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_9.xyz);
			    u_xlat16_19.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_19.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu40 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat40 = unpackHalf2x16(u_xlatu40).x;
			    u_xlat16_6.x = u_xlat40 * u_xlat16_6.x;
			    u_xlat16_19.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_19.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_19.xyz = u_xlat1.xyz + u_xlat16_19.xyz;
			    u_xlatu1.x = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_12 =  uint((-int(u_xlatu1.x)) + 30605);
			    u_xlat1.x = unpackHalf2x16(u_xlatu16_12).x;
			    u_xlat16_6.x = (-u_xlat1.x) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat1.x * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_19.xyz;
			    u_xlat1.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_40 = texture(_Grain_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat16_6.x = u_xlat16_40 + -0.5;
			    u_xlat16_6.x = u_xlat16_6.x + u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat1.xyz * u_xlat16_6.xxx;
			    u_xlat3.xyz = u_xlat16_6.xyz * _Grain_Params.xxx;
			    u_xlat16_6.x = dot(u_xlat1.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat40 = sqrt(u_xlat16_6.x);
			    u_xlat40 = _Grain_Params.y * (-u_xlat40) + 1.0;
			    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat40) + u_xlat1.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			    u_xlat1.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			    u_xlat1.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_22"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump ivec4 u_xlati16_0;
			vec3 u_xlat1;
			uvec4 u_xlatu1;
			mediump ivec4 u_xlati16_2;
			vec3 u_xlat3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump uint u_xlatu16_12;
			mediump vec3 u_xlat16_19;
			float u_xlat40;
			mediump float u_xlat16_40;
			uint u_xlatu40;
			bool u_xlatb40;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_4.xy = u_xlati16_0.zw;
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_9.xyz = max(u_xlat1.xyz, u_xlat16_6.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_9.xyz = (-u_xlat16_9.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_10.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_10.xyz = min(u_xlat7.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = min(u_xlat8.xyz, u_xlat16_10.xyz);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_10.xyz = min(u_xlat1.xyz, u_xlat16_10.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_10.xyz;
			    u_xlat16_10.xyz = vec3(1.0) / vec3(u_xlat16_11.xyz);
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_10.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_9.xyz);
			    u_xlat16_19.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_19.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu40 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat40 = unpackHalf2x16(u_xlatu40).x;
			    u_xlat16_6.x = u_xlat40 * u_xlat16_6.x;
			    u_xlat16_19.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_19.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_19.xyz = u_xlat1.xyz + u_xlat16_19.xyz;
			    u_xlatu1.x = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_12 =  uint((-int(u_xlatu1.x)) + 30605);
			    u_xlat1.x = unpackHalf2x16(u_xlatu16_12).x;
			    u_xlat16_6.x = (-u_xlat1.x) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat1.x * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_19.xyz;
			    u_xlat1.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_40 = texture(_Grain_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat16_6.x = u_xlat16_40 + -0.5;
			    u_xlat16_6.x = u_xlat16_6.x + u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat1.xyz * u_xlat16_6.xxx;
			    u_xlat3.xyz = u_xlat16_6.xyz * _Grain_Params.xxx;
			    u_xlat16_6.x = dot(u_xlat1.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat40 = sqrt(u_xlat16_6.x);
			    u_xlat40 = _Grain_Params.y * (-u_xlat40) + 1.0;
			    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat40) + u_xlat1.xyz;
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_40 = texture(_BlueNoise_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat40 = u_xlat16_40 * 2.0 + -1.0;
			    u_xlat3.x = -abs(u_xlat40) + 1.0;
			    u_xlatb40 = u_xlat40>=0.0;
			    u_xlat40 = (u_xlatb40) ? 1.0 : -1.0;
			    u_xlat3.x = sqrt(u_xlat3.x);
			    u_xlat3.x = (-u_xlat3.x) + 1.0;
			    u_xlat40 = u_xlat40 * u_xlat3.x;
			    u_xlat1.xyz = vec3(u_xlat40) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_23"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			mediump ivec4 u_xlati16_0;
			vec3 u_xlat1;
			uvec4 u_xlatu1;
			mediump ivec4 u_xlati16_2;
			vec3 u_xlat3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump uint u_xlatu16_12;
			mediump vec3 u_xlat16_19;
			float u_xlat40;
			mediump float u_xlat16_40;
			uint u_xlatu40;
			bool u_xlatb40;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_4.xy = u_xlati16_0.zw;
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_9.xyz = max(u_xlat1.xyz, u_xlat16_6.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_9.xyz = (-u_xlat16_9.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_10.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_10.xyz = min(u_xlat7.xyz, u_xlat16_10.xyz);
			    u_xlat16_10.xyz = min(u_xlat8.xyz, u_xlat16_10.xyz);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_10.xyz = min(u_xlat1.xyz, u_xlat16_10.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_10.xyz;
			    u_xlat16_10.xyz = vec3(1.0) / vec3(u_xlat16_11.xyz);
			    u_xlat16_9.xyz = u_xlat16_9.xyz * u_xlat16_10.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_9.xyz);
			    u_xlat16_19.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_19.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu40 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat40 = unpackHalf2x16(u_xlatu40).x;
			    u_xlat16_6.x = u_xlat40 * u_xlat16_6.x;
			    u_xlat16_19.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_19.xyz;
			    u_xlat16_19.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_19.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_19.xyz = u_xlat1.xyz + u_xlat16_19.xyz;
			    u_xlatu1.x = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_12 =  uint((-int(u_xlatu1.x)) + 30605);
			    u_xlat1.x = unpackHalf2x16(u_xlatu16_12).x;
			    u_xlat16_6.x = (-u_xlat1.x) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat1.x * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_19.xyz;
			    u_xlat1.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_40 = texture(_Grain_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat16_6.x = u_xlat16_40 + -0.5;
			    u_xlat16_6.x = u_xlat16_6.x + u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat1.xyz * u_xlat16_6.xxx;
			    u_xlat3.xyz = u_xlat16_6.xyz * _Grain_Params.xxx;
			    u_xlat16_6.x = dot(u_xlat1.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat40 = sqrt(u_xlat16_6.x);
			    u_xlat40 = _Grain_Params.y * (-u_xlat40) + 1.0;
			    u_xlat1.xyz = u_xlat3.xyz * vec3(u_xlat40) + u_xlat1.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			    u_xlat1.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			    u_xlat1.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_40 = texture(_BlueNoise_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat40 = u_xlat16_40 * 2.0 + -1.0;
			    u_xlat3.x = -abs(u_xlat40) + 1.0;
			    u_xlatb40 = u_xlat40>=0.0;
			    u_xlat40 = (u_xlatb40) ? 1.0 : -1.0;
			    u_xlat3.x = sqrt(u_xlat3.x);
			    u_xlat3.x = (-u_xlat3.x) + 1.0;
			    u_xlat40 = u_xlat40 * u_xlat3.x;
			    u_xlat1.xyz = vec3(u_xlat40) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_24"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			mediump ivec4 u_xlati16_0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bvec2 u_xlatb1;
			vec4 u_xlat2;
			mediump ivec4 u_xlati16_2;
			uvec4 u_xlatu2;
			vec3 u_xlat3;
			mediump vec3 u_xlat16_3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			vec3 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump uint u_xlatu16_13;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_24;
			float u_xlat29;
			uint u_xlatu29;
			mediump float u_xlat16_34;
			mediump float u_xlat16_48;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlati16_4 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_0.xy = u_xlati16_4.zw;
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat9.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_10.xyz = max(u_xlat16_6.xyz, u_xlat9.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_10.xyz = (-u_xlat16_10.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_11.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_11.xyz = min(u_xlat7.xyz, u_xlat16_11.xyz);
			    u_xlat16_11.xyz = min(u_xlat8.xyz, u_xlat16_11.xyz);
			    u_xlat16_12.xyz = u_xlat16_11.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_11.xyz = min(u_xlat9.xyz, u_xlat16_11.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_11.xyz;
			    u_xlat16_11.xyz = vec3(1.0) / vec3(u_xlat16_12.xyz);
			    u_xlat16_10.xyz = u_xlat16_10.xyz * u_xlat16_11.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_10.xyz);
			    u_xlat16_20.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu29 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat29 = unpackHalf2x16(u_xlatu29).x;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_20.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_20.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_20.xyz = u_xlat9.xyz + u_xlat16_20.xyz;
			    u_xlatu29 = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_13 =  uint((-int(u_xlatu29)) + 30605);
			    u_xlat29 = unpackHalf2x16(u_xlatu16_13).x;
			    u_xlat16_6.x = (-u_xlat29) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_20.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat16_6;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			        u_xlat16_6 = hlslcc_movcTemp;
			    }
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_6.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlati0 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati1 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_20.xyz = u_xlat3.xyz;
			    u_xlat16_20.xyz = clamp(u_xlat16_20.xyz, 0.0, 1.0);
			    u_xlat16_20.x = dot(u_xlat16_20.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_34 = min(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_20.z = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.xy = u_xlatu1.zw;
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_11.xyz = u_xlat1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_10.z = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_10.z, u_xlat16_24.x);
			    u_xlat16_6.z = min(u_xlat16_34, u_xlat16_24.x);
			    u_xlat16_24.xz = u_xlat16_20.xz + u_xlat16_10.xz;
			    u_xlat16_20.x = u_xlat16_20.z + u_xlat16_20.x;
			    u_xlat16_48 = max(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlat16_48 = max(u_xlat16_10.z, u_xlat16_48);
			    u_xlat16_6.x = max(u_xlat16_48, u_xlat16_6.x);
			    u_xlat16_0.yw = (-u_xlat16_24.zz) + u_xlat16_24.xx;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_10.x = u_xlat16_10.x + u_xlat16_20.x;
			    u_xlat16_20.x = (-u_xlat16_48) + u_xlat16_20.x;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_48 = u_xlat16_48 * 0.03125;
			    u_xlat16_48 = max(u_xlat16_48, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_20.x));
			    u_xlat16_0.xz = (-u_xlat16_20.xx);
			    u_xlat1.x = u_xlat16_48 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_10.xyz = u_xlat16_1.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_3.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_3.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_10.xyz = u_xlat16_10.xyz + u_xlat16_12.xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_12.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_11.xyz;
			    u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_6.y = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatb1.xy = lessThan(u_xlat16_6.yxyy, u_xlat16_6.zyzz).xy;
			    u_xlatb1.x = u_xlatb1.y || u_xlatb1.x;
			    SV_Target0.xyz = (u_xlatb1.x) ? u_xlat16_10.xyz : u_xlat16_11.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_25"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			mediump ivec4 u_xlati16_0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bvec2 u_xlatb1;
			vec4 u_xlat2;
			mediump ivec4 u_xlati16_2;
			uvec4 u_xlatu2;
			vec3 u_xlat3;
			mediump vec3 u_xlat16_3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			vec3 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump uint u_xlatu16_13;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_24;
			float u_xlat29;
			uint u_xlatu29;
			mediump float u_xlat16_34;
			mediump float u_xlat16_48;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlati16_4 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_0.xy = u_xlati16_4.zw;
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat9.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_10.xyz = max(u_xlat16_6.xyz, u_xlat9.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_10.xyz = (-u_xlat16_10.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_11.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_11.xyz = min(u_xlat7.xyz, u_xlat16_11.xyz);
			    u_xlat16_11.xyz = min(u_xlat8.xyz, u_xlat16_11.xyz);
			    u_xlat16_12.xyz = u_xlat16_11.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_11.xyz = min(u_xlat9.xyz, u_xlat16_11.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_11.xyz;
			    u_xlat16_11.xyz = vec3(1.0) / vec3(u_xlat16_12.xyz);
			    u_xlat16_10.xyz = u_xlat16_10.xyz * u_xlat16_11.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_10.xyz);
			    u_xlat16_20.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu29 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat29 = unpackHalf2x16(u_xlatu29).x;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_20.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_20.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_20.xyz = u_xlat9.xyz + u_xlat16_20.xyz;
			    u_xlatu29 = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_13 =  uint((-int(u_xlatu29)) + 30605);
			    u_xlat29 = unpackHalf2x16(u_xlatu16_13).x;
			    u_xlat16_6.x = (-u_xlat29) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_20.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat16_6;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			        u_xlat16_6 = hlslcc_movcTemp;
			    }
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_6.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlati0 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati1 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_20.xyz = u_xlat3.xyz;
			    u_xlat16_20.xyz = clamp(u_xlat16_20.xyz, 0.0, 1.0);
			    u_xlat16_20.x = dot(u_xlat16_20.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_34 = min(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_20.z = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.xy = u_xlatu1.zw;
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_11.xyz = u_xlat1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_10.z = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_10.z, u_xlat16_24.x);
			    u_xlat16_6.z = min(u_xlat16_34, u_xlat16_24.x);
			    u_xlat16_24.xz = u_xlat16_20.xz + u_xlat16_10.xz;
			    u_xlat16_20.x = u_xlat16_20.z + u_xlat16_20.x;
			    u_xlat16_48 = max(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlat16_48 = max(u_xlat16_10.z, u_xlat16_48);
			    u_xlat16_6.x = max(u_xlat16_48, u_xlat16_6.x);
			    u_xlat16_0.yw = (-u_xlat16_24.zz) + u_xlat16_24.xx;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_10.x = u_xlat16_10.x + u_xlat16_20.x;
			    u_xlat16_20.x = (-u_xlat16_48) + u_xlat16_20.x;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_48 = u_xlat16_48 * 0.03125;
			    u_xlat16_48 = max(u_xlat16_48, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_20.x));
			    u_xlat16_0.xz = (-u_xlat16_20.xx);
			    u_xlat1.x = u_xlat16_48 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_10.xyz = u_xlat16_1.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_3.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_3.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_10.xyz = u_xlat16_10.xyz + u_xlat16_12.xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_12.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_11.xyz;
			    u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_6.y = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatb1.xy = lessThan(u_xlat16_6.yxyy, u_xlat16_6.zyzz).xy;
			    u_xlatb1.x = u_xlatb1.y || u_xlatb1.x;
			    u_xlat16_6.xyz = (u_xlatb1.x) ? u_xlat16_10.xyz : u_xlat16_11.xyz;
			    u_xlat1.xyz = log2(u_xlat16_6.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec4 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_26"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _Dithering_Params;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			mediump ivec4 u_xlati16_0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bvec2 u_xlatb1;
			vec4 u_xlat2;
			mediump ivec4 u_xlati16_2;
			uvec4 u_xlatu2;
			vec3 u_xlat3;
			mediump vec3 u_xlat16_3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			vec3 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump uint u_xlatu16_13;
			float u_xlat15;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_24;
			float u_xlat29;
			uint u_xlatu29;
			mediump float u_xlat16_34;
			mediump float u_xlat16_48;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlati16_4 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_0.xy = u_xlati16_4.zw;
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat9.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_10.xyz = max(u_xlat16_6.xyz, u_xlat9.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_10.xyz = (-u_xlat16_10.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_11.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_11.xyz = min(u_xlat7.xyz, u_xlat16_11.xyz);
			    u_xlat16_11.xyz = min(u_xlat8.xyz, u_xlat16_11.xyz);
			    u_xlat16_12.xyz = u_xlat16_11.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_11.xyz = min(u_xlat9.xyz, u_xlat16_11.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_11.xyz;
			    u_xlat16_11.xyz = vec3(1.0) / vec3(u_xlat16_12.xyz);
			    u_xlat16_10.xyz = u_xlat16_10.xyz * u_xlat16_11.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_10.xyz);
			    u_xlat16_20.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu29 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat29 = unpackHalf2x16(u_xlatu29).x;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_20.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_20.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_20.xyz = u_xlat9.xyz + u_xlat16_20.xyz;
			    u_xlatu29 = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_13 =  uint((-int(u_xlatu29)) + 30605);
			    u_xlat29 = unpackHalf2x16(u_xlatu16_13).x;
			    u_xlat16_6.x = (-u_xlat29) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_20.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat16_6;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			        u_xlat16_6 = hlslcc_movcTemp;
			    }
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_6.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlati0 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati1 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_20.xyz = u_xlat3.xyz;
			    u_xlat16_20.xyz = clamp(u_xlat16_20.xyz, 0.0, 1.0);
			    u_xlat16_20.x = dot(u_xlat16_20.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_34 = min(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_20.z = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.xy = u_xlatu1.zw;
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_11.xyz = u_xlat1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_10.z = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_10.z, u_xlat16_24.x);
			    u_xlat16_6.z = min(u_xlat16_34, u_xlat16_24.x);
			    u_xlat16_24.xz = u_xlat16_20.xz + u_xlat16_10.xz;
			    u_xlat16_20.x = u_xlat16_20.z + u_xlat16_20.x;
			    u_xlat16_48 = max(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlat16_48 = max(u_xlat16_10.z, u_xlat16_48);
			    u_xlat16_6.x = max(u_xlat16_48, u_xlat16_6.x);
			    u_xlat16_0.yw = (-u_xlat16_24.zz) + u_xlat16_24.xx;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_10.x = u_xlat16_10.x + u_xlat16_20.x;
			    u_xlat16_20.x = (-u_xlat16_48) + u_xlat16_20.x;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_48 = u_xlat16_48 * 0.03125;
			    u_xlat16_48 = max(u_xlat16_48, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_20.x));
			    u_xlat16_0.xz = (-u_xlat16_20.xx);
			    u_xlat1.x = u_xlat16_48 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_10.xyz = u_xlat16_1.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_3.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_3.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_10.xyz = u_xlat16_10.xyz + u_xlat16_12.xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_12.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_11.xyz;
			    u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_6.y = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatb1.xy = lessThan(u_xlat16_6.yxyy, u_xlat16_6.zyzz).xy;
			    u_xlatb1.x = u_xlatb1.y || u_xlatb1.x;
			    u_xlat16_6.xyz = (u_xlatb1.x) ? u_xlat16_10.xyz : u_xlat16_11.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_1.x = texture(_BlueNoise_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat1.x = u_xlat16_1.x * 2.0 + -1.0;
			    u_xlat15 = -abs(u_xlat1.x) + 1.0;
			    u_xlatb1.x = u_xlat1.x>=0.0;
			    u_xlat1.x = (u_xlatb1.x) ? 1.0 : -1.0;
			    u_xlat15 = sqrt(u_xlat15);
			    u_xlat15 = (-u_xlat15) + 1.0;
			    u_xlat1.x = u_xlat15 * u_xlat1.x;
			    u_xlat1.xyz = u_xlat1.xxx * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat16_6.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_27"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _Dithering_Params;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			mediump ivec4 u_xlati16_0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bvec2 u_xlatb1;
			vec4 u_xlat2;
			mediump ivec4 u_xlati16_2;
			uvec4 u_xlatu2;
			vec3 u_xlat3;
			mediump vec3 u_xlat16_3;
			bvec3 u_xlatb3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			vec3 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump uint u_xlatu16_13;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_24;
			float u_xlat29;
			uint u_xlatu29;
			mediump float u_xlat16_34;
			float u_xlat43;
			mediump float u_xlat16_43;
			bool u_xlatb43;
			mediump float u_xlat16_48;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlati16_4 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_0.xy = u_xlati16_4.zw;
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat9.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_10.xyz = max(u_xlat16_6.xyz, u_xlat9.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_10.xyz = (-u_xlat16_10.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_11.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_11.xyz = min(u_xlat7.xyz, u_xlat16_11.xyz);
			    u_xlat16_11.xyz = min(u_xlat8.xyz, u_xlat16_11.xyz);
			    u_xlat16_12.xyz = u_xlat16_11.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_11.xyz = min(u_xlat9.xyz, u_xlat16_11.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_11.xyz;
			    u_xlat16_11.xyz = vec3(1.0) / vec3(u_xlat16_12.xyz);
			    u_xlat16_10.xyz = u_xlat16_10.xyz * u_xlat16_11.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_10.xyz);
			    u_xlat16_20.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu29 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat29 = unpackHalf2x16(u_xlatu29).x;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_20.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_20.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_20.xyz = u_xlat9.xyz + u_xlat16_20.xyz;
			    u_xlatu29 = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_13 =  uint((-int(u_xlatu29)) + 30605);
			    u_xlat29 = unpackHalf2x16(u_xlatu16_13).x;
			    u_xlat16_6.x = (-u_xlat29) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_20.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat16_6;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			        u_xlat16_6 = hlslcc_movcTemp;
			    }
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_6.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlati0 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati1 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_20.xyz = u_xlat3.xyz;
			    u_xlat16_20.xyz = clamp(u_xlat16_20.xyz, 0.0, 1.0);
			    u_xlat16_20.x = dot(u_xlat16_20.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_34 = min(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_20.z = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.xy = u_xlatu1.zw;
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_11.xyz = u_xlat1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_10.z = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_10.z, u_xlat16_24.x);
			    u_xlat16_6.z = min(u_xlat16_34, u_xlat16_24.x);
			    u_xlat16_24.xz = u_xlat16_20.xz + u_xlat16_10.xz;
			    u_xlat16_20.x = u_xlat16_20.z + u_xlat16_20.x;
			    u_xlat16_48 = max(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlat16_48 = max(u_xlat16_10.z, u_xlat16_48);
			    u_xlat16_6.x = max(u_xlat16_48, u_xlat16_6.x);
			    u_xlat16_0.yw = (-u_xlat16_24.zz) + u_xlat16_24.xx;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_10.x = u_xlat16_10.x + u_xlat16_20.x;
			    u_xlat16_20.x = (-u_xlat16_48) + u_xlat16_20.x;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_48 = u_xlat16_48 * 0.03125;
			    u_xlat16_48 = max(u_xlat16_48, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_20.x));
			    u_xlat16_0.xz = (-u_xlat16_20.xx);
			    u_xlat1.x = u_xlat16_48 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_10.xyz = u_xlat16_1.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_3.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_3.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_10.xyz = u_xlat16_10.xyz + u_xlat16_12.xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_12.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_11.xyz;
			    u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_6.y = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatb1.xy = lessThan(u_xlat16_6.yxyy, u_xlat16_6.zyzz).xy;
			    u_xlatb1.x = u_xlatb1.y || u_xlatb1.x;
			    u_xlat16_6.xyz = (u_xlatb1.x) ? u_xlat16_10.xyz : u_xlat16_11.xyz;
			    u_xlat1.xyz = log2(u_xlat16_6.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec4 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb3.x) ? u_xlat16_6.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb3.y) ? u_xlat16_6.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb3.z) ? u_xlat16_6.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_43 = texture(_BlueNoise_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat43 = u_xlat16_43 * 2.0 + -1.0;
			    u_xlat3.x = -abs(u_xlat43) + 1.0;
			    u_xlatb43 = u_xlat43>=0.0;
			    u_xlat43 = (u_xlatb43) ? 1.0 : -1.0;
			    u_xlat3.x = sqrt(u_xlat3.x);
			    u_xlat3.x = (-u_xlat3.x) + 1.0;
			    u_xlat43 = u_xlat43 * u_xlat3.x;
			    u_xlat1.xyz = vec3(u_xlat43) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_28"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			mediump ivec4 u_xlati16_0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bvec2 u_xlatb1;
			vec4 u_xlat2;
			mediump ivec4 u_xlati16_2;
			uvec4 u_xlatu2;
			vec3 u_xlat3;
			mediump vec3 u_xlat16_3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			vec3 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump uint u_xlatu16_13;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_24;
			float u_xlat29;
			uint u_xlatu29;
			mediump float u_xlat16_34;
			float u_xlat43;
			mediump float u_xlat16_48;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlati16_4 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_0.xy = u_xlati16_4.zw;
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat9.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_10.xyz = max(u_xlat16_6.xyz, u_xlat9.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_10.xyz = (-u_xlat16_10.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_11.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_11.xyz = min(u_xlat7.xyz, u_xlat16_11.xyz);
			    u_xlat16_11.xyz = min(u_xlat8.xyz, u_xlat16_11.xyz);
			    u_xlat16_12.xyz = u_xlat16_11.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_11.xyz = min(u_xlat9.xyz, u_xlat16_11.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_11.xyz;
			    u_xlat16_11.xyz = vec3(1.0) / vec3(u_xlat16_12.xyz);
			    u_xlat16_10.xyz = u_xlat16_10.xyz * u_xlat16_11.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_10.xyz);
			    u_xlat16_20.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu29 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat29 = unpackHalf2x16(u_xlatu29).x;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_20.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_20.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_20.xyz = u_xlat9.xyz + u_xlat16_20.xyz;
			    u_xlatu29 = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_13 =  uint((-int(u_xlatu29)) + 30605);
			    u_xlat29 = unpackHalf2x16(u_xlatu16_13).x;
			    u_xlat16_6.x = (-u_xlat29) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_20.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat16_6;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			        u_xlat16_6 = hlslcc_movcTemp;
			    }
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_6.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlati0 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati1 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_20.xyz = u_xlat3.xyz;
			    u_xlat16_20.xyz = clamp(u_xlat16_20.xyz, 0.0, 1.0);
			    u_xlat16_20.x = dot(u_xlat16_20.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_34 = min(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_20.z = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.xy = u_xlatu1.zw;
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_11.xyz = u_xlat1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_10.z = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_10.z, u_xlat16_24.x);
			    u_xlat16_6.z = min(u_xlat16_34, u_xlat16_24.x);
			    u_xlat16_24.xz = u_xlat16_20.xz + u_xlat16_10.xz;
			    u_xlat16_20.x = u_xlat16_20.z + u_xlat16_20.x;
			    u_xlat16_48 = max(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlat16_48 = max(u_xlat16_10.z, u_xlat16_48);
			    u_xlat16_6.x = max(u_xlat16_48, u_xlat16_6.x);
			    u_xlat16_0.yw = (-u_xlat16_24.zz) + u_xlat16_24.xx;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_10.x = u_xlat16_10.x + u_xlat16_20.x;
			    u_xlat16_20.x = (-u_xlat16_48) + u_xlat16_20.x;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_48 = u_xlat16_48 * 0.03125;
			    u_xlat16_48 = max(u_xlat16_48, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_20.x));
			    u_xlat16_0.xz = (-u_xlat16_20.xx);
			    u_xlat1.x = u_xlat16_48 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_10.xyz = u_xlat16_1.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_3.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_3.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_10.xyz = u_xlat16_10.xyz + u_xlat16_12.xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_12.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_11.xyz;
			    u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_6.y = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatb1.xy = lessThan(u_xlat16_6.yxyy, u_xlat16_6.zyzz).xy;
			    u_xlatb1.x = u_xlatb1.y || u_xlatb1.x;
			    u_xlat16_6.xyz = (u_xlatb1.x) ? u_xlat16_10.xyz : u_xlat16_11.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_48 = u_xlat16_1.x + -0.5;
			    u_xlat16_48 = u_xlat16_48 + u_xlat16_48;
			    u_xlat16_10.xyz = vec3(u_xlat16_48) * u_xlat16_6.xyz;
			    u_xlat1.xyz = u_xlat16_10.xyz * _Grain_Params.xxx;
			    u_xlat16_48 = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat43 = sqrt(u_xlat16_48);
			    u_xlat43 = _Grain_Params.y * (-u_xlat43) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat43) + u_xlat16_6.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_29"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			mediump ivec4 u_xlati16_0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bvec2 u_xlatb1;
			vec4 u_xlat2;
			mediump ivec4 u_xlati16_2;
			uvec4 u_xlatu2;
			vec3 u_xlat3;
			mediump vec3 u_xlat16_3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			vec3 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump uint u_xlatu16_13;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_24;
			float u_xlat29;
			uint u_xlatu29;
			mediump float u_xlat16_34;
			float u_xlat43;
			mediump float u_xlat16_48;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlati16_4 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_0.xy = u_xlati16_4.zw;
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat9.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_10.xyz = max(u_xlat16_6.xyz, u_xlat9.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_10.xyz = (-u_xlat16_10.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_11.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_11.xyz = min(u_xlat7.xyz, u_xlat16_11.xyz);
			    u_xlat16_11.xyz = min(u_xlat8.xyz, u_xlat16_11.xyz);
			    u_xlat16_12.xyz = u_xlat16_11.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_11.xyz = min(u_xlat9.xyz, u_xlat16_11.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_11.xyz;
			    u_xlat16_11.xyz = vec3(1.0) / vec3(u_xlat16_12.xyz);
			    u_xlat16_10.xyz = u_xlat16_10.xyz * u_xlat16_11.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_10.xyz);
			    u_xlat16_20.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu29 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat29 = unpackHalf2x16(u_xlatu29).x;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_20.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_20.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_20.xyz = u_xlat9.xyz + u_xlat16_20.xyz;
			    u_xlatu29 = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_13 =  uint((-int(u_xlatu29)) + 30605);
			    u_xlat29 = unpackHalf2x16(u_xlatu16_13).x;
			    u_xlat16_6.x = (-u_xlat29) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_20.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat16_6;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			        u_xlat16_6 = hlslcc_movcTemp;
			    }
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_6.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlati0 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati1 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_20.xyz = u_xlat3.xyz;
			    u_xlat16_20.xyz = clamp(u_xlat16_20.xyz, 0.0, 1.0);
			    u_xlat16_20.x = dot(u_xlat16_20.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_34 = min(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_20.z = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.xy = u_xlatu1.zw;
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_11.xyz = u_xlat1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_10.z = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_10.z, u_xlat16_24.x);
			    u_xlat16_6.z = min(u_xlat16_34, u_xlat16_24.x);
			    u_xlat16_24.xz = u_xlat16_20.xz + u_xlat16_10.xz;
			    u_xlat16_20.x = u_xlat16_20.z + u_xlat16_20.x;
			    u_xlat16_48 = max(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlat16_48 = max(u_xlat16_10.z, u_xlat16_48);
			    u_xlat16_6.x = max(u_xlat16_48, u_xlat16_6.x);
			    u_xlat16_0.yw = (-u_xlat16_24.zz) + u_xlat16_24.xx;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_10.x = u_xlat16_10.x + u_xlat16_20.x;
			    u_xlat16_20.x = (-u_xlat16_48) + u_xlat16_20.x;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_48 = u_xlat16_48 * 0.03125;
			    u_xlat16_48 = max(u_xlat16_48, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_20.x));
			    u_xlat16_0.xz = (-u_xlat16_20.xx);
			    u_xlat1.x = u_xlat16_48 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_10.xyz = u_xlat16_1.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_3.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_3.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_10.xyz = u_xlat16_10.xyz + u_xlat16_12.xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_12.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_11.xyz;
			    u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_6.y = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatb1.xy = lessThan(u_xlat16_6.yxyy, u_xlat16_6.zyzz).xy;
			    u_xlatb1.x = u_xlatb1.y || u_xlatb1.x;
			    u_xlat16_6.xyz = (u_xlatb1.x) ? u_xlat16_10.xyz : u_xlat16_11.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_48 = u_xlat16_1.x + -0.5;
			    u_xlat16_48 = u_xlat16_48 + u_xlat16_48;
			    u_xlat16_10.xyz = vec3(u_xlat16_48) * u_xlat16_6.xyz;
			    u_xlat1.xyz = u_xlat16_10.xyz * _Grain_Params.xxx;
			    u_xlat16_48 = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat43 = sqrt(u_xlat16_48);
			    u_xlat43 = _Grain_Params.y * (-u_xlat43) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat43) + u_xlat16_6.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			    u_xlat1.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			    u_xlat1.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_30"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			mediump ivec4 u_xlati16_0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bvec2 u_xlatb1;
			vec4 u_xlat2;
			mediump ivec4 u_xlati16_2;
			uvec4 u_xlatu2;
			vec3 u_xlat3;
			mediump vec3 u_xlat16_3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			vec3 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump uint u_xlatu16_13;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_24;
			float u_xlat29;
			uint u_xlatu29;
			mediump float u_xlat16_34;
			float u_xlat43;
			mediump float u_xlat16_43;
			bool u_xlatb43;
			mediump float u_xlat16_48;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlati16_4 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_0.xy = u_xlati16_4.zw;
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat9.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_10.xyz = max(u_xlat16_6.xyz, u_xlat9.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_10.xyz = (-u_xlat16_10.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_11.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_11.xyz = min(u_xlat7.xyz, u_xlat16_11.xyz);
			    u_xlat16_11.xyz = min(u_xlat8.xyz, u_xlat16_11.xyz);
			    u_xlat16_12.xyz = u_xlat16_11.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_11.xyz = min(u_xlat9.xyz, u_xlat16_11.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_11.xyz;
			    u_xlat16_11.xyz = vec3(1.0) / vec3(u_xlat16_12.xyz);
			    u_xlat16_10.xyz = u_xlat16_10.xyz * u_xlat16_11.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_10.xyz);
			    u_xlat16_20.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu29 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat29 = unpackHalf2x16(u_xlatu29).x;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_20.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_20.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_20.xyz = u_xlat9.xyz + u_xlat16_20.xyz;
			    u_xlatu29 = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_13 =  uint((-int(u_xlatu29)) + 30605);
			    u_xlat29 = unpackHalf2x16(u_xlatu16_13).x;
			    u_xlat16_6.x = (-u_xlat29) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_20.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat16_6;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			        u_xlat16_6 = hlslcc_movcTemp;
			    }
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_6.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlati0 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati1 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_20.xyz = u_xlat3.xyz;
			    u_xlat16_20.xyz = clamp(u_xlat16_20.xyz, 0.0, 1.0);
			    u_xlat16_20.x = dot(u_xlat16_20.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_34 = min(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_20.z = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.xy = u_xlatu1.zw;
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_11.xyz = u_xlat1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_10.z = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_10.z, u_xlat16_24.x);
			    u_xlat16_6.z = min(u_xlat16_34, u_xlat16_24.x);
			    u_xlat16_24.xz = u_xlat16_20.xz + u_xlat16_10.xz;
			    u_xlat16_20.x = u_xlat16_20.z + u_xlat16_20.x;
			    u_xlat16_48 = max(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlat16_48 = max(u_xlat16_10.z, u_xlat16_48);
			    u_xlat16_6.x = max(u_xlat16_48, u_xlat16_6.x);
			    u_xlat16_0.yw = (-u_xlat16_24.zz) + u_xlat16_24.xx;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_10.x = u_xlat16_10.x + u_xlat16_20.x;
			    u_xlat16_20.x = (-u_xlat16_48) + u_xlat16_20.x;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_48 = u_xlat16_48 * 0.03125;
			    u_xlat16_48 = max(u_xlat16_48, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_20.x));
			    u_xlat16_0.xz = (-u_xlat16_20.xx);
			    u_xlat1.x = u_xlat16_48 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_10.xyz = u_xlat16_1.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_3.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_3.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_10.xyz = u_xlat16_10.xyz + u_xlat16_12.xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_12.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_11.xyz;
			    u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_6.y = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatb1.xy = lessThan(u_xlat16_6.yxyy, u_xlat16_6.zyzz).xy;
			    u_xlatb1.x = u_xlatb1.y || u_xlatb1.x;
			    u_xlat16_6.xyz = (u_xlatb1.x) ? u_xlat16_10.xyz : u_xlat16_11.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_48 = u_xlat16_1.x + -0.5;
			    u_xlat16_48 = u_xlat16_48 + u_xlat16_48;
			    u_xlat16_10.xyz = vec3(u_xlat16_48) * u_xlat16_6.xyz;
			    u_xlat1.xyz = u_xlat16_10.xyz * _Grain_Params.xxx;
			    u_xlat16_48 = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat43 = sqrt(u_xlat16_48);
			    u_xlat43 = _Grain_Params.y * (-u_xlat43) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat43) + u_xlat16_6.xyz;
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_43 = texture(_BlueNoise_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat43 = u_xlat16_43 * 2.0 + -1.0;
			    u_xlat3.x = -abs(u_xlat43) + 1.0;
			    u_xlatb43 = u_xlat43>=0.0;
			    u_xlat43 = (u_xlatb43) ? 1.0 : -1.0;
			    u_xlat3.x = sqrt(u_xlat3.x);
			    u_xlat3.x = (-u_xlat3.x) + 1.0;
			    u_xlat43 = u_xlat43 * u_xlat3.x;
			    u_xlat1.xyz = vec3(u_xlat43) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_31"
			// Platform: Gles3x (index 0), compressed chunk: 0
			GLSLPROGRAM
			#ifdef VERTEX
			#version 310 es

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
			layout(location = 0) out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			uvec3 u_xlatu0;
			void main()
			{
			    u_xlatu0.x =  uint(int(bitfieldInsert(0, gl_VertexID, 1 & int(0x1F), 1)));
			    u_xlatu0.z = uint(gl_VertexID) & 2u;
			    u_xlat0.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    gl_Position.zw = vec2(-1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 310 es

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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			uniform 	vec4 _FsrRcasConstants;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			mediump ivec4 u_xlati16_0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bvec2 u_xlatb1;
			vec4 u_xlat2;
			mediump ivec4 u_xlati16_2;
			uvec4 u_xlatu2;
			vec3 u_xlat3;
			mediump vec3 u_xlat16_3;
			mediump ivec4 u_xlati16_4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			mediump vec3 u_xlat16_6;
			vec3 u_xlat7;
			vec3 u_xlat8;
			vec3 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump uint u_xlatu16_13;
			mediump vec3 u_xlat16_20;
			mediump vec3 u_xlat16_24;
			float u_xlat29;
			uint u_xlatu29;
			mediump float u_xlat16_34;
			float u_xlat43;
			mediump float u_xlat16_43;
			bool u_xlatb43;
			mediump float u_xlat16_48;
			void main()
			{
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlat1.xy = vs_TEXCOORD0.xy * _SourceSize.xy;
			    u_xlatu1.xy =  uvec2(ivec2(u_xlat1.xy));
			    u_xlati16_2 = ivec4(u_xlatu1.xyxy) + ivec4(0, int(0xFFFFFFFFu), int(0xFFFFFFFFu), 0);
			    u_xlati16_0.xy = u_xlati16_2.zw;
			    u_xlat3.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlati16_0.z = int(0);
			    u_xlati16_0.w = int(0);
			    u_xlati16_4 = ivec4(u_xlatu1.xyxy) + ivec4(0, 1, 1, 0);
			    u_xlati16_0.xy = u_xlati16_4.zw;
			    u_xlat5.xyz = texelFetch(_BlitTexture, u_xlati16_0.xy, u_xlati16_0.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlati16_2.z = int(0);
			    u_xlati16_2.w = int(0);
			    u_xlat7.xyz = texelFetch(_BlitTexture, u_xlati16_2.xy, u_xlati16_2.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat7.xyz);
			    u_xlati16_4.z = int(0);
			    u_xlati16_4.w = int(0);
			    u_xlat8.xyz = texelFetch(_BlitTexture, u_xlati16_4.xy, u_xlati16_4.w).xyz;
			    u_xlat16_6.xyz = max(u_xlat16_6.xyz, u_xlat8.xyz);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat9.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_10.xyz = max(u_xlat16_6.xyz, u_xlat9.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(4.0, 4.0, 4.0);
			    u_xlat16_6.xyz = vec3(1.0) / vec3(u_xlat16_6.xyz);
			    u_xlat16_10.xyz = (-u_xlat16_10.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat16_11.xyz = min(u_xlat3.xyz, u_xlat5.xyz);
			    u_xlat16_11.xyz = min(u_xlat7.xyz, u_xlat16_11.xyz);
			    u_xlat16_11.xyz = min(u_xlat8.xyz, u_xlat16_11.xyz);
			    u_xlat16_12.xyz = u_xlat16_11.xyz * vec3(4.0, 4.0, 4.0) + vec3(-4.0, -4.0, -4.0);
			    u_xlat16_11.xyz = min(u_xlat9.xyz, u_xlat16_11.xyz);
			    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat16_11.xyz;
			    u_xlat16_11.xyz = vec3(1.0) / vec3(u_xlat16_12.xyz);
			    u_xlat16_10.xyz = u_xlat16_10.xyz * u_xlat16_11.xyz;
			    u_xlat16_6.xyz = max((-u_xlat16_6.xyz), u_xlat16_10.xyz);
			    u_xlat16_20.x = max(u_xlat16_6.z, u_xlat16_6.y);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = min(u_xlat16_6.x, 0.0);
			    u_xlat16_6.x = max(u_xlat16_6.x, -0.1875);
			    u_xlatu29 = uint(floatBitsToUint(_FsrRcasConstants.y)) & 65535u;
			    u_xlat29 = unpackHalf2x16(u_xlatu29).x;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_20.xyz = u_xlat3.xyz * u_xlat16_6.xxx;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat7.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat8.xyz + u_xlat16_20.xyz;
			    u_xlat16_20.xyz = u_xlat16_6.xxx * u_xlat5.xyz + u_xlat16_20.xyz;
			    u_xlat16_6.x = u_xlat16_6.x * 4.0 + 1.0;
			    u_xlat16_20.xyz = u_xlat9.xyz + u_xlat16_20.xyz;
			    u_xlatu29 = packHalf2x16(vec2(u_xlat16_6.x, 0.0));
			    u_xlatu16_13 =  uint((-int(u_xlatu29)) + 30605);
			    u_xlat29 = unpackHalf2x16(u_xlatu16_13).x;
			    u_xlat16_6.x = (-u_xlat29) * u_xlat16_6.x + 2.0;
			    u_xlat16_6.x = u_xlat29 * u_xlat16_6.x;
			    u_xlat16_6.xyz = u_xlat16_6.xxx * u_xlat16_20.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat16_6.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_6.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat16_6.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec3 hlslcc_movcTemp = u_xlat16_6;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			        u_xlat16_6 = hlslcc_movcTemp;
			    }
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_6.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlati0 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati1 = ivec4(u_xlatu1.xyxy) + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(0u);
			    u_xlatu2.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_20.xyz = u_xlat3.xyz;
			    u_xlat16_20.xyz = clamp(u_xlat16_20.xyz, 0.0, 1.0);
			    u_xlat16_20.x = dot(u_xlat16_20.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_34 = min(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlat16_6.x = max(u_xlat16_20.x, u_xlat16_6.x);
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_20.z = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.xy = u_xlatu1.zw;
			    u_xlatu0.z = uint(0u);
			    u_xlatu0.w = uint(0u);
			    u_xlat3.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_10.xyz = u_xlat3.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlatu1.z = uint(0u);
			    u_xlatu1.w = uint(0u);
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_11.xyz = u_xlat1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_10.z = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_24.x = min(u_xlat16_10.z, u_xlat16_24.x);
			    u_xlat16_6.z = min(u_xlat16_34, u_xlat16_24.x);
			    u_xlat16_24.xz = u_xlat16_20.xz + u_xlat16_10.xz;
			    u_xlat16_20.x = u_xlat16_20.z + u_xlat16_20.x;
			    u_xlat16_48 = max(u_xlat16_20.z, u_xlat16_10.x);
			    u_xlat16_48 = max(u_xlat16_10.z, u_xlat16_48);
			    u_xlat16_6.x = max(u_xlat16_48, u_xlat16_6.x);
			    u_xlat16_0.yw = (-u_xlat16_24.zz) + u_xlat16_24.xx;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_10.x = u_xlat16_10.x + u_xlat16_20.x;
			    u_xlat16_20.x = (-u_xlat16_48) + u_xlat16_20.x;
			    u_xlat16_48 = u_xlat16_10.z + u_xlat16_10.x;
			    u_xlat16_48 = u_xlat16_48 * 0.03125;
			    u_xlat16_48 = max(u_xlat16_48, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_20.x));
			    u_xlat16_0.xz = (-u_xlat16_20.xx);
			    u_xlat1.x = u_xlat16_48 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_10.xyz = u_xlat16_1.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_3.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_3.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_3.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_10.xyz = u_xlat16_10.xyz + u_xlat16_12.xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_12.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_11.xyz = u_xlat16_10.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_11.xyz;
			    u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_6.y = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatb1.xy = lessThan(u_xlat16_6.yxyy, u_xlat16_6.zyzz).xy;
			    u_xlatb1.x = u_xlatb1.y || u_xlatb1.x;
			    u_xlat16_6.xyz = (u_xlatb1.x) ? u_xlat16_10.xyz : u_xlat16_11.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_48 = u_xlat16_1.x + -0.5;
			    u_xlat16_48 = u_xlat16_48 + u_xlat16_48;
			    u_xlat16_10.xyz = vec3(u_xlat16_48) * u_xlat16_6.xyz;
			    u_xlat1.xyz = u_xlat16_10.xyz * _Grain_Params.xxx;
			    u_xlat16_48 = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat43 = sqrt(u_xlat16_48);
			    u_xlat43 = _Grain_Params.y * (-u_xlat43) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat43) + u_xlat16_6.xyz;
			    u_xlat3.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat3.xyz = u_xlat3.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat3.xyz = exp2(u_xlat3.xyz);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_6.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb5.x) ? u_xlat16_6.x : u_xlat3.x;
			    u_xlat1.y = (u_xlatb5.y) ? u_xlat16_6.y : u_xlat3.y;
			    u_xlat1.z = (u_xlatb5.z) ? u_xlat16_6.z : u_xlat3.z;
			    u_xlat3.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_43 = texture(_BlueNoise_Texture, u_xlat3.xy, _GlobalMipBias.x).w;
			    u_xlat43 = u_xlat16_43 * 2.0 + -1.0;
			    u_xlat3.x = -abs(u_xlat43) + 1.0;
			    u_xlatb43 = u_xlat43>=0.0;
			    u_xlat43 = (u_xlatb43) ? 1.0 : -1.0;
			    u_xlat3.x = sqrt(u_xlat3.x);
			    u_xlat3.x = (-u_xlat3.x) + 1.0;
			    u_xlat43 = u_xlat43 * u_xlat3.x;
			    u_xlat1.xyz = vec3(u_xlat43) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_32"
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
		Pass {
			Name "GLSL_33"
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
			vec3 u_xlat1;
			bvec3 u_xlatb2;
			mediump vec3 u_xlat16_3;
			void main()
			{
			    u_xlat0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat1.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb2.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.x = (u_xlatb2.x) ? u_xlat16_3.x : u_xlat1.x;
			    u_xlat0.y = (u_xlatb2.y) ? u_xlat16_3.y : u_xlat1.y;
			    u_xlat0.z = (u_xlatb2.z) ? u_xlat16_3.z : u_xlat1.z;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_34"
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
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump float u_xlat16_0;
			bool u_xlatb0;
			float u_xlat1;
			mediump vec3 u_xlat16_1;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_0 = texture(_BlueNoise_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat0.x = u_xlat16_0 * 2.0 + -1.0;
			    u_xlat1 = -abs(u_xlat0.x) + 1.0;
			    u_xlatb0 = u_xlat0.x>=0.0;
			    u_xlat0.x = (u_xlatb0) ? 1.0 : -1.0;
			    u_xlat1 = sqrt(u_xlat1);
			    u_xlat1 = (-u_xlat1) + 1.0;
			    u_xlat0.x = u_xlat1 * u_xlat0.x;
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat0.xyz = u_xlat0.xxx * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat16_1.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_35"
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
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump float u_xlat16_0;
			bool u_xlatb0;
			vec3 u_xlat1;
			bvec3 u_xlatb2;
			mediump vec3 u_xlat16_3;
			vec3 u_xlat4;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_0 = texture(_BlueNoise_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat0.x = u_xlat16_0 * 2.0 + -1.0;
			    u_xlat4.x = -abs(u_xlat0.x) + 1.0;
			    u_xlatb0 = u_xlat0.x>=0.0;
			    u_xlat0.x = (u_xlatb0) ? 1.0 : -1.0;
			    u_xlat4.x = sqrt(u_xlat4.x);
			    u_xlat4.x = (-u_xlat4.x) + 1.0;
			    u_xlat0.x = u_xlat4.x * u_xlat0.x;
			    u_xlat4.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat1.xyz = log2(abs(u_xlat4.xyz));
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb2.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat4.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat4.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat4.x = (u_xlatb2.x) ? u_xlat16_3.x : u_xlat1.x;
			    u_xlat4.y = (u_xlatb2.y) ? u_xlat16_3.y : u_xlat1.y;
			    u_xlat4.z = (u_xlatb2.z) ? u_xlat16_3.z : u_xlat1.z;
			    u_xlat0.xyz = u_xlat0.xxx * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat4.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_36"
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
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			float u_xlat9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_0.x = texture(_Grain_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat16_1.x = u_xlat16_0.x + -0.5;
			    u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.x;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_0.xyz;
			    u_xlat2.xyz = u_xlat16_1.xyz * _Grain_Params.xxx;
			    u_xlat16_1.x = dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat9 = sqrt(u_xlat16_1.x);
			    u_xlat9 = _Grain_Params.y * (-u_xlat9) + 1.0;
			    u_xlat0.xyz = u_xlat2.xyz * vec3(u_xlat9) + u_xlat16_0.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_37"
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
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			bvec3 u_xlatb3;
			float u_xlat12;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_0.x = texture(_Grain_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat16_1.x = u_xlat16_0.x + -0.5;
			    u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.x;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_0.xyz;
			    u_xlat2.xyz = u_xlat16_1.xyz * _Grain_Params.xxx;
			    u_xlat16_1.x = dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat12 = sqrt(u_xlat16_1.x);
			    u_xlat12 = _Grain_Params.y * (-u_xlat12) + 1.0;
			    u_xlat0.xyz = u_xlat2.xyz * vec3(u_xlat12) + u_xlat16_0.xyz;
			    u_xlat2.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat2.xyz = u_xlat2.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat2.xyz = exp2(u_xlat2.xyz);
			    u_xlat2.xyz = u_xlat2.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.x = (u_xlatb3.x) ? u_xlat16_1.x : u_xlat2.x;
			    u_xlat0.y = (u_xlatb3.y) ? u_xlat16_1.y : u_xlat2.y;
			    u_xlat0.z = (u_xlatb3.z) ? u_xlat16_1.z : u_xlat2.z;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_38"
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
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			float u_xlat9;
			mediump float u_xlat16_9;
			bool u_xlatb9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_0.x = texture(_Grain_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat16_1.x = u_xlat16_0.x + -0.5;
			    u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.x;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_0.xyz;
			    u_xlat2.xyz = u_xlat16_1.xyz * _Grain_Params.xxx;
			    u_xlat16_1.x = dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat9 = sqrt(u_xlat16_1.x);
			    u_xlat9 = _Grain_Params.y * (-u_xlat9) + 1.0;
			    u_xlat0.xyz = u_xlat2.xyz * vec3(u_xlat9) + u_xlat16_0.xyz;
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_9 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat9 = u_xlat16_9 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat9) + 1.0;
			    u_xlatb9 = u_xlat9>=0.0;
			    u_xlat9 = (u_xlatb9) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat9 = u_xlat9 * u_xlat2.x;
			    u_xlat0.xyz = vec3(u_xlat9) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat0.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_39"
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
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			bvec3 u_xlatb3;
			float u_xlat12;
			mediump float u_xlat16_12;
			bool u_xlatb12;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_0.x = texture(_Grain_Texture, u_xlat0.xy, _GlobalMipBias.x).w;
			    u_xlat16_1.x = u_xlat16_0.x + -0.5;
			    u_xlat16_1.x = u_xlat16_1.x + u_xlat16_1.x;
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = u_xlat16_1.xxx * u_xlat16_0.xyz;
			    u_xlat2.xyz = u_xlat16_1.xyz * _Grain_Params.xxx;
			    u_xlat16_1.x = dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat12 = sqrt(u_xlat16_1.x);
			    u_xlat12 = _Grain_Params.y * (-u_xlat12) + 1.0;
			    u_xlat0.xyz = u_xlat2.xyz * vec3(u_xlat12) + u_xlat16_0.xyz;
			    u_xlat2.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat2.xyz = u_xlat2.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat2.xyz = exp2(u_xlat2.xyz);
			    u_xlat2.xyz = u_xlat2.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb3.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.x = (u_xlatb3.x) ? u_xlat16_1.x : u_xlat2.x;
			    u_xlat0.y = (u_xlatb3.y) ? u_xlat16_1.y : u_xlat2.y;
			    u_xlat0.z = (u_xlatb3.z) ? u_xlat16_1.z : u_xlat2.z;
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_12 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat12 = u_xlat16_12 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat12) + 1.0;
			    u_xlatb12 = u_xlat12>=0.0;
			    u_xlat12 = (u_xlatb12) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat12 = u_xlat12 * u_xlat2.x;
			    u_xlat0.xyz = vec3(u_xlat12) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat0.xyz;
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_40"
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
			uniform 	vec4 _SourceSize;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(uint(0u));
			    u_xlatu1.w = uint(uint(0u));
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    SV_Target0.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_41"
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
			uniform 	vec4 _SourceSize;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			bvec3 u_xlatb2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(uint(0u));
			    u_xlatu1.w = uint(uint(0u));
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xyz = log2(u_xlat16_3.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb2.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_3.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec4 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? u_xlat16_3.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? u_xlat16_3.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb2.z) ? u_xlat16_3.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_42"
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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			float u_xlat8;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(uint(0u));
			    u_xlatu1.w = uint(uint(0u));
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_1.x = texture(_BlueNoise_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat1.x = u_xlat16_1.x * 2.0 + -1.0;
			    u_xlat8 = -abs(u_xlat1.x) + 1.0;
			    u_xlatb1 = u_xlat1.x>=0.0;
			    u_xlat1.x = (u_xlatb1) ? 1.0 : -1.0;
			    u_xlat8 = sqrt(u_xlat8);
			    u_xlat8 = (-u_xlat8) + 1.0;
			    u_xlat1.x = u_xlat8 * u_xlat1.x;
			    u_xlat1.xyz = u_xlat1.xxx * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat16_3.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_43"
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
			uniform 	vec4 _SourceSize;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			bvec3 u_xlatb2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			float u_xlat22;
			mediump float u_xlat16_22;
			bool u_xlatb22;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(uint(0u));
			    u_xlatu1.w = uint(uint(0u));
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xyz = log2(u_xlat16_3.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat1.xyz = exp2(u_xlat1.xyz);
			    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb2.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_3.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    {
			        vec4 hlslcc_movcTemp = u_xlat1;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? u_xlat16_3.x : u_xlat1.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? u_xlat16_3.y : u_xlat1.y;
			        hlslcc_movcTemp.z = (u_xlatb2.z) ? u_xlat16_3.z : u_xlat1.z;
			        u_xlat1 = hlslcc_movcTemp;
			    }
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_22 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat22 = u_xlat16_22 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat22) + 1.0;
			    u_xlatb22 = u_xlat22>=0.0;
			    u_xlat22 = (u_xlatb22) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat22 = u_xlat22 * u_xlat2.x;
			    u_xlat1.xyz = vec3(u_xlat22) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_44"
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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			float u_xlat22;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(uint(0u));
			    u_xlatu1.w = uint(uint(0u));
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_24 = u_xlat16_1.x + -0.5;
			    u_xlat16_24 = u_xlat16_24 + u_xlat16_24;
			    u_xlat16_4.xyz = vec3(u_xlat16_24) * u_xlat16_3.xyz;
			    u_xlat1.xyz = u_xlat16_4.xyz * _Grain_Params.xxx;
			    u_xlat16_24 = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat22 = sqrt(u_xlat16_24);
			    u_xlat22 = _Grain_Params.y * (-u_xlat22) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat22) + u_xlat16_3.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_45"
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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bvec3 u_xlatb7;
			bool u_xlatb9;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump float u_xlat16_19;
			float u_xlat25;
			mediump float u_xlat16_27;
			mediump float u_xlat16_29;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_11.xyz = u_xlat2.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_11.x = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_19 = u_xlat16_11.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(uint(0u));
			    u_xlatu1.w = uint(uint(0u));
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_27 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_12.x = u_xlat16_27 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_19) + (-u_xlat16_12.xx);
			    u_xlat16_19 = u_xlat16_27 + u_xlat16_3.x;
			    u_xlat16_12.x = u_xlat16_11.x + u_xlat16_4.x;
			    u_xlat16_12.x = u_xlat16_19 + (-u_xlat16_12.x);
			    u_xlat16_19 = u_xlat16_11.x + u_xlat16_19;
			    u_xlat16_19 = u_xlat16_4.x + u_xlat16_19;
			    u_xlat16_19 = u_xlat16_19 * 0.03125;
			    u_xlat16_19 = max(u_xlat16_19, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_12.x));
			    u_xlat16_0.xz = (-u_xlat16_12.xx);
			    u_xlat1.x = u_xlat16_19 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_12.xyz = u_xlat16_12.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_12.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_12.xyz = u_xlat16_12.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_19 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_29 = min(u_xlat16_11.x, u_xlat16_27);
			    u_xlat16_11.x = max(u_xlat16_11.x, u_xlat16_27);
			    u_xlat16_11.x = max(u_xlat16_4.x, u_xlat16_11.x);
			    u_xlat16_27 = min(u_xlat16_4.x, u_xlat16_29);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_29 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_11.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_19;
			    u_xlat16_3.x = min(u_xlat16_27, u_xlat16_29);
			    u_xlatb9 = u_xlat16_19<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb9;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_12.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_27 = u_xlat16_1.x + -0.5;
			    u_xlat16_27 = u_xlat16_27 + u_xlat16_27;
			    u_xlat16_4.xyz = vec3(u_xlat16_27) * u_xlat16_3.xyz;
			    u_xlat1.xyz = u_xlat16_4.xyz * _Grain_Params.xxx;
			    u_xlat16_27 = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat25 = sqrt(u_xlat16_27);
			    u_xlat25 = _Grain_Params.y * (-u_xlat25) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat25) + u_xlat16_3.xyz;
			    u_xlat2.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat2.xyz = u_xlat2.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat2.xyz = exp2(u_xlat2.xyz);
			    u_xlat2.xyz = u_xlat2.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb7.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb7.x) ? u_xlat16_3.x : u_xlat2.x;
			    u_xlat1.y = (u_xlatb7.y) ? u_xlat16_3.y : u_xlat2.y;
			    u_xlat1.z = (u_xlatb7.z) ? u_xlat16_3.z : u_xlat2.z;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_46"
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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bool u_xlatb8;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			mediump float u_xlat16_17;
			float u_xlat22;
			mediump float u_xlat16_22;
			bool u_xlatb22;
			mediump float u_xlat16_24;
			mediump float u_xlat16_26;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_10.xyz = u_xlat2.xyz;
			    u_xlat16_10.xyz = clamp(u_xlat16_10.xyz, 0.0, 1.0);
			    u_xlat16_10.x = dot(u_xlat16_10.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(uint(0u));
			    u_xlatu1.w = uint(uint(0u));
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_24 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_11.x = u_xlat16_24 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_17) + (-u_xlat16_11.xx);
			    u_xlat16_17 = u_xlat16_24 + u_xlat16_3.x;
			    u_xlat16_11.x = u_xlat16_10.x + u_xlat16_4.x;
			    u_xlat16_11.x = u_xlat16_17 + (-u_xlat16_11.x);
			    u_xlat16_17 = u_xlat16_10.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_4.x + u_xlat16_17;
			    u_xlat16_17 = u_xlat16_17 * 0.03125;
			    u_xlat16_17 = max(u_xlat16_17, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_11.x));
			    u_xlat16_0.xz = (-u_xlat16_11.xx);
			    u_xlat1.x = u_xlat16_17 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_11.xyz = u_xlat16_1.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_11.xyz = u_xlat16_11.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_11.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_11.xyz = u_xlat16_11.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_17 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_10.x, u_xlat16_24);
			    u_xlat16_10.x = max(u_xlat16_4.x, u_xlat16_10.x);
			    u_xlat16_24 = min(u_xlat16_4.x, u_xlat16_26);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_26 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_10.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_17;
			    u_xlat16_3.x = min(u_xlat16_24, u_xlat16_26);
			    u_xlatb8 = u_xlat16_17<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb8;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_11.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_24 = u_xlat16_1.x + -0.5;
			    u_xlat16_24 = u_xlat16_24 + u_xlat16_24;
			    u_xlat16_4.xyz = vec3(u_xlat16_24) * u_xlat16_3.xyz;
			    u_xlat1.xyz = u_xlat16_4.xyz * _Grain_Params.xxx;
			    u_xlat16_24 = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat22 = sqrt(u_xlat16_24);
			    u_xlat22 = _Grain_Params.y * (-u_xlat22) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat22) + u_xlat16_3.xyz;
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_22 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat22 = u_xlat16_22 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat22) + 1.0;
			    u_xlatb22 = u_xlat22>=0.0;
			    u_xlat22 = (u_xlatb22) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat22 = u_xlat22 * u_xlat2.x;
			    u_xlat1.xyz = vec3(u_xlat22) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
		Pass {
			Name "GLSL_47"
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
			uniform 	vec4 _SourceSize;
			uniform 	vec2 _Grain_Params;
			uniform 	vec4 _Grain_TilingParams;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Grain_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			ivec4 u_xlati0;
			uvec4 u_xlatu0;
			vec4 u_xlat1;
			mediump vec3 u_xlat16_1;
			ivec4 u_xlati1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec4 u_xlat2;
			mediump vec3 u_xlat16_2;
			uvec4 u_xlatu2;
			mediump vec3 u_xlat16_3;
			mediump vec3 u_xlat16_4;
			mediump vec3 u_xlat16_5;
			mediump vec3 u_xlat16_6;
			bvec3 u_xlatb7;
			bool u_xlatb9;
			mediump vec3 u_xlat16_11;
			mediump vec3 u_xlat16_12;
			mediump float u_xlat16_19;
			float u_xlat25;
			mediump float u_xlat16_25;
			bool u_xlatb25;
			mediump float u_xlat16_27;
			mediump float u_xlat16_29;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.xyxy * _SourceSize.xyxy;
			    u_xlati0 = ivec4(u_xlat0);
			    u_xlati1 = u_xlati0.zwzw + ivec4(int(0xFFFFFFFFu), int(0xFFFFFFFFu), 1, int(0xFFFFFFFFu));
			    u_xlati0 = u_xlati0 + ivec4(int(0xFFFFFFFFu), 1, 1, 1);
			    u_xlat0 = vec4(u_xlati0);
			    u_xlat0 = max(u_xlat0, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat1 = vec4(u_xlati1);
			    u_xlat1 = max(u_xlat1, vec4(0.0, 0.0, 0.0, 0.0));
			    u_xlat2 = _SourceSize.xyxy + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat1 = min(u_xlat1, u_xlat2);
			    u_xlat0 = min(u_xlat0, u_xlat2);
			    u_xlatu0 =  uvec4(ivec4(u_xlat0.zwxy));
			    u_xlatu1 =  uvec4(ivec4(u_xlat1.zwxy));
			    u_xlatu2.xy = u_xlatu1.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_3.xyz = u_xlat2.xyz;
			    u_xlat16_3.xyz = clamp(u_xlat16_3.xyz, 0.0, 1.0);
			    u_xlat16_3.x = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu2.xy = u_xlatu0.zw;
			    u_xlatu2.z = uint(uint(0u));
			    u_xlatu2.w = uint(uint(0u));
			    u_xlat2.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu2.xy), int(u_xlatu2.w)).xyz;
			    u_xlat16_11.xyz = u_xlat2.xyz;
			    u_xlat16_11.xyz = clamp(u_xlat16_11.xyz, 0.0, 1.0);
			    u_xlat16_11.x = dot(u_xlat16_11.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_19 = u_xlat16_11.x + u_xlat16_3.x;
			    u_xlatu1.z = uint(uint(0u));
			    u_xlatu1.w = uint(uint(0u));
			    u_xlat1.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).xyz;
			    u_xlat16_4.xyz = u_xlat1.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_27 = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlatu0.z = uint(uint(0u));
			    u_xlatu0.w = uint(uint(0u));
			    u_xlat0.xyz = texelFetch(_BlitTexture, ivec2(u_xlatu0.xy), int(u_xlatu0.w)).xyz;
			    u_xlat16_4.xyz = u_xlat0.xyz;
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_4.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_12.x = u_xlat16_27 + u_xlat16_4.x;
			    u_xlat16_0.yw = vec2(u_xlat16_19) + (-u_xlat16_12.xx);
			    u_xlat16_19 = u_xlat16_27 + u_xlat16_3.x;
			    u_xlat16_12.x = u_xlat16_11.x + u_xlat16_4.x;
			    u_xlat16_12.x = u_xlat16_19 + (-u_xlat16_12.x);
			    u_xlat16_19 = u_xlat16_11.x + u_xlat16_19;
			    u_xlat16_19 = u_xlat16_4.x + u_xlat16_19;
			    u_xlat16_19 = u_xlat16_19 * 0.03125;
			    u_xlat16_19 = max(u_xlat16_19, 0.0078125);
			    u_xlat1.x = min(abs(u_xlat16_0.w), abs(u_xlat16_12.x));
			    u_xlat16_0.xz = (-u_xlat16_12.xx);
			    u_xlat1.x = u_xlat16_19 + u_xlat1.x;
			    u_xlat1.x = float(1.0) / float(u_xlat1.x);
			    u_xlat0 = u_xlat16_0 * u_xlat1.xxxx;
			    u_xlat0 = max(u_xlat0, vec4(-8.0, -8.0, -8.0, -8.0));
			    u_xlat0 = min(u_xlat0, vec4(8.0, 8.0, 8.0, 8.0));
			    u_xlat0 = u_xlat0 * _SourceSize.zwzw;
			    u_xlat1 = u_xlat0.zwzw * vec4(-0.5, -0.5, -0.166666672, -0.166666672) + vs_TEXCOORD0.xyxy;
			    u_xlat0 = u_xlat0 * vec4(0.166666672, 0.166666672, 0.5, 0.5) + vs_TEXCOORD0.xyxy;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat1.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat1.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_12.xyz = u_xlat16_1.xyz;
			    u_xlat16_12.xyz = clamp(u_xlat16_12.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_2.xyz;
			    u_xlat16_5.xyz = clamp(u_xlat16_5.xyz, 0.0, 1.0);
			    u_xlat16_1.xyz = texture(_BlitTexture, u_xlat0.zw, _GlobalMipBias.x).xyz;
			    u_xlat16_2.xyz = texture(_BlitTexture, u_xlat0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_2.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_12.xyz = u_xlat16_12.xyz + u_xlat16_6.xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
			    u_xlat16_5.xyz = u_xlat16_5.xyz * vec3(0.25, 0.25, 0.25);
			    u_xlat16_5.xyz = u_xlat16_12.xyz * vec3(0.25, 0.25, 0.25) + u_xlat16_5.xyz;
			    u_xlat16_12.xyz = u_xlat16_12.xyz * vec3(0.5, 0.5, 0.5);
			    u_xlat16_19 = dot(u_xlat16_5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_29 = min(u_xlat16_11.x, u_xlat16_27);
			    u_xlat16_11.x = max(u_xlat16_11.x, u_xlat16_27);
			    u_xlat16_11.x = max(u_xlat16_4.x, u_xlat16_11.x);
			    u_xlat16_27 = min(u_xlat16_4.x, u_xlat16_29);
			    u_xlat16_1.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_6.xyz = u_xlat16_1.xyz;
			    u_xlat16_6.xyz = clamp(u_xlat16_6.xyz, 0.0, 1.0);
			    u_xlat16_4.x = dot(u_xlat16_6.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_29 = min(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_3.x, u_xlat16_4.x);
			    u_xlat16_3.x = max(u_xlat16_11.x, u_xlat16_3.x);
			    u_xlatb1 = u_xlat16_3.x<u_xlat16_19;
			    u_xlat16_3.x = min(u_xlat16_27, u_xlat16_29);
			    u_xlatb9 = u_xlat16_19<u_xlat16_3.x;
			    u_xlatb1 = u_xlatb1 || u_xlatb9;
			    u_xlat16_3.xyz = (bool(u_xlatb1)) ? u_xlat16_12.xyz : u_xlat16_5.xyz;
			    u_xlat1.xy = vs_TEXCOORD0.xy * _Grain_TilingParams.xy + _Grain_TilingParams.zw;
			    u_xlat16_1.x = texture(_Grain_Texture, u_xlat1.xy, _GlobalMipBias.x).w;
			    u_xlat16_27 = u_xlat16_1.x + -0.5;
			    u_xlat16_27 = u_xlat16_27 + u_xlat16_27;
			    u_xlat16_4.xyz = vec3(u_xlat16_27) * u_xlat16_3.xyz;
			    u_xlat1.xyz = u_xlat16_4.xyz * _Grain_Params.xxx;
			    u_xlat16_27 = dot(u_xlat16_3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat25 = sqrt(u_xlat16_27);
			    u_xlat25 = _Grain_Params.y * (-u_xlat25) + 1.0;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(u_xlat25) + u_xlat16_3.xyz;
			    u_xlat2.xyz = log2(abs(u_xlat1.xyz));
			    u_xlat2.xyz = u_xlat2.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat2.xyz = exp2(u_xlat2.xyz);
			    u_xlat2.xyz = u_xlat2.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    u_xlatb7.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat1.xyzx).xyz;
			    u_xlat16_3.xyz = u_xlat1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat1.x = (u_xlatb7.x) ? u_xlat16_3.x : u_xlat2.x;
			    u_xlat1.y = (u_xlatb7.y) ? u_xlat16_3.y : u_xlat2.y;
			    u_xlat1.z = (u_xlatb7.z) ? u_xlat16_3.z : u_xlat2.z;
			    u_xlat2.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_25 = texture(_BlueNoise_Texture, u_xlat2.xy, _GlobalMipBias.x).w;
			    u_xlat25 = u_xlat16_25 * 2.0 + -1.0;
			    u_xlat2.x = -abs(u_xlat25) + 1.0;
			    u_xlatb25 = u_xlat25>=0.0;
			    u_xlat25 = (u_xlatb25) ? 1.0 : -1.0;
			    u_xlat2.x = sqrt(u_xlat2.x);
			    u_xlat2.x = (-u_xlat2.x) + 1.0;
			    u_xlat25 = u_xlat25 * u_xlat2.x;
			    u_xlat1.xyz = vec3(u_xlat25) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat1.xyz;
			    SV_Target0.xyz = u_xlat1.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}