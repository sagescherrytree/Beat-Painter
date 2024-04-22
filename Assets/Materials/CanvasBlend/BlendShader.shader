Shader "CustomRenderTexture/BlendShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _CanvasTex("CanvasTex", 2D) = "white" {}
        _PaintTex("PaintTex", 2D) = "white" {}
     }

     SubShader
     {
        Lighting Off
        Blend One Zero

        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            sampler2D _CanvasTex;
            sampler2D _PaintTex;

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 paint = tex2D(_PaintTex, IN.localTexcoord.xy);
                return paint.a == 0 ? tex2D(_CanvasTex, IN.localTexcoord.xy) : paint;
            }
            ENDCG
        }
    }
}
