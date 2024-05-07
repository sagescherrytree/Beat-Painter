using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteMerge : MonoBehaviour
{
    public int currColor;

    public GameObject[] colors;

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
        ////// joystick implementation
        Vector2 joystickDir = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (joystickDir.magnitude > 0.5)
        {
            float angle = Mathf.Atan2(joystickDir.y, joystickDir.x) * Mathf.Rad2Deg;
            if (angle < 0f) {
                angle += 360f;
            }
            var color = Mathf.FloorToInt(angle / 45f);
            if (color == 0)
            {
                Debug.Log("Joystick sets to red");
            }
            UpdateMaterials(color);
        } else
        {
            int direction = 0;
            if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                direction += 1;
            }
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                direction -= 1;
            }
            var color = (currColor + direction + colors.Length) % colors.Length;
            if (color == 0)
            {
                Debug.Log($"Button sets to red: currColor: {currColor}, direction: {direction}, colorsLen: {colors.Length}");
            }
            UpdateMaterials(color);
        }
    }

    void UpdateMaterials(int color)
    {
        if (color == currColor)
        {
            return;
        }
        colors[currColor].GetComponent<Renderer>().material.DisableKeyword("_EMISSION"); // Disable prev colour mat.
        currColor = color;
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
