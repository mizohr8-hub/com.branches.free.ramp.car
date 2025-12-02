Shader "Avari/DiffuseOpaqueColouredCubemapAdditive (Mobile Optimized)"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Color("Diffuse Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_ReflectionColor("Reflection Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_ReflectionMap("Reflection Map", Cube) = "" {}
		_ReflectionStrength("Reflection Strength", Range(0.0, 1.0)) = 0.5
		_LightExposure("Light Exposure", Range(0.0, 3.0)) = 1.8
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Geometry"
				"RenderType" = "Opaque"
			}

			Blend Off
			Cull Back
			ZWrite On

			CGPROGRAM
			#pragma surface SurfaceMain Lambert

			sampler2D _MainTex;
			fixed4 _Color;
			fixed4 _ReflectionColor;
			samplerCUBE _ReflectionMap;
			fixed _ReflectionStrength;
			fixed _LightExposure;

			struct Input
			{
				fixed2 uv_MainTex;
				fixed3 worldRefl;
			};

			void SurfaceMain(Input input, inout SurfaceOutput output)
			{
				fixed3 reflectionColor = texCUBE(_ReflectionMap, WorldReflectionVector(input, o.Normal)).rgb * _ReflectionColor.rgb;
				fixed3 finalColor = lerp(_Color, reflectionColor, _ReflectionStrength);

				output.Albedo = (finalColor * _ReflectionStrength) + (tex2D(_MainTex, input.uv_MainTex) * (_Color * _LightExposure));
				output.Alpha = _Color.a;
			}
			ENDCG
		}

			FallBack "Mobile/VertexLit"
}
