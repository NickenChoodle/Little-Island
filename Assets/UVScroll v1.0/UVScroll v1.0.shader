Shader "Cdrsan/UV Scroll" {
	
	Properties {
		_Color0 ("Tint", Color) = (1,1,1,1)
		_Texture ("Main Texture", 2D) = "grey" {}
		_Color1 ("Scrolling Texture Tint", Color) = (1,1,1,1)
		_ScrollingTexture ("Scrolling Texture", 2D) = "grey" {}
		_Blend ("Blending", Range(0,1)) = .5
		_Alpha ("Transparency", Range(0,1)) = 1
		_ScrollX ("Scroll Speed X", int) = 0
		_ScrollY ("Scroll Speed Y", int) = 0
		_Emission ("Emission Power", Range(0,2)) = 1
	}
	
	Subshader {
		Tags {"Queue" = "Transparent"}
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				sampler2D _Texture, _ScrollingTexture;
				float4 _Texture_ST, _ScrollingTexture_ST, _Color0, _Color1;
				float  _ScrollX, _ScrollY, _Blend, _Alpha, _Emission;
				
				struct input {
					float4 pos	: POSITION;
					float2 uv0	: TEXCOORD0;
					float2 uv1	: TEXCOORD1;
				};
				
				struct output {
					float4 pos 	: SV_POSITION;
					float2 uv0	: TEXCOORD0;
					float2 uv1	: TEXCOORD1;
				};
				
				output vert (input v) {
					output o;
					o.uv0 = TRANSFORM_TEX(v.uv0, _Texture);
					o.uv1 = TRANSFORM_TEX(v.uv1, _ScrollingTexture);
					o.pos = UnityObjectToClipPos(v.pos);
					return o;
				};
				
				float4 frag (input i) : SV_TARGET {
					float2 timeScale = float2(_Time.y * _ScrollX, _Time.y * _ScrollY);
					float4 mainTex = tex2D(_Texture, i.uv0);
					float4 scrollingTex = tex2D(_ScrollingTexture, i.uv1 + timeScale);
					mainTex *= _Color0;
					scrollingTex *= _Color1;
					float4 color = float4(lerp(scrollingTex.xyz, mainTex.xyz, 1 - _Blend), 1);
					color *= float4(_Emission, _Emission, _Emission, _Alpha);
					return color;
				};
				
			ENDCG
		}
	}
	CustomEditor "UVScroll"
	Fallback "Standard"
}