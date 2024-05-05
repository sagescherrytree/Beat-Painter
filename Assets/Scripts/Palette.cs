using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    public int currColor;
    public int maxColor;

    // Start is called before the first frame update
    void Start()
    {
        currColor = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // left controller
        Vector2 pos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (pos.x > 0 || pos.y < 0) { // right/down
            currColor += 1;
            if (currColor > maxColor) {
                currColor = 1;
            }
        } else if (pos.x < 0 || pos.y > 0) { // left/up
            currColor -= 1;
            if (currColor < 1) {
                currColor = maxColor;
            }
        }
        Invoke("DelayedAction", 1.0f);
    }
}
