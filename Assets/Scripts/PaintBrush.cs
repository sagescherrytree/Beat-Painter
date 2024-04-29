using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    public int hits;

    void Start() {
        hits = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        Color color = Color.green;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            color = Color.red;
            var target = hit.transform.gameObject.GetComponent<GhostStrokeTarget>();
            if (target != null)
            {
                Debug.Log("Found a target!");
                target.Hit();
                hits++;
            }
        }
        Debug.DrawRay(transform.position, transform.forward * 100, color);
    }
}
