Shader "Custom/Wind Shader"
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
        _WindSpeed("Wind Speed", Float) = 1.0
        _WindDirection("Wind Direction", Vector) = (1.0, 0.1, 0.5)
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
        float _WindSpeed;
        float3 _WindDirection;

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.uv_MainTex = v.texcoord.xy;

            float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
            float heightAboveStart = max(0.0, v.vertex.y - _WindStartHeight);
            float windEffect = _WindIntensity * pow(heightAboveStart * _WindHeightInfluence, 0.5);

            float windPhase = sin(worldPos.x + worldPos.z + _Time.y * _WindSpeed);

            v.vertex.xyz += _WindDirection * windEffect * windPhase;
            v.vertex.y += 0.0001;
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