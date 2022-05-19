Shader "Unlit/UnlitHealthBar"
{
    Properties
    {
        _Value ("Value", Range(0,1)) = 1
        _LowColor ("Low Color", Color) = (1, 0, 0, 1)
        _MidColor ("Mid Color", Color) = (1, 1, 0, 1)
        _HighColor ("High Color", Color) = (0, 1, 0, 1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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

            half _Value;
            fixed4 _LowColor, _MidColor, _HighColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float x = step(_Value, 0.5);
                fixed4 col = lerp(_LowColor, _MidColor, _Value * 2) * x + lerp(_MidColor, _HighColor, _Value * 2 - 1) * (1 - x);
                col.a *= step(1 - i.uv.x, _Value);

                return col;
            }
            ENDCG
        }
    }
}
