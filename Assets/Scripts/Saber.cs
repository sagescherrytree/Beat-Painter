using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saber : MonoBehaviour
{
    // I do not know what I am doing XD.
    public LayerMask layer;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {

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
                Debug.Log("Found cube!");
            }
        }
        previousPosition = transform.position;
    }
}
