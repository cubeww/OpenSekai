Shader "Hidden/Universal Render Pipeline/Edge Adaptive Spatial Upsampling" {
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
			uniform 	vec4 _ScreenParams;
			uniform 	vec4 _FsrEasuConstants0;
			uniform 	vec4 _FsrEasuConstants1;
			uniform 	vec4 _FsrEasuConstants2;
			uniform 	vec4 _FsrEasuConstants3;
			UNITY_LOCATION(0) uniform mediump sampler2D _BlitTexture;
			layout(location = 0) in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec4 u_xlat16_0;
			uvec4 u_xlatu0;
			vec2 u_xlat1;
			mediump vec4 u_xlat16_1;
			mediump vec4 u_xlat16_2;
			mediump vec4 u_xlat16_3;
			mediump vec4 u_xlat16_4;
			vec3 u_xlat5;
			vec4 u_xlat6;
			bvec3 u_xlatb6;
			vec4 u_xlat7;
			vec4 u_xlat8;
			vec4 u_xlat9;
			mediump vec4 u_xlat16_10;
			mediump vec4 u_xlat16_11;
			vec4 u_xlat12;
			vec4 u_xlat13;
			mediump vec4 u_xlat16_14;
			mediump vec4 u_xlat16_15;
			mediump vec4 u_xlat16_16;
			vec2 u_xlat17;
			mediump vec4 u_xlat16_18;
			mediump uint u_xlatu16_18;
			mediump vec4 u_xlat16_19;
			mediump vec3 u_xlat16_22;
			vec2 u_xlat41;
			mediump float u_xlat16_42;
			mediump vec2 u_xlat16_43;
			vec2 u_xlat45;
			mediump vec2 u_xlat16_50;
			mediump vec2 u_xlat16_51;
			mediump vec2 u_xlat16_55;
			float u_xlat57;
			uint u_xlatu57;
			bool u_xlatb77;
			void main()
			{
			    u_xlat0 = vs_TEXCOORD0.yxxy * _ScreenParams.yxxy;
			    u_xlatu0 = uvec4(u_xlat0);
			    u_xlat0 = vec4(u_xlatu0);
			    u_xlat0 = u_xlat0 * _FsrEasuConstants0.yxxy + _FsrEasuConstants0.wzzw;
			    u_xlat1.xy = floor(u_xlat0.zw);
			    u_xlat0 = u_xlat0 + (-u_xlat1.yxxy);
			    u_xlat1.xy = u_xlat1.xy * _FsrEasuConstants1.xy + _FsrEasuConstants1.zw;
			    u_xlat16_2.y = u_xlat0.z;
			    u_xlat16_2.xz = (-u_xlat0.zw) + vec2(1.0, 2.0);
			    u_xlat16_3.xy = u_xlat0.ww * u_xlat16_2.xy;
			    u_xlat16_4 = (-u_xlat0) + vec4(1.0, 0.0, 1.0, -1.0);
			    u_xlat16_2.xy = u_xlat16_2.xy * u_xlat16_4.xx;
			    u_xlat0.xy = textureGather(_BlitTexture, u_xlat1.xy).xy;
			    u_xlat41.xy = textureGather(_BlitTexture, u_xlat1.xy, 1).xy;
			    u_xlat16_43.xy = u_xlat0.xy * vec2(0.5, 0.5) + u_xlat41.xy;
			    u_xlat5.xy = textureGather(_BlitTexture, u_xlat1.xy, 2).xy;
			    u_xlat16_43.xy = u_xlat5.xy * vec2(0.5, 0.5) + u_xlat16_43.xy;
			    u_xlat6 = u_xlat1.xyxy + _FsrEasuConstants2;
			    u_xlat1.xy = u_xlat1.xy + _FsrEasuConstants3.xy;
			    u_xlat7 = textureGather(_BlitTexture, u_xlat6.xy, 2);
			    u_xlat8 = textureGather(_BlitTexture, u_xlat6.xy);
			    u_xlat9 = textureGather(_BlitTexture, u_xlat6.xy, 1);
			    u_xlat16_10 = u_xlat8 * vec4(0.5, 0.5, 0.5, 0.5) + u_xlat9;
			    u_xlat16_10 = u_xlat7 * vec4(0.5, 0.5, 0.5, 0.5) + u_xlat16_10;
			    u_xlat16_11.xzw = u_xlat16_10.zwz;
			    u_xlat16_10.xzw = u_xlat16_10.yxy;
			    u_xlat12 = textureGather(_BlitTexture, u_xlat6.zw, 2);
			    u_xlat13 = textureGather(_BlitTexture, u_xlat6.zw);
			    u_xlat6 = textureGather(_BlitTexture, u_xlat6.zw, 1);
			    u_xlat16_14 = u_xlat13 * vec4(0.5, 0.5, 0.5, 0.5) + u_xlat6;
			    u_xlat16_14 = u_xlat12 * vec4(0.5, 0.5, 0.5, 0.5) + u_xlat16_14;
			    u_xlat16_11.y = u_xlat16_14.w;
			    u_xlat16_15.xy = (-u_xlat16_43.xy) + u_xlat16_11.wy;
			    u_xlat16_10.y = u_xlat16_14.x;
			    u_xlat16_55.xy = u_xlat16_10.wy + (-u_xlat16_11.xy);
			    u_xlat16_15.xy = max(abs(u_xlat16_15.xy), abs(u_xlat16_55.xy));
			    u_xlat16_15.xy = vec2(1.0) / vec2(u_xlat16_15.xy);
			    u_xlat16_43.xy = (-u_xlat16_43.xy) + u_xlat16_10.wy;
			    u_xlat16_15.xy = u_xlat16_15.xy * abs(u_xlat16_43.xy);
			    u_xlat16_15.xy = clamp(u_xlat16_15.xy, 0.0, 1.0);
			    u_xlat16_15.xy = u_xlat16_15.xy * u_xlat16_15.xy;
			    u_xlat16_15.xy = u_xlat16_2.xy * u_xlat16_15.xy;
			    u_xlat16_16 = (-u_xlat16_11) + u_xlat16_14.wzwz;
			    u_xlat16_14 = (-u_xlat16_10) + u_xlat16_14.xyxy;
			    u_xlat16_51.xy = (-u_xlat16_11.zw) + u_xlat16_11.wy;
			    u_xlat16_51.xy = max(abs(u_xlat16_51.xy), abs(u_xlat16_16.xy));
			    u_xlat16_51.xy = vec2(1.0) / vec2(u_xlat16_51.xy);
			    u_xlat16_51.xy = u_xlat16_51.xy * abs(u_xlat16_16.zw);
			    u_xlat16_51.xy = clamp(u_xlat16_51.xy, 0.0, 1.0);
			    u_xlat16_51.xy = u_xlat16_51.xy * u_xlat16_51.xy;
			    u_xlat16_51.xy = u_xlat16_51.xy * u_xlat16_2.xy + u_xlat16_15.xy;
			    u_xlat16_50.xy = (-u_xlat16_10.zw) + u_xlat16_10.wy;
			    u_xlat16_50.xy = max(abs(u_xlat16_50.xy), abs(u_xlat16_14.xy));
			    u_xlat16_50.xy = vec2(1.0) / vec2(u_xlat16_50.xy);
			    u_xlat16_50.xy = u_xlat16_50.xy * abs(u_xlat16_14.zw);
			    u_xlat16_50.xy = clamp(u_xlat16_50.xy, 0.0, 1.0);
			    u_xlat16_14.xy = u_xlat16_3.xy * u_xlat16_14.zw;
			    u_xlat16_14.xy = u_xlat16_16.zw * u_xlat16_2.xy + u_xlat16_14.xy;
			    u_xlat16_14.y = u_xlat16_14.y + u_xlat16_14.x;
			    u_xlat16_50.xy = u_xlat16_50.xy * u_xlat16_50.xy;
			    u_xlat16_50.xy = u_xlat16_50.xy * u_xlat16_3.xy + u_xlat16_51.xy;
			    u_xlat45.xy = textureGather(_BlitTexture, u_xlat1.xy).zw;
			    u_xlat17.xy = textureGather(_BlitTexture, u_xlat1.xy, 1).zw;
			    u_xlat1.xy = textureGather(_BlitTexture, u_xlat1.xy, 2).zw;
			    u_xlat16_51.xy = u_xlat45.yx * vec2(0.5, 0.5) + u_xlat17.yx;
			    u_xlat16_51.xy = u_xlat1.yx * vec2(0.5, 0.5) + u_xlat16_51.xy;
			    u_xlat16_10.xy = (-u_xlat16_10.xy) + u_xlat16_51.xy;
			    u_xlat16_11.xy = (-u_xlat16_11.xy) + u_xlat16_51.xy;
			    u_xlat16_10.xy = max(abs(u_xlat16_55.xy), abs(u_xlat16_10.xy));
			    u_xlat16_10.xy = vec2(1.0) / vec2(u_xlat16_10.xy);
			    u_xlat16_10.xy = u_xlat16_10.xy * abs(u_xlat16_11.xy);
			    u_xlat16_10.xy = clamp(u_xlat16_10.xy, 0.0, 1.0);
			    u_xlat16_11.xy = u_xlat16_3.xy * u_xlat16_11.xy;
			    u_xlat16_2.xy = u_xlat16_43.xy * u_xlat16_2.xy + u_xlat16_11.xy;
			    u_xlat16_14.z = u_xlat16_2.y + u_xlat16_2.x;
			    u_xlat16_2.xy = u_xlat16_10.xy * u_xlat16_10.xy;
			    u_xlat16_2.xy = u_xlat16_2.xy * u_xlat16_3.xy + u_xlat16_50.xy;
			    u_xlat16_2.x = u_xlat16_2.y + u_xlat16_2.x;
			    u_xlat16_2.x = u_xlat16_2.x * 0.5;
			    u_xlat16_2.x = u_xlat16_2.x * u_xlat16_2.x;
			    u_xlat16_22.xz = u_xlat16_14.yz * u_xlat16_14.yz;
			    u_xlat16_22.x = u_xlat16_22.z + u_xlat16_22.x;
			    u_xlatu57 = packHalf2x16(vec2(u_xlat16_22.x, 0.0));
			    u_xlatb77 = u_xlat16_22.x<3.05175781e-05;
			    u_xlatu16_18 = u_xlatu57 >> (1u & uint(0x1F));
			    u_xlatu16_18 =  uint((-int(u_xlatu16_18)) + 22947);
			    u_xlat57 = unpackHalf2x16(u_xlatu16_18).x;
			    u_xlat16_22.x = (u_xlatb77) ? 1.0 : u_xlat57;
			    u_xlat16_14.x = (u_xlatb77) ? 1.0 : u_xlat16_14.y;
			    u_xlat16_22.xz = u_xlat16_22.xx * u_xlat16_14.zx;
			    u_xlat16_3.x = max(abs(u_xlat16_22.x), abs(u_xlat16_22.z));
			    u_xlatu57 = packHalf2x16(vec2(u_xlat16_3.x, 0.0));
			    u_xlatu16_18 =  uint((-int(u_xlatu57)) + 30596);
			    u_xlat57 = unpackHalf2x16(u_xlatu16_18).x;
			    u_xlat16_3.x = dot(u_xlat16_22.xz, u_xlat16_22.xz);
			    u_xlat16_3.x = u_xlat16_3.x * u_xlat57 + -1.0;
			    u_xlat16_3.x = u_xlat16_3.x * u_xlat16_2.x + 1.0;
			    u_xlat16_3.yz = u_xlat16_2.xx * vec2(-0.5, -0.289999992) + vec2(1.0, 0.5);
			    u_xlat16_10 = u_xlat16_22.zzxz * u_xlat16_4.yzww;
			    u_xlat16_10.xy = u_xlat16_10.zz + u_xlat16_10.xy;
			    u_xlat16_10.zw = u_xlat16_4.yz * (-u_xlat16_22.xx) + u_xlat16_10.ww;
			    u_xlat16_10 = u_xlat16_3.xxyy * u_xlat16_10;
			    u_xlat16_50.xy = u_xlat16_10.zw * u_xlat16_10.zw;
			    u_xlat16_10.xy = u_xlat16_10.xy * u_xlat16_10.xy + u_xlat16_50.xy;
			    u_xlatu57 = packHalf2x16(vec2(u_xlat16_3.z, 0.0));
			    u_xlatu16_18 =  uint((-int(u_xlatu57)) + 30596);
			    u_xlat57 = unpackHalf2x16(u_xlatu16_18).x;
			    u_xlat16_10.xy = min(u_xlat16_10.xy, vec2(u_xlat57));
			    u_xlat16_10.zw = u_xlat16_3.zz * u_xlat16_10.xy + vec2(-1.0, -1.0);
			    u_xlat16_10.xy = u_xlat16_10.xy * vec2(0.400000006, 0.400000006) + vec2(-1.0, -1.0);
			    u_xlat16_10 = u_xlat16_10 * u_xlat16_10;
			    u_xlat16_10.xy = u_xlat16_10.xy * vec2(1.5625, 1.5625) + vec2(-0.5625, -0.5625);
			    u_xlat16_11 = (-u_xlat0.zzzz) + vec4(0.0, -1.0, 2.0, 1.0);
			    u_xlat16_14.xy = (-u_xlat0.ww) * u_xlat16_22.xz;
			    u_xlat16_4.xw = u_xlat16_22.xz * u_xlat16_4.xx;
			    u_xlat16_15 = u_xlat16_11.yxwz * u_xlat16_22.zzzz + u_xlat16_4.xxxx;
			    u_xlat16_16 = u_xlat16_11.yxwz * (-u_xlat16_22.xxxx) + u_xlat16_4.wwww;
			    u_xlat16_16 = u_xlat16_3.yyyy * u_xlat16_16;
			    u_xlat16_16 = u_xlat16_16 * u_xlat16_16;
			    u_xlat16_15 = u_xlat16_3.xxxx * u_xlat16_15;
			    u_xlat16_15 = u_xlat16_15 * u_xlat16_15 + u_xlat16_16;
			    u_xlat16_15 = min(vec4(u_xlat57), u_xlat16_15);
			    u_xlat16_16 = u_xlat16_3.zzzz * u_xlat16_15 + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat16_15 = u_xlat16_15 * vec4(0.400000006, 0.400000006, 0.400000006, 0.400000006) + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat16_15 = u_xlat16_15 * u_xlat16_15;
			    u_xlat16_15 = u_xlat16_15 * vec4(1.5625, 1.5625, 1.5625, 1.5625) + vec4(-0.5625, -0.5625, -0.5625, -0.5625);
			    u_xlat16_16 = u_xlat16_16 * u_xlat16_16;
			    u_xlat16_18 = u_xlat16_15 * u_xlat16_16;
			    u_xlat16_4.xw = u_xlat16_10.xy * u_xlat16_10.zw + u_xlat16_18.xy;
			    u_xlat16_10.xy = u_xlat16_10.zw * u_xlat16_10.xy;
			    u_xlat16_19 = u_xlat16_11 * u_xlat16_22.zzzz + u_xlat16_14.xxxx;
			    u_xlat16_11 = u_xlat16_11 * (-u_xlat16_22.xxxx) + u_xlat16_14.yyyy;
			    u_xlat16_11 = u_xlat16_3.yyyy * u_xlat16_11;
			    u_xlat16_11 = u_xlat16_11 * u_xlat16_11;
			    u_xlat16_14 = u_xlat16_3.xxxx * u_xlat16_19;
			    u_xlat16_11 = u_xlat16_14 * u_xlat16_14 + u_xlat16_11;
			    u_xlat16_11 = min(vec4(u_xlat57), u_xlat16_11);
			    u_xlat16_14 = u_xlat16_3.zzzz * u_xlat16_11 + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat16_11 = u_xlat16_11 * vec4(0.400000006, 0.400000006, 0.400000006, 0.400000006) + vec4(-1.0, -1.0, -1.0, -1.0);
			    u_xlat16_11 = u_xlat16_11 * u_xlat16_11;
			    u_xlat16_11 = u_xlat16_11 * vec4(1.5625, 1.5625, 1.5625, 1.5625) + vec4(-0.5625, -0.5625, -0.5625, -0.5625);
			    u_xlat16_14 = u_xlat16_14 * u_xlat16_14;
			    u_xlat16_4.xw = u_xlat16_11.xy * u_xlat16_14.xy + u_xlat16_4.xw;
			    u_xlat16_4.xw = u_xlat16_15.zw * u_xlat16_16.zw + u_xlat16_4.xw;
			    u_xlat16_4.xw = u_xlat16_11.zw * u_xlat16_14.zw + u_xlat16_4.xw;
			    u_xlat16_11 = u_xlat16_11 * u_xlat16_14;
			    u_xlat16_2.xz = u_xlat16_22.xz * u_xlat16_2.zz;
			    u_xlat16_2.xw = u_xlat16_4.zy * u_xlat16_22.zz + u_xlat16_2.xx;
			    u_xlat16_2.yz = u_xlat16_4.zy * (-u_xlat16_22.xx) + u_xlat16_2.zz;
			    u_xlat16_2 = u_xlat16_3.xyyx * u_xlat16_2;
			    u_xlat16_22.xy = u_xlat16_2.yz * u_xlat16_2.yz;
			    u_xlat16_2.xy = u_xlat16_2.xw * u_xlat16_2.xw + u_xlat16_22.xy;
			    u_xlat16_2.xy = min(vec2(u_xlat57), u_xlat16_2.xy);
			    u_xlat16_2.zw = u_xlat16_3.zz * u_xlat16_2.xy + vec2(-1.0, -1.0);
			    u_xlat16_2.xy = u_xlat16_2.xy * vec2(0.400000006, 0.400000006) + vec2(-1.0, -1.0);
			    u_xlat16_2 = u_xlat16_2 * u_xlat16_2;
			    u_xlat16_2.xy = u_xlat16_2.xy * vec2(1.5625, 1.5625) + vec2(-0.5625, -0.5625);
			    u_xlat16_3.xy = u_xlat16_2.xy * u_xlat16_2.zw + u_xlat16_4.xw;
			    u_xlat16_2.xy = u_xlat16_2.zw * u_xlat16_2.xy;
			    u_xlat16_42 = u_xlat16_3.y + u_xlat16_3.x;
			    u_xlat16_42 = float(1.0) / float(u_xlat16_42);
			    u_xlat16_3.xy = u_xlat8.xy * u_xlat16_18.xy;
			    u_xlat16_3.xy = u_xlat0.xy * u_xlat16_10.xy + u_xlat16_3.xy;
			    u_xlat16_3.xy = u_xlat8.zw * u_xlat16_11.xy + u_xlat16_3.xy;
			    u_xlat16_3.xy = u_xlat13.xy * u_xlat16_18.zw + u_xlat16_3.xy;
			    u_xlat16_3.xy = u_xlat13.zw * u_xlat16_11.zw + u_xlat16_3.xy;
			    u_xlat16_3.xy = u_xlat45.xy * u_xlat16_2.xy + u_xlat16_3.xy;
			    u_xlat16_3.x = u_xlat16_3.y + u_xlat16_3.x;
			    u_xlat16_4.xy = u_xlat9.xy * u_xlat16_18.xy;
			    u_xlat16_4.xy = u_xlat41.xy * u_xlat16_10.xy + u_xlat16_4.xy;
			    u_xlat16_4.xy = u_xlat9.zw * u_xlat16_11.xy + u_xlat16_4.xy;
			    u_xlat16_4.xy = u_xlat6.xy * u_xlat16_18.zw + u_xlat16_4.xy;
			    u_xlat16_4.xy = u_xlat6.zw * u_xlat16_11.zw + u_xlat16_4.xy;
			    u_xlat16_4.xy = u_xlat17.xy * u_xlat16_2.xy + u_xlat16_4.xy;
			    u_xlat16_3.y = u_xlat16_4.y + u_xlat16_4.x;
			    u_xlat16_4.xy = u_xlat7.xy * u_xlat16_18.xy;
			    u_xlat16_4.xy = u_xlat5.xy * u_xlat16_10.xy + u_xlat16_4.xy;
			    u_xlat16_4.xy = u_xlat7.zw * u_xlat16_11.xy + u_xlat16_4.xy;
			    u_xlat16_4.xy = u_xlat12.xy * u_xlat16_18.zw + u_xlat16_4.xy;
			    u_xlat16_4.xy = u_xlat12.zw * u_xlat16_11.zw + u_xlat16_4.xy;
			    u_xlat16_2.xy = u_xlat1.xy * u_xlat16_2.xy + u_xlat16_4.xy;
			    u_xlat16_3.z = u_xlat16_2.y + u_xlat16_2.x;
			    u_xlat16_2.xyz = vec3(u_xlat16_42) * u_xlat16_3.xyz;
			    u_xlat16_0.xz = max((-u_xlat6.wx), (-u_xlat9.zy));
			    u_xlat16_0.yw = max(u_xlat6.wx, u_xlat9.zy);
			    u_xlat16_0.xy = max(u_xlat16_0.zw, u_xlat16_0.xy);
			    u_xlat16_1.z = u_xlat16_0.x;
			    u_xlat16_3.xz = max((-u_xlat7.zy), (-u_xlat12.wx));
			    u_xlat16_3.yw = max(u_xlat7.zy, u_xlat12.wx);
			    u_xlat16_0.xw = max(u_xlat16_3.zw, u_xlat16_3.xy);
			    u_xlat16_1.w = u_xlat16_0.x;
			    u_xlat16_3.xz = max((-u_xlat8.zy), (-u_xlat13.wx));
			    u_xlat16_3.yw = max(u_xlat8.zy, u_xlat13.wx);
			    u_xlat16_1.xy = max(u_xlat16_3.zw, u_xlat16_3.xy);
			    u_xlat16_2.xyz = max(u_xlat16_2.xyz, (-u_xlat16_1.xzw));
			    u_xlat16_0.z = u_xlat16_1.y;
			    u_xlat16_2.xyz = min(u_xlat16_0.zyw, u_xlat16_2.xyz);
			    u_xlat16_2.xyz = min(u_xlat16_2.xyz, vec3(100.0, 100.0, 100.0));
			    u_xlat16_3.xyz = u_xlat16_2.xyz + vec3(0.0549999997, 0.0549999997, 0.0549999997);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.947867334, 0.947867334, 0.947867334);
			    u_xlat5.xyz = log2(abs(u_xlat16_3.xyz));
			    u_xlat5.xyz = u_xlat5.xyz * vec3(2.4000001, 2.4000001, 2.4000001);
			    u_xlat5.xyz = exp2(u_xlat5.xyz);
			    u_xlat16_3.xyz = u_xlat16_2.xyz * vec3(0.0773993805, 0.0773993805, 0.0773993805);
			    u_xlatb6.xyz = greaterThanEqual(vec4(0.0404499993, 0.0404499993, 0.0404499993, 0.0), u_xlat16_2.xyzx).xyz;
			    SV_Target0.x = (u_xlatb6.x) ? u_xlat16_3.x : u_xlat5.x;
			    SV_Target0.y = (u_xlatb6.y) ? u_xlat16_3.y : u_xlat5.y;
			    SV_Target0.z = (u_xlatb6.z) ? u_xlat16_3.z : u_xlat5.z;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}