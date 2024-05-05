using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    public int currColor;
    public int maxColor;

    // public Material col1;
    // public Material col2;
    // public Material col3;
    // public Material col4;
    // public Material col5;
    // public Material col6;
    // public Material col7;
    // public Material col8;
    public Material[] colors;

    public MeshRenderer paintbrush;
    public ParticleSystemRenderer particles;

    // Start is called before the first frame update
    void Start()
    {
        currColor = 1;
        StartCoroutine(ChangeColor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator ChangeColor() {
        while (true) {
            Vector2 pos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            var direction = (int) Mathf.Sign(pos.x);
            if (direction != 0)
            {
                colors[currColor].DisableKeyword("_EMISSION");
                currColor = (currColor + direction) % 8;
                var currMat = colors[currColor];
                currMat.EnableKeyword("_EMISSION");
                Material[] bMats = paintbrush.materials;
                Material[] pMats = particles.materials;
                bMats[3] = currMat;
                pMats[0] = currMat;
            }
            // Debug.Log("changed color");
            yield return new WaitForSeconds(0.1f);
        }
    }
}
