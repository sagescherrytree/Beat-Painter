using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public struct GhostStrokeInitData
{
    public int Index;
    public PaintColor Color;
    public Transform transform;
}

public class GhostStrokeSpawner : MonoBehaviour
{
    [SerializeField] private string filename;
    [SerializeField] StrokePointsRecord strokePoints;
    
    // TODO: use a list of initData w/ list of spawn times
    private GhostStrokeInitData _initData;
    
    [SerializeField]
    private GameObject ghostStrokePrefab;
    // Start is called before the first frame update
    void Start()
    {
        _initData = new GhostStrokeInitData
        {
            Index = 0,
            Color = PaintColor.Cyan,
            transform = GetComponent<Transform>()
        };
        _initData.transform.localScale *= 10;
        
        var ghostStroke = Instantiate(ghostStrokePrefab).GetComponent<GhostStroke>();
        
        // TODO: Create strokes in update loop, not at start
        var list = strokePoints.strokes[_initData.Index].data
            .Select(v => (Vector2) _initData.transform.TransformPoint(v))
            .ToList();
        
        ghostStroke.Init(list);
    }

}
