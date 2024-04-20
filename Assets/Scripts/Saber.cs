using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saber : MonoBehaviour
{
    // I do not know what I am doing XD.
    public LayerMask layer;
    private Vector3 previousPosition;
    // Try something.
    public GameObject canvas;
    private MeshRenderer canvasRenderer;
    private Material[] canvasMats;
    private int counter;
    private bool isMatVisible = true;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        canvasRenderer = canvas.GetComponent<MeshRenderer>();
        if (canvasRenderer == null)
        {
            Debug.Log("Error: Canvas MeshRenderer does not exist.");
        }
        canvasMats = canvasRenderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, layer))
        {
            if (Vector3.Angle(transform.position - previousPosition, hit.transform.up) > 130)
            {
                Destroy(hit.transform.gameObject);
                ChangeMaterial(counter++);
                Debug.Log("Found cube!");
            }
        }
        previousPosition = transform.position;
    }

    void ChangeMaterial(int counter)
    {
        Material mat = canvasMats[counter];
        isMatVisible = !isMatVisible;
        if (counter < canvasMats.Length)
        {
            mat.SetInt("_SrcBlend", isMatVisible ? (int)UnityEngine.Rendering.BlendMode.One : (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_DstBlend", isMatVisible ? (int)UnityEngine.Rendering.BlendMode.Zero : (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_ZWrite", isMatVisible ? 1 : 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = isMatVisible ? -1 : 3000;
            Debug.Log("Colour change.");
        }
    }
}
