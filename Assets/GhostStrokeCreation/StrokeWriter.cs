using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GhostStrokeCreation
{
    public class StrokeWriter
    {
        private StreamWriter _sr;

        public StrokeWriter(string filename)
        {
            _sr = File.CreateText(filename);
        }

        public void WriteStroke(List<Vector3> positions)
        {
            foreach (var position in positions)
            {
                _sr.Write(position.x);
                _sr.Write(",");
                _sr.Write(position.y);
                _sr.Write(",");
            }
            _sr.WriteLine();
        }

        public void Close()
        {
            _sr.Close();
        }
    }
}
