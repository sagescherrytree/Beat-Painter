using System.Collections.Generic;
using UnityEngine;

namespace GhostStrokeCreation
{
    public class TraceMain : MonoBehaviour
    {
        [SerializeField] private StrokePointsRecord strokePoints;
        [SerializeField] private DrawTool drawTool;
        [SerializeField] private CanvasSwitcher canvasSwitcher;
        

        // Update is called once per frame
        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space) || drawTool.positions.Count == 0)
            {
                return;
            }
            
            StrokePoints stroke = new StrokePoints();
            stroke.data = drawTool.positions;
            strokePoints.strokes.Add(stroke);
            drawTool.NextStroke();
            if (canvasSwitcher.HasNext())
            {
                canvasSwitcher.NextStroke();
                return;
            }
            
            Debug.Log("All traces finished");
        }
    }
}