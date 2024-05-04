
Shader "Hidden/Masker"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MainTex2("Texture 2", 2D) = ""
		_MaskTex ("Mask Texture", 2D) = "white" {}
		_MaskValue ("Mask Value", Range(0,1)) = 0.5
		_MaskSpread("Blur edges", Range(0,1)) = 0.5
		[Toggle(INVERT_MASK)] _INVERT_MASK ("Mask Invert", Float) = 0
		[Toggle(NO_MASK)] _NO_MASK("Mask Invert", Float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag		
			#include "UnityCG.cginc"

			#pragma shader_feature INVERT_MASK
			#pragma shader_feature FADE_TO_COLOR
			#pragma shader_feature NO_MASK


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv     : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv     : TEXCOORD0;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

			

				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _MainTex2;
			sampler2D _MaskTex;
			float _MaskValue;
			float _MaskSpread;

			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);
				float4 col2 = tex2D(_MainTex2, i.uv);
				float4 mask = tex2D(_MaskTex, i.uv);

				// Scale 0..255 to 0..254 range.
				float alpha = mask.a * (1 - 1/255.0);

				// If the mask value is greater than the alpha value,
				// we want to draw the mask.

				float maskSpread = _MaskSpread - _MaskSpread * _MaskValue;								
				float weight = smoothstep(_MaskValue - maskSpread, _MaskValue, alpha);

			#if INVERT_MASK
				weight = 1 - weight;
			#endif

#if NO_MASK
				col.rgb = lerp(col.rgb, col2.rgb, _MaskValue);

#else
				col.rgb = lerp(col.rgb, lerp(col.rgb, col2.rgb, weight), 1);
#endif

				


				return col;
			}
			ENDCG
		}
	}
}
