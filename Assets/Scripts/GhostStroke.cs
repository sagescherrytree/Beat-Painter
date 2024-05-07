using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public enum Zone
{
    Pending,
    Active,
    Pass
}

public class GhostStroke : MonoBehaviour
{
    private static readonly int StrokeLength = Shader.PropertyToID("_StrokeLength");
    private static readonly int FillLength = Shader.PropertyToID("_FillLength");
    private const float TargetHover = 0.1f;

    [SerializeField] private int numActive;
    [SerializeField] private GameObject targetPrefab;

    [SerializeField] private float pendingZ;
    [SerializeField] private float activeZ;
    [SerializeField] private float passZ;
    [SerializeField] private float despawnZ;

    private float _zoneStartTime;
    [SerializeField] private float pendingDuration;
    [SerializeField] private float activeDuration;
    [SerializeField] private float passDuration;

    [SerializeField] private AnimationCurve pendingCurve;
    [SerializeField] private AnimationCurve activeCurve;
    [SerializeField] private AnimationCurve passCurve;
    
    private Zone _currZone;

    private List<GhostStrokeTarget> _targetInstances;
    private int _firstActiveIndex;

    private Vector3[] _positions;
    private LineRenderer _renderer;
    private List<float> _fillLengths;

    public int ColorInd { get; private set;}
    private int _canvasIndex;
    private Canvas _canvas;

    private float GetDuration(Zone zone)
    {
        return _currZone switch
        {
            Zone.Pending => pendingDuration,
            Zone.Active => activeDuration,
            Zone.Pass => passDuration,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    private void SetZ(float t)
    {
        var z = _currZone switch
        {
            Zone.Pending => Mathf.Lerp(pendingZ, activeZ, pendingCurve.Evaluate(t)),
            Zone.Active => Mathf.Lerp(activeZ, passZ, activeCurve.Evaluate(t)),
            Zone.Pass => Mathf.Lerp(passZ, despawnZ, passCurve.Evaluate(t)),
            _ => throw new ArgumentOutOfRangeException()
        };
    
        for (var i = 0; i < _positions.Length; i++)
        {
            _positions[i].z = z;
        }
        _renderer.SetPositions(_positions);

        foreach (var target in _targetInstances)
        {
            var objectTransform = target.gameObject.transform;
            var position = objectTransform.position;
            position.z = z;
            objectTransform.position = position;
        }
    }

    private void FixedUpdate()
    {
        var duration = GetDuration(_currZone);
        var t = (Time.time - _zoneStartTime) / duration;
        
        // No state transition occurs
        if (t <= 1f)
        {
            SetZ(t);
            return;
        }

        _zoneStartTime += duration;
        switch (_currZone)
        {
            case Zone.Pending:
                _currZone = Zone.Active;
                t = (Time.time - _zoneStartTime) / activeDuration;
                SetZ(t);
                SpawnTargets();
                break;
            case Zone.Active:
                _currZone = Zone.Pass;
                t = (Time.time - _zoneStartTime) / passDuration;
                SetZ(t);
                for (; _firstActiveIndex < _targetInstances.Count; _firstActiveIndex++)
                {
                    _targetInstances[_firstActiveIndex].State = TargetState.Missed;
                }
                break;
            case Zone.Pass:
                Complete();
                return;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    public void Init(Vector3[] positions, Canvas canvas, int canvasIndex, int colorInd, ColorSwatch swatch)
    {
        _positions = positions;
        _canvas = canvas;
        _canvasIndex = canvasIndex;
        ColorInd = colorInd;
        
        _currZone = Zone.Pending;
        _zoneStartTime = Time.time;
        
        _firstActiveIndex = 0;
        _targetInstances = new();
        
        _renderer = gameObject.GetComponent<LineRenderer>();
        _renderer.positionCount = positions.Length;
        _renderer.SetPositions(positions);
        _renderer.material.SetColor("_Color", swatch.colors[colorInd]);
        
        _fillLengths = new();
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
        for (int i = 0; i < numActive; i++)
        {
            _targetInstances[i].State = TargetState.Active;
        }
    }

    public void HitTarget(int hitIndex)
    {
        for (var i = _firstActiveIndex; i < hitIndex; i++)
        {
            _targetInstances[i].State = TargetState.Missed;
        }
        _renderer.material.SetFloat(FillLength, _fillLengths[hitIndex]);
        _firstActiveIndex = hitIndex + 1;
        for (int i = _firstActiveIndex; i < Mathf.Min(_firstActiveIndex + numActive, _targetInstances.Count); i++)
        {
            _targetInstances[i].State = TargetState.Active;
        }
        if (_firstActiveIndex == _targetInstances.Count)
        {
            Complete();
        }
    }

    private void Complete()
    {
        var hitTargets = _targetInstances.Count(target => target.State == TargetState.Hit);
        var score = 10 * (hitTargets + _targetInstances.Count - 1) / _targetInstances.Count;
        if (score > 5)
        {
            _canvas.Paint(_canvasIndex);
        }
        UI.manager.IncreaseScore(score);
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
            if (target != null)
            {
                Destroy(target.gameObject);
            }
        }
    }
}