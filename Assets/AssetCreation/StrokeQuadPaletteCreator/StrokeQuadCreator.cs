using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeQuadCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> PaletteItems;
    void Start()
    {
        for (int i = 0; i < PaletteItems.Count; i++)
        {
            var item = PaletteItems[i].AddComponent<StrokeQuad>();
            item.id = i;
            item.name = $"Stroke {i}";
            item.transform.position = new Vector3(i, 0, 0);
        }
    }
}
