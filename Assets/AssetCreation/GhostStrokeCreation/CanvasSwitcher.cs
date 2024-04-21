using UnityEngine;

namespace GhostStrokeCreation
{
    public class CanvasSwitcher : MonoBehaviour
    {
        public Material[] materials;
        private MeshRenderer _renderer;
        private int _materialIndex = 0;
    
        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material = materials[_materialIndex];
        }

        public void NextStroke()
        {
            _materialIndex = _materialIndex + 1;
            _renderer.material = materials[_materialIndex];
        }

        public bool HasNext()
        {
            return _materialIndex < materials.Length - 1;
        }
    }
}
