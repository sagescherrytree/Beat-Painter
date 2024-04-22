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
            strokeInitData.mats.Add(child.localToWorldMatrix);
            strokeInitData.ids.Add(id);
        }
    }
}
