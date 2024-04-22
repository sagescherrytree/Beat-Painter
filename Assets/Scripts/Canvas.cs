using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    [SerializeField] private List<Texture2D> strokes;
    [SerializeField] private CustomRenderTexture texture;
    [SerializeField] private Material blendMat;
    private static readonly int PaintTex = Shader.PropertyToID("_PaintTex");

    void Start()
    {
        blendMat.SetTexture(PaintTex, Texture2D.blackTexture);
        texture.Update();
    }
    
    public void Paint(int index)
    {
        blendMat.SetTexture(PaintTex, strokes[index]);
        texture.Update();
    }
}
