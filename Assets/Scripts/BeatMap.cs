using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMap : MonoBehaviour
{
    private float[] map;
    // Just a test.
    // On awake, initialize map.
    void Awake()
    {
        map = new float[150];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = 1.5f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float[] getMap()
    {
        return map;
    }
}
