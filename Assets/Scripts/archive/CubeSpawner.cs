using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] cubes;
    [SerializeField] Transform[] points;

    [SerializeField] float beat;
    [SerializeField] BeatMap beatMap;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > beat)
        {
            int posToSpawn = Random.Range(0, 4);

            if (posToSpawn == 0)
            {
                GameObject redCube = cubes[0];
                Transform pointLeft = points[0];
                GameObject cube = Instantiate(redCube, pointLeft);
                cube.transform.localPosition = Vector3.zero;
                cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
                timer -= beat;

            }
            else if (posToSpawn == 1)
            {
                GameObject blueCube = cubes[1];
                Transform pointRight = points[1];
                GameObject cube = Instantiate(blueCube, pointRight);
                cube.transform.localPosition = Vector3.zero;
                cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
                timer -= beat;
            }
            else if (posToSpawn == 2 || posToSpawn == 3)
            {
                GameObject cube = Instantiate(cubes[Random.Range(0, 2)], points[Random.Range(2, 4)]);
                cube.transform.localPosition = Vector3.zero;
                cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
                timer -= beat;
            }
        }
        timer += Time.deltaTime;
    }
}
