using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialPalette : MonoBehaviour
{
    public int currColor;
    public GameObject[] colours;

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
        Vector2 joystickDir = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        joystickDir.Normalize();
        float angle = Mathf.Atan2(joystickDir.x, joystickDir.y);
        float deg = angle * Mathf.Rad2Deg;
        deg = deg % 360;
    }
}
