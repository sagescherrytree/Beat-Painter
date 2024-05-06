using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialPalette : MonoBehaviour
{
    public int currColor;
    public GameObject[] colours;

    public MeshRenderer paintbrush;
    public ParticleSystemRenderer particles;
    private int prevColor;

    // Start is called before the first frame update
    void Start()
    {
        currColor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 joystickDir = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        joystickDir.Normalize();
        float angle = Mathf.Atan2(joystickDir.y, joystickDir.x) * Mathf.Rad2Deg;
        if (angle < 0f)
            angle += 360f;

        // Determine the selected color based on the angle
        currColor = Mathf.FloorToInt(angle / 45f) % numberOfColors;

        if (currColor != prevColor)
        {
            // Update materials only when the color changes
            UpdateMaterials();
            prevColor = currColor;
        }

        // The stupid way.
        // float angle = Mathf.Atan2(joystickDir.x, joystickDir.y);
        // float deg = angle * Mathf.Rad2Deg;
        // deg = deg % 360;
        // Debug.Log("Angle: " + deg);

        // float span = 360 / 8; // 45 degrees.
        // if (deg >= 0f && deg <= span)
        // {

        //     prevColor = currColor;
        //     currColor = 0;
        //     UpdateMaterials();
        // }
        // else if (deg >= span && deg <= span + 1)
        // {
        //     prevColor = currColor;
        //     currColor = 1;
        //     UpdateMaterials();
        // }
        // else if (deg >= span + 1 && deg <= span + 2)
        // {
        //     prevColor = currColor;
        //     currColor = 2;
        //     UpdateMaterials();
        // }
        // else if (deg >= span + 2 && deg <= span + 3)
        // {
        //     prevColor = currColor;
        //     currColor = 3;
        //     UpdateMaterials();
        // }
        // else if (deg >= span + 3 && deg <= span + 4)
        // {
        //     prevColor = currColor;
        //     currColor = 4;
        //     UpdateMaterials();
        // }
        // else if (deg >= span + 4 && deg <= span + 5)
        // {
        //     prevColor = currColor;
        //     currColor = 5;
        //     UpdateMaterials();
        // }
        // else if (deg >= span + 5 && deg <= span + 6)
        // {
        //     prevColor = currColor;
        //     currColor = 6;
        //     UpdateMaterials();
        // }
        // else if (deg >= span + 6 && deg <= span + 7)
        // {
        //     prevColor = currColor;
        //     currColor = 7;
        //     UpdateMaterials();
        // }
        // else
        // {
        //     Debug.Log("Radial palette out of bounds.");
        // }
    }

    void UpdateMaterials()
    {
        colours[prevColor].GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); // Disable prev colour mat.
        Material currMat = colours[currColor].GetComponent<Renderer>().material;
        currMat.EnableKeyword("_EMISSION"); // Enable curr colour mat.
        Material[] bMats = paintbrush.materials;
        Material[] pMats = particles.materials;
        bMats[3] = currMat;
        pMats[0] = currMat;
        paintbrush.materials = bMats;
        particles.materials = pMats;
    }
}
