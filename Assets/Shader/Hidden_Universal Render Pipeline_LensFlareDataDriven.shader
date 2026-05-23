Shader "Hidden/Universal Render Pipeline/LensFlareDataDriven" {
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec2 u_xlat1;
			float u_xlat4;
			int u_xlati4;
			float u_xlat6;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu0.x);
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xy = vec2(u_xlatu0.yx);
			    u_xlat0.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat0.xy = u_xlat0.xy * _FlareData2.zw;
			    u_xlat4 = u_xlat0.y * _FlareData0.y;
			    u_xlat4 = u_xlat0.x * _FlareData0.x + (-u_xlat4);
			    u_xlat0.y = dot(u_xlat0.yx, _FlareData0.xy);
			    u_xlat6 = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlat0.x = u_xlat6 * u_xlat4;
			    u_xlat0.xy = u_xlat0.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			UNITY_LOCATION(0) uniform mediump sampler2D _FlareTex;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			mediump vec4 u_xlat16_0;
			void main()
			{
			    u_xlat16_0 = texture(_FlareTex, vs_TEXCOORD0.xy, _GlobalMipBias.x);
			    SV_Target0 = u_xlat16_0 * _FlareColorValue;
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec2 u_xlat1;
			float u_xlat4;
			int u_xlati4;
			float u_xlat6;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu0.x);
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xy = vec2(u_xlatu0.yx);
			    u_xlat0.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat0.xy = u_xlat0.xy * _FlareData2.zw;
			    u_xlat4 = u_xlat0.y * _FlareData0.y;
			    u_xlat4 = u_xlat0.x * _FlareData0.x + (-u_xlat4);
			    u_xlat0.y = dot(u_xlat0.yx, _FlareData0.xy);
			    u_xlat6 = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlat0.x = u_xlat6 * u_xlat4;
			    u_xlat0.xy = u_xlat0.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec2 u_xlat0;
			float u_xlat1;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat0.xy = u_xlat0.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x + -1.0;
			    u_xlat1 = _FlareData3.y + -1.0;
			    u_xlat0.x = u_xlat0.x / u_xlat1;
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    SV_Target0 = u_xlat0.xxxx * _FlareColorValue;
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec2 u_xlat1;
			float u_xlat4;
			int u_xlati4;
			float u_xlat6;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu0.x);
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xy = vec2(u_xlatu0.yx);
			    u_xlat0.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat0.xy = u_xlat0.xy * _FlareData2.zw;
			    u_xlat4 = u_xlat0.y * _FlareData0.y;
			    u_xlat4 = u_xlat0.x * _FlareData0.x + (-u_xlat4);
			    u_xlat0.y = dot(u_xlat0.yx, _FlareData0.xy);
			    u_xlat6 = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlat0.x = u_xlat6 * u_xlat4;
			    u_xlat0.xy = u_xlat0.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec2 u_xlat0;
			float u_xlat1;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat0.xy = u_xlat0.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x + -1.0;
			    u_xlat1 = _FlareData3.y + -1.0;
			    u_xlat0.x = u_xlat0.x / u_xlat1;
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat1 = (-u_xlat0.x) + 1.0;
			    u_xlat1 = u_xlat1 * u_xlat0.x;
			    u_xlat0.x = u_xlat0.x + 9.99999997e-07;
			    u_xlat0.x = u_xlat1 / u_xlat0.x;
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    SV_Target0 = u_xlat0.xxxx * _FlareColorValue;
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec2 u_xlat1;
			float u_xlat4;
			int u_xlati4;
			float u_xlat6;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu0.x);
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xy = vec2(u_xlatu0.yx);
			    u_xlat0.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat0.xy = u_xlat0.xy * _FlareData2.zw;
			    u_xlat4 = u_xlat0.y * _FlareData0.y;
			    u_xlat4 = u_xlat0.x * _FlareData0.x + (-u_xlat4);
			    u_xlat0.y = dot(u_xlat0.yx, _FlareData0.xy);
			    u_xlat6 = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlat0.x = u_xlat6 * u_xlat4;
			    u_xlat0.xy = u_xlat0.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareData4;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			int u_xlati1;
			bool u_xlatb1;
			vec2 u_xlat2;
			bool u_xlatb4;
			vec2 u_xlat6;
			int u_xlati6;
			float u_xlat9;
			int u_xlati9;
			bool u_xlatb9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlat6.x = max(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = float(1.0) / u_xlat6.x;
			    u_xlat9 = min(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = u_xlat6.x * u_xlat9;
			    u_xlat9 = u_xlat6.x * u_xlat6.x;
			    u_xlat1 = u_xlat9 * 0.0208350997 + -0.0851330012;
			    u_xlat1 = u_xlat9 * u_xlat1 + 0.180141002;
			    u_xlat1 = u_xlat9 * u_xlat1 + -0.330299497;
			    u_xlat9 = u_xlat9 * u_xlat1 + 0.999866009;
			    u_xlat1 = u_xlat9 * u_xlat6.x;
			    u_xlat1 = u_xlat1 * -2.0 + 1.57079637;
			    u_xlatb4 = abs(u_xlat0.x)<abs(u_xlat0.y);
			    u_xlat1 = u_xlatb4 ? u_xlat1 : float(0.0);
			    u_xlat6.x = u_xlat6.x * u_xlat9 + u_xlat1;
			    u_xlatb9 = u_xlat0.x<(-u_xlat0.x);
			    u_xlat9 = u_xlatb9 ? -3.14159274 : float(0.0);
			    u_xlat6.x = u_xlat9 + u_xlat6.x;
			    u_xlat9 = min(u_xlat0.x, u_xlat0.y);
			    u_xlatb9 = u_xlat9<(-u_xlat9);
			    u_xlat1 = max(u_xlat0.x, u_xlat0.y);
			    u_xlatb1 = u_xlat1>=(-u_xlat1);
			    u_xlatb9 = u_xlatb9 && u_xlatb1;
			    u_xlat6.x = (u_xlatb9) ? (-u_xlat6.x) : u_xlat6.x;
			    u_xlat6.x = _FlareData4.z * 0.5 + u_xlat6.x;
			    u_xlat6.x = u_xlat6.x / _FlareData4.z;
			    u_xlat6.x = floor(u_xlat6.x);
			    u_xlat6.x = u_xlat6.x * _FlareData4.z;
			    u_xlat1 = sin(u_xlat6.x);
			    u_xlat2.x = cos(u_xlat6.x);
			    u_xlat6.xy = u_xlat0.xy * u_xlat2.xx;
			    u_xlat2.x = u_xlat1 * u_xlat0.y + u_xlat6.x;
			    u_xlat2.y = (-u_xlat1) * u_xlat0.x + u_xlat6.y;
			    u_xlat0.xyz = (-_FlareData4.wxy);
			    u_xlat0.x = max(u_xlat0.x, u_xlat2.y);
			    u_xlat0.x = min(u_xlat0.x, _FlareData4.w);
			    u_xlat0.w = (-u_xlat0.x);
			    u_xlat0.xz = u_xlat0.zw + u_xlat2.xy;
			    u_xlati9 = int((0.0<u_xlat0.x) ? 0xFFFFFFFFu : uint(0));
			    u_xlati1 = int((u_xlat0.x<0.0) ? 0xFFFFFFFFu : uint(0));
			    u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlati6 = (-u_xlati9) + u_xlati1;
			    u_xlat6.x = float(u_xlati6);
			    u_xlat0.x = u_xlat0.x * u_xlat6.x + u_xlat0.y;
			    u_xlat0.x = u_xlat0.x * _FlareData3.y;
			    u_xlat0.x = (-u_xlat0.x);
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0.x = min(u_xlat0.x, 1.0);
			    SV_Target0 = u_xlat0.xxxx * _FlareColorValue;
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			out highp vec2 vs_TEXCOORD0;
			vec2 u_xlat0;
			int u_xlati0;
			uvec2 u_xlatu0;
			vec2 u_xlat1;
			float u_xlat4;
			int u_xlati4;
			float u_xlat6;
			void main()
			{
			    u_xlati0 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlatu0.y = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = (-u_xlati0) + (-int(u_xlatu0.y));
			    u_xlati0 = u_xlati0 + int(u_xlatu0.y);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu0.x);
			    u_xlati0 = u_xlati4 + 1;
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    u_xlat1.xy = vec2(u_xlatu0.yx);
			    u_xlat0.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat0.xy = u_xlat0.xy * _FlareData2.zw;
			    u_xlat4 = u_xlat0.y * _FlareData0.y;
			    u_xlat4 = u_xlat0.x * _FlareData0.x + (-u_xlat4);
			    u_xlat0.y = dot(u_xlat0.yx, _FlareData0.xy);
			    u_xlat6 = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlat0.x = u_xlat6 * u_xlat4;
			    u_xlat0.xy = u_xlat0.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareData4;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			int u_xlati1;
			bool u_xlatb1;
			vec2 u_xlat2;
			float u_xlat3;
			bool u_xlatb4;
			vec2 u_xlat6;
			int u_xlati6;
			float u_xlat9;
			int u_xlati9;
			bool u_xlatb9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlat6.x = max(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = float(1.0) / u_xlat6.x;
			    u_xlat9 = min(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = u_xlat6.x * u_xlat9;
			    u_xlat9 = u_xlat6.x * u_xlat6.x;
			    u_xlat1 = u_xlat9 * 0.0208350997 + -0.0851330012;
			    u_xlat1 = u_xlat9 * u_xlat1 + 0.180141002;
			    u_xlat1 = u_xlat9 * u_xlat1 + -0.330299497;
			    u_xlat9 = u_xlat9 * u_xlat1 + 0.999866009;
			    u_xlat1 = u_xlat9 * u_xlat6.x;
			    u_xlat1 = u_xlat1 * -2.0 + 1.57079637;
			    u_xlatb4 = abs(u_xlat0.x)<abs(u_xlat0.y);
			    u_xlat1 = u_xlatb4 ? u_xlat1 : float(0.0);
			    u_xlat6.x = u_xlat6.x * u_xlat9 + u_xlat1;
			    u_xlatb9 = u_xlat0.x<(-u_xlat0.x);
			    u_xlat9 = u_xlatb9 ? -3.14159274 : float(0.0);
			    u_xlat6.x = u_xlat9 + u_xlat6.x;
			    u_xlat9 = min(u_xlat0.x, u_xlat0.y);
			    u_xlatb9 = u_xlat9<(-u_xlat9);
			    u_xlat1 = max(u_xlat0.x, u_xlat0.y);
			    u_xlatb1 = u_xlat1>=(-u_xlat1);
			    u_xlatb9 = u_xlatb9 && u_xlatb1;
			    u_xlat6.x = (u_xlatb9) ? (-u_xlat6.x) : u_xlat6.x;
			    u_xlat6.x = _FlareData4.z * 0.5 + u_xlat6.x;
			    u_xlat6.x = u_xlat6.x / _FlareData4.z;
			    u_xlat6.x = floor(u_xlat6.x);
			    u_xlat6.x = u_xlat6.x * _FlareData4.z;
			    u_xlat1 = sin(u_xlat6.x);
			    u_xlat2.x = cos(u_xlat6.x);
			    u_xlat6.xy = u_xlat0.xy * u_xlat2.xx;
			    u_xlat2.x = u_xlat1 * u_xlat0.y + u_xlat6.x;
			    u_xlat2.y = (-u_xlat1) * u_xlat0.x + u_xlat6.y;
			    u_xlat0.xyz = (-_FlareData4.wxy);
			    u_xlat0.x = max(u_xlat0.x, u_xlat2.y);
			    u_xlat0.x = min(u_xlat0.x, _FlareData4.w);
			    u_xlat0.w = (-u_xlat0.x);
			    u_xlat0.xz = u_xlat0.zw + u_xlat2.xy;
			    u_xlati9 = int((0.0<u_xlat0.x) ? 0xFFFFFFFFu : uint(0));
			    u_xlati1 = int((u_xlat0.x<0.0) ? 0xFFFFFFFFu : uint(0));
			    u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlati6 = (-u_xlati9) + u_xlati1;
			    u_xlat6.x = float(u_xlati6);
			    u_xlat0.x = u_xlat0.x * u_xlat6.x + u_xlat0.y;
			    u_xlat0.x = u_xlat0.x * _FlareData3.y;
			    u_xlat0.x = (-u_xlat0.x);
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat3 = (-u_xlat0.x) + 1.0;
			    u_xlat3 = u_xlat3 * u_xlat0.x;
			    u_xlat0.x = u_xlat0.x + 9.99999997e-07;
			    u_xlat0.x = u_xlat3 / u_xlat0.x;
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0.x = min(u_xlat0.x, 1.0);
			    SV_Target0 = u_xlat0.xxxx * _FlareColorValue;
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	int unity_StereoEyeIndex;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareOcclusionIndex;
			UNITY_LOCATION(1) uniform mediump sampler2DArray _FlareOcclusionTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			bool u_xlatb0;
			vec2 u_xlat1;
			vec2 u_xlat2;
			int u_xlati2;
			uvec3 u_xlatu2;
			bvec2 u_xlatb2;
			vec2 u_xlat3;
			int u_xlati4;
			bvec2 u_xlatb4;
			float u_xlat6;
			int u_xlati6;
			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu2.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati6 = (-u_xlati4) + (-int(u_xlatu2.x));
			    u_xlati6 = u_xlati6 + 1;
			    u_xlatu2.z = uint(uint(u_xlati6) & 1u);
			    u_xlat1.xy = vec2(u_xlatu2.xz);
			    u_xlat3.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati2 = u_xlati4 + int(u_xlatu2.x);
			    u_xlatu2.x = uint(uint(u_xlati2) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu2.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat2.xy = u_xlat3.xy * _FlareData2.zw;
			    u_xlat6 = u_xlat2.y * _FlareData0.y;
			    u_xlat6 = u_xlat2.x * _FlareData0.x + (-u_xlat6);
			    u_xlat1.y = dot(u_xlat2.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat6;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb2.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb2.x = u_xlatb2.y || u_xlatb2.x;
			    u_xlatb4.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb4.x = u_xlatb4.y || u_xlatb4.x;
			    u_xlatb2.x = u_xlatb4.x || u_xlatb2.x;
			    u_xlatb0 = u_xlatb2.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatu0.x = uint(_FlareOcclusionIndex.x);
			        u_xlatu0.y = uint(uint(0u));
			        u_xlatu0.w = uint(uint(0u));
			        u_xlatu0.z =  uint(unity_StereoEyeIndex);
			        u_xlat0.x = texelFetch(_FlareOcclusionTex, ivec3(u_xlatu0.xyz), int(u_xlatu0.w)).x;
			        vs_TEXCOORD1 = u_xlat0.x;
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			UNITY_LOCATION(0) uniform mediump sampler2D _FlareTex;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			void main()
			{
			    u_xlat16_0 = texture(_FlareTex, vs_TEXCOORD0.xy, _GlobalMipBias.x);
			    u_xlat0 = u_xlat16_0 * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	int unity_StereoEyeIndex;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareOcclusionIndex;
			UNITY_LOCATION(0) uniform mediump sampler2DArray _FlareOcclusionTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			bool u_xlatb0;
			vec2 u_xlat1;
			vec2 u_xlat2;
			int u_xlati2;
			uvec3 u_xlatu2;
			bvec2 u_xlatb2;
			vec2 u_xlat3;
			int u_xlati4;
			bvec2 u_xlatb4;
			float u_xlat6;
			int u_xlati6;
			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu2.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati6 = (-u_xlati4) + (-int(u_xlatu2.x));
			    u_xlati6 = u_xlati6 + 1;
			    u_xlatu2.z = uint(uint(u_xlati6) & 1u);
			    u_xlat1.xy = vec2(u_xlatu2.xz);
			    u_xlat3.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati2 = u_xlati4 + int(u_xlatu2.x);
			    u_xlatu2.x = uint(uint(u_xlati2) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu2.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat2.xy = u_xlat3.xy * _FlareData2.zw;
			    u_xlat6 = u_xlat2.y * _FlareData0.y;
			    u_xlat6 = u_xlat2.x * _FlareData0.x + (-u_xlat6);
			    u_xlat1.y = dot(u_xlat2.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat6;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb2.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb2.x = u_xlatb2.y || u_xlatb2.x;
			    u_xlatb4.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb4.x = u_xlatb4.y || u_xlatb4.x;
			    u_xlatb2.x = u_xlatb4.x || u_xlatb2.x;
			    u_xlatb0 = u_xlatb2.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatu0.x = uint(_FlareOcclusionIndex.x);
			        u_xlatu0.y = uint(uint(0u));
			        u_xlatu0.w = uint(uint(0u));
			        u_xlatu0.z =  uint(unity_StereoEyeIndex);
			        u_xlat0.x = texelFetch(_FlareOcclusionTex, ivec3(u_xlatu0.xyz), int(u_xlatu0.w)).x;
			        vs_TEXCOORD1 = u_xlat0.x;
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat0.xy = u_xlat0.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x + -1.0;
			    u_xlat1 = _FlareData3.y + -1.0;
			    u_xlat0.x = u_xlat0.x / u_xlat1;
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0 = u_xlat0.xxxx * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	int unity_StereoEyeIndex;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareOcclusionIndex;
			UNITY_LOCATION(0) uniform mediump sampler2DArray _FlareOcclusionTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			bool u_xlatb0;
			vec2 u_xlat1;
			vec2 u_xlat2;
			int u_xlati2;
			uvec3 u_xlatu2;
			bvec2 u_xlatb2;
			vec2 u_xlat3;
			int u_xlati4;
			bvec2 u_xlatb4;
			float u_xlat6;
			int u_xlati6;
			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu2.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati6 = (-u_xlati4) + (-int(u_xlatu2.x));
			    u_xlati6 = u_xlati6 + 1;
			    u_xlatu2.z = uint(uint(u_xlati6) & 1u);
			    u_xlat1.xy = vec2(u_xlatu2.xz);
			    u_xlat3.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati2 = u_xlati4 + int(u_xlatu2.x);
			    u_xlatu2.x = uint(uint(u_xlati2) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu2.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat2.xy = u_xlat3.xy * _FlareData2.zw;
			    u_xlat6 = u_xlat2.y * _FlareData0.y;
			    u_xlat6 = u_xlat2.x * _FlareData0.x + (-u_xlat6);
			    u_xlat1.y = dot(u_xlat2.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat6;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb2.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb2.x = u_xlatb2.y || u_xlatb2.x;
			    u_xlatb4.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb4.x = u_xlatb4.y || u_xlatb4.x;
			    u_xlatb2.x = u_xlatb4.x || u_xlatb2.x;
			    u_xlatb0 = u_xlatb2.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatu0.x = uint(_FlareOcclusionIndex.x);
			        u_xlatu0.y = uint(uint(0u));
			        u_xlatu0.w = uint(uint(0u));
			        u_xlatu0.z =  uint(unity_StereoEyeIndex);
			        u_xlat0.x = texelFetch(_FlareOcclusionTex, ivec3(u_xlatu0.xyz), int(u_xlatu0.w)).x;
			        vs_TEXCOORD1 = u_xlat0.x;
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat0.xy = u_xlat0.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x + -1.0;
			    u_xlat1 = _FlareData3.y + -1.0;
			    u_xlat0.x = u_xlat0.x / u_xlat1;
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat1 = (-u_xlat0.x) + 1.0;
			    u_xlat1 = u_xlat1 * u_xlat0.x;
			    u_xlat0.x = u_xlat0.x + 9.99999997e-07;
			    u_xlat0.x = u_xlat1 / u_xlat0.x;
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0 = u_xlat0.xxxx * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	int unity_StereoEyeIndex;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareOcclusionIndex;
			UNITY_LOCATION(0) uniform mediump sampler2DArray _FlareOcclusionTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			bool u_xlatb0;
			vec2 u_xlat1;
			vec2 u_xlat2;
			int u_xlati2;
			uvec3 u_xlatu2;
			bvec2 u_xlatb2;
			vec2 u_xlat3;
			int u_xlati4;
			bvec2 u_xlatb4;
			float u_xlat6;
			int u_xlati6;
			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu2.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati6 = (-u_xlati4) + (-int(u_xlatu2.x));
			    u_xlati6 = u_xlati6 + 1;
			    u_xlatu2.z = uint(uint(u_xlati6) & 1u);
			    u_xlat1.xy = vec2(u_xlatu2.xz);
			    u_xlat3.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati2 = u_xlati4 + int(u_xlatu2.x);
			    u_xlatu2.x = uint(uint(u_xlati2) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu2.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat2.xy = u_xlat3.xy * _FlareData2.zw;
			    u_xlat6 = u_xlat2.y * _FlareData0.y;
			    u_xlat6 = u_xlat2.x * _FlareData0.x + (-u_xlat6);
			    u_xlat1.y = dot(u_xlat2.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat6;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb2.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb2.x = u_xlatb2.y || u_xlatb2.x;
			    u_xlatb4.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb4.x = u_xlatb4.y || u_xlatb4.x;
			    u_xlatb2.x = u_xlatb4.x || u_xlatb2.x;
			    u_xlatb0 = u_xlatb2.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatu0.x = uint(_FlareOcclusionIndex.x);
			        u_xlatu0.y = uint(uint(0u));
			        u_xlatu0.w = uint(uint(0u));
			        u_xlatu0.z =  uint(unity_StereoEyeIndex);
			        u_xlat0.x = texelFetch(_FlareOcclusionTex, ivec3(u_xlatu0.xyz), int(u_xlatu0.w)).x;
			        vs_TEXCOORD1 = u_xlat0.x;
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareData4;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			int u_xlati1;
			bool u_xlatb1;
			vec2 u_xlat2;
			bool u_xlatb4;
			vec2 u_xlat6;
			int u_xlati6;
			float u_xlat9;
			int u_xlati9;
			bool u_xlatb9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlat6.x = max(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = float(1.0) / u_xlat6.x;
			    u_xlat9 = min(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = u_xlat6.x * u_xlat9;
			    u_xlat9 = u_xlat6.x * u_xlat6.x;
			    u_xlat1 = u_xlat9 * 0.0208350997 + -0.0851330012;
			    u_xlat1 = u_xlat9 * u_xlat1 + 0.180141002;
			    u_xlat1 = u_xlat9 * u_xlat1 + -0.330299497;
			    u_xlat9 = u_xlat9 * u_xlat1 + 0.999866009;
			    u_xlat1 = u_xlat9 * u_xlat6.x;
			    u_xlat1 = u_xlat1 * -2.0 + 1.57079637;
			    u_xlatb4 = abs(u_xlat0.x)<abs(u_xlat0.y);
			    u_xlat1 = u_xlatb4 ? u_xlat1 : float(0.0);
			    u_xlat6.x = u_xlat6.x * u_xlat9 + u_xlat1;
			    u_xlatb9 = u_xlat0.x<(-u_xlat0.x);
			    u_xlat9 = u_xlatb9 ? -3.14159274 : float(0.0);
			    u_xlat6.x = u_xlat9 + u_xlat6.x;
			    u_xlat9 = min(u_xlat0.x, u_xlat0.y);
			    u_xlatb9 = u_xlat9<(-u_xlat9);
			    u_xlat1 = max(u_xlat0.x, u_xlat0.y);
			    u_xlatb1 = u_xlat1>=(-u_xlat1);
			    u_xlatb9 = u_xlatb9 && u_xlatb1;
			    u_xlat6.x = (u_xlatb9) ? (-u_xlat6.x) : u_xlat6.x;
			    u_xlat6.x = _FlareData4.z * 0.5 + u_xlat6.x;
			    u_xlat6.x = u_xlat6.x / _FlareData4.z;
			    u_xlat6.x = floor(u_xlat6.x);
			    u_xlat6.x = u_xlat6.x * _FlareData4.z;
			    u_xlat1 = sin(u_xlat6.x);
			    u_xlat2.x = cos(u_xlat6.x);
			    u_xlat6.xy = u_xlat0.xy * u_xlat2.xx;
			    u_xlat2.x = u_xlat1 * u_xlat0.y + u_xlat6.x;
			    u_xlat2.y = (-u_xlat1) * u_xlat0.x + u_xlat6.y;
			    u_xlat0.xyz = (-_FlareData4.wxy);
			    u_xlat0.x = max(u_xlat0.x, u_xlat2.y);
			    u_xlat0.x = min(u_xlat0.x, _FlareData4.w);
			    u_xlat0.w = (-u_xlat0.x);
			    u_xlat0.xz = u_xlat0.zw + u_xlat2.xy;
			    u_xlati9 = int((0.0<u_xlat0.x) ? 0xFFFFFFFFu : uint(0));
			    u_xlati1 = int((u_xlat0.x<0.0) ? 0xFFFFFFFFu : uint(0));
			    u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlati6 = (-u_xlati9) + u_xlati1;
			    u_xlat6.x = float(u_xlati6);
			    u_xlat0.x = u_xlat0.x * u_xlat6.x + u_xlat0.y;
			    u_xlat0.x = u_xlat0.x * _FlareData3.y;
			    u_xlat0.x = (-u_xlat0.x);
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0.x = min(u_xlat0.x, 1.0);
			    u_xlat0 = u_xlat0.xxxx * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	int unity_StereoEyeIndex;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareOcclusionIndex;
			UNITY_LOCATION(0) uniform mediump sampler2DArray _FlareOcclusionTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			uvec4 u_xlatu0;
			bool u_xlatb0;
			vec2 u_xlat1;
			vec2 u_xlat2;
			int u_xlati2;
			uvec3 u_xlatu2;
			bvec2 u_xlatb2;
			vec2 u_xlat3;
			int u_xlati4;
			bvec2 u_xlatb4;
			float u_xlat6;
			int u_xlati6;
			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu2.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati4 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati6 = (-u_xlati4) + (-int(u_xlatu2.x));
			    u_xlati6 = u_xlati6 + 1;
			    u_xlatu2.z = uint(uint(u_xlati6) & 1u);
			    u_xlat1.xy = vec2(u_xlatu2.xz);
			    u_xlat3.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati2 = u_xlati4 + int(u_xlatu2.x);
			    u_xlatu2.x = uint(uint(u_xlati2) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu2.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat2.xy = u_xlat3.xy * _FlareData2.zw;
			    u_xlat6 = u_xlat2.y * _FlareData0.y;
			    u_xlat6 = u_xlat2.x * _FlareData0.x + (-u_xlat6);
			    u_xlat1.y = dot(u_xlat2.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat6;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb2.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb2.x = u_xlatb2.y || u_xlatb2.x;
			    u_xlatb4.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb4.x = u_xlatb4.y || u_xlatb4.x;
			    u_xlatb2.x = u_xlatb4.x || u_xlatb2.x;
			    u_xlatb0 = u_xlatb2.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatu0.x = uint(_FlareOcclusionIndex.x);
			        u_xlatu0.y = uint(uint(0u));
			        u_xlatu0.w = uint(uint(0u));
			        u_xlatu0.z =  uint(unity_StereoEyeIndex);
			        u_xlat0.x = texelFetch(_FlareOcclusionTex, ivec3(u_xlatu0.xyz), int(u_xlatu0.w)).x;
			        vs_TEXCOORD1 = u_xlat0.x;
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareData4;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			int u_xlati1;
			bool u_xlatb1;
			vec2 u_xlat2;
			float u_xlat3;
			bool u_xlatb4;
			vec2 u_xlat6;
			int u_xlati6;
			float u_xlat9;
			int u_xlati9;
			bool u_xlatb9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlat6.x = max(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = float(1.0) / u_xlat6.x;
			    u_xlat9 = min(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = u_xlat6.x * u_xlat9;
			    u_xlat9 = u_xlat6.x * u_xlat6.x;
			    u_xlat1 = u_xlat9 * 0.0208350997 + -0.0851330012;
			    u_xlat1 = u_xlat9 * u_xlat1 + 0.180141002;
			    u_xlat1 = u_xlat9 * u_xlat1 + -0.330299497;
			    u_xlat9 = u_xlat9 * u_xlat1 + 0.999866009;
			    u_xlat1 = u_xlat9 * u_xlat6.x;
			    u_xlat1 = u_xlat1 * -2.0 + 1.57079637;
			    u_xlatb4 = abs(u_xlat0.x)<abs(u_xlat0.y);
			    u_xlat1 = u_xlatb4 ? u_xlat1 : float(0.0);
			    u_xlat6.x = u_xlat6.x * u_xlat9 + u_xlat1;
			    u_xlatb9 = u_xlat0.x<(-u_xlat0.x);
			    u_xlat9 = u_xlatb9 ? -3.14159274 : float(0.0);
			    u_xlat6.x = u_xlat9 + u_xlat6.x;
			    u_xlat9 = min(u_xlat0.x, u_xlat0.y);
			    u_xlatb9 = u_xlat9<(-u_xlat9);
			    u_xlat1 = max(u_xlat0.x, u_xlat0.y);
			    u_xlatb1 = u_xlat1>=(-u_xlat1);
			    u_xlatb9 = u_xlatb9 && u_xlatb1;
			    u_xlat6.x = (u_xlatb9) ? (-u_xlat6.x) : u_xlat6.x;
			    u_xlat6.x = _FlareData4.z * 0.5 + u_xlat6.x;
			    u_xlat6.x = u_xlat6.x / _FlareData4.z;
			    u_xlat6.x = floor(u_xlat6.x);
			    u_xlat6.x = u_xlat6.x * _FlareData4.z;
			    u_xlat1 = sin(u_xlat6.x);
			    u_xlat2.x = cos(u_xlat6.x);
			    u_xlat6.xy = u_xlat0.xy * u_xlat2.xx;
			    u_xlat2.x = u_xlat1 * u_xlat0.y + u_xlat6.x;
			    u_xlat2.y = (-u_xlat1) * u_xlat0.x + u_xlat6.y;
			    u_xlat0.xyz = (-_FlareData4.wxy);
			    u_xlat0.x = max(u_xlat0.x, u_xlat2.y);
			    u_xlat0.x = min(u_xlat0.x, _FlareData4.w);
			    u_xlat0.w = (-u_xlat0.x);
			    u_xlat0.xz = u_xlat0.zw + u_xlat2.xy;
			    u_xlati9 = int((0.0<u_xlat0.x) ? 0xFFFFFFFFu : uint(0));
			    u_xlati1 = int((u_xlat0.x<0.0) ? 0xFFFFFFFFu : uint(0));
			    u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlati6 = (-u_xlati9) + u_xlati1;
			    u_xlat6.x = float(u_xlati6);
			    u_xlat0.x = u_xlat0.x * u_xlat6.x + u_xlat0.y;
			    u_xlat0.x = u_xlat0.x * _FlareData3.y;
			    u_xlat0.x = (-u_xlat0.x);
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat3 = (-u_xlat0.x) + 1.0;
			    u_xlat3 = u_xlat3 * u_xlat0.x;
			    u_xlat0.x = u_xlat0.x + 9.99999997e-07;
			    u_xlat0.x = u_xlat3 / u_xlat0.x;
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0.x = min(u_xlat0.x, 1.0);
			    u_xlat0 = u_xlat0.xxxx * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _ZBufferParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData1;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			UNITY_LOCATION(1) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(2) uniform mediump sampler2D _FlareOcclusionRemapTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			bool u_xlatb0;
			vec2 u_xlat1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec2 u_xlat2;
			uint u_xlatu3;
			bvec2 u_xlatb3;
			mediump vec2 u_xlat16_4;
			mediump float u_xlat16_5;
			mediump vec2 u_xlat16_6;
			vec2 u_xlat7;
			int u_xlati7;
			uvec3 u_xlatu7;
			bvec2 u_xlatb7;
			vec2 u_xlat8;
			bvec2 u_xlatb10;
			mediump float u_xlat16_11;
			int u_xlati14;
			bvec2 u_xlatb14;
			vec2 u_xlat16;
			int u_xlati16;
			uint u_xlatu16;
			bool u_xlatb16;
			float u_xlat21;
			int u_xlati21;
			uint u_xlatu21;
			float u_xlat23;
			int u_xlati23;
			uint u_xlatu23;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu7.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati14 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati21 = (-u_xlati14) + (-int(u_xlatu7.x));
			    u_xlati21 = u_xlati21 + 1;
			    u_xlatu7.z = uint(uint(u_xlati21) & 1u);
			    u_xlat1.xy = vec2(u_xlatu7.xz);
			    u_xlat8.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati7 = u_xlati14 + int(u_xlatu7.x);
			    u_xlatu7.x = uint(uint(u_xlati7) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu7.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat7.xy = u_xlat8.xy * _FlareData2.zw;
			    u_xlat21 = u_xlat7.y * _FlareData0.y;
			    u_xlat21 = u_xlat7.x * _FlareData0.x + (-u_xlat21);
			    u_xlat1.y = dot(u_xlat7.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat21;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb7.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb7.x = u_xlatb7.y || u_xlatb7.x;
			    u_xlatb14.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb14.x = u_xlatb14.y || u_xlatb14.x;
			    u_xlatb7.x = u_xlatb14.x || u_xlatb7.x;
			    u_xlatb0 = u_xlatb7.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatb0 = _FlareData1.y!=0.0;
			        if(u_xlatb0){
			            u_xlat0.x = float(1.0) / _FlareData1.y;
			            u_xlatu7.x = uint(_FlareData1.y);
			            u_xlatb14.x = 0.0<_FlareData3.x;
			            u_xlatu1.z = uint(uint(0u));
			            u_xlatu1.w = uint(uint(0u));
			            u_xlat2.x = 0.0;
			            for(uint u_xlatu_loop_1 = uint(0u) ; u_xlatu_loop_1<u_xlatu7.x ; u_xlatu_loop_1++)
			            {
			                u_xlati16 = int(int(u_xlatu_loop_1) << (1 & int(0x1F)));
			                u_xlati16 = int(uint(uint(u_xlati16) ^ 2747636419u));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlat16.x = float(u_xlatu16);
			                u_xlat16.x = u_xlat16.x * 2.32830644e-10;
			                u_xlati23 = int(int_bitfieldInsert(1, int(u_xlatu_loop_1), 1 & int(0x1F), 31));
			                u_xlati23 = int(uint(uint(u_xlati23) ^ 2747636419u));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlat23 = float(u_xlatu23);
			                u_xlat16_4.x = sqrt(u_xlat16.x);
			                u_xlat16_11 = u_xlat23 * 1.46291812e-09;
			                u_xlat16_5 = sin(u_xlat16_11);
			                u_xlat16_6.x = cos(u_xlat16_11);
			                u_xlat16_6.y = u_xlat16_5;
			                u_xlat16_4.xy = u_xlat16_4.xx * u_xlat16_6.xy;
			                u_xlat16.xy = _FlareData1.xx * u_xlat16_4.xy + _FlareData2.xy;
			                u_xlat16.xy = u_xlat16.xy * vec2(0.5, 0.5) + vec2(0.5, 0.5);
			                u_xlatb3.xy = greaterThanEqual(u_xlat16.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			                u_xlatb3.x = u_xlatb3.y && u_xlatb3.x;
			                u_xlatb10.xy = greaterThanEqual(vec4(1.0, 1.0, 0.0, 0.0), u_xlat16.xyxx).xy;
			                u_xlatb10.x = u_xlatb10.y && u_xlatb10.x;
			                u_xlatb3.x = u_xlatb10.x && u_xlatb3.x;
			                if(u_xlatb3.x){
			                    u_xlat16.xy = u_xlat16.xy * _ScaledScreenParams.xy;
			                    u_xlatu1.xy = uvec2(u_xlat16.xy);
			                    u_xlat1.x = texelFetch(_CameraDepthTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).x;
			                    u_xlat1.x = _ZBufferParams.z * u_xlat1.x + _ZBufferParams.w;
			                    u_xlat1.x = float(1.0) / u_xlat1.x;
			                    u_xlatb1 = _FlareData1.z<u_xlat1.x;
			                    u_xlat8.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb1) ? u_xlat8.x : u_xlat2.x;
			                } else {
			                    u_xlat1.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb14.x) ? u_xlat1.x : u_xlat2.x;
			                }
			            }
			            u_xlat2.x = u_xlat2.x;
			            u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			            u_xlat2.y = 0.0;
			            u_xlat0.x = textureLod(_FlareOcclusionRemapTex, u_xlat2.xy, 0.0).x;
			            vs_TEXCOORD1 = u_xlat0.x;
			            vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
			        } else {
			            vs_TEXCOORD1 = 1.0;
			        }
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			UNITY_LOCATION(0) uniform mediump sampler2D _FlareTex;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			void main()
			{
			    u_xlat16_0 = texture(_FlareTex, vs_TEXCOORD0.xy, _GlobalMipBias.x);
			    u_xlat0 = u_xlat16_0 * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _ZBufferParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData1;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			UNITY_LOCATION(0) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _FlareOcclusionRemapTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			bool u_xlatb0;
			vec2 u_xlat1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec2 u_xlat2;
			uint u_xlatu3;
			bvec2 u_xlatb3;
			mediump vec2 u_xlat16_4;
			mediump float u_xlat16_5;
			mediump vec2 u_xlat16_6;
			vec2 u_xlat7;
			int u_xlati7;
			uvec3 u_xlatu7;
			bvec2 u_xlatb7;
			vec2 u_xlat8;
			bvec2 u_xlatb10;
			mediump float u_xlat16_11;
			int u_xlati14;
			bvec2 u_xlatb14;
			vec2 u_xlat16;
			int u_xlati16;
			uint u_xlatu16;
			bool u_xlatb16;
			float u_xlat21;
			int u_xlati21;
			uint u_xlatu21;
			float u_xlat23;
			int u_xlati23;
			uint u_xlatu23;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu7.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati14 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati21 = (-u_xlati14) + (-int(u_xlatu7.x));
			    u_xlati21 = u_xlati21 + 1;
			    u_xlatu7.z = uint(uint(u_xlati21) & 1u);
			    u_xlat1.xy = vec2(u_xlatu7.xz);
			    u_xlat8.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati7 = u_xlati14 + int(u_xlatu7.x);
			    u_xlatu7.x = uint(uint(u_xlati7) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu7.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat7.xy = u_xlat8.xy * _FlareData2.zw;
			    u_xlat21 = u_xlat7.y * _FlareData0.y;
			    u_xlat21 = u_xlat7.x * _FlareData0.x + (-u_xlat21);
			    u_xlat1.y = dot(u_xlat7.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat21;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb7.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb7.x = u_xlatb7.y || u_xlatb7.x;
			    u_xlatb14.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb14.x = u_xlatb14.y || u_xlatb14.x;
			    u_xlatb7.x = u_xlatb14.x || u_xlatb7.x;
			    u_xlatb0 = u_xlatb7.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatb0 = _FlareData1.y!=0.0;
			        if(u_xlatb0){
			            u_xlat0.x = float(1.0) / _FlareData1.y;
			            u_xlatu7.x = uint(_FlareData1.y);
			            u_xlatb14.x = 0.0<_FlareData3.x;
			            u_xlatu1.z = uint(uint(0u));
			            u_xlatu1.w = uint(uint(0u));
			            u_xlat2.x = 0.0;
			            for(uint u_xlatu_loop_1 = uint(0u) ; u_xlatu_loop_1<u_xlatu7.x ; u_xlatu_loop_1++)
			            {
			                u_xlati16 = int(int(u_xlatu_loop_1) << (1 & int(0x1F)));
			                u_xlati16 = int(uint(uint(u_xlati16) ^ 2747636419u));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlat16.x = float(u_xlatu16);
			                u_xlat16.x = u_xlat16.x * 2.32830644e-10;
			                u_xlati23 = int(int_bitfieldInsert(1, int(u_xlatu_loop_1), 1 & int(0x1F), 31));
			                u_xlati23 = int(uint(uint(u_xlati23) ^ 2747636419u));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlat23 = float(u_xlatu23);
			                u_xlat16_4.x = sqrt(u_xlat16.x);
			                u_xlat16_11 = u_xlat23 * 1.46291812e-09;
			                u_xlat16_5 = sin(u_xlat16_11);
			                u_xlat16_6.x = cos(u_xlat16_11);
			                u_xlat16_6.y = u_xlat16_5;
			                u_xlat16_4.xy = u_xlat16_4.xx * u_xlat16_6.xy;
			                u_xlat16.xy = _FlareData1.xx * u_xlat16_4.xy + _FlareData2.xy;
			                u_xlat16.xy = u_xlat16.xy * vec2(0.5, 0.5) + vec2(0.5, 0.5);
			                u_xlatb3.xy = greaterThanEqual(u_xlat16.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			                u_xlatb3.x = u_xlatb3.y && u_xlatb3.x;
			                u_xlatb10.xy = greaterThanEqual(vec4(1.0, 1.0, 0.0, 0.0), u_xlat16.xyxx).xy;
			                u_xlatb10.x = u_xlatb10.y && u_xlatb10.x;
			                u_xlatb3.x = u_xlatb10.x && u_xlatb3.x;
			                if(u_xlatb3.x){
			                    u_xlat16.xy = u_xlat16.xy * _ScaledScreenParams.xy;
			                    u_xlatu1.xy = uvec2(u_xlat16.xy);
			                    u_xlat1.x = texelFetch(_CameraDepthTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).x;
			                    u_xlat1.x = _ZBufferParams.z * u_xlat1.x + _ZBufferParams.w;
			                    u_xlat1.x = float(1.0) / u_xlat1.x;
			                    u_xlatb1 = _FlareData1.z<u_xlat1.x;
			                    u_xlat8.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb1) ? u_xlat8.x : u_xlat2.x;
			                } else {
			                    u_xlat1.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb14.x) ? u_xlat1.x : u_xlat2.x;
			                }
			            }
			            u_xlat2.x = u_xlat2.x;
			            u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			            u_xlat2.y = 0.0;
			            u_xlat0.x = textureLod(_FlareOcclusionRemapTex, u_xlat2.xy, 0.0).x;
			            vs_TEXCOORD1 = u_xlat0.x;
			            vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
			        } else {
			            vs_TEXCOORD1 = 1.0;
			        }
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat0.xy = u_xlat0.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x + -1.0;
			    u_xlat1 = _FlareData3.y + -1.0;
			    u_xlat0.x = u_xlat0.x / u_xlat1;
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0 = u_xlat0.xxxx * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _ZBufferParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData1;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			UNITY_LOCATION(0) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _FlareOcclusionRemapTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			bool u_xlatb0;
			vec2 u_xlat1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec2 u_xlat2;
			uint u_xlatu3;
			bvec2 u_xlatb3;
			mediump vec2 u_xlat16_4;
			mediump float u_xlat16_5;
			mediump vec2 u_xlat16_6;
			vec2 u_xlat7;
			int u_xlati7;
			uvec3 u_xlatu7;
			bvec2 u_xlatb7;
			vec2 u_xlat8;
			bvec2 u_xlatb10;
			mediump float u_xlat16_11;
			int u_xlati14;
			bvec2 u_xlatb14;
			vec2 u_xlat16;
			int u_xlati16;
			uint u_xlatu16;
			bool u_xlatb16;
			float u_xlat21;
			int u_xlati21;
			uint u_xlatu21;
			float u_xlat23;
			int u_xlati23;
			uint u_xlatu23;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu7.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati14 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati21 = (-u_xlati14) + (-int(u_xlatu7.x));
			    u_xlati21 = u_xlati21 + 1;
			    u_xlatu7.z = uint(uint(u_xlati21) & 1u);
			    u_xlat1.xy = vec2(u_xlatu7.xz);
			    u_xlat8.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati7 = u_xlati14 + int(u_xlatu7.x);
			    u_xlatu7.x = uint(uint(u_xlati7) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu7.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat7.xy = u_xlat8.xy * _FlareData2.zw;
			    u_xlat21 = u_xlat7.y * _FlareData0.y;
			    u_xlat21 = u_xlat7.x * _FlareData0.x + (-u_xlat21);
			    u_xlat1.y = dot(u_xlat7.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat21;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb7.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb7.x = u_xlatb7.y || u_xlatb7.x;
			    u_xlatb14.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb14.x = u_xlatb14.y || u_xlatb14.x;
			    u_xlatb7.x = u_xlatb14.x || u_xlatb7.x;
			    u_xlatb0 = u_xlatb7.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatb0 = _FlareData1.y!=0.0;
			        if(u_xlatb0){
			            u_xlat0.x = float(1.0) / _FlareData1.y;
			            u_xlatu7.x = uint(_FlareData1.y);
			            u_xlatb14.x = 0.0<_FlareData3.x;
			            u_xlatu1.z = uint(uint(0u));
			            u_xlatu1.w = uint(uint(0u));
			            u_xlat2.x = 0.0;
			            for(uint u_xlatu_loop_1 = uint(0u) ; u_xlatu_loop_1<u_xlatu7.x ; u_xlatu_loop_1++)
			            {
			                u_xlati16 = int(int(u_xlatu_loop_1) << (1 & int(0x1F)));
			                u_xlati16 = int(uint(uint(u_xlati16) ^ 2747636419u));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlat16.x = float(u_xlatu16);
			                u_xlat16.x = u_xlat16.x * 2.32830644e-10;
			                u_xlati23 = int(int_bitfieldInsert(1, int(u_xlatu_loop_1), 1 & int(0x1F), 31));
			                u_xlati23 = int(uint(uint(u_xlati23) ^ 2747636419u));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlat23 = float(u_xlatu23);
			                u_xlat16_4.x = sqrt(u_xlat16.x);
			                u_xlat16_11 = u_xlat23 * 1.46291812e-09;
			                u_xlat16_5 = sin(u_xlat16_11);
			                u_xlat16_6.x = cos(u_xlat16_11);
			                u_xlat16_6.y = u_xlat16_5;
			                u_xlat16_4.xy = u_xlat16_4.xx * u_xlat16_6.xy;
			                u_xlat16.xy = _FlareData1.xx * u_xlat16_4.xy + _FlareData2.xy;
			                u_xlat16.xy = u_xlat16.xy * vec2(0.5, 0.5) + vec2(0.5, 0.5);
			                u_xlatb3.xy = greaterThanEqual(u_xlat16.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			                u_xlatb3.x = u_xlatb3.y && u_xlatb3.x;
			                u_xlatb10.xy = greaterThanEqual(vec4(1.0, 1.0, 0.0, 0.0), u_xlat16.xyxx).xy;
			                u_xlatb10.x = u_xlatb10.y && u_xlatb10.x;
			                u_xlatb3.x = u_xlatb10.x && u_xlatb3.x;
			                if(u_xlatb3.x){
			                    u_xlat16.xy = u_xlat16.xy * _ScaledScreenParams.xy;
			                    u_xlatu1.xy = uvec2(u_xlat16.xy);
			                    u_xlat1.x = texelFetch(_CameraDepthTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).x;
			                    u_xlat1.x = _ZBufferParams.z * u_xlat1.x + _ZBufferParams.w;
			                    u_xlat1.x = float(1.0) / u_xlat1.x;
			                    u_xlatb1 = _FlareData1.z<u_xlat1.x;
			                    u_xlat8.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb1) ? u_xlat8.x : u_xlat2.x;
			                } else {
			                    u_xlat1.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb14.x) ? u_xlat1.x : u_xlat2.x;
			                }
			            }
			            u_xlat2.x = u_xlat2.x;
			            u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			            u_xlat2.y = 0.0;
			            u_xlat0.x = textureLod(_FlareOcclusionRemapTex, u_xlat2.xy, 0.0).x;
			            vs_TEXCOORD1 = u_xlat0.x;
			            vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
			        } else {
			            vs_TEXCOORD1 = 1.0;
			        }
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
			    u_xlat0.xy = u_xlat0.xy + u_xlat0.xy;
			    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x + -1.0;
			    u_xlat1 = _FlareData3.y + -1.0;
			    u_xlat0.x = u_xlat0.x / u_xlat1;
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat1 = (-u_xlat0.x) + 1.0;
			    u_xlat1 = u_xlat1 * u_xlat0.x;
			    u_xlat0.x = u_xlat0.x + 9.99999997e-07;
			    u_xlat0.x = u_xlat1 / u_xlat0.x;
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0 = u_xlat0.xxxx * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _ZBufferParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData1;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			UNITY_LOCATION(0) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _FlareOcclusionRemapTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			bool u_xlatb0;
			vec2 u_xlat1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec2 u_xlat2;
			uint u_xlatu3;
			bvec2 u_xlatb3;
			mediump vec2 u_xlat16_4;
			mediump float u_xlat16_5;
			mediump vec2 u_xlat16_6;
			vec2 u_xlat7;
			int u_xlati7;
			uvec3 u_xlatu7;
			bvec2 u_xlatb7;
			vec2 u_xlat8;
			bvec2 u_xlatb10;
			mediump float u_xlat16_11;
			int u_xlati14;
			bvec2 u_xlatb14;
			vec2 u_xlat16;
			int u_xlati16;
			uint u_xlatu16;
			bool u_xlatb16;
			float u_xlat21;
			int u_xlati21;
			uint u_xlatu21;
			float u_xlat23;
			int u_xlati23;
			uint u_xlatu23;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu7.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati14 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati21 = (-u_xlati14) + (-int(u_xlatu7.x));
			    u_xlati21 = u_xlati21 + 1;
			    u_xlatu7.z = uint(uint(u_xlati21) & 1u);
			    u_xlat1.xy = vec2(u_xlatu7.xz);
			    u_xlat8.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati7 = u_xlati14 + int(u_xlatu7.x);
			    u_xlatu7.x = uint(uint(u_xlati7) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu7.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat7.xy = u_xlat8.xy * _FlareData2.zw;
			    u_xlat21 = u_xlat7.y * _FlareData0.y;
			    u_xlat21 = u_xlat7.x * _FlareData0.x + (-u_xlat21);
			    u_xlat1.y = dot(u_xlat7.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat21;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb7.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb7.x = u_xlatb7.y || u_xlatb7.x;
			    u_xlatb14.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb14.x = u_xlatb14.y || u_xlatb14.x;
			    u_xlatb7.x = u_xlatb14.x || u_xlatb7.x;
			    u_xlatb0 = u_xlatb7.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatb0 = _FlareData1.y!=0.0;
			        if(u_xlatb0){
			            u_xlat0.x = float(1.0) / _FlareData1.y;
			            u_xlatu7.x = uint(_FlareData1.y);
			            u_xlatb14.x = 0.0<_FlareData3.x;
			            u_xlatu1.z = uint(uint(0u));
			            u_xlatu1.w = uint(uint(0u));
			            u_xlat2.x = 0.0;
			            for(uint u_xlatu_loop_1 = uint(0u) ; u_xlatu_loop_1<u_xlatu7.x ; u_xlatu_loop_1++)
			            {
			                u_xlati16 = int(int(u_xlatu_loop_1) << (1 & int(0x1F)));
			                u_xlati16 = int(uint(uint(u_xlati16) ^ 2747636419u));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlat16.x = float(u_xlatu16);
			                u_xlat16.x = u_xlat16.x * 2.32830644e-10;
			                u_xlati23 = int(int_bitfieldInsert(1, int(u_xlatu_loop_1), 1 & int(0x1F), 31));
			                u_xlati23 = int(uint(uint(u_xlati23) ^ 2747636419u));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlat23 = float(u_xlatu23);
			                u_xlat16_4.x = sqrt(u_xlat16.x);
			                u_xlat16_11 = u_xlat23 * 1.46291812e-09;
			                u_xlat16_5 = sin(u_xlat16_11);
			                u_xlat16_6.x = cos(u_xlat16_11);
			                u_xlat16_6.y = u_xlat16_5;
			                u_xlat16_4.xy = u_xlat16_4.xx * u_xlat16_6.xy;
			                u_xlat16.xy = _FlareData1.xx * u_xlat16_4.xy + _FlareData2.xy;
			                u_xlat16.xy = u_xlat16.xy * vec2(0.5, 0.5) + vec2(0.5, 0.5);
			                u_xlatb3.xy = greaterThanEqual(u_xlat16.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			                u_xlatb3.x = u_xlatb3.y && u_xlatb3.x;
			                u_xlatb10.xy = greaterThanEqual(vec4(1.0, 1.0, 0.0, 0.0), u_xlat16.xyxx).xy;
			                u_xlatb10.x = u_xlatb10.y && u_xlatb10.x;
			                u_xlatb3.x = u_xlatb10.x && u_xlatb3.x;
			                if(u_xlatb3.x){
			                    u_xlat16.xy = u_xlat16.xy * _ScaledScreenParams.xy;
			                    u_xlatu1.xy = uvec2(u_xlat16.xy);
			                    u_xlat1.x = texelFetch(_CameraDepthTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).x;
			                    u_xlat1.x = _ZBufferParams.z * u_xlat1.x + _ZBufferParams.w;
			                    u_xlat1.x = float(1.0) / u_xlat1.x;
			                    u_xlatb1 = _FlareData1.z<u_xlat1.x;
			                    u_xlat8.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb1) ? u_xlat8.x : u_xlat2.x;
			                } else {
			                    u_xlat1.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb14.x) ? u_xlat1.x : u_xlat2.x;
			                }
			            }
			            u_xlat2.x = u_xlat2.x;
			            u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			            u_xlat2.y = 0.0;
			            u_xlat0.x = textureLod(_FlareOcclusionRemapTex, u_xlat2.xy, 0.0).x;
			            vs_TEXCOORD1 = u_xlat0.x;
			            vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
			        } else {
			            vs_TEXCOORD1 = 1.0;
			        }
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareData4;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			int u_xlati1;
			bool u_xlatb1;
			vec2 u_xlat2;
			bool u_xlatb4;
			vec2 u_xlat6;
			int u_xlati6;
			float u_xlat9;
			int u_xlati9;
			bool u_xlatb9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlat6.x = max(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = float(1.0) / u_xlat6.x;
			    u_xlat9 = min(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = u_xlat6.x * u_xlat9;
			    u_xlat9 = u_xlat6.x * u_xlat6.x;
			    u_xlat1 = u_xlat9 * 0.0208350997 + -0.0851330012;
			    u_xlat1 = u_xlat9 * u_xlat1 + 0.180141002;
			    u_xlat1 = u_xlat9 * u_xlat1 + -0.330299497;
			    u_xlat9 = u_xlat9 * u_xlat1 + 0.999866009;
			    u_xlat1 = u_xlat9 * u_xlat6.x;
			    u_xlat1 = u_xlat1 * -2.0 + 1.57079637;
			    u_xlatb4 = abs(u_xlat0.x)<abs(u_xlat0.y);
			    u_xlat1 = u_xlatb4 ? u_xlat1 : float(0.0);
			    u_xlat6.x = u_xlat6.x * u_xlat9 + u_xlat1;
			    u_xlatb9 = u_xlat0.x<(-u_xlat0.x);
			    u_xlat9 = u_xlatb9 ? -3.14159274 : float(0.0);
			    u_xlat6.x = u_xlat9 + u_xlat6.x;
			    u_xlat9 = min(u_xlat0.x, u_xlat0.y);
			    u_xlatb9 = u_xlat9<(-u_xlat9);
			    u_xlat1 = max(u_xlat0.x, u_xlat0.y);
			    u_xlatb1 = u_xlat1>=(-u_xlat1);
			    u_xlatb9 = u_xlatb9 && u_xlatb1;
			    u_xlat6.x = (u_xlatb9) ? (-u_xlat6.x) : u_xlat6.x;
			    u_xlat6.x = _FlareData4.z * 0.5 + u_xlat6.x;
			    u_xlat6.x = u_xlat6.x / _FlareData4.z;
			    u_xlat6.x = floor(u_xlat6.x);
			    u_xlat6.x = u_xlat6.x * _FlareData4.z;
			    u_xlat1 = sin(u_xlat6.x);
			    u_xlat2.x = cos(u_xlat6.x);
			    u_xlat6.xy = u_xlat0.xy * u_xlat2.xx;
			    u_xlat2.x = u_xlat1 * u_xlat0.y + u_xlat6.x;
			    u_xlat2.y = (-u_xlat1) * u_xlat0.x + u_xlat6.y;
			    u_xlat0.xyz = (-_FlareData4.wxy);
			    u_xlat0.x = max(u_xlat0.x, u_xlat2.y);
			    u_xlat0.x = min(u_xlat0.x, _FlareData4.w);
			    u_xlat0.w = (-u_xlat0.x);
			    u_xlat0.xz = u_xlat0.zw + u_xlat2.xy;
			    u_xlati9 = int((0.0<u_xlat0.x) ? 0xFFFFFFFFu : uint(0));
			    u_xlati1 = int((u_xlat0.x<0.0) ? 0xFFFFFFFFu : uint(0));
			    u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlati6 = (-u_xlati9) + u_xlati1;
			    u_xlat6.x = float(u_xlati6);
			    u_xlat0.x = u_xlat0.x * u_xlat6.x + u_xlat0.y;
			    u_xlat0.x = u_xlat0.x * _FlareData3.y;
			    u_xlat0.x = (-u_xlat0.x);
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0.x = min(u_xlat0.x, 1.0);
			    u_xlat0 = u_xlat0.xxxx * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _ZBufferParams;
			uniform 	vec4 _FlareData0;
			uniform 	vec4 _FlareData1;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			UNITY_LOCATION(0) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _FlareOcclusionRemapTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			vec2 u_xlat0;
			bool u_xlatb0;
			vec2 u_xlat1;
			uvec4 u_xlatu1;
			bool u_xlatb1;
			vec2 u_xlat2;
			uint u_xlatu3;
			bvec2 u_xlatb3;
			mediump vec2 u_xlat16_4;
			mediump float u_xlat16_5;
			mediump vec2 u_xlat16_6;
			vec2 u_xlat7;
			int u_xlati7;
			uvec3 u_xlatu7;
			bvec2 u_xlatb7;
			vec2 u_xlat8;
			bvec2 u_xlatb10;
			mediump float u_xlat16_11;
			int u_xlati14;
			bvec2 u_xlatb14;
			vec2 u_xlat16;
			int u_xlati16;
			uint u_xlatu16;
			bool u_xlatb16;
			float u_xlat21;
			int u_xlati21;
			uint u_xlatu21;
			float u_xlat23;
			int u_xlati23;
			uint u_xlatu23;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    u_xlat0.x = _ScaledScreenParams.y / _ScaledScreenParams.x;
			    u_xlatu7.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati14 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati21 = (-u_xlati14) + (-int(u_xlatu7.x));
			    u_xlati21 = u_xlati21 + 1;
			    u_xlatu7.z = uint(uint(u_xlati21) & 1u);
			    u_xlat1.xy = vec2(u_xlatu7.xz);
			    u_xlat8.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati7 = u_xlati14 + int(u_xlatu7.x);
			    u_xlatu7.x = uint(uint(u_xlati7) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu7.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlat7.xy = u_xlat8.xy * _FlareData2.zw;
			    u_xlat21 = u_xlat7.y * _FlareData0.y;
			    u_xlat21 = u_xlat7.x * _FlareData0.x + (-u_xlat21);
			    u_xlat1.y = dot(u_xlat7.yx, _FlareData0.xy);
			    u_xlat1.x = u_xlat0.x * u_xlat21;
			    u_xlat0.xy = u_xlat1.xy + _FlareData2.xy;
			    gl_Position.xy = u_xlat0.xy + _FlareData0.zw;
			    u_xlatb0 = _FlareData3.x<0.0;
			    u_xlatb7.xy = lessThan(_FlareData2.xyxx, vec4(-1.0, -1.0, 0.0, 0.0)).xy;
			    u_xlatb7.x = u_xlatb7.y || u_xlatb7.x;
			    u_xlatb14.xy = lessThan(vec4(1.0, 1.0, 1.0, 1.0), _FlareData2.xyxy).xy;
			    u_xlatb14.x = u_xlatb14.y || u_xlatb14.x;
			    u_xlatb7.x = u_xlatb14.x || u_xlatb7.x;
			    u_xlatb0 = u_xlatb7.x && u_xlatb0;
			    if(u_xlatb0){
			        vs_TEXCOORD1 = 0.0;
			    } else {
			        u_xlatb0 = _FlareData1.y!=0.0;
			        if(u_xlatb0){
			            u_xlat0.x = float(1.0) / _FlareData1.y;
			            u_xlatu7.x = uint(_FlareData1.y);
			            u_xlatb14.x = 0.0<_FlareData3.x;
			            u_xlatu1.z = uint(uint(0u));
			            u_xlatu1.w = uint(uint(0u));
			            u_xlat2.x = 0.0;
			            for(uint u_xlatu_loop_1 = uint(0u) ; u_xlatu_loop_1<u_xlatu7.x ; u_xlatu_loop_1++)
			            {
			                u_xlati16 = int(int(u_xlatu_loop_1) << (1 & int(0x1F)));
			                u_xlati16 = int(uint(uint(u_xlati16) ^ 2747636419u));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			                u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			                u_xlatu16 = uint(u_xlati16) * 2654435769u;
			                u_xlat16.x = float(u_xlatu16);
			                u_xlat16.x = u_xlat16.x * 2.32830644e-10;
			                u_xlati23 = int(int_bitfieldInsert(1, int(u_xlatu_loop_1), 1 & int(0x1F), 31));
			                u_xlati23 = int(uint(uint(u_xlati23) ^ 2747636419u));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			                u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			                u_xlatu23 = uint(u_xlati23) * 2654435769u;
			                u_xlat23 = float(u_xlatu23);
			                u_xlat16_4.x = sqrt(u_xlat16.x);
			                u_xlat16_11 = u_xlat23 * 1.46291812e-09;
			                u_xlat16_5 = sin(u_xlat16_11);
			                u_xlat16_6.x = cos(u_xlat16_11);
			                u_xlat16_6.y = u_xlat16_5;
			                u_xlat16_4.xy = u_xlat16_4.xx * u_xlat16_6.xy;
			                u_xlat16.xy = _FlareData1.xx * u_xlat16_4.xy + _FlareData2.xy;
			                u_xlat16.xy = u_xlat16.xy * vec2(0.5, 0.5) + vec2(0.5, 0.5);
			                u_xlatb3.xy = greaterThanEqual(u_xlat16.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			                u_xlatb3.x = u_xlatb3.y && u_xlatb3.x;
			                u_xlatb10.xy = greaterThanEqual(vec4(1.0, 1.0, 0.0, 0.0), u_xlat16.xyxx).xy;
			                u_xlatb10.x = u_xlatb10.y && u_xlatb10.x;
			                u_xlatb3.x = u_xlatb10.x && u_xlatb3.x;
			                if(u_xlatb3.x){
			                    u_xlat16.xy = u_xlat16.xy * _ScaledScreenParams.xy;
			                    u_xlatu1.xy = uvec2(u_xlat16.xy);
			                    u_xlat1.x = texelFetch(_CameraDepthTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).x;
			                    u_xlat1.x = _ZBufferParams.z * u_xlat1.x + _ZBufferParams.w;
			                    u_xlat1.x = float(1.0) / u_xlat1.x;
			                    u_xlatb1 = _FlareData1.z<u_xlat1.x;
			                    u_xlat8.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb1) ? u_xlat8.x : u_xlat2.x;
			                } else {
			                    u_xlat1.x = u_xlat0.x + u_xlat2.x;
			                    u_xlat2.x = (u_xlatb14.x) ? u_xlat1.x : u_xlat2.x;
			                }
			            }
			            u_xlat2.x = u_xlat2.x;
			            u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			            u_xlat2.y = 0.0;
			            u_xlat0.x = textureLod(_FlareOcclusionRemapTex, u_xlat2.xy, 0.0).x;
			            vs_TEXCOORD1 = u_xlat0.x;
			            vs_TEXCOORD1 = clamp(vs_TEXCOORD1, 0.0, 1.0);
			        } else {
			            vs_TEXCOORD1 = 1.0;
			        }
			    }
			    gl_Position.zw = vec2(1.0, 1.0);
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
			uniform 	vec4 _FlareColorValue;
			uniform 	vec4 _FlareData3;
			uniform 	vec4 _FlareData4;
			in highp vec2 vs_TEXCOORD0;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			float u_xlat1;
			int u_xlati1;
			bool u_xlatb1;
			vec2 u_xlat2;
			float u_xlat3;
			bool u_xlatb4;
			vec2 u_xlat6;
			int u_xlati6;
			float u_xlat9;
			int u_xlati9;
			bool u_xlatb9;
			void main()
			{
			    u_xlat0.xy = vs_TEXCOORD0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlat6.x = max(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = float(1.0) / u_xlat6.x;
			    u_xlat9 = min(abs(u_xlat0.x), abs(u_xlat0.y));
			    u_xlat6.x = u_xlat6.x * u_xlat9;
			    u_xlat9 = u_xlat6.x * u_xlat6.x;
			    u_xlat1 = u_xlat9 * 0.0208350997 + -0.0851330012;
			    u_xlat1 = u_xlat9 * u_xlat1 + 0.180141002;
			    u_xlat1 = u_xlat9 * u_xlat1 + -0.330299497;
			    u_xlat9 = u_xlat9 * u_xlat1 + 0.999866009;
			    u_xlat1 = u_xlat9 * u_xlat6.x;
			    u_xlat1 = u_xlat1 * -2.0 + 1.57079637;
			    u_xlatb4 = abs(u_xlat0.x)<abs(u_xlat0.y);
			    u_xlat1 = u_xlatb4 ? u_xlat1 : float(0.0);
			    u_xlat6.x = u_xlat6.x * u_xlat9 + u_xlat1;
			    u_xlatb9 = u_xlat0.x<(-u_xlat0.x);
			    u_xlat9 = u_xlatb9 ? -3.14159274 : float(0.0);
			    u_xlat6.x = u_xlat9 + u_xlat6.x;
			    u_xlat9 = min(u_xlat0.x, u_xlat0.y);
			    u_xlatb9 = u_xlat9<(-u_xlat9);
			    u_xlat1 = max(u_xlat0.x, u_xlat0.y);
			    u_xlatb1 = u_xlat1>=(-u_xlat1);
			    u_xlatb9 = u_xlatb9 && u_xlatb1;
			    u_xlat6.x = (u_xlatb9) ? (-u_xlat6.x) : u_xlat6.x;
			    u_xlat6.x = _FlareData4.z * 0.5 + u_xlat6.x;
			    u_xlat6.x = u_xlat6.x / _FlareData4.z;
			    u_xlat6.x = floor(u_xlat6.x);
			    u_xlat6.x = u_xlat6.x * _FlareData4.z;
			    u_xlat1 = sin(u_xlat6.x);
			    u_xlat2.x = cos(u_xlat6.x);
			    u_xlat6.xy = u_xlat0.xy * u_xlat2.xx;
			    u_xlat2.x = u_xlat1 * u_xlat0.y + u_xlat6.x;
			    u_xlat2.y = (-u_xlat1) * u_xlat0.x + u_xlat6.y;
			    u_xlat0.xyz = (-_FlareData4.wxy);
			    u_xlat0.x = max(u_xlat0.x, u_xlat2.y);
			    u_xlat0.x = min(u_xlat0.x, _FlareData4.w);
			    u_xlat0.w = (-u_xlat0.x);
			    u_xlat0.xz = u_xlat0.zw + u_xlat2.xy;
			    u_xlati9 = int((0.0<u_xlat0.x) ? 0xFFFFFFFFu : uint(0));
			    u_xlati1 = int((u_xlat0.x<0.0) ? 0xFFFFFFFFu : uint(0));
			    u_xlat0.x = dot(u_xlat0.xz, u_xlat0.xz);
			    u_xlat0.x = sqrt(u_xlat0.x);
			    u_xlati6 = (-u_xlati9) + u_xlati1;
			    u_xlat6.x = float(u_xlati6);
			    u_xlat0.x = u_xlat0.x * u_xlat6.x + u_xlat0.y;
			    u_xlat0.x = u_xlat0.x * _FlareData3.y;
			    u_xlat0.x = (-u_xlat0.x);
			    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
			    u_xlat3 = (-u_xlat0.x) + 1.0;
			    u_xlat3 = u_xlat3 * u_xlat0.x;
			    u_xlat0.x = u_xlat0.x + 9.99999997e-07;
			    u_xlat0.x = u_xlat3 / u_xlat0.x;
			    u_xlat0.x = log2(u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _FlareData3.z;
			    u_xlat0.x = exp2(u_xlat0.x);
			    u_xlat0.x = min(u_xlat0.x, 1.0);
			    u_xlat0 = u_xlat0.xxxx * _FlareColorValue;
			    SV_Target0 = u_xlat0 * vec4(vs_TEXCOORD1);
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
			uniform 	vec4 _ScaledScreenParams;
			uniform 	vec4 _ZBufferParams;
			uniform 	vec4 _FlareData1;
			uniform 	vec4 _FlareData2;
			uniform 	vec4 _FlareData3;
			UNITY_LOCATION(0) uniform highp sampler2D _CameraDepthTexture;
			UNITY_LOCATION(1) uniform mediump sampler2D _FlareOcclusionRemapTex;
			out highp vec2 vs_TEXCOORD0;
			out highp float vs_TEXCOORD1;
			float u_xlat0;
			int u_xlati0;
			uvec3 u_xlatu0;
			bool u_xlatb0;
			vec2 u_xlat1;
			uvec4 u_xlatu1;
			bvec2 u_xlatb1;
			vec2 u_xlat2;
			uint u_xlatu3;
			bvec2 u_xlatb3;
			mediump vec2 u_xlat16_4;
			mediump float u_xlat16_5;
			mediump vec2 u_xlat16_6;
			int u_xlati7;
			uint u_xlatu7;
			bool u_xlatb7;
			float u_xlat8;
			bvec2 u_xlatb10;
			mediump float u_xlat16_11;
			int u_xlati14;
			bvec2 u_xlatb14;
			vec2 u_xlat16;
			int u_xlati16;
			uint u_xlatu16;
			bool u_xlatb16;
			uint u_xlatu21;
			bool u_xlatb21;
			float u_xlat23;
			int u_xlati23;
			uint u_xlatu23;
			int int_bitfieldInsert(int base, int insert, int offset, int bits) {
			    uint mask = uint(~(int(~0) << uint(bits)) << uint(offset));
			    return int((uint(base) & ~mask) | ((uint(insert) << uint(offset)) & mask));
			}

			void main()
			{
			    u_xlatu0.x = uint(uint(gl_VertexID) >> (1u & uint(0x1F)));
			    u_xlati7 = int(uint(uint(gl_VertexID) & 1u));
			    u_xlati14 = (-u_xlati7) + (-int(u_xlatu0.x));
			    u_xlati14 = u_xlati14 + 1;
			    u_xlatu0.z = uint(uint(u_xlati14) & 1u);
			    u_xlat1.xy = vec2(u_xlatu0.xz);
			    gl_Position.xy = u_xlat1.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
			    u_xlati0 = u_xlati7 + int(u_xlatu0.x);
			    u_xlatu0.x = uint(uint(u_xlati0) & 1u);
			    vs_TEXCOORD0.y = float(u_xlatu0.x);
			    vs_TEXCOORD0.x = (-u_xlat1.x) + 1.0;
			    u_xlatb0 = _FlareData1.y!=0.0;
			    if(u_xlatb0){
			        u_xlat0 = float(1.0) / _FlareData1.y;
			        u_xlatu7 = uint(_FlareData1.y);
			        u_xlatb14.x = 0.0<_FlareData3.x;
			        u_xlatu1.z = uint(uint(0u));
			        u_xlatu1.w = uint(uint(0u));
			        u_xlat2.x = 0.0;
			        for(uint u_xlatu_loop_1 = uint(0u) ; u_xlatu_loop_1<u_xlatu7 ; u_xlatu_loop_1++)
			        {
			            u_xlati16 = int(int(u_xlatu_loop_1) << (1 & int(0x1F)));
			            u_xlati16 = int(uint(uint(u_xlati16) ^ 2747636419u));
			            u_xlatu16 = uint(u_xlati16) * 2654435769u;
			            u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			            u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			            u_xlatu16 = uint(u_xlati16) * 2654435769u;
			            u_xlatu23 = uint(u_xlatu16 >> (16u & uint(0x1F)));
			            u_xlati16 = int(uint(u_xlatu23 ^ u_xlatu16));
			            u_xlatu16 = uint(u_xlati16) * 2654435769u;
			            u_xlat16.x = float(u_xlatu16);
			            u_xlat16.x = u_xlat16.x * 2.32830644e-10;
			            u_xlati23 = int(int_bitfieldInsert(1, int(u_xlatu_loop_1), 1 & int(0x1F), 31));
			            u_xlati23 = int(uint(uint(u_xlati23) ^ 2747636419u));
			            u_xlatu23 = uint(u_xlati23) * 2654435769u;
			            u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			            u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			            u_xlatu23 = uint(u_xlati23) * 2654435769u;
			            u_xlatu3 = uint(u_xlatu23 >> (16u & uint(0x1F)));
			            u_xlati23 = int(uint(u_xlatu23 ^ u_xlatu3));
			            u_xlatu23 = uint(u_xlati23) * 2654435769u;
			            u_xlat23 = float(u_xlatu23);
			            u_xlat16_4.x = sqrt(u_xlat16.x);
			            u_xlat16_11 = u_xlat23 * 1.46291812e-09;
			            u_xlat16_5 = sin(u_xlat16_11);
			            u_xlat16_6.x = cos(u_xlat16_11);
			            u_xlat16_6.y = u_xlat16_5;
			            u_xlat16_4.xy = u_xlat16_4.xx * u_xlat16_6.xy;
			            u_xlat16.xy = _FlareData1.xx * u_xlat16_4.xy + _FlareData2.xy;
			            u_xlat16.xy = u_xlat16.xy * vec2(0.5, 0.5) + vec2(0.5, 0.5);
			            u_xlatb3.xy = greaterThanEqual(u_xlat16.xyxx, vec4(0.0, 0.0, 0.0, 0.0)).xy;
			            u_xlatb3.x = u_xlatb3.y && u_xlatb3.x;
			            u_xlatb10.xy = greaterThanEqual(vec4(1.0, 1.0, 0.0, 0.0), u_xlat16.xyxx).xy;
			            u_xlatb10.x = u_xlatb10.y && u_xlatb10.x;
			            u_xlatb3.x = u_xlatb10.x && u_xlatb3.x;
			            if(u_xlatb3.x){
			                u_xlat16.xy = u_xlat16.xy * _ScaledScreenParams.xy;
			                u_xlatu1.xy = uvec2(u_xlat16.xy);
			                u_xlat1.x = texelFetch(_CameraDepthTexture, ivec2(u_xlatu1.xy), int(u_xlatu1.w)).x;
			                u_xlat1.x = _ZBufferParams.z * u_xlat1.x + _ZBufferParams.w;
			                u_xlat1.x = float(1.0) / u_xlat1.x;
			                u_xlatb1.x = _FlareData1.z<u_xlat1.x;
			                u_xlat8 = u_xlat0 + u_xlat2.x;
			                u_xlat2.x = (u_xlatb1.x) ? u_xlat8 : u_xlat2.x;
			            } else {
			                u_xlat1.x = u_xlat0 + u_xlat2.x;
			                u_xlat2.x = (u_xlatb14.x) ? u_xlat1.x : u_xlat2.x;
			            }
			        }
			        u_xlat2.x = u_xlat2.x;
			        u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			        u_xlat2.y = 0.0;
			        u_xlat0 = textureLod(_FlareOcclusionRemapTex, u_xlat2.xy, 0.0).x;
			        u_xlat0 = u_xlat0;
			        u_xlat0 = clamp(u_xlat0, 0.0, 1.0);
			    } else {
			        u_xlat0 = 1.0;
			    }
			    u_xlatb7 = _FlareData3.x<0.0;
			    u_xlatb14.xy = lessThan(_FlareData2.xyxy, vec4(-1.0, -1.0, -1.0, -1.0)).xy;
			    u_xlatb14.x = u_xlatb14.y || u_xlatb14.x;
			    u_xlatb1.xy = greaterThanEqual(_FlareData2.xyxx, vec4(1.0, 1.0, 0.0, 0.0)).xy;
			    u_xlatb21 = u_xlatb1.y || u_xlatb1.x;
			    u_xlatb14.x = u_xlatb21 || u_xlatb14.x;
			    u_xlatb7 = u_xlatb14.x && u_xlatb7;
			    vs_TEXCOORD1 = (u_xlatb7) ? 0.0 : u_xlat0;
			    gl_Position.zw = vec2(1.0, 1.0);
			    return;
			}

			#endif
			#ifdef FRAGMENT
			#version 300 es

			precision highp float;
			precision highp int;
			in highp float vs_TEXCOORD1;
			layout(location = 0) out highp vec4 SV_Target0;
			void main()
			{
			    SV_Target0.xyz = vec3(vs_TEXCOORD1);
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}