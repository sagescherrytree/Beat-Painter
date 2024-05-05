using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    public int currColor;
    public float test;

    public Material[] colors;

    public MeshRenderer paintbrush;
    public ParticleSystemRenderer particles;

    // Start is called before the first frame update
    void Start()
    {
        currColor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int direction = 0;
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                direction += 1;
            }
            if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                direction -= 1;
            }

            if (direction != 0)
            {
                colors[currColor].DisableKeyword("_EMISSION");
                currColor = (currColor + direction + colors.Length) % colors.Length;
                Material currMat = colors[currColor];
                currMat.EnableKeyword("_EMISSION");
                Material[] bMats = paintbrush.materials;
                Material[] pMats = particles.materials;
                bMats[3] = currMat;
                pMats[0] = currMat;
                paintbrush.materials = bMats;
                particles.materials = pMats;
            }
    }
}
