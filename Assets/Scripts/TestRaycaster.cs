using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycaster : MonoBehaviour
{
    private Camera _camera;

    // simple counter
    int hits;


    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        hits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                var target = hit.collider.gameObject.GetComponent<GhostStrokeTarget>();
                target.Hit();

                hits++;
            }
        }
    }
}
