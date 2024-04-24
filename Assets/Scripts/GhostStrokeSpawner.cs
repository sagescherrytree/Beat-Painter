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
            var offset = initData.mats[i].GetColumn(3);
            Vector3 translate = offset;
            translate = translate.normalized * 4f;
            var positions = strokePoints.strokes[initData.ids[i]].data
                .Select(v =>
                {
                    // var res = initData.mats[i].MultiplyPoint(v);
                    var res = gameObject.transform.TransformPoint(v) + translate;
                    return res;
                })
                .ToArray();
            var ghostStroke = Instantiate(ghostStrokePrefab).GetComponent<GhostStroke>();
            ghostStroke.Init(positions, canvas, i);
            yield return new WaitForSeconds(spawnRate);
        }
    }

}
