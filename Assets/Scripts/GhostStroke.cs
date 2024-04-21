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

    [SerializeField] private float endZ;
    [SerializeField] private float speed;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float lifeTime;
    
    private List<GhostStrokeTarget> _targetInstances;
    private int _activeIndex;

    private Vector3[] _positions;
    private LineRenderer _renderer;
    private List<float> _fillLengths;

    private void FixedUpdate()
    {
        if (_activeIndex >= 0)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Miss();
            }
            return;
        }
        var currZ = _positions[0].z;
        currZ += speed * Time.deltaTime;
        if (endZ > currZ)
        {
            currZ = endZ;
            _activeIndex = 0;
        }
        for (var i = 0; i < _positions.Length; i++)
        {
            _positions[i].z = currZ;
        }
        _renderer.SetPositions(_positions);
        if (_activeIndex == 0)
        {
            SpawnTargets();
        }
    }

    public void Init(Vector3[] positions)
    {
        _positions = positions;
        _activeIndex = -1;
        _renderer = gameObject.GetComponent<LineRenderer>();
        _fillLengths = new();
        
        _renderer.positionCount = positions.Length;
        _renderer.SetPositions(positions);
        var fillLength = 0f;
        for (var i = 1; i < positions.Length; i++)
        {
            _fillLengths.Add(fillLength);
            fillLength += (positions[i] - positions[i - 1]).magnitude;
        }
        _fillLengths.Add(fillLength);
        _renderer.material.SetFloat(StrokeLength, fillLength);
    }

    public void SpawnTargets()
    {
        _targetInstances = new();
        for (var i = 0; i < _positions.Length; i++)
        {
            var spawnPos = _positions[i];
            spawnPos.z -= TargetHover;
            var target = Instantiate(targetPrefab, spawnPos, Quaternion.identity)
                .GetComponent<GhostStrokeTarget>();
            target.Init(i, this);
            _targetInstances.Add(target);
        }
        _targetInstances[0].State = TargetState.Active;
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
            Complete();
        }
    }

    private void Complete()
    {
        Destroy(gameObject);
    }

    private void Miss()
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        if (_targetInstances == null)
        {
            return;
        }
        foreach (var target in _targetInstances)
        {
            Destroy(target.gameObject);
        }
    }
}