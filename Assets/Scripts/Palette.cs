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

    public MeshRenderer paintbrush;

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

            Material[] mats = paintbrush.materials;
            switch (currColor) {
                case 1:
                    col2.DisableKeyword("_EMISSION");
                    col8.DisableKeyword("_EMISSION");
                    col1.EnableKeyword("_EMISSION");
                    mats[3] = col1;
                    break;
                case 2:
                    col3.DisableKeyword("_EMISSION");
                    col1.DisableKeyword("_EMISSION");
                    col2.EnableKeyword("_EMISSION");
                    mats[3] = col2;
                    break;
                case 3:
                    col4.DisableKeyword("_EMISSION");
                    col2.DisableKeyword("_EMISSION");
                    col3.EnableKeyword("_EMISSION");
                    mats[3] = col3;
                    break;
                case 4:
                    col5.DisableKeyword("_EMISSION");
                    col3.DisableKeyword("_EMISSION");
                    col4.EnableKeyword("_EMISSION");
                    mats[3] = col4;
                    break;
                case 5:
                    col6.DisableKeyword("_EMISSION");
                    col4.DisableKeyword("_EMISSION");
                    col5.EnableKeyword("_EMISSION");
                    mats[3] = col5;
                    break;
                case 6:
                    col7.DisableKeyword("_EMISSION");
                    col5.DisableKeyword("_EMISSION");
                    col6.EnableKeyword("_EMISSION");
                    mats[3] = col6;
                    break;
                case 7:
                    col8.DisableKeyword("_EMISSION");
                    col6.DisableKeyword("_EMISSION");
                    col7.EnableKeyword("_EMISSION");
                    mats[3] = col7;
                    break;
                case 8:
                    col1.DisableKeyword("_EMISSION");
                    col7.DisableKeyword("_EMISSION");
                    col8.EnableKeyword("_EMISSION");
                    mats[3] = col8;
                    break;
            }
            paintbrush.materials = mats;

            Debug.Log("changed color");
            yield return new WaitForSeconds(0.1f);
        }
        // left controller
        
        
    }
}
