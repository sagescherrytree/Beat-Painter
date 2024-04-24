using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GhostStrokeSpawner : MonoBehaviour
{
    [SerializeField] private StrokePointsRecord strokePoints;
    [SerializeField] private StrokeInitData initData;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject ghostStrokePrefab;
    [SerializeField] private float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        var n = initData.ids.Count;
        for (int i = 0; i < n; i++)
        {
            var mat = initData.mats[i];
            var translate = (Vector3) mat.GetColumn(3).normalized * 4f;
            var rotate = Quaternion.LookRotation(mat.GetColumn(2), mat.GetColumn(1));
            
            var positions = strokePoints.strokes[initData.ids[i]].data
                .Select(v =>
                {
                    // var res = initData.mats[i].MultiplyPoint(v);
                    var res = rotate * gameObject.transform.TransformPoint(v) + translate;
                    return res;
                })
                .ToArray();
            var ghostStroke = Instantiate(ghostStrokePrefab).GetComponent<GhostStroke>();
            ghostStroke.Init(positions, canvas, i);
            yield return new WaitForSeconds(spawnRate);
        }
    }

}
