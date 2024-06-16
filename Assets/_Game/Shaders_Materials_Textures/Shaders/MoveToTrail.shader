Shader "MyShader/Move To Trail"
{
    Properties
    {
        _MainTex("Main Texture (RGB)", 2D) = "white" {}
		_MainTexVFade("MainTex V Fade", Range(0, 1)) = 0
		_MainTexVFadePow("MainTex V Fade Pow", Float) = 1
		_MainTexPow("Main Texture Gamma", Float) = 1
		_MainTexMultiplier("Main Texture Multiplier", Float) = 1
		_TintTex("Tint Texture (RGB)", 2D) = "white" {}
		_Multiplier("Multiplier", Float) = 1
		_MainScrollSpeedU("Main Scroll U Speed", Float) = 10
		_MainScrollSpeedV("Main Scroll V Speed", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend One One
        ZWrite On
        ZTest LEqual
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                half4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uvOrigin : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			sampler2D _TintTex;
					half _MainTexVFade;
					half _MainTexVFadePow;
					half _MainTexPow;
					half _MainTexMultiplier;
					half _Multiplier;
					half _MainScrollSpeedU;
					half _MainScrollSpeedV;
                    half _MoveToMaterialUV;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.x -= frac(_Time.x * _MainScrollSpeedU) + _MoveToMaterialUV;
				o.uv.y -= frac(_Time.x * _MainScrollSpeedV);
                o.uvOrigin = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainTex = tex2D(_MainTex, i.uv);
					half vFade = 1 - abs(i.uvOrigin.y - 0.5) * 2;
					vFade = pow(abs(vFade), _MainTexVFadePow);
					vFade = lerp(1, vFade, _MainTexVFade);
					mainTex.rgb *= vFade;
					mainTex.rgb = pow(abs(mainTex.rgb), _MainTexPow) * _MainTexMultiplier;
                    half avr = mainTex.r * 0.3333 + mainTex.g * 0.3334 + mainTex.b * 0.3333;
                    half4 col = tex2D(_TintTex, half2(avr, 0.5));
					half intensityHigh = 1;
					col.rgb *=  mainTex;
                return col;
            }
            ENDCG
        }
    }
}
