Shader "Hidden/Universal Render Pipeline/LutBuilderHdr" {
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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _ColorBalance;
			uniform 	vec4 _ColorFilter;
			uniform 	vec4 _ChannelMixerRed;
			uniform 	vec4 _ChannelMixerGreen;
			uniform 	vec4 _ChannelMixerBlue;
			uniform 	vec4 _HueSatCon;
			uniform 	vec4 _Lift;
			uniform 	vec4 _Gamma;
			uniform 	vec4 _Gain;
			uniform 	vec4 _Shadows;
			uniform 	vec4 _Midtones;
			uniform 	vec4 _Highlights;
			uniform 	vec4 _ShaHiLimits;
			uniform 	vec4 _SplitShadows;
			uniform 	vec4 _SplitHighlights;
			UNITY_LOCATION(0) uniform mediump sampler2D _CurveMaster;
			UNITY_LOCATION(1) uniform mediump sampler2D _CurveRed;
			UNITY_LOCATION(2) uniform mediump sampler2D _CurveGreen;
			UNITY_LOCATION(3) uniform mediump sampler2D _CurveBlue;
			UNITY_LOCATION(4) uniform mediump sampler2D _CurveHueVsHue;
			UNITY_LOCATION(5) uniform mediump sampler2D _CurveHueVsSat;
			UNITY_LOCATION(6) uniform mediump sampler2D _CurveSatVsSat;
			UNITY_LOCATION(7) uniform mediump sampler2D _CurveLumVsSat;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec2 u_xlat16_0;
			bool u_xlatb0;
			vec3 u_xlat1;
			mediump float u_xlat16_1;
			ivec3 u_xlati1;
			bool u_xlatb1;
			vec3 u_xlat2;
			mediump float u_xlat16_2;
			ivec3 u_xlati2;
			vec3 u_xlat3;
			mediump vec4 u_xlat16_3;
			mediump vec4 u_xlat16_4;
			vec3 u_xlat5;
			mediump vec4 u_xlat16_5;
			bvec3 u_xlatb5;
			vec3 u_xlat6;
			mediump vec2 u_xlat16_7;
			mediump vec3 u_xlat16_8;
			float u_xlat10;
			mediump vec2 u_xlat16_10;
			bool u_xlatb10;
			vec3 u_xlat11;
			mediump vec3 u_xlat16_13;
			mediump vec2 u_xlat16_18;
			vec2 u_xlat20;
			mediump float u_xlat16_22;
			mediump float u_xlat16_25;
			float u_xlat27;
			bool u_xlatb27;
			float u_xlat28;
			mediump float u_xlat16_28;
			mediump float u_xlat16_31;
			void main()
			{
			    u_xlat0.x = vs_TEXCOORD0.x * _Lut_Params.x;
			    u_xlat0.x = floor(u_xlat0.x);
			    u_xlat1.x = vs_TEXCOORD0.x * _Lut_Params.x + (-u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _Lut_Params.z;
			    u_xlat0.z = u_xlat0.x * _Lut_Params.w;
			    u_xlat1.y = vs_TEXCOORD0.y;
			    u_xlat0.xy = u_xlat1.xy + (-_Lut_Params.zz);
			    u_xlat1.x = _Lut_Params.w;
			    u_xlat1.z = 2.0;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat1.xxz + vec3(-0.386036009, -0.386036009, -0.386036009);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(13.6054821, 13.6054821, 13.6054821);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.0479959995, -0.0479959995, -0.0479959995);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.179999992, 0.179999992, 0.179999992);
			    u_xlat1.x = dot(vec3(0.390404999, 0.549941003, 0.00892631989), u_xlat0.xyz);
			    u_xlat1.y = dot(vec3(0.070841603, 0.963172019, 0.00135775004), u_xlat0.xyz);
			    u_xlat1.z = dot(vec3(0.0231081992, 0.128021002, 0.936245024), u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat1.xyz * _ColorBalance.xyz;
			    u_xlat1.x = dot(vec3(2.85846996, -1.62879002, -0.0248910002), u_xlat0.xyz);
			    u_xlat1.y = dot(vec3(-0.210181996, 1.15820003, 0.000324280991), u_xlat0.xyz);
			    u_xlat1.z = dot(vec3(-0.0418119989, -0.118169002, 1.06867003), u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat1.xyz * vec3(5.55555582, 5.55555582, 5.55555582) + vec3(0.0479959995, 0.0479959995, 0.0479959995);
			    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
			    u_xlat0.xyz = log2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0734997839, 0.0734997839, 0.0734997839) + vec3(-0.0275523961, -0.0275523961, -0.0275523961);
			    u_xlat0.xyz = u_xlat0.xyz * _HueSatCon.zzz + vec3(0.0275523961, 0.0275523961, 0.0275523961);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(13.6054821, 13.6054821, 13.6054821);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.0479959995, -0.0479959995, -0.0479959995);
			    u_xlat0.xyz = u_xlat0.xyz * _ColorFilter.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.179999992, 0.179999992, 0.179999992);
			    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
			    u_xlat0.xyz = log2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.454545468, 0.454545468, 0.454545468);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat1.xyz = u_xlat0.xyz + u_xlat0.xyz;
			    u_xlat2.xyz = u_xlat0.xyz * u_xlat0.xyz;
			    u_xlat3.xyz = min(u_xlat0.xyz, vec3(1.0, 1.0, 1.0));
			    u_xlat0.xyz = sqrt(u_xlat0.xyz);
			    u_xlat16_4.x = dot(u_xlat3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat27 = u_xlat16_4.x + _SplitShadows.w;
			    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
			    u_xlat28 = (-u_xlat27) + 1.0;
			    u_xlat3.xyz = _SplitShadows.xyz + vec3(-0.5, -0.5, -0.5);
			    u_xlat3.xyz = vec3(u_xlat28) * u_xlat3.xyz + vec3(0.5, 0.5, 0.5);
			    u_xlat5.xyz = (-u_xlat3.xyz) * vec3(2.0, 2.0, 2.0) + vec3(1.0, 1.0, 1.0);
			    u_xlat2.xyz = u_xlat2.xyz * u_xlat5.xyz;
			    u_xlat2.xyz = u_xlat1.xyz * u_xlat3.xyz + u_xlat2.xyz;
			    u_xlatb5.xyz = greaterThanEqual(u_xlat3.xyzx, vec4(0.5, 0.5, 0.5, 0.0)).xyz;
			    u_xlat6.x = (u_xlatb5.x) ? float(0.0) : float(1.0);
			    u_xlat6.y = (u_xlatb5.y) ? float(0.0) : float(1.0);
			    u_xlat6.z = (u_xlatb5.z) ? float(0.0) : float(1.0);
			    u_xlat5.x = u_xlatb5.x ? float(1.0) : 0.0;
			    u_xlat5.y = u_xlatb5.y ? float(1.0) : 0.0;
			    u_xlat5.z = u_xlatb5.z ? float(1.0) : 0.0;
			;
			    u_xlat2.xyz = u_xlat2.xyz * u_xlat6.xyz;
			    u_xlat6.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
			    u_xlat1.xyz = u_xlat1.xyz * u_xlat6.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.xyz + u_xlat1.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat5.xyz + u_xlat2.xyz;
			    u_xlat1.xyz = u_xlat0.xyz + u_xlat0.xyz;
			    u_xlat2.xyz = u_xlat0.xyz * u_xlat0.xyz;
			    u_xlat0.xyz = sqrt(u_xlat0.xyz);
			    u_xlat3.xyz = _SplitHighlights.xyz + vec3(-0.5, -0.5, -0.5);
			    u_xlat3.xyz = vec3(u_xlat27) * u_xlat3.xyz + vec3(0.5, 0.5, 0.5);
			    u_xlat5.xyz = (-u_xlat3.xyz) * vec3(2.0, 2.0, 2.0) + vec3(1.0, 1.0, 1.0);
			    u_xlat2.xyz = u_xlat2.xyz * u_xlat5.xyz;
			    u_xlat2.xyz = u_xlat1.xyz * u_xlat3.xyz + u_xlat2.xyz;
			    u_xlatb5.xyz = greaterThanEqual(u_xlat3.xyzx, vec4(0.5, 0.5, 0.5, 0.0)).xyz;
			    u_xlat6.x = (u_xlatb5.x) ? float(0.0) : float(1.0);
			    u_xlat6.y = (u_xlatb5.y) ? float(0.0) : float(1.0);
			    u_xlat6.z = (u_xlatb5.z) ? float(0.0) : float(1.0);
			    u_xlat5.x = u_xlatb5.x ? float(1.0) : 0.0;
			    u_xlat5.y = u_xlatb5.y ? float(1.0) : 0.0;
			    u_xlat5.z = u_xlatb5.z ? float(1.0) : 0.0;
			;
			    u_xlat2.xyz = u_xlat2.xyz * u_xlat6.xyz;
			    u_xlat6.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
			    u_xlat1.xyz = u_xlat1.xyz * u_xlat6.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.xyz + u_xlat1.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat5.xyz + u_xlat2.xyz;
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.20000005, 2.20000005, 2.20000005);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat1.x = dot(u_xlat0.xyz, _ChannelMixerRed.xyz);
			    u_xlat1.y = dot(u_xlat0.xyz, _ChannelMixerGreen.xyz);
			    u_xlat1.z = dot(u_xlat0.xyz, _ChannelMixerBlue.xyz);
			    u_xlat0.xyz = u_xlat1.xyz * _Midtones.xyz;
			    u_xlat16_4.x = dot(u_xlat1.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat2.xy = u_xlat16_4.xx + (-_ShaHiLimits.xz);
			    u_xlat20.xy = (-_ShaHiLimits.xz) + _ShaHiLimits.yw;
			    u_xlat20.xy = vec2(1.0, 1.0) / u_xlat20.xy;
			    u_xlat2.xy = u_xlat20.xy * u_xlat2.xy;
			    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
			    u_xlat20.xy = u_xlat2.xy * vec2(-2.0, -2.0) + vec2(3.0, 3.0);
			    u_xlat2.xy = u_xlat2.xy * u_xlat2.xy;
			    u_xlat27 = (-u_xlat20.x) * u_xlat2.x + 1.0;
			    u_xlat28 = (-u_xlat27) + 1.0;
			    u_xlat28 = (-u_xlat20.y) * u_xlat2.y + u_xlat28;
			    u_xlat2.x = u_xlat2.y * u_xlat20.y;
			    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat28);
			    u_xlat11.xyz = u_xlat1.xyz * _Shadows.xyz;
			    u_xlat1.xyz = u_xlat1.xyz * _Highlights.xyz;
			    u_xlat0.xyz = u_xlat11.xyz * vec3(u_xlat27) + u_xlat0.xyz;
			    u_xlat0.xyz = u_xlat1.xyz * u_xlat2.xxx + u_xlat0.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * _Gain.xyz + _Lift.xyz;
			    u_xlati1.xyz = ivec3(uvec3(lessThan(vec4(0.0, 0.0, 0.0, 0.0), u_xlat0.xyzx).xyz) * 0xFFFFFFFFu);
			    u_xlati2.xyz = ivec3(uvec3(lessThan(u_xlat0.xyzx, vec4(0.0, 0.0, 0.0, 0.0)).xyz) * 0xFFFFFFFFu);
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * _Gamma.xyz;
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlati1.xyz = (-u_xlati1.xyz) + u_xlati2.xyz;
			    u_xlat1.xyz = vec3(u_xlati1.xyz);
			    u_xlat2.xyz = u_xlat0.xyz * u_xlat1.xyz;
			    u_xlat16_4.xy = u_xlat1.yz * u_xlat0.yz + (-u_xlat2.zy);
			    u_xlatb27 = u_xlat2.y>=u_xlat2.z;
			    u_xlat16_22 = (u_xlatb27) ? 1.0 : 0.0;
			    u_xlat16_4.xy = u_xlat16_4.xy * vec2(u_xlat16_22);
			    u_xlat16_3.xy = u_xlat1.zy * u_xlat0.zy + u_xlat16_4.xy;
			    u_xlat16_5.w = (-u_xlat2.x);
			    u_xlat16_4.x = float(1.0);
			    u_xlat16_4.y = float(-1.0);
			    u_xlat16_3.zw = vec2(u_xlat16_22) * u_xlat16_4.xy + vec2(-1.0, 0.666666687);
			    u_xlat16_5.xyz = (-u_xlat16_3.xyw);
			    u_xlat16_4.yzw = u_xlat16_3.yzx + u_xlat16_5.yzw;
			    u_xlat16_4.x = u_xlat1.x * u_xlat0.x + u_xlat16_5.x;
			    u_xlatb0 = u_xlat2.x>=u_xlat16_3.x;
			    u_xlat16_7.x = (u_xlatb0) ? 1.0 : 0.0;
			    u_xlat16_31 = u_xlat16_7.x * u_xlat16_4.w + u_xlat2.x;
			    u_xlat16_4.xyz = u_xlat16_7.xxx * u_xlat16_4.xyz + u_xlat16_3.xyw;
			    u_xlat16_7.x = dot(u_xlat2.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_25 = min(u_xlat16_4.y, u_xlat16_31);
			    u_xlat16_13.x = (-u_xlat16_4.y) + u_xlat16_31;
			    u_xlat16_31 = u_xlat16_4.x + (-u_xlat16_25);
			    u_xlat16_25 = u_xlat16_31 * 6.0 + 9.99999975e-05;
			    u_xlat16_13.x = u_xlat16_13.x / u_xlat16_25;
			    u_xlat16_0.x = u_xlat16_13.x + u_xlat16_4.z;
			    u_xlat1.x = abs(u_xlat16_0.x) + _HueSatCon.x;
			    u_xlat16_0.x = abs(u_xlat16_0.x);
			    u_xlat1.y = 0.0;
			    u_xlat16_10.x = texture(_CurveHueVsHue, u_xlat1.xy, _GlobalMipBias.x).x;
			    u_xlat10 = u_xlat16_10.x;
			    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
			    u_xlat1.x = u_xlat1.x + u_xlat10;
			    u_xlat1.x = u_xlat1.x + -0.5;
			    u_xlatb10 = 1.0<u_xlat1.x;
			    u_xlat16_13.xy = u_xlat1.xx + vec2(1.0, -1.0);
			    u_xlat16_22 = (u_xlatb10) ? u_xlat16_13.y : u_xlat1.x;
			    u_xlatb1 = u_xlat1.x<0.0;
			    u_xlat16_13.x = (u_xlatb1) ? u_xlat16_13.x : u_xlat16_22;
			    u_xlat16_8.xyz = u_xlat16_13.xxx + vec3(1.0, 0.666666687, 0.333333343);
			    u_xlat16_8.xyz = fract(u_xlat16_8.xyz);
			    u_xlat16_8.xyz = u_xlat16_8.xyz * vec3(6.0, 6.0, 6.0) + vec3(-3.0, -3.0, -3.0);
			    u_xlat16_8.xyz = abs(u_xlat16_8.xyz) + vec3(-1.0, -1.0, -1.0);
			    u_xlat16_8.xyz = clamp(u_xlat16_8.xyz, 0.0, 1.0);
			    u_xlat16_8.xyz = u_xlat16_8.xyz + vec3(-1.0, -1.0, -1.0);
			    u_xlat16_13.x = u_xlat16_4.x + 9.99999975e-05;
			    u_xlat16_18.x = u_xlat16_31 / u_xlat16_13.x;
			    u_xlat16_13.xyz = u_xlat16_18.xxx * u_xlat16_8.xyz + vec3(1.0, 1.0, 1.0);
			    u_xlat16_8.xyz = u_xlat16_13.xyz * u_xlat16_4.xxx;
			    u_xlat16_25 = dot(u_xlat16_8.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat1.xyz = u_xlat16_4.xxx * u_xlat16_13.xyz + (-vec3(u_xlat16_25));
			    u_xlat16_0.y = float(0.0);
			    u_xlat16_18.y = float(0.0);
			    u_xlat16_28 = texture(_CurveHueVsSat, u_xlat16_0.xy, _GlobalMipBias.x).x;
			    u_xlat16_2 = texture(_CurveSatVsSat, u_xlat16_18.xy, _GlobalMipBias.x).x;
			    u_xlat2.x = u_xlat16_2;
			    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			    u_xlat28 = u_xlat16_28;
			    u_xlat28 = clamp(u_xlat28, 0.0, 1.0);
			    u_xlat28 = u_xlat28 + u_xlat28;
			    u_xlat28 = dot(u_xlat2.xx, vec2(u_xlat28));
			    u_xlat16_7.y = 0.0;
			    u_xlat16_2 = texture(_CurveLumVsSat, u_xlat16_7.xy, _GlobalMipBias.x).x;
			    u_xlat2.x = u_xlat16_2;
			    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			    u_xlat28 = u_xlat28 * u_xlat2.x;
			    u_xlat28 = dot(_HueSatCon.yy, vec2(u_xlat28));
			    u_xlat1.xyz = vec3(u_xlat28) * u_xlat1.xyz + vec3(u_xlat16_25);
			    u_xlat16_4.x = max(u_xlat1.y, u_xlat1.x);
			    u_xlat16_4.x = max(u_xlat1.z, u_xlat16_4.x);
			    u_xlat16_4.x = u_xlat16_4.x + 1.0;
			    u_xlat16_4.x = float(1.0) / float(u_xlat16_4.x);
			    u_xlat0.xyz = u_xlat1.xyz * u_xlat16_4.xxx + vec3(0.00390625, 0.00390625, 0.00390625);
			    u_xlat0.w = 0.0;
			    u_xlat16_1 = texture(_CurveMaster, u_xlat0.xw, _GlobalMipBias.x).x;
			    u_xlat1.x = u_xlat16_1;
			    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
			    u_xlat16_10.x = texture(_CurveMaster, u_xlat0.yw, _GlobalMipBias.x).x;
			    u_xlat16_10.y = texture(_CurveMaster, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlat1.yz = u_xlat16_10.xy;
			    u_xlat1.yz = clamp(u_xlat1.yz, 0.0, 1.0);
			    u_xlat0.xyz = u_xlat1.xyz + vec3(0.00390625, 0.00390625, 0.00390625);
			    u_xlat0.w = 0.0;
			    u_xlat16_1 = texture(_CurveRed, u_xlat0.xw, _GlobalMipBias.x).x;
			    u_xlat1.x = u_xlat16_1;
			    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
			    u_xlat16_10.x = texture(_CurveGreen, u_xlat0.yw, _GlobalMipBias.x).x;
			    u_xlat16_10.y = texture(_CurveBlue, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlat1.yz = u_xlat16_10.xy;
			    u_xlat1.yz = clamp(u_xlat1.yz, 0.0, 1.0);
			    u_xlat16_4.x = max(u_xlat1.y, u_xlat1.x);
			    u_xlat16_4.x = max(u_xlat1.z, u_xlat16_4.x);
			    u_xlat16_4.x = (-u_xlat16_4.x) + 1.0;
			    u_xlat16_4.x = float(1.0) / float(u_xlat16_4.x);
			    u_xlat16_4.xyz = u_xlat1.xyz * u_xlat16_4.xxx;
			    SV_Target0.xyz = max(u_xlat16_4.xyz, vec3(0.0, 0.0, 0.0));
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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _ColorBalance;
			uniform 	vec4 _ColorFilter;
			uniform 	vec4 _ChannelMixerRed;
			uniform 	vec4 _ChannelMixerGreen;
			uniform 	vec4 _ChannelMixerBlue;
			uniform 	vec4 _HueSatCon;
			uniform 	vec4 _Lift;
			uniform 	vec4 _Gamma;
			uniform 	vec4 _Gain;
			uniform 	vec4 _Shadows;
			uniform 	vec4 _Midtones;
			uniform 	vec4 _Highlights;
			uniform 	vec4 _ShaHiLimits;
			uniform 	vec4 _SplitShadows;
			uniform 	vec4 _SplitHighlights;
			UNITY_LOCATION(0) uniform mediump sampler2D _CurveMaster;
			UNITY_LOCATION(1) uniform mediump sampler2D _CurveRed;
			UNITY_LOCATION(2) uniform mediump sampler2D _CurveGreen;
			UNITY_LOCATION(3) uniform mediump sampler2D _CurveBlue;
			UNITY_LOCATION(4) uniform mediump sampler2D _CurveHueVsHue;
			UNITY_LOCATION(5) uniform mediump sampler2D _CurveHueVsSat;
			UNITY_LOCATION(6) uniform mediump sampler2D _CurveSatVsSat;
			UNITY_LOCATION(7) uniform mediump sampler2D _CurveLumVsSat;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec2 u_xlat16_0;
			bvec3 u_xlatb0;
			vec3 u_xlat1;
			mediump float u_xlat16_1;
			ivec3 u_xlati1;
			bvec4 u_xlatb1;
			mediump vec4 u_xlat16_2;
			mediump vec4 u_xlat16_3;
			mediump vec4 u_xlat16_4;
			vec3 u_xlat5;
			mediump float u_xlat16_5;
			ivec3 u_xlati5;
			vec3 u_xlat6;
			vec3 u_xlat7;
			bvec3 u_xlatb7;
			vec3 u_xlat8;
			vec3 u_xlat10;
			mediump vec2 u_xlat16_10;
			bvec2 u_xlatb10;
			mediump vec3 u_xlat16_11;
			vec3 u_xlat14;
			mediump vec2 u_xlat16_18;
			float u_xlat19;
			bool u_xlatb19;
			mediump float u_xlat16_20;
			mediump float u_xlat16_21;
			vec2 u_xlat23;
			float u_xlat27;
			bool u_xlatb27;
			float u_xlat28;
			mediump float u_xlat16_28;
			bool u_xlatb28;
			mediump float u_xlat16_29;
			void main()
			{
			    u_xlat0.x = vs_TEXCOORD0.x * _Lut_Params.x;
			    u_xlat0.x = floor(u_xlat0.x);
			    u_xlat1.x = vs_TEXCOORD0.x * _Lut_Params.x + (-u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _Lut_Params.z;
			    u_xlat0.z = u_xlat0.x * _Lut_Params.w;
			    u_xlat1.y = vs_TEXCOORD0.y;
			    u_xlat0.xy = u_xlat1.xy + (-_Lut_Params.zz);
			    u_xlat1.x = _Lut_Params.w;
			    u_xlat1.z = 2.0;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat1.xxz + vec3(-0.386036009, -0.386036009, -0.386036009);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(13.6054821, 13.6054821, 13.6054821);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.0479959995, -0.0479959995, -0.0479959995);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.179999992, 0.179999992, 0.179999992);
			    u_xlat1.x = dot(vec3(0.390404999, 0.549941003, 0.00892631989), u_xlat0.xyz);
			    u_xlat1.y = dot(vec3(0.070841603, 0.963172019, 0.00135775004), u_xlat0.xyz);
			    u_xlat1.z = dot(vec3(0.0231081992, 0.128021002, 0.936245024), u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat1.xyz * _ColorBalance.xyz;
			    u_xlat1.x = dot(vec3(2.85846996, -1.62879002, -0.0248910002), u_xlat0.xyz);
			    u_xlat1.y = dot(vec3(-0.210181996, 1.15820003, 0.000324280991), u_xlat0.xyz);
			    u_xlat1.z = dot(vec3(-0.0418119989, -0.118169002, 1.06867003), u_xlat0.xyz);
			    u_xlat16_2.x = dot(vec3(0.439700991, 0.382977992, 0.177334994), u_xlat1.xyz);
			    u_xlat16_2.y = dot(vec3(0.0897922963, 0.813422978, 0.0967615992), u_xlat1.xyz);
			    u_xlat16_2.z = dot(vec3(0.0175439995, 0.111543998, 0.870703995), u_xlat1.xyz);
			    u_xlat16_2.xyz = max(u_xlat16_2.xyz, vec3(0.0, 0.0, 0.0));
			    u_xlat16_2.xyz = min(u_xlat16_2.xyz, vec3(65504.0, 65504.0, 65504.0));
			    u_xlat16_3.xyz = u_xlat16_2.xyz * vec3(0.5, 0.5, 0.5) + vec3(1.525878e-05, 1.525878e-05, 1.525878e-05);
			    u_xlat16_3.xyz = log2(u_xlat16_3.xyz);
			    u_xlat16_3.xyz = u_xlat16_3.xyz + vec3(9.72000027, 9.72000027, 9.72000027);
			    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(0.0570776239, 0.0570776239, 0.0570776239);
			    u_xlat16_4.xyz = log2(u_xlat16_2.xyz);
			    u_xlatb0.xyz = lessThan(u_xlat16_2.xyzx, vec4(3.05175708e-05, 3.05175708e-05, 3.05175708e-05, 0.0)).xyz;
			    u_xlat16_2.xyz = u_xlat16_4.xyz + vec3(9.72000027, 9.72000027, 9.72000027);
			    u_xlat16_2.xyz = u_xlat16_2.xyz * vec3(0.0570776239, 0.0570776239, 0.0570776239);
			    {
			        vec4 hlslcc_movcTemp = u_xlat16_2;
			        hlslcc_movcTemp.x = (u_xlatb0.x) ? u_xlat16_3.x : u_xlat16_2.x;
			        hlslcc_movcTemp.y = (u_xlatb0.y) ? u_xlat16_3.y : u_xlat16_2.y;
			        hlslcc_movcTemp.z = (u_xlatb0.z) ? u_xlat16_3.z : u_xlat16_2.z;
			        u_xlat16_2 = hlslcc_movcTemp;
			    }
			    u_xlat0.xyz = u_xlat16_2.xyz + vec3(-0.413588405, -0.413588405, -0.413588405);
			    u_xlat0.xyz = u_xlat0.xyz * _HueSatCon.zzz + vec3(0.413588405, 0.413588405, 0.413588405);
			    u_xlatb1 = lessThan(u_xlat0.xxyy, vec4(-0.301369876, 1.46799636, -0.301369876, 1.46799636));
			    u_xlat16_2.xyz = u_xlat0.xyz * vec3(17.5200005, 17.5200005, 17.5200005) + vec3(-9.72000027, -9.72000027, -9.72000027);
			    u_xlatb0.xy = lessThan(u_xlat0.zzzz, vec4(-0.301369876, 1.46799636, 0.0, 0.0)).xy;
			    u_xlat16_2.xyz = exp2(u_xlat16_2.xyz);
			    u_xlat16_3.x = (u_xlatb1.y) ? u_xlat16_2.x : float(65504.0);
			    u_xlat16_3.y = (u_xlatb1.w) ? u_xlat16_2.y : float(65504.0);
			    u_xlat16_2.xyw = u_xlat16_2.xyz + vec3(-1.52587891e-05, -1.52587891e-05, -1.52587891e-05);
			    u_xlat16_20 = (u_xlatb0.y) ? u_xlat16_2.z : 65504.0;
			    u_xlat16_2.xyw = u_xlat16_2.xyw + u_xlat16_2.xyw;
			    {
			        vec4 hlslcc_movcTemp = u_xlat16_3;
			        hlslcc_movcTemp.x = (u_xlatb1.x) ? u_xlat16_2.x : u_xlat16_3.x;
			        hlslcc_movcTemp.y = (u_xlatb1.z) ? u_xlat16_2.y : u_xlat16_3.y;
			        u_xlat16_3 = hlslcc_movcTemp;
			    }
			    u_xlat16_3.z = (u_xlatb0.x) ? u_xlat16_2.w : u_xlat16_20;
			    u_xlat0.x = dot(vec3(1.45143926, -0.236510754, -0.214928567), u_xlat16_3.xyz);
			    u_xlat0.y = dot(vec3(-0.0765537769, 1.17622972, -0.0996759236), u_xlat16_3.xyz);
			    u_xlat0.z = dot(vec3(0.00831614807, -0.00603244966, 0.997716308), u_xlat16_3.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * _ColorFilter.xyz;
			    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
			    u_xlat0.xyz = log2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.454545468, 0.454545468, 0.454545468);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat1.xyz = u_xlat0.xyz + u_xlat0.xyz;
			    u_xlat5.xyz = u_xlat0.xyz * u_xlat0.xyz;
			    u_xlat6.xyz = min(u_xlat0.xyz, vec3(1.0, 1.0, 1.0));
			    u_xlat0.xyz = sqrt(u_xlat0.xyz);
			    u_xlat16_2.x = dot(u_xlat6.xyz, vec3(0.272228986, 0.674081981, 0.0536894985));
			    u_xlat27 = u_xlat16_2.x + _SplitShadows.w;
			    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
			    u_xlat28 = (-u_xlat27) + 1.0;
			    u_xlat6.xyz = _SplitShadows.xyz + vec3(-0.5, -0.5, -0.5);
			    u_xlat6.xyz = vec3(u_xlat28) * u_xlat6.xyz + vec3(0.5, 0.5, 0.5);
			    u_xlat7.xyz = (-u_xlat6.xyz) * vec3(2.0, 2.0, 2.0) + vec3(1.0, 1.0, 1.0);
			    u_xlat5.xyz = u_xlat5.xyz * u_xlat7.xyz;
			    u_xlat5.xyz = u_xlat1.xyz * u_xlat6.xyz + u_xlat5.xyz;
			    u_xlatb7.xyz = greaterThanEqual(u_xlat6.xyzx, vec4(0.5, 0.5, 0.5, 0.0)).xyz;
			    u_xlat8.x = (u_xlatb7.x) ? float(0.0) : float(1.0);
			    u_xlat8.y = (u_xlatb7.y) ? float(0.0) : float(1.0);
			    u_xlat8.z = (u_xlatb7.z) ? float(0.0) : float(1.0);
			    u_xlat7.x = u_xlatb7.x ? float(1.0) : 0.0;
			    u_xlat7.y = u_xlatb7.y ? float(1.0) : 0.0;
			    u_xlat7.z = u_xlatb7.z ? float(1.0) : 0.0;
			;
			    u_xlat5.xyz = u_xlat5.xyz * u_xlat8.xyz;
			    u_xlat8.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat6.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
			    u_xlat1.xyz = u_xlat1.xyz * u_xlat8.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat6.xyz + u_xlat1.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat7.xyz + u_xlat5.xyz;
			    u_xlat1.xyz = u_xlat0.xyz + u_xlat0.xyz;
			    u_xlat5.xyz = u_xlat0.xyz * u_xlat0.xyz;
			    u_xlat0.xyz = sqrt(u_xlat0.xyz);
			    u_xlat6.xyz = _SplitHighlights.xyz + vec3(-0.5, -0.5, -0.5);
			    u_xlat6.xyz = vec3(u_xlat27) * u_xlat6.xyz + vec3(0.5, 0.5, 0.5);
			    u_xlat7.xyz = (-u_xlat6.xyz) * vec3(2.0, 2.0, 2.0) + vec3(1.0, 1.0, 1.0);
			    u_xlat5.xyz = u_xlat5.xyz * u_xlat7.xyz;
			    u_xlat5.xyz = u_xlat1.xyz * u_xlat6.xyz + u_xlat5.xyz;
			    u_xlatb7.xyz = greaterThanEqual(u_xlat6.xyzx, vec4(0.5, 0.5, 0.5, 0.0)).xyz;
			    u_xlat8.x = (u_xlatb7.x) ? float(0.0) : float(1.0);
			    u_xlat8.y = (u_xlatb7.y) ? float(0.0) : float(1.0);
			    u_xlat8.z = (u_xlatb7.z) ? float(0.0) : float(1.0);
			    u_xlat7.x = u_xlatb7.x ? float(1.0) : 0.0;
			    u_xlat7.y = u_xlatb7.y ? float(1.0) : 0.0;
			    u_xlat7.z = u_xlatb7.z ? float(1.0) : 0.0;
			;
			    u_xlat5.xyz = u_xlat5.xyz * u_xlat8.xyz;
			    u_xlat8.xyz = (-u_xlat6.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat6.xyz = u_xlat6.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
			    u_xlat1.xyz = u_xlat1.xyz * u_xlat8.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat6.xyz + u_xlat1.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat7.xyz + u_xlat5.xyz;
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.20000005, 2.20000005, 2.20000005);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat1.x = dot(u_xlat0.xyz, _ChannelMixerRed.xyz);
			    u_xlat1.y = dot(u_xlat0.xyz, _ChannelMixerGreen.xyz);
			    u_xlat1.z = dot(u_xlat0.xyz, _ChannelMixerBlue.xyz);
			    u_xlat0.xyz = u_xlat1.xyz * _Midtones.xyz;
			    u_xlat16_2.x = dot(u_xlat1.xyz, vec3(0.272228986, 0.674081981, 0.0536894985));
			    u_xlat5.xy = u_xlat16_2.xx + (-_ShaHiLimits.xz);
			    u_xlat23.xy = (-_ShaHiLimits.xz) + _ShaHiLimits.yw;
			    u_xlat23.xy = vec2(1.0, 1.0) / u_xlat23.xy;
			    u_xlat5.xy = u_xlat23.xy * u_xlat5.xy;
			    u_xlat5.xy = clamp(u_xlat5.xy, 0.0, 1.0);
			    u_xlat23.xy = u_xlat5.xy * vec2(-2.0, -2.0) + vec2(3.0, 3.0);
			    u_xlat5.xy = u_xlat5.xy * u_xlat5.xy;
			    u_xlat27 = (-u_xlat23.x) * u_xlat5.x + 1.0;
			    u_xlat28 = (-u_xlat27) + 1.0;
			    u_xlat28 = (-u_xlat23.y) * u_xlat5.y + u_xlat28;
			    u_xlat5.x = u_xlat5.y * u_xlat23.y;
			    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat28);
			    u_xlat14.xyz = u_xlat1.xyz * _Shadows.xyz;
			    u_xlat1.xyz = u_xlat1.xyz * _Highlights.xyz;
			    u_xlat0.xyz = u_xlat14.xyz * vec3(u_xlat27) + u_xlat0.xyz;
			    u_xlat0.xyz = u_xlat1.xyz * u_xlat5.xxx + u_xlat0.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * _Gain.xyz + _Lift.xyz;
			    u_xlati1.xyz = ivec3(uvec3(lessThan(vec4(0.0, 0.0, 0.0, 0.0), u_xlat0.xyzx).xyz) * 0xFFFFFFFFu);
			    u_xlati5.xyz = ivec3(uvec3(lessThan(u_xlat0.xyzx, vec4(0.0, 0.0, 0.0, 0.0)).xyz) * 0xFFFFFFFFu);
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * _Gamma.xyz;
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlati1.xyz = (-u_xlati1.xyz) + u_xlati5.xyz;
			    u_xlat1.xyz = vec3(u_xlati1.xyz);
			    u_xlat5.xyz = u_xlat0.xyz * u_xlat1.xyz;
			    u_xlat16_2.xy = u_xlat1.yz * u_xlat0.yz + (-u_xlat5.zy);
			    u_xlatb27 = u_xlat5.y>=u_xlat5.z;
			    u_xlat16_20 = (u_xlatb27) ? 1.0 : 0.0;
			    u_xlat16_2.xy = u_xlat16_2.xy * vec2(u_xlat16_20);
			    u_xlat16_3.xy = u_xlat1.zy * u_xlat0.zy + u_xlat16_2.xy;
			    u_xlat16_4.w = (-u_xlat5.x);
			    u_xlat16_2.x = float(1.0);
			    u_xlat16_2.y = float(-1.0);
			    u_xlat16_3.zw = vec2(u_xlat16_20) * u_xlat16_2.xy + vec2(-1.0, 0.666666687);
			    u_xlat16_4.xyz = (-u_xlat16_3.xyw);
			    u_xlat16_2.yzw = u_xlat16_3.yzx + u_xlat16_4.yzw;
			    u_xlat16_2.x = u_xlat1.x * u_xlat0.x + u_xlat16_4.x;
			    u_xlatb0.x = u_xlat5.x>=u_xlat16_3.x;
			    u_xlat16_21 = (u_xlatb0.x) ? 1.0 : 0.0;
			    u_xlat16_29 = u_xlat16_21 * u_xlat16_2.w + u_xlat5.x;
			    u_xlat16_2.xyz = vec3(u_xlat16_21) * u_xlat16_2.xyz + u_xlat16_3.xyw;
			    u_xlat16_3.x = dot(u_xlat5.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_21 = min(u_xlat16_2.y, u_xlat16_29);
			    u_xlat16_11.x = (-u_xlat16_2.y) + u_xlat16_29;
			    u_xlat16_29 = u_xlat16_2.x + (-u_xlat16_21);
			    u_xlat16_21 = u_xlat16_29 * 6.0 + 9.99999975e-05;
			    u_xlat16_11.x = u_xlat16_11.x / u_xlat16_21;
			    u_xlat16_0.x = u_xlat16_11.x + u_xlat16_2.z;
			    u_xlat1.x = abs(u_xlat16_0.x) + _HueSatCon.x;
			    u_xlat16_0.x = abs(u_xlat16_0.x);
			    u_xlat1.y = 0.0;
			    u_xlat16_10.x = texture(_CurveHueVsHue, u_xlat1.xy, _GlobalMipBias.x).x;
			    u_xlat10.x = u_xlat16_10.x;
			    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
			    u_xlat1.x = u_xlat1.x + u_xlat10.x;
			    u_xlat1.x = u_xlat1.x + -0.5;
			    u_xlatb10.x = 1.0<u_xlat1.x;
			    u_xlat16_11.xy = u_xlat1.xx + vec2(1.0, -1.0);
			    u_xlat16_20 = (u_xlatb10.x) ? u_xlat16_11.y : u_xlat1.x;
			    u_xlatb1.x = u_xlat1.x<0.0;
			    u_xlat16_11.x = (u_xlatb1.x) ? u_xlat16_11.x : u_xlat16_20;
			    u_xlat16_4.xyz = u_xlat16_11.xxx + vec3(1.0, 0.666666687, 0.333333343);
			    u_xlat16_4.xyz = fract(u_xlat16_4.xyz);
			    u_xlat16_4.xyz = u_xlat16_4.xyz * vec3(6.0, 6.0, 6.0) + vec3(-3.0, -3.0, -3.0);
			    u_xlat16_4.xyz = abs(u_xlat16_4.xyz) + vec3(-1.0, -1.0, -1.0);
			    u_xlat16_4.xyz = clamp(u_xlat16_4.xyz, 0.0, 1.0);
			    u_xlat16_4.xyz = u_xlat16_4.xyz + vec3(-1.0, -1.0, -1.0);
			    u_xlat16_11.x = u_xlat16_2.x + 9.99999975e-05;
			    u_xlat16_18.x = u_xlat16_29 / u_xlat16_11.x;
			    u_xlat16_11.xyz = u_xlat16_18.xxx * u_xlat16_4.xyz + vec3(1.0, 1.0, 1.0);
			    u_xlat16_4.xyz = u_xlat16_11.xyz * u_xlat16_2.xxx;
			    u_xlat16_21 = dot(u_xlat16_4.xyz, vec3(0.272228986, 0.674081981, 0.0536894985));
			    u_xlat1.xyz = u_xlat16_2.xxx * u_xlat16_11.xyz + (-vec3(u_xlat16_21));
			    u_xlat16_0.y = float(0.0);
			    u_xlat16_18.y = float(0.0);
			    u_xlat16_28 = texture(_CurveHueVsSat, u_xlat16_0.xy, _GlobalMipBias.x).x;
			    u_xlat16_5 = texture(_CurveSatVsSat, u_xlat16_18.xy, _GlobalMipBias.x).x;
			    u_xlat5.x = u_xlat16_5;
			    u_xlat5.x = clamp(u_xlat5.x, 0.0, 1.0);
			    u_xlat28 = u_xlat16_28;
			    u_xlat28 = clamp(u_xlat28, 0.0, 1.0);
			    u_xlat28 = u_xlat28 + u_xlat28;
			    u_xlat28 = dot(u_xlat5.xx, vec2(u_xlat28));
			    u_xlat16_3.y = 0.0;
			    u_xlat16_5 = texture(_CurveLumVsSat, u_xlat16_3.xy, _GlobalMipBias.x).x;
			    u_xlat5.x = u_xlat16_5;
			    u_xlat5.x = clamp(u_xlat5.x, 0.0, 1.0);
			    u_xlat28 = u_xlat28 * u_xlat5.x;
			    u_xlat28 = dot(_HueSatCon.yy, vec2(u_xlat28));
			    u_xlat1.xyz = vec3(u_xlat28) * u_xlat1.xyz + vec3(u_xlat16_21);
			    u_xlat16_2.x = max(u_xlat1.y, u_xlat1.x);
			    u_xlat16_2.x = max(u_xlat1.z, u_xlat16_2.x);
			    u_xlat16_2.x = u_xlat16_2.x + 1.0;
			    u_xlat16_2.x = float(1.0) / float(u_xlat16_2.x);
			    u_xlat0.xyz = u_xlat1.xyz * u_xlat16_2.xxx + vec3(0.00390625, 0.00390625, 0.00390625);
			    u_xlat0.w = 0.0;
			    u_xlat16_1 = texture(_CurveMaster, u_xlat0.xw, _GlobalMipBias.x).x;
			    u_xlat1.x = u_xlat16_1;
			    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
			    u_xlat16_10.x = texture(_CurveMaster, u_xlat0.yw, _GlobalMipBias.x).x;
			    u_xlat16_10.y = texture(_CurveMaster, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlat1.yz = u_xlat16_10.xy;
			    u_xlat1.yz = clamp(u_xlat1.yz, 0.0, 1.0);
			    u_xlat0.xyz = u_xlat1.xyz + vec3(0.00390625, 0.00390625, 0.00390625);
			    u_xlat0.w = 0.0;
			    u_xlat16_1 = texture(_CurveRed, u_xlat0.xw, _GlobalMipBias.x).x;
			    u_xlat1.x = u_xlat16_1;
			    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
			    u_xlat16_10.x = texture(_CurveGreen, u_xlat0.yw, _GlobalMipBias.x).x;
			    u_xlat16_10.y = texture(_CurveBlue, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlat1.yz = u_xlat16_10.xy;
			    u_xlat1.yz = clamp(u_xlat1.yz, 0.0, 1.0);
			    u_xlat16_2.x = max(u_xlat1.y, u_xlat1.x);
			    u_xlat16_2.x = max(u_xlat1.z, u_xlat16_2.x);
			    u_xlat16_2.x = (-u_xlat16_2.x) + 1.0;
			    u_xlat16_2.x = float(1.0) / float(u_xlat16_2.x);
			    u_xlat16_2.xyz = u_xlat1.xyz * u_xlat16_2.xxx;
			    u_xlat1.xyz = max(u_xlat16_2.xyz, vec3(0.0, 0.0, 0.0));
			    u_xlat14.x = dot(vec3(0.695452213, 0.140678704, 0.163869068), u_xlat1.xyz);
			    u_xlat14.y = dot(vec3(0.0447945632, 0.859671116, 0.0955343172), u_xlat1.xyz);
			    u_xlat14.z = dot(vec3(-0.00552588282, 0.00402521016, 1.00150073), u_xlat1.xyz);
			    u_xlat16_2.xyz = (-u_xlat14.yxz) + u_xlat14.zyx;
			    u_xlat16_2.xy = u_xlat16_2.xy * u_xlat14.zy;
			    u_xlat16_2.x = u_xlat16_2.y + u_xlat16_2.x;
			    u_xlat16_2.x = u_xlat14.x * u_xlat16_2.z + u_xlat16_2.x;
			    u_xlat1.x = max(u_xlat16_2.x, 0.0);
			    u_xlat16_2.x = sqrt(u_xlat1.x);
			    u_xlat16_11.x = u_xlat14.y + u_xlat14.z;
			    u_xlat16_11.x = u_xlat14.x + u_xlat16_11.x;
			    u_xlat16_2.x = u_xlat16_2.x * 1.75 + u_xlat16_11.x;
			    u_xlat16_11.x = u_xlat16_2.x * 0.333333343;
			    u_xlat16_11.x = 0.0799999982 / u_xlat16_11.x;
			    u_xlat16_11.x = u_xlat16_11.x + -0.5;
			    u_xlat16_20 = min(u_xlat14.y, u_xlat14.x);
			    u_xlat16_20 = min(u_xlat14.z, u_xlat16_20);
			    u_xlat16_20 = max(u_xlat16_20, 9.99999975e-05);
			    u_xlat16_29 = max(u_xlat14.y, u_xlat14.x);
			    u_xlat16_29 = max(u_xlat14.z, u_xlat16_29);
			    u_xlat16_3.xy = max(vec2(u_xlat16_29), vec2(9.99999975e-05, 0.00999999978));
			    u_xlat16_20 = (-u_xlat16_20) + u_xlat16_3.x;
			    u_xlat16_20 = u_xlat16_20 / u_xlat16_3.y;
			    u_xlat1.x = u_xlat16_20 + -0.400000006;
			    u_xlat16_29 = u_xlat1.x * 2.5;
			    u_xlatb1.x = u_xlat1.x>=0.0;
			    u_xlat1.x = (u_xlatb1.x) ? 1.0 : -1.0;
			    u_xlat16_29 = -abs(u_xlat16_29) + 1.0;
			    u_xlat16_29 = max(u_xlat16_29, 0.0);
			    u_xlat16_29 = (-u_xlat16_29) * u_xlat16_29 + 1.0;
			    u_xlat1.x = u_xlat1.x * u_xlat16_29 + 1.0;
			    u_xlat1.x = u_xlat1.x * 0.0250000004;
			    u_xlat16_11.x = u_xlat16_11.x * u_xlat1.x;
			    u_xlatb10.x = u_xlat16_2.x>=0.479999989;
			    u_xlatb19 = 0.159999996>=u_xlat16_2.x;
			    u_xlat16_2.x = (u_xlatb10.x) ? 0.0 : u_xlat16_11.x;
			    u_xlat16_2.x = (u_xlatb19) ? u_xlat1.x : u_xlat16_2.x;
			    u_xlat16_2.x = u_xlat16_2.x + 1.0;
			    u_xlat0.yzw = u_xlat16_2.xxx * u_xlat14.xyz;
			    u_xlat1.x = (-u_xlat14.x) * u_xlat16_2.x + 0.0299999993;
			    u_xlat16_11.x = u_xlat14.y * u_xlat16_2.x + (-u_xlat0.w);
			    u_xlat16_11.x = u_xlat16_11.x * 1.73205078;
			    u_xlat16_29 = u_xlat0.y * 2.0 + (-u_xlat0.z);
			    u_xlat16_2.x = (-u_xlat14.z) * u_xlat16_2.x + u_xlat16_29;
			    u_xlat16_29 = max(abs(u_xlat16_2.x), abs(u_xlat16_11.x));
			    u_xlat16_29 = float(1.0) / u_xlat16_29;
			    u_xlat16_3.x = min(abs(u_xlat16_2.x), abs(u_xlat16_11.x));
			    u_xlat16_29 = u_xlat16_29 * u_xlat16_3.x;
			    u_xlat16_3.x = u_xlat16_29 * u_xlat16_29;
			    u_xlat10.x = u_xlat16_3.x * 0.0208350997 + -0.0851330012;
			    u_xlat10.x = u_xlat16_3.x * u_xlat10.x + 0.180141002;
			    u_xlat10.x = u_xlat16_3.x * u_xlat10.x + -0.330299497;
			    u_xlat10.x = u_xlat16_3.x * u_xlat10.x + 0.999866009;
			    u_xlat19 = u_xlat10.x * u_xlat16_29;
			    u_xlat19 = u_xlat19 * -2.0 + 1.57079637;
			    u_xlatb28 = abs(u_xlat16_2.x)<abs(u_xlat16_11.x);
			    u_xlat19 = u_xlatb28 ? u_xlat19 : float(0.0);
			    u_xlat10.x = u_xlat16_29 * u_xlat10.x + u_xlat19;
			    u_xlatb19 = u_xlat16_2.x<(-u_xlat16_2.x);
			    u_xlat19 = u_xlatb19 ? -3.14159274 : float(0.0);
			    u_xlat10.x = u_xlat19 + u_xlat10.x;
			    u_xlat16_29 = min(u_xlat16_2.x, u_xlat16_11.x);
			    u_xlat16_2.x = max(u_xlat16_2.x, u_xlat16_11.x);
			    u_xlatb19 = u_xlat16_2.x>=(-u_xlat16_2.x);
			    u_xlatb28 = u_xlat16_29<(-u_xlat16_29);
			    u_xlatb19 = u_xlatb19 && u_xlatb28;
			    u_xlat10.x = (u_xlatb19) ? (-u_xlat10.x) : u_xlat10.x;
			    u_xlat16_2.x = u_xlat10.x * 57.2957802;
			    u_xlatb10.xy = equal(u_xlat0.zwzz, u_xlat0.yzyy).xy;
			    u_xlatb10.x = u_xlatb10.y && u_xlatb10.x;
			    u_xlat16_2.x = (u_xlatb10.x) ? 0.0 : u_xlat16_2.x;
			    u_xlatb10.x = u_xlat16_2.x<0.0;
			    u_xlat16_11.x = u_xlat16_2.x + 360.0;
			    u_xlat16_2.x = (u_xlatb10.x) ? u_xlat16_11.x : u_xlat16_2.x;
			    u_xlatb10.x = 180.0<u_xlat16_2.x;
			    u_xlat16_11.xz = u_xlat16_2.xx + vec2(360.0, -360.0);
			    u_xlat16_29 = (u_xlatb10.x) ? u_xlat16_11.z : u_xlat16_2.x;
			    u_xlatb10.x = u_xlat16_2.x<-180.0;
			    u_xlat16_2.x = (u_xlatb10.x) ? u_xlat16_11.x : u_xlat16_29;
			    u_xlat10.x = u_xlat16_2.x * 0.0148148146;
			    u_xlat10.x = -abs(u_xlat10.x) + 1.0;
			    u_xlat10.x = max(u_xlat10.x, 0.0);
			    u_xlat19 = u_xlat10.x * -2.0 + 3.0;
			    u_xlat10.x = u_xlat10.x * u_xlat10.x;
			    u_xlat10.x = u_xlat10.x * u_xlat19;
			    u_xlat10.x = u_xlat10.x * u_xlat10.x;
			    u_xlat10.x = u_xlat16_20 * u_xlat10.x;
			    u_xlat1.x = u_xlat1.x * u_xlat10.x;
			    u_xlat0.x = u_xlat1.x * 0.180000007 + u_xlat0.y;
			    u_xlat1.x = dot(vec3(1.45143926, -0.236510754, -0.214928567), u_xlat0.xzw);
			    u_xlat1.y = dot(vec3(-0.0765537769, 1.17622972, -0.0996759236), u_xlat0.xzw);
			    u_xlat1.z = dot(vec3(0.00831614807, -0.00603244966, 0.997716308), u_xlat0.xzw);
			    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.0, 0.0, 0.0));
			    u_xlat28 = dot(u_xlat1.xyz, vec3(0.272228986, 0.674081981, 0.0536894985));
			    u_xlat1.xyz = (-vec3(u_xlat28)) + u_xlat1.xyz;
			    u_xlat1.xyz = u_xlat1.xyz * vec3(0.959999979, 0.959999979, 0.959999979) + vec3(u_xlat28);
			    u_xlat5.xyz = u_xlat1.xyz + vec3(0.0245785993, 0.0245785993, 0.0245785993);
			    u_xlat5.xyz = u_xlat1.xyz * u_xlat5.xyz + vec3(-9.05370034e-05, -9.05370034e-05, -9.05370034e-05);
			    u_xlat6.xyz = u_xlat1.xyz * vec3(0.983729005, 0.983729005, 0.983729005) + vec3(0.432951003, 0.432951003, 0.432951003);
			    u_xlat1.xyz = u_xlat1.xyz * u_xlat6.xyz + vec3(0.238080993, 0.238080993, 0.238080993);
			    u_xlat1.xyz = u_xlat5.xyz / u_xlat1.xyz;
			    u_xlat5.z = dot(vec3(-0.00557464967, 0.0040607336, 1.01033914), u_xlat1.xyz);
			    u_xlat5.x = dot(vec3(0.662454188, 0.134004205, 0.156187683), u_xlat1.xyz);
			    u_xlat5.y = dot(vec3(0.272228718, 0.674081743, 0.0536895171), u_xlat1.xyz);
			    u_xlat16_2.x = dot(u_xlat5.xyz, vec3(1.0, 1.0, 1.0));
			    u_xlat16_2.x = max(u_xlat16_2.x, 9.99999975e-05);
			    u_xlat16_2.xy = u_xlat5.xy / u_xlat16_2.xx;
			    u_xlat16_29 = max(u_xlat5.y, 0.0);
			    u_xlat16_29 = min(u_xlat16_29, 65504.0);
			    u_xlat16_29 = log2(u_xlat16_29);
			    u_xlat16_29 = u_xlat16_29 * 0.981100023;
			    u_xlat16_3.y = exp2(u_xlat16_29);
			    u_xlat16_29 = (-u_xlat16_2.x) + 1.0;
			    u_xlat16_2.z = (-u_xlat16_2.y) + u_xlat16_29;
			    u_xlat16_11.x = max(u_xlat16_2.y, 9.99999975e-05);
			    u_xlat16_11.x = u_xlat16_3.y / u_xlat16_11.x;
			    u_xlat16_3.xz = u_xlat16_11.xx * u_xlat16_2.xz;
			    u_xlat16_2.x = dot(vec3(1.6410234, -0.324803293, -0.236424699), u_xlat16_3.xyz);
			    u_xlat16_2.y = dot(vec3(-0.663662851, 1.61533165, 0.0167563483), u_xlat16_3.xyz);
			    u_xlat16_2.z = dot(vec3(0.0117218941, -0.00828444213, 0.988394856), u_xlat16_3.xyz);
			    u_xlat1.x = dot(u_xlat16_2.xyz, vec3(0.272228986, 0.674081981, 0.0536894985));
			    u_xlat10.xyz = (-u_xlat1.xxx) + u_xlat16_2.xyz;
			    u_xlat1.xyz = u_xlat10.xyz * vec3(0.930000007, 0.930000007, 0.930000007) + u_xlat1.xxx;
			    u_xlat5.x = dot(vec3(0.662454188, 0.134004205, 0.156187683), u_xlat1.xyz);
			    u_xlat5.y = dot(vec3(0.272228718, 0.674081743, 0.0536895171), u_xlat1.xyz);
			    u_xlat5.z = dot(vec3(-0.00557464967, 0.0040607336, 1.01033914), u_xlat1.xyz);
			    u_xlat1.x = dot(vec3(0.987223983, -0.00611326983, 0.0159533005), u_xlat5.xyz);
			    u_xlat1.y = dot(vec3(-0.00759836007, 1.00186002, 0.00533019984), u_xlat5.xyz);
			    u_xlat1.z = dot(vec3(0.00307257008, -0.00509594986, 1.08168006), u_xlat5.xyz);
			    SV_Target0.x = dot(vec3(3.2409699, -1.5373832, -0.498610765), u_xlat1.xyz);
			    SV_Target0.y = dot(vec3(-0.969243646, 1.8759675, 0.0415550582), u_xlat1.xyz);
			    SV_Target0.z = dot(vec3(0.0556300804, -0.203976959, 1.05697155), u_xlat1.xyz);
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
			uniform 	vec4 _Lut_Params;
			uniform 	vec4 _ColorBalance;
			uniform 	vec4 _ColorFilter;
			uniform 	vec4 _ChannelMixerRed;
			uniform 	vec4 _ChannelMixerGreen;
			uniform 	vec4 _ChannelMixerBlue;
			uniform 	vec4 _HueSatCon;
			uniform 	vec4 _Lift;
			uniform 	vec4 _Gamma;
			uniform 	vec4 _Gain;
			uniform 	vec4 _Shadows;
			uniform 	vec4 _Midtones;
			uniform 	vec4 _Highlights;
			uniform 	vec4 _ShaHiLimits;
			uniform 	vec4 _SplitShadows;
			uniform 	vec4 _SplitHighlights;
			UNITY_LOCATION(0) uniform mediump sampler2D _CurveMaster;
			UNITY_LOCATION(1) uniform mediump sampler2D _CurveRed;
			UNITY_LOCATION(2) uniform mediump sampler2D _CurveGreen;
			UNITY_LOCATION(3) uniform mediump sampler2D _CurveBlue;
			UNITY_LOCATION(4) uniform mediump sampler2D _CurveHueVsHue;
			UNITY_LOCATION(5) uniform mediump sampler2D _CurveHueVsSat;
			UNITY_LOCATION(6) uniform mediump sampler2D _CurveSatVsSat;
			UNITY_LOCATION(7) uniform mediump sampler2D _CurveLumVsSat;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out highp vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec2 u_xlat16_0;
			bool u_xlatb0;
			vec3 u_xlat1;
			mediump float u_xlat16_1;
			ivec3 u_xlati1;
			bool u_xlatb1;
			vec3 u_xlat2;
			mediump float u_xlat16_2;
			ivec3 u_xlati2;
			vec3 u_xlat3;
			mediump vec4 u_xlat16_3;
			mediump vec4 u_xlat16_4;
			vec3 u_xlat5;
			mediump vec4 u_xlat16_5;
			bvec3 u_xlatb5;
			vec3 u_xlat6;
			mediump vec3 u_xlat16_7;
			mediump vec3 u_xlat16_8;
			float u_xlat10;
			mediump vec2 u_xlat16_10;
			bool u_xlatb10;
			vec3 u_xlat11;
			mediump vec3 u_xlat16_13;
			mediump vec2 u_xlat16_18;
			vec2 u_xlat20;
			mediump float u_xlat16_22;
			mediump float u_xlat16_25;
			float u_xlat27;
			bool u_xlatb27;
			float u_xlat28;
			mediump float u_xlat16_28;
			mediump float u_xlat16_31;
			void main()
			{
			    u_xlat0.x = vs_TEXCOORD0.x * _Lut_Params.x;
			    u_xlat0.x = floor(u_xlat0.x);
			    u_xlat1.x = vs_TEXCOORD0.x * _Lut_Params.x + (-u_xlat0.x);
			    u_xlat0.x = u_xlat0.x * _Lut_Params.z;
			    u_xlat0.z = u_xlat0.x * _Lut_Params.w;
			    u_xlat1.y = vs_TEXCOORD0.y;
			    u_xlat0.xy = u_xlat1.xy + (-_Lut_Params.zz);
			    u_xlat1.x = _Lut_Params.w;
			    u_xlat1.z = 2.0;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat1.xxz + vec3(-0.386036009, -0.386036009, -0.386036009);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(13.6054821, 13.6054821, 13.6054821);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.0479959995, -0.0479959995, -0.0479959995);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.179999992, 0.179999992, 0.179999992);
			    u_xlat1.x = dot(vec3(0.390404999, 0.549941003, 0.00892631989), u_xlat0.xyz);
			    u_xlat1.y = dot(vec3(0.070841603, 0.963172019, 0.00135775004), u_xlat0.xyz);
			    u_xlat1.z = dot(vec3(0.0231081992, 0.128021002, 0.936245024), u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat1.xyz * _ColorBalance.xyz;
			    u_xlat1.x = dot(vec3(2.85846996, -1.62879002, -0.0248910002), u_xlat0.xyz);
			    u_xlat1.y = dot(vec3(-0.210181996, 1.15820003, 0.000324280991), u_xlat0.xyz);
			    u_xlat1.z = dot(vec3(-0.0418119989, -0.118169002, 1.06867003), u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat1.xyz * vec3(5.55555582, 5.55555582, 5.55555582) + vec3(0.0479959995, 0.0479959995, 0.0479959995);
			    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
			    u_xlat0.xyz = log2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.0734997839, 0.0734997839, 0.0734997839) + vec3(-0.0275523961, -0.0275523961, -0.0275523961);
			    u_xlat0.xyz = u_xlat0.xyz * _HueSatCon.zzz + vec3(0.0275523961, 0.0275523961, 0.0275523961);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(13.6054821, 13.6054821, 13.6054821);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz + vec3(-0.0479959995, -0.0479959995, -0.0479959995);
			    u_xlat0.xyz = u_xlat0.xyz * _ColorFilter.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.179999992, 0.179999992, 0.179999992);
			    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
			    u_xlat0.xyz = log2(u_xlat0.xyz);
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.454545468, 0.454545468, 0.454545468);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat1.xyz = u_xlat0.xyz + u_xlat0.xyz;
			    u_xlat2.xyz = u_xlat0.xyz * u_xlat0.xyz;
			    u_xlat3.xyz = min(u_xlat0.xyz, vec3(1.0, 1.0, 1.0));
			    u_xlat0.xyz = sqrt(u_xlat0.xyz);
			    u_xlat16_4.x = dot(u_xlat3.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat27 = u_xlat16_4.x + _SplitShadows.w;
			    u_xlat27 = clamp(u_xlat27, 0.0, 1.0);
			    u_xlat28 = (-u_xlat27) + 1.0;
			    u_xlat3.xyz = _SplitShadows.xyz + vec3(-0.5, -0.5, -0.5);
			    u_xlat3.xyz = vec3(u_xlat28) * u_xlat3.xyz + vec3(0.5, 0.5, 0.5);
			    u_xlat5.xyz = (-u_xlat3.xyz) * vec3(2.0, 2.0, 2.0) + vec3(1.0, 1.0, 1.0);
			    u_xlat2.xyz = u_xlat2.xyz * u_xlat5.xyz;
			    u_xlat2.xyz = u_xlat1.xyz * u_xlat3.xyz + u_xlat2.xyz;
			    u_xlatb5.xyz = greaterThanEqual(u_xlat3.xyzx, vec4(0.5, 0.5, 0.5, 0.0)).xyz;
			    u_xlat6.x = (u_xlatb5.x) ? float(0.0) : float(1.0);
			    u_xlat6.y = (u_xlatb5.y) ? float(0.0) : float(1.0);
			    u_xlat6.z = (u_xlatb5.z) ? float(0.0) : float(1.0);
			    u_xlat5.x = u_xlatb5.x ? float(1.0) : 0.0;
			    u_xlat5.y = u_xlatb5.y ? float(1.0) : 0.0;
			    u_xlat5.z = u_xlatb5.z ? float(1.0) : 0.0;
			;
			    u_xlat2.xyz = u_xlat2.xyz * u_xlat6.xyz;
			    u_xlat6.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
			    u_xlat1.xyz = u_xlat1.xyz * u_xlat6.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.xyz + u_xlat1.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat5.xyz + u_xlat2.xyz;
			    u_xlat1.xyz = u_xlat0.xyz + u_xlat0.xyz;
			    u_xlat2.xyz = u_xlat0.xyz * u_xlat0.xyz;
			    u_xlat0.xyz = sqrt(u_xlat0.xyz);
			    u_xlat3.xyz = _SplitHighlights.xyz + vec3(-0.5, -0.5, -0.5);
			    u_xlat3.xyz = vec3(u_xlat27) * u_xlat3.xyz + vec3(0.5, 0.5, 0.5);
			    u_xlat5.xyz = (-u_xlat3.xyz) * vec3(2.0, 2.0, 2.0) + vec3(1.0, 1.0, 1.0);
			    u_xlat2.xyz = u_xlat2.xyz * u_xlat5.xyz;
			    u_xlat2.xyz = u_xlat1.xyz * u_xlat3.xyz + u_xlat2.xyz;
			    u_xlatb5.xyz = greaterThanEqual(u_xlat3.xyzx, vec4(0.5, 0.5, 0.5, 0.0)).xyz;
			    u_xlat6.x = (u_xlatb5.x) ? float(0.0) : float(1.0);
			    u_xlat6.y = (u_xlatb5.y) ? float(0.0) : float(1.0);
			    u_xlat6.z = (u_xlatb5.z) ? float(0.0) : float(1.0);
			    u_xlat5.x = u_xlatb5.x ? float(1.0) : 0.0;
			    u_xlat5.y = u_xlatb5.y ? float(1.0) : 0.0;
			    u_xlat5.z = u_xlatb5.z ? float(1.0) : 0.0;
			;
			    u_xlat2.xyz = u_xlat2.xyz * u_xlat6.xyz;
			    u_xlat6.xyz = (-u_xlat3.xyz) + vec3(1.0, 1.0, 1.0);
			    u_xlat3.xyz = u_xlat3.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
			    u_xlat1.xyz = u_xlat1.xyz * u_xlat6.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat3.xyz + u_xlat1.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat5.xyz + u_xlat2.xyz;
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * vec3(2.20000005, 2.20000005, 2.20000005);
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlat1.x = dot(u_xlat0.xyz, _ChannelMixerRed.xyz);
			    u_xlat1.y = dot(u_xlat0.xyz, _ChannelMixerGreen.xyz);
			    u_xlat1.z = dot(u_xlat0.xyz, _ChannelMixerBlue.xyz);
			    u_xlat0.xyz = u_xlat1.xyz * _Midtones.xyz;
			    u_xlat16_4.x = dot(u_xlat1.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat2.xy = u_xlat16_4.xx + (-_ShaHiLimits.xz);
			    u_xlat20.xy = (-_ShaHiLimits.xz) + _ShaHiLimits.yw;
			    u_xlat20.xy = vec2(1.0, 1.0) / u_xlat20.xy;
			    u_xlat2.xy = u_xlat20.xy * u_xlat2.xy;
			    u_xlat2.xy = clamp(u_xlat2.xy, 0.0, 1.0);
			    u_xlat20.xy = u_xlat2.xy * vec2(-2.0, -2.0) + vec2(3.0, 3.0);
			    u_xlat2.xy = u_xlat2.xy * u_xlat2.xy;
			    u_xlat27 = (-u_xlat20.x) * u_xlat2.x + 1.0;
			    u_xlat28 = (-u_xlat27) + 1.0;
			    u_xlat28 = (-u_xlat20.y) * u_xlat2.y + u_xlat28;
			    u_xlat2.x = u_xlat2.y * u_xlat20.y;
			    u_xlat0.xyz = u_xlat0.xyz * vec3(u_xlat28);
			    u_xlat11.xyz = u_xlat1.xyz * _Shadows.xyz;
			    u_xlat1.xyz = u_xlat1.xyz * _Highlights.xyz;
			    u_xlat0.xyz = u_xlat11.xyz * vec3(u_xlat27) + u_xlat0.xyz;
			    u_xlat0.xyz = u_xlat1.xyz * u_xlat2.xxx + u_xlat0.xyz;
			    u_xlat0.xyz = u_xlat0.xyz * _Gain.xyz + _Lift.xyz;
			    u_xlati1.xyz = ivec3(uvec3(lessThan(vec4(0.0, 0.0, 0.0, 0.0), u_xlat0.xyzx).xyz) * 0xFFFFFFFFu);
			    u_xlati2.xyz = ivec3(uvec3(lessThan(u_xlat0.xyzx, vec4(0.0, 0.0, 0.0, 0.0)).xyz) * 0xFFFFFFFFu);
			    u_xlat0.xyz = log2(abs(u_xlat0.xyz));
			    u_xlat0.xyz = u_xlat0.xyz * _Gamma.xyz;
			    u_xlat0.xyz = exp2(u_xlat0.xyz);
			    u_xlati1.xyz = (-u_xlati1.xyz) + u_xlati2.xyz;
			    u_xlat1.xyz = vec3(u_xlati1.xyz);
			    u_xlat2.xyz = u_xlat0.xyz * u_xlat1.xyz;
			    u_xlat16_4.xy = u_xlat1.yz * u_xlat0.yz + (-u_xlat2.zy);
			    u_xlatb27 = u_xlat2.y>=u_xlat2.z;
			    u_xlat16_22 = (u_xlatb27) ? 1.0 : 0.0;
			    u_xlat16_4.xy = u_xlat16_4.xy * vec2(u_xlat16_22);
			    u_xlat16_3.xy = u_xlat1.zy * u_xlat0.zy + u_xlat16_4.xy;
			    u_xlat16_5.w = (-u_xlat2.x);
			    u_xlat16_4.x = float(1.0);
			    u_xlat16_4.y = float(-1.0);
			    u_xlat16_3.zw = vec2(u_xlat16_22) * u_xlat16_4.xy + vec2(-1.0, 0.666666687);
			    u_xlat16_5.xyz = (-u_xlat16_3.xyw);
			    u_xlat16_4.yzw = u_xlat16_3.yzx + u_xlat16_5.yzw;
			    u_xlat16_4.x = u_xlat1.x * u_xlat0.x + u_xlat16_5.x;
			    u_xlatb0 = u_xlat2.x>=u_xlat16_3.x;
			    u_xlat16_7.x = (u_xlatb0) ? 1.0 : 0.0;
			    u_xlat16_31 = u_xlat16_7.x * u_xlat16_4.w + u_xlat2.x;
			    u_xlat16_4.xyz = u_xlat16_7.xxx * u_xlat16_4.xyz + u_xlat16_3.xyw;
			    u_xlat16_7.x = dot(u_xlat2.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat16_25 = min(u_xlat16_4.y, u_xlat16_31);
			    u_xlat16_13.x = (-u_xlat16_4.y) + u_xlat16_31;
			    u_xlat16_31 = u_xlat16_4.x + (-u_xlat16_25);
			    u_xlat16_25 = u_xlat16_31 * 6.0 + 9.99999975e-05;
			    u_xlat16_13.x = u_xlat16_13.x / u_xlat16_25;
			    u_xlat16_0.x = u_xlat16_13.x + u_xlat16_4.z;
			    u_xlat1.x = abs(u_xlat16_0.x) + _HueSatCon.x;
			    u_xlat16_0.x = abs(u_xlat16_0.x);
			    u_xlat1.y = 0.0;
			    u_xlat16_10.x = texture(_CurveHueVsHue, u_xlat1.xy, _GlobalMipBias.x).x;
			    u_xlat10 = u_xlat16_10.x;
			    u_xlat10 = clamp(u_xlat10, 0.0, 1.0);
			    u_xlat1.x = u_xlat1.x + u_xlat10;
			    u_xlat1.x = u_xlat1.x + -0.5;
			    u_xlatb10 = 1.0<u_xlat1.x;
			    u_xlat16_13.xy = u_xlat1.xx + vec2(1.0, -1.0);
			    u_xlat16_22 = (u_xlatb10) ? u_xlat16_13.y : u_xlat1.x;
			    u_xlatb1 = u_xlat1.x<0.0;
			    u_xlat16_13.x = (u_xlatb1) ? u_xlat16_13.x : u_xlat16_22;
			    u_xlat16_8.xyz = u_xlat16_13.xxx + vec3(1.0, 0.666666687, 0.333333343);
			    u_xlat16_8.xyz = fract(u_xlat16_8.xyz);
			    u_xlat16_8.xyz = u_xlat16_8.xyz * vec3(6.0, 6.0, 6.0) + vec3(-3.0, -3.0, -3.0);
			    u_xlat16_8.xyz = abs(u_xlat16_8.xyz) + vec3(-1.0, -1.0, -1.0);
			    u_xlat16_8.xyz = clamp(u_xlat16_8.xyz, 0.0, 1.0);
			    u_xlat16_8.xyz = u_xlat16_8.xyz + vec3(-1.0, -1.0, -1.0);
			    u_xlat16_13.x = u_xlat16_4.x + 9.99999975e-05;
			    u_xlat16_18.x = u_xlat16_31 / u_xlat16_13.x;
			    u_xlat16_13.xyz = u_xlat16_18.xxx * u_xlat16_8.xyz + vec3(1.0, 1.0, 1.0);
			    u_xlat16_8.xyz = u_xlat16_13.xyz * u_xlat16_4.xxx;
			    u_xlat16_25 = dot(u_xlat16_8.xyz, vec3(0.212672904, 0.715152204, 0.0721750036));
			    u_xlat1.xyz = u_xlat16_4.xxx * u_xlat16_13.xyz + (-vec3(u_xlat16_25));
			    u_xlat16_0.y = float(0.0);
			    u_xlat16_18.y = float(0.0);
			    u_xlat16_28 = texture(_CurveHueVsSat, u_xlat16_0.xy, _GlobalMipBias.x).x;
			    u_xlat16_2 = texture(_CurveSatVsSat, u_xlat16_18.xy, _GlobalMipBias.x).x;
			    u_xlat2.x = u_xlat16_2;
			    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			    u_xlat28 = u_xlat16_28;
			    u_xlat28 = clamp(u_xlat28, 0.0, 1.0);
			    u_xlat28 = u_xlat28 + u_xlat28;
			    u_xlat28 = dot(u_xlat2.xx, vec2(u_xlat28));
			    u_xlat16_7.y = 0.0;
			    u_xlat16_2 = texture(_CurveLumVsSat, u_xlat16_7.xy, _GlobalMipBias.x).x;
			    u_xlat2.x = u_xlat16_2;
			    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
			    u_xlat28 = u_xlat28 * u_xlat2.x;
			    u_xlat28 = dot(_HueSatCon.yy, vec2(u_xlat28));
			    u_xlat1.xyz = vec3(u_xlat28) * u_xlat1.xyz + vec3(u_xlat16_25);
			    u_xlat16_4.x = max(u_xlat1.y, u_xlat1.x);
			    u_xlat16_4.x = max(u_xlat1.z, u_xlat16_4.x);
			    u_xlat16_4.x = u_xlat16_4.x + 1.0;
			    u_xlat16_4.x = float(1.0) / float(u_xlat16_4.x);
			    u_xlat0.xyz = u_xlat1.xyz * u_xlat16_4.xxx + vec3(0.00390625, 0.00390625, 0.00390625);
			    u_xlat0.w = 0.0;
			    u_xlat16_1 = texture(_CurveMaster, u_xlat0.xw, _GlobalMipBias.x).x;
			    u_xlat1.x = u_xlat16_1;
			    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
			    u_xlat16_10.x = texture(_CurveMaster, u_xlat0.yw, _GlobalMipBias.x).x;
			    u_xlat16_10.y = texture(_CurveMaster, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlat1.yz = u_xlat16_10.xy;
			    u_xlat1.yz = clamp(u_xlat1.yz, 0.0, 1.0);
			    u_xlat0.xyz = u_xlat1.xyz + vec3(0.00390625, 0.00390625, 0.00390625);
			    u_xlat0.w = 0.0;
			    u_xlat16_1 = texture(_CurveRed, u_xlat0.xw, _GlobalMipBias.x).x;
			    u_xlat1.x = u_xlat16_1;
			    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
			    u_xlat16_10.x = texture(_CurveGreen, u_xlat0.yw, _GlobalMipBias.x).x;
			    u_xlat16_10.y = texture(_CurveBlue, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlat1.yz = u_xlat16_10.xy;
			    u_xlat1.yz = clamp(u_xlat1.yz, 0.0, 1.0);
			    u_xlat16_4.x = max(u_xlat1.y, u_xlat1.x);
			    u_xlat16_4.x = max(u_xlat1.z, u_xlat16_4.x);
			    u_xlat16_4.x = (-u_xlat16_4.x) + 1.0;
			    u_xlat16_4.x = float(1.0) / float(u_xlat16_4.x);
			    u_xlat16_4.xyz = u_xlat1.xyz * u_xlat16_4.xxx;
			    u_xlat1.xyz = max(u_xlat16_4.xyz, vec3(0.0, 0.0, 0.0));
			    u_xlat16_4.xyz = min(u_xlat1.xyz, vec3(435.187134, 435.187134, 435.187134));
			    u_xlat1.xyz = u_xlat16_4.xyz * vec3(0.262677222, 0.262677222, 0.262677222) + vec3(0.0695999935, 0.0695999935, 0.0695999935);
			    u_xlat16_7.xyz = u_xlat16_4.xyz * vec3(1.31338608, 1.31338608, 1.31338608);
			    u_xlat2.xyz = u_xlat16_4.xyz * vec3(0.262677222, 0.262677222, 0.262677222) + vec3(0.289999992, 0.289999992, 0.289999992);
			    u_xlat2.xyz = u_xlat16_7.xyz * u_xlat2.xyz + vec3(0.0816000104, 0.0816000104, 0.0816000104);
			    u_xlat1.xyz = u_xlat16_7.xyz * u_xlat1.xyz + vec3(0.00543999998, 0.00543999998, 0.00543999998);
			    u_xlat1.xyz = u_xlat1.xyz / u_xlat2.xyz;
			    u_xlat1.xyz = u_xlat1.xyz + vec3(-0.0666666627, -0.0666666627, -0.0666666627);
			    u_xlat16_4.xyz = u_xlat1.xyz * vec3(1.31338608, 1.31338608, 1.31338608);
			    SV_Target0.xyz = u_xlat16_4.xyz;
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}