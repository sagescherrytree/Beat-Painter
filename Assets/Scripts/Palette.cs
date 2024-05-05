using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    public int currColor;
    public int maxColor;

    public Material col1;
    public Material col2;
    public Material col3;
    public Material col4;
    public Material col5;
    public Material col6;
    public Material col7;
    public Material col8;

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

        switch (currColor) {
            case 1:
                col2.DisableKeyword("_EMISSION");
                col8.DisableKeyword("_EMISSION");
                col1.EnableKeyword("_EMISSION");
                break;
            case 2:
                col3.DisableKeyword("_EMISSION");
                col1.DisableKeyword("_EMISSION");
                col2.EnableKeyword("_EMISSION");
                break;
            case 3:
                col4.DisableKeyword("_EMISSION");
                col2.DisableKeyword("_EMISSION");
                col3.EnableKeyword("_EMISSION");
                break;
            case 4:
                col5.DisableKeyword("_EMISSION");
                col3.DisableKeyword("_EMISSION");
                col4.EnableKeyword("_EMISSION");
                break;
            case 5:
                col6.DisableKeyword("_EMISSION");
                col4.DisableKeyword("_EMISSION");
                col5.EnableKeyword("_EMISSION");
                break;
            case 6:
                col7.DisableKeyword("_EMISSION");
                col5.DisableKeyword("_EMISSION");
                col6.EnableKeyword("_EMISSION");
                break;
            case 7:
                col8.DisableKeyword("_EMISSION");
                col6.DisableKeyword("_EMISSION");
                col7.EnableKeyword("_EMISSION");
                break;
            case 8:
                col1.DisableKeyword("_EMISSION");
                col7.DisableKeyword("_EMISSION");
                col8.EnableKeyword("_EMISSION");
                break;
        }
        Invoke("DelayedAction", 1.0f);
    }
}
