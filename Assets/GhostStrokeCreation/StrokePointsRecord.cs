using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
[System.Serializable]
public class StrokePoints
{
    public List<Vector3> data;
}
public class StrokePointsRecord : MonoBehaviour
{
    public List<StrokePoints> strokes;
}