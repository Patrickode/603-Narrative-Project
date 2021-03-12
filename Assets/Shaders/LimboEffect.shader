Shader "Custom/LimboImageEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _OldTVNoise("Old TV Noise Texture", 2D) = "white" {}
        _NoiseAttenuation("Noise Attenuation", Range(0.0, 1.0)) = 0.5
        _GrainScale("Grain Scale", Range(0.0, 10.0)) = 0.5
        _RandomNumber("Random Value", Range(0.0, 1.0)) = 1.0

        _VignetteBlinkvelocity("Vignette Blink Velocity", Range(0, 10)) = 5.0
        _VignetteDarkAmount("Vignette Dark Amount", Range(0, 10)) = 1.0
        _VigneteDistanceFromCenter("Vignette Distance from Center", Range(0, 10)) = 1.2

        _ScreenPartitionWidth("Screen Partition Width", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Box blur
            float4 boxblur(sampler2D tex, float2 uv, float4 size)
            {
                float4 c = tex2D(tex, uv + float2(-size.x, size.y)) + tex2D(tex, uv + float2(0, size.y)) + tex2D(tex, uv + float2(size.x, size.y)) +
                            tex2D(tex, uv + float2(-size.x, 0)) + tex2D(tex, uv + float2(0, 0)) + tex2D(tex, uv + float2(size.x, 0)) +
                            tex2D(tex, uv + float2(-size.x, -size.y)) + tex2D(tex, uv + float2(0, -size.y)) + tex2D(tex, uv + float2(size.x, -size.y));
                return c / 9;
            }

            sampler2D _MainTex;
            sampler2D _OldTVNoise;
            float4 _MainTex_ST;
            float _ScreenPartitionWidth;
            float _NoiseAttenuation;
            float _GrainScale;
            float _RandomNumber;

            float _VignetteBlinkvelocity;
            float _VignetteDarkAmount;
            float _VigneteDistanceFromCenter;

            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, i.uv);

                if (i.uv.x > _ScreenPartitionWidth)
                {
                    // Debug line
                    //if (abs(i.uv.x - _ScreenPartitionWidth) < 0.005)
                    //    return fixed4(0.0, 0.0, 0.0, 1.0);

                    //Apply the perception brightness proportion for each color chanel
                    float luminosity = col.x * 0.3 + col.y * 0.59 + col.z * 0.11;

                    fixed4 noise = clamp(fixed4(_NoiseAttenuation,_NoiseAttenuation,_NoiseAttenuation,1.0) + tex2D(_OldTVNoise, i.uv * _GrainScale + float2(_RandomNumber,_RandomNumber)), 0.0, 1.0);
                    float fadeInBlack = pow(clamp(_VigneteDistanceFromCenter - distance(i.uv, float2(0.5,0.5)) + abs(cos(_RandomNumber / 10 + _Time * 10 * _VignetteBlinkvelocity)) / 4, 0.0, 1.0),_VignetteDarkAmount);
                    float4 blurCol = boxblur(_MainTex, i.uv, float4(1.0, 1.0, 1.0, 1.0));
                    float blurValue = (blurCol.x * 0.3 + blurCol.y * 0.59 + blurCol.z * 0.11);
                    return fixed4(luminosity, luminosity, luminosity, 1.0) / blurValue * noise * fadeInBlack * fadeInBlack * blurValue;
                }
                else
                {
                    return col;
                }
            }
            ENDCG
        }
    }
}
