﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Inverted Mask"
{
	Properties
	{
		_MainTex("Sprite Texture",2D) = "white"{}
		_Color("Color",Color) = (1,1,1,1)
		_Alpha("Alpha",Range(0,1)) = 1
	}
		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass{

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		sampler2D _MainTex;
	float _Alpha;
	float4 _Color;

	struct v2f {
		float4  pos : SV_POSITION;
		float2  uv : TEXCOORD0;
	};

	float4 _MainTex_ST;

	v2f vert(appdata_base v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

	half4 frag(v2f i) : COLOR
	{
		half4 texcol = tex2D(_MainTex, i.uv) * _Color * _Alpha;
		//texcol.rgb = dot(texcol.rgb, float3(0.3, 0.59, 0.11));
		texcol.a = 1-texcol.a;
		return texcol;
	}
		ENDCG

	}
	}
		Fallback "VertexLit"
}