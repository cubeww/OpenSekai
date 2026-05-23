Shader "Hidden/Universal Render Pipeline/LutBuilderLdr" {
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
			uniform 	mediump vec4 _ColorFilter;
			uniform 	mediump vec4 _ChannelMixerRed;
			uniform 	mediump vec4 _ChannelMixerGreen;
			uniform 	mediump vec4 _ChannelMixerBlue;
			uniform 	vec4 _HueSatCon;
			uniform 	vec4 _Lift;
			uniform 	vec4 _Gamma;
			uniform 	vec4 _Gain;
			uniform 	vec4 _Shadows;
			uniform 	vec4 _Midtones;
			uniform 	vec4 _Highlights;
			uniform 	vec4 _ShaHiLimits;
			uniform 	mediump vec4 _SplitShadows;
			uniform 	mediump vec4 _SplitHighlights;
			UNITY_LOCATION(0) uniform mediump sampler2D _CurveMaster;
			UNITY_LOCATION(1) uniform mediump sampler2D _CurveRed;
			UNITY_LOCATION(2) uniform mediump sampler2D _CurveGreen;
			UNITY_LOCATION(3) uniform mediump sampler2D _CurveBlue;
			UNITY_LOCATION(4) uniform mediump sampler2D _CurveHueVsHue;
			UNITY_LOCATION(5) uniform mediump sampler2D _CurveHueVsSat;
			UNITY_LOCATION(6) uniform mediump sampler2D _CurveSatVsSat;
			UNITY_LOCATION(7) uniform mediump sampler2D _CurveLumVsSat;
			in highp vec2 vs_TEXCOORD0;
			layout(location = 0) out mediump vec4 SV_Target0;
			vec4 u_xlat0;
			mediump vec2 u_xlat16_0;
			bool u_xlatb0;
			vec3 u_xlat1;
			mediump vec2 u_xlat16_1;
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
			mediump float u_xlat16_10;
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
			    u_xlat0.xyz = u_xlat0.xyz * u_xlat1.xxz;
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
			    u_xlat0.xyz = u_xlat0.xyz * vec3(0.179999992, 0.179999992, 0.179999992);
			    u_xlat0.xyz = u_xlat0.xyz * _ColorFilter.xyz;
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
			    u_xlat16_10 = texture(_CurveHueVsHue, u_xlat1.xy, _GlobalMipBias.x).x;
			    u_xlat16_13.x = u_xlat16_10;
			    u_xlat16_13.x = clamp(u_xlat16_13.x, 0.0, 1.0);
			    u_xlat16_13.x = u_xlat16_13.x + -0.5;
			    u_xlat1.x = u_xlat1.x + u_xlat16_13.x;
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
			    u_xlat16_4.x = u_xlat16_2;
			    u_xlat16_4.x = clamp(u_xlat16_4.x, 0.0, 1.0);
			    u_xlat16_13.x = u_xlat16_28;
			    u_xlat16_13.x = clamp(u_xlat16_13.x, 0.0, 1.0);
			    u_xlat16_13.x = u_xlat16_13.x + u_xlat16_13.x;
			    u_xlat28 = dot(u_xlat16_4.xx, u_xlat16_13.xx);
			    u_xlat16_7.y = 0.0;
			    u_xlat16_2 = texture(_CurveLumVsSat, u_xlat16_7.xy, _GlobalMipBias.x).x;
			    u_xlat16_4.x = u_xlat16_2;
			    u_xlat16_4.x = clamp(u_xlat16_4.x, 0.0, 1.0);
			    u_xlat16_4.x = u_xlat16_4.x + u_xlat16_4.x;
			    u_xlat28 = u_xlat28 * u_xlat16_4.x;
			    u_xlat28 = u_xlat28 * _HueSatCon.y;
			    u_xlat1.xyz = vec3(u_xlat28) * u_xlat1.xyz + vec3(u_xlat16_25);
			    u_xlat0.xyz = u_xlat1.xyz + vec3(0.00390625, 0.00390625, 0.00390625);
			    u_xlat0.w = 0.0;
			    u_xlat16_1.x = texture(_CurveMaster, u_xlat0.xw, _GlobalMipBias.x).x;
			    u_xlat16_4.x = u_xlat16_1.x;
			    u_xlat16_4.x = clamp(u_xlat16_4.x, 0.0, 1.0);
			    u_xlat16_1.x = texture(_CurveMaster, u_xlat0.yw, _GlobalMipBias.x).x;
			    u_xlat16_1.y = texture(_CurveMaster, u_xlat0.zw, _GlobalMipBias.x).x;
			    u_xlat16_4.yz = u_xlat16_1.xy;
			    u_xlat16_4.yz = clamp(u_xlat16_4.yz, 0.0, 1.0);
			    u_xlat0.xyz = u_xlat16_4.xyz + vec3(0.00390625, 0.00390625, 0.00390625);
			    u_xlat0.w = 0.0;
			    u_xlat16_1.x = texture(_CurveRed, u_xlat0.xw, _GlobalMipBias.x).x;
			    SV_Target0.x = u_xlat16_1.x;
			    SV_Target0.x = clamp(SV_Target0.x, 0.0, 1.0);
			    u_xlat16_1.x = texture(_CurveGreen, u_xlat0.yw, _GlobalMipBias.x).x;
			    u_xlat16_1.y = texture(_CurveBlue, u_xlat0.zw, _GlobalMipBias.x).x;
			    SV_Target0.yz = u_xlat16_1.xy;
			    SV_Target0.yz = clamp(SV_Target0.yz, 0.0, 1.0);
			    SV_Target0.w = 1.0;
			    return;
			}

			#endif
			ENDGLSL
		}
	}
}