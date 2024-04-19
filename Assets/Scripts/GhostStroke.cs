using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public enum PaintColor
{
    Cyan,
    Magenta
}

public class GhostStroke : MonoBehaviour
{
    private static readonly int StrokeLength = Shader.PropertyToID("_StrokeLength");
    private static readonly int FillLength = Shader.PropertyToID("_FillLength");
    private const float TargetHover = 0.1f;
    
    [SerializeField] private float startZ = 0;
    [SerializeField] private float endZ = -25;
    [SerializeField] private float speed = -2;
    [SerializeField] private GameObject targetPrefab;
    
    private List<GhostStrokeTarget> _targetInstances;
    private int _activeIndex;
    
    private LineRenderer _renderer;
    private List<float> _fillLengths;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(List<Vector2> points)
    {
        _activeIndex = 0;
        _renderer = gameObject.GetComponent<LineRenderer>();
        _targetInstances = new();
        _fillLengths = new();
        
        for (var i = 0; i < points.Count; i++)
        {
            var target = Instantiate(targetPrefab, new Vector3(points[i].x, points[i].y, startZ - TargetHover), Quaternion.identity)
                .GetComponent<GhostStrokeTarget>();
            target.Init(i, this);
            _targetInstances.Add(target);
        }
        _targetInstances[0].State = TargetState.Active;
        
        _renderer.positionCount = points.Count;
        _renderer.SetPositions(points.Select(point => new Vector3(point.x, point.y, startZ)).ToArray());
        var fillLength = 0f;
        for (var i = 1; i < points.Count; i++)
        {
            _fillLengths.Add(fillLength);
            fillLength += (points[i] - points[i - 1]).magnitude;
        }
        _fillLengths.Add(fillLength);
        _renderer.material.SetFloat(StrokeLength, fillLength);
    }

    public void HitTarget(int hitIndex)
    {
        for (var i = _activeIndex; i < hitIndex; i++)
        {
            _targetInstances[i].State = TargetState.Missed;
        }
        _renderer.material.SetFloat(FillLength, _fillLengths[hitIndex]);
        _activeIndex = hitIndex + 1;
        if (_activeIndex < _targetInstances.Count)
        {
            _targetInstances[_activeIndex].State = TargetState.Active;
        }
        else
        {
            // TODO: logic for completed stroke
            foreach (var target in _targetInstances)
            {
                Destroy(target.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        
    }
}