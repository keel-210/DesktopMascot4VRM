Shader "DesktopMascotMaker/MascotMakerShaderChromakey"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Chroma Key Color", Color) = (1,0,0,0)
		_Amount ("Chroma Range", Range(0.005, 1.0)) = 0.005
	}

	SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest 
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				half4 _Color;
				half _Amount;

				fixed4 frag(v2f_img i):COLOR
				{
					half4 input = tex2D(_MainTex, i.uv);

					//half3 output = (input.rrr * half3(1,0,0))
					//			 + (input.ggg * half3(0,1,0))
					//			 + (input.bbb * half3(0,0,1));
					
					fixed a = 255;
					half rr = (half)input.rrr;
					half gg = (half)input.ggg;
					half bb = (half)input.bbb;

					if (abs(rr - _Color.r) < _Amount)
					{
					    if (abs(gg - _Color.g) < _Amount)
					    {
					        if (abs(bb - _Color.b) < _Amount)
					        {
					            a = 0;
					        }
					    }
					}

					//return fixed4(output, a);
					return fixed4(rr, gg, bb, a);
				}

			ENDCG
		}
	}
	FallBack off
}
