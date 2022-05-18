Shader "Custom/CustomSurfaceSkyBox"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _UVScaleX ("ScaleX", Float) = 1
        _UVScaleY ("ScaleY", Float) = 1
        _UVOffsetX ("OffsetX", Float) = 0
        _UVOffsetY ("OffsetY", Float) = 0
        _BorderColor ("BorderColor", Color) = (0,0,0,1)
        _BorderColorMix ("BorderColorMix", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Front

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        // #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _UVScaleX;
        float _UVScaleY;
        float _UVOffsetX;
        float _UVOffsetY;
        fixed4 _BorderColor;
        float _BorderColorMix;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.uv_MainTex;
            uv.x -= _UVOffsetX;
            uv.y -= _UVOffsetY;

            uv.x /= _UVScaleX;
            uv.y /= _UVScaleY;

            float isBorder = step(1 - uv.x, 0) + step(1 - uv.y, 0) + step(uv.x, 0) + step(uv.y, 0);

            uv.x = -clamp(uv.x, 0, 1);
            uv.y = clamp(uv.y, 0, 1);

            fixed4 t = tex2D (_MainTex, uv);
            fixed4 b = lerp(t, _BorderColor, _BorderColorMix);

            // Albedo comes from a texture tinted by color
            fixed4 c = (t * (1 - isBorder) + b * isBorder) * _Color;
            // fixed4 c = isBorder * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
