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
        if (counter < canvasMats.Length)
        {
            canvasMats[counter].color = Color.white;
            Debug.Log("Colour change.");
        }
    }
}
