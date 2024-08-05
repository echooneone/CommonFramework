Shader "Custom/GridShader"
{
    Properties
    {
        _CellSize("Cell Size", Float) = 16
        _DotSize("Dot Size", Range(0.0, 0.5)) = 0.25
        _BackgroundColor("Background Color", Color) = (0.1176, 0.1176, 0.1176, 1.0)
        _DotColor("Dot Color", Color) = (0.2156, 0.2156, 0.2156, 1.0)
        
        _Offset("Offset", Vector) = (0.0, 0.0,0.0)
        _AntiAliasingFactor("Anti Aliasing Factor", Float) = 0.01
        _StochasticSamples("Stochastic Samples", Int) = 16
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _CellSize;
            float _DotSize;
            float4 _BackgroundColor;
            float4 _DotColor;
            float2 _Offset;
            float _AntiAliasingFactor;
            int _StochasticSamples;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float random(float2 st)
            {
               // return frac(sin(dot(st, float2(12.9898,78.233))) * 43758.5453123);
                 return frac(sin(dot(st, float2(12.9898,78.233))) * 43758.5453123);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float4 finalColor = float4(0.0, 0.0, 0.0, 0.0);
                float2 resolution = _ScreenParams.xy;

                for (int j = 0; j < _StochasticSamples; ++j)
                {
                    float2 randomOffset = float2(
                        random(float2(float(j), 0.0)),
                        random(float2(0.0, float(j)))
                    ) / resolution;

                    float2 scaled_pos = ((uv + randomOffset) * resolution + _Offset) / _CellSize;
                    float2 nearest_grid = round(scaled_pos) * _CellSize;
                    float dist = length(((uv + randomOffset) * resolution + _Offset) - nearest_grid);
                    float radius = _DotSize * _CellSize * 0.5;
                    float edge_smoothness = _AntiAliasingFactor;
                    float alpha = smoothstep(radius - edge_smoothness, radius + edge_smoothness, dist);
                    finalColor += lerp(_DotColor, _BackgroundColor, alpha);
                }

                return finalColor / float(_StochasticSamples);
            }
            ENDCG
        }
    }
}