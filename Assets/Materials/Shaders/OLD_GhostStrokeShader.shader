Shader "Unlit/GhostStrokeShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 1, 1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _StrokeLength ("Stroke Length", float) = 1
        _FillLength ("Fill Length", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
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

            sampler2D _MainTex;
            fixed4 _Color;
            float _FillLength;
            float _StrokeLength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;
                col.a = max(0.0, abs(i.uv.y - 0.5) - 0.3) * 10;
                col.a = max(col.a, clamp(_FillLength - i.uv.x + 1, 0.0, 1.0));
                return col;
            }
            ENDCG
        }
    }
}
