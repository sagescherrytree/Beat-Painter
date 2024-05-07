using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteMerge : MonoBehaviour
{
    public int currColor;

    public GameObject[] colors;

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
        ////// joystick implementation
        Vector2 joystickDir = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        joystickDir.Normalize();
        float angle = Mathf.Atan2(joystickDir.y, joystickDir.x) * Mathf.Rad2Deg;
        if (angle < 0f) {
            angle += 360f;
        }

        if (joystickDir.x != 0 && joystickDir.y != 0) {
            // Determine the selected color based on the angle
            currColor = Mathf.FloorToInt(angle / 45f) % 8;
        }

        if (currColor != prevColor)
        {
            // Update materials only when the color changes
            UpdateMaterials();
            prevColor = currColor;
        }


        // button implementation
        int direction = 0;
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            prevColor = currColor;
            direction += 1;
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            prevColor = currColor;
            direction -= 1;
        }

        if (direction != 0)
        {
            currColor = (currColor + direction + colors.Length) % colors.Length;
            UpdateMaterials();
        }
    }

    void UpdateMaterials()
    {
        colors[prevColor].GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); // Disable prev colour mat.
        Material currMat = colors[currColor].GetComponent<Renderer>().material;
        currMat.EnableKeyword("_EMISSION"); // Enable curr colour mat.
        Material[] bMats = paintbrush.materials;
        Material[] pMats = particles.materials;
        bMats[3] = currMat;
        pMats[0] = currMat;
        paintbrush.materials = bMats;
        particles.materials = pMats;
    }
}
