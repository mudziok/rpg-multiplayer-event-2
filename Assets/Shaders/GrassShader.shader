Shader "Custom/GrassShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _WindIntensity("Wind Intensity", Float) = 1.0
        _WindStartHeight("Wind Start Height", Float) = 0.0
        _WindHeightInfluence("Wind Height Influence", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0
        #pragma vertex vert

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _WindIntensity;
        float _WindStartHeight;
        float _WindHeightInfluence;

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o); 
            o.uv_MainTex = v.texcoord.xy;

            float heightAboveStart = max(0.0, v.vertex.y - _WindStartHeight);
            float windEffect = _WindIntensity * pow(heightAboveStart * _WindHeightInfluence, 0.5); 

            float3 windDirection = float3(1.0, 0.0, 0.0);
            v.vertex.xyz += windDirection * windEffect * sin(_Time.y + v.vertex.x + v.vertex.z);
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}