using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PaintBrush;

public class GhostStrokeSpawner : MonoBehaviour
{
    [SerializeField] private StrokePointsRecord strokePoints;
    [SerializeField] private StrokeInitData initData;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject ghostStrokePrefab;
    [SerializeField] private float spawnRate;
    [SerializeField] private float offset;

    [SerializeField] private PaintBrush brush;
    [SerializeField] private UI uiManager;
    [SerializeField] private ColorSwatch swatch;
    
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
            var translate = initData.displacements[i];
            var rotate = initData.rotations[i];
            var flipY = initData.flipY[i];
            var scale = 2f;
            
            var positions = strokePoints.strokes[initData.ids[i]].data
                .Select(v =>
                {
                    if (flipY)
                    {
                        v.y *= -1;
                    }
                    v = scale * (rotate * v);
                    v = gameObject.transform.TransformPoint(v);
                    v += translate;
                    return v;
                })
                .ToArray();
            var ghostStroke = Instantiate(ghostStrokePrefab).GetComponent<GhostStroke>();
            ghostStroke.Init(positions, canvas, i, swatch.strokeIdToColor[initData.ids[i]], swatch);
            yield return new WaitForSeconds(spawnRate);
        }
        
        int hits = brush.hits;
        UI.manager.SetScore(hits);
        if (hits < n) {
            UI.manager.LoseLevel();    
        } else {
            UI.manager.WinLevel();
        }       

    }

}
