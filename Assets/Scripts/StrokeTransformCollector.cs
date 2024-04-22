using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeTransformCollector : MonoBehaviour
{
    [SerializeField]
    private StrokeTransforms StrokeTransforms;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var id = child.GetComponent<StrokeQuad>().id;
            StrokeTransforms.transforms.Add(child);
            StrokeTransforms.ids.Add(id);
        }
    }
}
