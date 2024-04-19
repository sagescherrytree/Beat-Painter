using UnityEngine;

namespace GhostStrokeCreation
{
    public class TraceMain : MonoBehaviour
    {
        public string fileName;
        public DrawTool drawTool;
        public CanvasSwitcher canvasSwitcher;

        private StrokeWriter _writer;
        // Start is called before the first frame update
        void Start()
        {
            _writer = new StrokeWriter(fileName);
        }

        // Update is called once per frame
        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space))
            {
                return;
            }

            if (canvasSwitcher.HasNext())
            {
                _writer.WriteStroke(drawTool.positions);
                drawTool.NextStroke();
                canvasSwitcher.NextStroke();
                return;
            }
        
            _writer.Close();
            Debug.Log("All traces finished");
        }
    }
}
