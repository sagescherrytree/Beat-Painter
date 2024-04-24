using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Canvas : MonoBehaviour
{
    [SerializeField] private List<Material> materials;
    [SerializeField] private GameObject canvasPrefab;
    private float _spawnZ = 0;

    public void Paint(int index)
    {
        _spawnZ -= 0.01f;
        var position = canvasPrefab.transform.position;
        position.z += _spawnZ;
        var canvasInstance = Instantiate(canvasPrefab, position, canvasPrefab.transform.rotation);
        canvasInstance.GetComponent<MeshRenderer>().material = materials[index];
    }
}
