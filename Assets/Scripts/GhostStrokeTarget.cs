using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TargetState
{
    Active,
    Inactive,
    Hit,
    Missed,
}

public class GhostStrokeTarget : MonoBehaviour
{
    [SerializeField]
    private Material activeMat;
    [SerializeField]
    public Material inactiveMat;
    [SerializeField]
    public Material hitMat;
    [SerializeField]
    public Material missedMat;
    public TargetState State
    {
        get => _state;
        set
        {
            _state = value;
            var mat = value switch
            {
                TargetState.Active => activeMat,
                TargetState.Inactive => inactiveMat,
                TargetState.Hit => hitMat,
                TargetState.Missed => missedMat,
                _ => throw new Exception()
            };
            gameObject.GetComponent<MeshRenderer>().SetMaterials(new List<Material> { mat });
        }
    }

    private int _index;
    private GhostStroke _owner;
    private TargetState _state;

    public void Init(int index, GhostStroke owner)
    {
        _index = index;
        _owner = owner;
        State = TargetState.Inactive;
    }

    public void Hit()
    {
        if (State is TargetState.Hit or TargetState.Missed or TargetState.Inactive)
        {
            return;
        }
        State = TargetState.Hit;
        _owner.HitTarget(_index);
    }
}
