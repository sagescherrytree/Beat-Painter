using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
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
            }
        }
        Debug.DrawRay(transform.position, transform.forward * 100, color);
    }
}
