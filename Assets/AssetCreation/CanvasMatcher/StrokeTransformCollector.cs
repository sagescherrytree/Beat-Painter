using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StrokeTransformCollector : MonoBehaviour
{
    [SerializeField]
    private StrokeInitData strokeInitData;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var id = child.GetComponent<StrokeQuad>().id;
            var displacement = child.position;
            displacement.z = 0;
            strokeInitData.displacements.Add(displacement.normalized);
            strokeInitData.rotations.Add(child.rotation.normalized);
            strokeInitData.flipY.Add(child.localScale.y < 0f);
            strokeInitData.ids.Add(id);
        }
    }
}
