using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintBrush_UI : MonoBehaviour
{
    public GameObject particles;
    public float particleLifetime = 2f;
    private GameObject currParticleObject;
    private GameObject lastParticleObject;

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Color color = Color.green;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            color = Color.red;
            var target = hit.transform.gameObject.GetComponent<Button>();
            if (target != null)
            {
                Debug.Log("Found a target!");
                target.Select();
            }
        }
        Debug.DrawRay(transform.position, transform.forward * 100, color);
    }

    void DestroyLastParticle()
    {
        // Check if the lastParticleObject exists and is not already destroyed
        if (lastParticleObject != null && lastParticleObject.activeSelf)
        {
            // Destroy the last instantiated particle system
            Destroy(lastParticleObject);
        }
    }
}
