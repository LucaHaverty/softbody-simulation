Shader "Custom/Ground"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _GridWidth ("Grid Width", Range(0.001, 0.03)) = 0.01
        _GridTileCount("Grid Tile Count", Range(3, 30)) = 10
        _GridColor("Grid Color", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _GridWidth;
        int _GridTileCount;
        fixed4 _GridColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            const float gridSpacing = 1.0 / _GridTileCount;
            
            if ((IN.uv_MainTex.x+_GridWidth/2) % gridSpacing < _GridWidth
                || (IN.uv_MainTex.y+_GridWidth/2) % gridSpacing < _GridWidth)
                    o.Albedo = _GridColor.rgb;
            //else o.Albedo    = fixed3(IN.uv_MainTex.x, IN.uv_MainTex.y, 0.5);
            else o.Albedo = _Color;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
