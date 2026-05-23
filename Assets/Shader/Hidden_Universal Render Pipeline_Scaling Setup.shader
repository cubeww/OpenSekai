Shader "Hidden/Universal Render Pipeline/Scaling Setup" {
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