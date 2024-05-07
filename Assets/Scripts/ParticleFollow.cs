using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    [SerializeField] GameObject toFollow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (toFollow != null)
        {
            // Debug.Log("Target pos: " + toFollow.transform.position);
            transform.position = toFollow.transform.position;
            transform.rotation = toFollow.transform.rotation;
        }
    }
}
