Shader "Hidden/Universal Render Pipeline/UberPost" {
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
			#ifdef GL_EXT_shader_texture_lod
			#extension GL_EXT_shader_texture_lod : enable
			#endif

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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _UserLut_Params;
			uniform 	mediump vec4 _Vignette_Params1;
			uniform 	vec4 _Vignette_Params2;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _InternalLut;
			UNITY_LOCATION(2) uniform mediump sampler2D _UserLut;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			bvec3 u_xlatb0;
			vec3 u_xlat1;
			mediump vec3 u_xlat16_1;
			mediump vec3 u_xlat16_2;
			mediump vec3 u_xlat16_3;
			vec4 u_xlat4;
			mediump vec3 u_xlat16_4;
			bvec3 u_xlatb4;
			vec2 u_xlat5;
			mediump vec3 u_xlat16_5;
			vec2 u_xlat6;
			mediump vec3 u_xlat16_7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_8;
			float u_xlat24;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_0.xyz, vec3(100.0, 100.0, 100.0));
			    u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			    u_xlat16_3.xyz = u_xlat16_1.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			    u_xlat0.xyz = log2(abs(u_xlat16_3.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_1.xyzx).xyz;
			    u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_2.x : u_xlat0.x;
			    u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_2.y : u_xlat0.y;
			    u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_2.z : u_xlat0.z;
			    u_xlatb0.x = 0.0<_Vignette_Params2.z;
			    if(u_xlatb0.x){
			        u_xlat0.xy = vs_TEXCOORD0.xy + (-_Vignette_Params2.xy);
			        u_xlat0.yz = abs(u_xlat0.xy) * _Vignette_Params2.zz;
			        u_xlat0.x = u_xlat0.y * _Vignette_Params1.w;
			        u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			        u_xlat0.x = (-u_xlat0.x) + 1.0;
			        u_xlat0.x = max(u_xlat0.x, 0.0);
			        u_xlat0.x = log2(u_xlat0.x);
			        u_xlat0.x = u_xlat0.x * _Vignette_Params2.w;
			        u_xlat0.x = exp2(u_xlat0.x);
			        u_xlat8.xyz = (-_Vignette_Params1.xyz) + vec3(1.0, 1.0, 1.0);
			        u_xlat0.xyz = u_xlat0.xxx * u_xlat8.xyz + _Vignette_Params1.xyz;
			        u_xlat1.xyz = u_xlat0.xyz * u_xlat16_1.xyz;
			        u_xlat16_1.xyz = u_xlat1.xyz;
			    }
			    u_xlat16_1.xyz = u_xlat16_1.xyz * _Lut_Params.www;
			    u_xlat16_1.xyz = clamp(u_xlat16_1.xyz, 0.0, 1.0);
			    u_xlatb0.x = 0.0<_UserLut_Params.w;
			    if(u_xlatb0.x){
			        u_xlatb0.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_1.xyzx).xyz;
			        u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			        u_xlat4.xyz = log2(u_xlat16_1.xyz);
			        u_xlat4.xyz = u_xlat4.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			        u_xlat4.xyz = exp2(u_xlat4.xyz);
			        u_xlat4.xyz = u_xlat4.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			        u_xlat0.x = (u_xlatb0.x) ? u_xlat16_2.x : u_xlat4.x;
			        u_xlat0.y = (u_xlatb0.y) ? u_xlat16_2.y : u_xlat4.y;
			        u_xlat0.z = (u_xlatb0.z) ? u_xlat16_2.z : u_xlat4.z;
			        u_xlat4.xyz = u_xlat0.zxy * _UserLut_Params.zzz;
			        u_xlat24 = floor(u_xlat4.x);
			        u_xlat4.xw = _UserLut_Params.xy * vec2(0.5, 0.5);
			        u_xlat4.yz = u_xlat4.yz * _UserLut_Params.xy + u_xlat4.xw;
			        u_xlat4.x = u_xlat24 * _UserLut_Params.y + u_xlat4.y;
			        u_xlat16_5.xyz = textureLod(_UserLut, u_xlat4.xz, 0.0).xyz;
			        u_xlat6.x = _UserLut_Params.y;
			        u_xlat6.y = 0.0;
			        u_xlat4.xy = u_xlat4.xz + u_xlat6.xy;
			        u_xlat16_4.xyz = textureLod(_UserLut, u_xlat4.xy, 0.0).xyz;
			        u_xlat24 = u_xlat0.z * _UserLut_Params.z + (-u_xlat24);
			        u_xlat4.xyz = (-u_xlat16_5.xyz) + u_xlat16_4.xyz;
			        u_xlat4.xyz = vec3(u_xlat24) * u_xlat4.xyz + u_xlat16_5.xyz;
			        u_xlat4.xyz = (-u_xlat0.xyz) + u_xlat4.xyz;
			        u_xlat0.xyz = _UserLut_Params.www * u_xlat4.xyz + u_xlat0.xyz;
			        u_xlat16_2.xyz = min(u_xlat0.xyz, vec3(100.0, 100.0, 100.0));
			        u_xlat16_3.xyz = u_xlat16_2.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			        u_xlat16_7.xyz = u_xlat16_2.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			        u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			        u_xlat0.xyz = log2(abs(u_xlat16_7.xyz));
			        u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			        u_xlat0.xyz = exp2(u_xlat0.xyz);
			        u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_2.xyzx).xyz;
			        u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_3.x : u_xlat0.x;
			        u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_3.y : u_xlat0.y;
			        u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_3.z : u_xlat0.z;
			    }
			    u_xlat0.xyz = u_xlat16_1.zxy * _Lut_Params.zzz;
			    u_xlat0.x = floor(u_xlat0.x);
			    u_xlat4.xy = _Lut_Params.xy * vec2(0.5, 0.5);
			    u_xlat4.yz = u_xlat0.yz * _Lut_Params.xy + u_xlat4.xy;
			    u_xlat4.x = u_xlat0.x * _Lut_Params.y + u_xlat4.y;
			    u_xlat16_8.xyz = textureLod(_InternalLut, u_xlat4.xz, 0.0).xyz;
			    u_xlat5.x = _Lut_Params.y;
			    u_xlat5.y = 0.0;
			    u_xlat4.xy = u_xlat4.xz + u_xlat5.xy;
			    u_xlat16_4.xyz = textureLod(_InternalLut, u_xlat4.xy, 0.0).xyz;
			    u_xlat0.x = u_xlat16_1.z * _Lut_Params.z + (-u_xlat0.x);
			    u_xlat4.xyz = (-u_xlat16_8.xyz) + u_xlat16_4.xyz;
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz + u_xlat16_8.xyz;
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    {
			        vec3 hlslcc_movcTemp = u_xlat0;
			        hlslcc_movcTemp.x = (u_xlatb4.x) ? u_xlat16_1.x : u_xlat0.x;
			        hlslcc_movcTemp.y = (u_xlatb4.y) ? u_xlat16_1.y : u_xlat0.y;
			        hlslcc_movcTemp.z = (u_xlatb4.z) ? u_xlat16_1.z : u_xlat0.z;
			        u_xlat0 = hlslcc_movcTemp;
			    }
			    SV_Target0.xyz = u_xlat0.xyz;
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
			#ifdef GL_EXT_shader_texture_lod
			#extension GL_EXT_shader_texture_lod : enable
			#endif

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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _UserLut_Params;
			uniform 	mediump vec4 _Vignette_Params1;
			uniform 	vec4 _Vignette_Params2;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _InternalLut;
			UNITY_LOCATION(2) uniform mediump sampler2D _UserLut;
			UNITY_LOCATION(3) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			bvec3 u_xlatb0;
			vec3 u_xlat1;
			mediump vec3 u_xlat16_1;
			mediump vec3 u_xlat16_2;
			mediump vec3 u_xlat16_3;
			vec4 u_xlat4;
			mediump vec3 u_xlat16_4;
			bvec3 u_xlatb4;
			vec2 u_xlat5;
			mediump vec3 u_xlat16_5;
			vec2 u_xlat6;
			mediump vec3 u_xlat16_7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_8;
			float u_xlat24;
			mediump float u_xlat16_24;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_0.xyz, vec3(100.0, 100.0, 100.0));
			    u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			    u_xlat16_3.xyz = u_xlat16_1.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			    u_xlat0.xyz = log2(abs(u_xlat16_3.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_1.xyzx).xyz;
			    u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_2.x : u_xlat0.x;
			    u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_2.y : u_xlat0.y;
			    u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_2.z : u_xlat0.z;
			    u_xlatb0.x = 0.0<_Vignette_Params2.z;
			    if(u_xlatb0.x){
			        u_xlat0.xy = vs_TEXCOORD0.xy + (-_Vignette_Params2.xy);
			        u_xlat0.yz = abs(u_xlat0.xy) * _Vignette_Params2.zz;
			        u_xlat0.x = u_xlat0.y * _Vignette_Params1.w;
			        u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			        u_xlat0.x = (-u_xlat0.x) + 1.0;
			        u_xlat0.x = max(u_xlat0.x, 0.0);
			        u_xlat0.x = log2(u_xlat0.x);
			        u_xlat0.x = u_xlat0.x * _Vignette_Params2.w;
			        u_xlat0.x = exp2(u_xlat0.x);
			        u_xlat8.xyz = (-_Vignette_Params1.xyz) + vec3(1.0, 1.0, 1.0);
			        u_xlat0.xyz = u_xlat0.xxx * u_xlat8.xyz + _Vignette_Params1.xyz;
			        u_xlat1.xyz = u_xlat0.xyz * u_xlat16_1.xyz;
			        u_xlat16_1.xyz = u_xlat1.xyz;
			    }
			    u_xlat16_1.xyz = u_xlat16_1.xyz * _Lut_Params.www;
			    u_xlat16_1.xyz = clamp(u_xlat16_1.xyz, 0.0, 1.0);
			    u_xlatb0.x = 0.0<_UserLut_Params.w;
			    if(u_xlatb0.x){
			        u_xlatb0.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_1.xyzx).xyz;
			        u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			        u_xlat4.xyz = log2(u_xlat16_1.xyz);
			        u_xlat4.xyz = u_xlat4.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			        u_xlat4.xyz = exp2(u_xlat4.xyz);
			        u_xlat4.xyz = u_xlat4.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			        u_xlat0.x = (u_xlatb0.x) ? u_xlat16_2.x : u_xlat4.x;
			        u_xlat0.y = (u_xlatb0.y) ? u_xlat16_2.y : u_xlat4.y;
			        u_xlat0.z = (u_xlatb0.z) ? u_xlat16_2.z : u_xlat4.z;
			        u_xlat4.xyz = u_xlat0.zxy * _UserLut_Params.zzz;
			        u_xlat24 = floor(u_xlat4.x);
			        u_xlat4.xw = _UserLut_Params.xy * vec2(0.5, 0.5);
			        u_xlat4.yz = u_xlat4.yz * _UserLut_Params.xy + u_xlat4.xw;
			        u_xlat4.x = u_xlat24 * _UserLut_Params.y + u_xlat4.y;
			        u_xlat16_5.xyz = textureLod(_UserLut, u_xlat4.xz, 0.0).xyz;
			        u_xlat6.x = _UserLut_Params.y;
			        u_xlat6.y = 0.0;
			        u_xlat4.xy = u_xlat4.xz + u_xlat6.xy;
			        u_xlat16_4.xyz = textureLod(_UserLut, u_xlat4.xy, 0.0).xyz;
			        u_xlat24 = u_xlat0.z * _UserLut_Params.z + (-u_xlat24);
			        u_xlat4.xyz = (-u_xlat16_5.xyz) + u_xlat16_4.xyz;
			        u_xlat4.xyz = vec3(u_xlat24) * u_xlat4.xyz + u_xlat16_5.xyz;
			        u_xlat4.xyz = (-u_xlat0.xyz) + u_xlat4.xyz;
			        u_xlat0.xyz = _UserLut_Params.www * u_xlat4.xyz + u_xlat0.xyz;
			        u_xlat16_2.xyz = min(u_xlat0.xyz, vec3(100.0, 100.0, 100.0));
			        u_xlat16_3.xyz = u_xlat16_2.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			        u_xlat16_7.xyz = u_xlat16_2.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			        u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			        u_xlat0.xyz = log2(abs(u_xlat16_7.xyz));
			        u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			        u_xlat0.xyz = exp2(u_xlat0.xyz);
			        u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_2.xyzx).xyz;
			        u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_3.x : u_xlat0.x;
			        u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_3.y : u_xlat0.y;
			        u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_3.z : u_xlat0.z;
			    }
			    u_xlat0.xyz = u_xlat16_1.zxy * _Lut_Params.zzz;
			    u_xlat0.x = floor(u_xlat0.x);
			    u_xlat4.xy = _Lut_Params.xy * vec2(0.5, 0.5);
			    u_xlat4.yz = u_xlat0.yz * _Lut_Params.xy + u_xlat4.xy;
			    u_xlat4.x = u_xlat0.x * _Lut_Params.y + u_xlat4.y;
			    u_xlat16_8.xyz = textureLod(_InternalLut, u_xlat4.xz, 0.0).xyz;
			    u_xlat5.x = _Lut_Params.y;
			    u_xlat5.y = 0.0;
			    u_xlat4.xy = u_xlat4.xz + u_xlat5.xy;
			    u_xlat16_4.xyz = textureLod(_InternalLut, u_xlat4.xy, 0.0).xyz;
			    u_xlat0.x = u_xlat16_1.z * _Lut_Params.z + (-u_xlat0.x);
			    u_xlat4.xyz = (-u_xlat16_8.xyz) + u_xlat16_4.xyz;
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz + u_xlat16_8.xyz;
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    {
			        vec3 hlslcc_movcTemp = u_xlat0;
			        hlslcc_movcTemp.x = (u_xlatb4.x) ? u_xlat16_1.x : u_xlat0.x;
			        hlslcc_movcTemp.y = (u_xlatb4.y) ? u_xlat16_1.y : u_xlat0.y;
			        hlslcc_movcTemp.z = (u_xlatb4.z) ? u_xlat16_1.z : u_xlat0.z;
			        u_xlat0 = hlslcc_movcTemp;
			    }
			    u_xlat4.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_24 = texture(_BlueNoise_Texture, u_xlat4.xy, _GlobalMipBias.x).w;
			    u_xlat24 = u_xlat16_24 * 2.0 + -1.0;
			    u_xlatb4.x = u_xlat24>=0.0;
			    u_xlat4.x = (u_xlatb4.x) ? 1.0 : -1.0;
			    u_xlat24 = -abs(u_xlat24) + 1.0;
			    u_xlat24 = sqrt(u_xlat24);
			    u_xlat24 = (-u_xlat24) + 1.0;
			    u_xlat24 = u_xlat24 * u_xlat4.x;
			    u_xlat0.xyz = vec3(u_xlat24) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat0.xyz;
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
			#ifdef GL_EXT_shader_texture_lod
			#extension GL_EXT_shader_texture_lod : enable
			#endif

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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _UserLut_Params;
			uniform 	vec4 _Bloom_Params;
			uniform 	float _Bloom_RGBM;
			uniform 	vec4 _LensDirt_Params;
			uniform 	float _LensDirt_Intensity;
			uniform 	mediump vec4 _Vignette_Params1;
			uniform 	vec4 _Vignette_Params2;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Bloom_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _LensDirt_Texture;
			UNITY_LOCATION(3) uniform mediump sampler2D _InternalLut;
			UNITY_LOCATION(4) uniform mediump sampler2D _UserLut;
			UNITY_LOCATION(5) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec4 u_xlat16_0;
			bvec3 u_xlatb0;
			vec3 u_xlat1;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			mediump vec3 u_xlat16_2;
			mediump vec3 u_xlat16_3;
			vec4 u_xlat4;
			mediump vec3 u_xlat16_4;
			bvec3 u_xlatb4;
			vec3 u_xlat5;
			mediump vec3 u_xlat16_5;
			vec2 u_xlat6;
			mediump vec3 u_xlat16_7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_8;
			float u_xlat24;
			mediump float u_xlat16_24;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_0.xyz, vec3(100.0, 100.0, 100.0));
			    u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			    u_xlat16_3.xyz = u_xlat16_1.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			    u_xlat0.xyz = log2(abs(u_xlat16_3.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_1.xyzx).xyz;
			    u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_2.x : u_xlat0.x;
			    u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_2.y : u_xlat0.y;
			    u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_2.z : u_xlat0.z;
			    u_xlat16_0 = texture(_Bloom_Texture, vs_TEXCOORD0.xy, _GlobalMipBias.x);
			    u_xlat16_2.xyz = u_xlat16_0.xyz * u_xlat16_0.xyz;
			    u_xlatb0.x = 0.0<_Bloom_RGBM;
			    if(u_xlatb0.x){
			        u_xlat16_3.xyz = u_xlat16_0.www * u_xlat16_2.xyz;
			        u_xlat2.xyz = u_xlat16_3.xyz * vec3(8.0, 8.0, 8.0);
			        u_xlat16_2.xyz = u_xlat2.xyz;
			    }
			    u_xlat0.xyz = u_xlat16_2.xyz * _Bloom_Params.xxx;
			    u_xlat4.xyz = u_xlat0.xyz * _Bloom_Params.yzw + u_xlat16_1.xyz;
			    u_xlat5.xy = vs_TEXCOORD0.xy * _LensDirt_Params.xy + _LensDirt_Params.zw;
			    u_xlat16_5.xyz = texture(_LensDirt_Texture, u_xlat5.xy, _GlobalMipBias.x).xyz;
			    u_xlat5.xyz = u_xlat16_5.xyz * vec3(_LensDirt_Intensity);
			    u_xlat16_1.xyz = u_xlat5.xyz * u_xlat0.xyz + u_xlat4.xyz;
			    u_xlatb0.x = 0.0<_Vignette_Params2.z;
			    if(u_xlatb0.x){
			        u_xlat0.xy = vs_TEXCOORD0.xy + (-_Vignette_Params2.xy);
			        u_xlat0.yz = abs(u_xlat0.xy) * _Vignette_Params2.zz;
			        u_xlat0.x = u_xlat0.y * _Vignette_Params1.w;
			        u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			        u_xlat0.x = (-u_xlat0.x) + 1.0;
			        u_xlat0.x = max(u_xlat0.x, 0.0);
			        u_xlat0.x = log2(u_xlat0.x);
			        u_xlat0.x = u_xlat0.x * _Vignette_Params2.w;
			        u_xlat0.x = exp2(u_xlat0.x);
			        u_xlat8.xyz = (-_Vignette_Params1.xyz) + vec3(1.0, 1.0, 1.0);
			        u_xlat0.xyz = u_xlat0.xxx * u_xlat8.xyz + _Vignette_Params1.xyz;
			        u_xlat1.xyz = u_xlat0.xyz * u_xlat16_1.xyz;
			        u_xlat16_1.xyz = u_xlat1.xyz;
			    }
			    u_xlat16_1.xyz = u_xlat16_1.xyz * _Lut_Params.www;
			    u_xlat16_1.xyz = clamp(u_xlat16_1.xyz, 0.0, 1.0);
			    u_xlatb0.x = 0.0<_UserLut_Params.w;
			    if(u_xlatb0.x){
			        u_xlatb0.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_1.xyzx).xyz;
			        u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			        u_xlat4.xyz = log2(u_xlat16_1.xyz);
			        u_xlat4.xyz = u_xlat4.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			        u_xlat4.xyz = exp2(u_xlat4.xyz);
			        u_xlat4.xyz = u_xlat4.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			        u_xlat0.x = (u_xlatb0.x) ? u_xlat16_2.x : u_xlat4.x;
			        u_xlat0.y = (u_xlatb0.y) ? u_xlat16_2.y : u_xlat4.y;
			        u_xlat0.z = (u_xlatb0.z) ? u_xlat16_2.z : u_xlat4.z;
			        u_xlat4.xyz = u_xlat0.zxy * _UserLut_Params.zzz;
			        u_xlat24 = floor(u_xlat4.x);
			        u_xlat4.xw = _UserLut_Params.xy * vec2(0.5, 0.5);
			        u_xlat4.yz = u_xlat4.yz * _UserLut_Params.xy + u_xlat4.xw;
			        u_xlat4.x = u_xlat24 * _UserLut_Params.y + u_xlat4.y;
			        u_xlat16_5.xyz = textureLod(_UserLut, u_xlat4.xz, 0.0).xyz;
			        u_xlat6.x = _UserLut_Params.y;
			        u_xlat6.y = 0.0;
			        u_xlat4.xy = u_xlat4.xz + u_xlat6.xy;
			        u_xlat16_4.xyz = textureLod(_UserLut, u_xlat4.xy, 0.0).xyz;
			        u_xlat24 = u_xlat0.z * _UserLut_Params.z + (-u_xlat24);
			        u_xlat4.xyz = (-u_xlat16_5.xyz) + u_xlat16_4.xyz;
			        u_xlat4.xyz = vec3(u_xlat24) * u_xlat4.xyz + u_xlat16_5.xyz;
			        u_xlat4.xyz = (-u_xlat0.xyz) + u_xlat4.xyz;
			        u_xlat0.xyz = _UserLut_Params.www * u_xlat4.xyz + u_xlat0.xyz;
			        u_xlat16_2.xyz = min(u_xlat0.xyz, vec3(100.0, 100.0, 100.0));
			        u_xlat16_3.xyz = u_xlat16_2.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			        u_xlat16_7.xyz = u_xlat16_2.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			        u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			        u_xlat0.xyz = log2(abs(u_xlat16_7.xyz));
			        u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			        u_xlat0.xyz = exp2(u_xlat0.xyz);
			        u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_2.xyzx).xyz;
			        u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_3.x : u_xlat0.x;
			        u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_3.y : u_xlat0.y;
			        u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_3.z : u_xlat0.z;
			    }
			    u_xlat0.xyz = u_xlat16_1.zxy * _Lut_Params.zzz;
			    u_xlat0.x = floor(u_xlat0.x);
			    u_xlat4.xy = _Lut_Params.xy * vec2(0.5, 0.5);
			    u_xlat4.yz = u_xlat0.yz * _Lut_Params.xy + u_xlat4.xy;
			    u_xlat4.x = u_xlat0.x * _Lut_Params.y + u_xlat4.y;
			    u_xlat16_8.xyz = textureLod(_InternalLut, u_xlat4.xz, 0.0).xyz;
			    u_xlat5.x = _Lut_Params.y;
			    u_xlat5.y = 0.0;
			    u_xlat4.xy = u_xlat4.xz + u_xlat5.xy;
			    u_xlat16_4.xyz = textureLod(_InternalLut, u_xlat4.xy, 0.0).xyz;
			    u_xlat0.x = u_xlat16_1.z * _Lut_Params.z + (-u_xlat0.x);
			    u_xlat4.xyz = (-u_xlat16_8.xyz) + u_xlat16_4.xyz;
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz + u_xlat16_8.xyz;
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    {
			        vec3 hlslcc_movcTemp = u_xlat0;
			        hlslcc_movcTemp.x = (u_xlatb4.x) ? u_xlat16_1.x : u_xlat0.x;
			        hlslcc_movcTemp.y = (u_xlatb4.y) ? u_xlat16_1.y : u_xlat0.y;
			        hlslcc_movcTemp.z = (u_xlatb4.z) ? u_xlat16_1.z : u_xlat0.z;
			        u_xlat0 = hlslcc_movcTemp;
			    }
			    u_xlat4.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_24 = texture(_BlueNoise_Texture, u_xlat4.xy, _GlobalMipBias.x).w;
			    u_xlat24 = u_xlat16_24 * 2.0 + -1.0;
			    u_xlatb4.x = u_xlat24>=0.0;
			    u_xlat4.x = (u_xlatb4.x) ? 1.0 : -1.0;
			    u_xlat24 = -abs(u_xlat24) + 1.0;
			    u_xlat24 = sqrt(u_xlat24);
			    u_xlat24 = (-u_xlat24) + 1.0;
			    u_xlat24 = u_xlat24 * u_xlat4.x;
			    u_xlat0.xyz = vec3(u_xlat24) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat0.xyz;
			    SV_Target0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
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
			#ifdef GL_EXT_shader_texture_lod
			#extension GL_EXT_shader_texture_lod : enable
			#endif

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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _UserLut_Params;
			uniform 	vec4 _Bloom_Params;
			uniform 	float _Bloom_RGBM;
			uniform 	vec4 _LensDirt_Params;
			uniform 	float _LensDirt_Intensity;
			uniform 	mediump vec4 _Vignette_Params1;
			uniform 	vec4 _Vignette_Params2;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Bloom_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _LensDirt_Texture;
			UNITY_LOCATION(3) uniform mediump sampler2D _InternalLut;
			UNITY_LOCATION(4) uniform mediump sampler2D _UserLut;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec4 u_xlat16_0;
			bvec3 u_xlatb0;
			vec3 u_xlat1;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			mediump vec3 u_xlat16_2;
			mediump vec3 u_xlat16_3;
			vec4 u_xlat4;
			mediump vec3 u_xlat16_4;
			bvec3 u_xlatb4;
			vec3 u_xlat5;
			mediump vec3 u_xlat16_5;
			vec2 u_xlat6;
			mediump vec3 u_xlat16_7;
			vec3 u_xlat8;
			mediump vec3 u_xlat16_8;
			float u_xlat24;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_0.xyz, vec3(100.0, 100.0, 100.0));
			    u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			    u_xlat16_3.xyz = u_xlat16_1.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			    u_xlat0.xyz = log2(abs(u_xlat16_3.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_1.xyzx).xyz;
			    u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_2.x : u_xlat0.x;
			    u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_2.y : u_xlat0.y;
			    u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_2.z : u_xlat0.z;
			    u_xlat16_0 = texture(_Bloom_Texture, vs_TEXCOORD0.xy, _GlobalMipBias.x);
			    u_xlat16_2.xyz = u_xlat16_0.xyz * u_xlat16_0.xyz;
			    u_xlatb0.x = 0.0<_Bloom_RGBM;
			    if(u_xlatb0.x){
			        u_xlat16_3.xyz = u_xlat16_0.www * u_xlat16_2.xyz;
			        u_xlat2.xyz = u_xlat16_3.xyz * vec3(8.0, 8.0, 8.0);
			        u_xlat16_2.xyz = u_xlat2.xyz;
			    }
			    u_xlat0.xyz = u_xlat16_2.xyz * _Bloom_Params.xxx;
			    u_xlat4.xyz = u_xlat0.xyz * _Bloom_Params.yzw + u_xlat16_1.xyz;
			    u_xlat5.xy = vs_TEXCOORD0.xy * _LensDirt_Params.xy + _LensDirt_Params.zw;
			    u_xlat16_5.xyz = texture(_LensDirt_Texture, u_xlat5.xy, _GlobalMipBias.x).xyz;
			    u_xlat5.xyz = u_xlat16_5.xyz * vec3(_LensDirt_Intensity);
			    u_xlat16_1.xyz = u_xlat5.xyz * u_xlat0.xyz + u_xlat4.xyz;
			    u_xlatb0.x = 0.0<_Vignette_Params2.z;
			    if(u_xlatb0.x){
			        u_xlat0.xy = vs_TEXCOORD0.xy + (-_Vignette_Params2.xy);
			        u_xlat0.yz = abs(u_xlat0.xy) * _Vignette_Params2.zz;
			        u_xlat0.x = u_xlat0.y * _Vignette_Params1.w;
			        u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			        u_xlat0.x = (-u_xlat0.x) + 1.0;
			        u_xlat0.x = max(u_xlat0.x, 0.0);
			        u_xlat0.x = log2(u_xlat0.x);
			        u_xlat0.x = u_xlat0.x * _Vignette_Params2.w;
			        u_xlat0.x = exp2(u_xlat0.x);
			        u_xlat8.xyz = (-_Vignette_Params1.xyz) + vec3(1.0, 1.0, 1.0);
			        u_xlat0.xyz = u_xlat0.xxx * u_xlat8.xyz + _Vignette_Params1.xyz;
			        u_xlat1.xyz = u_xlat0.xyz * u_xlat16_1.xyz;
			        u_xlat16_1.xyz = u_xlat1.xyz;
			    }
			    u_xlat16_1.xyz = u_xlat16_1.xyz * _Lut_Params.www;
			    u_xlat16_1.xyz = clamp(u_xlat16_1.xyz, 0.0, 1.0);
			    u_xlatb0.x = 0.0<_UserLut_Params.w;
			    if(u_xlatb0.x){
			        u_xlatb0.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_1.xyzx).xyz;
			        u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			        u_xlat4.xyz = log2(u_xlat16_1.xyz);
			        u_xlat4.xyz = u_xlat4.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			        u_xlat4.xyz = exp2(u_xlat4.xyz);
			        u_xlat4.xyz = u_xlat4.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			        u_xlat0.x = (u_xlatb0.x) ? u_xlat16_2.x : u_xlat4.x;
			        u_xlat0.y = (u_xlatb0.y) ? u_xlat16_2.y : u_xlat4.y;
			        u_xlat0.z = (u_xlatb0.z) ? u_xlat16_2.z : u_xlat4.z;
			        u_xlat4.xyz = u_xlat0.zxy * _UserLut_Params.zzz;
			        u_xlat24 = floor(u_xlat4.x);
			        u_xlat4.xw = _UserLut_Params.xy * vec2(0.5, 0.5);
			        u_xlat4.yz = u_xlat4.yz * _UserLut_Params.xy + u_xlat4.xw;
			        u_xlat4.x = u_xlat24 * _UserLut_Params.y + u_xlat4.y;
			        u_xlat16_5.xyz = textureLod(_UserLut, u_xlat4.xz, 0.0).xyz;
			        u_xlat6.x = _UserLut_Params.y;
			        u_xlat6.y = 0.0;
			        u_xlat4.xy = u_xlat4.xz + u_xlat6.xy;
			        u_xlat16_4.xyz = textureLod(_UserLut, u_xlat4.xy, 0.0).xyz;
			        u_xlat24 = u_xlat0.z * _UserLut_Params.z + (-u_xlat24);
			        u_xlat4.xyz = (-u_xlat16_5.xyz) + u_xlat16_4.xyz;
			        u_xlat4.xyz = vec3(u_xlat24) * u_xlat4.xyz + u_xlat16_5.xyz;
			        u_xlat4.xyz = (-u_xlat0.xyz) + u_xlat4.xyz;
			        u_xlat0.xyz = _UserLut_Params.www * u_xlat4.xyz + u_xlat0.xyz;
			        u_xlat16_2.xyz = min(u_xlat0.xyz, vec3(100.0, 100.0, 100.0));
			        u_xlat16_3.xyz = u_xlat16_2.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			        u_xlat16_7.xyz = u_xlat16_2.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			        u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			        u_xlat0.xyz = log2(abs(u_xlat16_7.xyz));
			        u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			        u_xlat0.xyz = exp2(u_xlat0.xyz);
			        u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_2.xyzx).xyz;
			        u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_3.x : u_xlat0.x;
			        u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_3.y : u_xlat0.y;
			        u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_3.z : u_xlat0.z;
			    }
			    u_xlat0.xyz = u_xlat16_1.zxy * _Lut_Params.zzz;
			    u_xlat0.x = floor(u_xlat0.x);
			    u_xlat4.xy = _Lut_Params.xy * vec2(0.5, 0.5);
			    u_xlat4.yz = u_xlat0.yz * _Lut_Params.xy + u_xlat4.xy;
			    u_xlat4.x = u_xlat0.x * _Lut_Params.y + u_xlat4.y;
			    u_xlat16_8.xyz = textureLod(_InternalLut, u_xlat4.xz, 0.0).xyz;
			    u_xlat5.x = _Lut_Params.y;
			    u_xlat5.y = 0.0;
			    u_xlat4.xy = u_xlat4.xz + u_xlat5.xy;
			    u_xlat16_4.xyz = textureLod(_InternalLut, u_xlat4.xy, 0.0).xyz;
			    u_xlat0.x = u_xlat16_1.z * _Lut_Params.z + (-u_xlat0.x);
			    u_xlat4.xyz = (-u_xlat16_8.xyz) + u_xlat16_4.xyz;
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz + u_xlat16_8.xyz;
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    {
			        vec3 hlslcc_movcTemp = u_xlat0;
			        hlslcc_movcTemp.x = (u_xlatb4.x) ? u_xlat16_1.x : u_xlat0.x;
			        hlslcc_movcTemp.y = (u_xlatb4.y) ? u_xlat16_1.y : u_xlat0.y;
			        hlslcc_movcTemp.z = (u_xlatb4.z) ? u_xlat16_1.z : u_xlat0.z;
			        u_xlat0 = hlslcc_movcTemp;
			    }
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
			#ifdef GL_EXT_shader_texture_lod
			#extension GL_EXT_shader_texture_lod : enable
			#endif

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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _UserLut_Params;
			uniform 	vec4 _Bloom_Params;
			uniform 	float _Bloom_RGBM;
			uniform 	mediump vec4 _Vignette_Params1;
			uniform 	vec4 _Vignette_Params2;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Bloom_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _InternalLut;
			UNITY_LOCATION(3) uniform mediump sampler2D _UserLut;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec4 u_xlat16_0;
			bool u_xlatb0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			mediump vec3 u_xlat16_2;
			mediump vec3 u_xlat16_3;
			vec3 u_xlat4;
			mediump vec3 u_xlat16_4;
			bvec3 u_xlatb4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			vec4 u_xlat6;
			mediump vec3 u_xlat16_6;
			bvec3 u_xlatb6;
			vec2 u_xlat7;
			mediump vec3 u_xlat16_7;
			vec2 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_15;
			float u_xlat30;
			bool u_xlatb30;
			float u_xlat35;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_0.xyz, vec3(100.0, 100.0, 100.0));
			    u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			    u_xlat16_3.xyz = u_xlat16_1.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			    u_xlat0.xyz = log2(abs(u_xlat16_3.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_1.xyzx).xyz;
			    u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_2.x : u_xlat0.x;
			    u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_2.y : u_xlat0.y;
			    u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_2.z : u_xlat0.z;
			    u_xlat16_0 = texture(_Bloom_Texture, vs_TEXCOORD0.xy, _GlobalMipBias.x);
			    u_xlat16_2.xyz = u_xlat16_0.xyz * u_xlat16_0.xyz;
			    u_xlatb0 = 0.0<_Bloom_RGBM;
			    if(u_xlatb0){
			        u_xlat16_3.xyz = u_xlat16_0.www * u_xlat16_2.xyz;
			        u_xlat2.xyz = u_xlat16_3.xyz * vec3(8.0, 8.0, 8.0);
			        u_xlat16_2.xyz = u_xlat2.xyz;
			    }
			    u_xlat0.xyz = u_xlat16_2.xyz * _Bloom_Params.xxx;
			    u_xlat0.xyz = u_xlat0.xyz * _Bloom_Params.yzw + u_xlat16_1.xyz;
			    u_xlatb30 = 0.0<_Vignette_Params2.z;
			    if(u_xlatb30){
			        u_xlat4.xy = vs_TEXCOORD0.xy + (-_Vignette_Params2.xy);
			        u_xlat4.yz = abs(u_xlat4.xy) * _Vignette_Params2.zz;
			        u_xlat4.x = u_xlat4.y * _Vignette_Params1.w;
			        u_xlat30 = dot(u_xlat4.xz, u_xlat4.xz);
			        u_xlat30 = (-u_xlat30) + 1.0;
			        u_xlat30 = max(u_xlat30, 0.0);
			        u_xlat30 = log2(u_xlat30);
			        u_xlat30 = u_xlat30 * _Vignette_Params2.w;
			        u_xlat30 = exp2(u_xlat30);
			        u_xlat4.xyz = (-_Vignette_Params1.xyz) + vec3(1.0, 1.0, 1.0);
			        u_xlat4.xyz = vec3(u_xlat30) * u_xlat4.xyz + _Vignette_Params1.xyz;
			        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
			        u_xlat16_4.xyz = u_xlat4.xyz;
			    } else {
			        u_xlat16_4.xyz = u_xlat0.xyz;
			    }
			    u_xlat16_1.xyz = u_xlat16_4.xyz * _Lut_Params.www;
			    u_xlat16_1.xyz = clamp(u_xlat16_1.xyz, 0.0, 1.0);
			    u_xlatb5.x = 0.0<_UserLut_Params.w;
			    if(u_xlatb5.x){
			        u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_1.xyzx).xyz;
			        u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			        u_xlat6.xyz = log2(u_xlat16_1.xyz);
			        u_xlat6.xyz = u_xlat6.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			        u_xlat6.xyz = exp2(u_xlat6.xyz);
			        u_xlat6.xyz = u_xlat6.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			        u_xlat5.x = (u_xlatb5.x) ? u_xlat16_2.x : u_xlat6.x;
			        u_xlat5.y = (u_xlatb5.y) ? u_xlat16_2.y : u_xlat6.y;
			        u_xlat5.z = (u_xlatb5.z) ? u_xlat16_2.z : u_xlat6.z;
			        u_xlat6.xyz = u_xlat5.zxy * _UserLut_Params.zzz;
			        u_xlat35 = floor(u_xlat6.x);
			        u_xlat6.xw = _UserLut_Params.xy * vec2(0.5, 0.5);
			        u_xlat6.yz = u_xlat6.yz * _UserLut_Params.xy + u_xlat6.xw;
			        u_xlat6.x = u_xlat35 * _UserLut_Params.y + u_xlat6.y;
			        u_xlat16_7.xyz = textureLod(_UserLut, u_xlat6.xz, 0.0).xyz;
			        u_xlat8.x = _UserLut_Params.y;
			        u_xlat8.y = 0.0;
			        u_xlat6.xy = u_xlat6.xz + u_xlat8.xy;
			        u_xlat16_6.xyz = textureLod(_UserLut, u_xlat6.xy, 0.0).xyz;
			        u_xlat35 = u_xlat5.z * _UserLut_Params.z + (-u_xlat35);
			        u_xlat6.xyz = (-u_xlat16_7.xyz) + u_xlat16_6.xyz;
			        u_xlat6.xyz = vec3(u_xlat35) * u_xlat6.xyz + u_xlat16_7.xyz;
			        u_xlat6.xyz = (-u_xlat5.xyz) + u_xlat6.xyz;
			        u_xlat5.xyz = _UserLut_Params.www * u_xlat6.xyz + u_xlat5.xyz;
			        u_xlat16_2.xyz = min(u_xlat5.xyz, vec3(100.0, 100.0, 100.0));
			        u_xlat16_3.xyz = u_xlat16_2.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			        u_xlat16_9.xyz = u_xlat16_2.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			        u_xlat16_9.xyz = u_xlat16_9.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			        u_xlat5.xyz = log2(abs(u_xlat16_9.xyz));
			        u_xlat5.xyz = u_xlat5.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			        u_xlat5.xyz = exp2(u_xlat5.xyz);
			        u_xlatb6.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_2.xyzx).xyz;
			        u_xlat16_1.x = (u_xlatb6.x) ? u_xlat16_3.x : u_xlat5.x;
			        u_xlat16_1.y = (u_xlatb6.y) ? u_xlat16_3.y : u_xlat5.y;
			        u_xlat16_1.z = (u_xlatb6.z) ? u_xlat16_3.z : u_xlat5.z;
			    }
			    u_xlat5.xyz = u_xlat16_1.zxy * _Lut_Params.zzz;
			    u_xlat5.x = floor(u_xlat5.x);
			    u_xlat6.xy = _Lut_Params.xy * vec2(0.5, 0.5);
			    u_xlat6.yz = u_xlat5.yz * _Lut_Params.xy + u_xlat6.xy;
			    u_xlat6.x = u_xlat5.x * _Lut_Params.y + u_xlat6.y;
			    u_xlat16_15.xyz = textureLod(_InternalLut, u_xlat6.xz, 0.0).xyz;
			    u_xlat7.x = _Lut_Params.y;
			    u_xlat7.y = 0.0;
			    u_xlat6.xy = u_xlat6.xz + u_xlat7.xy;
			    u_xlat16_6.xyz = textureLod(_InternalLut, u_xlat6.xy, 0.0).xyz;
			    u_xlat5.x = u_xlat16_1.z * _Lut_Params.z + (-u_xlat5.x);
			    u_xlat6.xyz = (-u_xlat16_15.xyz) + u_xlat16_6.xyz;
			    u_xlat5.xyz = u_xlat5.xxx * u_xlat6.xyz + u_xlat16_15.xyz;
			    u_xlatb6.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat5.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat5.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat5.xyz = log2(abs(u_xlat5.xyz));
			    u_xlat5.xyz = u_xlat5.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat5.xyz = exp2(u_xlat5.xyz);
			    u_xlat5.xyz = u_xlat5.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    {
			        vec3 hlslcc_movcTemp = u_xlat5;
			        hlslcc_movcTemp.x = (u_xlatb6.x) ? u_xlat16_1.x : u_xlat5.x;
			        hlslcc_movcTemp.y = (u_xlatb6.y) ? u_xlat16_1.y : u_xlat5.y;
			        hlslcc_movcTemp.z = (u_xlatb6.z) ? u_xlat16_1.z : u_xlat5.z;
			        u_xlat5 = hlslcc_movcTemp;
			    }
			    SV_Target0.xyz = u_xlat5.xyz;
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
			#ifdef GL_EXT_shader_texture_lod
			#extension GL_EXT_shader_texture_lod : enable
			#endif

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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _UserLut_Params;
			uniform 	vec4 _Bloom_Params;
			uniform 	float _Bloom_RGBM;
			uniform 	mediump vec4 _Vignette_Params1;
			uniform 	vec4 _Vignette_Params2;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Bloom_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _InternalLut;
			UNITY_LOCATION(3) uniform mediump sampler2D _UserLut;
			UNITY_LOCATION(4) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec4 u_xlat16_0;
			bool u_xlatb0;
			mediump vec3 u_xlat16_1;
			vec3 u_xlat2;
			mediump vec3 u_xlat16_2;
			mediump vec3 u_xlat16_3;
			vec3 u_xlat4;
			mediump vec3 u_xlat16_4;
			bvec3 u_xlatb4;
			vec3 u_xlat5;
			bvec3 u_xlatb5;
			vec4 u_xlat6;
			mediump vec3 u_xlat16_6;
			bvec3 u_xlatb6;
			vec2 u_xlat7;
			mediump vec3 u_xlat16_7;
			vec2 u_xlat8;
			mediump vec3 u_xlat16_9;
			mediump vec3 u_xlat16_15;
			float u_xlat30;
			bool u_xlatb30;
			float u_xlat35;
			mediump float u_xlat16_35;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_0.xyz, vec3(100.0, 100.0, 100.0));
			    u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			    u_xlat16_3.xyz = u_xlat16_1.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			    u_xlat0.xyz = log2(abs(u_xlat16_3.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_1.xyzx).xyz;
			    u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_2.x : u_xlat0.x;
			    u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_2.y : u_xlat0.y;
			    u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_2.z : u_xlat0.z;
			    u_xlat16_0 = texture(_Bloom_Texture, vs_TEXCOORD0.xy, _GlobalMipBias.x);
			    u_xlat16_2.xyz = u_xlat16_0.xyz * u_xlat16_0.xyz;
			    u_xlatb0 = 0.0<_Bloom_RGBM;
			    if(u_xlatb0){
			        u_xlat16_3.xyz = u_xlat16_0.www * u_xlat16_2.xyz;
			        u_xlat2.xyz = u_xlat16_3.xyz * vec3(8.0, 8.0, 8.0);
			        u_xlat16_2.xyz = u_xlat2.xyz;
			    }
			    u_xlat0.xyz = u_xlat16_2.xyz * _Bloom_Params.xxx;
			    u_xlat0.xyz = u_xlat0.xyz * _Bloom_Params.yzw + u_xlat16_1.xyz;
			    u_xlatb30 = 0.0<_Vignette_Params2.z;
			    if(u_xlatb30){
			        u_xlat4.xy = vs_TEXCOORD0.xy + (-_Vignette_Params2.xy);
			        u_xlat4.yz = abs(u_xlat4.xy) * _Vignette_Params2.zz;
			        u_xlat4.x = u_xlat4.y * _Vignette_Params1.w;
			        u_xlat30 = dot(u_xlat4.xz, u_xlat4.xz);
			        u_xlat30 = (-u_xlat30) + 1.0;
			        u_xlat30 = max(u_xlat30, 0.0);
			        u_xlat30 = log2(u_xlat30);
			        u_xlat30 = u_xlat30 * _Vignette_Params2.w;
			        u_xlat30 = exp2(u_xlat30);
			        u_xlat4.xyz = (-_Vignette_Params1.xyz) + vec3(1.0, 1.0, 1.0);
			        u_xlat4.xyz = vec3(u_xlat30) * u_xlat4.xyz + _Vignette_Params1.xyz;
			        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
			        u_xlat16_4.xyz = u_xlat4.xyz;
			    } else {
			        u_xlat16_4.xyz = u_xlat0.xyz;
			    }
			    u_xlat16_1.xyz = u_xlat16_4.xyz * _Lut_Params.www;
			    u_xlat16_1.xyz = clamp(u_xlat16_1.xyz, 0.0, 1.0);
			    u_xlatb5.x = 0.0<_UserLut_Params.w;
			    if(u_xlatb5.x){
			        u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_1.xyzx).xyz;
			        u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			        u_xlat6.xyz = log2(u_xlat16_1.xyz);
			        u_xlat6.xyz = u_xlat6.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			        u_xlat6.xyz = exp2(u_xlat6.xyz);
			        u_xlat6.xyz = u_xlat6.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			        u_xlat5.x = (u_xlatb5.x) ? u_xlat16_2.x : u_xlat6.x;
			        u_xlat5.y = (u_xlatb5.y) ? u_xlat16_2.y : u_xlat6.y;
			        u_xlat5.z = (u_xlatb5.z) ? u_xlat16_2.z : u_xlat6.z;
			        u_xlat6.xyz = u_xlat5.zxy * _UserLut_Params.zzz;
			        u_xlat35 = floor(u_xlat6.x);
			        u_xlat6.xw = _UserLut_Params.xy * vec2(0.5, 0.5);
			        u_xlat6.yz = u_xlat6.yz * _UserLut_Params.xy + u_xlat6.xw;
			        u_xlat6.x = u_xlat35 * _UserLut_Params.y + u_xlat6.y;
			        u_xlat16_7.xyz = textureLod(_UserLut, u_xlat6.xz, 0.0).xyz;
			        u_xlat8.x = _UserLut_Params.y;
			        u_xlat8.y = 0.0;
			        u_xlat6.xy = u_xlat6.xz + u_xlat8.xy;
			        u_xlat16_6.xyz = textureLod(_UserLut, u_xlat6.xy, 0.0).xyz;
			        u_xlat35 = u_xlat5.z * _UserLut_Params.z + (-u_xlat35);
			        u_xlat6.xyz = (-u_xlat16_7.xyz) + u_xlat16_6.xyz;
			        u_xlat6.xyz = vec3(u_xlat35) * u_xlat6.xyz + u_xlat16_7.xyz;
			        u_xlat6.xyz = (-u_xlat5.xyz) + u_xlat6.xyz;
			        u_xlat5.xyz = _UserLut_Params.www * u_xlat6.xyz + u_xlat5.xyz;
			        u_xlat16_2.xyz = min(u_xlat5.xyz, vec3(100.0, 100.0, 100.0));
			        u_xlat16_3.xyz = u_xlat16_2.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			        u_xlat16_9.xyz = u_xlat16_2.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			        u_xlat16_9.xyz = u_xlat16_9.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			        u_xlat5.xyz = log2(abs(u_xlat16_9.xyz));
			        u_xlat5.xyz = u_xlat5.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			        u_xlat5.xyz = exp2(u_xlat5.xyz);
			        u_xlatb6.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_2.xyzx).xyz;
			        u_xlat16_1.x = (u_xlatb6.x) ? u_xlat16_3.x : u_xlat5.x;
			        u_xlat16_1.y = (u_xlatb6.y) ? u_xlat16_3.y : u_xlat5.y;
			        u_xlat16_1.z = (u_xlatb6.z) ? u_xlat16_3.z : u_xlat5.z;
			    }
			    u_xlat5.xyz = u_xlat16_1.zxy * _Lut_Params.zzz;
			    u_xlat5.x = floor(u_xlat5.x);
			    u_xlat6.xy = _Lut_Params.xy * vec2(0.5, 0.5);
			    u_xlat6.yz = u_xlat5.yz * _Lut_Params.xy + u_xlat6.xy;
			    u_xlat6.x = u_xlat5.x * _Lut_Params.y + u_xlat6.y;
			    u_xlat16_15.xyz = textureLod(_InternalLut, u_xlat6.xz, 0.0).xyz;
			    u_xlat7.x = _Lut_Params.y;
			    u_xlat7.y = 0.0;
			    u_xlat6.xy = u_xlat6.xz + u_xlat7.xy;
			    u_xlat16_6.xyz = textureLod(_InternalLut, u_xlat6.xy, 0.0).xyz;
			    u_xlat5.x = u_xlat16_1.z * _Lut_Params.z + (-u_xlat5.x);
			    u_xlat6.xyz = (-u_xlat16_15.xyz) + u_xlat16_6.xyz;
			    u_xlat5.xyz = u_xlat5.xxx * u_xlat6.xyz + u_xlat16_15.xyz;
			    u_xlatb6.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat5.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat5.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat5.xyz = log2(abs(u_xlat5.xyz));
			    u_xlat5.xyz = u_xlat5.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat5.xyz = exp2(u_xlat5.xyz);
			    u_xlat5.xyz = u_xlat5.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    {
			        vec3 hlslcc_movcTemp = u_xlat5;
			        hlslcc_movcTemp.x = (u_xlatb6.x) ? u_xlat16_1.x : u_xlat5.x;
			        hlslcc_movcTemp.y = (u_xlatb6.y) ? u_xlat16_1.y : u_xlat5.y;
			        hlslcc_movcTemp.z = (u_xlatb6.z) ? u_xlat16_1.z : u_xlat5.z;
			        u_xlat5 = hlslcc_movcTemp;
			    }
			    u_xlat6.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_35 = texture(_BlueNoise_Texture, u_xlat6.xy, _GlobalMipBias.x).w;
			    u_xlat35 = u_xlat16_35 * 2.0 + -1.0;
			    u_xlatb6.x = u_xlat35>=0.0;
			    u_xlat6.x = (u_xlatb6.x) ? 1.0 : -1.0;
			    u_xlat35 = -abs(u_xlat35) + 1.0;
			    u_xlat35 = sqrt(u_xlat35);
			    u_xlat35 = (-u_xlat35) + 1.0;
			    u_xlat35 = u_xlat35 * u_xlat6.x;
			    u_xlat5.xyz = vec3(u_xlat35) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat5.xyz;
			    SV_Target0.xyz = max(u_xlat5.xyz, vec3(0.0, 0.0, 0.0));
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
			#ifdef GL_EXT_shader_texture_lod
			#extension GL_EXT_shader_texture_lod : enable
			#endif

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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _UserLut_Params;
			uniform 	vec4 _Bloom_Params;
			uniform 	float _Bloom_RGBM;
			uniform 	mediump vec4 _Vignette_Params1;
			uniform 	vec4 _Vignette_Params2;
			uniform 	vec4 _Bloom_Texture_TexelSize;
			uniform 	vec4 _Dithering_Params;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Bloom_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _InternalLut;
			UNITY_LOCATION(3) uniform mediump sampler2D _UserLut;
			UNITY_LOCATION(4) uniform mediump sampler2D _BlueNoise_Texture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			bvec3 u_xlatb0;
			mediump vec3 u_xlat16_1;
			vec4 u_xlat2;
			mediump vec4 u_xlat16_2;
			vec4 u_xlat3;
			mediump vec4 u_xlat16_3;
			vec3 u_xlat4;
			mediump vec3 u_xlat16_4;
			bvec3 u_xlatb4;
			vec4 u_xlat5;
			mediump vec4 u_xlat16_5;
			bvec3 u_xlatb5;
			vec3 u_xlat6;
			mediump vec3 u_xlat16_6;
			mediump vec3 u_xlat16_7;
			vec2 u_xlat8;
			mediump vec3 u_xlat16_8;
			vec2 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			vec2 u_xlat22;
			vec2 u_xlat26;
			float u_xlat33;
			mediump float u_xlat16_33;
			bool u_xlatb33;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_0.xyz, vec3(100.0, 100.0, 100.0));
			    u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			    u_xlat16_3.xyz = u_xlat16_1.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			    u_xlat0.xyz = log2(abs(u_xlat16_3.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_1.xyzx).xyz;
			    u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_2.x : u_xlat0.x;
			    u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_2.y : u_xlat0.y;
			    u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_2.z : u_xlat0.z;
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Bloom_Texture_TexelSize.zw + vec2(0.5, 0.5);
			    u_xlat22.xy = floor(u_xlat0.xy);
			    u_xlat0.xy = fract(u_xlat0.xy);
			    u_xlat16_2 = (-u_xlat0.xyxy) * vec4(0.5, 0.5, 0.166666672, 0.166666672) + vec4(0.5, 0.5, 0.5, 0.5);
			    u_xlat16_2 = u_xlat0.xyxy * u_xlat16_2 + vec4(0.5, 0.5, -0.5, -0.5);
			    u_xlat16_3.xy = u_xlat0.xy * vec2(0.5, 0.5) + vec2(-1.0, -1.0);
			    u_xlat16_3.xy = u_xlat0.xy * u_xlat16_3.xy;
			    u_xlat16_3.xy = u_xlat16_3.xy * u_xlat0.xy + vec2(0.666666687, 0.666666687);
			    u_xlat16_2 = u_xlat0.xyxy * u_xlat16_2 + vec4(0.166666672, 0.166666672, 0.166666672, 0.166666672);
			    u_xlat0.xy = (-u_xlat16_3.xy) + vec2(1.0, 1.0);
			    u_xlat0.xy = (-u_xlat16_2.xy) + u_xlat0.xy;
			    u_xlat0.xy = (-u_xlat16_2.zw) + u_xlat0.xy;
			    u_xlat4.xy = u_xlat16_3.xy + u_xlat16_2.zw;
			    u_xlat26.xy = u_xlat0.xy + u_xlat16_2.xy;
			    u_xlat5.xy = vec2(1.0) / vec2(u_xlat4.xy);
			    u_xlat2.zw = u_xlat16_3.xy * u_xlat5.xy + vec2(-1.0, -1.0);
			    u_xlat5.xy = vec2(1.0) / vec2(u_xlat26.xy);
			    u_xlat2.xy = u_xlat0.xy * u_xlat5.xy + vec2(1.0, 1.0);
			    u_xlat3 = u_xlat22.xyxy + u_xlat2.zwxw;
			    u_xlat3 = u_xlat3 + vec4(-0.5, -0.5, -0.5, -0.5);
			    u_xlat3 = u_xlat3 * _Bloom_Texture_TexelSize.xyxy;
			    u_xlat3 = min(u_xlat3, vec4(1.0, 1.0, 1.0, 1.0));
			    u_xlat16_5 = textureLod(_Bloom_Texture, u_xlat3.xy, 0.0);
			    u_xlat16_3 = textureLod(_Bloom_Texture, u_xlat3.zw, 0.0);
			    u_xlat3 = u_xlat16_3 * u_xlat26.xxxx;
			    u_xlat3 = u_xlat4.xxxx * u_xlat16_5 + u_xlat3;
			    u_xlat0 = u_xlat22.xyxy + u_xlat2.zyxy;
			    u_xlat0 = u_xlat0 + vec4(-0.5, -0.5, -0.5, -0.5);
			    u_xlat0 = u_xlat0 * _Bloom_Texture_TexelSize.xyxy;
			    u_xlat0 = min(u_xlat0, vec4(1.0, 1.0, 1.0, 1.0));
			    u_xlat16_2 = textureLod(_Bloom_Texture, u_xlat0.xy, 0.0);
			    u_xlat16_0 = textureLod(_Bloom_Texture, u_xlat0.zw, 0.0);
			    u_xlat0 = u_xlat16_0 * u_xlat26.xxxx;
			    u_xlat0 = u_xlat4.xxxx * u_xlat16_2 + u_xlat0;
			    u_xlat0 = u_xlat0 * u_xlat26.yyyy;
			    u_xlat0 = u_xlat4.yyyy * u_xlat3 + u_xlat0;
			    u_xlat16_6.xyz = u_xlat0.xyz * u_xlat0.xyz;
			    u_xlatb0.x = 0.0<_Bloom_RGBM;
			    if(u_xlatb0.x){
			        u_xlat16_7.xyz = u_xlat0.www * u_xlat16_6.xyz;
			        u_xlat6.xyz = u_xlat16_7.xyz * vec3(8.0, 8.0, 8.0);
			        u_xlat16_6.xyz = u_xlat6.xyz;
			    }
			    u_xlat0.xyz = u_xlat16_6.xyz * _Bloom_Params.xxx;
			    u_xlat0.xyz = u_xlat0.xyz * _Bloom_Params.yzw + u_xlat16_1.xyz;
			    u_xlatb33 = 0.0<_Vignette_Params2.z;
			    if(u_xlatb33){
			        u_xlat4.xy = vs_TEXCOORD0.xy + (-_Vignette_Params2.xy);
			        u_xlat4.yz = abs(u_xlat4.xy) * _Vignette_Params2.zz;
			        u_xlat4.x = u_xlat4.y * _Vignette_Params1.w;
			        u_xlat33 = dot(u_xlat4.xz, u_xlat4.xz);
			        u_xlat33 = (-u_xlat33) + 1.0;
			        u_xlat33 = max(u_xlat33, 0.0);
			        u_xlat33 = log2(u_xlat33);
			        u_xlat33 = u_xlat33 * _Vignette_Params2.w;
			        u_xlat33 = exp2(u_xlat33);
			        u_xlat4.xyz = (-_Vignette_Params1.xyz) + vec3(1.0, 1.0, 1.0);
			        u_xlat4.xyz = vec3(u_xlat33) * u_xlat4.xyz + _Vignette_Params1.xyz;
			        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
			        u_xlat16_4.xyz = u_xlat4.xyz;
			    } else {
			        u_xlat16_4.xyz = u_xlat0.xyz;
			    }
			    u_xlat16_1.xyz = u_xlat16_4.xyz * _Lut_Params.www;
			    u_xlat16_1.xyz = clamp(u_xlat16_1.xyz, 0.0, 1.0);
			    u_xlatb0.x = 0.0<_UserLut_Params.w;
			    if(u_xlatb0.x){
			        u_xlatb0.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_1.xyzx).xyz;
			        u_xlat16_6.xyz = u_xlat16_1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			        u_xlat5.xyz = log2(u_xlat16_1.xyz);
			        u_xlat5.xyz = u_xlat5.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			        u_xlat5.xyz = exp2(u_xlat5.xyz);
			        u_xlat5.xyz = u_xlat5.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			        u_xlat0.x = (u_xlatb0.x) ? u_xlat16_6.x : u_xlat5.x;
			        u_xlat0.y = (u_xlatb0.y) ? u_xlat16_6.y : u_xlat5.y;
			        u_xlat0.z = (u_xlatb0.z) ? u_xlat16_6.z : u_xlat5.z;
			        u_xlat5.xyz = u_xlat0.zxy * _UserLut_Params.zzz;
			        u_xlat33 = floor(u_xlat5.x);
			        u_xlat5.xw = _UserLut_Params.xy * vec2(0.5, 0.5);
			        u_xlat5.yz = u_xlat5.yz * _UserLut_Params.xy + u_xlat5.xw;
			        u_xlat5.x = u_xlat33 * _UserLut_Params.y + u_xlat5.y;
			        u_xlat16_8.xyz = textureLod(_UserLut, u_xlat5.xz, 0.0).xyz;
			        u_xlat9.x = _UserLut_Params.y;
			        u_xlat9.y = 0.0;
			        u_xlat5.xy = u_xlat5.xz + u_xlat9.xy;
			        u_xlat16_5.xyz = textureLod(_UserLut, u_xlat5.xy, 0.0).xyz;
			        u_xlat33 = u_xlat0.z * _UserLut_Params.z + (-u_xlat33);
			        u_xlat5.xyz = (-u_xlat16_8.xyz) + u_xlat16_5.xyz;
			        u_xlat5.xyz = vec3(u_xlat33) * u_xlat5.xyz + u_xlat16_8.xyz;
			        u_xlat5.xyz = (-u_xlat0.xyz) + u_xlat5.xyz;
			        u_xlat0.xyz = _UserLut_Params.www * u_xlat5.xyz + u_xlat0.xyz;
			        u_xlat16_6.xyz = min(u_xlat0.xyz, vec3(100.0, 100.0, 100.0));
			        u_xlat16_7.xyz = u_xlat16_6.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			        u_xlat16_10.xyz = u_xlat16_6.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			        u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			        u_xlat0.xyz = log2(abs(u_xlat16_10.xyz));
			        u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			        u_xlat0.xyz = exp2(u_xlat0.xyz);
			        u_xlatb5.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_6.xyzx).xyz;
			        u_xlat16_1.x = (u_xlatb5.x) ? u_xlat16_7.x : u_xlat0.x;
			        u_xlat16_1.y = (u_xlatb5.y) ? u_xlat16_7.y : u_xlat0.y;
			        u_xlat16_1.z = (u_xlatb5.z) ? u_xlat16_7.z : u_xlat0.z;
			    }
			    u_xlat0.xyz = u_xlat16_1.zxy * _Lut_Params.zzz;
			    u_xlat0.x = floor(u_xlat0.x);
			    u_xlat5.xy = _Lut_Params.xy * vec2(0.5, 0.5);
			    u_xlat5.yz = u_xlat0.yz * _Lut_Params.xy + u_xlat5.xy;
			    u_xlat5.x = u_xlat0.x * _Lut_Params.y + u_xlat5.y;
			    u_xlat16_11.xyz = textureLod(_InternalLut, u_xlat5.xz, 0.0).xyz;
			    u_xlat8.x = _Lut_Params.y;
			    u_xlat8.y = 0.0;
			    u_xlat5.xy = u_xlat5.xz + u_xlat8.xy;
			    u_xlat16_5.xyz = textureLod(_InternalLut, u_xlat5.xy, 0.0).xyz;
			    u_xlat0.x = u_xlat16_1.z * _Lut_Params.z + (-u_xlat0.x);
			    u_xlat5.xyz = (-u_xlat16_11.xyz) + u_xlat16_5.xyz;
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat5.xyz + u_xlat16_11.xyz;
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    {
			        vec4 hlslcc_movcTemp = u_xlat0;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_1.x : u_xlat0.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_1.y : u_xlat0.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_1.z : u_xlat0.z;
			        u_xlat0 = hlslcc_movcTemp;
			    }
			    u_xlat5.xy = vs_TEXCOORD0.xy * _Dithering_Params.xy + _Dithering_Params.zw;
			    u_xlat16_33 = texture(_BlueNoise_Texture, u_xlat5.xy, _GlobalMipBias.x).w;
			    u_xlat33 = u_xlat16_33 * 2.0 + -1.0;
			    u_xlatb5.x = u_xlat33>=0.0;
			    u_xlat5.x = (u_xlatb5.x) ? 1.0 : -1.0;
			    u_xlat33 = -abs(u_xlat33) + 1.0;
			    u_xlat33 = sqrt(u_xlat33);
			    u_xlat33 = (-u_xlat33) + 1.0;
			    u_xlat33 = u_xlat33 * u_xlat5.x;
			    u_xlat0.xyz = vec3(u_xlat33) * vec3(0.00392156886, 0.00392156886, 0.00392156886) + u_xlat0.xyz;
			    SV_Target0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
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
			#ifdef GL_EXT_shader_texture_lod
			#extension GL_EXT_shader_texture_lod : enable
			#endif

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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _UserLut_Params;
			uniform 	vec4 _Bloom_Params;
			uniform 	float _Bloom_RGBM;
			uniform 	mediump vec4 _Vignette_Params1;
			uniform 	vec4 _Vignette_Params2;
			uniform 	vec4 _Bloom_Texture_TexelSize;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _Bloom_Texture;
			UNITY_LOCATION(2) uniform mediump sampler2D _InternalLut;
			UNITY_LOCATION(3) uniform mediump sampler2D _UserLut;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			bvec3 u_xlatb0;
			mediump vec3 u_xlat16_1;
			vec4 u_xlat2;
			mediump vec4 u_xlat16_2;
			vec4 u_xlat3;
			mediump vec4 u_xlat16_3;
			vec3 u_xlat4;
			mediump vec3 u_xlat16_4;
			bvec3 u_xlatb4;
			vec4 u_xlat5;
			mediump vec4 u_xlat16_5;
			bvec3 u_xlatb5;
			vec3 u_xlat6;
			mediump vec3 u_xlat16_6;
			mediump vec3 u_xlat16_7;
			vec2 u_xlat8;
			mediump vec3 u_xlat16_8;
			vec2 u_xlat9;
			mediump vec3 u_xlat16_10;
			mediump vec3 u_xlat16_11;
			vec2 u_xlat22;
			vec2 u_xlat26;
			float u_xlat33;
			bool u_xlatb33;
			void main()
			{
			    u_xlat16_0.xyz = texture(_BlitTexture, vs_TEXCOORD0.xy, _GlobalMipBias.x).xyz;
			    u_xlat16_1.xyz = min(u_xlat16_0.xyz, vec3(100.0, 100.0, 100.0));
			    u_xlat16_2.xyz = u_xlat16_1.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			    u_xlat16_3.xyz = u_xlat16_1.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			    u_xlat0.xyz = log2(abs(u_xlat16_3.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlatb4.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_1.xyzx).xyz;
			    u_xlat16_1.x = (u_xlatb4.x) ? u_xlat16_2.x : u_xlat0.x;
			    u_xlat16_1.y = (u_xlatb4.y) ? u_xlat16_2.y : u_xlat0.y;
			    u_xlat16_1.z = (u_xlatb4.z) ? u_xlat16_2.z : u_xlat0.z;
			    u_xlat0.xy = vs_TEXCOORD0.xy * _Bloom_Texture_TexelSize.zw + vec2(0.5, 0.5);
			    u_xlat22.xy = floor(u_xlat0.xy);
			    u_xlat0.xy = fract(u_xlat0.xy);
			    u_xlat16_2 = (-u_xlat0.xyxy) * vec4(0.5, 0.5, 0.166666672, 0.166666672) + vec4(0.5, 0.5, 0.5, 0.5);
			    u_xlat16_2 = u_xlat0.xyxy * u_xlat16_2 + vec4(0.5, 0.5, -0.5, -0.5);
			    u_xlat16_3.xy = u_xlat0.xy * vec2(0.5, 0.5) + vec2(-1.0, -1.0);
			    u_xlat16_3.xy = u_xlat0.xy * u_xlat16_3.xy;
			    u_xlat16_3.xy = u_xlat16_3.xy * u_xlat0.xy + vec2(0.666666687, 0.666666687);
			    u_xlat16_2 = u_xlat0.xyxy * u_xlat16_2 + vec4(0.166666672, 0.166666672, 0.166666672, 0.166666672);
			    u_xlat0.xy = (-u_xlat16_3.xy) + vec2(1.0, 1.0);
			    u_xlat0.xy = (-u_xlat16_2.xy) + u_xlat0.xy;
			    u_xlat0.xy = (-u_xlat16_2.zw) + u_xlat0.xy;
			    u_xlat4.xy = u_xlat16_3.xy + u_xlat16_2.zw;
			    u_xlat26.xy = u_xlat0.xy + u_xlat16_2.xy;
			    u_xlat5.xy = vec2(1.0) / vec2(u_xlat4.xy);
			    u_xlat2.zw = u_xlat16_3.xy * u_xlat5.xy + vec2(-1.0, -1.0);
			    u_xlat5.xy = vec2(1.0) / vec2(u_xlat26.xy);
			    u_xlat2.xy = u_xlat0.xy * u_xlat5.xy + vec2(1.0, 1.0);
			    u_xlat3 = u_xlat22.xyxy + u_xlat2.zwxw;
			    u_xlat3 = u_xlat3 + vec4(-0.5, -0.5, -0.5, -0.5);
			    u_xlat3 = u_xlat3 * _Bloom_Texture_TexelSize.xyxy;
			    u_xlat3 = min(u_xlat3, vec4(1.0, 1.0, 1.0, 1.0));
			    u_xlat16_5 = textureLod(_Bloom_Texture, u_xlat3.xy, 0.0);
			    u_xlat16_3 = textureLod(_Bloom_Texture, u_xlat3.zw, 0.0);
			    u_xlat3 = u_xlat16_3 * u_xlat26.xxxx;
			    u_xlat3 = u_xlat4.xxxx * u_xlat16_5 + u_xlat3;
			    u_xlat0 = u_xlat22.xyxy + u_xlat2.zyxy;
			    u_xlat0 = u_xlat0 + vec4(-0.5, -0.5, -0.5, -0.5);
			    u_xlat0 = u_xlat0 * _Bloom_Texture_TexelSize.xyxy;
			    u_xlat0 = min(u_xlat0, vec4(1.0, 1.0, 1.0, 1.0));
			    u_xlat16_2 = textureLod(_Bloom_Texture, u_xlat0.xy, 0.0);
			    u_xlat16_0 = textureLod(_Bloom_Texture, u_xlat0.zw, 0.0);
			    u_xlat0 = u_xlat16_0 * u_xlat26.xxxx;
			    u_xlat0 = u_xlat4.xxxx * u_xlat16_2 + u_xlat0;
			    u_xlat0 = u_xlat0 * u_xlat26.yyyy;
			    u_xlat0 = u_xlat4.yyyy * u_xlat3 + u_xlat0;
			    u_xlat16_6.xyz = u_xlat0.xyz * u_xlat0.xyz;
			    u_xlatb0.x = 0.0<_Bloom_RGBM;
			    if(u_xlatb0.x){
			        u_xlat16_7.xyz = u_xlat0.www * u_xlat16_6.xyz;
			        u_xlat6.xyz = u_xlat16_7.xyz * vec3(8.0, 8.0, 8.0);
			        u_xlat16_6.xyz = u_xlat6.xyz;
			    }
			    u_xlat0.xyz = u_xlat16_6.xyz * _Bloom_Params.xxx;
			    u_xlat0.xyz = u_xlat0.xyz * _Bloom_Params.yzw + u_xlat16_1.xyz;
			    u_xlatb33 = 0.0<_Vignette_Params2.z;
			    if(u_xlatb33){
			        u_xlat4.xy = vs_TEXCOORD0.xy + (-_Vignette_Params2.xy);
			        u_xlat4.yz = abs(u_xlat4.xy) * _Vignette_Params2.zz;
			        u_xlat4.x = u_xlat4.y * _Vignette_Params1.w;
			        u_xlat33 = dot(u_xlat4.xz, u_xlat4.xz);
			        u_xlat33 = (-u_xlat33) + 1.0;
			        u_xlat33 = max(u_xlat33, 0.0);
			        u_xlat33 = log2(u_xlat33);
			        u_xlat33 = u_xlat33 * _Vignette_Params2.w;
			        u_xlat33 = exp2(u_xlat33);
			        u_xlat4.xyz = (-_Vignette_Params1.xyz) + vec3(1.0, 1.0, 1.0);
			        u_xlat4.xyz = vec3(u_xlat33) * u_xlat4.xyz + _Vignette_Params1.xyz;
			        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
			        u_xlat16_4.xyz = u_xlat4.xyz;
			    } else {
			        u_xlat16_4.xyz = u_xlat0.xyz;
			    }
			    u_xlat16_1.xyz = u_xlat16_4.xyz * _Lut_Params.www;
			    u_xlat16_1.xyz = clamp(u_xlat16_1.xyz, 0.0, 1.0);
			    u_xlatb0.x = 0.0<_UserLut_Params.w;
			    if(u_xlatb0.x){
			        u_xlatb0.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat16_1.xyzx).xyz;
			        u_xlat16_6.xyz = u_xlat16_1.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			        u_xlat5.xyz = log2(u_xlat16_1.xyz);
			        u_xlat5.xyz = u_xlat5.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			        u_xlat5.xyz = exp2(u_xlat5.xyz);
			        u_xlat5.xyz = u_xlat5.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			        u_xlat0.x = (u_xlatb0.x) ? u_xlat16_6.x : u_xlat5.x;
			        u_xlat0.y = (u_xlatb0.y) ? u_xlat16_6.y : u_xlat5.y;
			        u_xlat0.z = (u_xlatb0.z) ? u_xlat16_6.z : u_xlat5.z;
			        u_xlat5.xyz = u_xlat0.zxy * _UserLut_Params.zzz;
			        u_xlat33 = floor(u_xlat5.x);
			        u_xlat5.xw = _UserLut_Params.xy * vec2(0.5, 0.5);
			        u_xlat5.yz = u_xlat5.yz * _UserLut_Params.xy + u_xlat5.xw;
			        u_xlat5.x = u_xlat33 * _UserLut_Params.y + u_xlat5.y;
			        u_xlat16_8.xyz = textureLod(_UserLut, u_xlat5.xz, 0.0).xyz;
			        u_xlat9.x = _UserLut_Params.y;
			        u_xlat9.y = 0.0;
			        u_xlat5.xy = u_xlat5.xz + u_xlat9.xy;
			        u_xlat16_5.xyz = textureLod(_UserLut, u_xlat5.xy, 0.0).xyz;
			        u_xlat33 = u_xlat0.z * _UserLut_Params.z + (-u_xlat33);
			        u_xlat5.xyz = (-u_xlat16_8.xyz) + u_xlat16_5.xyz;
			        u_xlat5.xyz = vec3(u_xlat33) * u_xlat5.xyz + u_xlat16_8.xyz;
			        u_xlat5.xyz = (-u_xlat0.xyz) + u_xlat5.xyz;
			        u_xlat0.xyz = _UserLut_Params.www * u_xlat5.xyz + u_xlat0.xyz;
			        u_xlat16_6.xyz = min(u_xlat0.xyz, vec3(100.0, 100.0, 100.0));
			        u_xlat16_7.xyz = u_xlat16_6.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			        u_xlat16_10.xyz = u_xlat16_6.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			        u_xlat16_10.xyz = u_xlat16_10.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			        u_xlat0.xyz = log2(abs(u_xlat16_10.xyz));
			        u_xlat0.xyz = u_xlat0.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			        u_xlat0.xyz = exp2(u_xlat0.xyz);
			        u_xlatb5.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_6.xyzx).xyz;
			        u_xlat16_1.x = (u_xlatb5.x) ? u_xlat16_7.x : u_xlat0.x;
			        u_xlat16_1.y = (u_xlatb5.y) ? u_xlat16_7.y : u_xlat0.y;
			        u_xlat16_1.z = (u_xlatb5.z) ? u_xlat16_7.z : u_xlat0.z;
			    }
			    u_xlat0.xyz = u_xlat16_1.zxy * _Lut_Params.zzz;
			    u_xlat0.x = floor(u_xlat0.x);
			    u_xlat5.xy = _Lut_Params.xy * vec2(0.5, 0.5);
			    u_xlat5.yz = u_xlat0.yz * _Lut_Params.xy + u_xlat5.xy;
			    u_xlat5.x = u_xlat0.x * _Lut_Params.y + u_xlat5.y;
			    u_xlat16_11.xyz = textureLod(_InternalLut, u_xlat5.xz, 0.0).xyz;
			    u_xlat8.x = _Lut_Params.y;
			    u_xlat8.y = 0.0;
			    u_xlat5.xy = u_xlat5.xz + u_xlat8.xy;
			    u_xlat16_5.xyz = textureLod(_InternalLut, u_xlat5.xy, 0.0).xyz;
			    u_xlat0.x = u_xlat16_1.z * _Lut_Params.z + (-u_xlat0.x);
			    u_xlat5.xyz = (-u_xlat16_11.xyz) + u_xlat16_5.xyz;
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat5.xyz + u_xlat16_11.xyz;
			    u_xlatb5.xyz = greaterThanEqual(vec4(0.00313080009, 0.00313080009, 0.00313080009, 0.0), u_xlat0.xyzx).xyz;
			    u_xlat16_1.xyz = u_xlat0.xyz * vec3(12.9232101, 12.9232101, 12.9232101);
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
			    {
			        vec4 hlslcc_movcTemp = u_xlat0;
			        hlslcc_movcTemp.x = (u_xlatb5.x) ? u_xlat16_1.x : u_xlat0.x;
			        hlslcc_movcTemp.y = (u_xlatb5.y) ? u_xlat16_1.y : u_xlat0.y;
			        hlslcc_movcTemp.z = (u_xlatb5.z) ? u_xlat16_1.z : u_xlat0.z;
			        u_xlat0 = hlslcc_movcTemp;
			    }
			    SV_Target0.xyz = u_xlat0.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}