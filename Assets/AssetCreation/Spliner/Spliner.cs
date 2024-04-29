using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spliner : MonoBehaviour
{
    public int DisplayStroke;
    public static Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        Vector3 result = 0.5f *
                         ((2.0f * p1) +
                          (-p0 + p2) * t +
                          (2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3) * t2 +
                          (-p0 + 3.0f * p1 - 3.0f * p2 + p3) * t3);

        return result;
    }

    public static IEnumerable<Vector3> GenerateSpline(List<Vector3> points, int segments)
    {
        for (int i = 0; i < points.Count - 3; i++)
        {
            for (int j = 0; j < segments; j++)
            {
                float t = (float)j / segments;
                Vector3 point = CatmullRom(points[i], points[i + 1], points[i + 2], points[i + 3], t);
                yield return point;
            }
        }
    }
    
    public static IEnumerable<Vector3> EqualSpace(List<Vector3> points, float space)
    {
        List<float> segmentLengths = new List<float>();
        var totalLength = 0f;
        for (int i = 1; i < points.Count; i++)
        {
            var segmentLength = (points[i] - points[i - 1]).magnitude;
            totalLength += segmentLength;
            segmentLengths.Add(segmentLength);
        }

        var numSegments = (int)(totalLength / space);
        space = totalLength / numSegments;

        var segmentIndex = 0;
        var cumulativeLength = 0f;
        for (int i = 0; i < numSegments; i++)
        {
            var lengthAlongPolyline = space * i;
            while (lengthAlongPolyline > cumulativeLength + segmentLengths[segmentIndex])
            {
                cumulativeLength += segmentLengths[segmentIndex];
                segmentIndex++;
            }

            var t = (lengthAlongPolyline - cumulativeLength) / segmentLengths[segmentIndex];
            yield return Vector3.Lerp(points[segmentIndex], points[segmentIndex + 1], t);
        }

        yield return points[^1];
    }
    public static List<Vector3> BakedSplinePoints(List<Vector3> controlPoints)
    {
        int segments = 2;
        float space = 0.05f;
        controlPoints.Insert(0, controlPoints[0]);
        controlPoints.Add(controlPoints[^1]);
        List<Vector3> pointsAlongSpline = GenerateSpline(controlPoints, segments).ToList();
        // return pointsAlongSpline;
        List<Vector3> bakedPoints = EqualSpace(pointsAlongSpline, space).ToList();
        return bakedPoints;
    }

    void Start()
    {
        // Generate the spline
        var record = GetComponent<StrokePointsRecord>();
        var strokes = record.strokes;
        foreach (var stroke in strokes)
        {
            stroke.data = BakedSplinePoints(stroke.data);
        }
    }

    private void OnDrawGizmos()
    {
        var record = GetComponent<StrokePointsRecord>();
        var strokes = record.strokes;
        foreach (var point in strokes[DisplayStroke].data)
        {
            Gizmos.DrawSphere(point, 0.02f);
        }
    }

    private void Update()
    {
        UnityEditor.SceneView.RepaintAll();
    }
}
