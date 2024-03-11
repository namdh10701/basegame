Shader "Custom/CRT" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Curvature("Curvature", Float) = 0.1
        _Blur("Blur", Float) = 0.1
        _CA_Amount("Chromatic Aberration Amount", Float) = 0.1
        _ScanlineIntensity("Scanline Intensity", Float) = 0.7
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                uniform sampler2D _MainTex;
                float _Curvature;
                float _Blur;
                float _CA_Amount;
                float _ScanlineIntensity;

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 fragColor = tex2D(_MainTex, i.uv);

                // Curving
                float2 crtUV = i.uv * 2.0 - 1.0;
                float2 offset = crtUV.yx/ _Curvature;
                crtUV += crtUV * offset * offset;   
                crtUV = crtUV * 0.5 + 0.5;

                float2 edge = smoothstep(0.0, _Blur, crtUV) * (1.0 - smoothstep(1.0 - _Blur, 1.0, crtUV));

                // Chromatic Aberration
                fragColor.rgb = fixed3(
                    tex2D(_MainTex, (crtUV - 0.5) * _CA_Amount + 0.5).r,
                    tex2D(_MainTex, crtUV).g,
                    tex2D(_MainTex, (crtUV - 0.5) / _CA_Amount + 0.5).b
                ) * edge.x * edge.y;

                // Scanlines
                if (fmod(i.vertex.y, 10.0) < 1.0)
                    fragColor.rgb *= _ScanlineIntensity;
                else if (fmod(i.vertex.x, 3.0) < 1.0)
                    fragColor.rgb *= _ScanlineIntensity;
                else
                    fragColor *= 1.2;


                return fragColor;
            }
            ENDCG
        }
        }
}