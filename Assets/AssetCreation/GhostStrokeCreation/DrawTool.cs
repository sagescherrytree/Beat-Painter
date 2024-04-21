using System.Collections.Generic;
using UnityEngine;

namespace GhostStrokeCreation
{
    public class DrawTool : MonoBehaviour
    {
        public Camera drawCam;
        public float simplifyTolerance;
        public List<Vector3> positions;
    
        private LineRenderer _line;
        private float _screenSpaceZ;

        // Start is called before the first frame update
        void Start()
        {
            _line = gameObject.GetComponent<LineRenderer>();
            _screenSpaceZ = drawCam.WorldToScreenPoint(Vector3.zero).z;
            NextStroke();
        }

        Vector3 GetMouseWorldPosition()
        {
            var mousePoint = Input.mousePosition;
            mousePoint.z = _screenSpaceZ;
            return drawCam.ScreenToWorldPoint(mousePoint);
        }

        public void NextStroke()
        {
            positions = new();
            _line.positionCount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                positions.Add(GetMouseWorldPosition());
            }
            if (Input.GetMouseButtonUp(0))
            {
                _line.Simplify(simplifyTolerance);
                positions.Clear();
                for (int i = 0; i < _line.positionCount; i++)
                {
                    positions.Add(_line.GetPosition(i));
                }
            }
        
            if (Input.GetMouseButton(0))
            {
                var mousePoint = GetMouseWorldPosition();

                if (positions[^1] != mousePoint)
                {            
                    positions.Add(mousePoint);
                    _line.positionCount = positions.Count;
                    _line.SetPositions(positions.ToArray());
                }
            }
        }
    }
}
