Shader "Hidden/Universal/CoreBlit" {
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			void main()
			{
			    SV_Target0 = textureLod(_BlitTexture, vs_TEXCOORD0.xy, _BlitMipLevel);
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
			uniform 	vec4 _BlitScaleBiasRt;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    vs_TEXCOORD0.xy = u_xlat1.xw * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			void main()
			{
			    SV_Target0 = textureLod(_BlitTexture, vs_TEXCOORD0.xy, _BlitMipLevel);
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
			uniform 	vec4 _BlitScaleBiasRt;
			uniform 	vec2 _BlitTextureSize;
			uniform 	uint _BlitPaddingSize;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			vec3 u_xlat2;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
			    u_xlat0.x = float(_BlitPaddingSize);
			    u_xlat2.x = u_xlat0.x * 0.5;
			    u_xlat0.xz = u_xlat0.xx + vec2(_BlitTextureSize.x, _BlitTextureSize.y);
			    u_xlat2.xz = u_xlat2.xx / u_xlat0.xz;
			    u_xlat0.xz = u_xlat0.xz / vec2(_BlitTextureSize.x, _BlitTextureSize.y);
			    u_xlat2.xz = (-u_xlat2.xz) + u_xlat1.xw;
			    u_xlat0.xy = u_xlat0.xz * u_xlat2.xz;
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			void main()
			{
			    SV_Target0 = textureLod(_BlitTexture, vs_TEXCOORD0.xy, _BlitMipLevel);
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
			uniform 	vec4 _BlitScaleBiasRt;
			uniform 	vec2 _BlitTextureSize;
			uniform 	uint _BlitPaddingSize;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			vec3 u_xlat2;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
			    u_xlat0.x = float(_BlitPaddingSize);
			    u_xlat2.x = u_xlat0.x * 0.5;
			    u_xlat0.xz = u_xlat0.xx + vec2(_BlitTextureSize.x, _BlitTextureSize.y);
			    u_xlat2.xz = u_xlat2.xx / u_xlat0.xz;
			    u_xlat0.xz = u_xlat0.xz / vec2(_BlitTextureSize.x, _BlitTextureSize.y);
			    u_xlat2.xz = (-u_xlat2.xz) + u_xlat1.xw;
			    u_xlat0.xy = u_xlat0.xz * u_xlat2.xz;
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec2 u_xlat0;
			bvec2 u_xlatb0;
			vec3 u_xlat1;
			bvec3 u_xlatb1;
			vec4 u_xlat2;
			vec3 u_xlat3;
			vec2 u_xlat4;
			vec2 u_xlat5;
			vec2 u_xlat8;
			vec2 u_xlat9;
			void main()
			{
			    u_xlatb0.x = vs_TEXCOORD0.x<0.0;
			    if(u_xlatb0.x){
			        u_xlatb0.xy = lessThan(vs_TEXCOORD0.yyyy, vec4(0.0, 1.0, 0.0, 0.0)).xy;
			        u_xlat1.xyz = vs_TEXCOORD0.xyy + vec3(1.0, 1.0, -1.0);
			        u_xlat8.xy = (-vs_TEXCOORD0.xy) + vec2(0.0, 1.0);
			        u_xlat4.xy = (u_xlatb0.y) ? u_xlat8.xy : u_xlat1.xz;
			        u_xlat0.xy = (u_xlatb0.x) ? u_xlat1.xy : u_xlat4.xy;
			    } else {
			        u_xlatb1.xyz = lessThan(vs_TEXCOORD0.xyyx, vec4(1.0, 0.0, 1.0, 0.0)).xyz;
			        u_xlat2 = (-vs_TEXCOORD0.xyxy) + vec4(1.0, 2.0, 2.0, 1.0);
			        u_xlat8.xy = (u_xlatb1.z) ? vs_TEXCOORD0.xy : u_xlat2.xy;
			        u_xlat2.xy = (-vs_TEXCOORD0.xy) + vec2(1.0, 0.0);
			        u_xlat8.xy = (u_xlatb1.y) ? u_xlat2.xy : u_xlat8.xy;
			        u_xlat3.xyz = vs_TEXCOORD0.xyy + vec3(-1.0, 1.0, -1.0);
			        u_xlat9.xy = (u_xlatb1.z) ? u_xlat2.zw : u_xlat3.xz;
			        u_xlat5.xy = (u_xlatb1.y) ? u_xlat3.xy : u_xlat9.xy;
			        u_xlat0.xy = (u_xlatb1.x) ? u_xlat8.xy : u_xlat5.xy;
			    }
			    SV_Target0 = textureLod(_BlitTexture, u_xlat0.xy, _BlitMipLevel);
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
			uniform 	vec4 _BlitScaleBiasRt;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    vs_TEXCOORD0.xy = u_xlat1.xw * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump samplerCube _BlitCubeTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			vec3 u_xlat1;
			bvec2 u_xlatb2;
			vec2 u_xlat6;
			bvec2 u_xlatb6;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy;
			    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
			    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlatb6.xy = lessThan(u_xlat0.xyxy, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    u_xlat6.x = (u_xlatb6.x) ? (-u_xlat0.x) : u_xlat0.x;
			    u_xlat6.y = (u_xlatb6.y) ? (-u_xlat0.y) : u_xlat0.y;
			    u_xlat6.x = (-u_xlat6.x) + 1.0;
			    u_xlat1.z = (-u_xlat6.y) + u_xlat6.x;
			    u_xlat6.x = max((-u_xlat1.z), 0.0);
			    u_xlatb2.xy = greaterThanEqual(u_xlat0.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    {
			        vec2 hlslcc_movcTemp = u_xlat6;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? (-u_xlat6.x) : u_xlat6.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? (-u_xlat6.x) : u_xlat6.x;
			        u_xlat6 = hlslcc_movcTemp;
			    }
			    u_xlat1.xy = u_xlat6.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat1.xyz, u_xlat1.xyz);
			    u_xlat0.x = inversesqrt(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
			    u_xlat16_0.xyz = textureLod(_BlitCubeTexture, u_xlat0.xyz, _BlitMipLevel).xyz;
			    SV_Target0.xyz = u_xlat16_0.xyz;
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
			uniform 	vec4 _BlitScaleBiasRt;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    vs_TEXCOORD0.xy = u_xlat1.xw * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump samplerCube _BlitCubeTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			vec3 u_xlat1;
			bvec2 u_xlatb2;
			vec2 u_xlat6;
			bvec2 u_xlatb6;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy;
			    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
			    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlatb6.xy = lessThan(u_xlat0.xyxy, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    u_xlat6.x = (u_xlatb6.x) ? (-u_xlat0.x) : u_xlat0.x;
			    u_xlat6.y = (u_xlatb6.y) ? (-u_xlat0.y) : u_xlat0.y;
			    u_xlat6.x = (-u_xlat6.x) + 1.0;
			    u_xlat1.z = (-u_xlat6.y) + u_xlat6.x;
			    u_xlat6.x = max((-u_xlat1.z), 0.0);
			    u_xlatb2.xy = greaterThanEqual(u_xlat0.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    {
			        vec2 hlslcc_movcTemp = u_xlat6;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? (-u_xlat6.x) : u_xlat6.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? (-u_xlat6.x) : u_xlat6.x;
			        u_xlat6 = hlslcc_movcTemp;
			    }
			    u_xlat1.xy = u_xlat6.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat1.xyz, u_xlat1.xyz);
			    u_xlat0.x = inversesqrt(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
			    u_xlat16_0.xyz = textureLod(_BlitCubeTexture, u_xlat0.xyz, _BlitMipLevel).xyz;
			    SV_Target0 = vec4(dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036)));
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
			uniform 	vec4 _BlitScaleBiasRt;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    vs_TEXCOORD0.xy = u_xlat1.xw * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump samplerCube _BlitCubeTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec3 u_xlat0;
			mediump float u_xlat16_0;
			vec3 u_xlat1;
			bvec2 u_xlatb2;
			vec2 u_xlat6;
			bvec2 u_xlatb6;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy;
			    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
			    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlatb6.xy = lessThan(u_xlat0.xyxy, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    u_xlat6.x = (u_xlatb6.x) ? (-u_xlat0.x) : u_xlat0.x;
			    u_xlat6.y = (u_xlatb6.y) ? (-u_xlat0.y) : u_xlat0.y;
			    u_xlat6.x = (-u_xlat6.x) + 1.0;
			    u_xlat1.z = (-u_xlat6.y) + u_xlat6.x;
			    u_xlat6.x = max((-u_xlat1.z), 0.0);
			    u_xlatb2.xy = greaterThanEqual(u_xlat0.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    {
			        vec2 hlslcc_movcTemp = u_xlat6;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? (-u_xlat6.x) : u_xlat6.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? (-u_xlat6.x) : u_xlat6.x;
			        u_xlat6 = hlslcc_movcTemp;
			    }
			    u_xlat1.xy = u_xlat6.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat1.xyz, u_xlat1.xyz);
			    u_xlat0.x = inversesqrt(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
			    u_xlat16_0 = textureLod(_BlitCubeTexture, u_xlat0.xyz, _BlitMipLevel).w;
			    SV_Target0 = vec4(u_xlat16_0);
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
			uniform 	vec4 _BlitScaleBiasRt;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    vs_TEXCOORD0.xy = u_xlat1.xw * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump samplerCube _BlitCubeTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec3 u_xlat0;
			mediump float u_xlat16_0;
			vec3 u_xlat1;
			bvec2 u_xlatb2;
			vec2 u_xlat6;
			bvec2 u_xlatb6;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy;
			    u_xlat0.xy = clamp(u_xlat0.xy, 0.0, 1.0);
			    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlatb6.xy = lessThan(u_xlat0.xyxy, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    u_xlat6.x = (u_xlatb6.x) ? (-u_xlat0.x) : u_xlat0.x;
			    u_xlat6.y = (u_xlatb6.y) ? (-u_xlat0.y) : u_xlat0.y;
			    u_xlat6.x = (-u_xlat6.x) + 1.0;
			    u_xlat1.z = (-u_xlat6.y) + u_xlat6.x;
			    u_xlat6.x = max((-u_xlat1.z), 0.0);
			    u_xlatb2.xy = greaterThanEqual(u_xlat0.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    {
			        vec2 hlslcc_movcTemp = u_xlat6;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? (-u_xlat6.x) : u_xlat6.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? (-u_xlat6.x) : u_xlat6.x;
			        u_xlat6 = hlslcc_movcTemp;
			    }
			    u_xlat1.xy = u_xlat6.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat1.xyz, u_xlat1.xyz);
			    u_xlat0.x = inversesqrt(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
			    u_xlat16_0 = textureLod(_BlitCubeTexture, u_xlat0.xyz, _BlitMipLevel).x;
			    SV_Target0 = vec4(u_xlat16_0);
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
			uniform 	vec4 _BlitScaleBiasRt;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    vs_TEXCOORD0.xy = u_xlat1.xw * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			mediump vec3 u_xlat16_0;
			void main()
			{
			    u_xlat16_0.xyz = textureLod(_BlitTexture, vs_TEXCOORD0.xy, _BlitMipLevel).xyz;
			    SV_Target0 = vec4(dot(u_xlat16_0.xyz, vec3(0.212672904, 0.715152204, 0.0721750036)));
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
			uniform 	vec4 _BlitScaleBiasRt;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    vs_TEXCOORD0.xy = u_xlat1.xw * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			mediump float u_xlat16_0;
			void main()
			{
			    u_xlat16_0 = textureLod(_BlitTexture, vs_TEXCOORD0.xy, _BlitMipLevel).w;
			    SV_Target0 = vec4(u_xlat16_0);
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
			uniform 	vec4 _BlitScaleBiasRt;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    vs_TEXCOORD0.xy = u_xlat1.xw * _BlitScaleBias.xy + _BlitScaleBias.zw;
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			mediump float u_xlat16_0;
			void main()
			{
			    u_xlat16_0 = textureLod(_BlitTexture, vs_TEXCOORD0.xy, _BlitMipLevel).x;
			    SV_Target0 = vec4(u_xlat16_0);
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
			uniform 	vec4 _BlitScaleBiasRt;
			uniform 	vec2 _BlitTextureSize;
			uniform 	uint _BlitPaddingSize;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			vec3 u_xlat2;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
			    u_xlat0.x = float(_BlitPaddingSize);
			    u_xlat2.x = u_xlat0.x * 0.5;
			    u_xlat0.xz = u_xlat0.xx + vec2(_BlitTextureSize.x, _BlitTextureSize.y);
			    u_xlat2.xz = u_xlat2.xx / u_xlat0.xz;
			    u_xlat0.xz = u_xlat0.xz / vec2(_BlitTextureSize.x, _BlitTextureSize.y);
			    u_xlat2.xz = (-u_xlat2.xz) + u_xlat1.xw;
			    u_xlat0.xy = u_xlat0.xz * u_xlat2.xz;
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	float _BlitMipLevel;
			UNITY_LOCATION(0) uniform mediump samplerCube _BlitCubeTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec3 u_xlat16_0;
			bvec2 u_xlatb0;
			vec3 u_xlat1;
			bvec3 u_xlatb1;
			vec4 u_xlat2;
			bvec2 u_xlatb2;
			vec3 u_xlat3;
			vec2 u_xlat4;
			vec2 u_xlat5;
			vec2 u_xlat8;
			bvec2 u_xlatb8;
			vec2 u_xlat9;
			void main()
			{
			    u_xlatb0.x = vs_TEXCOORD0.x<0.0;
			    if(u_xlatb0.x){
			        u_xlatb0.xy = lessThan(vs_TEXCOORD0.yyyy, vec4(0.0, 1.0, 0.0, 0.0)).xy;
			        u_xlat1.xyz = vs_TEXCOORD0.xyy + vec3(1.0, 1.0, -1.0);
			        u_xlat8.xy = (-vs_TEXCOORD0.xy) + vec2(0.0, 1.0);
			        u_xlat4.xy = (u_xlatb0.y) ? u_xlat8.xy : u_xlat1.xz;
			        u_xlat0.xy = (u_xlatb0.x) ? u_xlat1.xy : u_xlat4.xy;
			    } else {
			        u_xlatb1.xyz = lessThan(vs_TEXCOORD0.xyyx, vec4(1.0, 0.0, 1.0, 0.0)).xyz;
			        u_xlat2 = (-vs_TEXCOORD0.xyxy) + vec4(1.0, 2.0, 2.0, 1.0);
			        u_xlat8.xy = (u_xlatb1.z) ? vs_TEXCOORD0.xy : u_xlat2.xy;
			        u_xlat2.xy = (-vs_TEXCOORD0.xy) + vec2(1.0, 0.0);
			        u_xlat8.xy = (u_xlatb1.y) ? u_xlat2.xy : u_xlat8.xy;
			        u_xlat3.xyz = vs_TEXCOORD0.xyy + vec3(-1.0, 1.0, -1.0);
			        u_xlat9.xy = (u_xlatb1.z) ? u_xlat2.zw : u_xlat3.xz;
			        u_xlat5.xy = (u_xlatb1.y) ? u_xlat3.xy : u_xlat9.xy;
			        u_xlat0.xy = (u_xlatb1.x) ? u_xlat8.xy : u_xlat5.xy;
			    }
			    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlatb8.xy = lessThan(u_xlat0.xyxy, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    u_xlat8.x = (u_xlatb8.x) ? (-u_xlat0.x) : u_xlat0.x;
			    u_xlat8.y = (u_xlatb8.y) ? (-u_xlat0.y) : u_xlat0.y;
			    u_xlat8.x = (-u_xlat8.x) + 1.0;
			    u_xlat1.z = (-u_xlat8.y) + u_xlat8.x;
			    u_xlat8.x = max((-u_xlat1.z), 0.0);
			    u_xlatb2.xy = greaterThanEqual(u_xlat0.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    {
			        vec2 hlslcc_movcTemp = u_xlat8;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? (-u_xlat8.x) : u_xlat8.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? (-u_xlat8.x) : u_xlat8.x;
			        u_xlat8 = hlslcc_movcTemp;
			    }
			    u_xlat1.xy = u_xlat8.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat1.xyz, u_xlat1.xyz);
			    u_xlat0.x = inversesqrt(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
			    u_xlat16_0.xyz = textureLod(_BlitCubeTexture, u_xlat0.xyz, _BlitMipLevel).xyz;
			    SV_Target0.xyz = u_xlat16_0.xyz;
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
			uniform 	vec4 _BlitScaleBiasRt;
			uniform 	vec2 _BlitTextureSize;
			uniform 	uint _BlitPaddingSize;
			out highp vec2 vs_TEXCOORD0;
			vec4 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec4 u_xlat1;
			vec3 u_xlat2;
			int u_xlati4;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xw = vec2(u_xlatu0.yx);
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.y = float(u_xlatu0.x);
			    u_xlat0.xy = u_xlat1.xy * _BlitScaleBiasRt.xy + _BlitScaleBiasRt.zw;
			    u_xlat0.z = float(-1.0);
			    u_xlat0.w = float(1.0);
			    gl_Position = u_xlat0 * vec4(2.0, -2.0, 1.0, 1.0) + vec4(-1.0, 1.0, 0.0, 0.0);
			    u_xlat0.x = float(_BlitPaddingSize);
			    u_xlat2.x = u_xlat0.x * 0.5;
			    u_xlat0.xz = u_xlat0.xx + vec2(_BlitTextureSize.x, _BlitTextureSize.y);
			    u_xlat2.xz = u_xlat2.xx / u_xlat0.xz;
			    u_xlat0.xz = u_xlat0.xz / vec2(_BlitTextureSize.x, _BlitTextureSize.y);
			    u_xlat2.xz = (-u_xlat2.xz) + u_xlat1.xw;
			    u_xlat0.xy = u_xlat0.xz * u_xlat2.xz;
			    vs_TEXCOORD0.xy = u_xlat0.xy * _BlitScaleBias.xy + _BlitScaleBias.zw;
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
			uniform 	float _BlitMipLevel;
			uniform 	vec4 _BlitDecodeInstructions;
			UNITY_LOCATION(0) uniform mediump samplerCube _BlitCubeTexture;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec3 u_xlat0;
			mediump vec4 u_xlat16_0;
			bvec2 u_xlatb0;
			vec3 u_xlat1;
			bvec3 u_xlatb1;
			vec4 u_xlat2;
			bvec2 u_xlatb2;
			vec3 u_xlat3;
			mediump vec3 u_xlat16_4;
			vec2 u_xlat5;
			vec2 u_xlat6;
			vec2 u_xlat10;
			bvec2 u_xlatb10;
			vec2 u_xlat11;
			void main()
			{
			    u_xlatb0.x = vs_TEXCOORD0.x<0.0;
			    if(u_xlatb0.x){
			        u_xlatb0.xy = lessThan(vs_TEXCOORD0.yyyy, vec4(0.0, 1.0, 0.0, 0.0)).xy;
			        u_xlat1.xyz = vs_TEXCOORD0.xyy + vec3(1.0, 1.0, -1.0);
			        u_xlat10.xy = (-vs_TEXCOORD0.xy) + vec2(0.0, 1.0);
			        u_xlat5.xy = (u_xlatb0.y) ? u_xlat10.xy : u_xlat1.xz;
			        u_xlat0.xy = (u_xlatb0.x) ? u_xlat1.xy : u_xlat5.xy;
			    } else {
			        u_xlatb1.xyz = lessThan(vs_TEXCOORD0.xyyx, vec4(1.0, 0.0, 1.0, 0.0)).xyz;
			        u_xlat2 = (-vs_TEXCOORD0.xyxy) + vec4(1.0, 2.0, 2.0, 1.0);
			        u_xlat10.xy = (u_xlatb1.z) ? vs_TEXCOORD0.xy : u_xlat2.xy;
			        u_xlat2.xy = (-vs_TEXCOORD0.xy) + vec2(1.0, 0.0);
			        u_xlat10.xy = (u_xlatb1.y) ? u_xlat2.xy : u_xlat10.xy;
			        u_xlat3.xyz = vs_TEXCOORD0.xyy + vec3(-1.0, 1.0, -1.0);
			        u_xlat11.xy = (u_xlatb1.z) ? u_xlat2.zw : u_xlat3.xz;
			        u_xlat6.xy = (u_xlatb1.y) ? u_xlat3.xy : u_xlat11.xy;
			        u_xlat0.xy = (u_xlatb1.x) ? u_xlat10.xy : u_xlat6.xy;
			    }
			    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlatb10.xy = lessThan(u_xlat0.xyxy, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    u_xlat10.x = (u_xlatb10.x) ? (-u_xlat0.x) : u_xlat0.x;
			    u_xlat10.y = (u_xlatb10.y) ? (-u_xlat0.y) : u_xlat0.y;
			    u_xlat10.x = (-u_xlat10.x) + 1.0;
			    u_xlat1.z = (-u_xlat10.y) + u_xlat10.x;
			    u_xlat10.x = max((-u_xlat1.z), 0.0);
			    u_xlatb2.xy = greaterThanEqual(u_xlat0.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			    {
			        vec2 hlslcc_movcTemp = u_xlat10;
			        hlslcc_movcTemp.x = (u_xlatb2.x) ? (-u_xlat10.x) : u_xlat10.x;
			        hlslcc_movcTemp.y = (u_xlatb2.y) ? (-u_xlat10.x) : u_xlat10.x;
			        u_xlat10 = hlslcc_movcTemp;
			    }
			    u_xlat1.xy = u_xlat10.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat1.xyz, u_xlat1.xyz);
			    u_xlat0.x = inversesqrt(u_xlat0.x);
			    u_xlat0.xyz = u_xlat0.xxx * u_xlat1.xyz;
			    u_xlat16_0 = textureLod(_BlitCubeTexture, u_xlat0.xyz, _BlitMipLevel);
			    u_xlat16_4.x = u_xlat16_0.w + -1.0;
			    u_xlat16_4.x = _BlitDecodeInstructions.w * u_xlat16_4.x + 1.0;
			    u_xlat16_4.x = max(u_xlat16_4.x, 0.0);
			    u_xlat16_4.x = log2(u_xlat16_4.x);
			    u_xlat16_4.x = u_xlat16_4.x * _BlitDecodeInstructions.y;
			    u_xlat16_4.x = exp2(u_xlat16_4.x);
			    u_xlat16_4.x = u_xlat16_4.x * _BlitDecodeInstructions.x;
			    u_xlat16_4.xyz = u_xlat16_0.xyz * u_xlat16_4.xxx;
			    SV_Target0.xyz = u_xlat16_4.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}